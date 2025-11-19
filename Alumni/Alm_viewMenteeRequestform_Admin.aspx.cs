using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using DataAccessLayer;
//using CrystalDecisions.CrystalReports.Engine;
using System.Drawing;
using System.Web.Hosting;
using System.Linq;
using System.Data.SqlClient;

public partial class Alumni_Alm_viewMenteeRequestform_Admin : System.Web.UI.Page
{
    DataAccess DAobj = new DataAccess();
    DataAccess Dobj = new DataAccess();
    ArrayList name = new ArrayList(); ArrayList type = new ArrayList(); ArrayList value = new ArrayList();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

    /// <summary>
    /// Clear function of Sp
    /// </summary>
    void clear()
    {
        name.Clear(); value.Clear(); type.Clear();
    }
    void clears()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    public string dsxml { get; set; }
    public int pk_MenteeID { get; set; }
    public int pk_alumnid { get; set; }

    crypto crp = new crypto();

    /// <summary>
    /// Sp to Insert Record into multiple table
    /// </summary>
    /// <param name="Message"></param>
    /// <returns></returns>
    public int InsertRecord(ref string Message)
    {
        clear();
        name.Add("@doc"); value.Add(dsxml); type.Add(SqlDbType.VarChar);
        if (DAobj.ExecuteTransactionMsg("Alm_insertMenteeReq", value, name, type, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int UpdateRecord(ref string Message)
    {
        clear();
        name.Add("@doc"); value.Add(dsxml); type.Add(SqlDbType.VarChar);
        name.Add("@pk_MenteeID"); value.Add(pk_MenteeID); type.Add(SqlDbType.Int);
        if (DAobj.ExecuteTransactionMsg("ALM_SP_MenteeReq_Upd", value, name, type, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Sp to Bind Checkbox listfor connect
    /// </summary>
    /// <returns></returns>
    public DataSet Fillconnects()
    {
        DataAccess obj = new DataAccess();
        DataSet ds = null;
        try
        {
            this.clear();
            ds = obj.GetDataSet("Alm_ConnectBind", value, name, type);
        }
        catch
        {
        }
        return ds;
    }

    /// <summary>
    /// Sp to check count
    /// </summary>
    /// <returns></returns>
    /// /// <summary>
    /// Sp to fill Controls
    /// </summary>
    /// <returns></returns>
    private DataSet FillmenteeDetails()
    {
        clears();
        names.Add("@Pk_MenteeReqid"); values.Add(pk_MenteeID); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("Alm_FillmenteeDetails", values, names, types);
    }

    /// <summary>
    /// Sp for Bind Checkbox list Problems
    /// </summary>
    /// <returns></returns>
    public DataSet FillToEmployee()
    {
        DataAccess obj = new DataAccess();
        DataSet ds = null;
        try
        {
            this.clear();
            ds = obj.GetDataSet("Alm_probemfacingBind", value, name, type);
        }
        catch
        {
        }
        return ds;
    }

    private bool IspageReferesh = false;

    //Page Load 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
            {
                try
                {
                    pk_MenteeID = int.Parse((crp.Decrypt((Request.QueryString["id"].ToString()).ToString())));
                    //pk_MenteeID = int.Parse(crp.Encrypt(Request.QueryString["id"]));
                }
                catch
                {
                    Response.Redirect(Page.ResolveUrl("~/UserLogin.html"));
                }
            }
            else
            {
                Response.Redirect(Page.ResolveUrl("~/UserLogin.html"));
            }
            bindproblems();
            bindconnects();
            filldata();
            txtspcfy.Enabled = false;
            txtbckground.Enabled = false;
            txtdomains.Enabled = false;
            chkproblems.Enabled = false;
            connectList.Enabled = false;
        }
    }

    /// <summary>
    /// Bind Problems Checkbox List
    /// </summary>
    protected void bindproblems()
    {
        DataSet ds = FillToEmployee();
        chkproblems.DataSource = ds;
        chkproblems.DataTextField = "Description";
        chkproblems.DataValueField = "pk_ProblemId";
        chkproblems.DataBind();
    }

    /// <summary>
    /// Bind connects Checkbox List
    /// </summary>
    protected void bindconnects()
    {
        DataSet ds = Fillconnects();
        connectList.DataSource = ds;
        connectList.DataTextField = "Description";
        connectList.DataValueField = "pk_communicationTypeID";
        connectList.DataBind();
    }

    //Get all Form Data
    public void filldata()
    {
        DataSet ds = FillmenteeDetails();
        if (ds.Tables.Count > 0)
        {
            for (int k = 0; k < ds.Tables[1].Rows.Count; k++)
            {
                for (int j = 0; j < chkproblems.Items.Count; j++)
                {
                    if (chkproblems.Items[j].Value.ToString() == ds.Tables[1].Rows[k]["fk_ProblemId"].ToString())
                    {
                        chkproblems.Items[j].Selected = true;
                    }
                }
            }
            if (chkproblems.SelectedItem.Text.ToString() == "Other")
            {
                pnl.Visible = true;
                txtspcfy.Text = ds.Tables[0].Rows[0]["Others"].ToString();
            }
            Labelname.Text = ds.Tables[0].Rows[0]["alumni_name"].ToString();
            txtbckground.Text = ds.Tables[0].Rows[0]["Professional_Background"].ToString();
            txtdomains.Text = ds.Tables[0].Rows[0]["domains"].ToString();
            // txtdegree.Text = ds.Tables[0].Rows[0]["fk_degreeid"].ToString();
            //string pk_alumniid = ds.Tables[0].Rows[0]["pk_alumniid"].ToString();
            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
            {
                for (int j = 0; j < connectList.Items.Count; j++)
                {
                    if (connectList.Items[j].Value.ToString() == ds.Tables[2].Rows[k]["fk_communicationTypeID"].ToString())
                    {
                        connectList.Items[j].Selected = true;
                    }
                }
            }
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Alumni/Alm_AssignedMentor.aspx");
    }
}