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

public partial class Alumni_ALM_Alumni_Achievers_Lists : System.Web.UI.Page
{
    crypto crp = new crypto();
	
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AlumniID"].ToString() == "" || Session["AlumniID"] == null)
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
        int check = int.Parse(alm_alumni_check(int.Parse(Session["AlumniID"].ToString())).GetDataSet().Tables[0].Rows[0][0].ToString());
        if (check < 1)
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
        if (!IsPostBack)
        {
            FillGrid();
            //Clear();
        }
    }
	
    public static StoredProcedure alm_alumni_check(int pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_alumni_check", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    //private void getAchieversRepeter()
    //{
    //    DataSet ds = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();

    //    if(ds.Tables[0].Rows.Count > 0)
    //    {
    //        // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\");
    //        // string[] filePaths = Directory.GetFiles("D:\\Alumni\\UploadSliders\\");
    //        galleryAchieversRepeater.DataSource = ds.Tables[0];
    //        galleryAchieversRepeater.DataBind();
    //    }
    //}

    private void FillGrid()
    {
        //DataSet ds = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();
        //if (ds.Tables[0].Rows.Count > 0)
        //    galleryAchieversRepeater.DataSource = ds.Tables[0];
        //else
        //    galleryAchieversRepeater.DataSource = null;
        //galleryAchieversRepeater.DataBind();

        crypto cpt = new crypto();
        DataSet dsA = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();
        dsA.Tables[0].Columns.Add("encId");
        int rnum = (dsA.Tables[0].Rows.Count) + 1;

        if (dsA.Tables[0].Rows.Count > 0)
        {
            for (int x = 0; x < dsA.Tables[0].Rows.Count; x++)
            {
                string pkid = dsA.Tables[0].Rows[x]["pk_alumniid"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dsA.Tables[0].Rows[x]["encId"] = encId;
            }
            galleryAchieversRepeater.DataSource = dsA.Tables[0];
            galleryAchieversRepeater.DataBind();
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Alumni/ALM_Alumni_Home.aspx");
    }
}
