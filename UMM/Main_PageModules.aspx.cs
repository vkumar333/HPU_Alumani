using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using SubSonic;
using System.IO;
using System.Collections;
//using HRMS;

public partial class UMM_Main_PageModules : System.Web.UI.Page
{
    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList();
    ArrayList types = new ArrayList();
    ArrayList values = new ArrayList();
    CommonCode.Common objComm = new CommonCode.Common();
//    EST_Order_Mst objBus = new EST_Order_Mst();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateSelectMod();
            FillGrid();
        }
        
    }
    private void PopulateSelectMod()
    {
        DataSet ds = GetModulesDetails().GetDataSet();
        ViewState["id"] = ds.Tables[0].Rows[0]["pk_moduleId"];
        ddlmodule.DataSource = ds;
        ddlmodule.DataValueField = "pk_moduleId";
        ddlmodule.DataTextField = "modulename";
        ddlmodule.DataBind();
        ddlmodule.Items.Insert(0, new ListItem("--Select Module--", "0"));

    }
    public StoredProcedure GetModulesDetails()
    {
        StoredProcedure sp = new StoredProcedure("sp_GetModuleName", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }
    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }
    private void Save()
    {
        string Message = "";
        try
        {
           

            string module = ddlmodule.SelectedItem.Text.ToString().Trim();
            string moduletype = ddlmtype.SelectedValue.ToString().Trim();
            string modulepath = txtpath.Text.ToString().Trim();
            string moduleid = ViewState["id"].ToString();
            string folderPath = Server.MapPath("~/img");
            flUpload.SaveAs(folderPath + Path.GetFileName(flUpload.FileName));

            int i = InsertRecord(module, moduletype, modulepath, flUpload.FileName, moduleid, ref Message).Execute();

            if (i > 0)
            {
                reset();
                
                //ClientMessaging("Record Save Successfully");
                lblMsg.Text = "Record Save Successfully";
                FillGrid();

            }
        }
        catch (Exception e)
        { }




    }
    public void FillGrid()
    {
        
        DataSet ds = GetModule().GetDataSet();
        gv.DataSource = ds.Tables[0];
        gv.DataBind();

    }
    public StoredProcedure GetModule()
    {
        StoredProcedure sp = new StoredProcedure("sp_GetModule", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }
    public StoredProcedure InsertRecord(string module, string moduletype, string modulepath, string filename,string moduleid, ref string Message)
    {
        StoredProcedure sp = new StoredProcedure("sp_MainPage_Ins", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Module_name", module, DbType.String);
        sp.Command.AddParameter("@Module_id", moduleid, DbType.String);
        sp.Command.AddParameter("@module_path", modulepath, DbType.String);
        sp.Command.AddParameter("@moduleimage", filename, DbType.String);
        sp.Command.AddParameter("@moduletype", moduletype, DbType.String);
        return sp;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.CommandName == "SAVE")
        {
            if (ddlmodule.SelectedIndex == 0)
            {
                ClientMessaging("module Required !");
                ddlmodule.Focus();
                return;
            }
            if (ddlmtype.SelectedIndex == 0)
            {
                ClientMessaging("module type Required !");
                ddlmtype.Focus();
                return;
            }
            if (txtpath.Text == "")
            {
                ClientMessaging("path Required !");
                txtpath.Focus();
                return;
            }

            Save();
            
        }
        else if (btnSave.CommandName.ToUpper().ToString() == "UPDATE")
        {
            if (ViewState["id"].ToString() != "")
            {
                Update();
            }
        }



    }
    public void reset()
    {
        ddlmodule.SelectedIndex=0;
        ddlmtype.SelectedIndex=0;
        txtpath.Text = null;
        




    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (btnReset.CommandName == "RESET")
        {
            reset();
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["id"] = e.CommandArgument.ToString().Trim();
            if (e.CommandName.ToUpper().ToString() == "EDITREC")
            {
                
                Edit();
            }
            else if (e.CommandName.ToUpper().ToString() == "DELETEREC")
            {
                Delete();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }


    }
    public DataSet EditRecord()
    {
        int Empid =Convert.ToInt32( ViewState["id"]);
        names.Add("@pk_id"); values.Add(Empid); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("sp_EditModule", values, names, types);
    }
    private void Edit()
    {
       
        try
        {
            btnSave.Text = "UPDATE";
            btnSave.CommandName = "UPDATE";
            DataSet ds = EditRecord();
            ddlmodule.SelectedItem.Text = ds.Tables[0].Rows[0]["Module_name"].ToString();
            ddlmtype.SelectedItem.Text = ds.Tables[0].Rows[0]["moduletype"].ToString();
            txtpath.Text= ds.Tables[0].Rows[0]["module_path"].ToString();
            ds.Tables[0].Rows[0]["moduleimage"].ToString();
            Anthem.Manager.IncludePageScripts = true;
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }
    private void Delete()
    {
        try
        {
            int pk_id = Convert.ToInt32(ViewState["id"]);
            string Message = "";
            if (DeleteRecord(ref Message) > 0)
            {
                FillGrid();
                
            }
            lblMsg.Text = Message;
        }
        catch (Exception ex)
        {
            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }
    public int DeleteRecord(ref string Message)
    {
        int Empid = Convert.ToInt32(ViewState["id"]);
        names.Add("@pk_id"); values.Add(Empid); types.Add(SqlDbType.Int);
        if (DAobj.ExecuteTransactionMsg("sp_Module_Del", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("D", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }
    private void Update()
    {
        try
        {
            string Message = ""; //string doc = "";
            int Empid = Convert.ToInt32(ViewState["id"]);


            string module = ddlmodule.SelectedItem.Text.ToString().Trim();
            string moduletype = ddlmtype.SelectedItem.Text.ToString().Trim();
            string modulepath = txtpath.Text.ToString().Trim();



            string folderPath = Server.MapPath("~/img/");
            flUpload.SaveAs(folderPath + Path.GetFileName(flUpload.FileName));

            if (ViewState["id"].ToString() != "")
            {
               
               int i= UpdateRecord(module, moduletype, modulepath, flUpload.FileName, Empid, ref Message).Execute();
                
                if (i > 0)
                {
                    FillGrid();
                    reset();
                }
                lblMsg.Text = "Record Update Successfully ";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }
    public StoredProcedure UpdateRecord(string module, string moduletype, string modulepath, string filename, int pk_id, ref string Message)
    {
        pk_id = Convert.ToInt32(ViewState["id"]);
        StoredProcedure sp = new StoredProcedure("sp_Module_Upd", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_id", pk_id, DbType.String);
        sp.Command.AddParameter("@Module_name", module, DbType.String);
        sp.Command.AddParameter("@module_path", modulepath, DbType.String);
        sp.Command.AddParameter("@moduleimage", filename, DbType.String);
        sp.Command.AddParameter("@moduletype", moduletype, DbType.String);

        return sp;
    }
}

