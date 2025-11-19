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