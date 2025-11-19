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

public partial class Alumni_ALM_Mentees_ViewAll : System.Web.UI.Page
{
    public int pk_MenteeReqID { get; set; }
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

    public static StoredProcedure getMenteeDetails(int? menteeReqID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_Get_Mentee_Details", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_MenteeReqID", menteeReqID, DbType.Int32);
        return sp;
    }

    #region "Page Events"

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (Session["AlumniID"].ToString() != "" && Session["AlumniID"] != null)
    //    {
    //        if (!IsPostBack)
    //        {

    //            if (Session["AlumniID"].ToString() == "717" && Session["AlumniID"] != null)
    //            {
    //                FillMenteeProfileRepeterRolewise();
    //            }
    //            else
    //            {
    //                int AlumniID = Convert.ToInt32(Session["AlumniID"].ToString());
    //                FillMenteeProfileRepeter(AlumniID);
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
                    FillMenteeProfileRepeter(senderMsgAlumniID);
                    clearControls();
                }
            }
        }
        else
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
    }

    private void FillMenteeProfileRepeter(int logginUserAlumniID)
    {
        DataTable dt = new DataTable();
        dt = GetAllMenteesProfile(logginUserAlumniID);
        if (dt.Rows.Count > 0)
        {
            RepMenteeProfile.DataSource = dt;
            RepMenteeProfile.DataBind();
            lblProfileCnt.Text = dt.Rows.Count.ToString() + " Mentees Profile Records";
        }
        else
        {
            lblProfileCnt.Text = "No Mentees Profile Records";
        }
    }

    private void FillMenteeProfileRepeterRolewise()
    {
        DataTable dt = new DataTable();
        dt = GetAllMenteesProfilesRoleWise();
        if (dt.Rows.Count > 0)
        {
            RepMenteeProfile.DataSource = dt;
            RepMenteeProfile.DataBind();
            lblProfileCnt.Text = dt.Rows.Count.ToString() + " Mentees Profile Records";
        }
        else
        {
            lblProfileCnt.Text = "No Mentees Profile Records";
        }
    }

    private DataTable GetAllMenteesProfile(int AlmniId)
    {
        ClearArrayLists();
        names.Add("@alumniID"); types.Add(SqlDbType.Int); values.Add(AlmniId);
        return DAobj.GetDataTable("ALM_GetAll_Mentees_Profiles", values, names, types);
    }

    private DataTable GetAllMenteesProfilesRoleWise()
    {
        ClearArrayLists();
        return DAobj.GetDataTable("ALM_GetAll_Mentees_Profiles_Rolewise", values, names, types);
    }

    protected void ClearArrayLists()
    {
        //names.Clear(); values.Clear(); types.Clear();
        names.Clear(); values.Clear(); types.Clear(); //size.Clear(); outtype.Clear();
    }

    #endregion

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    #region  

    #endregion

    #region "Button Search, Clear and Reset Event"

    protected void clearControls()
    {
        cfobj.ClearTextSetDropDown(this);
        txtMsg.Text = "";
        Anthem.Manager.IncludePageScripts = true;
        txtMessagess.Text = "";
        msgPnl.Visible = false;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        clearControls();
    }

    #endregion

    public string GetBase64Image(string imagePath)
    {
        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
        string base64String = Convert.ToBase64String(imageBytes);
        string mimeType = System.Web.MimeMapping.GetMimeMapping(imagePath);
        return string.Format("data:{0}; base64, {1}", mimeType, base64String);
    }

    protected void lnkViewProfile_Click(object sender, EventArgs e)
    {
        // Show modal using JavaScript
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#profileModal').modal('show');", true);
    }

    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        Session["dtArchive"] = null;
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", @"window.open(htmlContent, '_blank', 'toolbar=yes, scrollbars=yes, resizable=yes, top=500, left=500, width=400, height=400');", true);
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

    protected void getMenteeBasicDetails(int mReqID)
    {
        try
        {
            DataSet dsDA = getMenteeDetails(mReqID).GetDataSet();

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
                //    //string base64Image = GetBase64Image(imgPath);
                //    imgProfile.Src = base64Image;
                //}
                //else
                //{
                //    imgProfile.Src = "https://alumni.hpushimla.in/Alumni/StuImage/default-user.jpg";
                //}

                if (fileName != "")
                {
                    imgProfile.ImageUrl = dsDA.Tables[0].Rows[0]["fileUrlPic"].ToString();
                }
                else
                {
                    imgProfile.ImageUrl = "https://alumni.hpushimla.in/Alumni/StuImage/default-user.jpg";
                }

                lblOfferHelpIn.Text = dsDA.Tables[0].Rows[0]["topicTextWithCommaSeparated"].ToString();
                lblOfferingHelpTo.Text = dsDA.Tables[0].Rows[0]["Professional_Background"].ToString();
                lblDomains.Text = dsDA.Tables[0].Rows[0]["domains"].ToString();

                lblMenteeName.Text = dsDA.Tables[0].Rows[0]["alumni_name"].ToString();
                hdnMReqID.Value = dsDA.Tables[0].Rows[0]["Pk_Mentee_Reqid"].ToString();

                hdnMAlumniID.Value = dsDA.Tables[0].Rows[0]["pk_alumniid"].ToString();

                pk_MenteeReqID = mReqID;

                btnSendMenteeMsg.Visible = Convert.ToBoolean(dsDA.Tables[0].Rows[0]["isBtnEnabledForSendReqMsg"].ToString());
                btnSendMenteeMsg.Enabled = Convert.ToBoolean(dsDA.Tables[0].Rows[0]["isBtnEnabledForSendReqMsg"].ToString());

                Anthem.Manager.IncludePageScripts = true;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }

    protected void btnViewMenteeProfile_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["ProfileId"] == null || Session["ProfileId"].ToString() == "")
            {
                btnSendMenteeMsg.Visible = false;
            }
            else if(Session["ProfileId"].ToString() == "1")
            {
                btnSendMenteeMsg.Visible = false;
            }
            else if (Session["ProfileId"].ToString() == "2")
            {
                btnSendMenteeMsg.Visible = false;
            }
            else if (Session["ProfileId"].ToString() == "3")
            {
                btnSendMenteeMsg.Visible = true;
            }

            Anthem.Button btn = sender as Anthem.Button;
            if (btn != null && btn.CommandName == "MENTEE")
            {
                string menteeReqID = btn.CommandArgument.ToString();
                pk_MenteeReqID = Convert.ToInt32(menteeReqID);
                ViewState["MReqID"] = pk_MenteeReqID;
                getMenteeBasicDetails(pk_MenteeReqID);

                senderMsgAlumniID = Session["AlumniID"] != null ? Convert.ToInt32(Session["AlumniID"].ToString()) : 0;
                receiverMsgAlumniID = hdnMAlumniID.Value != null ? Convert.ToInt32(hdnMAlumniID.Value.ToString()) : 0;

                if (senderMsgAlumniID == receiverMsgAlumniID)
                {
                    ClientMessaging("Please choose a different mentor to send the request as the logged-in user.");
                    btnSendMenteeMsg.Visible = false;
                    btnSendMenteeMsg.Enabled = false;
                    return;
                }
                //btnSendMenteeMsg.Visible = true;
                //btnSendMenteeMsg.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message);
        }
    }

    protected void btnGoToMenteeProfile_Click(object sender, EventArgs e)
    {
        Anthem.Button btn = sender as Anthem.Button;
        string Query = "";

        string mReqAlumniID = ""; //btn.CommandArgument.ToString();

        mReqAlumniID = hdnMAlumniID.Value.ToString();

        string encId = crp.EncodeString(Convert.ToInt32(mReqAlumniID));

        if (!string.IsNullOrEmpty(encId))
        {
            Query = "~//Alumni//Alm_Alumni_Show_Alumni_Profile_Search.aspx?ID=" + encId.ToString();
        }
        Response.Redirect(Query);

        //if (btn != null && btn.CommandName == "MENTEEPROFILE")
        //{
        //    string mReqAlumniID = btn.CommandArgument.ToString();

        //    if (!string.IsNullOrEmpty(mReqAlumniID))
        //    {
        //        Query = "~//Alumni//Alm_Alumni_Show_Alumni_Profile.aspx?Alumni=" + mReqAlumniID.ToString();
        //    }
        //    Response.Redirect(Query);
        //}
    }

    protected void btnSendMenteeMsg_Click(object sender, EventArgs e)
    {
        Anthem.Button btn = sender as Anthem.Button;

        if (btn != null && btn.CommandName.ToUpper().ToString() == "MENTEE")
        {
            string menteeReqID = ViewState["MReqID"].ToString();
            getMenteeBasicDetails(Convert.ToInt32(menteeReqID));
        }

        string scriptMsgPopUp = "showMsgPopUp()";
        Anthem.Manager.IncludePageScripts = true;
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), scriptMsgPopUp, true);
    }

    protected bool validation()
    {
        if (string.IsNullOrEmpty(txtMsg.Text))
        {
            ClientMessaging("Message is Required.!!!");
            txtMsg.Focus();
            return false;
        }
        return true;
    }

    protected void btnSendMsg_Click(object sender, EventArgs e)
    {
        try
        {
            if (!validation())
            {
                String scriptMsgPopUp = String.Format("document.getElementById('modalPopUpMentee').style.display = 'block';" +
                      "document.getElementById('fade1').style.display = 'block';");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);

                ClientMessaging("Message is Required.!!!");
                txtMsg.Focus();
                return;
            }

            string Message = "";
            DataSet ds_details = new DataSet();
            ds_details = GetMain();
            xmlDoc = ds_details.GetXml();
            mode = "INSERT";

            if (ALM_Mentees_SendMessages_InsertRecord(ref Message) >= 0)
            {
                ClientMessaging("Message Send Successfully.!!!");
                clearControls();
            }
        }
        catch (SqlException ex)
        {
            lblMsg.Text = DAobj.ShowSQLErrorMsg(ex.Message.ToString().Trim(), "", ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string scriptMsgPopUp = "hideMsgPopUp()";
        Anthem.Manager.IncludePageScripts = true;
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), scriptMsgPopUp, true);
    }

    protected DataSet GetMain()
    {
        DataSet dsMain = DAobj.GetSchema("ALM_Mentees_SendMessages");
        DataRow dr = dsMain.Tables[0].NewRow();

        senderMsgAlumniID = Session["AlumniID"] != null ? Convert.ToInt32(Session["AlumniID"].ToString()) : 0;
        receiverMsgAlumniID = hdnMAlumniID.Value != null ? Convert.ToInt32(hdnMAlumniID.Value.ToString()) : 0;

        dr["pk_MsgID"] = 0;
        dr["messageText"] = txtMsg.Text.Trim().ToString();
        dr["fk_SenderMsgAlumniID"] = senderMsgAlumniID;
        dr["fk_ReceiverMsgAlumniID"] = receiverMsgAlumniID;
        dsMain.Tables[0].Rows.Add(dr);
        return dsMain;
    }

    public int ALM_Mentees_SendMessages_InsertRecord(ref string Message)
    {
        try
        {
            ClearArrayLists();
            names.Add("@xmlDoc"); types.Add(SqlDbType.VarChar); values.Add(xmlDoc);
            names.Add("@mode"); types.Add(SqlDbType.VarChar); values.Add(mode);
            names.Add("@pk_MsgID"); types.Add(SqlDbType.Int); values.Add(0);
            if (DAobj.ExecuteTransactionMsg("ALM_Mentees_SendMessages_Insert", values, names, types, ref Message) > 0)
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

    protected string GetCardClass(string name)
    {
        string currentUser = Session["AlumniID"] != null ? Session["AlumniID"].ToString() : "0";
        if (string.Equals(name, currentUser, StringComparison.OrdinalIgnoreCase))
        {
            return "highlight-card";
        }
        return "regular-card";
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
        else if (e.CommandName.ToUpper().ToString().Trim() == "SENDMSGNOW")
        {
            try
            {
                string[] args = e.CommandArgument.ToString().Split('|');

                if (args.Length == 3)
                {
                    string userId = args[0];
                    string mReqId = args[1];
                    string receiverId = userId;

                    hdnSelectedAlumniID.Value = receiverId;
                    hdnpk_MRID.Value = mReqId;
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
                    hdnpk_MRID.UpdateAfterCallBack = true;
                    msgPnl.UpdateAfterCallBack = true;
                }
            }
            catch (Exception ex)
            {
                ClientMessaging(ex.Message.ToString());
            }

            //try
            //{
            //    string[] args = e.CommandArgument.ToString().Split('|');

            //    if (args.Length != 3)
            //        return;

            //    string receiverId = args[0];
            //    string mReqId = args[1];

            //    hdnSelectedAlumniID.Value = receiverId;
            //    hdnpk_MRID.Value = mReqId;

            //    int senderAlumniID = Convert.ToInt32(Session["AlumniID"]);

            //    DataSet dsReqStatus = checkRequestedStatusEachOther(Convert.ToInt32(receiverId), senderAlumniID).GetDataSet();

            //    if (dsReqStatus.Tables[0].Rows.Count == 0)
            //        return;

            //    string mREQID = dsReqStatus.Tables[0].Rows[0]["pk_MRID"].ToString();

            //    if (mREQID != mReqId)
            //    {
            //        ClearMessages();
            //        return;
            //    }

            //    DataSet dsMsgs = getReceivedMessagesEachOther(Convert.ToInt32(receiverId), senderAlumniID).GetDataSet();

            //    if (dsMsgs.Tables[0].Rows.Count > 0)
            //    {
            //        rptReceivedMsgs.DataSource = dsMsgs.Tables[0];
            //        rptReceivedMsgs.DataBind();
            //        pnlMsg.Visible = true;
            //    }
            //    else
            //    {
            //        ClearMessages();
            //    }

            //    hdnSelectedAlumniID.UpdateAfterCallBack = true;
            //    hdnpk_MRID.UpdateAfterCallBack = true;
            //    msgPnl.UpdateAfterCallBack = true;
            //}
            //catch (Exception ex)
            //{
            //    ClientMessaging(ex.Message.ToString()); 
            //}
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    private void ClearMessages()
    {
        rptReceivedMsgs.DataSource = null;
        rptReceivedMsgs.DataBind();
        pnlMsg.Visible = false;
    }

    private void UpdateMRMStatus(string requestId, string status)
    {
        if (status.ToUpper().ToString().Trim() == "ACCEPTED")
        {
            int AlumniI = mrmStatusUpdation(Convert.ToInt32(requestId), status).Execute();

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
            int AlumniI = mrmStatusUpdation(Convert.ToInt32(requestId), status).Execute();

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

    public static StoredProcedure mrmStatusUpdation(int mRMID, string actionMRMRemarks)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_MenteeRequests_For_Mentors_Acc_Or_Rej", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_MRID", mRMID, DbType.Int32);
        sp.Command.AddParameter("@actionTaken", actionMRMRemarks, DbType.String);
        return sp;
    }

    //public StoredProcedure getReceivedMessagesEachOther(int alumniid)
    //{
    //    SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Received_Messages_WithEachOther", DataService.GetInstance("IUMSNXG"), "");
    //    sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
    //    return sp;
    //}

    public StoredProcedure getReceivedMessagesEachOther(int alumniid, int mentorid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Received_Messages_WithEachOther_FromMentorToMentee", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@FromMentorID", mentorid, DbType.Int32);
        sp.Command.AddParameter("@ToMenteeID", alumniid, DbType.Int32);
        return sp;
    }

    public StoredProcedure checkRequestedStatusEachOther(int alumniid, int mentorid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Requested_Status_Sended_FromMentorToMentee", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@FromMentorID", mentorid, DbType.Int32);
        sp.Command.AddParameter("@ToMenteeID", alumniid, DbType.Int32);
        return sp;
    }

    public List<MenteeRequest> getMenteeRequests(int senderAlumniID, int receiverAlumniID)
    {
        List<MenteeRequest> listMRMRequests = new List<MenteeRequest>();
        int Menterid = Convert.ToInt32(Session["AlumniID"].ToString());
        //DataSet dsMRM = getReceivedMenteeMessagesStatus(alumniID).GetDataSet();
        //DataSet dsMRM = getReceivedMessages(alumniID).GetDataSet();
        DataSet dsMRM = getRequestDetailsMessagesStatus(senderAlumniID, receiverAlumniID).GetDataSet();
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
                objMRM.receiverID = Convert.ToInt32(drMRM["receiverID"].ToString());
                listMRMRequests.Add(objMRM);
            }
        }
        return listMRMRequests;
    }

    public StoredProcedure getReceivedMenteeMessagesStatus(int alumniid, int mentorid)
    {
        //SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Mentee_Requests_Received_Msg_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Mentee_MentorConversation", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@MenteeId", alumniid, DbType.Int32);
        sp.Command.AddParameter("@Mentorid", mentorid, DbType.Int32);
        return sp;
    }

    public StoredProcedure getReceivedMessages(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Mentorship_Details_Msg_View_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@AlumniID", alumniid, DbType.Int32);
        return sp;
    }

    public StoredProcedure getRequestDetailsMessagesStatus(int senderID, int receiverID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Requested_Details_Msg_View_MentorToMentee_OnPortal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@senderID", senderID, DbType.Int32);
        sp.Command.AddParameter("@receiverID", receiverID, DbType.Int32);
        return sp;
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
                hdnpk_MRID.UpdateAfterCallBack = true;
                msgPnl.Visible = false; msgPnl.UpdateAfterCallBack = true;
                pnlMsg.Visible = false; pnlMsg.UpdateAfterCallBack = true;

                int alumniID;
                string commandArg = btn.CommandArgument.ToString() ?? "0";

                if (int.TryParse(commandArg, out alumniID) && alumniID > 0)
                {
                    hdnSelectedAlumniID.Value = alumniID.ToString();
                    int receiverAlumniID = Session["AlumniID"].ToString() == "" ? 0 : Convert.ToInt32(Session["AlumniID"].ToString());
                    List<MenteeRequest> mentorRequests = getMenteeRequests(alumniID, receiverAlumniID);
                    rptMRM.DataSource = mentorRequests;
                    rptMRM.DataBind();

                    int receiverALMID = Convert.ToInt32(hdnSelectedAlumniID.Value.ToString());
                    int senderALMID = Convert.ToInt32(Session["AlumniID"].ToString());

                    DataSet dsMsgs = getReceivedMessagesEachOther(Convert.ToInt32(receiverALMID), senderALMID).GetDataSet();

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

    protected void btnSendMsgss_Click(object sender, EventArgs e)
    {
        try
        {
            if (!validationMsg())
            {
                String scriptMsgPopUp = String.Format("document.getElementById('modalPopUpMenteeMsgss').style.display = 'block';" +
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

        if (!string.IsNullOrEmpty(hdnpk_MRID.Value) && int.TryParse(hdnpk_MRID.Value, out mrID))
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

        String scriptMsgPopUp = String.Format("document.getElementById('modalPopUpMenteeMsgss').style.display = 'block';" +
                      "document.getElementById('fade1').style.display = 'block';");
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);
    }

    protected void RepMenteeProfile_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Anthem.Button btnMsgNow = (Anthem.Button)e.Item.FindControl("btnMsgNow");

            if (btnMsgNow == null)
                return;

            if (Session["ProfileId"].ToString() == "1")
            {
                btnMsgNow.Visible = false;
                return;
            }

            int alumniID = Convert.ToInt32(Session["AlumniID"]);
            DataTable dt = GetAllMenteesProfile(alumniID);

            if (dt.Rows.Count > 0)
            {
                string currentMenteeId = DataBinder.Eval(e.Item.DataItem, "Pk_Mentee_Reqid").ToString();

                DataRow[] matchedRows = dt.Select("Pk_Mentee_Reqid = " + currentMenteeId);

                if (matchedRows.Length > 0)
                {
                    string isMsgNow = matchedRows[0]["isBtnEnabledForMessageToEachOther"].ToString();
                    btnMsgNow.Visible = (isMsgNow == "True");
                }
                else
                {
                    btnMsgNow.Visible = false;
                }
            }
            else
            {
                btnMsgNow.Visible = false;
            }
        }
    }

    protected void rptMRM_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var data = (MenteeRequest)e.Item.DataItem;
            var btnSendMessageNow = (Anthem.Button)e.Item.FindControl("btnSendMessageNow");

            if (btnSendMessageNow != null && Session["AlumniID"] != null)
            {
                btnSendMessageNow.Enabled = (data.receiverID.ToString() == Session["AlumniID"].ToString());
            }
        }
    }

    protected void btnCloseMsgPopUp_Click(object sender, EventArgs e)
    {
        String scriptMsgPopUp = String.Format("document.getElementById('modalPopUpMenteeMsgss').style.display = 'none';" +
                      "document.getElementById('fade1').style.display = 'none';");
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Showdiv", scriptMsgPopUp, true);

        if (Session["AlumniID"] != null && !string.IsNullOrWhiteSpace(Session["AlumniID"].ToString()))
        {
            string alumniIdStr = Session["AlumniID"].ToString();
            int senderMsgAlumniID;
            if (int.TryParse(alumniIdStr, out senderMsgAlumniID))
            {
                FillMenteeProfileRepeter(senderMsgAlumniID);
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
        String scriptMsgPopUp = String.Format("document.getElementById('modalPopUpMenteeMsgss').style.display = 'none';" +
                      "document.getElementById('fade1').style.display = 'none';");
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "HideModal", scriptMsgPopUp, true);

        if (Session["AlumniID"] != null && !string.IsNullOrWhiteSpace(Session["AlumniID"].ToString()))
        {
            string alumniIdStr = Session["AlumniID"].ToString();
            int senderMsgAlumniID;
            if (int.TryParse(alumniIdStr, out senderMsgAlumniID))
            {
                FillMenteeProfileRepeter(senderMsgAlumniID);
                clearControls();
            }
        }
        else
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
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