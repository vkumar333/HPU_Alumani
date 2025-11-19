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
using System.Data.SqlClient;

public partial class Alumni_AlumniMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Alumnitype"] == null || Session["Alumnitype"].ToString() == "")
        {
            login.Text = "Sign up/Login";
			getSocialURLLinks();
        }
        else
        {
            login.Text = "Logout";
        }
    }
	
    protected void login_Click(object sender, EventArgs e)
    {
        if (login.Text == "Sign up/Login")
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
        else if (login.Text == "Logout")
        {
            Session.Abandon();
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
    }
	
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        login.Text = "Sign up/Login";
        Session.Abandon();
        Response.Redirect("Alm_Default.aspx");
        Session["Alumnitype"] = "";
    }
	
	#region "Get Social URL Links"

    private void getSocialURLLinks()
    {
        try
        {
            using (DataSet dsURL = getSocialMediaURLLinks().GetDataSet())
            {
                if (dsURL.Tables.Count > 0 && dsURL.Tables[0].Rows.Count > 0)
                {
                    int count = dsURL.Tables[0].Rows.Count;

                    if (dsURL.Tables[0].Rows.Count > 0)
                    {
                        facebookLink.HRef = dsURL.Tables[0].Rows[0]["facebookLink"].ToString();
                        facebookLink.Target = "_blank";
                        twitterLink.HRef = dsURL.Tables[0].Rows[0]["twitterLink"].ToString();
                        twitterLink.Target = "_blank";
                        linkedInLink.HRef = dsURL.Tables[0].Rows[0]["linkedInLink"].ToString();
                        linkedInLink.Target = "_blank";
                        youtubeLink.HRef = dsURL.Tables[0].Rows[0]["youtubeLink"].ToString();
                        youtubeLink.Target = "_blank";
                    }
                    else
                    {
                        facebookLink.HRef = "#";
                        twitterLink.HRef = "#";
                        linkedInLink.HRef = "#";
                        youtubeLink.HRef = "#";
                    }
                }
            }
        }
        catch (SqlException sqlEx)
        {
            // Handle SQL related errors here
            ClientMessaging("SQL Error: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            // Handle any other errors here
            ClientMessaging("Error: " + ex.Message);
        }
    }

    public StoredProcedure getSocialMediaURLLinks()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Social_Media_Links_Info", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }
	
    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    #endregion
}