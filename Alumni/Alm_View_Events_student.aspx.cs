using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using SubSonic;
using System.Data.SqlClient;

public partial class Alumni_Alm_View_Events_student : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    crypto cpt = new crypto();

    public object description { get; private set; }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable EventsDetails(int Pk_Eventsid)
    {
        ClearArrayLists();
        names.Add("@Pk_Eventsid"); types.Add(SqlDbType.NVarChar); values.Add(Pk_Eventsid);
        return Dobj.GetDataTable("Show_Events_Details_by_id", values, names, types);
    }

    private DataTable EventslatestDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("Show_Events_Details", values, names, types);
    }

    private DataTable CountEventsDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("Count_Events_Details", values, names, types);
    }

    private DataTable SearchEventsDetails()
    {
        ClearArrayLists();
        names.Add("@eventsname"); types.Add(SqlDbType.NVarChar); values.Add(description);
        return Dobj.GetDataTable("Search_Events_Details", values, names, types);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            crypto cpt = new crypto();
            string decId = cpt.DecodeString(Request.QueryString["ID"].ToString());
            int pk_Eventsid = Convert.ToInt32(decId);

            if (pk_Eventsid != 0)
            {
                EventsRepeter(pk_Eventsid);
                BindEventComments(pk_Eventsid);
                BindEventLikesCount(pk_Eventsid);
                getSocialURLLinks();
            }
        }
    }

    /// <summary>
    /// Bind Repeater
    /// </summary>
    /// <param name="Pk_Eventsid"></param>
    private void EventsRepeter(int Pk_Eventsid)
    {
        DataTable dt = new DataTable();
        dt = EventsDetails(Pk_Eventsid);
        if (dt.Rows.Count > 0)
        {
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            //RepeventsAll.DataSource = dt;
            //RepeventsAll.DataBind();

            string fileName = "";

            fileName = dt.Rows[0]["Filepath"].ToString().Trim();

            if (fileName != "")
            {
                if (dt.Rows[0]["Filepath"].ToString() != "None")
                {
                    Imge.Src = dt.Rows[0]["Filepath"].ToString();
                }
            }
            else
            {
                Imge.Src = "~/alumni/stuimage/No_image.png";
            }

            lblHeading.Text = dt.Rows[0]["Event_name"].ToString();
            lblStartDate.Text = dt.Rows[0]["Start_date"].ToString();
            lblDescription.Text = dt.Rows[0]["Description"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["eventRegistrationLink"].ToString()))
            {
                lblEventLink.Visible = true;
                lblEventLink.Text = "Event Registration Link";
                lblColon.Visible = true;
                lblColon.Text = " : ";
                lblEventLink.Style.Add("text-decoration", "underline");
                anchorEventLink.HRef = dt.Rows[0]["eventRegistrationLink"].ToString();
                anchorEventLink.Target = "_blank";
                anchorEventLink.InnerText = dt.Rows[0]["eventRegistrationLink"].ToString();
            }
            else
            {
                lblEventLink.Text = "";
                lblColon.Text = "";
                anchorEventLink.HRef = "#";
                anchorEventLink.InnerText = "";
            }
        }
    }

    protected void btnEventLike_Click(object sender, EventArgs e)
    {
        int eventId = Convert.ToInt32(cpt.DecodeString(Request.QueryString["ID"].ToString()));
        int userId = GetCurrentUserId();
        //LikeEvent(eventId, userId);
        ToggleLike(eventId, userId);
        BindEventLikesCount(eventId);
    }

    protected void btnEventComment_Click(object sender, EventArgs e)
    {
        int eventId = Convert.ToInt32(cpt.DecodeString(Request.QueryString["ID"].ToString()));
        int userId = GetCurrentUserId();
        string commentMsg = txtEventComment.Text.Trim();
        if (!string.IsNullOrEmpty(commentMsg))
        {
            AddEventComment(eventId, commentMsg, userId);
            BindEventComments(eventId);
            txtEventComment.Text = "";
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void BindEventComments(int eventId)
    {
        using (DataSet dsEventCmts = LoadEventComments(eventId).GetDataSet())
        {
            int commentsCount = dsEventCmts.Tables[0].Rows.Count;
            lblCommentsCount.Text = commentsCount == 0 ? "Comments " : "Comments (" + commentsCount + ")";

            if (dsEventCmts.Tables[0].Rows.Count > 0)
            {
                rptEventComments.DataSource = dsEventCmts.Tables[0];
                rptEventComments.DataBind();
            }
        }
    }

    protected void BindEventLikesCount(int eventId)
    {
        using (DataSet dsCnts = GetAllLikesCount(eventId).GetDataSet())
        {
            if (dsCnts.Tables[0].Rows.Count > 0)
            {
                var likesCount = dsCnts.Tables[0].Rows[0]["Cnt"].ToString();
                lblEventLikesCount.Text = likesCount != "0" ? "Likes: " + likesCount : "";
            }
            else
            {
                lblEventLikesCount.Text = "";
            }
        }
    }

    private int GetCurrentUserId()
    {
        int alumniuser = Convert.ToInt32(Session["AlumniID"].ToString());
        return alumniuser;
    }

    protected void AddEventComment(int eventId, string commentMsg, int userId)
    {
        if ((AddEventComments(eventId, txtEventComment.Text.Trim().ToString(), userId).Execute()) > 0)
        {
            ClientMessaging("Comments is Posted Successfully!");
        }
    }

    public List<Comment> GetCommentsForEvent(int eventId)
    {
        List<Comment> comments = new List<Comment>();

        using (DataSet dsCmts = LoadEventComments(eventId).GetDataSet())
        {
            if (dsCmts != null && dsCmts.Tables.Count > 0)
            {
                foreach (DataRow row in dsCmts.Tables[0].Rows)
                {
                    Comment comment = new Comment
                    {
                        pk_CommentID = Convert.ToInt32(row["pk_CommentID"]),
                        EventID = Convert.ToInt32(row["EventID"]),
                        CommentMsg = row["CommentMsg"].ToString(),
                        CreatedBy = Convert.ToInt32(row["CreatedBy"])
                    };
                    comments.Add(comment);
                }
            }
        }
        return comments;
    }

    protected void ToggleLike(int eventID, int createdBy)
    {
        bool userLiked = CheckIfUserLikedEvent(eventID, createdBy);

        if (userLiked)
        {
            UnlikeEvent(eventID, createdBy);
        }
        else
        {
            LikeEvent(eventID, createdBy);
        }
    }

    private bool CheckIfUserLikedEvent(int eventID, int createdBy)
    {
        DataTable dt = CheckIfUserLikedEvents(eventID, createdBy);
        int count = Convert.ToInt32(dt.Rows[0]["Cnt"].ToString());
        if (count > 0)
            return true;
        else
            return false;
    }

    public class Comment
    {
        public int pk_CommentID { get; set; }
        public int EventID { get; set; }
        public string CommentMsg { get; set; }
        public int CreatedBy { get; set; }
    }

    public static StoredProcedure LoadEventComments(int? EventID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Alumni_Events_Mst_Comments_Select", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@EventID", EventID, DbType.Int32);
        return sp;
    }

    public static StoredProcedure AddEventComments(int? EventID, string commentMsg, int? CreatedBy)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Alumni_Events_Mst_Comments_Ins", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@EventID", EventID, DbType.Int32);
        sp.Command.AddParameter("@CommentMsg", commentMsg, DbType.String);
        sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.Int32);
        return sp;
    }

    public static StoredProcedure GetAllLikesCount(int? EventID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Alumni_Events_Mst_LikesEvent_Count", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@EventID", EventID, DbType.Int32);
        return sp;
    }

    public static StoredProcedure LikeEvent(int? EventID, int? CreatedBy)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Alumni_Events_Mst_LikesEvent", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@EventID", EventID, DbType.Int32);
        sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.Int32);
        return sp;
    }

    private DataTable CheckIfUserLikedEvents(int PK_Events_id, int CreatedBy)
    {
        ClearArrayLists();
        names.Add("@EventID"); types.Add(SqlDbType.NVarChar); values.Add(PK_Events_id);
        names.Add("@CreatedBy"); types.Add(SqlDbType.NVarChar); values.Add(CreatedBy);
        return Dobj.GetDataTable("ALM_NewsStories_Details_Like_UnlikeEvent_Check", values, names, types);
    }

    private DataTable LikeEvent(int PK_Events_id, int CreatedBy)
    {
        ClearArrayLists();
        names.Add("@EventID"); types.Add(SqlDbType.NVarChar); values.Add(PK_Events_id);
        names.Add("@CreatedBy"); types.Add(SqlDbType.NVarChar); values.Add(CreatedBy);
        return Dobj.GetDataTable("ALM_Alumni_Events_Mst_LikesEvent", values, names, types);
    }

    private DataTable UnlikeEvent(int PK_Events_id, int CreatedBy)
    {
        ClearArrayLists();
        names.Add("@EventID"); types.Add(SqlDbType.NVarChar); values.Add(PK_Events_id);
        names.Add("@CreatedBy"); types.Add(SqlDbType.NVarChar); values.Add(CreatedBy);
        return Dobj.GetDataTable("ALM_Alumni_Events_Mst_Likes_UnlikeEvent", values, names, types);
    }

    #region "Get Social URL Links"

    private void getSocialURLLinks()
    {
        try
        {
            using (DataSet dsURL = getSocialMediaURLLinks().GetDataSet())
            {
                if (dsURL.Tables.Count > 0 && dsURL.Tables[0].Rows.Count > 0)
                {
                    int count = dsURL.Tables[0].Rows.Count;

                    if (dsURL.Tables[0].Rows.Count > 0)
                    {
                        facebookLink.HRef = dsURL.Tables[0].Rows[0]["facebookLink"].ToString();
                        facebookLink.Target = "_blank";
                        twitterLink.HRef = dsURL.Tables[0].Rows[0]["twitterLink"].ToString();
                        twitterLink.Target = "_blank";
                        linkedInLink.HRef = dsURL.Tables[0].Rows[0]["linkedInLink"].ToString();
                        linkedInLink.Target = "_blank";
                        youtubeLink.HRef = dsURL.Tables[0].Rows[0]["youtubeLink"].ToString();
                        youtubeLink.Target = "_blank";
                    }
                    else
                    {
                        facebookLink.HRef = "#";
                        twitterLink.HRef = "#";
                        linkedInLink.HRef = "#";
                        youtubeLink.HRef = "#";
                    }
                }
            }
        }
        catch (SqlException sqlEx)
        {
            // Handle SQL related errors here
            ClientMessaging("SQL Error: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            // Handle any other errors here
            ClientMessaging("Error: " + ex.Message);
        }
    }

    public StoredProcedure getSocialMediaURLLinks()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Social_Media_Links_Info", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    #endregion
}