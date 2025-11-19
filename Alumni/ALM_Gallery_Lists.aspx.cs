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

public partial class Alumni_ALM_Gallery_Lists : System.Web.UI.Page
{
    #region Global Declaration

    crypto cpt = new crypto();

    #endregion

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

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ImagesRepeter();
    }

    private void ImagesRepeter()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetGellary();
            dt.Columns.Add("encId");

            int rnum = (dt.Rows.Count) + 1;

            if (dt.Rows.Count > 0)
            {
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    string pkid = dt.Rows[x]["Pk_Groupid"].ToString();
                    string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                    dt.Rows[x]["encId"] = encId;
                }
                GalleryImages.DataSource = dt;
                GalleryImages.DataBind();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}