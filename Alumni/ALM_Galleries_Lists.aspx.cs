using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;

public partial class Alumni_ALM_Galleries_Lists : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable GetGellary()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GetGellaryRecords", values, names, types);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ImagesRepeter();
    }

    private void ImagesRepeter()
    {
        DataTable dt = new DataTable();
        dt = GetGellary();
        if (dt.Rows.Count > 0)
        {
            GalleryImages.DataSource = dt;
            GalleryImages.DataBind();
        }
    }
}