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
using System.Web.Hosting;
using System.Linq;
using System.Data.SqlClient;
using SubSonic;

public partial class Alumni_ALM_EditPreferencesMentee : System.Web.UI.Page
{
    DataAccess DAobj = new DataAccess();
    DataAccess Dobj = new DataAccess();
    ArrayList name = new ArrayList(); ArrayList type = new ArrayList(); ArrayList value = new ArrayList();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

    void clear()
    {
        name.Clear(); value.Clear(); type.Clear();
    }

    void clears()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    public string dsxml { get; set; }
    public int pk_MenteeID { get; set; }
    public int pk_alumnid { get; set; }

    public int InsertRecord(ref string Message)
    {
        clear();
        name.Add("@doc"); value.Add(dsxml); type.Add(SqlDbType.VarChar);
        if (DAobj.ExecuteTransactionMsg("Alm_insertMenteeReq", value, name, type, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int UpdateRecord(ref string Message)
    {
        clear();
        name.Add("@doc"); value.Add(dsxml); type.Add(SqlDbType.VarChar);
        name.Add("@pk_MenteeID"); value.Add(pk_MenteeID); type.Add(SqlDbType.Int);
        if (DAobj.ExecuteTransactionMsg("ALM_ChangePreferences_Mentee_Update", value, name, type, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public DataSet Fillconnects()
    {
        DataAccess obj = new DataAccess();
        DataSet ds = null;
        try
        {
            this.clear();
            ds = obj.GetDataSet("Alm_ConnectBind", value, name, type);
        }
        catch
        {

        }
        return ds;
    }

    private DataSet chkcount()
    {
        clears();
        names.Add("@Pk_MenteeReqid"); values.Add(pk_MenteeID); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("Alm_CheckmenteeReq", values, names, types);
    }

    private DataSet FillmenteeDetails(int menteeID)
    {
        clears();
        names.Add("@Pk_MenteeReqid"); values.Add(menteeID); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("Alm_FillmenteeDetails", values, names, types);
    }

    private DataSet getallreq(int almID)
    {
        clears();
        names.Add("@alumniid"); values.Add(almID); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("Alm_GetmenteeReq", values, names, types);
    }

    private DataSet ALM_CheckIfAlreadyApplied_ToBecomeAMentee()
    {
        clears();
        names.Add("@pk_AlumniId"); values.Add(pk_alumnid); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("ALM_Check_If_Already_Applied_To_Become_A_Mentee", values, names, types);
    }

    public DataSet FillToEmployee()
    {
        DataAccess obj = new DataAccess();
        DataSet ds = null;
        try
        {
            this.clear();
            ds = obj.GetDataSet("Alm_probemfacingBind", value, name, type);
        }
        catch
        {

        }
        return ds;
    }

    private bool IspageReferesh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["AlumniID"] != null)
            {
                pk_alumnid = Convert.ToInt32(Session["AlumniID"]);

                if (Session["alumniname"] != null)
                {
                    Labelname.Text = Session["alumniname"].ToString();
                    bindproblems();
                    bindconnects();
					ViewState["MenteeID"] = null;
                    getRequests(pk_alumnid);
                    //checkCountofmentee();
                    if (pk_MenteeID != 0)
                    {
                        filldata(pk_MenteeID);
                    }
                    lblmsg.Text = "";
                    lblmsg3.Text = "";
                    lblmsg1.Text = "";
                }
                else
                {
                    Response.Redirect("../Alumin_Loginpage.aspx");
                }
            }
            else
            {
                Response.Redirect("../Alumin_Loginpage.aspx");
            }
        }
    }

    private void DisableAllControls(Control parent)
    {
        foreach (Control control in parent.Controls)
        {
            if (control is Anthem.TextBox)
                ((Anthem.TextBox)control).Enabled = false;
            else if (control is Anthem.CheckBoxList)
                ((Anthem.CheckBoxList)control).Enabled = false;
            else if (control is Anthem.CheckBox)
                ((Anthem.CheckBox)control).Enabled = false;

            if (control.HasControls())
            {
                DisableAllControls(control);
            }
        }
    }

    protected void bindproblems()
    {
        DataSet ds = FillToEmployee();
        chkproblems.DataSource = ds;
        chkproblems.DataTextField = "Description";
        chkproblems.DataValueField = "pk_ProblemId";
        chkproblems.DataBind();
    }

    protected void bindconnects()
    {
        DataSet ds = Fillconnects();
        connectList.DataSource = ds;
        connectList.DataTextField = "Description";
        connectList.DataValueField = "pk_communicationTypeID";
        connectList.DataBind();
    }

    public void checkCountofmentee()
    {
        DataSet ds = chkcount();
        if (ds.Tables[1].Rows.Count == 0)
        {
            btnSave.Text = "SUBMIT";
            btnSave.CommandName = "SUBMIT";
        }
        else if (ds.Tables[0].Rows.Count > 0)
        {
            //filldata();
            //btnSave.Text = "UPDATE";
            //btnSave.CommandName = "UPDATE";
            btnSave.Enabled = false;
            btnSave.Visible = false;
        }
        else
        {
            //filldata();
            btnSave.Visible = false;
            txtspcfy.Enabled = false;
            txtbckground.Enabled = false;
            txtdomains.Enabled = false;
            chkproblems.Enabled = false;
            connectList.Enabled = false;
        }
    }

    public void ALM_CheckIfAlreadyAppliedForMentee()
    {
        DataSet ds = ALM_CheckIfAlreadyApplied_ToBecomeAMentee();
        if (ds.Tables[0].Rows.Count == 0)
        {
            btnSave.Text = "SUBMIT";
            btnSave.CommandName = "SUBMIT";
        }
        else if (ds.Tables[0].Rows.Count > 0)
        {
            //filldata();
            btnSave.Text = "UPDATE";
            btnSave.CommandName = "UPDATE";
        }
        else
        {
            //filldata();
            btnSave.Visible = false;
            txtspcfy.Enabled = false;
            txtbckground.Enabled = false;
            txtdomains.Enabled = false;
            chkproblems.Enabled = false;
            connectList.Enabled = false;
        }
    }

    //Get Mentee Request Details
    public void getRequests(int alumniID)
    {
        DataSet ds = getallreq(alumniID);
        if (ds.Tables[0].Rows.Count > 0)
        {
            pk_MenteeID = Convert.ToInt32(ds.Tables[0].Rows[0]["Pk_Mentee_Reqid"]);
            ViewState["MenteeID"] = pk_MenteeID;
        }
    }

    private bool checkIfUserIsMenteeUsedMentorship(int menteeRID)
    {
        bool flag = false;

        if (menteeRID > 0)
        {
            DataSet ds = isMenteeUsedMentorship(menteeRID).GetDataSet();

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["isUsedMentee"].ToString() == "1")
            {
                flag = true;
            }
            else if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["isUsedMentee"].ToString() == "1")
            {
                flag = false;
            }
            //return ds.Tables[0].Rows.Count > 0;
        }
        return flag;
    }

    public StoredProcedure isMenteeUsedMentorship(int menteeID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Is_Used_Mentee_For_Mentorship", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Pk_MenteeReqid", menteeID, DbType.Int32);
        return sp;
    }

    //Get all Form Data
    //public void filldata(int menteeID)
    //{
    //    try
    //    {
    //        bool isloggedUsedMentorship = checkIfUserIsMenteeUsedMentorship(menteeID);
    //        DataSet ds = FillmenteeDetails(menteeID);
    //        if (isloggedUsedMentorship)
    //        {
    //            if (ds.Tables.Count > 0)
    //            {
    //                for (int k = 0; k < ds.Tables[1].Rows.Count; k++)
    //                {
    //                    for (int j = 0; j < chkproblems.Items.Count; j++)
    //                    {
    //                        if (chkproblems.Items[j].Value.ToString() == ds.Tables[1].Rows[k]["fk_ProblemId"].ToString())
    //                        {
    //                            string selectedOtherID = chkproblems.Items[j].Value;
    //                            chkproblems.Items[j].Selected = true;

    //                            if (chkproblems.Items[j] != null && chkproblems.Items[j].Selected && chkproblems.Items[j].Text == "Other")
    //                            {
    //                                selectedOtherID = chkproblems.Items[j].Value;
    //                                pnl.Visible = true;
    //                                txtspcfy.Text = ds.Tables[0].Rows[0]["Others"].ToString();
    //                            }
    //                            else if (chkproblems.Items[j] != null && !chkproblems.Items[j].Selected && chkproblems.Items[j].Text != "Other")
    //                            {
    //                                pnl.Visible = false;
    //                            }
    //                            else if (chkproblems.Items[j] != null && !chkproblems.Items[j].Selected && chkproblems.Items[j].Text != "")
    //                            {
    //                                txtspcfy.Text = "";
    //                            }
    //                        }
    //                    }
    //                }

    //                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Professional_Background"].ToString()))
    //                {
    //                    txtbckground.Text = ds.Tables[0].Rows[0]["Professional_Background"].ToString();
    //                }
    //                else
    //                {
    //                    txtbckground.Text = "";
    //                }

    //                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["domains"].ToString()))
    //                {
    //                    txtdomains.Text = ds.Tables[0].Rows[0]["domains"].ToString();
    //                }
    //                else
    //                {
    //                    txtdomains.Text = "";
    //                }

    //                for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
    //                {
    //                    for (int j = 0; j < connectList.Items.Count; j++)
    //                    {
    //                        if (connectList.Items[j].Value.ToString() == ds.Tables[2].Rows[k]["fk_communicationTypeID"].ToString())
    //                        {
    //                            //connectList.Items[j].Selected = true;

    //                            string selectedCommID = connectList.Items[j].Value;
    //                            connectList.Items[j].Selected = true;

    //                            if (connectList.Items[j] != null && connectList.Items[j].Selected && connectList.Items[j].Text == "Other")
    //                            {
    //                                selectedCommID = connectList.Items[j].Value;
    //                                pnlComm.Visible = true;
    //                                txtOthersWayComm.Text = ds.Tables[0].Rows[0]["othersWayComm"].ToString();
    //                            }
    //                            else if (connectList.Items[j] != null && !connectList.Items[j].Selected && connectList.Items[j].Text != "Other")
    //                            {
    //                                pnlComm.Visible = false;
    //                                txtOthersWayComm.Text = "";
    //                            }
    //                            else if (connectList.Items[j] != null && !connectList.Items[j].Selected && connectList.Items[j].Text != "")
    //                            {
    //                                pnlComm.Visible = false;
    //                                txtOthersWayComm.Text = "";
    //                            }
    //                        }
    //                    }
    //                }

    //                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isDeclarationAccepted"].ToString()))
    //                {
    //                    chkDeclaration.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isDeclarationAccepted"].ToString().Trim());
    //                }
    //                else
    //                {
    //                    chkDeclaration.Checked = false;
    //                }
    //            }

    //            btnSave.Enabled = false;
    //            btnSave.Visible = false;
    //            DisableAllControls(this);

    //            ClientMessaging("Mentee record is in used for mentorship request or have messages history.");
    //        }
    //        else
    //        {
    //            for (int k = 0; k < ds.Tables[1].Rows.Count; k++)
    //            {
    //                for (int j = 0; j < chkproblems.Items.Count; j++)
    //                {
    //                    if (chkproblems.Items[j].Value.ToString() == ds.Tables[1].Rows[k]["fk_ProblemId"].ToString())
    //                    {
    //                        string selectedOtherID = chkproblems.Items[j].Value;
    //                        chkproblems.Items[j].Selected = true;

    //                        if (chkproblems.Items[j] != null && chkproblems.Items[j].Selected && chkproblems.Items[j].Text == "Other")
    //                        {
    //                            selectedOtherID = chkproblems.Items[j].Value;
    //                            pnl.Visible = true;
    //                            txtspcfy.Text = ds.Tables[0].Rows[0]["Others"].ToString();
    //                        }
    //                        else if (chkproblems.Items[j] != null && !chkproblems.Items[j].Selected && chkproblems.Items[j].Text != "Other")
    //                        {
    //                            pnl.Visible = false;
    //                        }
    //                        else if (chkproblems.Items[j] != null && !chkproblems.Items[j].Selected && chkproblems.Items[j].Text != "")
    //                        {
    //                            txtspcfy.Text = "";
    //                        }
    //                    }
    //                }
    //            }

    //            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Professional_Background"].ToString()))
    //            {
    //                txtbckground.Text = ds.Tables[0].Rows[0]["Professional_Background"].ToString();
    //            }
    //            else
    //            {
    //                txtbckground.Text = "";
    //            }

    //            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["domains"].ToString()))
    //            {
    //                txtdomains.Text = ds.Tables[0].Rows[0]["domains"].ToString();
    //            }
    //            else
    //            {
    //                txtdomains.Text = "";
    //            }

    //            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
    //            {
    //                for (int j = 0; j < connectList.Items.Count; j++)
    //                {
    //                    if (connectList.Items[j].Value.ToString() == ds.Tables[2].Rows[k]["fk_communicationTypeID"].ToString())
    //                    {
    //                        string selectedCommID = connectList.Items[j].Value;
    //                        connectList.Items[j].Selected = true;

    //                        if (connectList.Items[j] != null && connectList.Items[j].Selected && connectList.Items[j].Text == "Other")
    //                        {
    //                            selectedCommID = connectList.Items[j].Value;
    //                            pnlComm.Visible = true;
    //                            txtOthersWayComm.Text = ds.Tables[0].Rows[0]["othersWayComm"].ToString();
    //                        }
    //                        else if (connectList.Items[j] != null && !connectList.Items[j].Selected && connectList.Items[j].Text != "Other")
    //                        {
    //                            pnlComm.Visible = false;
    //                            txtOthersWayComm.Text = "";
    //                        }
    //                        else if (connectList.Items[j] != null && !connectList.Items[j].Selected && connectList.Items[j].Text != "")
    //                        {
    //                            pnlComm.Visible = false;
    //                            txtOthersWayComm.Text = "";
    //                        }
    //                    }
    //                }
    //            }

    //            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isDeclarationAccepted"].ToString()))
    //            {
    //                chkDeclaration.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isDeclarationAccepted"].ToString().Trim());
    //            }
    //            else
    //            {
    //                chkDeclaration.Checked = false;
    //            }

    //            btnSave.Text = "UPDATE";
    //            btnSave.CommandName = "UPDATE";
    //            btnSave.TextDuringCallBack = "UPDATE...";
    //            btnSave.Enabled = true;
    //            btnSave.Visible = true;
    //        }           
    //    }
    //    catch (Exception ex)
    //    {
    //        ClientMessaging(ex.Message.ToString());
    //    }
    //}

    public void filldata(int menteeID)
    {
        try
        {
            bool isloggedUsedMentorship = checkIfUserIsMenteeUsedMentorship(menteeID);

            DataSet ds = FillmenteeDetails(menteeID);

            if (ds == null || ds.Tables.Count < 3 || ds.Tables[0].Rows.Count == 0)
            {
                ClientMessaging("No mentee record was found.");
                btnSave.Enabled = false;
                btnSave.Visible = false;
                return;
            }

            DataRow mainRow = ds.Tables[0].Rows[0];

            BindProblems(ds.Tables[1], mainRow);
            BindProfessionalDetails(mainRow);
            BindCommunicationTypes(ds.Tables[2], mainRow);
            BindDeclaration(mainRow);

            //if (isloggedUsedMentorship)
            //{
            //    btnSave.Enabled = false;
            //    btnSave.Visible = false;
            //    DisableAllControls(this);
            //    ClientMessaging("Mentee record is in use for mentorship request or has message history.");
            //}
            //else
            //{
            //    btnSave.Text = "UPDATE";
            //    btnSave.CommandName = "UPDATE";
            //    btnSave.TextDuringCallBack = "UPDATE...";
            //    btnSave.Enabled = true;
            //    btnSave.Visible = true;
            //}
            btnSave.Text = "UPDATE";
            btnSave.CommandName = "UPDATE";
            btnSave.TextDuringCallBack = "UPDATE...";
            btnSave.Enabled = true;
            btnSave.Visible = true;
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message);
        }
    }

    private void BindProblems(DataTable problemTable, DataRow mainRow)
    {
        foreach (ListItem item in chkproblems.Items)
            item.Selected = false;

        foreach (DataRow row in problemTable.Rows)
        {
            string problemId = row["fk_ProblemId"].ToString();

            foreach (ListItem item in chkproblems.Items)
            {
                if (item.Value == problemId)
                {
                    item.Selected = true;

                    if (item.Text == "Other")
                    {
                        pnl.Visible = true;
                        txtspcfy.Text = mainRow["Others"].ToString() ?? "";
                    }
                }
            }
        }

        // Ensure visibility is properly managed
        if (!chkproblems.Items.Cast<ListItem>().Any(i => i.Selected && i.Text == "Other"))
        {
            pnl.Visible = false;
            txtspcfy.Text = "";
        }
    }

    private void BindProfessionalDetails(DataRow mainRow)
    {
        txtbckground.Text = mainRow["Professional_Background"].ToString() ?? "";
        txtdomains.Text = mainRow["domains"].ToString() ?? "";
    }

    private void BindCommunicationTypes(DataTable commTable, DataRow mainRow)
    {
        foreach (ListItem item in connectList.Items)
            item.Selected = false;

        foreach (DataRow row in commTable.Rows)
        {
            string commTypeId = row["fk_communicationTypeID"].ToString();

            foreach (ListItem item in connectList.Items)
            {
                if (item.Value == commTypeId)
                {
                    item.Selected = true;

                    if (item.Text == "Other")
                    {
                        pnlComm.Visible = true;
                        txtOthersWayComm.Text = mainRow["othersWayComm"].ToString() ?? "";
                    }
                }
            }
        }

        // Ensure visibility is properly managed
        if (!connectList.Items.Cast<ListItem>().Any(i => i.Selected && i.Text == "Other"))
        {
            pnlComm.Visible = false;
            txtOthersWayComm.Text = "";
        }
    }

    private void BindDeclaration(DataRow mainRow)
    {
        bool isAccepted;
        bool.TryParse(mainRow["isDeclarationAccepted"].ToString(), out isAccepted);
        chkDeclaration.Checked = isAccepted;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IspageReferesh)
            {
                int count = 0;
                int count1 = 0;

                for (int i = 0; i < chkproblems.Items.Count; i++)
                {
                    ListItem item = new ListItem();
                    item = chkproblems.Items[i];

                    if (item.Selected)
                    {
                        count = count + 1;
                        if (item != null && item.Selected && item.Text == "Other" && txtspcfy.Text == "")
                        {
                            pnl.Visible = true;
                            ClientMessaging("You selected other option, Please specify other problem that you are currently facing.");
                            txtspcfy.Focus();
                            return;
                        }
                        else if (item != null && !item.Selected && item.Text != "Other")
                        {
                            pnl.Visible = false;
                            txtspcfy.Text = "";
                            txtspcfy.Focus();
                        }
                        else if (item != null && !item.Selected && item.Text != "")
                        {
                            pnl.Visible = false;
                            txtspcfy.Text = "";
                            txtspcfy.Focus();
                        }
                    }
                }
                if (count == 0)
                {
                    //lblmsg1.Text = "Select atleast one problem.!!!";
                    ClientMessaging("Please choose at least one problem that you are currently facing with.");
                    return;
                }

                if (txtbckground.Text == "")
                {
                    //Label1.Text = "Professional Background is Requred.!!!";
                    ClientMessaging("Professional Background is Required.!!!");
                    txtbckground.Focus();
                    return;
                }
				
                for (int i = 0; i < connectList.Items.Count; i++)
                {
                    ListItem item = new ListItem();
                    item = connectList.Items[i];

                    if (item.Selected)
                    {
                        count1 = count1 + 1;

                        if (item != null && item.Selected && item.Text == "Other" && txtOthersWayComm.Text == "")
                        {
                            pnlComm.Visible = true;
                            ClientMessaging("You selected other option, Please specify other preferred way to connect.");
                            txtOthersWayComm.Focus();
                            return;
                        }
                        else if (item != null && !item.Selected && item.Text != "Other")
                        {
                            pnlComm.Visible = false;
                            txtOthersWayComm.Text = "";
                        }
                        else if (item != null && !item.Selected && item.Text != "")
                        {
                            pnlComm.Visible = false;
                            txtOthersWayComm.Text = "";
                        }
                    }
                }
                if (count1 == 0)
                {
                    //lblmsg3.Text = "Select atleast one way of connect.!!!";
                    ClientMessaging("Please choose at least one preferred way to connect.");
                    return;
                }

                if (!chkDeclaration.Checked)
                {
                    ClientMessaging("Please accept the declaration before submitting.");
                    return;
                }

                if (btnSave.CommandName == "SUBMIT")
                {
                    Save();
                    clearall();
                    //lblmsg.Text = "Request Submitted Successfully.!";
                    ClientMessaging("Request Submitted Succesfully.!!!");
                    Response.Redirect("../Alumni/Alm_BecomeMentee.aspx");
                }
                else
                {
                    try
                    {
                        //To update Records
                        string message = "";
                        DataSet DsMain = new DataSet();
                        DsMain = GetMain();
                        dsxml = DsMain.GetXml();
                        pk_MenteeID = ViewState["MenteeID"] == null ? 0 : Convert.ToInt32(ViewState["MenteeID"].ToString()); //Convert.ToInt32(Session["MenteeId"]);
                        if (UpdateRecord(ref message) > 0)
                        {
                            lblmsg.Text = "Record Update Successfully.!!!";
                            ClientMessaging("Record Update Successfully.!!!");
                        }
                        else
                        {
                            ClientMessaging("Something went wrong.!");
                        }
						ViewState["MenteeID"] = null;
                    }
                    catch (Exception ex)
                    {
                        lblmsg.Text = ex.Message;
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            lblmsg.Text = Ex.Message;
        }
    }

    protected void Save()
    {
        try
        {
            string message = "";
            DataSet DsMain = new DataSet();
            DsMain = GetMain();
            dsxml = DsMain.GetXml();

            if (InsertRecord(ref message) > 0)
            {

            }
            else
            {
                ClientMessaging("Something went wrong.!");
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    protected DataSet GetMain()
    {
        try
        {
            string value = string.Empty;
            DataSet ds = DAobj.GetSchema("Alm_MenteeReq_Mst");
            DataRow dr = ds.Tables[0].NewRow();
            dr["Pk_Mentee_Reqid"] = "0";
            dr["Professional_Background"] = txtbckground.Text.ToString();
            dr["domains"] = txtdomains.Text.ToString();

            //for (int i = 0; i < connectList.Items.Count; i++)
            //{
            //   ListItem item = new ListItem();
            //   item = connectList.Items[i];
            //   if (item.Selected)
            //    {
            //        value += connectList.Items[i].Value + ',';
            //    }
            //    int LenOfValu = value.Length;
            //    string finalValue = value.Remove(LenOfValu - 1);
            //}

            // Facing problems (checkbox list)
            string otherProblemText = "";
            for (int j = 0; j < chkproblems.Items.Count; j++)
            {
                ListItem item = chkproblems.Items[j];
                if (item.Selected)
                {
                    if (item.Text == "Other")
                    {
                        pnl.Visible = true;
                        otherProblemText = txtspcfy.Text;
                    }
                }
            }
            dr["Others"] = otherProblemText;

            // Communication type (checkbox list)
            string otherCommText = "";
            for (int k = 0; k < connectList.Items.Count; k++)
            {
                ListItem item = connectList.Items[k];
                if (item.Selected)
                {
                    if (item.Text == "Other")
                    {
                        pnlComm.Visible = true;
                        otherCommText = txtOthersWayComm.Text;
                    }
                }
            }
            dr["othersWayComm"] = otherCommText;

            dr["fk_AlumniId"] = Session["AlumniID"];
            dr["isDeclarationAccepted"] = chkDeclaration.Checked;


            ds.Tables[0].Rows.Add(dr);
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("fk_ProblemId", typeof(string)));
            for (int i = 0; i < chkproblems.Items.Count; i++)
            {
                ListItem item = new ListItem();
                item = chkproblems.Items[i];
                if (item.Selected)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["fk_ProblemId"] = item.Value;
                    dt.Rows.Add(dr1);
                }
            }
            DataSet ds1 = DAobj.GetSchema("alm_problem_facing_tran");
            DataTable dt1 = new DataTable();
            dt1.Columns.Add(new DataColumn("fk_communicationTypeID", typeof(string)));

            for (int i = 0; i < connectList.Items.Count; i++)
            {
                ListItem item = new ListItem();
                item = connectList.Items[i];
                if (item.Selected)
                {
                    DataRow dr1 = dt1.NewRow();
                    dr1["fk_communicationTypeID"] = item.Value;
                    dt1.Rows.Add(dr1);
                }
            }
            DataSet ds2 = DAobj.GetSchema("Alm_communicate_tran");
            //Merge Dataset           
            ds2.Merge(dt);
            ds1.Merge(ds2);
            ds1.Merge(dt1);
            ds.Merge(ds1);
            return ds;
        }
        catch (Exception exp)
        {
            lblmsg.Text = exp.Message;
            return null;
        }
    }

    public void clearall()
    {
        chkproblems.ClearSelection();
        connectList.ClearSelection();
        txtdomains.Text = "";
        txtbckground.Text = "";
        txtspcfy.Text = "";
        lblmsg3.Text = "";
        lblmsg1.Text = "";
        pnl.Visible = false;
        btnSave.Visible = false;
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void chkproblem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string selectedOtherID = "";

            for (int i = 0; i < chkproblems.Items.Count; i++)
            {
                ListItem item = new ListItem();
                item = chkproblems.Items[i];

                if (item != null && item.Selected && item.Text == "Other")
                {
                    selectedOtherID = item.Value;
                    pnl.Visible = true;
                }
                else if (item != null && !item.Selected && item.Text != "Other")
                {
                    //txtspcfy.Text = "";
                    //pnl.Visible = false;
                }
                else if (item != null && !item.Selected && item.Text != "")
                {
                    //txtspcfy.Text = "";
                    //pnl.Visible = false;
                }
            }
        }
        catch (Exception Ex)
        {
            lblmsg.Text = Ex.Message;
        }
    }

    // protected void connectList_SelectedIndexChanged(object sender, EventArgs e)
    // {
        // try
        // {
            // string selectedOtherID = "";

            // for (int i = 0; i < connectList.Items.Count; i++)
            // {
                // ListItem item = new ListItem();
                // item = connectList.Items[i];

                // if (item != null && item.Selected && item.Text == "Other")
                // {
                    // selectedOtherID = item.Value;
                    // pnlComm.Visible = true;
                // }
                // else if (item != null && !item.Selected && item.Text != "Other")
                // {
                    // //txtOthersWayComm.Text = "";
                    // //pnlComm.Visible = false;
                // }
                // else if (item != null && !item.Selected && item.Text != "")
                // {
                    // //txtOthersWayComm.Text = "";
                    // //pnlComm.Visible = false;
                // }
            // }
        // }
        // catch (Exception Ex)
        // {
            // lblmsg.Text = Ex.Message;
        // }
    // }
	
	protected void connectList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string anyOptionText = "Any of the above";
            string otherOptionText = "Other";

            int anyIndex = -1;
            int otherIndex = -1;

            // Identify indexes of "Any of the above" and "Other"
            for (int i = 0; i < connectList.Items.Count; i++)
            {
                string itemText = connectList.Items[i].Text.Trim();

                if (itemText == anyOptionText)
                    anyIndex = i;
                else if (itemText == otherOptionText)
                    otherIndex = i;
            }

            if (anyIndex == -1)
                return;

            bool isAnySelected = connectList.Items[anyIndex].Selected;

            if (isAnySelected)
            {
                // "Any of the above" is selected → select all individual items
                for (int i = 0; i < anyIndex; i++)
                {
                    if (connectList.Items[i].Text.Trim() != otherOptionText)
                    {
                        connectList.Items[i].Selected = true;
                    }
                }
            }

            // Show/hide panel for "Other"
            pnlComm.Visible = (otherIndex != -1 && connectList.Items[otherIndex].Selected);
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Alumni/ALM_Alumni_Home.aspx");
    }
}