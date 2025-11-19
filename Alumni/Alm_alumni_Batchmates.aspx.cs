using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using System.Data.SqlClient;
using SubSonic;

public partial class Alumni_Alm_alumni_Batchmates : System.Web.UI.Page
{
    crypto crp = new crypto();
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    /// <summary>
    /// Sp Section Called
    /// </summary>
    /// <returns></returns>
    private DataTable Getmemberbyyear()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GetMemberbyGroup", values, names, types);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ImagesRepeter();
        getSocialURLLinks();
    }

    /// <summary>
    /// Bind image Repeater
    /// </summary>
    private void ImagesRepeter()
    {
        DataTable dt = new DataTable();
        dt = Getmemberbyyear();
        dt.Columns.Add("encId");
        int rnum = (dt.Rows.Count) + 1;

        if (dt.Rows.Count > 0)
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                string pkid = dt.Rows[x]["YearofPassing"].ToString();
                string encId = crp.EncodeString(Convert.ToInt32(pkid));
                dt.Rows[x]["encId"] = encId;
            }
            GropbyYear.DataSource = dt;
            GropbyYear.DataBind();
        }
    }

    /// <summary>
    /// redirect Page and send value using query string 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkView_Click(object sender, EventArgs e)
    {
        string a = crp.Encrypt(((LinkButton)sender).CommandArgument.ToString());
        a = a.Replace("+", "%2B");
        Response.Redirect("~//Alumni//Alm_Alumni_Batchmates_member.aspx?id=" + a + "");
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