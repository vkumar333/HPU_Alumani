using Microsoft.AspNet.SignalR;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ChatHub
/// </summary>
public class ChatHub : Hub
{
    static List<Users> ConnectedUsers = new List<Users>();
    static List<Messages> CurrentMessage = new List<Messages>();
    ConnClass ConnC = new ConnClass();

    public void Connect(string userName)
    {
        var id = Context.ConnectionId;
        if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
        {
            string UserImg = GetUserImage(userName);
            string logintime = DateTime.Now.ToString();
            int Usercout = ConnectedUsers.Count;
            List<string> list = new List<string>();
            if (ConnectedUsers.Count == 0)
            {
                ConnectedUsers.Add(new Users { ConnectionId = id, UserName = userName, UserImage = UserImg, LoginTime = logintime });
            }
            else
                foreach (Users item in ConnectedUsers)
                {
                    if (userName == item.UserName)
                    {
                        break;
                    }
                    else
                    {
                        ConnectedUsers.Add(new Users { ConnectionId = id, UserName = userName, UserImage = UserImg, LoginTime = logintime });
                        break;
                    }
                }
            // send to caller
            Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);
            // send to all except caller client
            Clients.AllExcept(id).onNewUserConnected(id, userName, UserImg, logintime);
        }
    }

    public void SendMessageToAll(string userName, string message, string time)
    {
        string UserImg = GetUserImage(userName);
        // store last 100 messages in cache
        AddMessageinCache(userName, message, time, UserImg);

        // Broad cast message
        Clients.All.messageReceived(userName, message, time, UserImg);

    }

    private void AddMessageinCache(string userName, string message, string time, string UserImg)
    {
        CurrentMessage.Add(new Messages { UserName = userName, Message = message, Time = time, UserImage = UserImg });

        if (CurrentMessage.Count > 100)
            CurrentMessage.RemoveAt(0);

    }

    // Clear Chat History
    public void clearTimeout()
    {
        CurrentMessage.Clear();
    }

    //public string GetUserImage(string username)
    //{
    //    string RetimgName = "dummy.png";
    //    try
    //    {
    //        //string query = "select Photo from tbl_Users where UserName='" + username + "'";
    //        //string ImageName = ConnC.GetColumnVal(query, "Photo");
    //        //if (ImageName != "")
    //        //    RetimgName = "images/DP/" + RetimgName;
    //        //RetimgName = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/HPU_Alumni_PP__bc0e7d1f-1e80-4bd9-8f83-12cb1be46360_854602_20240529114405050PP_426818499_4384725.jpg";

    //        string query = @" SELECT top 1 Files_Unique_Name 
    //                                 FROM ALM_AlumniRegistration Ar
    //                                 INNER JOIN ALM_AlumniRegistration_File_dtl ad ON ar.pk_alumniid = ad.Fk_alumniid
    //                                 WHERE IsProfilePicOrDoc = 1 AND IsDeleted = 0 AND alumni_name = '" + username + "' order by ad.Pk_FileId DESC";

    //        string ImageName = ConnC.GetColumnVal(query, "Files_Unique_Name");

    //        if (ImageName != "")
    //        {
    //            RetimgName = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/" + ImageName;
    //        }
    //        else
    //        {
    //            RetimgName = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/HPU_Alumni_PP__bc0e7d1f-1e80-4bd9-8f83-12cb1be46360_854602_20240529114405050PP_426818499_4384725.jpg";
    //        }
    //    }
    //    catch (Exception ex)
    //    { }
    //    return RetimgName;
    //}

    public string GetUserImage(string username)
    {
        string retImgName = "";

        try
        {
            using (DataSet dsImg = getUserProfileImage(username).GetDataSet())
            {
                if (dsImg.Tables[0].Rows.Count > 0)
                {
                    string imgfileName = dsImg.Tables[0].Rows[0]["Files_Unique_Name"].ToString().Trim();
                    string imgftpURL = dsImg.Tables[0].Rows[0]["ftpImgURL"].ToString().Trim();

                    if (!string.IsNullOrEmpty(imgfileName))
                    {
                        retImgName = imgftpURL;
                    }
                    else
                    {
                        retImgName = "../Online/NoImage/default-user.jpg";
                    }
                }
                else
                {
                    retImgName = "../Online/NoImage/default-user.jpg";
                }
            }
        }
        catch (Exception ex)
        {
            // Optional: log exception
        }
        return retImgName;
    }

    public StoredProcedure getUserProfileImage(string userName)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("GetUserProfileImage", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Username", userName, DbType.String);
        return sp;
    }

    public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
    {
        var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
        if (item != null)
        {
            ConnectedUsers.Remove(item);

            var id = Context.ConnectionId;
            Clients.All.onUserDisconnected(id, item.UserName);

        }
        return base.OnDisconnected(stopCalled);
    }

    public void SendPrivateMessage(string toUserId, string message)
    {

        string fromUserId = Context.ConnectionId;

        var toUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
        var fromUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);

        if (toUser != null && fromUser != null)
        {
            string CurrentDateTime = DateTime.Now.ToString();
            string UserImg = GetUserImage(fromUser.UserName);
            // send to 
            Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message, UserImg, CurrentDateTime);

            // send to caller user
            Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message, UserImg, CurrentDateTime);
        }

    }

    public string ReturnPath()
    {
        try
        {
            string host = HttpContext.Current.Request.Url.Host;
            DataSet dsFilepath = new DataSet();
            dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
            foreach (DataRow dr in dsFilepath.Tables[0].Rows)
            {
                if (host == dr["Server_Ip"].ToString().Trim())
                {
                    return dr["http_Add"].ToString().Trim();
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}