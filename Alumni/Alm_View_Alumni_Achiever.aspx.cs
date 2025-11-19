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

public partial class Alumni_Alm_View_Alumni_Achiever : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    public object description { get; private set; }
    public int pk_alumniid { get; set; }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }
    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable GetAcheiversDetailss(int alumniid)
    {
        ClearArrayLists();
        names.Add("@pk_alumniid"); types.Add(SqlDbType.NVarChar); values.Add(alumniid);
        return Dobj.GetDataTable("ALM_Alumni_Acheivers_Get", values, names, types);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["id"] != null)
        //{
        //    int pk_alumniid = Convert.ToInt32(Request.QueryString["id"].ToString());
        //    ViewState["pk_alumniid"] = Request.QueryString["id"].ToString();
        //    if (pk_alumniid != 0)
        //    {
        //        GetAchieversDetails(pk_alumniid);
        //    }
        //}

        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
            {
                try
                {
                    crypto cpt = new crypto();
                    string decId = cpt.DecodeString(Request.QueryString["id"].ToString());
                    ViewState["pk_alumniid"] = Convert.ToInt32(decId);
                    pk_alumniid = int.Parse(decId.ToString());
                    if (pk_alumniid != 0)
                    {
                        GetAchieversDetails(pk_alumniid);
                    }
                }
                catch
                {
                    Response.Redirect(Page.ResolveUrl("~/Alumni/ALM_Alumni_Home.aspx"));
                }
            }
            else
            {
                Response.Redirect(Page.ResolveUrl("~/Alumni/ALM_Alumni_Home.aspx"));
            }
        }
    }
    /// <summary>
    /// Bind Acheivers Repeater
    /// </summary>
    /// <param name="pk_alumniid"></param>
    private void GetAchieversDetails(int alumniid)
    {
        DataTable dt = new DataTable();
        dt = GetAcheiversDetailss(alumniid);
        if (dt.Rows.Count > 0)
        {
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            RepeventsAchievers.DataSource = dt;
            RepeventsAchievers.DataBind();
        }
    }
}