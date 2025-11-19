using DataAccessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Alumni_Alm_NewsandEvents_pors : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable GetDetailsByid(int Pk_Stories_id)
    {
        ClearArrayLists();
        names.Add("@Pk_Stories_id"); types.Add(SqlDbType.NVarChar); values.Add(Pk_Stories_id);
        return Dobj.GetDataTable("ALM_Show_Stories_Details_by_id", values, names, types);
    }

    private DataTable NewsDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GetNewsNdstoriess", values, names, types);
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        EventsRepeter();
        LatestNewsandStoriesRepeter();
        if (Request.QueryString["ID"] != null)
        {
            ////int Pk_Stories_id = Convert.ToInt32(Request.QueryString["ID"].ToString());
            ////ViewState["Pk_Stories_id"] = Request.QueryString["ID"].ToString();
            ////if (Pk_Stories_id != 0)
            ////{
            ////    Repeterid(Pk_Stories_id);
            ////}

            crypto cpt = new crypto();
            string decId = cpt.DecodeString(Request.QueryString["ID"].ToString());
            int Pk_Stories_id = Convert.ToInt32(decId);

            if (Pk_Stories_id != 0)
            {
                Repeterid(Pk_Stories_id);
            }
        }
    }

    private void EventsRepeter()
    {
        DataTable dt = new DataTable();
        dt = NewsDetails();
           
        if (dt.Rows.Count > 0)
        {
            RepeventsAll.DataSource = dt;
            RepeventsAll.DataBind();
        }
    }

    private void Repeterid(int Pk_Stories_id)
    {
        DataTable dt = new DataTable();
        dt = GetDetailsByid(Pk_Stories_id);
        if (dt.Rows.Count > 0)
        {
            RepeventsAll.DataSource = dt;
            RepeventsAll.DataBind();
        }
    }

    private void LatestNewsandStoriesRepeter()
    {
        DataTable dt = new DataTable();
        dt = NewsDetails();
        {
            //RepLatestnews.DataSource = dt;
            //RepLatestnews.DataBind();
        }
    }
}