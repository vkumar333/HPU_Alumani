using DataAccessLayer;
using SubSonic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Alumni_ALM_Notable_Alumni_Mst : System.Web.UI.Page
{
    public string xmldoc { get; private set; }
    public int mode { get; private set; }
    public int PK_NAID { get; set; }

    #region "Global Declaration"

    private Boolean IsPageRefresh = false;
    DataAccess DAobj = new DataAccess();
    CustomMessaging eobj = new CustomMessaging();
    CommonFunction cfObj = new CommonFunction();
    bool active = false;

    ArrayList names = new ArrayList();
    ArrayList types = new ArrayList();
    ArrayList values = new ArrayList();

    #endregion

    #region "Page Events"

    protected void Page_PreInit()
    {
        //Page.Theme = "CCSBLUE";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Clear();
        }
    }

    public int Ins_Upd_Del_Notable_Alumni_Dtls(ref string Message)
    {
        try
        {
            names.Clear(); values.Clear(); types.Clear();
            names.Add("@xmlDoc"); types.Add(SqlDbType.VarChar); values.Add(xmldoc);
            names.Add("@mode"); types.Add(SqlDbType.Int); values.Add(mode);
            names.Add("@PK_NAID"); types.Add(SqlDbType.Int); values.Add(PK_NAID);
            if (DAobj.ExecuteTransactionMsg("ALM_Notable_Alumni_Mst_CRUD", values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch
        {
            throw;
        }
    }

    //public static StoredProcedure getUploadedNotableAlumniDetails(int mode)
    //{
    //    SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Notable_Alumni_Mst_CRUD", DataService.GetInstance("IUMSNXG"), "");
    //    sp.Command.AddParameter("@mode", mode, DbType.Int32);
    //    return sp;
    //}

    #endregion

    #region "Bind GridView Method"

    public void LoadFillGrid()
    {
        try
        {
            DataSet dsSS = ALM_Notable_Alumni_Mst_CRUD_Operations(xmldoc, 5, 0).GetDataSet();

            if (dsSS != null && dsSS.Tables.Count > 0 && dsSS.Tables[0].Rows.Count > 0)
            {
                gvNotableAlumni.DataSource = dsSS.Tables[0];
                gvNotableAlumni.DataBind();
            }
            else
            {
                gvNotableAlumni.DataSource = null;
                gvNotableAlumni.DataBind();
                ClientMessaging("No Record Found.");
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion

    #region "Common Methods"

    private void Clear()
    {
        txtName.Text = "";
        txtSubheading.Text = "";
        txtComments.Text = "";
        chkActive.Checked = Convert.ToBoolean(0);
        btnSave.CommandName = "SAVE";
        btnSave.Text = "SAVE";
        Anthem.Manager.IncludePageScripts = true;
        txtName.Focus();
        lnkViewPhoto.Text = "";

        if (!flUploadPhoto.HasFile)
        {
            flUploadPhoto.Dispose();
        }
        LoadFillGrid();
        gvNotableAlumni.PageIndex = 0;
        gvNotableAlumni.SelectedIndex = -1;
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
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
                    LoadFillGrid();
                }
                else
                {
                    Update();
                    LoadFillGrid();
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
        double filesize = 0;
        string Name = "";
        string Fileuniquename = string.Empty;

        if (txtName.Text == "" || txtName.Text == null)
        {
            ClientMessaging("Name is Required.!");
            txtName.Focus();
            return false;
        }
        if (txtSubheading.Text == "" || txtSubheading.Text == null)
        {
            ClientMessaging("Subheading is Required.!");
            txtSubheading.Focus();
            return false;
        }
        else if (txtSubheading.Text.Trim().Length > 200)
        {
            ClientMessaging("Event Description allowed limit is 200 characters only!");
            return false;
        }

        if (txtComments.Text == "" || txtComments.Text == null)
        {
            ClientMessaging("Comments is Required.!");
            txtComments.Focus();
            return false;
        }

        if (flUploadPhoto.HasFile && (System.IO.Path.GetExtension(flUploadPhoto.FileName) != ".jpg")
            && System.IO.Path.GetExtension(flUploadPhoto.FileName) != ".jpeg" && System.IO.Path.GetExtension(flUploadPhoto.FileName) != ".png")
        {
            ClientMessaging("Only JPG, JPEG and PNG files can be uploaded...!");
            flUploadPhoto.Focus();
            return false;
        }

        filesize = flUploadPhoto.PostedFile.ContentLength;
        Name = flUploadPhoto.PostedFile.FileName;

        if (Name.Length > 100)
        {
            ClientMessaging("File Name Should not be more than 100 characters!");
            flUploadPhoto.Focus();
            return false;
        }

        if (filesize > (2 * 1024 * 1024))
        {
            ClientMessaging("File should not be more than 2 MB !");
            flUploadPhoto.Focus();
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
                ds_details = GetMain();
                string doc = "";
                doc = ds_details.GetXml();
                xmldoc = doc;
                mode = 1;

                if (Ins_Upd_Del_Notable_Alumni_Dtls(ref Message) > 0)
                {
                    ClientMessaging("Record Save Successfully!!!.");
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

    public DataSet GetMain()
    {
        DataSet dsMain = DAobj.GetSchema("ALM_Notable_Alumni_Mst");
        DataRow dr = dsMain.Tables[0].NewRow();

        dr["Name"] = txtName.Text.Trim().ToString();
        dr["Description"] = txtSubheading.Text.Trim().ToString();
        dr["Comments"] = txtComments.Text.Trim().ToString();

        dsMain.Tables[0].Rows.Add(dr);

        #region "Upload Document"

        if (flUploadPhoto.HasFile)
        {
            if (flUploadPhoto.PostedFile.FileName != "")
            {
                string FileType = Path.GetExtension(flUploadPhoto.PostedFile.FileName);
                if (FileType != null)
                {
                    string lFileName = "";

                    Random r = new Random();
                    int n = r.Next();

                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + flUploadPhoto.FileName.Substring(flUploadPhoto.FileName.LastIndexOf("."));
                    lFileName = FU_physicalPath(flUploadPhoto, "Alumni", "NAA" + n.ToString() + filename);
                    dr["OFileName"] = flUploadPhoto.PostedFile.FileName.Trim().ToString();
                    dr["OFilePath"] = Session["filepath"];
                    dr["NFileName"] = lFileName;
                    dr["NFilePath"] = Session["filepath"];
                }
            }
        }

        #endregion

        dr["CreatedBy"] = Session["UserID"].ToString();
        dr["IsActive"] = chkActive.Checked;
        dr["Fk_AlumniId"] = 0;
        return dsMain;
    }

    public DataSet GetMainUPD()
    {
        DataSet ds_master = DAobj.GetSchema("ALM_Notable_Alumni_Mst");
        DataRow dr = ds_master.Tables[0].NewRow();
        dr["Name"] = txtName.Text.Trim().ToString();
        dr["Description"] = txtSubheading.Text.Trim().ToString();
        dr["Comments"] = txtComments.Text.Trim().ToString();

        ds_master.Tables[0].Rows.Add(dr);

        #region "Upload Document"

        if (flUploadPhoto.HasFile)
        {
            if (flUploadPhoto.PostedFile.FileName != "")
            {
                string FileType = Path.GetExtension(flUploadPhoto.PostedFile.FileName);
                if (FileType != null)
                {
                    string lFileName = "";

                    Random r = new Random();
                    int n = r.Next();

                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + flUploadPhoto.FileName.Substring(flUploadPhoto.FileName.LastIndexOf("."));
                    lFileName = FU_physicalPath(flUploadPhoto, "Alumni", "NAA" + n.ToString() + filename);
                    dr["OFileName"] = flUploadPhoto.PostedFile.FileName.Trim().ToString();
                    dr["OFilePath"] = Session["filepath"];
                    dr["NFileName"] = lFileName;
                    dr["NFilePath"] = Session["filepath"];
                }
            }
        }
        else
        {
            dr["NFileName"] = (string)ViewState["File1"].ToString();
        }

        #endregion

        dr["CreatedBy"] = Session["UserID"].ToString();
        dr["IsActive"] = chkActive.Checked;
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
            try
            {
                string Message = "";
                DataSet ds_details = new DataSet();
                ds_details = GetMainUPD();
                string doc = "";
                doc = ds_details.GetXml();
                xmldoc = doc;
                mode = 2;
                PK_NAID = Convert.ToInt32(ViewState["id"]);
                if (Ins_Upd_Del_Notable_Alumni_Dtls(ref Message) >= 0)
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
                lblMsg.Text = DAobj.ShowSQLErrorMsg(ex.Message.ToString().Trim(), "", ex);
            }
        }
    }

    private void Edit()
    {
        DataSet ds = ALM_Notable_Alumni_Mst_CRUD_Operations(xmldoc, 3, Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

        GridViewRow gvr = (GridViewRow)((LinkButton)ViewState["id"]).NamingContainer;
        int RowIndexS = gvr.RowIndex;

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            txtSubheading.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            ViewState["File1"] = ds.Tables[0].Rows[0]["NFileName"].ToString();
            if (ds.Tables[0].Rows[0]["NFileName"] != null && ds.Tables[0].Rows[0]["NFileName"].ToString() != "")
            {
                lnkViewPhoto.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
                lnkViewPhoto.CommandName = ds.Tables[0].Rows[0]["NFileName"].ToString();
                lnkViewPhoto.Visible = true;
                getFileName();
            }

            foreach (GridViewRow row in gvNotableAlumni.Rows)
            {
                row.BackColor = System.Drawing.Color.Transparent;
            }
            gvNotableAlumni.Rows[RowIndexS].BackColor = System.Drawing.Color.LightCyan;
            gvNotableAlumni.Rows[RowIndexS].Style.Add("Font-Weight", "Bold");
        }
    }

    protected void getFileName()
    {
        string FileName = lnkViewPhoto.CommandName;
        string FileUrl = ReturnPath();
        string FileDisplayName = "";
        string FileRealName = "";

        FileDisplayName = FileName;

        FileRealName = ReturnPath() + "/Alumni/" + FileName.Substring(FileName.IndexOf("/") + 1);
        FileUrl = FileUrl + FileName;
        lnkViewPhoto.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
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

    public static StoredProcedure ALM_Notable_Alumni_Mst_CRUD_Operations(string xmldoc, int mode, int pk_NAID)
    {
        SubSonic.StoredProcedure sp = new StoredProcedure("ALM_Notable_Alumni_Mst_CRUD", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@xmlDoc", xmldoc, DbType.String);
        sp.Command.AddParameter("@mode", mode, DbType.Int32);
        sp.Command.AddParameter("@PK_NAID", pk_NAID, DbType.Int32);
        return sp;
    }

    private void Delete()
    {
        try
        {
            if ((ALM_Notable_Alumni_Mst_CRUD_Operations(xmldoc, 4, Convert.ToInt32(ViewState["id"].ToString())).Execute()) > 0)
            {
                ClientMessaging("Record Deleted Successfully!");
            }
        }
        catch (SqlException ex)
        {
            lblMsg.Text = DAobj.ShowSQLErrorMsg(ex.Message.ToString().Trim(), "", ex);
        }
    }

    #endregion

    #region "GridView Events"

    protected void gvNotableAlumni_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["id"] = e.CommandArgument.ToString().Trim();
            if (e.CommandName.ToUpper().ToString() == "EDITREC")
            {
                DataSet ds = ALM_Notable_Alumni_Mst_CRUD_Operations(xmldoc, 3, Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int RowIndexS = gvr.RowIndex;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    txtSubheading.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                    if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "True")
                    {
                        chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"].ToString());
                    }
                    else
                    {
                        chkActive.Checked = Convert.ToBoolean(0);
                    }

                    ViewState["File1"] = ds.Tables[0].Rows[0]["NFileName"].ToString();
                    if (ds.Tables[0].Rows[0]["NFileName"] != null && ds.Tables[0].Rows[0]["NFileName"].ToString() != "")
                    {
                        lnkViewPhoto.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
                        lnkViewPhoto.CommandName = ds.Tables[0].Rows[0]["NFileName"].ToString();
                        lnkViewPhoto.Visible = true;
                        getFileName();
                    }

                    foreach (GridViewRow row in gvNotableAlumni.Rows)
                    {
                        row.BackColor = System.Drawing.Color.Transparent;
                    }
                    gvNotableAlumni.Rows[RowIndexS].BackColor = System.Drawing.Color.LightCyan;
                    gvNotableAlumni.Rows[RowIndexS].Style.Add("Font-Weight", "Bold");
                }
                btnSave.CommandName = "UPDATE";
                btnSave.Text = "UPDATE";
            }
            else if (e.CommandName.ToUpper().ToString() == "DELETEREC")
            {
                if (!IsPageRefresh)
                {
                    DataSet dsNA = ALM_Notable_Alumni_Mst_CRUD_Operations(xmldoc, 3, Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();
                    ReturnPhysicalPath();
                    System.IO.File.Delete(upldPath + dsNA.Tables[0].Rows[0]["NFileName"].ToString());

                    Delete();

                    Clear();
                    LoadFillGrid();
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
        gvNotableAlumni.PageIndex = e.NewPageIndex;
        LoadFillGrid();
    }

    #endregion

    protected void lnkViewPhoto_Click(object sender, EventArgs e)
    {
        getFileName();
    }

    #region "Global Variable"

    string upldPath = "";
    DataSet dsFile = null;

    #endregion

    public void ReturnPhysicalPath()
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
                    upldPath = dr["Physical_Path"].ToString().Trim();
                    upldPath = upldPath + "Alumni\\";
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }
}