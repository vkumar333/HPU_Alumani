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
using SubSonic;
using System.Data.SqlClient;

public partial class Alumni_ALM_Notable_Details : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    crypto cpt = new crypto();

    public object description { get; private set; }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable NotableDetails(int ID)
    {
        ClearArrayLists();
        names.Add("@PK_NAID"); types.Add(SqlDbType.NVarChar); values.Add(ID);
        return Dobj.GetDataTable("ALM_NotableAlumni_Details_By_ID", values, names, types);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            crypto cpt = new crypto();
            string decId = cpt.DecodeString(Request.QueryString["ID"].ToString());
            int pk_id = Convert.ToInt32(decId);

            if (pk_id != 0)
            {
                rptNotableAlumni(pk_id);
                getSocialURLLinks();
            }
        }
    }

    private void rptNotableAlumni(int pkID)
    {
        DataTable dt = new DataTable();
        dt = NotableDetails(pkID);
        if (dt.Rows.Count > 0)
        {
            string fileName = "";

            fileName = dt.Rows[0]["PicUrl"].ToString().Trim();

            if (fileName != "")
            {
                if (dt.Rows[0]["PicUrl"].ToString() != "None")
                {
                    Imge.Src = dt.Rows[0]["PicUrl"].ToString();
                }
            }
            else
            {
                Imge.Src = "~/alumni/stuimage/No_image.png";
            }

            lblName.Text = dt.Rows[0]["Name"].ToString();
            lblSubHeading.Text = dt.Rows[0]["SubHeading"].ToString();
            lblDescription.Text = dt.Rows[0]["Comments"].ToString();

            string websiteLink = string.Empty;

            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["WebsiteLinks"].ToString()))
            {
                websiteLink = dt.Rows[0]["WebsiteLinks"].ToString();
                //lnkWebsite.NavigateUrl = websiteLink;
                //lnkWebsite.Text = "Visit Website Link";
                //lnkWebsite.Visible = true;
                //lnkWebsite.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0099ff");
                //lnkWebsite.Font.Bold = true;
                anchorLink.HRef = websiteLink;
            }
            else
            {
                //lnkWebsite.NavigateUrl = websiteLink;
                //lnkWebsite.Text = "";
                //lnkWebsite.Visible = false;
                anchorLink.HRef = "";
            }
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    private int GetCurrentUserId()
    {
        int alumniuser = Convert.ToInt32(Session["AlumniID"].ToString());
        return alumniuser;
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

    #endregion
}