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
using System.IO;
using DataAccessLayer;
using System.Drawing;
using SubSonic;
using Anthem;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class Alumni_ALM_Mentors_ViewAll : System.Web.UI.Page
{
    public int pk_MentorReqID { get; set; }
    public string xmlDoc { get; private set; }
    public string mode { get; private set; }
    public int senderMsgAlumniID { get; set; }
    public int receiverMsgAlumniID { get; set; }

    public int pk_MsgID { get; set; }
    public int mReqID { get; set; }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    #region "Common Objects"

    CommonFunction cfobj = new CommonFunction();
    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList();
    ArrayList types = new ArrayList();
    ArrayList values = new ArrayList();
    ArrayList size = new ArrayList();
    ArrayList outtype = new ArrayList();

    crypto crp = new crypto();

    #endregion

    #region "Page Events"

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (Session["AlumniID"].ToString() != "" && Session["AlumniID"] != null)
    //    {
    //        if (!IsPostBack)
    //        {
    //            if (Session["AlumniID"].ToString() == "717" && Session["AlumniID"] != null)
    //            {
    //                FillMentorProfileRepeterRolewise();
    //            }
    //            else
    //            {
    //                senderMsgAlumniID = Convert.ToInt32(Session["AlumniID"].ToString());
    //                FillMentorProfileRepeter(senderMsgAlumniID);
    //                clearControls();
    //            }
    //        }
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AlumniID"] != null && !string.IsNullOrWhiteSpace(Session["AlumniID"].ToString()))
        {
            if (!IsPostBack)
            {
                string alumniIdStr = Session["AlumniID"].ToString();
                int senderMsgAlumniID;

                if (int.TryParse(alumniIdStr, out senderMsgAlumniID))
                {
                    FillMentorProfileRepeter(senderMsgAlumniID);
                    clearControls();
                }
            }
        }
        else
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
    }

    private void FillMentorProfileRepeter(int logginUserAlumniID)
    {
        DataTable dt = new DataTable();
        dt = GetAllMentorsProfile(logginUserAlumniID);
        if (dt.Rows.Count > 0)
        {
            RepMentorProfile.DataSource = dt;
            RepMentorProfile.DataBind();
            lblProfileCnt.Text = dt.Rows.Count.ToString() + " Mentors Profile Records";
        }
        else
        {
            lblProfileCnt.Text = " No Mentors Profile Records";
        }
    }

    private void FillMentorProfileRepeterRolewise()
    {
        DataTable dt = new DataTable();
        dt = GetAllMentorsProfilesRoleWise();
        if (dt.Rows.Count > 0)
        {
            RepMentorProfile.DataSource = dt;
            RepMentorProfile.DataBind();
            lblProfileCnt.Text = dt.Rows.Count.ToString() + " Mentors Profile Records";
        }
        else
        {
            lblProfileCnt.Text = "No Mentors Profile Records";
        }
    }

    //// Populate the alumni :
    //protected void BindMentorsLists()
    //{
    //    chkboxListsMentors.Items.Clear();

    //    DataTable dt = new DataTable();
    //    dt = GetAllMentorsProfile();

    //    if (dt.Rows.Count > 0)
    //    {
    //        string currentAlumniId = Session["AlumniID"].ToString();

    //        DataTable filteredMentors = dt.AsEnumerable()
    //            .Where(row => row["pk_alumniid"].ToString() != currentAlumniId)
    //            .CopyToDataTable();

    //        chkboxListsMentors.DataSource = filteredMentors;
    //        chkboxListsMentors.DataValueField = "pk_alumniid";
    //        chkboxListsMentors.DataTextField = "alumni_name";
    //        chkboxListsMentors.DataBind();
    //    }
    //}

    //// Populate the alumni :
    //protected void BindMenteesLists()
    //{
    //    chkboxListsMentees.Items.Clear();

    //    DataTable dt = new DataTable();
    //    dt = GetAllMenteesProfile();

    //    if (dt.Rows.Count > 0)
    //    {
    //        string currentAlumniId = Session["AlumniID"].ToString();

    //        DataTable filteredMentees = dt.AsEnumerable()
    //            .Where(row => row["pk_alumniid"].ToString() != currentAlumniId)
    //            .CopyToDataTable();

    //        chkboxListsMentees.DataSource = filteredMentees;
    //        chkboxListsMentees.DataValueField = "pk_alumniid";
    //        chkboxListsMentees.DataTextField = "alumni_name";
    //        chkboxListsMentees.DataBind();
    //    }
    //}

    private DataTable GetAllMentorsProfile(int AlmniId)
    {
        ClearArrayLists();
        names.Add("@alumniID"); types.Add(SqlDbType.Int); values.Add(AlmniId);
        return DAobj.GetDataTable("ALM_GetAll_Mentors_Profiles", values, names, types);
    }

    private DataTable GetAllMentorsProfilesRoleWise()
    {
        ClearArrayLists();
        return DAobj.GetDataTable("ALM_GetAll_Mentors_Profiles_RoleWise", values, names, types);
    }

    private DataTable GetAllMenteesProfile()
    {
        ClearArrayLists();
        return DAobj.GetDataTable("ALM_GetAll_Mentees_Profiles", values, names, types);
    }

    protected void ClearArrayLists()
    {
        //names.Clear(); values.Clear(); types.Clear();
        names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
    }

    #endregion

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    public string GetBase64Image(string imagePath)
    {
        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
        string base64String = Convert.ToBase64String(imageBytes);
        string mimeType = System.Web.MimeMapping.GetMimeMapping(imagePath);
        return string.Format("data:{0}; base64, {1}", mimeType, base64String);
    }

    protected void btnViewProfile_Click(object sender, EventArgs e)
    {
        //    try
        //    {
        //        int AlumniID = Convert.ToInt32(Session["AlumniID"].ToString());
        //        getMentorBasicDetails(AlumniID);

        //        //String script = String.Format("document.getElementById('exampleModalScrollableMentor').style.display = 'block';" +
        //        //      "document.getElementById('fade1').style.display = 'block';");
        //        //Anthem.Manager.IncludePageScripts = true;
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", script, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ClientMessaging(ex.Message.ToString());
        //    }

        try
        {
            if (Session["ProfileId"] == null || Session["ProfileId"].ToString() == "")
            {
                btnSendMentorMsg.Visible = false;
                btnSendMenteeMSG.Visible = false;
            }
            else if (Session["ProfileId"].ToString() == "1")
            {
                btnSendMentorMsg.Visible = false;
                btnSendMenteeMSG.Visible = false;
            }
            else if(Session["ProfileId"].ToString() == "2")
            {
                btnSendMentorMsg.Visible = false;
                btnSendMenteeMSG.Visible = false;
            }
            else if (Session["ProfileId"].ToString() == "3")
            {
                btnSendMentorMsg.Visible = false;
                btnSendMenteeMSG.Visible = true;
            }

            Anthem.Button btn = sender as Anthem.Button;
            if (btn != null && btn.CommandName.ToUpper().ToString() == "MENTOR")
            {
                pk_MentorReqID = Convert.ToInt32(btn.CommandArgument.ToString());
                ViewState["MReqID"] = pk_MentorReqID;
                getMentorBasicDetails(pk_MentorReqID);
            }
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message);
        }
    }

    protected void getMentorBasicDetails(int mReqID)
    {
        try
        {
            //DataSet dsDA = getMentorDetails(mReqID).GetDataSet();

            senderMsgAlumniID = Session["AlumniID"] != null ? Convert.ToInt32(Session["AlumniID"].ToString()) : 0;

            DataSet dsDA = getMentorDetailsForLoggedMenteeUser(mReqID, senderMsgAlumniID).GetDataSet();

            if (dsDA != null && dsDA.Tables[0].Rows.Count > 0)
            {
                lblAlumniName.Text = dsDA.Tables[0].Rows[0]["alumni_name"].ToString();
                lblPassingYear.Text = dsDA.Tables[0].Rows[0]["yearofpassing"].ToString();
                lblDegree.Text = dsDA.Tables[0].Rows[0]["degreename"].ToString();
                lblDept.Text = dsDA.Tables[0].Rows[0]["DeptName"].ToString();

                string fileName = "";

                fileName = dsDA.Tables[0].Rows[0]["Files_Unique_Name"].ToString().Trim();

                //if (fileName != "")
                //{
                //    string imgPath = ReturnPhysicalPath() + "\\Alumni\\StuImage\\" + fileName;
                //    string base64Image = GetBase64Image(imgPath);
                //    imgProfile.ImageUrl = base64Image;
                //}
                //else
                //{
                //    imgProfile.ImageUrl = "~/alumni/stuimage/No_image.png";
                //}

                if (fileName != "")
                {
                    imgProfile.ImageUrl = dsDA.Tables[0].Rows[0]["fileUrlPic"].ToString();
                }
                else
                {
                    imgProfile.ImageUrl = "https://alumni.hpushimla.in/Alumni/StuImage/default-user.jpg";
                }

                lblOfferHelpIn.Text = dsDA.Tables[0].Rows[0]["OfferingHelpIn"].ToString();
                lblOfferingHelpTo.Text = dsDA.Tables[0].Rows[0]["OfferingHelpTo"].ToString();
                lblDomains.Text = dsDA.Tables[0].Rows[0]["Domainss"].ToString();
                lblSkills.Text = dsDA.Tables[0].Rows[0]["skills_mentor"].ToString();
                lblMentorMsg.Text = dsDA.Tables[0].Rows[0]["message_mentor"].ToString();
                lblRequestStatus.Text = dsDA.Tables[0].Rows[0]["requestStatus"].ToString();

                //lblMentorName.Text = dsDA.Tables[0].Rows[0]["alumni_name"].ToString();
                hdnMReqID.Value = dsDA.Tables[0].Rows[0]["pk_mdtlid"].ToString();
                hdnMAlumniID.Value = dsDA.Tables[0].Rows[0]["pk_alumniid"].ToString();

                pk_MentorReqID = mReqID;

                senderMsgAlumniID = Session["AlumniID"] != null ? Convert.ToInt32(Session["AlumniID"].ToString()) : 0;
                receiverMsgAlumniID = hdnMAlumniID.Value != null ? Convert.ToInt32(hdnMAlumniID.Value.ToString()) : 0;

                bool isSenderMentee = checkIfUserIsMentee(senderMsgAlumniID);
                bool isReceiverMentor = checkIfUserIsMentor(receiverMsgAlumniID);

                if (senderMsgAlumniID == receiverMsgAlumniID)
                {
                    ClientMessaging("Please choose a different mentor to send the request as the logged-in user.");
                    ddl_goal.Focus();
                    btnSendMenteeMSG.Enabled = false;
                    btnSendMenteeMSG.Visible = false;
                    return;
                }
                else if (senderMsgAlumniID != receiverMsgAlumniID)
                {
                    if (lblRequestStatus.Text.ToUpper() == "ACCEPTED")
                    {
                        btnSendMentorMsg.Enabled = false;
                        btnSendMentorMsg.Visible = false;

                        if (isSenderMentee == true && dsDA.Tables[0].Rows[0]["isBtnEnabledForMessageToEachOther"].ToString().ToUpper() == "FALSE")
                        {
                            btnSendMenteeMSG.Enabled = true;
                            btnSendMenteeMSG.Visible = true;
                        }
                        else if (isReceiverMentor && dsDA.Tables[0].Rows[0]["isBtnEnabledForMessageToEachOther"].ToString().ToUpper() == "TRUE")
                        {
                            btnSendMenteeMSG.Enabled = false;
                            btnSendMenteeMSG.Visible = false;
                        }
                    }
                    else if (lblRequestStatus.Text.ToUpper() == "" && isReceiverMentor == true && isSenderMentee == false)
                    {
                        btnSendMenteeMSG.Enabled = false;
                        btnSendMenteeMSG.Visible = false;
                    }
                    else if (lblRequestStatus.Text.ToUpper() == "" && isReceiverMentor == true && isSenderMentee == true)
                    {
                        btnSendMenteeMSG.Enabled = true;
                        btnSendMenteeMSG.Visible = true;
                    }
                    else
                    {
                        if (isSenderMentee != isReceiverMentor)
                        {
                            ClientMessaging("Please choose a cross mentor/mentee for conversation request from logged-in user.");
                            ddl_goal.Focus();
                            btnSendMenteeMSG.Enabled = false;
                            btnSendMenteeMSG.Visible = false;
                            return;
                        }
                        else
                        {
                            btnSendMenteeMSG.Enabled = false;
                            btnSendMenteeMSG.Visible = false;
                        }
                    }
                }
                Anthem.Manager.IncludePageScripts = true;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }

    public StoredProcedure getMentorMessages(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_MentorMessages_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure getMentorDetails(int? mentorReqID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_Get_Mentor_Details", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_MentorReqID", mentorReqID, DbType.Int32);
        return sp;
    }

    public static StoredProcedure getMentorDetailsForLoggedMenteeUser(int? mentorReqID, int menteeAlumniID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_Get_Mentor_Details_for_LoggedUserMentee", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_MentorReqID", mentorReqID, DbType.Int32);
        sp.Command.AddParameter("@senderMenteeAlumniID", menteeAlumniID, DbType.Int32);
        return sp;
    }

    protected void btnMentor_Click(object sender, EventArgs e)
    {

    }

    public string ReturnPhysicalPath()
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
                    return dr["Physical_Path"].ToString().Trim();
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSendMentorMsg_Click(object sender, EventArgs e)
    {
        Anthem.Button btn = sender as Anthem.Button;

        if (btn != null && btn.CommandName.ToUpper().ToString() == "MENTOR")
        {
            string mentorReqID = ViewState["MReqID"].ToString();
            getMentorBasicDetails(Convert.ToInt32(mentorReqID));
        }

        string scriptMsgPopUp = "showMsgPopUp()";
        Anthem.Manager.IncludePageScripts = true;
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), scriptMsgPopUp, true);

        BindCategory();
    }

    protected void btnGoToMentorProfile_Click(object sender, EventArgs e)
    {
        Anthem.Button btn = sender as Anthem.Button;
        string Query = "";

        //string mReqAlumniID = btn.CommandArgument.ToString();

        string mReqAlumniID = hdnMAlumniID.Value.ToString();

        string encId = crp.EncodeString(Convert.ToInt32(mReqAlumniID));

        if (!string.IsNullOrEmpty(mReqAlumniID))
        {
            Query = "~//Alumni//Alm_Alumni_Show_Alumni_Profile_Search.aspx?ID=" + encId.ToString();
        }
        Response.Redirect(Query);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSubmit.CommandName.ToUpper().ToString() == "SUBMIT")
            {
                if (!validation())
                {
                    String scriptMsgPopUp = String.Format("document.getElementById('modalPopUpMentor').style.display = 'block';" +
                   "document.getElementById('fade1').style.display = 'block';");
                    Anthem.Manager.IncludePageScripts = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);

                    ClientMessaging("Please select a goal for this session.");
                    ddl_goal.Focus();
                    return;
                }


                senderMsgAlumniID = Session["AlumniID"] != null ? Convert.ToInt32(Session["AlumniID"].ToString()) : 0;
                receiverMsgAlumniID = hdnMAlumniID.Value != null ? Convert.ToInt32(hdnMAlumniID.Value.ToString()) : 0;

                if (senderMsgAlumniID == receiverMsgAlumniID)
                {
                    ClientMessaging("Please choose a different mentor to send the request as the logged-in user.");
                    ddl_goal.Focus();
                    return;
                }
                else
                {
                    string message = "";
                    DataSet DsMain = new DataSet();
                    DsMain = GetMain();
                    xmlDoc = DsMain.GetXml();

                    if (InsertRecord(ref message) > 0)
                    {
                        ClientMessaging("Request Submitted Successfully.!!!");
                        clearControls();
                    }
                    else
                    {
                        ClientMessaging("Something went wrong.!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message.ToString());
        }
    }

    protected bool validation()
    {
        if (string.IsNullOrEmpty(ddl_goal.SelectedValue) || ddl_goal.SelectedIndex == 0)
        {
            ClientMessaging("Please select a goal for this session.");
            ddl_goal.Focus();
            return false;
        }
        return true;
    }

    public int InsertRecord(ref string Message)
    {
        ClearArrayLists();
        names.Add("@doc"); values.Add(xmlDoc); types.Add(SqlDbType.VarChar);
        if (DAobj.ExecuteTransactionMsg("Alm_insert_Mentorship_Requests", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    protected DataSet GetMain()
    {
        try
        {
            int mentorReqID = Convert.ToInt32(ViewState["MReqID"].ToString());
            int goal = Convert.ToInt32(ddl_goal.SelectedValue.ToString());
            bool hasPersonalMsg = chk_personal.Checked;
            string message = hasPersonalMsg ? txtMessage.Text.Trim() : "";

            DataSet ds = DAobj.GetSchema("ALM_Mentorship_Requests");
            DataRow dr = ds.Tables[0].NewRow();
            dr["pk_MReqID"] = "0";
            dr["goalForSession"] = Convert.ToInt32(ddl_goal.SelectedValue.ToString());
            dr["isPersonalRequest"] = chk_personal.Checked == true ? Convert.ToBoolean(1) : Convert.ToBoolean(0);
            dr["messageText"] = message;
            dr["fk_SenderMsgAlumniID"] = senderMsgAlumniID;
            dr["fk_ReceiverMsgAlumniID"] = receiverMsgAlumniID;

            //string selectedMentors = string.Empty; string selectedMentees = string.Empty;

            //if (rdblist.SelectedItem.Text.ToString() == "Mentor")
            //{
            //    List<string> selectedMentorValues = new List<string>();

            //    foreach (ListItem item in chkboxListsMentors.Items)
            //    {
            //        if (item.Selected)
            //        {
            //            selectedMentorValues.Add(item.Value);
            //        }
            //    }
            //    // Join selected mentor values as comma-separated string
            //    selectedMentors = string.Join(",", selectedMentorValues);

            //    dr["mentorsAlumniIDs"] = selectedMentors;
            //}
            //else if (rdblist.SelectedItem.Text.ToString() == "Mentee")
            //{
            //    List<string> selectedMenteeValues = new List<string>();

            //    foreach (ListItem item in chkboxListsMentees.Items)
            //    {
            //        if (item.Selected)
            //        {
            //            selectedMenteeValues.Add(item.Value);
            //        }
            //    }
            //    // Join selected mentees values as comma-separated string
            //    selectedMentees = string.Join(",", selectedMenteeValues);

            //    dr["menteesAlumniIDs"] = selectedMentees;
            //}

            ds.Tables[0].Rows.Add(dr);
            return ds;
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message.ToString());
        }
        return null;
    }

    private void clearControls()
    {
        BindCategory();
        ddl_goal.SelectedIndex = 0;
        chk_personal.Checked = false;
        chk_personal_CheckedChanged(null, null);
        //BindRadioButtonList();
        //rdblist.SelectedIndex = -1;
        //chkboxListsMentors.Items.Clear();
        //chkboxListsMentees.Items.Clear();
        txtMessagess.Text = "";
        msgPnl.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearControls();
    }

    protected void chk_personal_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_personal.Checked)
        {
            txtMessage.Visible = true;
        }
        else
        {
            txtMessage.Text = "";
            txtMessage.Visible = false;
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void BindCategory()
    {
        DataSet ds = BindDropdown().GetDataSet();
        ddl_goal.DataSource = ds.Tables[0];
        ddl_goal.DataTextField = "Description";
        ddl_goal.DataValueField = "pk_ProblemId";
        ddl_goal.DataBind();
        ddl_goal.Items.Insert(0, new ListItem("-- Select your option --", "0"));
    }

    protected void fillMenteeProblems()
    {
        DataSet ds = BindDropdown().GetDataSet();
        ddlHelpIn.DataSource = ds.Tables[0];
        ddlHelpIn.DataTextField = "Description";
        ddlHelpIn.DataValueField = "pk_ProblemId";
        ddlHelpIn.DataBind();
        ddlHelpIn.Items.Insert(0, new ListItem("-- Select your option --", "0"));
    }

    public static StoredProcedure BindDropdown()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_probemfacingBind", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    protected string GetCardClass(string name)
    {
        string currentUser = Session["AlumniID"] != null ? Session["AlumniID"].ToString() : "0";
        if (string.Equals(name, currentUser, StringComparison.OrdinalIgnoreCase))
        {
            return "highlight-card";
        }
        return "regular-card";
    }

    //protected void rdblist_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (rdblist.SelectedItem.Text.ToString() == "Mentor")
    //        {
    //            pnlMentor.Style["display"] = "block";
    //            pnlMentee.Style["display"] = "none";
    //            BindMentorsLists();
    //        }
    //        else if (rdblist.SelectedItem.Text.ToString() == "Mentee")
    //        {
    //            pnlMentor.Style["display"] = "none";
    //            pnlMentee.Style["display"] = "block";
    //            BindMenteesLists();
    //        }
    //        else
    //        {
    //            pnlMentor.Style["display"] = "none";
    //            pnlMentee.Style["display"] = "none";
    //        }
    //        Anthem.Manager.IncludePageScripts = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        ClientMessaging(ex.Message.ToString());
    //    }
    //}

    //private void BindRadioButtonList()
    //{
    //    rdblist.Items.Clear();
    //    rdblist.Items.Add(new ListItem("Mentor", "1"));
    //    rdblist.Items.Add(new ListItem("Mentee", "2"));
    //    rdblist.SelectedIndex = -1;
    //}

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

    #endregion

    protected void btnSendMenteeMSG_Click(object sender, EventArgs e)
    {
        Anthem.Button btn = sender as Anthem.Button;

        if (btn != null && btn.CommandName.ToUpper().ToString() == "MENTORPROFILE" && checkIfUserIsMentor(Convert.ToInt32(hdnMAlumniID.Value)))
        {
            string mentorReqID = ViewState["MReqID"].ToString();
            getMentorBasicDetails(Convert.ToInt32(mentorReqID));
        }

        string scriptMsgPopUp = "showMsgPopUp()";
        Anthem.Manager.IncludePageScripts = true;
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), scriptMsgPopUp, true);

        fillMenteeProblems();
    }

    protected void btnSendCancel_Click(object sender, EventArgs e)
    {

    }

    protected void btnMRSendMsg_Click(object sender, EventArgs e)
    {

    }

    protected void chkIsPersonal_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIsPersonal.Checked)
        {
            txtMRMessage.Visible = true;
        }
        else
        {
            txtMRMessage.Text = "";
            txtMRMessage.Visible = false;
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void btnMRCancel_Click(object sender, EventArgs e)
    {
        clearMRControls();
    }

    protected bool validationMR()
    {
        if (string.IsNullOrEmpty(ddlHelpIn.SelectedValue) || ddlHelpIn.SelectedIndex == 0)
        {
            ClientMessaging("Please select a seeking help for.");
            ddlHelpIn.Focus();
            return false;
        }
        return true;
    }

    protected void btnMRSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnMRSubmit.CommandName.ToUpper().ToString() == "SUBMIT")
            {
                if (!validationMR())
                {
                    String scriptMsgPopUp = String.Format("document.getElementById('exampleModalMenteeMSG').style.display = 'block';" +
                   "document.getElementById('fade1').style.display = 'block';");
                    Anthem.Manager.IncludePageScripts = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);

                    ClientMessaging("Please select a seeking help for.");
                    ddlHelpIn.Focus();
                    return;
                }

                senderMsgAlumniID = Session["AlumniID"] != null ? Convert.ToInt32(Session["AlumniID"].ToString()) : 0;
                receiverMsgAlumniID = hdnMAlumniID.Value != null ? Convert.ToInt32(hdnMAlumniID.Value.ToString()) : 0;

                bool isReceiverMentor = checkIfUserIsMentor(receiverMsgAlumniID);
                bool isSenderMentee = checkIfUserIsMentee(senderMsgAlumniID);

                if (senderMsgAlumniID == receiverMsgAlumniID)
                {
                    ClientMessaging("Please choose a different mentor to send the request as the logged-in user.");
                    ddlHelpIn.Focus();
                    return;
                }
                else if (senderMsgAlumniID != receiverMsgAlumniID && (isReceiverMentor == isSenderMentee))
                {
                    string message = "";
                    DataSet DsMain = new DataSet();
                    DsMain = GetMainMR();
                    xmlDoc = DsMain.GetXml();

                    if (InsertRecordMR(ref message) > 0)
                    {
                        ClientMessaging("Request Submitted Successfully.!!!");
                        clearMRControls();
                    }
                    else
                    {
                        ClientMessaging("Something went wrong.!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message.ToString());
        }
    }

    private void clearMRControls()
    {
        fillMenteeProblems();
        ddlHelpIn.SelectedIndex = 0;
        chkIsPersonal.Checked = false;
        chkIsPersonal_CheckedChanged(null, null);
    }

    protected DataSet GetMainMR()
    {
        try
        {
            int mentorReqID = Convert.ToInt32(ViewState["MReqID"].ToString());
            int goal = Convert.ToInt32(ddlHelpIn.SelectedValue.ToString());
            bool hasChkPersonalMsg = chkIsPersonal.Checked;
            string messageMR = hasChkPersonalMsg ? txtMRMessage.Text.Trim() : "";

            DataSet ds = DAobj.GetSchema("ALM_MenteeRequests_For_Mentors");
            DataRow dr = ds.Tables[0].NewRow();
            dr["pk_MRID"] = "0";
            dr["seekingHelpFor"] = Convert.ToInt32(ddlHelpIn.SelectedValue.ToString());
            dr["isPersonalRequest"] = chkIsPersonal.Checked == true ? Convert.ToBoolean(1) : Convert.ToBoolean(0);
            dr["messageText"] = messageMR;
            dr["fk_SenderRID"] = senderMsgAlumniID;
            dr["fk_ReceiverRID"] = receiverMsgAlumniID;
            ds.Tables[0].Rows.Add(dr);
            return ds;
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message.ToString());
        }
        return null;
    }

    public int InsertRecordMR(ref string Message)
    {
        ClearArrayLists();
        names.Add("@xmlDoc"); values.Add(xmlDoc); types.Add(SqlDbType.VarChar);
        if (DAobj.ExecuteTransactionMsg("ALM_MenteeRequests_For_Mentors_Insert", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    protected void btnMsgNow_Click(object sender, EventArgs e)
    {
        try
        {
            Anthem.Button btn = sender as Anthem.Button;
            if (btn != null && btn.CommandName.ToUpper().ToString() == "MSGNOW")
            {
                hdnSelectedAlumniID.Value = btn.CommandArgument.ToString();
                hdnSelectedAlumniID.UpdateAfterCallBack = true;
                hdnpk_MReqID.UpdateAfterCallBack = true;
                msgPnl.Visible = false; msgPnl.UpdateAfterCallBack = true;
                pnlMsg.Visible = false; pnlMsg.UpdateAfterCallBack = true;

                int mentorAlumniID;
                string commandArg = btn.CommandArgument.ToString() ?? "0";

                if (int.TryParse(commandArg, out mentorAlumniID) && mentorAlumniID > 0)
                {
                    hdnSelectedAlumniID.Value = mentorAlumniID.ToString();
                    int menteeAlumniID = Session["AlumniID"].ToString() == "" ? 0 : Convert.ToInt32(Session["AlumniID"].ToString());
                    List<MentorRequest> mentorRequests = getMentorRequests(menteeAlumniID, mentorAlumniID);
                    rptMentorRequests.DataSource = mentorRequests;
                    rptMentorRequests.DataBind();

                    int receiverAlumniID = Convert.ToInt32(hdnSelectedAlumniID.Value.ToString());
                    int senderAlumniID = Convert.ToInt32(Session["AlumniID"].ToString());

                    DataSet dsMsgs = getReceivedMessagesEachOther(receiverAlumniID, senderAlumniID).GetDataSet();

                    if (dsMsgs.Tables[0].Rows.Count > 0)
                    {
                        rptReceivedMsgs.DataSource = dsMsgs.Tables[0];
                        rptReceivedMsgs.DataBind();
                        pnlMsg.Visible = true;
                    }
                    else
                    {
                        rptReceivedMsgs.DataSource = null;
                        rptReceivedMsgs.DataBind();
                        pnlMsg.Visible = false;
                    }
                }
                else
                {
                    hdnSelectedAlumniID.Value = "0";
                }

            }
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message);
        }
    }

    protected bool validationMsg()
    {
        string messageText = string.Empty;
        messageText = txtMessagess.Text.Trim().ToString();

        if (string.IsNullOrEmpty(messageText))
        {
            ClientMessaging("Message is Required.!!!");
            txtMessagess.Focus();
            return false;
        }

        if (!string.IsNullOrEmpty(messageText) && messageText.Length > 1000)
        {
            ClientMessaging("Message can not exceed 1000 characters.");
            txtMessagess.Focus();
            return false;
        }

        // if (!System.Text.RegularExpressions.Regex.IsMatch(messageText, @"^[a-zA-Z0-9 .,?!'""()\-\r\n]*$"))
        // {
            // ClientMessaging("Message contains invalid characters.");
            // txtMessagess.Focus();
            // return false;
        // }

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

        if (senderMsgAlumniID == receiverMsgAlumniID)
        {
            ClientMessaging("Please choose a different mentor to send the request as the logged-in user.");
            return false;
        }

        return true;
    }

    protected void btnSendMsg_Click(object sender, EventArgs e)
    {
        try
        {
            if (!validationMsg())
            {
                String scriptMsgPopUp = String.Format("document.getElementById('modalPopUpMentee').style.display = 'block';" +
                      "document.getElementById('fade1').style.display = 'block';");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);

                ClientMessaging("Message is Required.!!!");
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
                ClientMessaging("Message Send Successfully.!!!");
                clearMsgControls();
            }
        }
        catch (SqlException ex)
        {
            lblMsgss.Text = DAobj.ShowSQLErrorMsg(ex.Message.ToString().Trim(), "", ex);
        }
    }

    protected DataSet GetMainMsgss()
    {
        DataSet dsMain = DAobj.GetSchema("ALM_Mentors_Mentees_Messages_Mst");
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
            if (DAobj.ExecuteTransactionMsg("ALM_Mentors_Mentees_Messages_Insert", values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("S", "");
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
        cfobj.ClearTextSetDropDown(this);
        txtMessagess.Text = "";
        Anthem.Manager.IncludePageScripts = true;

        int receiverAlumniID = Convert.ToInt32(hdnSelectedAlumniID.Value.ToString());
        int senderAlumniID = Convert.ToInt32(Session["AlumniID"].ToString());

        DataSet dsMsgs = getReceivedMessagesEachOther(receiverAlumniID, senderAlumniID).GetDataSet();

        if (dsMsgs.Tables[0].Rows.Count > 0)
        {
            rptReceivedMsgs.DataSource = dsMsgs.Tables[0];
            rptReceivedMsgs.DataBind();
            pnlMsg.Visible = true;
        }
        else
        {
            rptReceivedMsgs.DataSource = null;
            rptReceivedMsgs.DataBind();
            pnlMsg.Visible = false;
        }

        msgPnl.Visible = true;

        String scriptMsgPopUp = String.Format("document.getElementById('modalPopUpMentorMsgss').style.display = 'block';" +
                      "document.getElementById('fade1').style.display = 'block';");
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);
    }

    public List<MentorRequest> getMentorRequests(int menteeALMID, int mentorALMID)
    {
        List<MentorRequest> listRequests = new List<MentorRequest>();
        int menteeID = Convert.ToInt32(Session["AlumniID"].ToString());
        //DataSet dsS = getReceivedMessages(alumniID).GetDataSet();
        DataSet dsS = getRequestDetailsMessagesStatus(menteeALMID, mentorALMID).GetDataSet();
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
                objMR.receiverID = Convert.ToInt32(drMR["receiverID"].ToString());
                objMR.receiverName = drMR["receiverName"].ToString();
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

    public StoredProcedure getReceivedMessages(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Received_Messages_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
        return sp;
    }

    public StoredProcedure getRequestDetailsMessagesStatus(int senderID, int receiverID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Request_Details_Msg_View_MenteeToMentor_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@senderID", senderID, DbType.Int32);
        sp.Command.AddParameter("@receiverID", receiverID, DbType.Int32);
        return sp;
    }

    public StoredProcedure getReceivedConversationMessages(int mentorID, int menteeID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Mentee_MentorConversation", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@MenteeId", menteeID, DbType.Int32);
        sp.Command.AddParameter("@Mentorid", mentorID, DbType.Int32);
        return sp;
    }

    protected void btnSendMsgss_Click(object sender, EventArgs e)
    {
        try
        {
            if (!validationMsg())
            {
                String scriptMsgPopUp = String.Format("document.getElementById('modalPopUpMentorMsgss').style.display = 'block';" +
                      "document.getElementById('fade1').style.display = 'block';");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);

                ClientMessaging("Message is Required.!!!");
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
                ClientMessaging("Message Send Successfully.!!!");
                clearMsgControls();
            }
        }
        catch (SqlException ex)
        {
            lblMsgss.Text = DAobj.ShowSQLErrorMsg(ex.Message.ToString().Trim(), "", ex);
        }
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
            try
            {
                string[] args = e.CommandArgument.ToString().Split('|');

                if (args.Length == 3)
                {
                    string userId = args[0];
                    string mReqId = args[1];
                    string receiverId = args[2];

                    hdnSelectedAlumniID.Value = receiverId;
                    hdnpk_MReqID.Value = mReqId;
                    int senderAlumniID = Convert.ToInt32(Session["AlumniID"].ToString());

                    //DataSet dsMsgs = getReceivedMessagesEachOther(Convert.ToInt32(receiverId), senderAlumniID).GetDataSet();

                    //if (dsMsgs.Tables[0].Rows.Count > 0)
                    //{
                    //    rptReceivedMsgs.DataSource = dsMsgs.Tables[0];
                    //    rptReceivedMsgs.DataBind();
                    //    pnlMsg.Visible = true;
                    //}
                    //else
                    //{
                    //    rptReceivedMsgs.DataSource = null;
                    //    rptReceivedMsgs.DataBind();
                    //    pnlMsg.Visible = false;
                    //}

                    msgPnl.Visible = true;

                    hdnSelectedAlumniID.UpdateAfterCallBack = true;
                    hdnpk_MReqID.UpdateAfterCallBack = true;
                    msgPnl.UpdateAfterCallBack = true;
                }
            }
            catch (Exception)
            {
                throw;
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
                ClientMessaging("Request Accepted Successfully.!!!");

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
                ClientMessaging("Request Rejected Successfully.!!!");

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

    //public StoredProcedure getReceivedMessagesEachOther(int alumniid)
    //{
    //    SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Received_Messages_WithEachOther", DataService.GetInstance("IUMSNXG"), "");
    //    sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
    //    return sp;
    //}

    public StoredProcedure getReceivedMessagesEachOther(int mentorID, int menteeID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Received_Messages_WithEachOther_FromMenteeToMentor", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@FromMenteeID", menteeID, DbType.Int32);
        sp.Command.AddParameter("@ToMentorID", mentorID, DbType.Int32);
        return sp;
    }

    protected void btnCloseMsgPopUp_Click(object sender, EventArgs e)
    {
        String scriptMsgPopUp = String.Format("document.getElementById('modalPopUpMentorMsgss').style.display = 'none';" +
                      "document.getElementById('fade1').style.display = 'none';");
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);

        if (Session["AlumniID"] != null && !string.IsNullOrWhiteSpace(Session["AlumniID"].ToString()))
        {
            string alumniIdStr = Session["AlumniID"].ToString();
            int senderMsgAlumniID;

            if (int.TryParse(alumniIdStr, out senderMsgAlumniID))
            {
                FillMentorProfileRepeter(senderMsgAlumniID);
                clearControls();
            }
        }
        else
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
    }

    protected void btnCloseModal_Click(object sender, EventArgs e)
    {
        string scriptMsgPopUp = "document.getElementById('modalPopUpMentorMsgss').style.display = 'none';" +
                                "document.getElementById('fade1').style.display = 'none';";

        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "HideModal", scriptMsgPopUp, true);

        if (Session["AlumniID"] != null && !string.IsNullOrWhiteSpace(Session["AlumniID"].ToString()))
        {
            string alumniIdStr = Session["AlumniID"].ToString();
            int senderMsgAlumniID;

            if (int.TryParse(alumniIdStr, out senderMsgAlumniID))
            {
                FillMentorProfileRepeter(senderMsgAlumniID);
                clearControls();
            }
        }
        else
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
    }
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

public class Messages
{
    public Int32 pk_MsgID { get; set; }
    public string messageText { get; set; }
    public DateTime sentDate { get; set; }
    public int senderID { get; set; }
    public string senderName { get; set; }
    public int receiverID { get; set; }
    public string receiverName { get; set; }
    public string sentAt { get; set; }
}