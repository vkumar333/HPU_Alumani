using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using DataAccessLayer;
using SubSonic;

public partial class Alumni_ALM_EventsGallary_SlideShow : System.Web.UI.Page
{
    #region Global Declaration
	
    crypto cpt = new crypto();
	
    #endregion

    private string check;

    public string Check
    {
        get { return check; }
    }

    public static StoredProcedure ALM_Uni_AlbumPhoto_SelOnGrpId(int grpid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Uni_AlbumPhoto_SelOnGrpId", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@grpid", grpid, DbType.Int32);
        return sp;
    }
	
    public static StoredProcedure ALM_AlbumGetGroup()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_AlbumGetGroup", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string ss = Request.QueryString["Groupid"].ToString();
            string ss1 = "";
            if (ss != null)
            {
                int gid = Convert.ToInt32(cpt.DecodeString(Request.QueryString["Groupid"].ToString()));
                galleryCoverRepeater.DataSource = ALM_Uni_AlbumPhoto_SelOnGrpId(gid).GetDataSet();
                galleryCoverRepeater.DataBind();
                try
                {
                    ss1 = Request.QueryString["pid"].ToString();
                }
                catch
                {

                }
                if (Convert.ToString(ss1).Trim() != "")
                {
                    check = Convert.ToString(ss1).Trim();
                }
                else
                    check = "0";
            }
            //DLAlbums.DataSource = ALM_AlbumGetGroup().GetDataSet();
            //DLAlbums.DataBind();
        }
    }
}