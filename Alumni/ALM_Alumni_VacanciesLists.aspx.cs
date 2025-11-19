using SubSonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Alumni_ALM_Alumni_VacanciesLists : System.Web.UI.Page
{
    crypto cpt = new crypto();
	
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
            FillVacancciesGrid();
        }
    }

    public static StoredProcedure alm_alumni_check(int pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_alumni_check", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    private void FillVacancciesGrid()
    {
        DataSet dsV = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();
        dsV.Tables[4].Columns.Add("encId");
        int rnum = (dsV.Tables[4].Rows.Count) + 1;

        if (dsV.Tables[4].Rows.Count > 0)
        {
            for (int x = 0; x < dsV.Tables[4].Rows.Count; x++)
            {
                string pkid = dsV.Tables[4].Rows[x]["Pk_JobPostedId"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dsV.Tables[4].Rows[x]["encId"] = encId;
            }
            newsVacanciesRepeater.DataSource = dsV.Tables[4];
            newsVacanciesRepeater.DataBind();
        }
    }
}