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

public partial class Alumni_EventsGallery_View : System.Web.UI.Page
{
    #region Global Declaration
	
    crypto cpt = new crypto();
	
    #endregion
	
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AlumniID"] != null)
        {
            int check = int.Parse(alm_alumni_check(int.Parse(Session["AlumniID"].ToString())).GetDataSet().Tables[0].Rows[0][0].ToString());
            if (check < 1)
            {
                Response.Redirect("../Alumin_Loginpage.aspx");
            }
        }

        if (Session["AlumniID"] != null)
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
    }

    protected void FillEvent()
    {
        DataSet ds = ALM_Event_Mst_SelForDDL().GetDataSet();
        //D_ddlEventName.DataSource = ds;
        //D_ddlEventName.DataTextField = "GroupName";
        //D_ddlEventName.DataValueField = "Pk_Groupid";
        //D_ddlEventName.DataBind();
    }

    public static StoredProcedure ALM_Event_Mst_SelForDDL()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Eavent_Mst_SelForDDL1new", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    public static StoredProcedure alm_alumni_check(int pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_alumni_check", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure GetCrateAlbum()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Alumnia_AlbumGroup", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    public string SetServiceDoc(string FileName)
    {
        string FolderName = "../UploadedImg";
        string host = HttpContext.Current.Request.Url.Host;
        string FilePath = "";
        DataSet dsFilepath = new DataSet();
        FilePath = FolderName + @"/" + FileName;
        return FilePath;
    }

    private void FillGrid()
    {
        try
        {
            DataSet ds = GetCrateAlbum().GetDataSet();
            ds.Tables[0].Columns.Add("encId");
            int rnum = (ds.Tables[0].Rows.Count) + 1;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                {
                    string pkid = ds.Tables[0].Rows[x]["Pk_Groupid"].ToString();
                    string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                    ds.Tables[0].Rows[x]["encId"] = encId;
                    ds.Tables[0].Rows[x]["PhotoDesc"] = GetShortDescription(ds.Tables[0].Rows[x]["PhotoDesc"].ToString());
                }
                galleryCoverRepeater.DataSource = ds.Tables[0];
                galleryCoverRepeater.DataBind();
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    public string GetShortDescription(string fullDescription, int maxLength = 30)
    {
        if (fullDescription.Length > maxLength)
        {
            return fullDescription.Substring(0, maxLength) + "...";
        }
        return fullDescription;
    }
}
