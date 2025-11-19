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

public partial class Alumni_ALM_MyPreferences : System.Web.UI.Page
{
    ArrayList names = new ArrayList();
    ArrayList types = new ArrayList();
    ArrayList values = new ArrayList();
    DataAccess DAobj = new DataAccess();

    public int menteeID { get; set; }
    public int mentorID { get; set; }

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

                    //lnkViewAllMentors.Visible = isloggedUserMentor;
                    //lnkViewAllMentors.Text = isloggedUserMentor ? "Click here" : string.Empty;

                    //lnkViewAllMentees.Visible = isloggedUserMentee;
                    //lnkViewAllMentees.Text = isloggedUserMentee ? "Click here" : string.Empty;

                    //lnkRemoveMentor.Visible = isloggedUserMentor;
                    //lnkRemoveMentor.Text = isloggedUserMentor ? "Click here" : string.Empty;

                    //lnkRemoveMentee.Visible = isloggedUserMentee;
                    //lnkRemoveMentee.Text = isloggedUserMentee ? "Click here" : string.Empty;
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
            lblPreferencesMentor.Text = "";
        }
        else
        {
            lblPreferencesMentor.Text = "0";
        }
    }

    private void FillMenteeProfileRepeter(int logginUserAlumniID)
    {
        DataTable dt = new DataTable();
        dt = GetAllMenteesProfiles(logginUserAlumniID);
        if (dt.Rows.Count > 0)
        {
            lblPreferencesMentee.Text = "";
        }
        else
        {
            lblPreferencesMentee.Text = "0";
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
            lblPreferencesMentor.Text = dt.Rows.Count.ToString();
        }
        else
        {
            lblPreferencesMentor.Text = "0";
        }
    }

    private void FillMenteeProfileRepeterRolewise()
    {
        DataTable dt = new DataTable();
        dt = GetAllMenteesProfilesRoleWise();
        if (dt.Rows.Count > 0)
        {
            lblPreferencesMentee.Text = "";
        }
        else
        {
            lblPreferencesMentee.Text = "0";
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

    public StoredProcedure getloggedUserMentorID(int alumniID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_MentorID", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@alumniID", alumniID, DbType.Int32);
        return sp;
    }

    #endregion

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
                    List<MenteeRequestModel> mentorRequests = getMenteeRequests();
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
                    List<MenteeRequestModel> menteeRequests = getMenteeRequests();
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

    protected void lnkViewAllMentors_Click(object sender, EventArgs e)
    {
        if (Session["AlumniID"].ToString() != "" && Session["AlumniID"] != null)
        {
            int loggedUserAlumniID = Convert.ToInt32(Session["AlumniID"].ToString());
            bool isloggedUserMentor = checkIfUserIsMentor(loggedUserAlumniID);

            if (isloggedUserMentor)
            {
                Response.Redirect("../Alumni/ALM_EditPreferencesMentor.aspx");
            }
            else
            {
                ClientMessaging("No mentor record found.");
            }
        }
    }

    protected void lnkViewAllMentees_Click(object sender, EventArgs e)
    {
        if (Session["AlumniID"].ToString() != "" && Session["AlumniID"] != null)
        {
            int loggedUserAlumniID = Convert.ToInt32(Session["AlumniID"].ToString());
            bool isloggedUserMentee = checkIfUserIsMentee(loggedUserAlumniID);

            if (isloggedUserMentee)
            {
                Response.Redirect("../Alumni/ALM_EditPreferencesMentee.aspx");
            }
            else
            {
                ClientMessaging("No mentee record found.");
            }
        }
    }

    //protected void lnkRemoveMentor_Click(object sender, EventArgs e)
    //{
    //    int loggedUserAlumniID = Convert.ToInt32(Session["AlumniID"].ToString());

    //    DataSet dsS = getloggedUserMentorID(loggedUserAlumniID).GetDataSet();

    //    if (dsS.Tables[0].Rows.Count > 0)
    //    {
    //        if (!string.IsNullOrEmpty(dsS.Tables[0].Rows[0]["MentorID"].ToString()))
    //        {
    //            mentorID = Convert.ToInt32(dsS.Tables[0].Rows[0]["MentorID"].ToString());
    //        }
    //    }

    //    if ((deleteBecomeMentorDetailsByMentorID(mentorID)).Execute() > 0)
    //    {
    //        ClientMessaging("Record Deleted Successfully");
    //    }
    //}

    protected void lnkRemoveMentor_Click(object sender, EventArgs e)
    {
        try
        {
            int userAlumniID = 0; int userMentorID = 0;

            if (Session["AlumniID"] == null)
            {
                Response.Redirect("../Alumin_Loginpage.aspx");
                return;
            }

            if (!int.TryParse(Session["AlumniID"].ToString(), out userAlumniID))
            {
                ClientMessaging("Invalid session user.");
                return;
            }

            DataSet dsMentor = getloggedUserMentorID(userAlumniID).GetDataSet();

            if (dsMentor.Tables.Count > 0 && dsMentor.Tables[0].Rows.Count > 0)
            {
                DataRow row = dsMentor.Tables[0].Rows[0];

                if (row["MentorID"] != DBNull.Value && int.TryParse(row["MentorID"].ToString(), out userMentorID))
                {
                    int result = deleteBecomeMentorDetailsByMentorID(userMentorID).Execute();

                    if (result > 0)
                    {
                        ClientMessaging("Mentor record removed successfully.");
                    }
                    else
                    {
                        //ClientMessaging("Mentor record is in used for mentorship request or have messages history.");
                        ClientMessaging("No Mentor Record found.");
                    }
                }
                else
                {
                    ClientMessaging("No Mentor Record found.");
                }
            }
            else
            {
                ClientMessaging("No mentor record found.");
            }
        }
        catch (Exception ex)
        {
            ClientMessaging("An error occurred while deleting mentor: " + ex.Message);
        }
    }

    //protected void lnkRemoveMentee_Click(object sender, EventArgs e)
    //{
    //    int loggedUserAlumniID = Convert.ToInt32(Session["AlumniID"].ToString());

    //    DataSet dsS = getloggedUserMenteeID(loggedUserAlumniID).GetDataSet();

    //    if (dsS.Tables[0].Rows.Count > 0)
    //    {
    //        if (!string.IsNullOrEmpty(dsS.Tables[0].Rows[0]["MenteeID"].ToString()))
    //        {
    //            menteeID = Convert.ToInt32(dsS.Tables[0].Rows[0]["MenteeID"].ToString());
    //        }
    //    }

    //    if ((deleteBecomeMenteeDetailsByMenteeID(menteeID)).Execute() > 0)
    //    {
    //        ClientMessaging("Record Deleted Successfully");
    //    }
    //}

    protected void lnkRemoveMentee_Click(object sender, EventArgs e)
    {
        try
        {
            int userAlumniID = 0; int userMenteeID = 0;

            if (Session["AlumniID"] == null)
            {
                Response.Redirect("../Alumin_Loginpage.aspx");
                return;
            }

            if (!int.TryParse(Session["AlumniID"].ToString(), out userAlumniID))
            {
                ClientMessaging("Invalid session user.");
                return;
            }

            DataSet dsMentee = getloggedUserMenteeID(userAlumniID).GetDataSet();

            if (dsMentee.Tables.Count > 0 && dsMentee.Tables[0].Rows.Count > 0)
            {
                DataRow row = dsMentee.Tables[0].Rows[0];

                if (row["MenteeID"] != DBNull.Value && int.TryParse(row["MenteeID"].ToString(), out userMenteeID))
                {
                    int result = deleteBecomeMenteeDetailsByMenteeID(userMenteeID).Execute();

                    if (result > 0)
                    {
                        ClientMessaging("Mentee record removed successfully.");
                    }
                    else
                    {
                        //ClientMessaging("Mentee record is in used for mentorship request or have messages history.");
                        ClientMessaging("No Mentee Record found.");
                    }
                }
                else
                {
                    ClientMessaging("No Mentee Record found.");
                }
            }
            else
            {
                ClientMessaging("No mentee record found.");
            }
        }
        catch (Exception ex)
        {
            ClientMessaging("An error occurred: " + ex.Message);
        }
    }
	
    public static StoredProcedure deleteBecomeMentorDetailsByMentorID(int mentorID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Become_Mentor_Details_Remove_By_MentorID", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_MentorID", mentorID, DbType.Int32);
        return sp;
    }

    public static StoredProcedure deleteBecomeMenteeDetailsByMenteeID(int menteeID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Become_Mentee_Details_Remove_By_LoggedUser", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_MenteeID", menteeID, DbType.Int32);
        return sp;
    }
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