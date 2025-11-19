using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;

public partial class Alumni_Certificate_upload : System.Web.UI.Page
{
    //  private Boolean IsPageRefresh = false;
    DataAccess Cu = new DataAccess();
    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList();
    ArrayList types = new ArrayList();
    ArrayList values = new ArrayList();

    public string xmlDoc { get; set; }
    public int PK_Alm_Mst_Info { get; set; }

    public void ClearArrayLists()
    {
        names.Clear(); types.Clear(); values.Clear();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clear();
        }
    }

    protected void FillGrid()
    {
        DataSet ds = FillCategory_Details();
        GvDetails.DataSource = ds.Tables[0];
        GvDetails.DataBind();
    }

    public DataSet FillCategory_Details()
    {
        try
        {
            ClearArrayLists();
            return DAobj.GetDataSet("Student_Alumani_Link_info_Grid", values, names, types);
        }
        catch { throw; }
    }

    public DataSet GetMain()
    {
        DataSet dsMain = Cu.GetSchema("Alm_Alumni_information_mst");
        DataRow dr = dsMain.Tables[0].NewRow();
        dr["Website_link"] = Webtext.Text.ToString().Trim();
        dr["Linkdin_Link"] = linktext.Text.ToString().Trim();
        dr["Twitter_Link"] = twittertext.Text.ToString().Trim();
        dr["FB_Link"] = fbtext.Text.ToString().Trim();
        dr["YoutubeLink"] = txtYoutubeLink.Text.ToString().Trim();
        dr["IsActive"] = chkActive.Checked;

        string filepath1 = "";
        //string fileName1 = "";
        filepath1 = UploadImg.FileName.ToString();
        ViewState["Originalname"] = filepath1;
        HttpPostedFile theFile1 = UploadImg.PostedFile;
        if (UploadImg.HasFile)
        {
            if (UploadImg.PostedFile.FileName != "")
            {
                string FileType = Path.GetExtension(UploadImg.PostedFile.FileName);
                if (FileType != null)
                {
                    Random r = new Random();
                    int n = r.Next();
                    //string lFileName = "";
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssfffcerti") + UploadImg.FileName.Substring
                    (UploadImg.FileName.LastIndexOf("."));
                    string UniqueFile_name = filename.ToString();
                    ViewState["FileName"] = UniqueFile_name;

                    //string strFolder = Server.MapPath("~/FileUpload/");
                    //string strFilePath = strFolder + UniqueFile_name;
                    //UploadImg.PostedFile.SaveAs(strFilePath);

                    ReturnPhysicalPath();
                    bool IsExistPath = System.IO.Directory.Exists(upldPath);
                    if (!IsExistPath)
                        System.IO.Directory.CreateDirectory(upldPath);
                    UploadImg.SaveAs(upldPath + UniqueFile_name.ToString());

                    dr["Unique_File_Name"] = UniqueFile_name.ToString();
                    dr["Certficate_file_Path"] = upldPath; //(Server.MapPath("~/FileUpload/"));
                    dr["Certficate_file_name"] = ViewState["Originalname"];
                }
            }
        }
        else
        {
            dr["Unique_File_Name"] = ViewState["RC"].ToString();
            dr["Certficate_file_Path"] = upldPath; //(Server.MapPath("~/FileUpload/"));
            dr["Certficate_file_name"] = ViewState["Originalname"];
        }

        string filepath2 = "";
        //string fileName1 = "";
        filepath2 = UploadFilemoa.FileName.ToString();
        ViewState["Originalnamess"] = filepath2;
        HttpPostedFile theFile2 = UploadFilemoa.PostedFile;
        if (UploadFilemoa.HasFile)
        {
            if (UploadFilemoa.PostedFile.FileName != "")
            {
                string FileType = Path.GetExtension(UploadFilemoa.PostedFile.FileName);
                if (FileType != null)
                {
                    Random s = new Random();
                    int b = s.Next();
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + UploadFilemoa.FileName.Substring
                    (UploadFilemoa.FileName.LastIndexOf("."));
                    string UniqueFile_namess = filename.ToString();
                    ViewState["FileNamess"] = UniqueFile_namess;

                    //string strFolder = Server.MapPath("~/FileUpload/");
                    //string strFilePath = strFolder + UniqueFile_namess;
                    //UploadFilemoa.PostedFile.SaveAs(strFilePath);

                    ReturnPhysicalPath();
                    bool IsExistPath = System.IO.Directory.Exists(upldPath);
                    if (!IsExistPath)
                        System.IO.Directory.CreateDirectory(upldPath);
                    UploadFilemoa.SaveAs(upldPath + UniqueFile_namess.ToString());

                    dr["Unique_File_Namess"] = UniqueFile_namess.ToString();
                    dr["Certficate_file_Pathss"] = upldPath; //(Server.MapPath("~/FileUpload/"));
                    dr["Certficate_file_namess"] = ViewState["Originalnamess"];
                }
            }
        }
        else
        {
            dr["Unique_File_Namess"] = ViewState["MOA"].ToString();
            dr["Certficate_file_Pathss"] = upldPath; //(Server.MapPath("~/FileUpload/"));
            dr["Certficate_file_namess"] = ViewState["Originalnamess"];
        }

        dsMain.Tables[0].Rows.Add(dr);
        return dsMain;
    }

    public int InsertRecord(ref string Message)
    {
        ClearArrayLists();
        names.Add("@xmlDoc"); values.Add(xmlDoc); types.Add(SqlDbType.VarChar);
        if (DAobj.ExecuteTransactionMsg("Student_Alumani_Link_info_insert", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected bool Validation()
    {
        if (Webtext.Text.Trim() == "")
        {
            ClientMessaging("Please Enter Website Link !");
            Webtext.Focus();
            return false;
        }
        if (fbtext.Text.Trim() == "")
        {
            ClientMessaging("Please Enter Facebook Link !");
            fbtext.Focus();
            return false;
        }
        if (linktext.Text.Trim() == "")
        {
            ClientMessaging("Please Enter LinkedIn Link !");
            linktext.Focus();
            return false;
        }
        if (twittertext.Text.Trim() == "")
        {
            ClientMessaging("Please EnterTwitter Link !");
            twittertext.Focus();
            return false;
        }
        if (txtYoutubeLink.Text.Trim() == "")
        {
            ClientMessaging("Please Enter Youtube Link !");
            txtYoutubeLink.Focus();
            return false;
        }
        return true;
    }

    public void Save()
    {
        try
        {
            // if (Validation())
            // if (!IsPageRefresh)
            if (Webtext.Text != "" || fbtext.Text != "" || linktext.Text != "" || twittertext.Text != "" || txtYoutubeLink.Text != "")
            {
                string Message = "";
                if (UploadImg.HasFile && (System.IO.Path.GetExtension(UploadImg.FileName) != ".jpg")
                    && (System.IO.Path.GetExtension(UploadImg.FileName) != ".jpeg")
                    && (System.IO.Path.GetExtension(UploadImg.FileName) != ".png")
                    && (System.IO.Path.GetExtension(UploadImg.FileName) != ".pdf")
                    && (System.IO.Path.GetExtension(UploadImg.FileName) != ".docs"))
                {
                    ClientMessaging("Only jpg, jpeg and png and pdf files are Upload!.....");
                    return;
                }

                if (UploadImg.HasFile && UploadImg.PostedFile.ContentLength > (10 * 1024 * 1024))
                {
                    ClientMessaging("Please upload slider upto 10 MB size only.!");
                    UploadImg.Focus();
                    return;
                }

                string Messagess = "";

                if (UploadFilemoa.HasFile && (System.IO.Path.GetExtension(UploadFilemoa.FileName) != ".jpg")
                    && (System.IO.Path.GetExtension(UploadFilemoa.FileName) != ".jpeg")
                    && (System.IO.Path.GetExtension(UploadFilemoa.FileName) != ".png")
                    && (System.IO.Path.GetExtension(UploadFilemoa.FileName) != ".pdf")
                    && (System.IO.Path.GetExtension(UploadFilemoa.FileName) != ".docs"))
                {
                    ClientMessaging("Only jpg, jpeg, docs, png and pdf files are Upload!.....");
                    return;
                }

                if (UploadFilemoa.HasFile)
                {
                    int s = UploadFilemoa.PostedFile.ContentLength;

                    if (UploadFilemoa.HasFile && UploadFilemoa.PostedFile.ContentLength > (10 * 1024 * 1024))
                    {
                        ClientMessaging("Please upload slider upto 10 MB size only.!");
                        UploadFilemoa.Focus();
                        return;
                    }
                }

                DataSet dsMain = GetMain();
                xmlDoc = dsMain.GetXml();
                if (InsertRecord(ref Message) > 0)
                {
                    ClientMessaging("Record Save Successfully !");
                    clear();
                    FillGrid();
                }
            }
            else
            {
                ClientMessaging("Atleast One Field Is Required !");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
            throw;
        }
    }

    public int UpdateRecord(ref string Message)
    {
        ClearArrayLists();
        names.Add("@xmlDoc"); values.Add(xmlDoc); types.Add(SqlDbType.VarChar);
        names.Add("@PK_Alm_Mst_Info"); values.Add(PK_Alm_Mst_Info); types.Add(SqlDbType.Int);
        if (DAobj.ExecuteTransactionMsg("Student_Alumani_Link_info_Update", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    protected void Update()
    {
        try
        {
            //  if (!IsPageRefresh)
            //{
            //PK_Alm_Mst_Info = Convert.ToInt32(ViewState["id"]);
            //string Message = "";
            //DataSet dsMain = GetMain();
            //xmlDoc = dsMain.GetXml();
            //if (UpdateRecord(ref Message) > 0)
            //{
            //    ClientMessaging("Record Updated Successfully !");
            //    clear();
            //    FillGrid();
            //}


            if (Webtext.Text != "" || fbtext.Text != "" || linktext.Text != "" || twittertext.Text != "" || txtYoutubeLink.Text != "")
            {
                string Message = "";
                if (UploadImg.HasFile && (System.IO.Path.GetExtension(UploadImg.FileName) != ".jpg")
                    && (System.IO.Path.GetExtension(UploadImg.FileName) != ".jpeg")
                    && (System.IO.Path.GetExtension(UploadImg.FileName) != ".png")
                    && (System.IO.Path.GetExtension(UploadImg.FileName) != ".pdf")
                    && (System.IO.Path.GetExtension(UploadImg.FileName) != ".docs"))
                {
                    ClientMessaging("Only jpg, jpeg and png and pdf files are Upload!.....");
                    return;
                }

                if (UploadImg.HasFile && UploadImg.PostedFile.ContentLength > (10 * 1024 * 1024))
                {
                    ClientMessaging("Please upload slider upto 10 MB size only.!");
                    UploadImg.Focus();
                    return;
                }

                string Messagess = "";

                if (UploadFilemoa.HasFile && (System.IO.Path.GetExtension(UploadFilemoa.FileName) != ".jpg")
                    && (System.IO.Path.GetExtension(UploadFilemoa.FileName) != ".jpeg")
                    && (System.IO.Path.GetExtension(UploadFilemoa.FileName) != ".png")
                    && (System.IO.Path.GetExtension(UploadFilemoa.FileName) != ".pdf")
                    && (System.IO.Path.GetExtension(UploadFilemoa.FileName) != ".docs"))
                {
                    ClientMessaging("Only jpg, jpeg, docs, png and pdf files are Upload!.....");
                    return;
                }

                if (UploadFilemoa.HasFile)
                {
                    int s = UploadFilemoa.PostedFile.ContentLength;

                    if (UploadFilemoa.HasFile && UploadFilemoa.PostedFile.ContentLength > (10 * 1024 * 1024))
                    {
                        ClientMessaging("Please upload slider upto 10 MB size only.!");
                        UploadFilemoa.Focus();
                        return;
                    }
                }

                PK_Alm_Mst_Info = Convert.ToInt32(ViewState["id"]);
                DataSet dsMain = GetMain();
                xmlDoc = dsMain.GetXml();
                if (UpdateRecord(ref Message) > 0)
                {
                    ClientMessaging("Record Updated Successfully !");
                    clear();
                    FillGrid();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.CommandName.ToUpper().ToString() == "SAVE")
            {
                Save();
            }
            else
            {
                Update();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
            throw;
        }
    }

    public DataTable EditRecord()
    {
        ClearArrayLists();
        names.Add("@PK_Alm_Mst_Info"); values.Add(this.PK_Alm_Mst_Info); types.Add(SqlDbType.Int);
        return DAobj.GetDataTable("Student_Alumani_Link_info_edit", values, names, types);
    }

    public void Edit()
    {
        try
        {
            DataTable dt = EditRecord();
            Webtext.Text = dt.Rows[0]["Website_link"].ToString();
            fbtext.Text = dt.Rows[0]["FB_Link"].ToString();
            linktext.Text = dt.Rows[0]["Linkdin_Link"].ToString();
            twittertext.Text = dt.Rows[0]["Twitter_Link"].ToString();
            txtYoutubeLink.Text = dt.Rows[0]["YoutubeLink"].ToString();
            chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
            //lnk_Download.Text = dt.Rows[0]["Unique_File_Name"].ToString();
            //LinkButton1.Text = dt.Rows[0]["Unique_File_Namess"].ToString();

            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["Unique_File_Name"].ToString()))
            {
                lbl1.Visible = true;
                lblcolon.Visible = true;
                lnk_Download.Text = dt.Rows[0]["Unique_File_Name"].ToString();
                ViewState["RC"] = dt.Rows[0]["Unique_File_Name"].ToString();
            }
            else
            {
                lbl1.Visible = false;
                lblcolon.Visible = false;
            }

            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["Unique_File_Namess"].ToString()))
            {
                Label8.Visible = true;
                Label.Visible = true;
                LinkButton1.Text = dt.Rows[0]["Unique_File_Namess"].ToString();
                ViewState["MOA"] = dt.Rows[0]["Unique_File_Namess"].ToString();
            }
            else
            {
                Label8.Visible = false;
                Label.Visible = false;
            }

            lblMsg.Text = "";
            btnSave.CommandName = "UPDATE";
            btnSave.Text = "UPDATE";
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    public void clear()
    {
        Webtext.Text = "";
        linktext.Text = "";
        fbtext.Text = "";
        twittertext.Text = "";
        txtYoutubeLink.Text = "";
        lnk_Download.Text = "";
        LinkButton1.Text = "";
        chkActive.Checked = false;
        btnSave.CommandName = "SAVE";
        btnSave.Text = "SAVE";
        lbl1.Visible = false;
        lblcolon.Visible = false;
        Label8.Visible = false;
        Label.Visible = false;
        FillGrid();
        lblMsg.Text = "";
        ViewState["RC"] = "";
        ViewState["MOA"] = "";
        ViewState["Originalname"] = "";
        ViewState["Originalnamess"] = "";
        ViewState["FileName"] = "";
        ViewState["FileNamess"] = "";
        ViewState["id"] = "";
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();
    }

    // string path = @"L:\Published_APP\HPUBACKOFFICE\FileUpload";
    protected void lnk_Download_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName;
            Session["filename"] = lnk_Download.CommandName;
            FileName = lnk_Download.Text;
            string path = ReturnDocFilePath(); //@"L:\Published_APP\FTPSITE\HPU_DOC\";
            ViewState["UniqueFilename"] = FileName.ToString();
            //System.Configuration.AppSettingsReader configSetting = new System.Configuration.AppSettingsReader();
            string filePath = path + FileName;
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
            Response.TransmitFile(filePath);
            Response.End();
        }
        catch (Exception)
        {
            ClientMessaging("Registration certificate file or file path not found.");
        }
    }

    //To Delete the Reocrd
    public int DeleteRecord(ref string Message)
    {
        ClearArrayLists();
        names.Add("@PK_Alm_Mst_Info"); values.Add(PK_Alm_Mst_Info); types.Add(SqlDbType.VarChar);
        if (DAobj.ExecuteTransactionMsg("Student_Alumani_Link_info_dele", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("D", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    protected void Delete()
    {
        try
        {
            // if (!IsPageRefresh)

            string Message = "";
            if (DeleteRecord(ref Message) > 0)
            {
                ClientMessaging("Record Deleted Successfully !");
                clear();
                FillGrid();
            }
            else
            {
                clear();
                FillGrid();
                lblMsg.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void GvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            Anthem.Manager.IncludePageScripts = true;
            if (e.CommandName.ToUpper().ToString() == "EDITREC")
            {
                ViewState["id"] = e.CommandArgument.ToString();
                PK_Alm_Mst_Info = Convert.ToInt32(e.CommandArgument.ToString());

                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;

                foreach (GridViewRow row in GvDetails.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                }
                GvDetails.Rows[rowIndex].BackColor = System.Drawing.Color.LightCyan;

                Edit();
            }
            else if (e.CommandName.ToUpper().ToString() == "DELETEREC")
            {
                PK_Alm_Mst_Info = Convert.ToInt32(e.CommandArgument.ToString());
                Delete();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void GvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvDetails.PageIndex = e.NewPageIndex;
        FillGrid();
        lblMsg.Text = "";
    }

    // string path = "F:\\grev\\HPU_Backoffice\\FileUpload";
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            string FileNames;
            Session["filenamess"] = LinkButton1.CommandName;
            FileNames = LinkButton1.Text;
            string path = ReturnDocFilePath(); // @"L:\Published_APP\FTPSITE\HPU_DOC\";
            ViewState["Unique_File_Namess"] = FileNames.ToString();
            //System.Configuration.AppSettingsReader configSetting = new System.Configuration.AppSettingsReader();
            string filePath = path + FileNames;
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
            Response.TransmitFile(filePath);
            Response.End();
        }
        catch (Exception)
        {
            ClientMessaging("Memorandum Of association file or file path not found.");
        }
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

    public string ReturnDocFilePath()
    {
        string FolderName = @"/Alumni/";
        string host = HttpContext.Current.Request.Url.Host;
        string FilePath = "";
        DataSet dsFilepath = new DataSet();
        dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
        foreach (DataRow dr in dsFilepath.Tables[0].Rows)
        {
            if (host == dr["Server_Ip"].ToString().Trim())
            {
                FilePath = dr["Physical_Path"].ToString().Trim();
                //FilePath = Server.MapPath("~/Published_App/FTPsite");
                FilePath = @FilePath + FolderName;
                return FilePath;
            }
        }
        return "";
    }
}