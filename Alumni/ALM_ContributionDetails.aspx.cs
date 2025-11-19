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
using CrystalDecisions.CrystalReports.Engine;

public partial class Alumni_ALM_ContributionDetails : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    ReportDocument rptdoc = new ReportDocument();
    private Boolean IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        //IsPageRefresh = false;
        if (!IsPostBack)
        {
            ViewState["postid"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postid"];
            //Viewfee_Receipts();
            TransactionHistory();
        }
        //else
        //{
        //    if (ViewState["postid"].ToString() != Session["postid"].ToString())
        //    {
        //        IsPageRefresh = true;
        //    }
        //    Session["postid"] = System.Guid.NewGuid().ToString();
        //   ViewState["postid"] = Session["postid"];
        // }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        if (rptdoc != null)
        {
            rptdoc.Close();
            rptdoc.Dispose();
        }
        rptdoc = null;
        //GC.Collect();
    }

    public void TransactionHistory()
    {
        DataSet gv_ds = ALM_GetAll_Contributors_Lists();

        if (gv_ds.Tables[0].Rows.Count > 0)
        {
            // lblMsg.Text = "";
            gvDetails.DataSource = gv_ds.Tables[0];
            gvDetails.DataBind();
        }
    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper().ToString() == "SELECT")
        {
            try
            {
                string pk_contrid = e.CommandArgument.ToString();
                ViewState["CID"] = pk_contrid;
                FillGrid(ViewState["CID"].ToString());
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                // lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
            }
            catch (Exception ex)
            {
                // lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
            }
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        //Viewfee_Receipts();
    }

    //private void bindContributors()
    //{
    //    try
    //    {
    //        DataSet ds;
    //        ds = ALM_GetAll_Contributors_Lists();
    //        contributorsRepeater.DataSource = ds.Tables[0];
    //        contributorsRepeater.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        //lblMsg.Text = ex.Message;
    //    }
    //}

    private void FillGrid(string contriID)
    {
        try
        {
            DataSet ds;
            int cid = Convert.ToInt32(contriID.ToString());
            ds = ALM_Get_Payment_History_By_ContriID(cid);
            gvConfPayHistory.DataSource = ds.Tables[0];
            gvConfPayHistory.DataBind();
            //gvConfPayHistory.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    private DataSet ALM_GetAll_Contributors_Lists()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("ALM_GetAll_Contributors_Lists", values, names, types);
    }

    private DataSet ALM_Get_Payment_History_By_ContriID(int pk_contribution_ID)
    {
        ClearArrayLists();
        names.Add("@pk_contribution_ID"); types.Add(SqlDbType.NVarChar); values.Add(pk_contribution_ID);
        return Dobj.GetDataSet("ALM_Get_Payment_History_By_ContriID", values, names, types);
    }

    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }
}