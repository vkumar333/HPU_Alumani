/*
Created By : Arunesh Singh
Dated : 20th Oct,2018
*/
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
using DataAccessLayer;

public partial class UMM_ChangePassword : System.Web.UI.Page
{
    UserAuthorization UAobj = new UserAuthorization();
    crypto crypt = new crypto();
    UMM_Child_UserSmtp_Dtls obj = new UMM_Child_UserSmtp_Dtls();
    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList();
    ArrayList values = new ArrayList();
    ArrayList types = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.DefaultButton = this.btnSave.UniqueID;
        UAobj.useridvalue = Session["UserID"].ToString();
        DataRow dr = UAobj.GetPassword();
        if (dr == null)
        {
            Anthem.Manager.IncludePageScripts = true;
            String script = String.Format("alert('{0}');", "Cannot change your password contact to your administrator!");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
        }
        else
        {
            username.Value = dr["loginname"].ToString();
        }
     }

    protected bool CheckValid()
    {
        if (R_txtOldPassword.Text.Trim() == "")
        {
            ClientMessaging("Old Password is required!");
            R_txtPwd.Focus();
            return false;
        }
        if (R_txtPwd.Text.Trim() == "")
        {
            ClientMessaging("New Password is required!");
            R_txtPwd.Focus();
            return false;
        }
        if (R_txtRPassword.Text.Trim() == "")
        {
            ClientMessaging("Confirm New Password is required!");
            R_txtRPassword.Focus();
            return false;
        }

        DataRow dr = UAobj.GetPassword();
        if (!dr.IsNull("password"))
        {
            string salt = dr["Salt"].ToString();//Create Salt of Size 8
            string hashpass = CreatePasswordHash(CommonFunction.ReturnTextifNotBlank(R_txtOldPassword), salt);
            if (hashpass == dr["password"].ToString().ToUpper())
                return true;
            else
            {
                ClientMessaging("Old password entered was incorrect");
                R_txtOldPassword.Focus();
                return false;
            }
        }
        return false;
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
        //string saltAndPwd = String.Concat(pwd, salt);
        string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "sha1");
        return hashedPwd;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckValid() == true)
        {
            string Message = "";
            int queryreturn = 0;
            string UserId = Session["UserID"].ToString();
            //string hashpass = CreatePasswordHash(CommonFunction.ReturnTextifNotBlank(R_txtPwd), salt);
            string Password = hash.Value;
            string Salt = CreateSalt(8);//Create Salt of Size 8
            string Plain_Password = R_txtPwd.Text.Trim();
            queryreturn = ChangePassword(UserId,Password,Salt,Plain_Password,ref Message);
            if (queryreturn > 0)
                {
                clear();
                ClientMessaging("Password changed successfully");
                }
            else
            {
                ClientMessaging(Message);
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    private void clear()
    {
        R_txtOldPassword.Text = "";
        R_txtPwd.Text = "";
        R_txtRPassword.Text = "";
    }
    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }
    protected void ClearArrayLists()
    {
        names.Clear();
        values.Clear();
        types.Clear();
    }
    public int ChangePassword(string UserId, string Password,string Salt,string Plain_Password, ref string Message)
    {
        ClearArrayLists();
        names.Add("@UserID"); values.Add(UserId); types.Add(SqlDbType.VarChar);
        names.Add("@NewPwd"); values.Add(Password); types.Add(SqlDbType.VarChar);
        names.Add("@Salt"); values.Add(Salt); types.Add(SqlDbType.Image);
        names.Add("@NewPwdText"); values.Add(Plain_Password); types.Add(SqlDbType.VarChar);
        if (DAobj.ExecuteTransactionMsg("UM_Users_ChangePassword", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
