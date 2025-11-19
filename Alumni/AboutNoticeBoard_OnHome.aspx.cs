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

public partial class Alumni_AboutNoticeBoard_OnHome : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable GetDetailsByid(int Pk_Board_ID)
    {
        ClearArrayLists();
        names.Add("@Pk_Board_ID"); types.Add(SqlDbType.NVarChar); values.Add(Pk_Board_ID);
        return Dobj.GetDataTable("GetNoticeBoard_Dtls_by_id", values, names, types);
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                int Pk_Board_ID = Convert.ToInt32(Request.QueryString["ID"].ToString());
                ViewState["Pk_Board_ID"] = Request.QueryString["ID"].ToString();
                if (Pk_Board_ID != 0)
                {
                    Repeterid(Pk_Board_ID);
                }
            }
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
}