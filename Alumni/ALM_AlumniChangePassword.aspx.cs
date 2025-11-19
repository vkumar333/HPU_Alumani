using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessLayer;
using SubSonic;

public partial class Alumni_ALM_AlumniChangePassword : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    DataAccess Dobj = new DataAccess();
	
    //crypto crp = new crypto();

    //protected void Page_PreInit()
    //{
    //    if (Session["DDOID"].ToString() != "" || Session["LocationID"].ToString() != "")
    //    {
    //       // Page.MasterPageFile = "~/UMM/MasterPage.master";
    //    }
    //    //Page.Theme = "CCSBLUE";
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        int alumniId = 0;
        //if (!IsPostBack)
        //{
        //    if (Session["DDOID"].ToString() == "" || Session["LocationID"].ToString() == "")
        //    {
        //        if (Session["AlumniID"] == "" || Session["AlumniID"] == null)
        //        {
        //            Response.Redirect("~//Alumni//Alumin_Loginpage.aspx");
        //        }
        //        else
        //        {
        //            int alumniId = 0;
        //            alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
        //            IDataReader rdr = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(alumniId).GetReader();
        //            DataTable dt = new DataTable();
        //            dt.Load(rdr);
        //            rdr.Close(); rdr.Dispose();
        //            if (dt.Rows.Count != 0)
        //            {
        //                if (dt.Rows[0]["password"].ToString() != "")
        //                {
        //                    //R_txtOldPassword.Attributes.Add("value", crp.Decrypt(dt.Rows[0]["password"].ToString()));
        //                    //R_txtRPassword.Text = crp.Decrypt(dt.Rows[0]["password"].ToString());
        //                }
        //                else
        //                {
        //                    Client_Messaging("Please Create Your Password");
        //                }
        //            }
        //        }
        //    }
        //}
        if (Session["AlumniID"].ToString() == null || Session["AlumniID"].ToString() == "")
            alumniId = Convert.ToInt32(Session["EmpView_AlumniID"].ToString());
        else
            alumniId = Convert.ToInt32(Session["AlumniID"].ToString());

        DataSet ds = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(alumniId).GetDataSet();
        if (ds.Tables[0].Rows[0]["password"].ToString() != "")
        {
            R_txtOldPassword.Attributes.Add("value", ds.Tables[0].Rows[0]["password"].ToString());
            R_txtOldPassword.Text = ds.Tables[0].Rows[0]["password"].ToString();
        }

    }

    // crypto crypt = new crypto();
    //Show Blank Validation Message
    protected void Client_Messaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected bool CheckValid()
    {
        if (R_txtPwd.Text.Trim() == "")
        {
            String script = String.Format("alert('{0}');", "Enter new password");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
            R_txtPwd.Focus();
            return false;
        }
        if (R_txtRPassword.Text.Trim() == "")
        {
            String script = String.Format("alert('{0}');", "Confirm password");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);

            R_txtRPassword.Focus();
            return false;
        }

        //string oldpass = crypt.Encrypt(CommonFunction.ReturnTextifNotBlank(R_txtOldPassword));
        string oldpass = CommonFunction.ReturnTextifNotBlank(R_txtOldPassword);
        IDataReader rdr = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(Session["AlumniID"].ToString())).GetReader();
        DataTable dt = new DataTable();
        dt.Load(rdr);
        rdr.Close(); rdr.Dispose();
        if (dt.Rows.Count > 0)
        {
            ViewState["Pass"] = dt.Rows[0]["Password"].ToString();
        }
        else
        {
            String script = String.Format("alert('{0}');", "Cannot change your password contact to your administrator");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);

            return false;
        }

        if (oldpass == ViewState["Pass"].ToString())
        {
            return true;
        }
        else
        {
            String script = String.Format("alert('{0}');", "Old password entered was incorrect");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
            R_txtOldPassword.Focus();
            return false;
        }
        return false;
    }

    public static StoredProcedure ALM_SP_AlumniRegistration_ChangePwd1(string doc)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_ChangePwd1", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@doc", doc, DbType.String);
        return sp;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckValid() == true)
        {
            int queryreturn = 0;
            //string pass=(crypt.Encrypt(R_txtRPassword.Text.Trim())).ToString();
            string pass = (R_txtRPassword.Text.Trim().ToString());
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AlumniID", typeof(int)));
            dt.Columns.Add(new DataColumn("pass", typeof(string)));
            DataRow row = dt.NewRow();
            row["pass"] = pass;
            row["AlumniID"] = Convert.ToInt32(Session["AlumniID"].ToString());
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            string doc = ds.GetXml();
            queryreturn = ALM_SP_AlumniRegistration_ChangePwd1(doc).Execute();
            if (queryreturn > 0)
            {
                String script = String.Format("alert('{0}');", "Password changed successfully");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
            }
        }
        else
        {
            return;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        R_txtRPassword.Text = "";
        R_txtPwd.Text = "";
        R_txtOldPassword.Text = "";

    }
}