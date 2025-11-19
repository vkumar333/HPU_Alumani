using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using SubSonic;
using IUMSNXG;
using DataAccessLayer;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class Alumni_ALM_Alumni_Home : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }
	
	protected void Page_PreRender(object sender, EventArgs e)
    {
        int count;
        phBadge.Visible = int.TryParse(lblNewReceivedMsgCount.Text.Trim(), out count) && count > 0;
    }

    #region "Global Declaration"

    DataAccess objDAO = new DataAccess();
    CustomMessaging eobj = new CustomMessaging();
    CommonFunction cfObj = new CommonFunction();
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["IUMSNXG"].ConnectionString);
    crypto cpt = new crypto();

    #endregion

    private bool IspageReferesh = false;
	public string userSessionAlumniID { get; set; }
	
	public int pk_MsgID { get; set; }
    public string xmlDoc { get; private set; }
    public string mode { get; private set; }
    public int senderMsgAlumniID { get; set; }
    public int receiverMsgAlumniID { get; set; }
    public int mReqID { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        checkbuttonenable();

        ////CheckMentor();
        ////CheckMenteee();
        //////GetCurrentEventRepeter();
        //////RepeterNotices();
        ////SliderRepeter();
        //////GetCurrentEventRepeter();
        ////GetAchieverss();
        ////NewsAndStoriesRepeter();
        ////GetAllEventsRepeater();
        ////vacancciesRepeter();
        ////ImagesRepeter();
        ////birthRepeter();
        ////fillNoticeBoards();
        //////fillNoticeBoardss();

        //if (!IsPostBack)
        //{
        //    if (Session["AlumniID"] != null)
        //    {
        // ScriptManager.RegisterClientScriptInclude( this.GetType(), "script", "alert('Hi');", true);

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Javascript", "alert('Hi')", true);
        //ViewState["postid"] = System.Guid.NewGuid().ToString();
        //Session["postid"] = ViewState["postid"].ToString();

        //    getUser();

        //    string onlineStatusStr = "UPDATE ALM_AlumniRegistration SET OnlineStatus = 1 WHERE pk_alumniid = @user";
        //    SqlCommand comm = new SqlCommand(onlineStatusStr, conn);
        //    comm.Parameters.AddWithValue("@user", Server.HtmlEncode(CurrentSenderId.Text));
        //    conn.Open();
        //    comm.ExecuteNonQuery();
        //    conn.Close();

        //    LoadChatList();
        //    MsgPanel.Visible = false;
        //    MSGTextBox.Focus();
        //    LoadingImage.Attributes.CssStyle.Add("opacity", "0");
        //}
        //else
        //{
        //if (ViewState["postid"].ToString() != Session["postid"].ToString())
        //{
        //    IspageReferesh = true;
        //}
        //Session["postid"] = System.Guid.NewGuid().ToString();
        //ViewState["postid"] = Session["postid"];
        //    }
        //}

        //int x = checkNewMessage();
        //UnreadMsgCountLabel.Text = x.ToString();
        //if (x != 0)
        //{
        //    LoadChatList();
        //    this.Title = x.ToString() + "new messages";
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Javascript", "javascript:playSound();", true);
        //}
        //else
        //{
        //    this.Title = "Chat";
        //}
    }

    protected void checkbuttonenable()
    {
        if (Session["AlumniID"] != null)
        {
            if (Session["AlumniID"].ToString() != "")
            {
                int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
                DataSet ds = HPU_ALM_Paymentcheck(alumniId).GetDataSet();
                var count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {
					//lnkpay.Visible = false;
                    CheckMentor();
                    CheckMenteee();
                    //GetCurrentEventRepeter();
                    //RepeterNotices();
                    SliderRepeter();
                    GetNotableAlumni();
                    //GetCurrentEventRepeter();
                    GetAchieverss();
                    NewsAndStoriesRepeter();
                    GetAllEventsRepeater();
                    vacancciesRepeter();
                    ImagesRepeter();
                    birthRepeter();
                    //fillNoticeBoards();
                    fillNoticeBoardss();
                    RepeterData();
                    VideosRepeter();
					
					userSessionAlumniID = Session["AlumniID"].ToString();
					
					mentorshipMsgDetails();
                    //MentorMessages();
                    LoadNotifications();
                }
                else
                {
                    //lnkpay.Visible = true;
                    Response.Redirect("../Alumni/ALM_AlumniPayLater.aspx");
                }
            }
        }
    }
	
	private void LoadNotifications()
    {
        try
        {
            int loggedUserAlumniID = Convert.ToInt32(Session["AlumniID"].ToString());

            bool isloggedUserMentor = checkIfUserIsMentor(loggedUserAlumniID);
            bool isloggedUserMentee = checkIfUserIsMentee(loggedUserAlumniID);

            if (isloggedUserMentor)
            {
                int newReceivedMessagesCount = getCountedMenteeRequestMessagesOnPortal();

                if (newReceivedMessagesCount > 0)
                {
                    lnkMenteeRequest.Text = "You have new mentee request received for mentorship.";
                    lblNewReceivedMsgCount.Text = newReceivedMessagesCount.ToString();
                }
                else
                {
                    lnkMenteeRequest.Text = "No mentee requests have been received for mentorship.";
                    lblNewReceivedMsgCount.Text = "";
                }
			}
            else
            {
                lnkMenteeRequest.Text = "";
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private int getCountedMenteeRequestMessagesOnPortal()
    {
        string userSessionAlumniID = Session["AlumniID"] != null ? Session["AlumniID"].ToString() : "0";
        int alumniId = Convert.ToInt32(userSessionAlumniID);
        DataSet ds = getCountedMenteeRequestMessages(alumniId).GetDataSet();
        int count = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CountUnReceivedMenteeRequest"].ToString()) ? Convert.ToInt32(ds.Tables[0].Rows[0]["CountUnReceivedMenteeRequest"].ToString()) : 0;
        return count;
    }
	
	private bool checkIfUserIsMentor(int mentorAlumniID)
    {
        if (mentorAlumniID > 0)
        {
            int alumniId = mentorAlumniID;
            DataSet ds = HPU_ALM_Mentorcheck(alumniId).GetDataSet();

            return ds.Tables[0].Rows.Count > 0;
        }
        return false;
    }

    private bool checkIfUserIsMentee(int menteeAlumniID)
    {
        if (menteeAlumniID > 0)
        {
            int alumniId = menteeAlumniID;
            DataSet ds = ALM_MenteeStatuscheck(alumniId).GetDataSet();

            return ds.Tables[0].Rows.Count > 0;
        }
        return false;
    }

    public StoredProcedure HPU_ALM_Mentorcheck(int pk_Regid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_ApproveMentorcheck", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
        return sp;
    }
	
    public StoredProcedure HPU_ALM_Paymentcheck(int pk_Regid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Paymentcheck", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
        return sp;
    }

    public StoredProcedure ALM_MentorStatuscheck(int pk_Regid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Mentortcheck", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure GetUpcomingBirthdaysAlumniss()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Alumni_GetUpcomingBirthdays", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Today", DateTime.Today, DbType.DateTime);
        sp.Command.AddParameter("@EndOfThisMonth", DateTime.Today.AddMonths(1).AddDays(-DateTime.Today.Day), DbType.Int32);
        return sp;
    }
	
	public StoredProcedure getCountedMenteeRequestMessages(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Un_Read_Mentee_Requests_Received_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
        return sp;
    }

    [WebMethod]
    public static List<object> GetCurrentEventDtls()
    {
        List<object> list = new List<object>();

        string Pk_userId = HttpContext.Current.Session["AlumniId"] != null ? HttpContext.Current.Session["AlumniId"].ToString() : "";
        DataSet ds = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(Pk_userId.Length > 0 ? Convert.ToInt32(Pk_userId) : 0).GetDataSet();
        if (ds.Tables[2].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[2].Rows)
            {
                string IsImg = dr["File_name"].ToString();
                var obj = new
                {
                    PkId = dr["PK_Events_id"].ToString(),
                    EventName = dr["Event_name"].ToString(),
                    Description = dr["Description"].ToString(),
                    Startdate = dr["Start_date"].ToString(),
                    //IsEventPaid = Convert.ToBoolean(dr["IsEventPaid"]) == true ? "visible" : "hidden",
                    //ISAlReadyApplied = Convert.ToBoolean(dr["ISAlReadyApplied"]) == true ? "Yes" : "No",
                    //ImgSrc = SetServiceDoc(IsImg.Length > 0 ? dr["File_name"].ToString() : "")
                    ImgSrc = dr["File_Url"].ToString()

                };
                list.Add(obj);
            }
        }
        return list;
    }

    private void GetCurrentEventRepeter()
    {
        string Pk_userId = HttpContext.Current.Session["AlumniId"] != null ? HttpContext.Current.Session["AlumniId"].ToString() : "";
        DataSet ds = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(Pk_userId.Length > 0 ? Convert.ToInt32(Pk_userId) : 0).GetDataSet();
        if (ds.Tables[2].Rows.Count > 0)
        {
            RepeaterEvents.DataSource = ds.Tables[2];
            RepeaterEvents.DataBind();
        }
    }

    private void GetAllEventsRepeater()
    {
        //string Pk_userId = HttpContext.Current.Session["AlumniId"] != null ? HttpContext.Current.Session["AlumniId"].ToString() : "";
        //DataSet ds = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(Pk_userId.Length > 0 ? Convert.ToInt32(Pk_userId) : 0).GetDataSet();
        //if (ds.Tables[2].Rows.Count > 0)
        //{
        //    repeaterEventss.DataSource = ds.Tables[2];
        //    repeaterEventss.DataBind();
        //}

        crypto cpt = new crypto();
        string Pk_userId = HttpContext.Current.Session["AlumniId"] != null ? HttpContext.Current.Session["AlumniId"].ToString() : "";
        DataSet dsE = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(Pk_userId.Length > 0 ? Convert.ToInt32(Pk_userId) : 0).GetDataSet();
        dsE.Tables[2].Columns.Add("encId");
        int rnum = (dsE.Tables[2].Rows.Count) + 1;

        if (dsE.Tables[2].Rows.Count > 0)
        {
            for (int x = 0; x < dsE.Tables[2].Rows.Count; x++)
            {
                string pkid = dsE.Tables[2].Rows[x]["PK_Events_id"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dsE.Tables[2].Rows[x]["encId"] = encId;
            }
            repeaterEventss.DataSource = dsE.Tables[2];
            repeaterEventss.DataBind();
        }
    }

    //[WebMethod]
    //public static List<object> GetNoticAlert()
    //{
    //    List<object> list = new List<object>();
    //    DataSet ds = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();
    //    if (ds.Tables[1].Rows.Count > 0)
    //    {
    //        foreach (DataRow dr in ds.Tables[1].Rows)
    //        {
    //            var obj = new
    //            {
    //                Newsid = dr["Newsid"].ToString(),
    //                NewsTitle = dr["NewsTitle"].ToString(),
    //                Newsdetail = dr["Newsdetail"].ToString(),
    //                PublishDate = dr["PublishDate"].ToString()

    //            };
    //            list.Add(obj);
    //        }
    //    }
    //    return list;
    //}

    [WebMethod]
    public static List<GetCureentVacancy> GetCurrentVaCancy()
    {
        List<GetCureentVacancy> list = new List<GetCureentVacancy>();
        DataSet ds = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();
        if (ds.Tables[4].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[4].Rows)
            {
                GetCureentVacancy obj = new GetCureentVacancy();
                obj.Pk_JobPostedId = Convert.ToInt32(dr["Pk_JobPostedId"]);
                obj.CompanyName = dr["CompanyName"].ToString();
                obj.Designation = dr["Designation"].ToString();
                obj.OpenFrom = dr["OpenFrom"].ToString();
                obj.OpenTo = dr["OpenTo"].ToString();
                obj.SkillsReq = dr["SkillsReq"].ToString();
                obj.SelectionProcess = dr["SelectionProcess"].ToString();
                obj.JobVacncyUrl = dr["JobVacncyUrl"].ToString();
                list.Add(obj);
                //id.HRef = "MyPage.aspx?id=" + obj.Pk_JobPostedId;
            }
        }
        return list;
    }

    [WebMethod]
    public static List<object> GetAcheivers()
    {
        List<object> list = new List<object>();

        DataSet ds = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string IsImg = dr["Files_Unique_Name"].ToString();
                var obj = new
                {
                    alumniid = dr["Pk_alumniid"],
                    alumni_name = dr["alumni_name"],
                    Achievement = dr["Achievement"],
                    FilesU_Name = dr["Files_Unique_Name"],
                    Files_Name = dr["Files_Name"],
                    file_Url = dr["file_Url"],
                    ImgSrc = SetServiceDoc(IsImg.Length > 0 ? dr["Files_Unique_Name"].ToString() : "")
                };
                list.Add(obj);
            }
        }
        return list;
    }

    //[WebMethod]
    //public static List<object> GetDetailsOfClicked_Element(int Pk_id, string DtlsOf)
    //{
    //    List<object> list = new List<object>();

    //    string Pk_userId = HttpContext.Current.Session["AlumniId"] != null ? HttpContext.Current.Session["AlumniId"].ToString() : "";
    //    DataSet ds = IUMSNXG.SP.ALM_Select_Alumni_DashBoard_Dtls(Pk_id, DtlsOf, Pk_userId.Length > 0 ? Convert.ToInt32(Pk_userId) : 0).GetDataSet();
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        foreach (DataRow dr in ds.Tables[0].Rows)
    //        {

    //            if (DtlsOf == "A")//for Acheivers
    //            {
    //                // pk_alumniid= dr["pk_alumniid"],
    //                // Pk_FileId= dr["Pk_FileId"],
    //                // IsAcheiverProcessed= dr["IsAcheiverProcessed"],
    //                // File_Path= dr["File_Path"],
    //                // ContactNo= dr["ContactNo"],

    //                string IsImg = dr["Files_Unique_Name"].ToString();
    //                var obj = new
    //                {


    //                    alumni_name = dr["alumni_name"],
    //                    FilesUName = dr["Files_Unique_Name"],
    //                    Files_Name = dr["Files_Name"],
    //                    FileExtension = dr["FileExtension"],

    //                    Degree = dr["Degree"],
    //                    College = dr["College"],
    //                    email = dr["email"],

    //                    currentoccupation = dr["currentoccupation"],
    //                    Achievement = dr["Achievement"],
    //                    special_interest = dr["special_interest"],
    //                    yearofpassing = dr["yearofpassing"],
    //                    ImgSrc = SetServiceDoc(IsImg.Length > 0 ? dr["Files_Unique_Name"].ToString() : "")


    //                };
    //                list.Add(obj);

    //            }

    //            if (DtlsOf == "N")//for Notice/Alert
    //            {
    //                var obj = new
    //                {

    //                    NewsTitle = dr["NewsTitle"],
    //                    Newsdetail = dr["Newsdetail"],
    //                    PublishDate = dr["PublishDate"]

    //                };
    //                list.Add(obj);
    //            }

    //            if (DtlsOf == "E")//for Current Events
    //            {
    //                var obj = new
    //                {
    //                    Event_Name = dr["Event_Name"],
    //                    Event_Descripion = dr["Event_Descripion"],
    //                    IsEventPaid = Convert.ToBoolean(dr["IsEventPaid"]) == true ? "Yes" : "No",
    //                    StartDate = dr["StartDate"],
    //                    EndDate = dr["EndDate"],
    //                    EventCharge = dr["EventCharge"].ToString() != "" ? dr["EventCharge"].ToString() : "N/A",
    //                    ISAlReadyApplied = Convert.ToBoolean(dr["ISAlReadyApplied"]) == true ? "You have Already Applied This Event!" : "",

    //                };
    //                list.Add(obj);
    //            }

    //            if (DtlsOf == "P")//for previous Events
    //            {
    //                var obj = new
    //                {
    //                    Event_Name = dr["Event_Name"],
    //                    Event_Descripion = dr["Event_Descripion"],
    //                    IsEventPaid = Convert.ToBoolean(dr["IsEventPaid"]) == true ? "Yes" : "No",
    //                    StartDate = dr["StartDate"],
    //                    EndDate = dr["EndDate"],
    //                    EventCharge = dr["EventCharge"].ToString() != "" ? dr["EventCharge"].ToString() : "N/A"

    //                };
    //                list.Add(obj);
    //            }

    //            if (DtlsOf == "V")//for Vacancy
    //            {
    //                var obj = new
    //                {
    //                    CompanyName = dr["CompanyName"],
    //                    Designation = dr["Designation"],
    //                    VacancyDtl = dr["VacancyDtl"],
    //                    SkillsReq = dr["SkillsReq"],
    //                    SelectionProcess = dr["SelectionProcess"],
    //                    JobVacncyUrl = dr["JobVacncyUrl"].ToString().Length > 0 ? dr["JobVacncyUrl"].ToString() : "N/A"


    //                };
    //                list.Add(obj);
    //            }

    //            if (DtlsOf == "EA")//Apply Event which is not paid
    //            {
    //                var obj = new
    //                {

    //                    Event_Name = dr["Event_Name"],
    //                    Event_Descripion = dr["Event_Descripion"],
    //                    IsEventPaid = Convert.ToBoolean(dr["IsEventPaid"]) == true ? "Yes" : "No",
    //                    StartDate = dr["StartDate"],
    //                    EndDate = dr["EndDate"],
    //                    EventCharge = dr["EventCharge"].ToString() != "" ? dr["EventCharge"].ToString() : "N/A"


    //                };
    //                list.Add(obj);
    //            }


    //            if (DtlsOf == "EAP")//Apply Event which is not paid
    //            {
    //                var obj = new
    //                {

    //                    Event_Name = dr["Event_Name"],
    //                    Event_Descripion = dr["Event_Descripion"],
    //                    IsEventPaid = Convert.ToBoolean(dr["IsEventPaid"]) == true ? "Yes" : "No",
    //                    StartDate = dr["StartDate"],
    //                    EndDate = dr["EndDate"],
    //                    EventCharge = dr["EventCharge"].ToString() != "" ? dr["EventCharge"].ToString() : "N/A"


    //                };
    //                list.Add(obj);
    //            }
    //        }
    //    }
    //    return list;

    //}

    public static dynamic GetImageUrl(byte[] ImageAttachedBytes, string ContentType)
    {
        string src = null;
        if (ImageAttachedBytes != null)
        {
            string base64String = Convert.ToBase64String(ImageAttachedBytes);
            src = "data:image/jpg;base64," + base64String;
        }
        else
        {
            src = "~/alumni/stuimage/No_image.png";
        }
        return src;
    }

    [WebMethod]
    public static int SaveUnpaid_EventApply_Dtls(int Pk_Eid)
    {
        int Result = 0;
        try
        {
            string AlumniId = HttpContext.Current.Session["AlumniId"] != null ? HttpContext.Current.Session["AlumniId"].ToString() : "";
            if (AlumniId.Length > 0)
            {
                if ((IUMSNXG.SP.Alm_Alumni_UnpaidEventApply(Pk_Eid, Convert.ToInt32(AlumniId)).Execute()) > 0)
                {
                    Result = 1;
                }
                else
                {
                    Result = 0;
                }
            }
        }
        catch (Exception ex)
        {
            Result = 0;
        }
        return Result;
    }

    [WebMethod]
    public static int Save_paid_EventApply_Dtls(int Pk_Eid)
    {
        int Result = 0;
        try
        {
            string AlumniId = HttpContext.Current.Session["AlumniId"] != null ? HttpContext.Current.Session["AlumniId"].ToString() : "";
            if (AlumniId.Length > 0)
            {
                DataTable dt = (IUMSNXG.SP.Alm_Alumni_paidEventApply(Pk_Eid, Convert.ToInt32(AlumniId))).GetDataSet().Tables[0];
                if (dt.Rows.Count > 0)
                {
                    HttpContext.Current.Session["EventPmtDtl"] = null;
                    HttpContext.Current.Session["EventPmtDtl"] = dt;
                    Result = 1;
                }
                else
                {
                    Result = 0;
                }

            }
        }
        catch (Exception ex)
        {
            Result = 0;
        }
        return Result;
    }

    protected void Client_Messaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
        //Anthem.Manager.AddScriptForClientSideEval("alert('" + msg + "');");
    }

    //it will return file based on file unique name
    public static string SetServiceDoc(string FileName)
    {
        if (FileName.Length > 0)
        {
            string FolderName = @"/Alumni";
            string host = HttpContext.Current.Request.Url.Host;
            string FilePath = "";
            DataSet dsFilepath = new DataSet();
            dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
            foreach (DataRow dr in dsFilepath.Tables[0].Rows)
            {
                if (host == dr["Server_Ip"].ToString().Trim())
                {
                    if (host != "localhost")
                    {
                        FilePath = dr["http_Add"].ToString().Trim();
                        FilePath = @FilePath + FolderName + @"/" + FileName;
                        // FilePath = FolderName + FileName;
                    }
                    else
                    {
                        FilePath = dr["Physical_Path"].ToString().Trim();
                        FilePath = @FilePath + FolderName + @"/" + FileName;
                        //  FilePath = FolderName  + FileName;
                    }
                    //return FolderName+FileName;
                    return FilePath;
                }
            }
            return FilePath;
        }
        else
        {
            return "~/alumni/stuimage/No_image.png"; //return "stuimage/No_image.png";
        }
    }
	
    protected void CheckMentor()
    {
        try
        {
            if (Session["AlumniID"] != null)
            {
                if (Session["AlumniID"].ToString() != "")
                {
                    int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
                    DataSet ds = ALM_MentorStatuscheck(alumniId).GetDataSet();
                    var count = ds.Tables[0].Rows.Count;
                    if (count > 0)
                    {
                        //lnkGenAlumniCard.Visible = true;
                        //LinkNotification.Visible = true;
                    }
                    else
                    {
                        //lnkGenAlumniCard.Visible = true;
                        //LinkNotification.Visible = false;
                    }
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Alumni/ALM_Default.aspx");
        }
    }
	
    public StoredProcedure ALM_MenteeStatuscheck(int pk_Regid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Menteecheck", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
        return sp;
    }
	
	public StoredProcedure getMentorMessages(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_MentorMessages_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
        return sp;
    }

    public StoredProcedure getMenteeMessages(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_MenteeMessages_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
        return sp;
    }
	
	public StoredProcedure getReceivedMessages(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Received_Messages_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
        return sp;
    }
	
    protected void CheckMenteee()
    {
        try
        {
			if (Session["AlumniID"] != null)
            {
                if (Session["AlumniID"].ToString() != "")
                {
                    int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
                    DataSet ds = ALM_MenteeStatuscheck(alumniId).GetDataSet();
                    var count = ds.Tables[0].Rows.Count;
                    if (count > 0)
                    {
                        //lnkGenAlumniCard.Visible = true;
                        //NotifishowMentor.Visible = true;
                        gvAlumni.DataSource = ds;
                        gvAlumni.DataBind();
                    }
                    else
                    {
                        //lnkGenAlumniCard.Visible = true;
                        //NotifishowMentor.Visible = false;
                        //lnkBtnMenteeMsg.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../Alumni/ALM_Default.aspx");
        }
    }

    protected void MentorMessages()
    {
        try
        {
            if (Session["AlumniID"] != null)
            {
                if (Session["AlumniID"].ToString() != "")
                {
                    //int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
                    //DataSet ds = getMentorMessages(alumniId).GetDataSet();
                    //var count = ds.Tables[0].Rows.Count;
                    //if (count > 0)
                    //{
                    //    gvMentorRequests.DataSource = ds;
                    //    gvMentorRequests.DataBind();
                    //}
                    //else
                    //{

                    //}

                    List<MentorRequest> mentorRequests = GetMentorRequests();
                    rptMentorRequests.DataSource = mentorRequests;
                    rptMentorRequests.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Client_Messaging(ex.Message.ToString());
        }
    }
	
    public List<MentorRequest> GetMentorRequests()
    {
        List<MentorRequest> listRequests = new List<MentorRequest>();

        string userSessionAlumniID = Session["AlumniID"] != null ? Session["AlumniID"].ToString() : "0";

        int alumniId = Convert.ToInt32(userSessionAlumniID);
        //DataSet dsS = getMentorMessages(alumniId).GetDataSet();
        DataSet dsS = getReceivedMessages(alumniId).GetDataSet();

        if (dsS.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drMR in dsS.Tables[0].Rows)
            {
                MentorRequest objMR = new MentorRequest();
                objMR.pk_MReqID = Convert.ToInt32(drMR["pk_MReqID"]);
                objMR.senderID = Convert.ToInt32(drMR["senderID"].ToString());
                objMR.senderName = drMR["senderName"].ToString();
                objMR.messageText = drMR["messageText"].ToString();
                objMR.sentAt = drMR["sentAt"].ToString();
                objMR.status = drMR["status"].ToString();
                objMR.isVisible = Convert.ToBoolean(drMR["isVisible"].ToString());
                objMR.isSendMsgNow = Convert.ToBoolean(drMR["isSendMsgNow"].ToString());
                objMR.isAccepted = Convert.ToBoolean(drMR["isAccepted"].ToString());
                listRequests.Add(objMR);
            }
        }
        return listRequests;
    }

    protected void MenteeMessages()
    {
        try
        {
            if (Session["AlumniID"] != null)
            {
                if (Session["AlumniID"].ToString() != "")
                {
                    //int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
                    //DataSet ds = getMenteeMessages(alumniId).GetDataSet();
                    //var count = ds.Tables[0].Rows.Count;
                    //if (count > 0)
                    //{
                    //    //gvMenteeMessages.DataSource = ds;
                    //    //gvMenteeMessages.DataBind();
                    //}
                    //else
                    //{

                    //}

                    //List<Message> messages = new List<Message>();

                    //DataSet ds = getMenteeMessages(Convert.ToInt32(Session["AlumniID"].ToString())).GetDataSet();

                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    foreach (DataRow dr in ds.Tables[0].Rows)
                    //    {
                    //        Message obj = new Message();
                    //        obj.pk_MsgID = Convert.ToInt32(dr["pk_MsgID"]);
                    //        obj.messageText = dr["messageText"].ToString();
                    //        obj.userName = dr["userName"].ToString();
                    //        obj.FormattedMessage = string.Format("{0} : {1} (sent at {2})", dr["userName"].ToString(), dr["messageText"].ToString(), dr["sentAt"].ToString());
                    //        messages.Add(obj);
                    //    }
                    //}

                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    foreach (DataRow dr in ds.Tables[0].Rows)
                    //    {
                    //        Message obj = new Message();
                    //        int pk_MsgID = Convert.ToInt32(dr["pk_MsgID"]);
                    //        string messageText = dr["messageText"].ToString();
                    //        string userName = dr["userName"].ToString();
                    //        string sentAt = dr["sentAt"].ToString();
                    //        string formatted = string.Format("{0} : {1} (sent at {2})", userName, messageText, sentAt);
                    //        messages.Add(new Message { FormattedMessage = formatted });
                    //    }
                    //}

                    DataSet ds = getMenteeMessages(Convert.ToInt32(Session["AlumniID"].ToString())).GetDataSet();
                    var count = ds.Tables[0].Rows.Count;

                    if (count > 0)
                    {
                        rptMenteeMessages.DataSource = ds.Tables[0];
                        rptMenteeMessages.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Client_Messaging(ex.Message.ToString());
        }
    }

    protected void LinkNotification_Click(object sender, EventArgs e)
    {
        CheckMentor();
        Response.Redirect("../Alumni/Alm_Mentees_Assigned.aspx");
    }

    protected void NotifishowMentor_Click(object sender, EventArgs e)
    {
        CheckMenteee();
    }

    protected void gvAlumni_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void gvAlumni_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    protected DataSet FillNoticeBoardsDtls()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("ALM_Get_Notiboard_Dtls", values, names, types);
    }

    [WebMethod]
    public static List<GetNoticeBoards> GetNoticeBoardLists()
    {
        List<GetNoticeBoards> lists = new List<GetNoticeBoards>();
        Alumni_ALM_Alumni_Home objnew = new Alumni_ALM_Alumni_Home();
        DataSet dsS = objnew.FillNoticeBoardsDtls();
        if (dsS.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drR in dsS.Tables[0].Rows)
            {
                GetNoticeBoards objs = new GetNoticeBoards();
                string IsImgs = drR["filename"].ToString();
                objs.pk_boardid = Convert.ToInt32(drR["Pk_Board_ID"]);
                objs.heading = drR["Heading"].ToString();
                objs.description = drR["Description"].ToString();
                objs.daydate = drR["DayDate"].ToString();
                objs.monthname = drR["Month_Name"].ToString();
                objs.filepath = drR["filepath"].ToString();
                objs.filename = drR["filename"].ToString();
                objs.ImgsSrc = SetServiceDoc(IsImgs.Length > 0 ? drR["filename"].ToString() : "");
                lists.Add(objs);
            }
        }
        return lists;
    }

    protected string GetActiveClass(int ItemIndex)
    {
        if (ItemIndex == 0)
        {
            return "active";
        }
        else
        {
            return "";
        }
    }

    private void SliderRepeter()
    {
        DataTable dt = new DataTable();
        dt = SliderDetails();
        {
            // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\");
            // string[] filePaths = Directory.GetFiles("D:\\Alumni\\UploadSliders\\");
            sliderRepeater.DataSource = dt;
            sliderRepeater.DataBind();
        }
    }

    private DataTable SliderDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_Show_slider", values, names, types);
    }

    private void ImagesRepeter()
    {
        DataTable dt = new DataTable();
        dt = GetGellary();
        if (dt.Rows.Count > 0)
        {
            GalleryImages.DataSource = dt;
            GalleryImages.DataBind();
        }
    }

    private DataTable GetGellary()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GetGellaryRecord", values, names, types);
    }

    private void GetAchieverss()
    {
        //DataSet dsA = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();
        //{
        //    achieversRepeater.DataSource = dsA.Tables[0];
        //    achieversRepeater.DataBind();
        //}

        crypto cpt = new crypto();
        DataSet dsA = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();
        dsA.Tables[0].Columns.Add("encId");
        int rnum = (dsA.Tables[0].Rows.Count) + 1;

        if (dsA.Tables[0].Rows.Count > 0)
        {
            for (int x = 0; x < dsA.Tables[0].Rows.Count; x++)
            {
                string pkid = dsA.Tables[0].Rows[x]["pk_alumniid"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dsA.Tables[0].Rows[x]["encId"] = encId;
            }
            achieversRepeater.DataSource = dsA.Tables[0];
            achieversRepeater.DataBind();
        }
    }

    private DataTable GetAchievers()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GetGellaryRecord", values, names, types);
    }

    private void vacancciesRepeter()
    {
        //DataSet ds = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();
        //if (ds.Tables[4].Rows.Count > 0)
        //{
        //    vaccanciesRepeater.DataSource = ds.Tables[4];
        //    vaccanciesRepeater.DataBind();
        //}

        crypto cpt = new crypto();
        DataSet dsV = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();
        dsV.Tables[4].Columns.Add("encId");
        int rnum = (dsV.Tables[4].Rows.Count) + 1;

        if (dsV.Tables[4].Rows.Count > 0)
        {
            for (int x = 0; x < dsV.Tables[4].Rows.Count; x++)
            {
                string pkid = dsV.Tables[4].Rows[x]["Pk_JobPostedId"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dsV.Tables[4].Rows[x]["encId"] = encId;
            }
            vaccanciesRepeater.DataSource = dsV.Tables[4];
            vaccanciesRepeater.DataBind();
        }
    }

    #region "News & Stories"

    private void NewsAndStoriesRepeter()
    {
        //DataTable dt = new DataTable();
        //dt = GetNewsNdstories();
        //if (dt.Rows.Count > 0)
        //{
        //    NewsStoriesRepeater.DataSource = dt;
        //    NewsStoriesRepeater.DataBind();
        //}

        string Pk_userId = HttpContext.Current.Session["AlumniId"] != null ? HttpContext.Current.Session["AlumniId"].ToString() : "";
        DataSet dsE = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(Pk_userId.Length > 0 ? Convert.ToInt32(Pk_userId) : 0).GetDataSet();
        dsE.Tables[5].Columns.Add("encId");
        int rnum = (dsE.Tables[5].Rows.Count) + 1;

        if (dsE.Tables[5].Rows.Count > 0)
        {
            for (int x = 0; x < dsE.Tables[5].Rows.Count; x++)
            {
                string pkid = dsE.Tables[5].Rows[x]["Pk_Stories_id"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dsE.Tables[5].Rows[x]["encId"] = encId;
            }
            NewsStoriesRepeater.DataSource = dsE.Tables[5];
            NewsStoriesRepeater.DataBind();
        }
    }

    private DataTable GetNewsNdstories()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_Get_NewsAndstories", values, names, types);
    }

    #endregion

    //protected void lnkGenAlumniCard_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("../Alumni/ALM_AlumniCard.aspx");
    //}

    private void birthRepeter()
    {
        DataTable dt = new DataTable();
        dt = birthDetails();
        {
            rptBirthday.DataSource = dt;
            rptBirthday.DataBind();
        }
    }

    private DataTable birthDetails()
    {
        ClearArrayLists();
        //return Dobj.GetDataTable("ALM_Get_All_Alumni_Current_Months_Birthday", values, names, types);
        return Dobj.GetDataTable("ALM_Get_All_Alumni_Birthdays_CurrentDay_To_Next_3_Months", values, names, types);
    }

    //public void loadAlumnis()
    //{
    //    string alumnid = Session["AlumniID"].ToString();
    //    DataSet ds = ALM_GetAll_Alumnis_Except_Current(Convert.ToInt32(alumnid)).GetDataSet();
    //    DataList1.DataSource = ds.Tables[0];
    //    DataList1.DataBind();
    //}

    public static StoredProcedure ALM_GetAll_Alumnis_Except_Current(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SelectAll_Alumnis_Excepts_Current", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@alumniID", alumniid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure ALM_GetAlumni_Details_By_ID(int alumnid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_GetAlumni_Details_By_ID", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@alumnid", alumnid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure ALM_Get_All_Notable_Alumni()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_All_Notable_Alumni", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    private DataSet FillBoardDtls()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("ALM_Get_Notiboard_Dtls", values, names, types);
    }
	
	public StoredProcedure getReceivedMentorshipMessages(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Mentorship_Request_Received_Msg_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
        return sp;
    }

    private void fillNoticeBoards()
    {
        DataSet dsNB = FillBoardDtls();
        dsNB.Tables[0].Columns.Add("encId");
        int rnum = (dsNB.Tables[0].Rows.Count) + 1;

        if (dsNB.Tables[0].Rows.Count > 0)
        {
            for (int x = 0; x < dsNB.Tables[0].Rows.Count; x++)
            {
                string pkid = dsNB.Tables[0].Rows[x]["Pk_Board_ID"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dsNB.Tables[0].Rows[x]["encId"] = encId;
            }
            //rptrNoticeBoards.DataSource = dsNB.Tables[0];
            //rptrNoticeBoards.DataBind();
        }
    }

    private void fillNoticeBoardss()
    {
        DataSet dsNB = FillBoardDtls();

        if (dsNB.Tables[0].Rows.Count > 0)
        {
            rptNoticeBords.DataSource = dsNB.Tables[0];
            rptNoticeBords.DataBind();
        }
    }

    //protected void Unnamed_ServerClick(object sender, EventArgs e)
    //{
    //    DateTime date = DateTime.Now;
    //    string date3 = date.ToString("yyyy-MM-dd");
    //    string time = date.ToString("HH:mm:ss");
    //    conn.Open();
    //    string str = "insert into Chatbox values('" + Label1.Text + "', '" + Label2.Text + "', '" + TextBox1.Text + "', '" + date3 + "', '" + time + "', '" + Image1.ImageUrl.ToString() + "')";
    //    SqlCommand cmd = new SqlCommand(str, conn);
    //    int i = cmd.ExecuteNonQuery();
    //    conn.Close();
    //    if (i >= 1)
    //    {
    //        TextBox1.Text = "";
    //        LoadChatBox();
    //    }
    //}

    //public void LoadChatBox()
    //{
    //    DateTime date = DateTime.Now;
    //    string date3 = date.ToString("yyyy-MM-dd");
    //    conn.Open();
    //    string str = "select * from Chatbox where Sender = '" + Label1.Text + "' and Reciever = '" + Label2.Text + "' or Sender = '" + Label2.Text + "' and Reciever = '" + Label1.Text + "' and Date = '" + date3 + "' order by id";
    //    SqlCommand cmd = new SqlCommand(str, conn);
    //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //    DataSet dsSS = new DataSet();
    //    sda.Fill(dsSS);
    //    dlist3.DataSource = dsSS;
    //    dlist3.DataBind();
    //    conn.Close();
    //}

    //public void getUser()
    //{
    //    try
    //    {
    //        if (Session["AlumniID"] != null || Session["AlumniID"].ToString() != "")
    //        {
    //            string alumnid = Session["AlumniID"].ToString();
    //            DataSet dsA = ALM_GetAlumni_Details_By_ID(Convert.ToInt32(alumnid)).GetDataSet();

    //            if (dsA.Tables[0].Rows.Count > 0)
    //            {
    //                //Image1.ImageUrl = dsA.Tables[0].Rows[0]["ImageUrl"].ToString();
    //                //Label1.Text = dsA.Tables[0].Rows[0]["alumni_name"].ToString();

    //                CurrentSenderId.Text = dsA.Tables[0].Rows[0]["pk_alumniid"].ToString();
    //                CurrentSender.Text = dsA.Tables[0].Rows[0]["alumni_name"].ToString();
    //                //active-user.Src = dsA.Tables[0].Rows[0]["ImageUrl"].ToString();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Redirect("../Alumin_Loginpage.aspx");
    //    }
    //}

    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    LinkButton lnkBtn = sender as LinkButton;
    //    string id = ((LinkButton)sender).CommandArgument.ToString();
    //    Label2.Text = lnkBtn.Text;
    //    DataListItem item = (DataListItem)lnkBtn.NamingContainer;
    //    Image NameLabel = (Image)item.FindControl("Image2");
    //    string url = NameLabel.ImageUrl.ToString();
    //    Image3.ImageUrl = url;
    //    LoadChatBox();
    //}

    //protected void Timer1_Tick(object sender, EventArgs e)
    //{
    //    LoadChatBox();
    //}

    #region ChatBox

    protected string GetWelcomeBanner(string str)
    {
        if (String.IsNullOrWhiteSpace(Server.HtmlEncode(str)))
            return "H!, Lets start chatting...";
        return str;
    }

    #endregion

    //protected void Timer1_Tick(object sender, EventArgs e)
    //{
    //    DataList1.DataBind();
    //    //DataList3.DataBind();
    //    //DataList4.DataBind();
    //}

    //protected void Logout_Click(object sender, EventArgs e)
    //{
    //    string onlineStatusStr = "UPDATE ALM_AlumniRegistration SET OnlineStatus = 0 WHERE pk_alumniid = @user";
    //    SqlCommand comm = new SqlCommand(onlineStatusStr, conn);
    //    comm.Parameters.AddWithValue("@user", Server.HtmlEncode(CurrentSender.Text));
    //    conn.Open();
    //    comm.ExecuteNonQuery();
    //    conn.Close();
    //    FormsAuthentication.SignOut();
    //    Response.Redirect("Alumin_Loginpage.aspx");
    //}

    //protected void lblUserName_Click(object sender, EventArgs e)
    //{
    //    CurrentReceiver.Text = Server.HtmlEncode(((LinkButton)sender).Text);
    //    CurrentReceiverId.Text = Server.HtmlEncode(((LinkButton)sender).CommandArgument);
    //    LoadChatList();
    //    MsgPanel.Visible = true;
    //}

    //void LoadChatList()
    //{
    //    DataSet dsS = new DataSet();
    //    string strcmd = "SELECT MsgSender, ChatMsg FROM ALM_MsgTable WHERE (MsgSenderId = @Sender AND MsgRecieverId = @Receiver) OR (MsgSenderId = @ViseSender and MsgRecieverId = @ViseReceiver) ORDER BY pk_MsgId";
    //    SqlCommand SqlCmd = new SqlCommand(strcmd, conn);
    //    SqlCmd.Parameters.AddWithValue("@Sender", Server.HtmlEncode(CurrentSenderId.Text));
    //    SqlCmd.Parameters.AddWithValue("@Receiver", Server.HtmlEncode(CurrentReceiverId.Text));
    //    SqlCmd.Parameters.AddWithValue("@ViseSender", Server.HtmlEncode(CurrentReceiverId.Text));
    //    SqlCmd.Parameters.AddWithValue("@ViseReceiver", Server.HtmlEncode(CurrentSenderId.Text));
    //    conn.Open();
    //    SqlDataAdapter sqlda = new SqlDataAdapter(SqlCmd);
    //    sqlda.Fill(dsS);
    //    DataList2.DataSource = dsS.Tables[0];
    //    DataList2.DataBind();
    //    conn.Close();
    //    seenAllMsg();
    //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Javascript", "alert('Hi Abhi')", true);
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Javascript", "ClickToShow();", true);

    //    //  ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "OpenModalDialog", "<script type='text/javascript'>document.getElementById('botchat').click() ;</script>", false);

    //    // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "OpenModalDialog", "<script type='text/javascript'>document.getElementById('botchat').click() ;</script>", false);
    //}

    //void seenAllMsg()
    //{
    //    conn.Open();
    //    string sCmd = "UPDATE ALM_MsgTable SET RecieverSeen = 1 WHERE MsgRecieverId = @MsgRec and MsgSenderId = @MsgSen";
    //    SqlCommand cmd = new SqlCommand(sCmd, conn);
    //    cmd.Parameters.AddWithValue("@MsgRec", Server.HtmlEncode(CurrentSenderId.Text));
    //    cmd.Parameters.AddWithValue("@MsgSen", Server.HtmlEncode(CurrentReceiverId.Text));
    //    cmd.ExecuteNonQuery();
    //    conn.Close();
    //}

    //protected string GetStyleForMsgList(string str)
    //{
    //    if (string.Equals(Server.HtmlEncode(str), Server.HtmlEncode(CurrentSenderId.Text), StringComparison.OrdinalIgnoreCase))
    //    {
    //        return "SenderClass";
    //    }
    //    return "ReceiverClass";
    //}

    //protected string GetPerfactName(string str)
    //{
    //    if (string.Equals(Server.HtmlEncode(str), Server.HtmlEncode(CurrentSenderId.Text), StringComparison.OrdinalIgnoreCase))
    //    {
    //        return "<span style='color: #efdab5'>You: </span>";
    //    }
    //    return "<span style='color: #efdab5'>" + Server.HtmlEncode(str) + ": </span>";
    //}

    //protected void sendBTN_Click(object sender, EventArgs e)
    //{
    //    if (string.IsNullOrWhiteSpace(Server.HtmlEncode(MSGTextBox.Text)))
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Javascript", "alert('Enter Message')", true);
    //        return;
    //    }

    //    if (string.IsNullOrWhiteSpace(Server.HtmlEncode(MSGTextBox.Text)))
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Javascript", "alert('Select Receiver')", true);
    //        return;
    //    }

    //    string strrCmd = "ALM_AddMsgToTable @Msg, @SenderId, @ReceiverId";
    //    SqlCommand sqlcmds = new SqlCommand(strrCmd, conn);
    //    sqlcmds.Parameters.AddWithValue("@Msg", Server.HtmlEncode(MSGTextBox.Text));
    //    sqlcmds.Parameters.AddWithValue("@SenderId", Server.HtmlEncode(CurrentSenderId.Text));
    //    sqlcmds.Parameters.AddWithValue("@ReceiverId", Server.HtmlEncode(CurrentReceiverId.Text));
    //    conn.Open();
    //    sqlcmds.ExecuteNonQuery();
    //    conn.Close();
    //    MSGTextBox.Text = "";
    //    MSGTextBox.Focus();
    //    LoadChatList();
    //    LoadingImage.Attributes.CssStyle.Add("opacity", "0");
    //}

    //int checkNewMessage()
    //{
    //    conn.Open();
    //    string sqlquery = "SELECT COUNT(*) FROM ALM_MsgTable WHERE RecieverSeen = 0 AND MsgRecieverId = @Receiver";
    //    SqlCommand cmds = new SqlCommand(sqlquery, conn);
    //    cmds.Parameters.AddWithValue("@Receiver", Server.HtmlEncode(CurrentSenderId.Text));
    //    int x = 0;
    //    x = Convert.ToInt32(cmds.ExecuteScalar());
    //    conn.Close();
    //    return x;
    //}

    private void RepeterData()
    {
        DataSet ds = FillBoardDtls();
        ds.Tables[0].Columns.Add("encId");

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            {
                string pkid = ds.Tables[0].Rows[x]["Pk_Board_ID"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                ds.Tables[0].Rows[x]["encId"] = encId;
            }

            RepterDetails.DataSource = ds.Tables[0];
            RepterDetails.DataBind();
        }
    }

    private void GetNotableAlumni()
    {
        crypto cpt = new crypto();
        DataSet dsNA = ALM_Get_All_Notable_Alumni().GetDataSet();
        dsNA.Tables[0].Columns.Add("encId");
        int rnum = (dsNA.Tables[0].Rows.Count) + 1;

        if (dsNA.Tables[0].Rows.Count > 0)
        {
            for (int x = 0; x < dsNA.Tables[0].Rows.Count; x++)
            {
                string pkid = dsNA.Tables[0].Rows[x]["PK_NAID"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dsNA.Tables[0].Rows[x]["encId"] = encId;
            }
            rptNotableAlumni.DataSource = dsNA.Tables[0];
            rptNotableAlumni.DataBind();
        }
    }

    private void VideosRepeter()
    {
        //DataTable dt = new DataTable();
        //dt = GetGellaryVideos();
        //if (dt.Rows.Count > 0)
        //{
        //    rptGalleryVideos.DataSource = dt;
        //    rptGalleryVideos.DataBind();
        //}

        DataTable dtVideos = new DataTable();
        dtVideos = GetGellaryVideos();

        List<VideoModel1> videoList = new List<VideoModel1>();

        foreach (DataRow row in dtVideos.Rows)
        {
            string originalUrl = row["videoURL"].ToString();
            string videoId = ExtractYouTubeVideoId(originalUrl);

            videoList.Add(new VideoModel1
            {
                VideoId = videoId,
                Title = row["title"].ToString()
            });
        }

        rptGalleryVideos.DataSource = videoList;
        rptGalleryVideos.DataBind();
    }

    private DataTable GetGellaryVideos()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GallaryVideos_GetAll", values, names, types);
    }

    public string ExtractYouTubeVideoId(string url)
    {
        try
        {
            if (url.Contains("youtube.com/watch?v="))
            {
                // Format: https://www.youtube.com/watch?v=abc123
                var uri = new Uri(url);
                var query = HttpUtility.ParseQueryString(uri.Query);
                return query["v"];
            }
            else if (url.Contains("youtube.com/embed/"))
            {
                // Format: https://www.youtube.com/embed/abc123
                var segments = new Uri(url).Segments;
                return segments.Last().TrimEnd('/');
            }
            else if (url.Contains("youtu.be/"))
            {
                // Format: https://youtu.be/abc123
                var segments = new Uri(url).Segments;
                return segments.Last().TrimEnd('/');
            }
        }
        catch { }

        return string.Empty; // fallback
    }
	
	protected void gvMenteeMessages_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
	
    protected void gvMentorRequests_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
	
	protected void lnkBtnMenteeMsg_Click(object sender, EventArgs e)
    {
        MenteeMessages();
    }

    protected void lnkBtnMentorMsg_Click(object sender, EventArgs e)
    {
        MentorMessages();
    }

    protected void btnMentor_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Alumni/ALM_Alumni_Home.aspx");
    }

    protected void btnMentee_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Alumni/ALM_Alumni_Home.aspx");
    }

    protected void rptMentorRequests_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string requestId = e.CommandArgument.ToString();

        if (e.CommandName.ToUpper().ToString().Trim() == "ACCEPT")
        {
            UpdateRequestStatus(requestId, "ACCEPTED");
        }
        else if (e.CommandName.ToUpper().ToString().Trim() == "REJECT")
        {
            UpdateRequestStatus(requestId, "REJECTED");
        }
		else if (e.CommandName.ToUpper().ToString().Trim() == "SENDMSGNOW")
        {
            string[] args = e.CommandArgument.ToString().Split('|');

            if (args.Length == 2)
            {
                string userId = args[0];
                string mReqId = args[1];

                hdnSelectedAlumniID.Value = userId;
                hdnpk_MReqID.Value = mReqId;

                msgPnl.Visible = true;

                hdnSelectedAlumniID.UpdateAfterCallBack = true;
                hdnpk_MReqID.UpdateAfterCallBack = true;
                msgPnl.UpdateAfterCallBack = true;
            }
        }
        Anthem.Manager.IncludePageScripts = true;
    }
	
    private void UpdateRequestStatus(string requestId, string status)
    {
        if (status.ToUpper().ToString().Trim() == "ACCEPTED")
        {
            int AlumniI = mentorshipRequestStatusUpdation(Convert.ToInt32(requestId), status).Execute();

            if (AlumniI > 0)
            {
                Client_Messaging("Request Accepted Successfully.!!!");
                MentorMessages();

                String scriptMsgPopUp = String.Format("document.getElementById('exampleModalScrollableMentor').style.display = 'block';" +
                   "document.getElementById('fade1').style.display = 'block';");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);
            }
        }
        else if (status.ToUpper().ToString().Trim() == "REJECTED")
        {
            int AlumniI = mentorshipRequestStatusUpdation(Convert.ToInt32(requestId), status).Execute();

            if (AlumniI > 0)
            {
                Client_Messaging("Request Rejected Successfully.!!!");
                MentorMessages();

                String scriptMsgPopUp = String.Format("document.getElementById('exampleModalScrollableMentor').style.display = 'block';" +
                   "document.getElementById('fade1').style.display = 'block';");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);

            }
        }
    }

    public static StoredProcedure mentorshipRequestStatusUpdation(int mRID, string actionRemarks)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Mentorship_Requests_Status_Acc_Or_Rej", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_MReqID", mRID, DbType.Int32);
        sp.Command.AddParameter("@actionTaken", actionRemarks, DbType.String);
        return sp;
    }

    protected void lnkAccept_Click(object sender, EventArgs e)
    {
        LinkButton lnkBtn = (LinkButton)sender;
        string mRequestId = lnkBtn.CommandArgument.ToString().Trim();
        string action = lnkBtn.CommandName.ToString().Trim();

        string actionStatus = action == "Accept" ? "ACCEPTED" : "REJECTED";

        bool success = AcceptMentorRequest(mRequestId, actionStatus);

        if (success)
        {
            Client_Messaging("Request Accepted Successfully.!!!");
            rptMentorRequests.DataSource = GetMentorRequests();
            rptMentorRequests.DataBind();
        }
    }

    private bool AcceptMentorRequest(string requestId, string action)
    {
        try
        {
            int result = mentorshipRequestStatusUpdation(Convert.ToInt32(requestId), action).Execute();

            return result > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
	
    protected void lnkReject_Click(object sender, EventArgs e)
    {
        LinkButton lnkBtn = (LinkButton)sender;
        string mRequestId = lnkBtn.CommandArgument.ToString().Trim();
        string action = lnkBtn.CommandName.ToString().Trim();

        string actionStatus = action == "Accept" ? "ACCEPTED" : "REJECTED";

        bool success = AcceptMentorRequest(mRequestId, actionStatus);

        if (success)
        {
            Client_Messaging("Request Rejected Successfully.!!!");
            rptMentorRequests.DataSource = GetMentorRequests();
            rptMentorRequests.DataBind();
        }
    }	
	
    protected bool validationMsg()
    {
        if (string.IsNullOrEmpty(txtMessagess.Text))
        {
            Client_Messaging("Message is Required.!!!");
            txtMessagess.Focus();
            return false;
        }
        return true;
    }

    protected void btnSendMsgss_Click(object sender, EventArgs e)
    {
        try
        {
            if (!validationMsg())
            {
                String scriptMsgPopUp = String.Format("document.getElementById('modalPopUpMentee').style.display = 'block';" +
                      "document.getElementById('fade1').style.display = 'block';");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);

                Client_Messaging("Message is Required.!!!");
                txtMessagess.Focus();
                return;
            }

            string Message = "";
            DataSet ds_details = new DataSet();
            ds_details = GetMainMsgss();
            xmlDoc = ds_details.GetXml();
            mode = "INSERT";

            if (ALM_Mentors_Mentees_Messages_InsertRecord(ref Message) >= 0)
            {
                Client_Messaging("Message Send Successfully.!!!");
                clearMsgControls();
            }
        }
        catch (SqlException ex)
        {
            lblMsgss.Text = objDAO.ShowSQLErrorMsg(ex.Message.ToString().Trim(), "", ex);
        }
    }

    protected DataSet GetMainMsgss()
    {
        DataSet dsMain = objDAO.GetSchema("ALM_Mentors_Mentees_Messages_Mst");
        DataRow dr = dsMain.Tables[0].NewRow();       

        int senderID = 0;

        if (Session["AlumniID"] != null && int.TryParse(Session["AlumniID"].ToString(), out senderID))
        {
            senderMsgAlumniID = senderID;
        }

        int receiverID = 0;

        if (!string.IsNullOrEmpty(hdnSelectedAlumniID.Value) && int.TryParse(hdnSelectedAlumniID.Value, out receiverID))
        {
            receiverMsgAlumniID = receiverID;
        }

        int mrID = 0;

        if (!string.IsNullOrEmpty(hdnpk_MReqID.Value) && int.TryParse(hdnpk_MReqID.Value, out mrID))
        {
            mReqID = mrID;
        }

        //DataSet dsALM = ALM_Get_AlumnDetails_By_MReqID(mReqID).GetDataSet();

        //if (dsALM.Tables[0].Rows.Count > 0)
        //{
        //    mReqID = Convert.ToInt32(dsALM.Tables[0].Rows[0]["pk_MReqID"].ToString());
        //}

        dr["pk_MsgID"] = 0;
        dr["messageText"] = txtMessagess.Text.Trim().ToString();
        dr["senderID"] = senderMsgAlumniID;
        dr["receiverID"] = receiverMsgAlumniID;
        dr["fk_MReqID"] = mReqID;

        dsMain.Tables[0].Rows.Add(dr);
        return dsMain;
    }

    public int ALM_Mentors_Mentees_Messages_InsertRecord(ref string Message)
    {
        try
        {
            ClearArrayLists();
            names.Add("@xmlDoc"); types.Add(SqlDbType.VarChar); values.Add(xmlDoc);
            names.Add("@mode"); types.Add(SqlDbType.VarChar); values.Add(mode);
            names.Add("@pk_MsgID"); types.Add(SqlDbType.Int); values.Add(0);
            if (objDAO.ExecuteTransactionMsg("ALM_Mentors_Mentees_Messages_Insert", values, names, types, ref Message) > 0)
            {
                Message = objDAO.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void clearMsgControls()
    {
        cfObj.ClearTextSetDropDown(this);
        txtMessagess.Text = "";
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void lnkMentorshipRequest_Click(object sender, EventArgs e)
    {
        mentorshipMsgDetails();
    }

    protected void mentorshipMsgDetails()
    {
        try
        {
            if (Session["AlumniID"] != null)
            {
                if (Session["AlumniID"].ToString() != "")
                {
                    List<MentorRequest> mentorRequests = getMentorshipRequests();
                    rptMR.DataSource = mentorRequests;
                    rptMR.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Client_Messaging(ex.Message.ToString());
        }
    }

    public List<MentorRequest> getMentorshipRequests()
    {
        List<MentorRequest> listRequests = new List<MentorRequest>();

        string userSessionAlumniID = Session["AlumniID"] != null ? Session["AlumniID"].ToString() : "0";

        int alumniId = Convert.ToInt32(userSessionAlumniID);
        DataSet dsS = getReceivedMentorshipMessages(alumniId).GetDataSet();

        if (dsS.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drMR in dsS.Tables[0].Rows)
            {
                MentorRequest objMR = new MentorRequest();
                objMR.pk_MReqID = Convert.ToInt32(drMR["pk_MReqID"]);
                objMR.senderID = Convert.ToInt32(drMR["senderID"].ToString());
                objMR.senderName = drMR["senderName"].ToString();
                objMR.goalDescription = drMR["goalDescription"].ToString();
                objMR.messageText = drMR["messageText"].ToString();
                objMR.sentAt = drMR["sentAt"].ToString();
                objMR.status = drMR["status"].ToString();
                objMR.isVisible = Convert.ToBoolean(drMR["isVisible"].ToString());
                objMR.isSendMsgNow = Convert.ToBoolean(drMR["isSendMsgNow"].ToString());
                objMR.isAccepted = Convert.ToBoolean(drMR["isAccepted"].ToString());
                listRequests.Add(objMR);
            }
        }
        return listRequests;
    }

    protected void rptMR_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string requestId = e.CommandArgument.ToString();

        if (e.CommandName.ToUpper().ToString().Trim() == "ACCEPT")
        {
            UpdateRequestStatus(requestId, "ACCEPTED");
        }
        else if (e.CommandName.ToUpper().ToString().Trim() == "REJECT")
        {
            UpdateRequestStatus(requestId, "REJECTED");
        }
        Anthem.Manager.IncludePageScripts = true;
    }
	
	protected void lnkMenteeRequest_Click(object sender, EventArgs e)
    {
        menteeMRMsgDetails();
    }

    protected void menteeMRMsgDetails()
    {
        try
        {
            if (Session["AlumniID"] != null)
            {
                if (Session["AlumniID"].ToString() != "")
                {
                    List<MenteeRequestModel> menteeRequests = getMenteeRequests();
                    rptMRM.DataSource = menteeRequests;
                    rptMRM.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Client_Messaging(ex.Message.ToString());
        }
    }
	
	public List<MenteeRequestModel> getMenteeRequests()
    {
        List<MenteeRequestModel> listMRMRequests = new List<MenteeRequestModel>();

        string userSessionAlumniID = Session["AlumniID"] != null ? Session["AlumniID"].ToString() : "0";

        int alumniId = Convert.ToInt32(userSessionAlumniID);
        DataSet dsMRM = getReceivedMenteeMessagesStatus(alumniId).GetDataSet();

        if (dsMRM.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drMRM in dsMRM.Tables[0].Rows)
            {
                MenteeRequestModel objMRM = new MenteeRequestModel();
                objMRM.pk_MRID = Convert.ToInt32(drMRM["pk_MRID"]);
                objMRM.seekingHelpFor = drMRM["seekingHelpFor"].ToString();
                objMRM.senderID = Convert.ToInt32(drMRM["senderID"].ToString());
                objMRM.senderName = drMRM["senderName"].ToString();
                objMRM.messageText = drMRM["messageText"].ToString();
                objMRM.sentAt = drMRM["sentAt"].ToString();
                objMRM.requestStatus = drMRM["requestStatus"].ToString();
                objMRM.isVisible = Convert.ToBoolean(drMRM["isVisible"].ToString());
                objMRM.isSendMsgNow = Convert.ToBoolean(drMRM["isSendMsgNow"].ToString());
                objMRM.isAccepted = Convert.ToBoolean(drMRM["isAccepted"].ToString());
                listMRMRequests.Add(objMRM);
            }
        }
        return listMRMRequests;
    }

    public StoredProcedure getReceivedMenteeMessagesStatus(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Mentee_Requests_Received_Msg_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
        return sp;
    }
	
	protected void rptMRM_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string requestId = e.CommandArgument.ToString();

        if (e.CommandName.ToUpper().ToString().Trim() == "ACCEPT")
        {
            UpdateMRMStatus(requestId, "ACCEPTED");
        }
        else if (e.CommandName.ToUpper().ToString().Trim() == "REJECT")
        {
            UpdateMRMStatus(requestId, "REJECTED");
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    private void UpdateMRMStatus(string requestId, string status)
    {
        if (status.ToUpper().ToString().Trim() == "ACCEPTED")
        {
            int AlumniI = mrmStatusUpdation(Convert.ToInt32(requestId), status).Execute();

            if (AlumniI > 0)
            {
                Client_Messaging("Request Accepted Successfully.!!!");
                menteeMessages();

                String scriptMsgPopUp = String.Format("document.getElementById('exampleModalScrollableMentor').style.display = 'block';" +
                   "document.getElementById('fade1').style.display = 'block';");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);
            }
        }
        else if (status.ToUpper().ToString().Trim() == "REJECTED")
        {
            int AlumniI = mrmStatusUpdation(Convert.ToInt32(requestId), status).Execute();

            if (AlumniI > 0)
            {
                Client_Messaging("Request Rejected Successfully.!!!");
                menteeMessages();

                String scriptMsgPopUp = String.Format("document.getElementById('exampleModalScrollableMentor').style.display = 'block';" +
                   "document.getElementById('fade1').style.display = 'block';");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);

            }
        }
    }
	
    public static StoredProcedure mrmStatusUpdation(int mRMID, string actionMRMRemarks)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_MenteeRequests_For_Mentors_Acc_Or_Rej", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_MRID", mRMID, DbType.Int32);
        sp.Command.AddParameter("@actionTaken", actionMRMRemarks, DbType.String);
        return sp;
    }
	
	protected void menteeMessages()
    {
        try
        {
            if (Session["AlumniID"] != null)
            {
                if (Session["AlumniID"].ToString() != "")
                {
                    List<MenteeRequestModel> mentorRequests = getMenteeRequests();
                    rptMRM.DataSource = mentorRequests;
                    rptMRM.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Client_Messaging(ex.Message.ToString());
        }
    }
	
	protected void btnCloseModal_Click(object sender, EventArgs e)
    {
        String scriptMsgPopUp = String.Format("document.getElementById('modalPopUpMenteeRequestMsgss').style.display = 'none';" +
                      "document.getElementById('fade1').style.display = 'none';");
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "HideModal", scriptMsgPopUp, true);
		LoadNotifications();
        Response.Redirect("../Alumni/ALM_Alumni_Home.aspx");
    }
	
	protected void Timer1_Tick(object sender, EventArgs e)
    {
        LoadNotifications();
    }
}

public class GetCureentVacancy
{
    public int Pk_JobPostedId { get; set; }
    public string CompanyName { get; set; }
    public string Designation { get; set; }
    public string OpenFrom { get; set; }
    public string OpenTo { get; set; }
    public string SkillsReq { get; set; }
    public string SelectionProcess { get; set; }
    public string JobVacncyUrl { get; set; }
}

public class Getimg
{
    public int pk_id { get; set; }
    public string image { get; set; }
}

public class GetNoticeBoards
{
    public int pk_boardid { get; set; }
    public string heading { get; set; }
    public string description { get; set; }
    public string daydate { get; set; }
    public string monthname { get; set; }
    public string filepath { get; set; }
    public string filename { get; set; }
    public string ImgsSrc { get; set; }
}

public class Birthday
{
    public DateTime Date { get; set; }
    public string Name { get; set; }
}

public class VideoModel1
{
    public string VideoId { get; set; }
    public string Title { get; set; }
}

public class Message
{
    public string FormattedMessage { get; set; }
}

public class MentorRequest
{
    //public Int32 pk_MReqID { get; set; }
    //public string userID { get; set; }
    //public string userName { get; set; }
    //public string messageText { get; set; }
    //public string sentAt { get; set; }
    //public string status { get; set; }
    //public bool isVisible { get; set; }
    //public bool isSendMsgNow { get; set; }

    public Int32 pk_MsgID { get; set; }
    public string messageText { get; set; }
    public DateTime sentDate { get; set; }
    public Int32 pk_MReqID { get; set; }
    public int senderID { get; set; }
    public string senderName { get; set; }
    public int receiverID { get; set; }
    public string receiverName { get; set; }
    public string goalDescription { get; set; }
    public string status { get; set; }
    public string email { get; set; }
    public DateTime dob { get; set; }
    public string sentAt { get; set; }
    public bool isVisible { get; set; }
    public bool isSendMsgNow { get; set; }
    public bool isAccepted { get; set; } 
}

public class MenteeRequestModel
{
    public Int32 pk_MRID { get; set; }
    public string messageText { get; set; }
    public DateTime sentDate { get; set; }
    public Int32 pk_MReqID { get; set; }
    public int senderID { get; set; }
    public string senderName { get; set; }
    public int receiverID { get; set; }
    public string receiverName { get; set; }
    public string seekingHelpFor { get; set; }
    public string requestStatus { get; set; }
    public string email { get; set; }
    public DateTime dob { get; set; }
    public string sentAt { get; set; }
    public bool isVisible { get; set; }
    public bool isSendMsgNow { get; set; }
    public bool isAccepted { get; set; }
}