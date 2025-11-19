using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using SubSonic;

public partial class Alumni_ALM_EmailSignature_Config_Mst : System.Web.UI.Page
{
    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList();
    ArrayList types = new ArrayList();
    ArrayList values = new ArrayList();

    #region Properties

    public string xmlDoc { get; set; }

    #endregion

    public void ClearArrayLists()
    {
        names.Clear(); types.Clear(); values.Clear();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet dsSet = getEmailSigns().GetDataSet();

            if (dsSet.Tables[0].Rows.Count > 0)
            {
                ViewState["id"] = dsSet.Tables[0].Rows[0]["pk_AEmailSignId"];
            }

            if (dsSet.Tables[0].Rows.Count == 1)
            {
                FillData();
            }
            else if (dsSet.Tables[0].Rows.Count == 0)
            {
                Clear();
            }
        }
        //Clear();
    }

    public bool Validation()
    {
        if (txtSenderEmail.Text.Trim() == "")
        {
            ClientMessaging("Please Enter Sender Email.");
            txtSenderEmail.Focus();
            return false;
        }

        if (txtReceiveReplyEmail.Text.Trim() == "")
        {
            ClientMessaging("Please Enter Receive Replies On.");
            txtReceiveReplyEmail.Focus();
            return false;
        }

        if (txtSigns.Text.Trim() == "")
        {
            ClientMessaging("Please Enter Email Signature.");
            txtSigns.Focus();
            return false;
        }
        return true;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Clear();
    }

    protected void Clear()
    {
        try
        {
            //txtSenderEmail.Text = ""; txtSenderEmail.Enabled = true; txtSenderEmail.Visible = true;
            txtSenderEmail.Attributes["oncontextmenu"] = "return true;";
            //txtReceiveReplyEmail.Text = ""; txtReceiveReplyEmail.Enabled = true; txtReceiveReplyEmail.Visible = true;
            txtReceiveReplyEmail.Attributes["oncontextmenu"] = "return true;";
            txtSigns.Text = "";
            //btnSaveSign.Text = "SAVE";
            //btnSaveSign.CommandName = "SAVE";
            //FillData();
            ViewState["id"] = null;

            string script = string.Format("clearCKEditor();", "");
            Anthem.Manager.IncludePageScripts = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errmsg", script, true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void Save()
    {
        try
        {
            string Message = "";
            DataSet dsMain = GetMain();
            xmlDoc = dsMain.GetXml();
            if (InsertRecord(ref Message) > 0)
            {
                ClientMessaging("Record Save Successfully.!!!");
                btnSaveSign.Text = "UPDATE";
                btnSaveSign.CommandName = "UPDATE";
                //Clear();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = ex.Message;
            throw;
        }
    }

    public DataSet GetMain()
    {
        DataSet dsMain = DAobj.GetSchema("ALM_Alumni_EmailSigns_Mst");
        string htmlContent = txtSigns.Text;
        //string plainText = StripHtmlTags(htmlContent);
        DataRow dr = dsMain.Tables[0].NewRow();
        dr["SenderEmail"] = txtSenderEmail.Text.Trim().ToString();
        dr["ReceiveReplyEmail"] = txtReceiveReplyEmail.Text.Trim().ToString();
        dr["EmailSigns"] = htmlContent;
        dr["createdBy"] = Session["UserID"].ToString();

        dsMain.Tables[0].Rows.Add(dr);
        return dsMain;
    }

    public int InsertRecord(ref string Message)
    {
        try
        {
            ClearArrayLists();
            names.Add("@xmlDoc"); values.Add(xmlDoc); types.Add(SqlDbType.VarChar);
            if (DAobj.ExecuteTransactionMsg("ALM_Alumni_EmailSigns_Mst_Insert", values, names, types, ref Message) > 0)
            {
                Message = "Record Save Successfully.!!!";//DAobj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = ex.Message;
            throw;
        }
    }

    public int UpdateRecord(ref string Message)
    {
        try
        {
            ClearArrayLists();
            names.Add("@xmlDoc"); values.Add(xmlDoc); types.Add(SqlDbType.VarChar);
            names.Add("@AEmailSignId"); values.Add(Convert.ToInt32(ViewState["id"].ToString())); types.Add(SqlDbType.VarChar);
            if (DAobj.ExecuteTransactionMsg("ALM_Alumni_EmailSigns_Mst_Update", values, names, types, ref Message) > 0)
            {
                Message = "Record Updated Successfully.!!!";//DAobj.ShowMessage("U", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = ex.Message;
            throw;
        }
    }

    //Convert CK Editor text to PlainText(Remove Tag)

    private string StripHtmlTags(string html)
    {
        if (string.IsNullOrEmpty(html))
        {
            return "";
        }
        string strippedText = Regex.Replace(html, "<.*?>", "");
        strippedText = DecodeHtmlEntities(strippedText);
        return strippedText;
    }

    private string DecodeHtmlEntities(string text)
    {
        return HttpUtility.HtmlDecode(text);
    }

    protected void Edit(int EmailSignId)
    {
        DataSet ds = ALM_Alumni_EmailSigns_Mst_Sel(EmailSignId).GetDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtSenderEmail.Text = ds.Tables[0].Rows[0]["SenderEmail"].ToString();
            txtSigns.Text = ds.Tables[0].Rows[0]["EmailSigns"].ToString();
            txtReceiveReplyEmail.Text = ds.Tables[0].Rows[0]["ReceiveReplyEmail"].ToString();
        }
    }

    public static StoredProcedure ALM_Alumni_EmailSigns_Mst_Sel(int EmailSignId)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Alumni_EmailSigns_Mst_Edit", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@EmailSignId", EmailSignId, DbType.Int32);
        return sp;
    }

    public static StoredProcedure ALM_Alumni_EmailSigns_Mst_Del(int EmailSignId)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Alumni_EmailSigns_Mst_Delete", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@EmailSignId", EmailSignId, DbType.Int32);
        return sp;
    }

    #region "Bind GridView Method"

    public void FillGrid()
    {

        DataSet ds = getEmailSigns().GetDataSet();
        //gvEmailSigns.DataSource = ds.Tables[0];
        //gvEmailSigns.DataBind();
    }

    #endregion

    #region "Stored Procedures"

    public static StoredProcedure getEmailSigns()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_EmailSigns_Mst_Lists", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    public static StoredProcedure ALM_Alumni_EmailTemplate_Mst_Upd(string xmlDoc, int AETempId)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Alumni_EmailTemplate_Mst_Update", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@xmlDoc", xmlDoc, DbType.String);
        sp.Command.AddParameter("@AETempId", AETempId, DbType.Int32);
        return sp;
    }

    #endregion

    protected void btnSaveSign_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (btnSaveSign.CommandName == "SAVE")
            {
                Save();
            }
            else if (btnSaveSign.CommandName == "UPDATE")
            {
                Update();
            }

            //Save();
            //Clear();
        }
    }

    private void Update()
    {
        try
        {
            string Message = "";
            DataSet dsMain = GetMain();
            xmlDoc = dsMain.GetXml();
            if (UpdateRecord(ref Message) > 0)
            {
                ClientMessaging("Record Updated Successfully!!!.");
                //Clear();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void SetCKEditorContent()
    {
        string desc = txtSigns.Text;
        string script = "CKEDITOR.instances['" + txtSigns.ClientID + "'].setData('" + desc.Replace("'", "\\'") + "');";
        ClientScript.RegisterStartupScript(this.GetType(), "setCKEditorContent", script, true);
    }

    protected void gvEmailSigns_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int emailSignId = Convert.ToInt32(e.CommandArgument);

            ViewState["id"] = emailSignId.ToString();

            if (e.CommandName.ToUpper().ToString().Trim().ToUpper() == "EDITREC")
            {
                DataSet dsS = ALM_Alumni_EmailSigns_Mst_Sel(emailSignId).GetDataSet();

                if (dsS.Tables[0].Rows.Count > 0)
                {
                    txtSenderEmail.Text = dsS.Tables[0].Rows[0]["SenderEmail"].ToString();
                    txtReceiveReplyEmail.Text = dsS.Tables[0].Rows[0]["ReceiveReplyEmail"].ToString();
                    txtSigns.Text = dsS.Tables[0].Rows[0]["EmailSigns"].ToString();
                    btnSaveSign.Text = "UPDATE";
                    btnSaveSign.CommandName = "UPDATE";
                    SetCKEditorContent();
                    pnl.Visible = true;
                }
                else
                {
                    string script = string.Format("clearCKEditor();", "");
                    Anthem.Manager.IncludePageScripts = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "errmsg", script, true);
                }
            }
            else if (e.CommandName.ToUpper().ToString() == "DELETEREC")
            {
                if ((ALM_Alumni_EmailSigns_Mst_Del(emailSignId)).Execute() > 0)
                {

                }
                ClientMessaging("Record Deleted Successfully");
                //Clear();
            }
            else
            {
                string script = string.Format("clearCKEditor();", "");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errmsg", script, true);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void gvEmailSigns_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    void FillData()
    {
        try
        {
            DataSet dsSS = getEmailSigns().GetDataSet();

            if (dsSS != null && dsSS.Tables.Count > 0 && dsSS.Tables[0].Rows.Count > 0)
            {
                txtSenderEmail.Text = ""; txtSenderEmail.Enabled = false; txtSenderEmail.Visible = true;
                txtSenderEmail.Attributes["oncontextmenu"] = "return false;";
                txtReceiveReplyEmail.Text = ""; txtReceiveReplyEmail.Enabled = false; txtReceiveReplyEmail.Visible = true;
                txtReceiveReplyEmail.Attributes["oncontextmenu"] = "return false;";

                txtSenderEmail.Text = dsSS.Tables[0].Rows[0]["SenderEmail"].ToString();
                txtReceiveReplyEmail.Text = dsSS.Tables[0].Rows[0]["ReceiveReplyEmail"].ToString();
                txtSigns.Text = dsSS.Tables[0].Rows[0]["EmailSigns"].ToString();
                btnSaveSign.Text = "UPDATE";
                btnSaveSign.CommandName = "UPDATE";
                SetCKEditorContent();
                pnl.Visible = true;
            }
            else
            {
                // Handle case where no data or no rows returned
                lblMsg.Text = "No Record Found.";
            }
        }
        catch (Exception EX)
        {
            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(EX.Message);
        }
    }

    //protected void FillData(string sId)
    //{
    //    DataSet dsS = ALM_Alumni_EmailSigns_Mst_Sel(Convert.ToInt32(sId)).GetDataSet();

    //    if (dsS.Tables[0].Rows.Count > 0)
    //    {
    //        txtSenderEmail.Text = dsS.Tables[0].Rows[0]["SenderEmail"].ToString();
    //        txtReceiveReplyEmail.Text = dsS.Tables[0].Rows[0]["ReceiveReplyEmail"].ToString();
    //        txtSigns.Text = dsS.Tables[0].Rows[0]["EmailSigns"].ToString();
    //        btnSaveSign.Text = "UPDATE";
    //        btnSaveSign.CommandName = "UPDATE";
    //        SetCKEditorContent();
    //        pnl.Visible = true;
    //    }
    //}
}