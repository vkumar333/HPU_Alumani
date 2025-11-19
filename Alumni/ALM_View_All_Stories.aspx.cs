using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using System.Collections;
using System.Data;
using System.IO;
using SubSonic;

public partial class Alumni_ALM_View_All_Stories : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    crypto cpt = new crypto();
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    private object lblmsg;

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable getStoriesDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_View_All_Stories", values, names, types);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getStoriesDetailsRepeter();
        }
    }

    private void getStoriesDetailsRepeter()
    {
        DataTable dt = new DataTable();
        dt = getStoriesDetails();
        dt.Columns.Add("encId");
        if (dt.Rows.Count > 0)
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                string pkid = dt.Rows[x]["Pk_Stories_id"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dt.Rows[x]["encId"] = encId;
            }
            storiesRepeater.DataSource = dt;
            storiesRepeater.DataBind();
        }
    }

    protected void lnkbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Alm_NewsandStoriesList.aspx");
    }
}