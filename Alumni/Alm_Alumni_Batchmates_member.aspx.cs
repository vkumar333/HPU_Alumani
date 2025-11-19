using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using Anthem;
using System.Web.UI.WebControls;
using DataAccessLayer;
using System.Data.SqlClient;
using SubSonic;

public partial class Alumni_Alm_Alumni_Batchmates_member : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    crypto crp = new crypto();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    /// <summary>
    /// Sp of ImagesRepeter
    /// </summary>
    /// <param name="yesrofpassing"></param>
    /// <returns></returns>
    private DataTable Getmemberbyyear(string yesrofpassing)
    {
        ClearArrayLists();
        names.Add("@yearofpassing"); types.Add(SqlDbType.NVarChar); values.Add(yesrofpassing);
        return Dobj.GetDataTable("ALM_GetMemberbyGroupss", values, names, types);
    }

    /// <summary>
    /// Pageload
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                string yesrofpassing = crp.DecodeString(Request.QueryString["ID"].ToString());
                Session["yesrofpassing"] = yesrofpassing;
                if (yesrofpassing != "0")
                {
                    ImagesRepeter(yesrofpassing);
                    getSocialURLLinks();
                }
            }
        }
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

    /// <summary>
    /// Repeater for get Image and  subject Details
    /// </summary>
    /// <param name="yesrofpassing"></param>
    private void ImagesRepeter(string yesrofpassing)
    {
        DataTable dt = new DataTable();
        dt = Getmemberbyyear(yesrofpassing);
        Session["Year"] = dt.Rows[0]["yearofpassing"].ToString();
        if (dt.Rows.Count > 0)
        {
            GropbyYear.DataSource = dt;
            GropbyYear.DataBind();
        }
    }

    /// <summary>
    /// Message Pop-up
    /// </summary>
    /// <param name="msg"></param>
    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    /// <summary>
    /// Button click for get subject id in Pop-up
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Click(object sender, EventArgs e)
    {
        //Get the reference of the clicked button.
        Anthem.LinkButton button = (sender as Anthem.LinkButton);

        //Get the command argument
        string commandArgument = button.CommandArgument;

        //Get the Repeater Item reference
        RepeaterItem item = button.NamingContainer as RepeaterItem;

        //Get the repeater item index
        int index = item.ItemIndex;
        Session["subjectid"] = commandArgument;
    }

    /// <summary>
    /// Pop-up Login Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (R_txtLogin.Text.Trim() == "")
            {
                ClientMessaging("Please Enter User ID");
                String script = String.Format("alert('{0}');", "Please Enter Login Name!");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                R_txtLogin.Focus();

                return;
            }
            if (R_txtPass.Text.Trim() == "")
            {
                ClientMessaging("Please Enter Password");
                String script = String.Format("alert('{0}');", "Please Enter Password!");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                R_txtPass.Focus();

                return;
            }
            DataSet ds = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_LoginAuthentication(R_txtLogin.Text.Trim(), R_txtPass.Text.Trim()).GetDataSet();
            if (ds.Tables[0].Rows[0]["cnt"].ToString() != "0")
            {
                string type = "P";
                Session["AlumniID"] = ds.Tables[1].Rows[0]["pk_alumniid"].ToString();
                Session["Alumnino"] = ds.Tables[1].Rows[0]["alumnino"].ToString();
                Session["AlumniName"] = ds.Tables[1].Rows[0]["alumni_name"].ToString();
                Session["Alumnitype"] = type.ToString();
                string a = Session["subjectid"].ToString();
                string b = "";// Session["degree"].ToString();
                string c = Session["yesrofpassing"].ToString();

                //a = a.Replace("+", "%2B");
                //b = b.Replace("+", "%2B");

                string subjID = string.Empty; string passYear = string.Empty;

                if (Session["subjectid"] != null )
                {
                    subjID = crp.EncodeString(Convert.ToInt32(Session["subjectid"].ToString()));
                }
                else
                {
                    subjID = "";
                }

                if (Session["yesrofpassing"] != null)
                {
                    passYear = crp.EncodeString(Convert.ToInt32(Session["yesrofpassing"].ToString()));
                }
                else
                {
                    passYear = "";
                }

                //string Query = "~//Alumni//Alm_alumni_Batchmetes_ShortProfile.aspx?degree=" + b.ToString() + "&subject=" + a.ToString() + "&Year=" + c.ToString();

                string Query = "~//Alumni//Alm_alumni_Batchmetes_ShortProfile.aspx?degree=" + b.ToString() + "&subject=" + subjID.ToString() + "&Year=" + passYear.ToString();

                Response.Redirect(Query);
            }
            else
            {
                Manager.IncludePageScripts = true;
                ClientMessaging("Invalid User Name or Password! Please contact your college for approval");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "id", "ShowRoomDetails()", true);
                Session["AlumniID"] = "";
                Session["Alumnino"] = "";
                Session["AlumniName"] = "";
                Session["Emailid"] = "";
            }
        }
        catch (Exception ex)
        {
            R_txtLogin.Text = "";
            R_txtPass.Text = "";
            R_txtLogin.Focus();
        }
    }
}