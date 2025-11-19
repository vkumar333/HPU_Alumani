using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DataAccessLayer;
using System.Collections;

public partial class Alumni_ALM_Publish_JobCategory : System.Web.UI.Page
{
    string Message = string.Empty;

    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
    DataAccess Dobj = new DataAccess();

    public string xmlDoc { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        Message = string.Empty;
        if (!IsPostBack)
        {
            ViewState["id"] = 1;
            Fill_Grid();
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = Dobj.GetSchema("ALM_Alumni_Jobs_Category_Mst");
            ds.Tables[0].TableName = "ALM_Alumni_Jobs_Category_Mst";
            DataRow dr = ds.Tables[0].NewRow();
            dr["Name"] = CommonFunction.RemoveWhitespaceWithSplit(R_txtJobName.Text);
            dr["IsActive"] = Chk_IsActive.Checked;
            int chkedisactive = Convert.ToInt32(Chk_IsActive.Checked);
            ds.Tables[0].Rows.Add(dr);
            xmlDoc = ds.GetXml();
            if (btnSave.CommandName.ToUpper() != "SAVE")
            {
                int s = UpdateRec(Convert.ToInt32(ViewState["id"]), chkedisactive, ref Message);
                if (s > 0)
                {
                    Reset();
                    Fill_Grid();
                }
                lblMsg.Text = Message;
            }
            else
            {
                int i = SAL_insert(ref Message);
                if (i > 0)
                {
                    Reset();
                    Fill_Grid();
                }
                lblMsg.Text = Message;
            }
        }
        catch (SqlException sqlex)
        {
            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(sqlex.Message);
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    void Fill_Grid()
    {
        gvDetails.DataSource = SelectAll();
        gvDetails.DataBind();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
        lblMsg.Text = "";
    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";
        btnSave.Enabled = true;
        try
        {
            ViewState["id"] = Convert.ToInt32(e.CommandArgument).ToString().Trim();
            if (e.CommandName.ToUpper().ToString().Trim() == "SELECT")
            {
                btnSave.Text = "UPDATE";
                btnSave.CommandName = "UPDATE";
                btnSave.TextDuringCallBack = "UPDATING...";
                DataSet ds = Edit(Convert.ToInt32(e.CommandArgument));
                ds.Tables[0].TableName = "ALM_Alumni_Jobs_Category_Mst";
                R_txtJobName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                Chk_IsActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]);
            }
            else if (e.CommandName.ToUpper().ToString().Trim() == "DELETEREC")
            {
                DataSet ds = DeleteRec1(Convert.ToInt32(e.CommandArgument));
                Reset();
                lblMsg.Text = "Record Deleted Successfully!";
                Fill_Grid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        Fill_Grid();
        lblMsg.Text = "";
    }

    #region Methods

    public int SAL_insert(ref string Message)
    {
        ClearArrayLists();
        names.Add("@xmldoc"); types.Add(SqlDbType.VarChar); values.Add(xmlDoc);
        if (Dobj.ExecuteTransactionMsg("ALM_Alumni_Jobs_Category_Mst_INS", values, names, types, ref Message) > 0)
        {
            Message = Dobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public DataSet DeleteRec1(int pk_sal_id)
    {
        ClearArrayLists();
        names.Add("@Pk_JobCId"); types.Add(SqlDbType.Int); values.Add(pk_sal_id);
        return Dobj.GetDataSet("ALM_Alumni_Jobs_Category_Mst_Delete", values, names, types);
    }

    public int UpdateRec(int pk_JobCId, int IsActive, ref string Message)
    {
        ClearArrayLists();
        names.Add("@Pk_JobCId"); types.Add(SqlDbType.Int); values.Add(pk_JobCId);
        //names.Add("@Pk_JobCId"); types.Add(SqlDbType.Int); values.Add(pk_JobCId);
        //names.Add("@IsActive"); types.Add(SqlDbType.VarChar); values.Add(IsActive);
        names.Add("@xmldoc"); types.Add(SqlDbType.VarChar); values.Add(xmlDoc);
        if (Dobj.ExecuteTransactionMsg("ALM_Alumni_Jobs_Category_Mst_UPDATE", values, names, types, ref Message) > 0)
        {
            Message = Dobj.ShowMessage("U", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public DataSet Edit(int pk_sal_id)
    {
        ClearArrayLists();
        names.Add("@Pk_JobCId"); types.Add(SqlDbType.Int); values.Add(pk_sal_id);
        return Dobj.GetDataSet("ALM_Alumni_Jobs_Category_Mst_Edit", values, names, types);
    }

    public DataSet SelectAll()
    {
        ClearArrayLists();

        return Dobj.GetRecords("ALM_Alumni_Jobs_Category_Mst_SelectAll");
    }

    public void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    void Reset()
    {
        btnSave.Text = "SAVE";
        btnSave.CommandName = "SAVE";
        btnSave.TextDuringCallBack = "SAVING...";
        R_txtJobName.Text = "";
        Chk_IsActive.Checked = false;
        gvDetails.SelectedIndex = -1;
        gvDetails.PageIndex = 0;
        Fill_Grid();
        lblMsg.Text = "";
        btnSave.Enabled = true;
    }

    #endregion
}