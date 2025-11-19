using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using SubSonic;

public partial class Alumni_ALM_Mentorship : System.Web.UI.Page
{
    ArrayList names = new ArrayList();
    ArrayList types = new ArrayList();
    ArrayList values = new ArrayList();
    DataAccess DAobj = new DataAccess();

    public int menteeID { get; set; }

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    Page.Theme = "CCSBLUE";
    //}

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["AlumniID"] != null && !string.IsNullOrWhiteSpace(Session["AlumniID"].ToString()))
            {
                if (!IsPostBack)
                {
                    if (Session["ProfileId"].ToString() == "2" && Session["AlumniID"] != null)
                    {
                        setButtonStates();
                    }
                    else if (Session["ProfileId"].ToString() == "3" && Session["AlumniID"] != null)
                    {
                        setButtonStates();
                    }
                    else
                    {
                        setButtonStates();
                    }
                }
            }
            else
            {
                Response.Redirect("../Alumin_Loginpage.aspx");
            }
            if (Session["AlumniID"].ToString() != "" && Session["AlumniID"] != null)
            {
                if (!IsPostBack)
                {
                    int loggedUserAlumniID = Convert.ToInt32(Session["AlumniID"].ToString());

                    bool isloggedUserMentor = checkIfUserIsMentor(loggedUserAlumniID);
                    bool isloggedUserMentee = checkIfUserIsMentee(loggedUserAlumniID);

                    DataSet dsS = getloggedUserMenteeID(loggedUserAlumniID).GetDataSet();

                    if (dsS.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsS.Tables[0].Rows[0]["MenteeID"].ToString()))
                        {
                            menteeID = Convert.ToInt32(dsS.Tables[0].Rows[0]["MenteeID"].ToString());
                        }
                    }

                    if (isloggedUserMentor)
                    {
                        FillMenteeProfileRepeter(loggedUserAlumniID);
                        FillMentorProfileRepeterRolewise();
                        //lnkViewAllMentors.Attributes["href"] = "#";
                        //lnkViewAllMentors.Attributes["title"] = "Access denied – You are a mentor";
                        //lnkViewAllMentors.Attributes["style"] = "pointer-events: none; color: white; text-decoration: none; cursor: not-allowed;";
                        notificationDiv.Visible = true;
                    }
                    else if (isloggedUserMentee)
                    {
                        FillMentorProfileRepeter(loggedUserAlumniID);
                        FillMenteeProfileRepeterRolewise();
                        //lnkViewAllMentees.Attributes["href"] = "#";
                        //lnkViewAllMentees.Attributes["title"] = "Access denied – You are a mentee";
                        //lnkViewAllMentees.Attributes["style"] = "pointer-events: none; color: white; text-decoration: none; cursor: not-allowed;";
                        notificationDiv.Visible = false;
                    }
                    else
                    {
                        FillMentorProfileRepeterRolewise(); FillMenteeProfileRepeterRolewise();
                        notificationDiv.Visible = false;
                    }
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
    }

    private void FillMentorProfileRepeter(int logginUserAlumniID)
    {
        DataTable dt = new DataTable();
        dt = GetAllMentorsProfiles(logginUserAlumniID);
        if (dt.Rows.Count > 0)
        {
            lblCountMentors.Text = dt.Rows.Count.ToString();
        }
        else
        {
            lblCountMentors.Text = "0";
        }
    }

    private void FillMenteeProfileRepeter(int logginUserAlumniID)
    {
        DataTable dt = new DataTable();
        dt = GetAllMenteesProfiles(logginUserAlumniID);
        if (dt.Rows.Count > 0)
        {
            lblCountMentees.Text = dt.Rows.Count.ToString();
        }
        else
        {
            lblCountMentees.Text = "0";
        }
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

    private void FillMentorProfileRepeterRolewise()
    {
        DataTable dt = new DataTable();
        dt = GetAllMentorsProfilesRoleWise();
        if (dt.Rows.Count > 0)
        {
            lblCountMentors.Text = dt.Rows.Count.ToString();
        }
        else
        {
            lblCountMentors.Text = "0";
        }
    }

    private void FillMenteeProfileRepeterRolewise()
    {
        DataTable dt = new DataTable();
        dt = GetAllMenteesProfilesRoleWise();
        if (dt.Rows.Count > 0)
        {
            lblCountMentees.Text = dt.Rows.Count.ToString();
        }
        else
        {
            lblCountMentees.Text = "0";
        }
    }

    private DataTable GetAllMentorsProfiles(int AlmniId)
    {
        ClearArrayLists();
        names.Add("@alumniID"); types.Add(SqlDbType.Int); values.Add(AlmniId);
        return DAobj.GetDataTable("ALM_GetAll_Mentors_Profiles", values, names, types);
    }

    private DataTable GetAllMenteesProfiles(int AlmniId)
    {
        ClearArrayLists();
        names.Add("@alumniID"); types.Add(SqlDbType.Int); values.Add(AlmniId);
        return DAobj.GetDataTable("ALM_GetAll_Mentees_Profiles", values, names, types);
    }

    private DataTable GetAllMentorsProfilesRoleWise()
    {
        ClearArrayLists();
        return DAobj.GetDataTable("ALM_GetAll_Mentors_Profiles_RoleWise", values, names, types);
    }

    private DataTable GetAllMenteesProfilesRoleWise()
    {
        ClearArrayLists();
        return DAobj.GetDataTable("ALM_GetAll_Mentees_Profiles_Rolewise", values, names, types);
    }

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    #region "Stored Procedures"

    public StoredProcedure HPU_ALM_Mentorcheck(int pk_Regid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_ApproveMentorcheck", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
        return sp;
    }

    public StoredProcedure ALM_MenteeStatuscheck(int pk_Regid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Menteecheck", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
        return sp;
    }

    public StoredProcedure getloggedUserMenteeID(int alumniID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_MenteeID", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@alumniID", alumniID, DbType.Int32);
        return sp;
    }

    #endregion

    public List<MenteeRequest> getMenteeRequests()
    {
        List<MenteeRequest> listMRMRequests = new List<MenteeRequest>();

        string userSessionAlumniID = Session["AlumniID"] != null ? Session["AlumniID"].ToString() : "0";

        int alumniId = Convert.ToInt32(userSessionAlumniID);
        DataSet dsMRM = getReceivedMenteeMessagesStatus(alumniId).GetDataSet();

        if (dsMRM.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drMRM in dsMRM.Tables[0].Rows)
            {
                MenteeRequest objMRM = new MenteeRequest();
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

    public static StoredProcedure mrmStatusUpdation(int mRMID, string actionMRMRemarks)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_MenteeRequests_For_Mentors_Acc_Or_Rej", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_MRID", mRMID, DbType.Int32);
        sp.Command.AddParameter("@actionTaken", actionMRMRemarks, DbType.String);
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
                ClientMessaging("Request Accepted Successfully.!!!");
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
                ClientMessaging("Request Rejected Successfully.!!!");
                menteeMessages();

                String scriptMsgPopUp = String.Format("document.getElementById('exampleModalScrollableMentor').style.display = 'block';" +
                   "document.getElementById('fade1').style.display = 'block';");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);

            }
        }
    }

    protected void menteeMessages()
    {
        try
        {
            if (Session["AlumniID"] != null)
            {
                if (Session["AlumniID"].ToString() != "")
                {
                    List<MenteeRequest> mentorRequests = getMenteeRequests();
                    rptMRM.DataSource = mentorRequests;
                    rptMRM.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message.ToString());
        }
    }

    protected void lnkMenteeRequested_Click(object sender, EventArgs e)
    {
        menteeRequestedMsgDetails();
    }

    protected void menteeRequestedMsgDetails()
    {
        try
        {
            if (Session["AlumniID"] != null)
            {
                if (Session["AlumniID"].ToString() != "")
                {
                    List<MenteeRequest> menteeRequests = getMenteeRequests();
                    rptMRM.DataSource = menteeRequests;
                    rptMRM.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message.ToString());
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }


    //-------------------------------------Vijay
    protected bool isMentor { get; set; }
    protected bool isMentee { get; set; }

    private void setButtonStates()
    {
        bool isMentor = checkIfUserIsMentor();
        bool isMentee = checkIfUserIsMentee();

        if (isMentor)
        {
            //btnBecomeMentor.Enabled = true;
            //btnBecomeMentee.Enabled = false;
        }
        else if (isMentee)
        {
            //btnBecomeMentee.Enabled = true;
            //btnBecomeMentor.Enabled = false;
        }
        else
        {
            btnBecomeMentor.Enabled = true;
            btnBecomeMentee.Enabled = true;
        }
    }

    private bool checkIfUserIsMentor()
    {
        if (Session["AlumniID"] != null && !string.IsNullOrEmpty(Session["AlumniID"].ToString()))
        {
            int alumniId = Convert.ToInt32(Session["AlumniID"]);
            DataSet ds = HPU_ALM_Mentorcheck(alumniId).GetDataSet();

            return ds.Tables[0].Rows.Count > 0;
        }
        return false;
    }

    private bool checkIfUserIsMentee()
    {
        if (Session["AlumniID"] != null && !string.IsNullOrEmpty(Session["AlumniID"].ToString()))
        {
            int alumniId = Convert.ToInt32(Session["AlumniID"]);
            DataSet ds = ALM_MenteeStatuscheck(alumniId).GetDataSet();

            return ds.Tables[0].Rows.Count > 0;
        }
        return false;
    }

    protected void btnBecomeMentor_Click(object sender, EventArgs e)
    {
        if (Session["AlumniID"] == null)
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
        else
        {
            int loggedUserAlumniID = Convert.ToInt32(Session["AlumniID"].ToString());

            bool isloggedUserMentor = checkIfUserIsMentor(loggedUserAlumniID);
            bool isloggedUserMentee = checkIfUserIsMentee(loggedUserAlumniID);

            if (isloggedUserMentor)
            {
                ClientMessaging("You have already registered for mentor.");
                Response.Redirect("../Alumni/Alm_BecomeMentor.aspx");
            }
            else if (isloggedUserMentee)
            {
                ClientMessaging("You have already registered for mentee.");
                return;
            }
            else
            {
                Response.Redirect("../Alumni/Alm_BecomeMentor.aspx");
            }
        }
        //Response.Redirect("../Alumni/Alm_BecomeMentor.aspx");      
    }

    protected void btnBecomeMentee_Click(object sender, EventArgs e)
    {
        if (Session["AlumniID"] == null)
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
        else
        {
            int loggedUserAlumniID = Convert.ToInt32(Session["AlumniID"].ToString());

            bool isloggedUserMentor = checkIfUserIsMentor(loggedUserAlumniID);
            bool isloggedUserMentee = checkIfUserIsMentee(loggedUserAlumniID);

            if (isloggedUserMentor)
            {
                ClientMessaging("You have already registered for mentor.");
                return;
            }
            else if (isloggedUserMentee)
            {
                ClientMessaging("You have already registered for mentee.");
                Response.Redirect("../Alumni/Alm_BecomeMentee.aspx");
            }
            else
            {
                Response.Redirect("../Alumni/Alm_BecomeMentee.aspx");
            }
        }
        //Response.Redirect("../Alumni/Alm_BecomeMentee.aspx");
    }

    protected void lnkAdminMentorshipRequest_Click(object sender, EventArgs e)
    {
        mentorshipMsgDetailsV();
    }

    protected void lnkMenteeRequestedV_Click(object sender, EventArgs e)
    {
        menteeRequestedMsgDetailsV();
    }

    protected void menteeRequestedMsgDetailsV()
    {
        try
        {
            if (Session["AlumniID"] != null)
            {
                if (Session["AlumniID"].ToString() != "")
                {
                    List<MenteeRequest> menteeRequests = getMenteeRequestsV();
                    rptMRM.DataSource = menteeRequests;
                    rptMRM.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message.ToString());
        }
    }

    public List<MenteeRequest> getMenteeRequestsV()
    {
        List<MenteeRequest> listMRMRequests = new List<MenteeRequest>();

        string userSessionAlumniID = Session["AlumniID"] != null ? Session["AlumniID"].ToString() : "0";

        int alumniId = Convert.ToInt32(userSessionAlumniID);
        DataSet dsMRM = getReceivedMenteeMessagesStatusV(alumniId).GetDataSet();

        if (dsMRM.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drMRM in dsMRM.Tables[0].Rows)
            {
                MenteeRequest objMRM = new MenteeRequest();
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

    public StoredProcedure getReceivedMenteeMessagesStatusV(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Mentee_Requests_Received_Msg_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
        return sp;
    }

    protected void rptMRMV_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string requestId = e.CommandArgument.ToString();

        if (e.CommandName.ToUpper().ToString().Trim() == "ACCEPT")
        {
            UpdateMRMStatusV(requestId, "ACCEPTED");
        }
        else if (e.CommandName.ToUpper().ToString().Trim() == "REJECT")
        {
            UpdateMRMStatusV(requestId, "REJECTED");
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    private void UpdateMRMStatusV(string requestId, string status)
    {
        if (status.ToUpper().ToString().Trim() == "ACCEPTED")
        {
            int AlumniI = mrmStatusUpdation(Convert.ToInt32(requestId), status).Execute();

            if (AlumniI > 0)
            {
                ClientMessaging("Request Accepted Successfully.!!!");
                menteeMessagesV();

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
                ClientMessaging("Request Rejected Successfully.!!!");
                menteeMessagesV();

                String scriptMsgPopUp = String.Format("document.getElementById('exampleModalScrollableMentor').style.display = 'block';" +
                   "document.getElementById('fade1').style.display = 'block';");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);

            }
        }
    }

    protected void menteeMessagesV()
    {
        try
        {
            if (Session["AlumniID"] != null)
            {
                if (Session["AlumniID"].ToString() != "")
                {
                    List<MenteeRequest> mentorRequests = getMenteeRequests();
                    rptMRM.DataSource = mentorRequests;
                    rptMRM.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message.ToString());
        }
    }

    public static StoredProcedure mrmStatusUpdationV(int mRMID, string actionMRMRemarks)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_MenteeRequests_For_Mentors_Acc_Or_Rej", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_MRID", mRMID, DbType.Int32);
        sp.Command.AddParameter("@actionTaken", actionMRMRemarks, DbType.String);
        return sp;
    }

    protected void mentorshipMsgDetailsV()
    {
        try
        {
            if (Session["AlumniID"] != null)
            {
                if (Session["AlumniID"].ToString() != "")
                {
                    List<MentorRequest> mentorRequests = getMentorshipRequestsV();
                    rptMR.DataSource = mentorRequests;
                    rptMR.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message.ToString());
        }
    }

    public List<MentorRequest> getMentorshipRequestsV()
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

    private void UpdateRequestStatus(string requestId, string status)
    {
        if (status.ToUpper().ToString().Trim() == "ACCEPTED")
        {
            int AlumniI = mentorshipRequestStatusUpdationV(Convert.ToInt32(requestId), status).Execute();

            if (AlumniI > 0)
            {
                ClientMessaging("Request Accepted Successfully.!!!");
                MentorMessagesV();

                String scriptMsgPopUp = String.Format("document.getElementById('exampleModalScrollableMentor').style.display = 'block';" +
                   "document.getElementById('fade1').style.display = 'block';");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);
            }
        }
        else if (status.ToUpper().ToString().Trim() == "REJECTED")
        {
            int AlumniI = mentorshipRequestStatusUpdationV(Convert.ToInt32(requestId), status).Execute();

            if (AlumniI > 0)
            {
                ClientMessaging("Request Rejected Successfully.!!!");
                MentorMessagesV();

                String scriptMsgPopUp = String.Format("document.getElementById('exampleModalScrollableMentor').style.display = 'block';" +
                   "document.getElementById('fade1').style.display = 'block';");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);

            }
        }
    }

    protected void MentorMessagesV()
    {
        try
        {
            if (Session["AlumniID"] != null)
            {
                if (Session["AlumniID"].ToString() != "")
                {
                    List<MentorRequest> mentorRequests = GetMentorRequestsV();
                    rptMR.DataSource = mentorRequests;
                    rptMR.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message.ToString());
        }
    }

    public List<MentorRequest> GetMentorRequestsV()
    {
        List<MentorRequest> listRequests = new List<MentorRequest>();

        string userSessionAlumniID = Session["AlumniID"] != null ? Session["AlumniID"].ToString() : "0";

        int alumniId = Convert.ToInt32(userSessionAlumniID);
        DataSet dsS = getReceivedMessagesV(alumniId).GetDataSet();

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

    #region "Stored Procedures"

    //public StoredProcedure HPU_ALM_Mentorcheck(int pk_Regid)
    //{
    //    SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_ApproveMentorcheck", DataService.GetInstance("IUMSNXG"), "");
    //    sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
    //    return sp;
    //}

    //public StoredProcedure ALM_MenteeStatuscheck(int pk_Regid)
    //{
    //    SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Menteecheck", DataService.GetInstance("IUMSNXG"), "");
    //    sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
    //    return sp;
    //}

    public StoredProcedure getReceivedMentorshipMessages(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Mentorship_Request_For_Mentor_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
        return sp;
    }

    public StoredProcedure getReceivedRequestOnMentorLoggedUser(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Received_Request_On_Mentor_LoggedUser_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure mentorshipRequestStatusUpdationV(int mRID, string actionRemarks)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Mentorship_Requests_Status_Acc_Or_Rej", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_MReqID", mRID, DbType.Int32);
        sp.Command.AddParameter("@actionTaken", actionRemarks, DbType.String);
        return sp;
    }

    public StoredProcedure getReceivedMessagesV(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Received_Messages_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
        return sp;
    }

    #endregion


}

public class MenteeRequest
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
public class MentorRequest
{
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

//public class MenteeRequest
//{
//    public Int32 pk_MRID { get; set; }
//    public string messageText { get; set; }
//    public DateTime sentDate { get; set; }
//    public Int32 pk_MReqID { get; set; }
//    public int senderID { get; set; }
//    public string senderName { get; set; }
//    public int receiverID { get; set; }
//    public string receiverName { get; set; }
//    public string seekingHelpFor { get; set; }
//    public string requestStatus { get; set; }
//    public string email { get; set; }
//    public DateTime dob { get; set; }
//    public string sentAt { get; set; }
//    public bool isVisible { get; set; }
//    public bool isSendMsgNow { get; set; }
//    public bool isAccepted { get; set; }
//}