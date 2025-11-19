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

public partial class Alumni_Alumni_NewsEvents_View : System.Web.UI.Page
{
    public string xmldoc { get; private set; }
    public int mode { get; private set; }
    public int PK_Events_id { get; set; }
    public int pk_Alumniid { get; set; }

    #region "Global Declaration"
	
    private Boolean IsPageRefresh = false;
    DataAccess Dobj = new DataAccess();
    CustomMessaging eobj = new CustomMessaging();
    CommonFunction cfObj = new CommonFunction();
    bool active = false;
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

    #endregion
	
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
        if (Session["AlumniID"] != null)
        {
            pk_Alumniid = Convert.ToInt32(Session["AlumniID"].ToString());
        }
       
        if (!IsPostBack)
        {
            FillGrid();
            //Clear();
        }
    }
	
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    public static StoredProcedure alm_alumni_check(int pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_alumni_check", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    private void FillGrid()
    {
        crypto cpt = new crypto();
        IDataReader rdr = IUMSNXG_ALM.SP.AlumniNewsEventsSelectIsActiveOnly().GetReader();
        DataTable dtE = new DataTable();
        dtE.Load(rdr);
        dtE.Columns.Add("encId");
        int rnum = (dtE.Rows.Count) + 1;

        if (dtE.Rows.Count > 0)
        {
            for (int x = 0; x < dtE.Rows.Count; x++)
            {
                string pkid = dtE.Rows[x]["PK_Events_id"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dtE.Rows[x]["encId"] = encId;
            }
            newsEventsRepeater.DataSource = dtE;
            newsEventsRepeater.DataBind();
        }
    }

    protected void lnkAddEvent_Click(object sender, EventArgs e)
    {
        String script = String.Format("document.getElementById('EventDiv').style.display = 'block';");
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CreateEventDetail", script, true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.CommandName.ToString().ToUpper() == "SAVE")
            {
                Save();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    public bool Validation()
    {
        if (R_txtEventTitle.Text == "" || R_txtEventTitle.Text == null)
        {
            ClientMessaging("Event Name is Required!");
            R_txtEventTitle.Focus();
            return false;
        }
        else if (R_txtEventDescription.Text == "" || R_txtEventDescription.Text == null)
        {
            ClientMessaging("Description is Required!");
            R_txtEventDescription.Focus();
            return false;
        }
        else if (V_txtStartDate.Text == "" || V_txtStartDate.Text == null)
        {
            ClientMessaging("Start Date is Required!");
            V_txtStartDate.Focus();
            return false;
        }
        else if (V_txtEndDate.Text == "" || V_txtEndDate.Text == null)
        {
            ClientMessaging("End Date is Required!");
            V_txtEndDate.Focus();
            return false;
        }

        string startDate = CommonCode.DateFormats.Date_FrontToDB_R(V_txtStartDate.Text.Trim());
        string EndDate = CommonCode.DateFormats.Date_FrontToDB_R(V_txtEndDate.Text.Trim());

        if (Convert.ToDateTime(startDate) > Convert.ToDateTime(EndDate))
        {
            ClientMessaging("Event Start Date Can not be greated than End Date.");
            // Anthem.Manager.IncludePageScripts = true;
            V_txtStartDate.Focus();
            return false;
        }
        else if (TextAddress.Text == "" || TextAddress.Text == null)
        {
            ClientMessaging("Address is Required");
            TextAddress.Focus();
            return false;
        }
        else if (flUploadLogo.FileName == null || flUploadLogo.FileName == "")
        {
            ClientMessaging("Please Upload File...!");
            flUploadLogo.Focus();
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

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    #region "Save Methods"

    public void Save()
    {
        if (!Validation())
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Load", "javascript:ShowEvent(this);", true);
            return;
        }
        else
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
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Load", "javascript:HideSearch(this);", true);
                    //getUploaded_details();
                    // R_txtStuyDesc.Text = "";
                    // ctctxt.Text = string.Empty;
                    //D_ddlJobList.SelectedIndex = 0;
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


    #endregion

    public DataSet Getmain()
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
                    lFileName = FU_physicalPath(flUploadLogo, "Alumni", "BRO" + n.ToString() + filename);
                    // dr["Student_img"] = flUploadLogo.FileName + "/" + lFileName;
                    dr["File_name"] = lFileName;
                    dr["File_path"] = Session["filepath"];
                }
            }
        }
        dr["Start_date"] = CommonCode.DateFormats.Date_FrontToDB_R(V_txtStartDate.Text.Trim());
        dr["End_date"] = CommonCode.DateFormats.Date_FrontToDB_R(V_txtEndDate.Text.Trim());
        // dr["CreatedBy"] = pk_Alumniid.ToString();
        dr["Fk_AlumniId"] = pk_Alumniid.ToString();
        dr["IsActive"] = true;
        return ds_master;
    }

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

    #region "Button Reset and Save Events"

    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Clear();
    }

    #endregion

    #region "Common Methods"

    private void Clear()
    {
        btnSave.CommandName = "SAVE";
        btnSave.Text = "SAVE";
        R_txtEventDescription.Text = "";
        V_txtStartDate.Text = "";
        R_txtEventTitle.Text = "";
        V_txtEndDate.Text = "";
        TextAddress.Text = "";
        Anthem.Manager.IncludePageScripts = true;
        R_txtEventTitle.Focus();

        if (!flUploadLogo.HasFile)
        {
            flUploadLogo.Dispose();
        }
    }

    protected void Client_Messaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
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
            lblMsg.Text = Dobj.ShowSQLErrorMsg(ex.Message.ToString().Trim(), "", ex);
        }
    }

    #endregion
}
