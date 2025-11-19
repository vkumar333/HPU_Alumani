using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using Anthem;
using System.Web.UI.WebControls;
using DataAccessLayer;

public partial class Alumni_Alm_show_Registration_Certificate : System.Web.UI.Page
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
    /// <summary>
    /// Sp of ImagesRepeter
    /// </summary>
    /// <param name="yesrofpassing"></param>
    /// <returns></returns>
    private DataTable Getmemberbyyear()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_Get_registration_certificate", values, names, types);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ImagesRepeter();
        }
    }
    /// <summary>
    /// Repeater for get Image and  subject Details
    /// </summary>
    /// <param name="yesrofpassing"></param>
    private void ImagesRepeter()
    {
        DataTable dt = new DataTable();
        dt = Getmemberbyyear();

        if (dt.Rows.Count > 0)
        {
            Reppdf.DataSource = dt;
            Reppdf.DataBind();
        }
    }
}