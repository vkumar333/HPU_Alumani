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
using UMMBusinessLayer;
using UMM_CHILDCLASSES;
using System.Security.Cryptography;
using System.Text;


public partial class UMM_SetNewpassword : System.Web.UI.Page
{
    UserAuthorization UAobj = new UserAuthorization();
    crypto crypt = new crypto();
    UMM_Child_UserSmtp_Dtls obj = new UMM_Child_UserSmtp_Dtls();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            clear();
        }
    }

    Boolean Check_Password_policies()
    {

        Boolean flag = false;
        DataSet ds = obj.GetPasswordPolicies();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            //Check Min. Length Policy
            if (R_txtPwd.Text.Trim().Length >= Convert.ToInt32(ds.Tables[0].Rows[0]["MinLength"]))
            {
                flag = true;
            }
            else
            {
                String script = String.Format("alert('{0}');", "New password should have minimum " + Convert.ToInt32(ds.Tables[0].Rows[0]["MinLength"]) + " Characters");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                return false;
            }
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["MinNumericChar"]) == true)
            {
                flag = false;
                //Check minimum number of Numeric values in password
                char[] s = R_txtPwd.Text.ToCharArray();


                for (int x = 0; x < s.Length; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        if (s[x].ToString() == y.ToString())
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (flag == false)
                {
                    String script = String.Format("alert('{0}');", "New password should Contain Numeric Value");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                    return false;
                }
            }

            //Check Special Character
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["MinSpecialChar"]) == true)
            {
                flag = false;
                string sss = @"~'!@#$%^&*()-+={[}]|\\:;\'<,>.?/\\""";
                char[] t = sss.ToCharArray();
                int indexOf = R_txtPwd.Text.Trim().IndexOfAny(t);
                if (indexOf == -1)
                {
                    flag = false;
                }
                else
                    flag = true;

                if (flag == false)
                {
                    String script = String.Format("alert('{0}');", "New password should Contain Special Character");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                    return false;
                }
            }


            //Check uppercase
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["MinUpperCaseChar"]) == true)
            {
                flag = false;
                foreach (char c in R_txtPwd.Text)
                {
                    if (char.IsUpper(c))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    String script = String.Format("alert('{0}');", "New password should Contain Upper Case Character");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                    return false;
                }
            }


            ////Check userid=password or not
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Allow_pass_equal_userid"]) == true)
            {
                flag = false;
                if (Session["Userid"].ToString() == R_txtPwd.Text.Trim())
                {
                    flag = true;
                }
                else
                {
                    String script = String.Format("alert('{0}');", "New password should be equal to user id");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                    return false;
                }
            }



        }
        return flag;
    }



    protected bool CheckValid()
    {
        if (R_txtPwd.Text.Trim() == "")
        {
            Anthem.Manager.IncludePageScripts = true;
            String script = String.Format("alert('{0}');", "User Name is required!");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
            R_txtPwd.Focus();
            return false;
        }
        if (R_txtPwd.Text.Trim() == "")
        {
            Anthem.Manager.IncludePageScripts = true;
            String script = String.Format("alert('{0}');", " New Password is required!");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
            R_txtPwd.Focus();
            return false;
        }
        return true;
    }

    private static string CreateSalt(int size)
    {
        //Generate a cryptographic random number.
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buff = new byte[size];
        rng.GetBytes(buff);
        // Return a Base64 string representation of the random number.
        return Convert.ToBase64String(buff);
    }

    public static string CreatePasswordHash(string pwd, string salt)
    {
        string saltAndPwd = String.Concat(pwd, salt);
        string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
        return hashedPwd;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckValid())
        {
            if (1 == 1)//Check_Password_policies() == true)
            {
                UMMBusinessLayer.LoginPage LPobj = new UMMBusinessLayer.LoginPage();
                LPobj.loginidvalue = CommonFunction.ReturnTextifNotBlank(R_txtusername);
                DataTable dt = LPobj.UserValidation();
                if (dt.Rows.Count > 0)
                {
                    int queryreturn = 0;
                    UAobj.useridvalue =dt.Rows[0]["pk_userid"].ToString();
                    string salt = CreateSalt(8);//Create Salt of Size 8
                    string hashpass = CreatePasswordHash(CommonFunction.ReturnTextifNotBlank(R_txtPwd), salt);

                    UAobj.newpwd = hashpass;
                    UAobj.newsalt = salt;
                    queryreturn = UAobj.ChangePassword();

                    if (queryreturn > 0)
                    {
                        Anthem.Manager.IncludePageScripts = true;
                        String script = String.Format("alert('{0}');", "Password changed successfully");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);

                    }
                }
                else
                {
                    Anthem.Manager.IncludePageScripts = true;
                    String script = String.Format("alert('{0}');", "Invalid User Name!");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                }
            }
            else
            {
                Anthem.Manager.IncludePageScripts = true;
                String script = String.Format("alert('{0}');", "New Password does not match with policy");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    private void clear()
    {
        R_txtPwd.Text = "";
        R_txtusername.Text = "";
    }
}
