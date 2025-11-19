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
using IUMSNXG;
using System.Threading.Tasks;



public partial class UMM_mysettings : System.Web.UI.Page
{
    UMM_CHILDCLASSES.UMM_Child_UserSmtp_Dtls obj = new UMM_CHILDCLASSES.UMM_Child_UserSmtp_Dtls();
    crypto crypt = new crypto();

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Menu tr;
        tr = (Menu)Master.FindControl("MainMenu");
        tr.Visible = false;

        if (!IsPostBack)
        {
            BindSmtpDropDown();
            BindUserSmtpSettings();
        }
    }

    /// <summary>
    /// Bind the smtp dropdown
    /// </summary>
    void BindSmtpDropDown()
    {
        try
        {
            DataTable dt = CRUD_BASECLASS.GetSTable<UMM_CHILDCLASSES.UMM_Child_UserSmtp_Dtls>(obj);
            D_ddlSmtp.DataSource = dt;
            D_ddlSmtp.DataTextField = "SmtpAdd";
            D_ddlSmtp.DataValueField = "Pk_SmtpId";
            D_ddlSmtp.DataBind();
            D_ddlSmtp.Items.Insert(0, new ListItem("-- Select --", "0"));
            //foreach (DataRow dr in dt.Rows)
            //{
            //    D_ddlSmtp.Items.Add(new ListItem(dr["SmtpAdd"].ToString(), dr["Pk_SmtpId"].ToString()));
            //}

            //D_ddlSmtp.Items.Insert(0, "-- Select --");
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    /// <summary>
    /// Bind the user smtp settings
    /// </summary>
    void BindUserSmtpSettings()
    {
        lblMsg.Text = "";

        try
        {
            obj.pk_userId = Session["UserID"].ToString();
            DataTable dt = CRUD_BASECLASS.EditRecDatatable<UMM_CHILDCLASSES.UMM_Child_UserSmtp_Dtls>(obj);
            D_ddlSmtp.SelectedValue = dt.Rows[0]["Fk_SmtpId"].ToString();
            E_txtEmail.Text = dt.Rows[0]["email"].ToString();
            //R_txtPassword.Attributes.Add("value", dt.Rows[0]["smtppassword"].ToString());

            if (!string.IsNullOrEmpty(dt.Rows[0]["smtppassword"].ToString()))
                R_txtPassword.Attributes.Add("value", crypt.Decrypt(dt.Rows[0]["smtppassword"].ToString()));
            Anthem.Manager.IncludePageScripts = true;
            D_ddlSmtp.Focus();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;

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

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        try
        {
            if (D_ddlSmtp.SelectedIndex > 0 && E_txtEmail.Text.Trim() != "" && R_txtPassword.Text != "")
            {
                obj.pk_userId = Session["UserID"].ToString();
                obj.Fk_SmtpId = Convert.ToInt32(D_ddlSmtp.SelectedValue);
                obj.email = E_txtEmail.Text.Trim();
                //obj.smtppassword = R_txtPassword.Text;
                obj.smtppassword = crypt.Encrypt(CommonFunction.ReturnTextifNotBlank(R_txtPassword));

                DataTable dt = CRUD_BASECLASS.UpdateRec<UMM_CHILDCLASSES.UMM_Child_UserSmtp_Dtls>(obj);

                if (dt.Rows.Count > 0)
                {
                    ClientMessaging("Settings Updated Successfully!");
                    BindUserSmtpSettings();
                }
                else
                {
                    ClientMessaging("Some Error Occurred!");
                }
            }
            else
            {
                Anthem.Manager.IncludePageScripts = true;
                D_ddlSmtp.Focus();
            }
        }
        catch (System.Data.SqlClient.SqlException exsql)
        {
            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(exsql.Message);

        }
        catch (Exception ex)
        {
            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);

        }
    }
}