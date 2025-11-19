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
using SubSonic;
using DataAccessLayer;
using System.IO;
using System.Data.SqlClient;
using System.Web.Services;

public partial class Alumni_ALM_Alumni_NewsStories_Lists : System.Web.UI.Page
{
    public string xmldoc { get; private set; }
    public int mode { get; private set; }
    public int PK_Events_id { get; set; }

    #region "Global Declaration"

    private Boolean IsPageRefresh = false;
    DataAccess Dobj = new DataAccess();
    CustomMessaging eobj = new CustomMessaging();
    CommonFunction cfObj = new CommonFunction();
    crypto cpt = new crypto();
    bool active = false;

    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

    #endregion

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AlumniID"].ToString() == "" || Session["AlumniID"] == null)
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }

        int check = int.Parse(alm_alumni_check(int.Parse(Session["AlumniID"].ToString())).GetDataSet().Tables[0].Rows[0][0].ToString());

        if (check < 1)
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }

        if (!IsPostBack)
        {
            getAllNewsRepeater();
            getAllStoriesRepeater();
        }
    }

    public static StoredProcedure alm_alumni_check(int pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_alumni_check", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure AlumniNewsStoriesSelectIsActiveOnly()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_NewsStories_SelectIsActiveOnly", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    protected void lnkAddEvent_Click(object sender, EventArgs e)
    {
        String script = String.Format("document.getElementById('EventDiv').style.display = 'block';");
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CreateEventDetail", script, true);
    }

    //public bool Validation()
    //{
    //    if (D_ddlCategories.SelectedIndex == 0)
    //    {
    //        ClientMessaging("Categories cannot be blank!");
    //        D_ddlCategories.Focus();
    //        return false;
    //    }
    //    if (text_heading.Text == "")
    //    {
    //        ClientMessaging("Heading cannot be blank!");
    //        text_heading.Focus();
    //        return false;
    //    }
    //    if (R_txtDiscription.Text == "")
    //    {
    //        ClientMessaging("Description cannot be blank!");
    //        R_txtDiscription.Focus();
    //        return false;
    //    }

    //    if (flUploadLogo.HasFile == true)
    //    {
    //        double filesize = flUploadLogo.PostedFile.ContentLength;
    //        string Name = flUploadLogo.PostedFile.FileName;
    //        string FileType = Path.GetExtension(flUploadLogo.PostedFile.FileName);

    //        filesize = Math.Round((filesize / 1024), 0);

    //        if (Name.Length > 100)
    //        {
    //            ClientMessaging("Upload Document Should not be more than 100 characters!");
    //            return false;
    //        }

    //        if (filesize > (5 * 1024 * 1024))
    //        {
    //            ClientMessaging("Upload Document is " + filesize.ToString("0") + " KB, it should not be more than 5 MB !");
    //            return false;
    //        }

    //        switch (FileType.ToLower())
    //        {
    //            case ".jpg":
    //                return true;
    //            case ".jpeg":
    //                return true;
    //            case ".png":
    //                return true;
    //            default:
    //                ClientMessaging("Only files PNG, JPEG, JPG extension are allowed");
    //                return false;
    //        }
    //    }
    //    return true;
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string xmlDoc = "";
            try
            {
                if (IsPageRefresh == true)
                {
                    return;
                }

                if (D_ddlCategories.SelectedIndex == 0)
                {
                    ClientMessaging("Categories cannot be blank!");
                    D_ddlCategories.Focus();
                    return;
                }
                if (text_heading.Text == "")
                {
                    ClientMessaging("Heading cannot be blank!");
                    text_heading.Focus();
                    return;
                }
                if (R_txtDiscription.Text == "")
                {
                    ClientMessaging("Description cannot be blank!");
                    R_txtDiscription.Focus();
                    return;
                }

                //if (!Validation())
                //{
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Load", "javascript:ShowEvent(this);", true);
                //return;
                //}
                //else
                //{

                if (btnSave.CommandName.ToUpper().ToString() == "SAVE")
                {
                    DataSet ds = UploadeFile();
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        ClientMessaging("No Image to Upload!");
                        return;
                    }

                    bool retVal = checkValidFileOrNot();

                    if (retVal == true)
                    {
                        if ((NewsNdstoriesInsertByPortal(Convert.ToInt32(D_ddlCategories.SelectedValue), false, ds.GetXml())).Execute() > 0)
                        {
                            ClientMessaging("Record Saved Successfully");
                            Clear();
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Load", "javascript:HideSearch(this);", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Load", "javascript:ShowEvent(this);", true);
                        flUploadLogo.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    #region "Save Methods"

    public static StoredProcedure NewsNdstoriesInsertByPortal(int? CatId, bool SetOnHomepage, string doc)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_News_dataInsert_By_Portal", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@CatId", CatId, DbType.Int32);
        sp.Command.AddParameter("@SetOnHomepage", SetOnHomepage, DbType.Boolean);
        sp.Command.AddParameter("@xmlDoc", doc, DbType.String);
        return sp;
    }

    #endregion

    public int ins_upd_delevents_dtls(ref string Message)
    {
        try
        {
            names.Clear(); values.Clear(); types.Clear();
            names.Add("@doc"); types.Add(SqlDbType.VarChar); values.Add(xmldoc);
            names.Add("@mode"); types.Add(SqlDbType.VarChar); values.Add(mode);
            names.Add("@PK_Events_id"); values.Add(PK_Events_id); types.Add(SqlDbType.Int);
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
                        upldPath = upldPath + "Alumni\\" + "";
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

    #region "Button Reset and Save Events"

    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
            D_ddlCategories.Enabled = true;
            flUploadLogo.Dispose();
            D_ddlCategories.SelectedIndex = 0;
            Label1.Text = "";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    #endregion

    #region "Common Methods"

    private void Clear()
    {
        try
        {
            D_ddlCategories.Enabled = true;
            flUploadLogo.Dispose();
            btnSave.CommandName = "SAVE";
            btnSave.Text = "SAVE";
            text_heading.Text = "";
            Anthem.Manager.IncludePageScripts = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "h", "show();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", "__doPostBack('UpdatePanel1', '');", true);
            Anthem.Manager.IncludePageScripts = true;
            D_ddlCategories.Focus();
            R_txtDiscription.Text = string.Empty;
            chkhomepage.Checked = false;
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    void clearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    protected void Client_Messaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
        //Anthem.Manager.AddScriptForClientSideEval("alert('" + msg + "');");
    }

    #endregion

    #region "Delete Methods"

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
                ClientMessaging("Record Deleted Successfully!");
            }
        }
        catch (SqlException ex)
        {
            // lblMsg.Text = Dobj.ShowSQLErrorMsg(ex.Message.ToString().Trim(), "", ex);
        }
    }

    #endregion

    protected DataSet AttTable()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add(new DataTable("attch"));
        ds.Tables[0].Columns.Add("Heading", typeof(string));
        ds.Tables[0].Columns.Add("Description", typeof(string));
        ds.Tables[0].Columns.Add("Image", typeof(string));
        ds.Tables[0].Columns.Add("FilePath", typeof(string));
        ds.Tables[0].Columns.Add("SetOnHomepage", typeof(bool));
        ds.Tables[0].Columns.Add("PostedBy", typeof(string));
        ds.Tables[0].Columns.Add("fk_AlumniId", typeof(int));
        ds.Tables[0].Columns.Add("IsActive", typeof(bool));
        return ds;
    }

    protected DataSet UploadeFile()
    {
        int i = 0;
        string fileName = "";
        string[] fnAll = null;
        string fnError = "";
        string c = "";
        ViewState["fileUploadeStatus"] = 0;
        ArrayList myfile = Session["Attachment"] as ArrayList;
        DataSet ds = AttTable();
        HttpFileCollection Files = Request.Files;
        for (int j = 0; j < Files.Count; j++)
        {
            fileName = "";
            i = i + 1;
            c = Files[j].FileName;
            double d = Files[j].ContentLength;
            try
            {
                Guid g = Guid.NewGuid();
                string uniqueStr = Convert.ToBase64String(g.ToByteArray());
                uniqueStr = uniqueStr.Replace("=", "");
                uniqueStr = uniqueStr.Replace("+", "");
                uniqueStr = uniqueStr.Replace("/", "");
                uniqueStr = uniqueStr.Replace("@", "");
                uniqueStr = uniqueStr.Replace("#", "");
                uniqueStr = uniqueStr.Replace("$", "");
                uniqueStr = uniqueStr.Replace(@"\", "");
                uniqueStr = uniqueStr.Replace("*", "");
                uniqueStr = uniqueStr.Replace("&", "");
                uniqueStr = uniqueStr.Replace("~", "");

                c = c.Replace("=", "");
                c = c.Replace("+", "");
                c = c.Replace("/", "");
                c = c.Replace("@", "");
                c = c.Replace("#", "");
                c = c.Replace("$", "");
                c = c.Replace(@"\", "");
                c = c.Replace("*", "");
                c = c.Replace("&", "");
                c = c.Replace("~", "");

                c = uniqueStr + c;
                c = c.Replace("#", "");
                fileName = c;
                DataRow dr = ds.Tables[0].NewRow();
                dr["Heading"] = text_heading.Text.Trim();
                dr["Description"] = R_txtDiscription.Text.Trim();//txtDesc.Text.Trim();
                dr["Image"] = fileName.Trim();
                if (chkhomepage.Checked == true)
                {
                    dr["SetOnHomepage"] = true;
                }
                else
                    dr["SetOnHomepage"] = false;

                ReturnPhysicalPath();

                dr["FilePath"] = upldPath;
                dr["PostedBy"] = Session["UserID"].ToString();

                if (Session["AlumniID"] != null)
                {
                    dr["fk_AlumniId"] = Convert.ToInt32(Session["AlumniID"]);
                }
                else
                {
                    dr["fk_AlumniId"] = DBNull.Value;
                }

                dr["IsActive"] = true;

                ds.Tables[0].Rows.Add(dr);

                bool IsExistPath = System.IO.Directory.Exists(upldPath);
                if (!IsExistPath)
                    System.IO.Directory.CreateDirectory(upldPath);
                Files[j].SaveAs(upldPath + fileName.ToString());

                R_txtDiscription.Text = string.Empty;
                text_heading.Text = string.Empty;
            }
            catch (Exception Exp)
            {
                fnError = fnError + Exp.Message + "<br />";
            }
        }

        if (i == (fileName.Split('#').Length - 1))
        {
            fnAll = fileName.Split('#');
            ViewState["fileUploadeStatus"] = 1;
        }
        ViewState["fileUploadeError"] = fnError;

        Session["photofile"] = Convert.ToString(ds.Tables[0].Rows[0]["Image"]);

        return ds;
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
                    upldPath = upldPath + "\\Alumni\\";
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }

    //it will return file based on file unique name
    public string SetServiceDoc(string FileName)
    {
        string FolderName = @"\Alumni\";
        string host = HttpContext.Current.Request.Url.Host;
        string FilePath = "";
        DataSet dsFilepath = new DataSet();
        dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
        foreach (DataRow dr in dsFilepath.Tables[0].Rows)
        {
            if (host == dr["Server_Ip"].ToString().Trim())
            {
                if (host != "localhost")
                {
                    FilePath = dr["http_Add"].ToString().Trim();
                    FilePath = @FilePath + FolderName + FileName;
                }
                else
                {
                    FilePath = dr["Physical_Path"].ToString().Trim();
                    FilePath = @FilePath + FolderName + FileName;
                }
                return FilePath;
            }
        }
        return FilePath;
    }

    private bool checkValidFileOrNot()
    {
        double filesize = flUploadLogo.PostedFile.ContentLength;
        string Name = flUploadLogo.PostedFile.FileName;
        string fileExtension = System.IO.Path.GetExtension(flUploadLogo.FileName);

        filesize = Math.Round((filesize / 1024), 0);

        if (Name.Length > 100)
        {
            ClientMessaging("Upload Document Should not be more than 100 characters!");
            return false;
        }

        if (filesize > (5 * 1024 * 1024))
        {
            ClientMessaging("Upload Document is " + filesize.ToString("0") + " KB, it should not be more than 5 MB !");
            return false;
        }

        string FileType = Path.GetExtension(flUploadLogo.PostedFile.FileName);

        if (flUploadLogo.HasFile == true)
        {
            switch (FileType.ToLower())
            {
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".png":
                    return true;
                default:
                    ClientMessaging("Only files PNG, JPEG, JPG extension are allowed");
                    return false;
            }
        }
        return true;
    }

    void BindCategory()
    {
        DataSet ds = GetNewsCategory().GetDataSet();
        D_ddlCategories.DataSource = ds.Tables[0];
        D_ddlCategories.DataTextField = "categories";
        D_ddlCategories.DataValueField = "Pk_Ctaegory_Id";
        D_ddlCategories.DataBind();
        D_ddlCategories.Items.Insert(0, new ListItem("-- Select --", "0"));
    }

    public static StoredProcedure GetNewsCategory()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Category_Dropdrown", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    public int Delete_By_Alumni_News_Stories(int pid, ref string Message)
    {
        clearArrayLists();
        names.Add("@pk_Id"); values.Add(pid); types.Add(SqlDbType.Int);
        names.Add("@alumniID"); values.Add(Convert.ToInt32(Session["AlumniID"].ToString())); types.Add(SqlDbType.Int);
        if (Dobj.ExecuteTransactionMsg("ALM_News_Stories_Mst_Delete_By_Alumni", values, names, types, ref Message) > 0)
        {
            Message = Dobj.ShowMessage("D", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    protected void lnkAddNews_Click(object sender, EventArgs e)
    {
        String script = String.Format("document.getElementById('EventDiv').style.display = 'block';");
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CreateEventDetail", script, true);
    }

    private DataSet NewsDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("ALM_GetNews_Latest", values, names, types);
    }

    private DataSet GetLatestNewsandStoriesDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("ALM_GetNews_Latest", values, names, types);
    }

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private void getAllStoriesRepeater()
    {
        DataSet ds = new DataSet();
        ds = NewsDetails();
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        dt.Columns.Add("encId");

        if (dt.Rows.Count > 0)
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                string pkid = dt.Rows[x]["Pk_Stories_id"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dt.Rows[x]["encId"] = encId;

                //string imgFile = dt.Rows[x]["Image"].ToString();
                //string imgPath = SetServiceDoc(imgFile);
                //string imgEncId = GetBase64Image(imgPath);
                //dt.Rows[x]["ImageUrl"] = imgEncId;
            }
            storiesRepeater.DataSource = dt;
            storiesRepeater.DataBind();
        }
    }

    private void getAllNewsRepeater()
    {
        DataSet ds = new DataSet();
        ds = GetLatestNewsandStoriesDetails();
        DataTable dt = new DataTable();
        dt = ds.Tables[1];
        dt.Columns.Add("encId");

        if (dt.Rows.Count > 0)
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                string pkid = dt.Rows[x]["Pk_Stories_id"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dt.Rows[x]["encId"] = encId;

                //string imgFile = dt.Rows[x]["Image"].ToString();
                //string imgPath = SetServiceDoc(imgFile);
                //string imgEncId = GetBase64Image(imgPath);
                //dt.Rows[x]["ImageUrl"] = imgEncId;
            }
            newsRepeater.DataSource = dt;
            newsRepeater.DataBind();
        }
    }
}