using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UMM_CHILDCLASSES;
using System.Data;
public partial class UMM_UMM_PasswordPolicyManager : System.Web.UI.Page
{
    private Boolean IsPageRefresh = false;
    UMM_Child_UserSmtp_Dtls obj = new UMM_Child_UserSmtp_Dtls();
    protected void Page_Load(object sender, EventArgs e)
    {
        IsPageRefresh = false;
        if (!IsPostBack)
        {
            ViewState["postid"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postid"];
            Get_Password_policies();
        }
        else
        {
            if (ViewState["postid"].ToString() != Session["postid"].ToString())
            {
                IsPageRefresh = true;
            }
            Session["postid"] = System.Guid.NewGuid().ToString();
            ViewState["postid"] = Session["postid"];
        }
    }

    void Get_Password_policies()
    {
        try
        {
            DataSet ds = obj.GetPasswordPolicies();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                R_txtLength.Text = ds.Tables[0].Rows[0]["MinLength"].ToString();
                //R_txtNumeric.Text = ds.Tables[0].Rows[0]["MinNumericChar"].ToString();
                //R_txtSpecial.Text = ds.Tables[0].Rows[0]["MinSpecialChar"].ToString();
                //R_txtUpperCase.Text = ds.Tables[0].Rows[0]["MinUpperCaseChar"].ToString();

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["MinNumericChar"]) == true)
                {
                    rdoNumericYes.Checked = true;
                    rdoNumericNo.Checked = false;
                }
                else
                {
                    rdoNumericYes.Checked = false;
                    rdoNumericNo.Checked = true;
                }

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["MinSpecialChar"]) == true)
                {
                    rdoSpecialYes.Checked = true;
                    rdoSpecialNo.Checked = false;
                }
                else
                {
                    rdoSpecialYes.Checked = false;
                    rdoSpecialNo.Checked = true;
                }


                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["MinUpperCaseChar"]) == true)
                {
                    rdoUpperYes.Checked = true;
                    rdoUpperNo.Checked = false;
                }
                else
                {
                    rdoUpperYes.Checked = false;
                    rdoUpperNo.Checked = true;
                }


                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Allow_pass_equal_userid"]) == true)
                {
                    rdo_equalYes.Checked = true;
                    rdo_equalNo.Checked = false;
                }
                else
                {
                    rdo_equalYes.Checked = false;
                    rdo_equalNo.Checked = true;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!IsPageRefresh)
        {
            try
            {

                bool flagPasswordEqual, flagupper, flagspecial, flagnumeric;
                if (rdo_equalYes.Checked == true)
                    flagPasswordEqual = true;
                else
                    flagPasswordEqual = false;

                if (rdoUpperYes.Checked == true)
                    flagupper = true;
                else
                    flagupper = false;

                if (rdoSpecialYes.Checked == true)
                    flagspecial = true;
                else
                    flagspecial = false;

                if (rdoNumericYes.Checked == true)
                    flagnumeric = true;
                else
                    flagnumeric = false;

                DataSet ds = obj.SavePasswordPolicies(Convert.ToInt32(R_txtLength.Text), flagupper, flagnumeric, flagspecial, flagPasswordEqual);
                ClientMessaging("Password Policies Saved Successfully");
            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            Get_Password_policies();
        }

    }

    /// <summary>
    /// Clients the messaging.
    /// </summary>
    /// <param name="msg">The MSG.</param>
    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

}