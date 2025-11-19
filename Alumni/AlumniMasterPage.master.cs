using System;
using System.Data;
using System.Web;
using SubSonic;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;

public partial class Alumni_AlumniMasterPage : System.Web.UI.MasterPage
{
	#region DB_Work
	
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["IUMSNXG"].ConnectionString);

    private Boolean IsPageRefresh = false;

    public StoredProcedure CompanyShow(int cmpId)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("PLC_SP_RejectedCompanyRegistration_List1", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@cmpId", cmpId, DbType.Int32);
        return sp;
    }
	
    public StoredProcedure Alumsessionvalueget(int pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_sessionvalueget", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    public StoredProcedure HPU_ALM_Paymentcheck(int pk_Regid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Paymentcheck", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
        return sp;
    }

    public StoredProcedure HPU_ALM_Statuscheck(int pk_Regid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Approvestatuscheck", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
        return sp;
    }

    public StoredProcedure HPU_ALM_Mentorcheck(int pk_Regid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_ApproveMentorcheck", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure ALM_SP_AlumniRegistration_Edit(int? pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_EditNew", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure ALM_AlumniUser_OnlineStatus_Upd(int? pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_AlumniUser_OnlineStatus_Upd", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    #endregion

    public string UserName = "";
    public string UserImage = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["AlumniName"] != null)
                {
                    UserName = Session["AlumniName"].ToString();
                    GetUserImage(UserName);
                    //Set_AlumniImg();
                }
                if (Session["AlumniID"] != null || Session["AlumniID"].ToString() != "")
                {
                    getprofile();
                    CheckApprovestatus();
                    CheckMentor();
                    Set_AlumniImg();
                    btnLogout.Enabled = true;
                    lnkprofile.Text = "My Profile";
					
					#region "checked logged user is mentor/mentee/none"

                    int loggedUserAlumniID = Convert.ToInt32(Session["AlumniID"].ToString());

                    bool isloggedUserMentor = checkIfUserIsMentor(loggedUserAlumniID);
                    bool isloggedUserMentee = checkIfUserIsMentee(loggedUserAlumniID);

                    if (isloggedUserMentor)
                    {
                        Session["isLoggedUser"] = "Mentor";
                    }
                    else if (isloggedUserMentee)
                    {
                        Session["isLoggedUser"] = "Mentee";
                    }
                    else
                    {
                        Session["isLoggedUser"] = "";
                    }

                    #endregion
                }
                else
                {
                    btnLogout.Enabled = false;
                    Response.Redirect("../Alumin_Loginpage.aspx");
                }
            }
            catch (Exception ex)
            {
                btnLogout.Enabled = false;
                Response.Redirect("../Alumin_Loginpage.aspx");
            }            
        }
		getSocialURLLinks();
    }

    public void GetUserImage(string Username)
    {
        ////if (Username != null)
        ////{
        ////    string query = "select Photo from tbl_Users where UserName='" + Username + "'";
        ////    string ImageName = ConnC.GetColumnVal(query, "Photo");
        ////    if (ImageName != "")
        ////        UserImage = "images/DP/" + ImageName;
        ////}
		
        UserImage = "https://ftpbackoffice.hpushimla.in/Alumni/StuImage/HPU_Alumni_PP__bc0e7d1f-1e80-4bd9-8f83-12cb1be46360_854602_20240529114405050PP_426818499_4384725.jpg";
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
                    lnkpay.Visible = false;
                }
                else
                {
                    lnkpay.Visible = true;
                }
            }
        }
    }

    protected void CheckApprovestatus()
    {
        if (Session["AlumniID"] != null)
        {
            if (Session["AlumniID"].ToString() != "")
            {
                int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
                DataSet ds = HPU_ALM_Statuscheck(alumniId).GetDataSet();
                var count = ds.Tables[0].Rows.Count;

                if (count > 0)
                {
                    Session["alumniname"] = ds.Tables[0].Rows[0]["alumni_name"].ToString().Trim();
                    // lnkpay.Visible = true;
                    LinkNews.Visible = true;
                    //LinkEvents.Visible = true;
                    LinkSuggestion.Visible = true;
                    //LinkSearch.Visible = true;
                    LinkPublishJobs.Visible = true;
                    lnkInternships.Visible = true;
                    LinkChangePass.Visible = true;
                    //LinkMentee.Visible = true;
                }
                else
                {
                    LinkNews.Visible = false;
                    //LinkEvents.Visible = false;
                    LinkSuggestion.Visible = false;
                    //LinkSearch.Visible = false;
                    LinkPublishJobs.Visible = false;
                    lnkInternships.Visible = false;
                    LinkChangePass.Visible = false;
                    //LinkMentee.Visible = false;
                    //Lnkmeneeassigned.Visible = false;
                }
            }
        }
    }
	
    protected void CheckMentor()
    {
        if (Session["AlumniID"] != null)
        {
            if (Session["AlumniID"].ToString() != "")
            {
                int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
                DataSet ds = HPU_ALM_Mentorcheck(alumniId).GetDataSet();
                var count = ds.Tables[0].Rows.Count;
                if (count > 0)
				{
                    LinkNews.Visible = true;
                    LinkSuggestion.Visible = true;                
                    LinkChangePass.Visible = true;
                    
                   if (Convert.ToBoolean(ds.Tables[0].Rows[0]["isMentor"]))
                    {
                        LinkPublishJobs.Visible = true;
                        lnkInternships.Visible = true;                      
                    }
                    else
                    {
                        LinkPublishJobs.Visible = false;
                        lnkInternships.Visible = false;
                    }
                }
                else
                {
                    LinkPublishJobs.Visible = false;
                    lnkInternships.Visible = false;
                }
            }
        }
        //else
        //{
        //  lnkpay.Visible = true;
        //}
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        if (Session["AlumniID"] != null)
        {
            int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
            DataSet dsUser = ALM_AlumniUser_OnlineStatus_Upd(alumniId).GetDataSet();            
        }
        Session.Abandon();
        Response.Redirect("../Alumin_Loginpage.aspx");
    }

    /// <summary>
    /// Modified Aditya Sharma
    /// date: 16-02-2023
    /// </summary>
    public void Set_AlumniImg()
    {
        if (Session["AlumniImg"] != null)
        {
            int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
            DataSet ds = ALM_SP_AlumniRegistration_Edit(alumniId).GetDataSet();
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            //if (dt.ToString() == string.Empty)
            //{
            if (dt.Rows.Count > 0)
            {
                string fileName = ""; string contenttype = ""; byte[] fileBytes = null;
                fileName = dt.Rows[0]["Files_Unique_Name"].ToString().Trim();
                if (ds.Tables[0].Rows[0]["Files_Unique_Name"].ToString() != "None")
                {
                    img_mast_user.Src = ds.Tables[0].Rows[0]["Files_Unique_Name"].ToString();
                    UserImage = ds.Tables[0].Rows[0]["Files_Unique_Name"].ToString();
                }
                else
                {
                    img_mast_user.Src = "~/Alumni/StuImage/default-user.jpg";
                    UserImage = "~/Alumni/StuImage/default-user.jpg";
                }
            }

            //  }
            //if (fileName != "")
            //{
            //    contenttype = dt.Rows[0]["fileextension"].ToString().Trim();
            //    string host = HttpContext.Current.Request.Url.Host;
            //    string upldPath = "";
            //    string showimgPath = "";
            //    DataSet dsFilepath = new DataSet();
            //    dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
            //    foreach (DataRow dr in dsFilepath.Tables[0].Rows)
            //    {
            //        if (host == dr["Server_Ip"].ToString().Trim())
            //        {
            //            upldPath = dr["Physical_Path"].ToString().Trim();
            //            upldPath = upldPath + "/Alumni/StuImage/" + fileName.ToString().Trim();
            //            //img_mast_user.ImageUrl = "~/alumni/stuimage/No_image.png";
            //            showimgPath = upldPath.ToString().Trim();
            //            //filepath = upldPath;
            //        }
            //    }
            //    //img_mast_user.ImageUrl = "~/alumni/stuimage/No_image.png";
            //    img_mast_user.ImageUrl = upldPath; //"~/alumni/stuimage/" + fileName;
            //    //upldPath = upldPath + "\\" + "Alumni\\StuImage" + "\\" + fileName.ToString().Trim();
            //    ////img_mast_user.ImageUrl = upldPath + dt.Rows[0]["Files_Unique_Name"].ToString();
            //    //img_mast_user.ImageUrl = upldPath.ToString().Trim();
            //}

            else
            {
                img_mast_user.Src = null;
                img_mast_user.Src = "~/Alumni/StuImage/default-user.jpg";
            }
        }
        else
        {
            img_mast_user.Src = null;
            img_mast_user.Src = "~/Alumni/StuImage/default-user.jpg";
        }
    }

    //protected void getsessionvalue()
    //{
    //    if(Session["AlumniID"]!=null)
    //  {
    //        int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
    //        DataSet ds = Alumsessionvalueget(alumniId).GetDataSet();
    //        Session["regid"] = ds.Tables[0].Rows[0]["regid"].ToString();
    //    }
    //}

    //protected void Pay()
    //{
    //    int loginId = Convert.ToInt32(Session["LoginId"].ToString());
    //    DataSet ds = CompanyShow(loginId).GetDataSet();
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        DataRow dr = ds.Tables[0].Rows[0];
    //        {
    //            if (Convert.ToBoolean(dr["Cnt"]) == true) /* && (Convert.ToBoolean(dr["Cnt"] = 1));*/
    //            {
    //                lnkpay.Visible = true;
    //            }
    //            else
    //            {
    //                lnkpay.Visible = false;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        lnkpay.Visible = false;
    //    }
    //}
	
    public void getprofile()
    {
        if (Session["AlumniID"] != null || Session["AlumniID"].ToString() != "")
        {
            btnLogout.Enabled = true;
            int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
            DataSet ds = ALM_SP_AlumniRegistration_Edit(alumniId).GetDataSet();
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            string fileName = ""; string contenttype = ""; byte[] fileBytes = null;

            if (dt.ToString() == string.Empty)
            {
                fileName = dt.Rows[0]["Files_Unique_Name"].ToString().Trim();

                if (fileName != "")
                {
                    contenttype = dt.Rows[0]["fileextension"].ToString().Trim();
                    string host = HttpContext.Current.Request.Url.Host;
                    string upldPath = "";
                    string showimgPath = "";
                    DataSet dsFilepath = new DataSet();
                    dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
                    foreach (DataRow dr in dsFilepath.Tables[0].Rows)
                    {
                        if (host == dr["Server_Ip"].ToString().Trim())
                        {
                            upldPath = dr["Physical_Path"].ToString().Trim();
                            showimgPath = upldPath.ToString().Trim();
                        }
                    }
                    img_mast_user.Src = upldPath; //"~/alumni/stuimage/" + fileName;
                }
                else
                {
                    img_mast_user.Src = "~/Alumni/StuImage/default-user.jpg";
                }
            }
        }
        else
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
    }

    protected void lnkpay_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_AlumniPayLater.aspx");
    }

    protected void LinkNews_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_Alumni_NewsStories_Lists.aspx");
    }

    protected void LinkEvents_Click(object sender, EventArgs e)
    {
        Response.Redirect("Alumni_EventsGallery_View.aspx");
    }

    protected void LinkSuggestion_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_AlumniSuggestion.aspx");
    }

    protected void LinkSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_AlumniSearch.aspx");
    }

    protected void LinkPublishJobs_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_Alumni_PublishJobs.aspx");
    }

    protected void LinkChangePass_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_AlumniChangePassword.aspx");
    }

    protected void LinkMentee_Click(object sender, EventArgs e)
    {
        Response.Redirect("Alm_BecomeMentee.aspx");
    }

    protected void Lnkmeneeassigned_Click(object sender, EventArgs e)
    {
        Response.Redirect("Alm_Mentees_Assigned.aspx");
    }

    protected void lnkprofile_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_AlumniProfileShow.aspx");
    }
	
    //protected void lnkbtnContribution_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("ALM_FundDonationlist_Contributions.aspx");
    //}

    protected void lnkContribtion_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_Fund_DonationLists.aspx");
    }

    protected void lnkInternships_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_Alumni_Publish_Internships.aspx");
    }

    protected void lnkGenAlumniCard_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Alumni/ALM_AlumniCard.aspx");
    }
	
	protected void lnkPrintRegistrationDetails_Click(object sender, EventArgs e)
    {
        try
        {
            //lblMsg.Text = "";
            ViewReport();
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    protected void ViewReport()
    {
        try
        {
            //lblMsg.Text = "";
            ReportDocument objRptDoc = new ReportDocument();
            string filename = "";

            try
            {
                int alumniId = 0;

                if (Session["AlumniID"].ToString() == null || Session["AlumniID"].ToString() == "")
                    alumniId = Convert.ToInt32(Session["EmpView_AlumniID"].ToString());
                else
                    alumniId = Convert.ToInt32(Session["AlumniID"].ToString());

                DataSet dsAR = ALM_SP_AlumniRegistration_Details_Report_Print_By_Alumni(alumniId).GetDataSet();

                if (dsAR.Tables[0].Rows.Count > 0)
                {
                    dsAR.Tables[0].TableName = "Alumni_Registration_Details";
                    dsAR.Tables[1].TableName = "Company_Details";
                    //dsAR.WriteXml(Server.MapPath("~/Alumni/ALM_XML/Alumni_Registration_Details_Report.xml"));
                    filename = Server.MapPath("~/Alumni/ALM_Reports/Alumni_Registration_Details_Report.rpt");
                    objRptDoc.Load(filename);
                    objRptDoc.SetDataSource(dsAR);
                    objRptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Alumni Registration Details Report");
                }
                else
                {
                    //lblMsg.Text = "No Details Found!";
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
            }
            catch (Exception ex)
            {
                //lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
            }
            finally
            {
                objRptDoc.Close();
                objRptDoc.Dispose();
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }
	
    public StoredProcedure ALM_SP_AlumniRegistration_Details_Report_Print_By_Alumni(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_Details_Report", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@alumniid", alumniid, DbType.Int32);
        return sp;
    }
	
	public StoredProcedure ALM_MenteeStatuscheck(int pk_Regid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Menteecheck", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
        return sp;
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

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    #endregion
}