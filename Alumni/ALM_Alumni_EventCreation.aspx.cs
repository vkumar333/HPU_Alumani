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
using System.IO;
using System.Globalization;

public partial class Alumni_ALM_Alumni_EventCreation : System.Web.UI.Page
{
    public string xmldoc { get; private set; }
    public int mode { get; private set; }
    public int PK_Events_id { get; set; }

    DataAccess Dobj = new DataAccess();

    #region "Global Declaration"

    private Boolean IsPageRefresh = false;
    DataAccess objDAO = new DataAccess();
    CustomMessaging eobj = new CustomMessaging();
    CommonFunction cfObj = new CommonFunction();
    bool active = false;

    #endregion

    #region "Page Events"

    protected void Page_PreInit()
    {
        //Page.Theme = "CCSBLUE";
    }

    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

    public int ins_upd_delevents_dtls(ref string Message)
    {
        try
        {
            names.Clear(); values.Clear(); types.Clear();
            names.Add("@doc"); types.Add(SqlDbType.VarChar); values.Add(xmldoc);
            names.Add("@mode"); types.Add(SqlDbType.Int); values.Add(mode);
            names.Add("@PK_Events_id"); types.Add(SqlDbType.Int); values.Add(PK_Events_id);
            if (Dobj.ExecuteTransactionMsg("ALM_Evnts_Dtls_Crud", values, names, types, ref Message) > 0)
            {
                Message = Dobj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch { throw; }
    }

    public static StoredProcedure getUploaded_compp__details()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Evnts_Dtls", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Form.DefaultButton = btnSave.UniqueID;
            //Fill_grid();
            Clear();
            //V_txtStartDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            //V_txtEndDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    #endregion

    #region "Bind GridView Methoid"

    public void Fill_grid()
    {

        DataSet ds = getUploaded_compp__details().GetDataSet();
        gvDetails.DataSource = ds.Tables[0];
        gvDetails.DataBind();
    }

    #endregion

    #region "Common Methods"

    private void Clear()
    {
        R_txtEventTitle.Text = "";
        R_txtEventDescription.Text = "";

        //V_txtStartDate.Text = "";
        //V_txtEndDate.Text = "";

        DateTime now = DateTime.Now;
        V_txtStartDate.Text = "01" + "/" + now.ToString("MM", CultureInfo.InvariantCulture) + "/" + now.ToString("yyyy", CultureInfo.InvariantCulture);
        V_txtEndDate.Text = now.ToString("dd", CultureInfo.InvariantCulture) + "/" + now.ToString("MM", CultureInfo.InvariantCulture) + "/" + now.ToString("yyyy", CultureInfo.InvariantCulture);

        TextAddress.Text = "";
        Chk_IsActive.Checked = Convert.ToBoolean(0);

        btnSave.CommandName = "SAVE";
        btnSave.Text = "SAVE";

        Anthem.Manager.IncludePageScripts = true;
        R_txtEventTitle.Focus();
        lnkviewBrc.Text = "";

        if (!flUploadLogo.HasFile)
        {
            flUploadLogo.Dispose();
        }

        Fill_grid();
        gvDetails.PageIndex = 0;
        gvDetails.SelectedIndex = -1;
    }

    protected void Client_Messaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
        //Anthem.Manager.AddScriptForClientSideEval("alert('" + msg + "');");
    }

    #endregion

    #region "Button Reset and Save Events"

    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Clear();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsPageRefresh)
            {
                if (btnSave.CommandName.ToString().ToUpper() == "SAVE")
                {
                    Save();
                    Fill_grid();
                }
                else
                {
                    Update();
                    Fill_grid();
                }
            }
        }
        catch (SqlException exp) // For SQLException (voilation on delete or unique key voilation)
        {
            lblMsg.Text = eobj.ShowSQLErrorMsg(exp.Message.ToString(), "", exp);
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    #endregion

    public bool Validation()
    {
        string FileTypes = "";
        string FileType = "";
        double filesize = 0;
        double filesizes = 0;
        string Name = "";
        string Names = "";
        DataRow drimg = null;
        string Fileuniquename = string.Empty;

        if (R_txtEventTitle.Text == "" || R_txtEventTitle.Text == null)
        {
            ClientMessaging("Event Name is Required.!");
            R_txtEventTitle.Focus();
            return false;
        }
        else if (R_txtEventTitle.Text.Length > 250)
        {
            ClientMessaging("Event Name allowed limit is 250 characters only!");
            return false;
        }
        else if (R_txtEventDescription.Text == "" || R_txtEventDescription.Text == null)
        {
            ClientMessaging("Event Description is Required.!");
            R_txtEventDescription.Focus();
            return false;
        }
        else if (R_txtEventDescription.Text.Trim().Length > 500)
        {
            ClientMessaging("Event Description allowed limit is 500 characters only!");
            return false;
        }
        else if (V_txtStartDate.Text == "" || V_txtStartDate.Text == null)
        {
            ClientMessaging("Start Date is Required.!");
            V_txtStartDate.Focus();
            return false;
        }
        else if (V_txtEndDate.Text == "" || V_txtEndDate.Text == null)
        {
            ClientMessaging("End Date is Required.!");
            V_txtEndDate.Focus();
            return false;
        }

        string startDate = CommonCode.DateFormats.Date_FrontToDB_R(V_txtStartDate.Text.Trim());
        string EndDate = CommonCode.DateFormats.Date_FrontToDB_R(V_txtEndDate.Text.Trim());

        if (Convert.ToDateTime(startDate) > Convert.ToDateTime(EndDate))
        {
            Client_Messaging("Event Start Date Can not be greater than End Date.");
            // Anthem.Manager.IncludePageScripts = true;
            V_txtStartDate.Focus();
            return false;
        }
        else if (TextAddress.Text == "" || TextAddress.Text == null)
        {
            ClientMessaging("Address is Required.!");
            TextAddress.Focus();
            return false;
        }
        else if (TextAddress.Text.Length > 250)
        {
            ClientMessaging("Address/Location allowed limit is 250 characters only!");
            return false;
        }
        //else if (flUploadLogo.FileName == null || flUploadLogo.FileName == "")
        //{
        //    ClientMessaging("Please Upload File...!");
        //    flUploadLogo.Focus();
        //    return false;
        //}
        if (flUploadLogo.HasFile && (System.IO.Path.GetExtension(flUploadLogo.FileName) != ".jpg")
            && System.IO.Path.GetExtension(flUploadLogo.FileName) != ".jpeg" && System.IO.Path.GetExtension(flUploadLogo.FileName) != ".png")
        {
            ClientMessaging("Only JPG, JPEG and PNG files can be uploaded...!");
            flUploadLogo.Focus();
            return false;
        }

        filesize = flUploadLogo.PostedFile.ContentLength;
        Name = flUploadLogo.PostedFile.FileName;
        //filesize = Math.Round((filesize / 1024), 0);
        if (Name.Length > 100)
        {
            ClientMessaging("File Name Should not be more than 100 characters!");
            flUploadLogo.Focus();
            return false;
        }
        if (filesize > (2 * 1024 * 1024))
        {
            ClientMessaging("File should not be more than 2 MB !");
            flUploadLogo.Focus();
            return false;
        }

        return true;
    }

    #region "Save, Edit, Update, Delete Methods"

    public void Save()
    {
        if (Validation())
        {
            try
            {
                string Message = "";
                DataSet ds_details = new DataSet();
                ds_details = Getmain();
                string doc = "";
                doc = ds_details.GetXml();
                xmldoc = doc;
                mode = 1;

                if (ins_upd_delevents_dtls(ref Message) > 0)
                {
                    ClientMessaging("Record Save Successfully.");

                    //getUploaded_details();
                    // R_txtStuyDesc.Text = "";
                    // ctctxt.Text = string.Empty;
                    //D_ddlJobList.SelectedIndex = 0;
                    Clear();
                }
                else
                {
                    ClientMessaging("Record not saved......!");
                }
            }
            catch (Exception Ex)
            {
                lblMsg.Text = CommonCode.ExceptionHandling.SqlExceptionHandling(Ex.Message);
            }
        }
    }

    public DataSet Getmain()
    {
        DataSet ds_master = Dobj.GetSchema("Alumni_Events_Mst");
        DataRow dr = ds_master.Tables[0].NewRow();
        var fddsf = R_txtEventTitle.Text.Length;

        dr["Event_Name"] = R_txtEventTitle.Text.ToString();
        dr["Description"] = R_txtEventDescription.Text.ToString();
        dr["Address"] = TextAddress.Text.ToString();

        ds_master.Tables[0].Rows.Add(dr);
        /////////////// Upload Document////////////////////
        if (flUploadLogo.HasFile)
        {
            if (flUploadLogo.PostedFile.FileName != "")
            {
                string FileType = Path.GetExtension(flUploadLogo.PostedFile.FileName);
                if (FileType != null)
                {
                    string lFileName = "";

                    Random r = new Random();
                    int n = r.Next();

                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + flUploadLogo.FileName.Substring(flUploadLogo.FileName.LastIndexOf("."));
                    lFileName = FU_physicalPath(flUploadLogo, "Alumni", "BRO" + n.ToString() + filename);
                    // dr["Student_img"] = flUploadLogo.FileName + "/" + lFileName;
                    dr["File_name"] = lFileName;
                    dr["File_path"] = Session["filepath"];
                }
            }
        }

        dr["Start_date"] = CommonCode.DateFormats.Date_FrontToDB_R(V_txtStartDate.Text.Trim());
        dr["End_date"] = CommonCode.DateFormats.Date_FrontToDB_R(V_txtEndDate.Text.Trim());
        dr["CreatedBy"] = Session["UserID"].ToString();
        dr["IsActive"] = Chk_IsActive.Checked;
        dr["Fk_AlumniId"] = 0;
        return ds_master;
    }
    public DataSet Getmain_UPD()
    {
        DataSet ds_master = Dobj.GetSchema("Alumni_Events_Mst");
        DataRow dr = ds_master.Tables[0].NewRow();
        dr["Event_Name"] = R_txtEventTitle.Text.ToString();
        dr["Description"] = R_txtEventDescription.Text.ToString();
        dr["Address"] = TextAddress.Text.ToString();

        ds_master.Tables[0].Rows.Add(dr);
        /////////////// Upload Document////////////////////
        if (flUploadLogo.HasFile)
        {
            if (flUploadLogo.PostedFile.FileName != "")
            {
                string FileType = Path.GetExtension(flUploadLogo.PostedFile.FileName);
                if (FileType != null)
                {
                    string lFileName = "";

                    Random r = new Random();
                    int n = r.Next();

                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + flUploadLogo.FileName.Substring(flUploadLogo.FileName.LastIndexOf("."));
                    lFileName = FU_physicalPath(flUploadLogo, "Alumni/", "BRO" + n.ToString() + filename);
                    // dr["Student_img"] = flUploadLogo.FileName + "/" + lFileName;
                    dr["File_name"] = lFileName;
                    dr["File_path"] = Session["filepath"];
                }
            }
        }
        else
        {
            dr["File_name"] = (string)ViewState["File1"].ToString();
        }
        dr["Start_date"] = CommonCode.DateFormats.Date_FrontToDB_R(V_txtStartDate.Text.Trim());
        dr["End_date"] = CommonCode.DateFormats.Date_FrontToDB_R(V_txtEndDate.Text.Trim());
        dr["CreatedBy"] = Session["UserID"].ToString();
        dr["IsActive"] = Chk_IsActive.Checked;
        dr["Fk_AlumniId"] = 0;
        return ds_master;
    }
    public string FU_physicalPath(FileUpload flu, string FolderName, string FileName)
    {
        try
        {
            if (flu.PostedFile != null && flu.HasFile && flu.PostedFile.ContentLength > 0)
            {
                string host = HttpContext.Current.Request.Url.Host;
                string upldPath = "";
                DataSet dsFilepath = new DataSet();
                dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
                foreach (DataRow dr in dsFilepath.Tables[0].Rows)
                {
                    if (host == dr["Server_Ip"].ToString().Trim())
                    {
                        upldPath = dr["Physical_Path"].ToString().Trim();
                        upldPath = upldPath + "\\Alumni\\" + "";
                        Session["filepath"] = upldPath;
                        upldPath = upldPath + FileName;
                        flu.SaveAs(upldPath);
                        return FileName;
                    }
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            throw new Exception("Upload Fails!");
        }
    }

    private void Update()
    {
        if (!Validation())
        {
            return;
        }
        else
        {
            //string FileTypes = "";
            //string FileType = "";
            //double filesize = 0;
            //double filesizes = 0;
            //string Name = "";
            //string Names = "";
            //DataRow drimg = null;
            //string Fileuniquename = string.Empty;
            //try
            //{
            //    if (R_txtEventTitle.Text == "" || R_txtEventTitle.Text == null)
            //    {
            //        ClientMessaging("Event Name is Required");
            //        R_txtEventTitle.Focus();
            //        return;
            //    }
            //    else if (R_txtEventDescription.Text == "" || R_txtEventDescription.Text == null)
            //    {
            //        ClientMessaging("Description is Required");
            //        R_txtEventDescription.Focus();
            //        return;
            //    }
            //    else if (V_txtStartDate.Text == "" || V_txtStartDate.Text == null)
            //    {
            //        ClientMessaging("Start Date id Required");
            //        V_txtStartDate.Focus();
            //        return;
            //    }
            //    else if (V_txtEndDate.Text == "" || V_txtEndDate.Text == null)
            //    {
            //        ClientMessaging("End Date id Required");
            //        V_txtEndDate.Focus();
            //        return;
            //    }

            //    string startDate = CommonCode.DateFormats.Date_FrontToDB_R(V_txtStartDate.Text.Trim());
            //    string EndDate = CommonCode.DateFormats.Date_FrontToDB_R(V_txtEndDate.Text.Trim());

            //    if (Convert.ToDateTime(startDate) > Convert.ToDateTime(EndDate))
            //    {
            //        Client_Messaging("Event Start Date Can not be greated than End Date.");
            //        // Anthem.Manager.IncludePageScripts = true;
            //        V_txtStartDate.Focus();
            //        return;
            //    }
            //    else if (TextAddress.Text == "" || TextAddress.Text == null)
            //    {
            //        ClientMessaging("Address is Required");
            //        TextAddress.Focus();
            //        return;
            //    }
            //    if (flUploadLogo.HasFile && (System.IO.Path.GetExtension(flUploadLogo.FileName) != ".jpg")
            //        && System.IO.Path.GetExtension(flUploadLogo.FileName) != ".jpeg" && System.IO.Path.GetExtension(flUploadLogo.FileName) != ".png")
            //    {
            //        ClientMessaging("Only JPG, JPEG and PNG files can be uploaded...!");
            //        flUploadLogo.Focus();
            //        return;
            //    }
            //    filesize = flUploadLogo.PostedFile.ContentLength;
            //    Name = flUploadLogo.PostedFile.FileName;
            //    //filesize = Math.Round((filesize / 1024), 0);
            //    if (Name.Length > 100)
            //    {
            //        ClientMessaging("Photo File Name Should not be more than 100 characters!");
            //        flUploadLogo.Focus();
            //        return;
            //    }
            //    if (filesize > (2 * 1024 * 1024))
            //    {
            //        ClientMessaging("Photo size should not be more than 2 MB !");
            //        flUploadLogo.Focus();
            //        return;
            //    }

            try
            {
                string Message = "";
                DataSet ds_details = new DataSet();
                ds_details = Getmain_UPD();
                string doc = "";
                doc = ds_details.GetXml();
                xmldoc = doc;
                mode = 2;
                PK_Events_id = Convert.ToInt32(ViewState["id"]);
                if (ins_upd_delevents_dtls(ref Message) >= 0)
                {
                    ClientMessaging("Record Update Successfully.");
                    Clear();
                }
                else
                {
                    ClientMessaging("Record not Update.");
                }
            }
            catch (SqlException ex)
            {
                lblMsg.Text = objDAO.ShowSQLErrorMsg(ex.Message.ToString().Trim(), "", ex);
            }
        }
    }

    public static StoredProcedure Alm_Events_GetById(int PK_Events_id)
    {
        SubSonic.StoredProcedure sp = new StoredProcedure("Alm_Events_GetById", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Pk_Eventid", PK_Events_id, DbType.Int32);
        return sp;
    }

    private void Edit()
    {
        DataSet ds = Alm_Events_GetById(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

        GridViewRow gvr = (GridViewRow)((LinkButton)ViewState["id"]).NamingContainer;
        int RowIndexS = gvr.RowIndex;

        if (ds.Tables[0].Rows.Count > 0)
        {
            R_txtEventTitle.Text = ds.Tables[0].Rows[0]["Event_name"].ToString();
            R_txtEventDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            V_txtStartDate.Text = ds.Tables[0].Rows[0]["Start_date"].ToString();
            V_txtEndDate.Text = ds.Tables[0].Rows[0]["End_date"].ToString();
            TextAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            ViewState["File1"] = ds.Tables[0].Rows[0]["File_name"].ToString();
            if (ds.Tables[0].Rows[0]["File_name"] != null && ds.Tables[0].Rows[0]["File_name"].ToString() != "")
            {
                lnkviewBrc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
                lnkviewBrc.CommandName = ds.Tables[0].Rows[0]["File_name"].ToString();
                lnkviewBrc.Visible = true;
                getFileName();
            }

            foreach (GridViewRow row in gvDetails.Rows)
            {
                row.BackColor = System.Drawing.Color.Transparent;
            }
            gvDetails.Rows[RowIndexS].BackColor = System.Drawing.Color.LightCyan;
            gvDetails.Rows[RowIndexS].Style.Add("Font-Weight", "Bold");
        }
    }

    protected void getFileName()
    {
        string FileName = lnkviewBrc.CommandName;
        string FileUrl = ReturnPath();
        string FileDisplayName = "";
        string FileRealName = "";
        //if (FileName.Contains("/"))
        //{
        FileDisplayName = FileName;

        FileRealName = ReturnPath() + "/Alumni/" + FileName.Substring(FileName.IndexOf("/") + 1);
        //   }
        FileUrl = FileUrl + FileName;
        lnkviewBrc.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
    }

    public string ReturnPath()
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
                    return dr["http_Add"].ToString().Trim();
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static StoredProcedure ALM_Events_Delete(int PK_Events_id)
    {
        SubSonic.StoredProcedure sp = new StoredProcedure("ALM_Events_Delete", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Pk_EventsId", PK_Events_id, DbType.Int32);
        return sp;
    }

    private void Delete()
    {
        try
        {
            if ((ALM_Events_Delete(Convert.ToInt32(ViewState["id"].ToString())).Execute()) > 0)
            {
                //lblMsg.Text = eobj.ShowMessage("D");
                Client_Messaging("Record Deleted Successfully!");
            }
        }
        catch (SqlException ex)
        {
            lblMsg.Text = objDAO.ShowSQLErrorMsg(ex.Message.ToString().Trim(), "", ex);
        }
    }

    #endregion

    #region "GridView Events"

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            btnSave.Text = "Update";
            // btnSave.Text = "Update";
            lblMsg.Text = "";
            ViewState["id"] = e.CommandArgument.ToString().Trim();
            if (e.CommandName.ToUpper().ToString() == "EDITREC")
            {
                //Edit();
                DataSet ds = Alm_Events_GetById(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int RowIndexS = gvr.RowIndex;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    R_txtEventTitle.Text = ds.Tables[0].Rows[0]["Event_name"].ToString();
                    R_txtEventDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                    V_txtStartDate.Text = ds.Tables[0].Rows[0]["Start_date"].ToString();
                    V_txtEndDate.Text = ds.Tables[0].Rows[0]["End_date"].ToString();
                    TextAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "True")
                    {
                        Chk_IsActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"].ToString());
                    }
                    else
                    {
                        Chk_IsActive.Checked = Convert.ToBoolean(0);
                    }

                    ViewState["File1"] = ds.Tables[0].Rows[0]["File_name"].ToString();
                    if (ds.Tables[0].Rows[0]["File_name"] != null && ds.Tables[0].Rows[0]["File_name"].ToString() != "")
                    {
                        lnkviewBrc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
                        lnkviewBrc.CommandName = ds.Tables[0].Rows[0]["File_name"].ToString();
                        lnkviewBrc.Visible = true;
                        getFileName();
                    }

                    foreach (GridViewRow row in gvDetails.Rows)
                    {
                        row.BackColor = System.Drawing.Color.Transparent;
                    }
                    gvDetails.Rows[RowIndexS].BackColor = System.Drawing.Color.LightCyan;
                    gvDetails.Rows[RowIndexS].Style.Add("Font-Weight", "Bold");
                }
                btnSave.CommandName = "UPDATE";
                btnSave.Text = "UPDATE";
            }
            else if (e.CommandName.ToUpper().ToString() == "DELETEREC")
            {
                if (!IsPageRefresh)
                {
                    Delete();
                    Clear();
                    Fill_grid();
                }
                else
                {
                    Clear();
                    lblMsg.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void gvDetails_PageIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Clear();
        gvDetails.PageIndex = e.NewPageIndex;
        Fill_grid();
    }

    #endregion

    protected void lnkviewBrc_Click(object sender, EventArgs e)
    {
        getFileName();
    }
}