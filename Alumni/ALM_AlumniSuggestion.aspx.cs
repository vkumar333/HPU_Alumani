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

public partial class Alumni_ALM_AlumniSuggestion : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AlumniID"].ToString() == "" || Session["AlumniID"] == null)
        {
            Response.Redirect("~//Alumin_Loginpage.aspx");
        }
        int check = int.Parse(alm_alumni_check(int.Parse(Session["AlumniID"].ToString())).GetDataSet().Tables[0].Rows[0][0].ToString());
        if (check < 1)
        {
            Response.Redirect("~//Alumin_Loginpage.aspx");
        }
        if (!IsPostBack)
        {
            FillGrid();
            R_txtSuggession.Text = "";
        }
    }
	
    public static StoredProcedure alm_alumni_check(int pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_alumni_check", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
        //return 0;
    }

    void FillGrid()
    {
        if (Session["AlumniID"].ToString() != "")
        {
            DataSet ds = IUMSNXG_ALM.SP.ALM_AlumniSuggestion_Edit(Convert.ToInt32(Session["AlumniID"])).GetDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAlumni.DataSource = ds;
                gvAlumni.DataBind();
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        R_txtSuggession.Text = "";
        lblMsg.Text = "";
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
            if (R_txtSuggession.Text.ToString().Trim() == "" && string.IsNullOrWhiteSpace(R_txtSuggession.Text.ToString().Trim()))
            {
                ClientMessaging("Suggession can not be blank!!! ");
                R_txtSuggession.Focus();
                return;
            }

            if ((IUMSNXG_ALM.SP.ALM_AlumniSuggestion_Ins(Convert.ToInt32(Session["AlumniID"]), R_txtSuggession.Text.Trim()).Execute()) > 0)
            {
                lblMsg.Text = "Record Saved Successfully!";
                R_txtSuggession.Text = "";
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void gvAlumni_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAlumni.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Alumni/ALM_Alumni_Home.aspx");
    }
}