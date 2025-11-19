using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using DataAccessLayer;
using System.Drawing;
using System.Web.Hosting;
using System.Data.SqlClient;
using SubSonic;

public partial class Alumni_Alm_Mentees_Assigned : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    private object lblmsg;
    public string menteename { get; set; }

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    /// <summary>
    /// Clear function of Sp
    /// </summary>
    /// 
    private DataTable ALM_MentorStatuscheck()
    {
        ClearArrayLists();
        names.Add("@name"); values.Add(menteename); types.Add(SqlDbType.VarChar);
        return Dobj.GetDataTable("Alm_Mentees_Dtl_new", values, names, types);
    }

    /// <summary>
    /// Create the method of get data of mentees
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckMentes()
    {
        if (Session["AlumniName"] != null)
        {
            if (Session["AlumniName"].ToString() != "")
            {
                menteename = Session["AlumniName"].ToString();
                DataTable dt = ALM_MentorStatuscheck();
                var count = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    gvAlumni.DataSource = dt;
                    gvAlumni.DataBind();
                }
            }
        }
    }

    private DataTable MenteesDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("Alm_Mentees_Dtl", values, names, types);
    }

    void clear()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    // <summary>
    /// Bind Theme
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckMentes();
            //Meentess();
        }
    }

    protected void gvAlumni_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void gvAlumni_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}