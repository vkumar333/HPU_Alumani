using DataAccessLayer;
using SubSonic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Alumni_ALM_Gallery : System.Web.UI.Page
{
    #region Global Declaration
    crypto cpt = new crypto();
    #endregion

    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    private string check;

    public string Check
    {
        get { return check; }
    }

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable GetImage()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GellaryRecords", values, names, types);
    }

    public static StoredProcedure GetImageOnGrpId(int grpid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_GetGellaryRecords_ByGrpId", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@grpid", grpid, DbType.Int32);
        return sp;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ImagesRepeter();
        }
    }

    private void ImagesRepeter()
    {
        //DataTable dt = new DataTable();
        //dt = GetImage();
        //if (dt.Rows.Count > 0)
        //{
        //    GalleryImages.DataSource = dt;
        //    GalleryImages.DataBind();
        //}

        string ss = Request.QueryString["Groupid"].ToString();
        string ss1 = "";

        if (ss != null)
        {
            int gid = Convert.ToInt32(cpt.DecodeString(ss));
            DataSet dsS = GetImageOnGrpId(gid).GetDataSet();
            GalleryImages.DataSource = dsS.Tables[0];
            GalleryImages.DataBind();

            lblGalleryAlbum.Text = dsS.Tables[1].Rows[0]["GroupName"].ToString();

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
    }
}