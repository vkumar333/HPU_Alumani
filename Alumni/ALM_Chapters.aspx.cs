using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using SubSonic;

public partial class Alumni_ALM_Chapters : System.Web.UI.Page
{
    crypto cpt = new crypto();

    DataAccess DAobj = new DataAccess();
    CustomMessaging msgObj = new CustomMessaging();

    ArrayList names = new ArrayList();
    ArrayList types = new ArrayList();
    ArrayList values = new ArrayList();

    private Boolean IsPageRefresh = false;

    public void ClearArrayLists()
    {
        names.Clear(); types.Clear(); values.Clear();
    }

    public string xmlDoc { get; set; }
    public int mode { get; private set; }
    public int chapterID { get; set; }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindChapters();
        }
    }

    protected void BindChapters()
    {
        try
        {
            DataSet dsS = ALM_Chapters_Mst_CRUD_Operations(xmlDoc, 5, 0, null).GetDataSet();
            dsS.Tables[0].Columns.Add("encId");

            if (dsS != null && dsS.Tables.Count > 0 && dsS.Tables[0].Rows.Count > 0)
            {
                for (int x = 0; x < dsS.Tables[0].Rows.Count; x++)
                {
                    string pkid = dsS.Tables[0].Rows[x]["pk_ChapterID"].ToString();
                    string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                    dsS.Tables[0].Rows[x]["encId"] = encId;
                }
                rpChapters.DataSource = dsS.Tables[0];
                rpChapters.DataBind();
            }
            else
            {
                rpChapters.DataSource = null;
                rpChapters.DataBind();
                ClientMessaging("No Record Found.");
            }
        }
        catch (Exception ex)
        {
            //throw;
            ClientMessaging(ex.Message);
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    #region "Stored Procedures"

    public static StoredProcedure ALM_Chapters_Mst_CRUD_Operations(string xmldoc, int mode, int ChapterID, string actionBy)
    {
        SubSonic.StoredProcedure sp = new StoredProcedure("ALM_Chapters_Mst_CRUD", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@xmlDoc", xmldoc, DbType.String);
        sp.Command.AddParameter("@mode", mode, DbType.Int32);
        sp.Command.AddParameter("@ChapterID", ChapterID, DbType.Int32);
        sp.Command.AddParameter("@ActionBy", actionBy, DbType.String);
        return sp;
    }

    #endregion

    protected void lnkChapter_Click(object sender, EventArgs e)
    {
        Anthem.LinkButton button = (sender as Anthem.LinkButton);
        string ChapterID = button.CommandArgument;
        RepeaterItem item = button.NamingContainer as RepeaterItem;
        int index = item.ItemIndex;
        ViewState["ChapterID"] = ChapterID;
    }

    protected void lnkNewRegs_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Alumni/ALM_AlumniRegistration.aspx");
    }

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
                string Query = "";
                string a = "";
                if (Session["pk_alumniid"] != null)
                {
                    a = Session["pk_alumniid"].ToString();
                }

                if (!string.IsNullOrEmpty(a))
                {
                    Query = "~//Alumni//ALM_Chapterwise_Alumnis.aspx?ID=" + ViewState["ChapterID"].ToString();
                }
                else
                {
                    Query = "~//Alumni//ALM_Chapterwise_Alumnis.aspx?ID=" + ViewState["ChapterID"].ToString();
                }
                Response.Redirect(Query);
            }
            else
            {
                Anthem.Manager.IncludePageScripts = true;
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