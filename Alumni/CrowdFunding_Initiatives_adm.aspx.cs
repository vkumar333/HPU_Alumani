/*
==================================================================================
Created By                                                : Ayush Tyagi
On Date                                                   :22 march 2023
Name                                                      :CrowdFunding_Initiatives_adm
Purpose                                                   :CrowdFunding Initiatives   
Tables used                                               :alm_contribution_mst
Stored Procedures used                                    :
Modules                                                   :Alumni
Form                                                      :CrowdFunding_Initiatives_adm.aspx
Last Updated Date                                         :
Last Updated By                                           :
==================================================================================
*/
using SubSonic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DataAccessLayer;
using System.IO;
using System.Data.SqlClient;
using System.Xml;

public partial class Alumni_CrowdFunding_Initiatives_adm : System.Web.UI.Page
{
    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
    DataAccess Dobj = new DataAccess();
    CustomMessaging eobj = new CustomMessaging();

    public int CatId { get; private set; }
    public string xmldoc { get; private set; }
    public int pk_contribution_ID { get; private set; }
    public int mode { get; private set; }

    private Boolean IsPageRefresh = false;

    public void ClearArrayLists()
    {
        names.Clear();
        types.Clear();
        values.Clear();
    }
    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    public static StoredProcedure BindDropdown()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Get_Cat_Dropdrown", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }
    /// Creates an object wrapper for the ContributionInsertData_Insert Procedure
    /// </summary>
    /// 
    public int ContributionInsert_and_update_Data(ref string Message)
    {
        try
        {
            names.Clear(); values.Clear(); types.Clear();
            names.Add("@doc"); types.Add(SqlDbType.VarChar); values.Add(xmldoc);
            names.Add("@mode"); types.Add(SqlDbType.VarChar); values.Add(mode);
            names.Add("@CatId"); values.Add(CatId); types.Add(SqlDbType.Int);
            names.Add("@pk_contribution_ID"); values.Add(pk_contribution_ID); types.Add(SqlDbType.Int);

            if (Dobj.ExecuteTransactionMsg("ALM_contribution_Ins_upd", values, names, types, ref Message) > 0)
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


    public int FileUpdateBit(ref string Message)
    {
        try
        {
            names.Clear(); values.Clear(); types.Clear();
            names.Add("@Pk_File_Detail_id"); values.Add(Pk_File_Detail_id); types.Add(SqlDbType.Int);
            if (Dobj.ExecuteTransactionMsg("ALM_FileBitUpdate", values, names, types, ref Message) > 0)
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
    public int FileUpdateBitzero(ref string Message)
    {
        try
        {
            names.Clear(); values.Clear(); types.Clear();
            names.Add("@Pk_File_Detail_id"); values.Add(Pk_File_Detail_id); types.Add(SqlDbType.Int);
            if (Dobj.ExecuteTransactionMsg("ALM_FileBitUpdatedisable", values, names, types, ref Message) > 0)
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
    //public static StoredProcedure ContributionInsertData(int? CatId, bool SetOnHomepage, string doc)
    //{
    //    SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_contribution_Insert", DataService.GetInstance("IUMSNXG"), "");

    //    sp.Command.AddParameter("@CatId", CatId, DbType.Int32);
    //    sp.Command.AddParameter("@SetOnHomepage", SetOnHomepage, DbType.Boolean);
    //    sp.Command.AddParameter("@xmlDoc", doc, DbType.String);

    //    return sp;
    //}

    public static StoredProcedure ContriPhotoGrpId(int CatId)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Contribution_photo_id", DataService.GetInstance("IUMSNXG"), "");

        sp.Command.AddParameter("@CatId", CatId, DbType.Int32);

        return sp;
    }
    public static StoredProcedure DetailsEdit(int pk_contribution_ID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Details_conEdit", DataService.GetInstance("IUMSNXG"), "");

        sp.Command.AddParameter("@pk_contribution_ID", pk_contribution_ID, DbType.Int32);

        return sp;
    }
    public static StoredProcedure Delete_Profile_dtls(int pk_contribution_ID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Delete_info", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_contribution_ID", pk_contribution_ID, DbType.Int32);
        return sp;
    }
    public static StoredProcedure Delete_Profile_inner_dtls(int Pk_File_Detail_id)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Delete_innergrid_info", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Pk_File_Detail_id", Pk_File_Detail_id, DbType.Int32);
        return sp;
    }

    public static StoredProcedure DetailsUpdate(int pk_contribution_ID, int fk_cat_id, string xmlDoc)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Update_Contribution_Details", DataService.GetInstance("IUMSNXG"), "");

        sp.Command.AddParameter("@pk_contribution_ID", pk_contribution_ID, DbType.Int32);
        sp.Command.AddParameter("@Fk_Ctaegory_Id", fk_cat_id, DbType.Int32);
        sp.Command.AddParameter("@xmlDoc", xmlDoc, DbType.String);

        return sp;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //var r = HttpContext.Current.Request;


        if (!IsPostBack)
        {
            BindCategory();


        }
    }

    void BindCategory()
    {
        DataSet ds = BindDropdown().GetDataSet();
        D_ddlCategories.DataSource = ds.Tables[0];
        D_ddlCategories.DataTextField = "Categories";
        D_ddlCategories.DataValueField = "pk_cat_id";
        D_ddlCategories.DataBind();
        D_ddlCategories.Items.Insert(0, new ListItem("-- Select --", "0"));

    }

    protected void D_ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Clear();
            gvDetails.Visible = false;

            if (D_ddlCategories.SelectedIndex == 0)
            {
                gvDetails.Visible = false;
            }
            else
            {
                gvDetails.Visible = true;
                FillGrid(Convert.ToInt32(D_ddlCategories.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }


    protected void FillGrid(int groupid)
    {

        try
        {

            DataSet ds = ContriPhotoGrpId(groupid).GetDataSet();
            gvDetails.DataSource = ds.Tables[0];
            try
            {

                gvDetails.DataBind();
            }
            catch
            {
                try
                {
                    gvDetails.DataBind();
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                }
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    private void Clear()
    {
        try
        {
            D_ddlCategories.Enabled = true;
            //gvDescription.SelectedIndex = -1;
            flUploadLogo.Dispose();
            btnSave.CommandName = "SAVE";
            btnSave.Text = "SAVE";
            text_heading.Text = "";
            Goal_amt_txt.Text = "";
            Anthem.Manager.IncludePageScripts = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "h", "show();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", "__doPostBack('UpdatePanel1', '');", true);
            Anthem.Manager.IncludePageScripts = true;
            lnkphotoname.Text = "";
            FillGrid(Convert.ToInt32(D_ddlCategories.SelectedValue));
            //gvDescription.Visible = true;
            lnkphotoname.Visible = false;
            Anthem.Manager.IncludePageScripts = true;
            D_ddlCategories.Focus();
            R_txtDiscription.Text = string.Empty;
            // chkhomepage.Checked = false;
            flUploadLogo.Dispose();
            GrdFile.Visible = false;
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    private bool checkValidFileOrNot()
    {
        double filesize = flUploadLogo.PostedFile.ContentLength;
        string Name = flUploadLogo.PostedFile.FileName;
        string fileExtension = System.IO.Path.GetExtension(flUploadLogo.FileName);

        filesize = Math.Round((filesize / 1024), 0);
        if (Name.Length > 50)
        {
            ClientMessaging("Upload Document Should not be more than 50 characters!");
            return false;
        }
        //if (fileExtension.ToLower() != ".doc" && fileExtension.ToUpper() != ".docx" && fileExtension.ToLower() != ".PDF" && fileExtension.ToLower() != ".jpg")
        //{
        //    Label1.ForeColor = System.Drawing.Color.Red;
        //    Label1.Text = "Only files with .doc and .docx,pdf,jpg extension are allowed";
        //}
        if (filesize > 10200)
        {
            ClientMessaging("Upload Document is " + filesize.ToString("0") + " KB, it should not be more than 10 MB !");
            return false;
        }
        if (flUploadLogo.HasFile == true)
        {
            string FileType = Path.GetExtension(flUploadLogo.PostedFile.FileName);

            //switch (FileType.ToLower())
            //{

            //    case ".jpg":
            //        return true;
            //    case ".jpeg":
            //        return true;
            //    case ".png":
            //        return true;
            //    default:
            //        ClientMessaging("Only files PNG, JPEG, JPG extension are allowed");
            //        return false;
            //}

        }


        return true;
    }
    public bool Validation()
    {

        if (D_ddlCategories.SelectedIndex == 0)
        {
            ClientMessaging("Categories cannot be blank!");
            D_ddlCategories.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(text_heading.Text.ToString()))
        {
            ClientMessaging("Heading cannot be blank!");
            text_heading.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(R_txtDiscription.Text.ToString()))
        {
            ClientMessaging("Discription cannot be blank!");
            R_txtDiscription.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(Goal_amt_txt.Text.ToString()))
        {
            ClientMessaging("Amount Raised  cannot be blank!..");
            R_txtDiscription.Focus();
            return false;
        }
        if (flUploadLogo.HasFile && (System.IO.Path.GetExtension(flUploadLogo.FileName) != ".jpg")
                && System.IO.Path.GetExtension(flUploadLogo.FileName) != ".jpeg" && System.IO.Path.GetExtension(flUploadLogo.FileName) != ".png")
        {
            ClientMessaging("Only JPG, JPEG and PNG files can be uploaded...!");
            flUploadLogo.Focus();
            return false;
        }

        return true;
    }
    public DataSet Getmain()
    {
        DataSet ds_master = Dobj.GetSchema("alm_contribution_mst");
        DataRow dr = ds_master.Tables[0].NewRow();
        dr["Heading"] = text_heading.Text.ToString();
        dr["Description"] = R_txtDiscription.Text.ToString();
        dr["Goal_Amount"] = Goal_amt_txt.Text.ToString();
        dr["fk_cat_id"] = D_ddlCategories.SelectedValue;
        dr["fk_userid"] = Session["UserID"].ToString();
        ds_master.Tables[0].Rows.Add(dr);

        DataSet ds1 = Dobj.GetSchema("Alm_Contributions_Uploadfile_Details");
        HttpFileCollection files = HttpContext.Current.Request.Files;

        /////////////// Upload Document////////////////////
        if (flUploadLogo.HasFile)
        {
            if (flUploadLogo.PostedFile.FileName != "")
            {
                string FileType = Path.GetExtension(flUploadLogo.PostedFile.FileName);
                if (FileType != null)
                {
                    string lFileName = "";
                    // dr["Student_img"] = flUploadLogo.FileName + "/" + lFileName;
                    //for (int i = 0; i < files.Count; i++)
                    //{
                    //    //file save
                    //    HttpPostedFile file = files[i];
                    //    dr["Image"] = lFileName;
                    //    //string fname = context.Server.MapPath("~/ALM_uploadimg/" + file.FileName);
                    //    //file.SaveAs(fname);
                    //}
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        Random r = new Random();
                        int n = r.Next();

                        // string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + flUploadLogo.FileName.Substring(flUploadLogo.FileName.LastIndexOf("."));
                        lFileName = FU_physicalPath(flUploadLogo, "PLACEMENT_DATA/Company_Profile", "BRO" + n.ToString() + file.FileName);
                        DataRow dr1 = ds1.Tables[0].NewRow();
                        dr1["Image"] = lFileName;
                        dr1["FilePath"] = Session["filepath"];

                        ds1.Tables[0].Rows.Add(dr1);

                    }
                }
            }
        }
        ds_master.Merge(ds1);
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
                        upldPath = upldPath + "\\PLACEMENT_DATA\\Company_Profile\\" + "";
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

                if (ContributionInsert_and_update_Data(ref Message) > 0)
                {
                    ClientMessaging("Record Save Successfully.");
                }
                else
                {
                    ClientMessaging("Record not saved......!");
                }
            }

            catch (Exception Ex)
            {
                Label1.Text = CommonCode.ExceptionHandling.SqlExceptionHandling(Ex.Message);
            }
        }
    }
    public DataSet Getmain_UPD()
    {
        DataSet ds_master = Dobj.GetSchema("alm_contribution_mst");
        DataRow dr = ds_master.Tables[0].NewRow();
        dr["Heading"] = text_heading.Text.ToString();
        dr["Description"] = R_txtDiscription.Text.ToString();
        dr["Goal_Amount"] = Goal_amt_txt.Text.ToString();
        dr["fk_cat_id"] = D_ddlCategories.SelectedValue;
        dr["fk_userid"] = Session["UserID"].ToString();
        ds_master.Tables[0].Rows.Add(dr);

        DataSet ds1 = Dobj.GetSchema("Alm_Contributions_Uploadfile_Details");
        HttpFileCollection files = HttpContext.Current.Request.Files;

        /////////////// Upload Document////////////////////
        if (flUploadLogo.HasFile)
        {
            if (flUploadLogo.PostedFile.FileName != "")
            {
                string FileType = Path.GetExtension(flUploadLogo.PostedFile.FileName);
                if (FileType != null)
                {
                    string lFileName = "";
                    // dr["Student_img"] = flUploadLogo.FileName + "/" + lFileName;
                    //for (int i = 0; i < files.Count; i++)
                    //{
                    //    //file save
                    //    HttpPostedFile file = files[i];
                    //    dr["Image"] = lFileName;
                    //    //string fname = context.Server.MapPath("~/ALM_uploadimg/" + file.FileName);
                    //    //file.SaveAs(fname);
                    //}
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        Random r = new Random();
                        int n = r.Next();

                        // string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + flUploadLogo.FileName.Substring(flUploadLogo.FileName.LastIndexOf("."));
                        lFileName = FU_physicalPath(flUploadLogo, "PLACEMENT_DATA/Company_Profile", "BRO" + n.ToString() + file.FileName);
                        DataRow dr1 = ds1.Tables[0].NewRow();
                        dr1["Image"] = lFileName;
                        dr1["FilePath"] = Session["filepath"];

                        ds1.Tables[0].Rows.Add(dr1);

                    }
                }
            }
        }
        ds_master.Merge(ds1);
        return ds_master;
    }

    private void Update()
    {
        try
        {
            if (D_ddlCategories.SelectedIndex == 0)
            {
                ClientMessaging("Categories cannot be blank!");
                D_ddlCategories.Focus();
                return;
            }
            if (string.IsNullOrEmpty(text_heading.Text.ToString()))
            {
                ClientMessaging("Heading cannot be blank!");
                //text_heading.Focus();
                //return false;
            }
            if (string.IsNullOrEmpty(R_txtDiscription.Text.ToString()))
            {
                ClientMessaging("Discription cannot be blank!");
                //R_txtDiscription.Focus();
                //return false;
            }
            if (string.IsNullOrEmpty(Goal_amt_txt.Text.ToString()))
            {
                ClientMessaging("Amount Raised  cannot be blank!..");
                //R_txtDiscription.Focus();
                //return false;
            }

            if (flUploadLogo.HasFile && (System.IO.Path.GetExtension(flUploadLogo.FileName) != ".jpg")
                && System.IO.Path.GetExtension(flUploadLogo.FileName) != ".jpeg" && System.IO.Path.GetExtension(flUploadLogo.FileName) != ".png")
            {
                ClientMessaging("Only JPG, JPEG and PNG files can be uploaded...!");
                flUploadLogo.Focus();
                return;
            }

            string Message = "";
            DataSet ds_details = new DataSet();
            ds_details = Getmain_UPD();
            pk_contribution_ID = Convert.ToInt32(ViewState["id"]);
            string doc = "";
            doc = ds_details.GetXml();
            xmldoc = doc;
            mode = 2;
            if (ContributionInsert_and_update_Data(ref Message) > 0)
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
            Label1.Text = CommonCode.ExceptionHandling.SqlExceptionHandling(ex.Message);
        }
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
                    Clear();
                    GrdFile.Visible = false;
                    //Fill_grid();
                }
                else
                {
                    Update();
                    GrdFile.Visible = false;
                    //Fill_grid();

                }
            }
        }
        catch (SqlException exp) // For SQLException (voilation on delete or unique key voilation)
        {
            Label1.Text = eobj.ShowSQLErrorMsg(exp.Message.ToString(), "", exp);
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    string xmlDoc = "";
    //    try
    //    {
    //        if (IsPageRefresh == true)
    //        {
    //            return;
    //        }

    //        if (D_ddlCategories.SelectedIndex == 0)
    //        {
    //            ClientMessaging("Categories cannot be blank!");
    //            D_ddlCategories.Focus();
    //            return;
    //        }
    //        if (text_heading.Text == "")
    //        {
    //            ClientMessaging("Heading cannot be blank!");
    //            text_heading.Focus();
    //            return;
    //        }
    //        if (R_txtDiscription.Text == "")
    //        {
    //            ClientMessaging("Discription cannot be blank!");
    //            R_txtDiscription.Focus();
    //            return;
    //        }
    //        if (Goal_amt_txt.Text == "")
    //        {
    //            ClientMessaging("Amount Raised  cannot be blank!..");
    //            R_txtDiscription.Focus();
    //            return;
    //        }
    //        //if (flUploadLogo.FileName.Trim() == "")
    //        //{
    //        //    ClientMessaging("Please Select File To Upload!..");
    //        //    flUploadLogo.Focus();
    //        //    return;
    //        //}
    //        gvDetails.Visible = true;
    //        if (btnSave.CommandName.ToUpper().ToString() == "SAVE")
    //        {
    //            HttpFileCollection files = HttpContext.Current.Request.Files;
    //            for (int i = 0; i < files.Count; i++)
    //            {
    //                //file save
    //                HttpPostedFile file = files[i];
    //                //string fname = context.Server.MapPath("~/ALM_uploadimg/" + file.FileName);
    //                //file.SaveAs(fname);
    //            }



    //            DataSet ds = UploadeFile();
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                ClientMessaging("No Image to Upload!");
    //                return;
    //            }

    //            bool retVal = checkValidFileOrNot();
    //            if (retVal == true)
    //                if ((ContributionInsertData(Convert.ToInt32(D_ddlCategories.SelectedValue), false, ds.GetXml())).Execute() > 0)
    //            {
    //                ClientMessaging("Record Saved Successfully");
    //                Clear();
    //            }
    //        }
    //        else
    //        {
    //            if (ViewState["id"].ToString() != "")
    //            {

    //                string fname = "";
    //                //  fname = lnkphotoname.Text;
    //                if (flUploadLogo.HasFile == true)
    //                {
    //                    DataSet dsFileupload = UploadeFile();
    //                    xmlDoc = dsFileupload.GetXml();
    //                }
    //                else
    //                {
    //                    DataSet ds = AttTable();
    //                    string fileName = Session["Photofilename"].ToString();
    //                    DataRow dr = ds.Tables[0].NewRow();
    //                    dr["Heading"] = text_heading.Text.Trim();
    //                    dr["Description"] = R_txtDiscription.Text.Trim();//txtDesc.Text.Trim();
    //                    dr["Goal_Amount"] = Goal_amt_txt.Text.Trim();

    //                    dr["Image"] = fileName.Trim();
    //                    if (chkhomepage.Checked == true)
    //                    {
    //                        dr["SetOnHomepage"] = true;
    //                    }
    //                    else
    //                        dr["SetOnHomepage"] = false;
    //                    ds.Tables[0].Rows.Add(dr);
    //                    xmlDoc = ds.GetXml();

    //                }

    //                bool retVal = checkValidFileOrNot();
    //                if (retVal == true)
    //                    if ((DetailsUpdate(Convert.ToInt32(ViewState["id"]), Convert.ToInt32(D_ddlCategories.SelectedValue), xmlDoc).Execute()) > 0)
    //                {
    //                    ClientMessaging("Record Updated Successfully");
    //                    Clear();
    //                }
    //            }
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        Label1.Text = ex.Message;
    //    }

    //}

    protected void Reset_Click(object sender, EventArgs e)
    {
        try
        {

            Clear();
            D_ddlCategories.SelectedIndex = 0;
            D_ddlCategories.Enabled = true;
            Goal_amt_txt.Text = "";
            FillGrid(0);
            GrdFile.Visible = false;
            Label1.Text = "";
            flUploadLogo.Dispose();
            gvDetails.Visible = false;
            GrdFile.Visible = false;

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void lnkviewBrc_Click(object sender, EventArgs e)
    {
        //getFileName();
    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        gvDetails.SelectedIndex = -1;
        this.FillGrid(0);
    }
    private void Edit()
    {
        DataSet ds = DetailsEdit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();
        DataSet ds1 = DetailsEdit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();
        if (ds.Tables[0].Rows.Count > 0 && ds1.Tables[1].Rows.Count > 0)
        {
            D_ddlCategories.SelectedValue = ds.Tables[0].Rows[0]["fk_cat_id"].ToString();
            R_txtDiscription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            text_heading.Text = ds.Tables[0].Rows[0]["Heading"].ToString();
            Goal_amt_txt.Text = ds.Tables[0].Rows[0]["Goal_Amount"].ToString();
            ViewState["File1"] = ds1.Tables[1].Rows[0]["Image"].ToString();
            GrdFile.Visible = true;
            GrdFile.DataSource = ds.Tables[1];
            GrdFile.DataBind();

            //if (ds.Tables[1].Rows[0]["Image"] != null && ds1.Tables[1].Rows[0]["Image"].ToString() != "")
            //{
            //lnkviewBrc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
            //lnkviewBrc.CommandName = ds1.Tables[1].Rows[0]["Image"].ToString();
            //lnkviewBrc.Visible = true;
            //getFileName();
            //}
            //btnSave.Text = "UPDATE";
            //btnSave.CommandName = "UPDATE";

        }
    }

    public string SetServiceDoc(string FileName)
    {
        string FolderName = @"/PLACEMENT_DATA/Company_Profile";
        string host = HttpContext.Current.Request.Url.Host;
        string FilePath = "";
        DataSet dsFilepath = new DataSet();
        //  dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
        XmlTextReader xmlreader = new XmlTextReader(Server.MapPath("~/UMM/IO_Config.xml"));
        dsFilepath.ReadXml(xmlreader);
        xmlreader.Close();

        foreach (DataRow dr in dsFilepath.Tables[0].Rows)
        {
            if (host == dr["Server_Ip"].ToString().Trim())
            {

                if (host != "localhost")
                {
                    // string FTPpath = ConfigurationManager.AppSettings["FTPpath"];
                    FilePath = "\\PLACEMENT_DATA\\Company_Profile\\" + FileName;
                    // FilePath = FolderName + FileName;
                }
                else
                {
                    FilePath = dr["http_Add"].ToString().Trim();
                    FilePath = @FilePath + FolderName + @"/" + FileName;
                    // FilePath = dr["Physical_Path"].ToString().Trim();
                    // FilePath = @FilePath + FolderName + @"/" + FileName;
                    //  FilePath = FolderName  + FileName;
                }
                //return FolderName+FileName;
                return FilePath;
            }
        }
        return FilePath;
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
    //protected void getFileName()
    //{
    //    string FileName = lnkviewBrc.CommandName;
    //    string FileUrl = ReturnPath();
    //    string FileDisplayName = "";
    //    string FileRealName = "";
    //    //if (FileName.Contains("/"))
    //    //{
    //    FileDisplayName = FileName;

    //    FileRealName = ReturnPath() + "/PLACEMENT_DATA/Company_Profile/" + FileName.Substring(FileName.IndexOf("/") + 1);
    //    //   }
    //    FileUrl = FileUrl + FileName;
    //    lnkviewBrc.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
    //}
    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            Label1.Text = "";
            D_ddlCategories.Enabled = false;
            ViewState["id"] = e.CommandArgument.ToString().Trim();
            gvDetails.SelectedIndex = -1;
            if (e.CommandName.ToUpper().ToString() == "EDITREC")
            {
                Edit();
                btnSave.Text = "UPDATE";
                btnSave.CommandName = "UPDATE";

            }
            else if (e.CommandName.ToUpper().ToString() == "DELETEREC")
            {
                IDataReader rdr = DetailsEdit(Convert.ToInt32(ViewState["id"])).GetReader();
                DataTable dtt = new DataTable();
                dtt.Load(rdr);

                rdr.Close();
                D_ddlCategories.Enabled = true;
                if ((Delete_Profile_dtls(Convert.ToInt32(ViewState["id"]))).Execute() > 0)
                {
                    D_ddlCategories.Focus();
                    FillGrid(0);
                    ClientMessaging("Record Deleted Successfully");
                    GrdFile.Visible = false;
                    Clear();
                }
            }

        }
        catch (Exception ex)
        {
            Label1.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
        D_ddlCategories.Focus();
        Anthem.Manager.IncludePageScripts = true;
    }
    private void Delete()
    {
        try
        {
            if ((Delete_Profile_inner_dtls(Convert.ToInt32(ViewState["idd"].ToString())).Execute()) > 0)
            {
                //lblMsg.Text = eobj.ShowMessage("D");
                ClientMessaging("Record Deleted Successfully!");
            }
        }
        catch (SqlException ex)
        {
            Label1.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }
    public static StoredProcedure getDetails_internaldetails()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_files_Dtls", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }
    public void Fill_grid()
    {

        DataSet ds = getDetails_internaldetails().GetDataSet();
        GrdFile.DataSource = ds.Tables[0];
        GrdFile.DataBind();
    }
    protected void GrdFile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper().ToString() == "DELETEREC")
        {
            ViewState["idd"] = e.CommandArgument.ToString().Trim();
            Delete();
            GrdFile.Visible = false;
            //Fill_grid();
            //btnSave.CommandName = "UPDATE";
            //btnSave.Text = "UPDATE";
        }
    }

    //protected void chkLeave_CheckedChanged(object sender, EventArgs e)
    //{
    //    int temp = 0;
    //    for(int i=0; i< GrdFile.Rows.Count;i++)
    //    {
    //        CheckBox chk = (CheckBox)GrdFile.Rows[i].FindControl("chkLeave");
    //        if(chk.Checked==true)
    //        {
    //            temp = 1;
    //        }

    //    }
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 

    public string Pk_File_Detail_id { get; set; }
    //protected void chkLeave_CheckedChanged(object sender, EventArgs e)
    //{
    //    string Message = "";
    //    int Counter = 0;
    //    for (int i = 0; i < GrdFile.Rows.Count; i++)
    //    {
    //        CheckBox chk = (CheckBox)GrdFile.Rows[i].FindControl("chkLeave");
    //        HiddenField hdnspkid = (HiddenField)GrdFile.Rows[i].FindControl("hdnspkid");
    //        if (chk.Checked == true)
    //        {
    //            Pk_File_Detail_id = hdnspkid.Value;
    //            Counter = 1;
    //        }
    //        else
    //        {
    //            chk.Checked = false;
    //            chk.Enabled = false;

    //        }
    //        if(Counter==0)
    //        {
    //            for (int j = 0; j < GrdFile.Rows.Count; j++)
    //            {
    //                CheckBox chkData = (CheckBox)GrdFile.Rows[i].FindControl("chkLeave");
    //                chkData.Checked = false;
    //                chkData.Enabled = true;

    //            }


    //        }

    //    }
    //    //CheckBox chkdd = (CheckBox)GrdFile.FindControl("chkLeave");
    //    //if (chkdd.Checked == true)
    //    //{
    //        if (FileUpdateBit(ref Message) > 0)
    //        {
    //            // ClientMessaging("set image on portal");
    //        }
    //    //}


    //}
    protected void chkLeave_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox activeCheckBox = sender as CheckBox;
        string Message = "";
        foreach (GridViewRow rw in GrdFile.Rows)
        {
            HiddenField hdnspkid = (HiddenField)rw.FindControl("hdnspkid");
            CheckBox chkBx = (CheckBox)rw.FindControl("chkLeave");
            if (chkBx != activeCheckBox)
            {
                Pk_File_Detail_id = hdnspkid.Value;
                chkBx.Checked = false;
                if (FileUpdateBitzero(ref Message) > 0)
                {
                    // ClientMessaging("set image on portal");
                }
            }
            else
            {
                Pk_File_Detail_id = hdnspkid.Value;
                chkBx.Checked = true;
                if (FileUpdateBit(ref Message) > 0)
                {
                    // ClientMessaging("set image on portal");
                }

            }
        }

    }
}