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
using DataAccessLayer;
//using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Drawing;
using SubSonic;
using System.Text;
using System.Net.Mail;
using System.Data.SqlClient;

public partial class Alumni_ALM_AlumniChangeMobileNo : System.Web.UI.Page
{

    #region "Common Objects"
    DataAccess Dobj = new DataAccess();
  //  crypto crp = new crypto();
    CustomMessaging eobj = new CustomMessaging();
    CommonFunction cfObj = new CommonFunction();
    #endregion
    private bool IspageReferesh = false;
    #region "Page Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["postid"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postid"].ToString();
            if (Session["Alumnino"] != null)
                Clear();

        }
        else
        {
            if (ViewState["postid"].ToString() != Session["postid"].ToString())
            {
                IspageReferesh = true;
            }
            Session["postid"] = System.Guid.NewGuid().ToString();
            ViewState["postid"] = Session["postid"];
        }


    }
    #endregion
    #region "Bind Registration No."
    void Bind_RegNo()
    {
        if (Session["Alumnino"] != null || Session["Alumnino"].ToString() != "")
        {
            R_txtRegNo.Text = Session["Alumnino"].ToString();
        }
        

    }
    #endregion

    #region "Save Event and Method"
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IspageReferesh)
            {
                Save();
            }
        }
        catch (SqlException exp)
        {
            lblMsg.Text = eobj.ShowSQLErrorMsg(exp.Message.ToString(), "", exp);
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    private void Save()
    {

        if ((IUMSNXG.SP.Alm_Alumni_ChangeMobileNo_Request_ins_sel(Convert.ToInt32(Session["AlumniID"].ToString()), R_txtNewMobileNo.Text.Trim(), "I").Execute()) > 0)
        {
            Client_Messaging("Request is sent Successfully!");
            Clear();
        }
    }
    #endregion


    #region "Common Methods and Reset Event"
    private void Clear()
    {
        lblMsg.Text = "";
        Bind_RegNo();
        R_txtNewMobileNo.Text = "";
        Anthem.Manager.IncludePageScripts = true;
        gvDetails.DataSource = null;
        gvDetails.DataBind();
        R_txtNewMobileNo.Focus();
       
        BindGridVeiw();
        gvDetails.SelectedIndex = -1;


    }
    protected void Client_Messaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        Clear();
       
    }
   
    #endregion
    #region "Check Mobile No Duplicacy"
    protected void R_txtNewMobileNo_TextChanged(object sender, EventArgs e)
    {
        lblMobleNoMsg.Text = "";
        if (R_txtNewMobileNo.Text.Trim() != "")
        {
            DataSet ds = IUMSNXG.SP.ALM_SP_GetDuplicate_Email_or_MobileNo("", R_txtNewMobileNo.Text.Trim()).GetDataSet();
            if (ds.Tables[1].Rows.Count > 0)
            {
                lblMobleNoMsg.Text = "Already Exists!";
                R_txtNewMobileNo.Text = "";
                Anthem.Manager.IncludePageScripts = true;
                R_txtNewMobileNo.Focus();
                return;
            }
        }
    }
    #endregion

    #region "GridViewBind and Page Change Event"
    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        BindGridVeiw();
    }

    private void BindGridVeiw()
    {
        gvDetails.DataSource = null;
        gvDetails.DataBind();
        DataSet ds = IUMSNXG.SP.Alm_Alumni_ChangeMobileNo_Request_ins_sel(Convert.ToInt32(Session["AlumniID"].ToString()), "", "S").GetDataSet();
        {
            if(ds.Tables[0].Rows.Count>0)
            {
                gvDetails.DataSource = ds.Tables[0];
                gvDetails.DataBind();
            }
            else
            {
                gvDetails.DataSource = null;
                gvDetails.DataBind();
            }
        }
    }
    #endregion
}