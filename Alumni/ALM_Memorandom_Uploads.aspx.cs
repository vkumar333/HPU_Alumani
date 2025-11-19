using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using Anthem;
using System.Web.UI.WebControls;
using DataAccessLayer;

public partial class Alumni_ALM_Memorandom_Uploads : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    crypto crp = new crypto();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    private DataTable GetMemorandomnCertificate()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_Get_Memorandomn_certificate", values, names, types);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ImagesRepeter();
        }
    }

    private void ImagesRepeter()
    {
        DataTable dt = new DataTable();
        dt = GetMemorandomnCertificate();

        if (dt.Rows.Count > 0)
        {
            Reppdf.DataSource = dt;
            Reppdf.DataBind();
        }
    }
}