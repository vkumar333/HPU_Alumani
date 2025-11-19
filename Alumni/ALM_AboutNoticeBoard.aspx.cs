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

public partial class Alumni_ALM_AboutNoticeBoard : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    crypto cpt = new crypto();

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
                string noticeID = cpt.DecodeString(Request.QueryString["ID"].ToString());
                int noticeid = Convert.ToInt32(noticeID);
                ViewState["Pk_Board_ID"] = noticeID.ToString();

                if (noticeid != 0)
                {
                    Repeterid(noticeid);
                }
            }
        }
    }

    private void Repeterid(int noticeboardid)
    {
        DataTable dt = new DataTable();
        dt = GetDetailsByid(noticeboardid);
        if (dt.Rows.Count > 0)
        {
            //RepeventsAll.DataSource = dt;
            //RepeventsAll.DataBind();

            string fileName = "";

            fileName = dt.Rows[0]["Filepath"].ToString().Trim();

            if (fileName != "")
            {
                if (dt.Rows[0]["Filepath"].ToString() != "None")
                {
                    Imge.Src = dt.Rows[0]["Filepath"].ToString();
                }
            }
            else
            {
                Imge.Src = "~/alumni/stuimage/No_image.png";
            }

            lblHeading.Text = dt.Rows[0]["Heading"].ToString();
            lblStartDate.Text = dt.Rows[0]["ConvertedDate"].ToString();
            lblDescription.Text = dt.Rows[0]["Description"].ToString();
        }
    }
}