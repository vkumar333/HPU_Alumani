using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using System.Web.UI;
using SubSonic;
using System.Web.UI.WebControls;
using NPOI.HSSF.Util;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;
using System.Text;
using System.Data.SqlClient;

public partial class Alumni_ALM_Comments_Approval_On_Posted_News_Stories : System.Web.UI.Page
{
    private bool IspageReferesh = false;
    CustomMessaging eobj = new CustomMessaging();
    CommonFunction cfObj = new CommonFunction();
    DataAccess Dobj = new DataAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Gridview();
            Bind_Gridview_Approved();
        }
    }

    protected void Client_Messaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void Bind_Gridview()
    {
        DataSet ds = BindComment_Alumni_approval_Panding().GetDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            dgv.DataSource = ds;
            dgv.DataBind();
        }
        else
        {
            btnprocess.Enabled = false;
            btnprocess.Visible = false;
            dgv.DataSource = null;
            dgv.DataBind();
        }
    }

    public StoredProcedure BindComment_Alumni_approval_Panding()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Comment_Alumni_ApprovalPending_Posted_News_Stories", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    protected void Bind_Gridview_Approved()
    {
        DataSet ds = BindComment_Alumni_approval().GetDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            gdapproval.DataSource = ds;
            gdapproval.DataBind();
        }
        else
        {
            gdapproval.DataSource = null;
            gdapproval.DataBind();
        }
    }

    public StoredProcedure BindComment_Alumni_approval()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Comment_Alumni_Approval_Posted_News_Stories", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    protected void btnprocess_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IspageReferesh)
            {
                int CountCheckedItem = 0;
                foreach (GridViewRow gr in dgv.Rows)
                {
                    CheckBox check = (CheckBox)(gr.FindControl("chkselect"));
                    if (check.Checked)
                    {
                        CountCheckedItem++;
                    }
                }

                if (CountCheckedItem == 0)
                {
                    Client_Messaging("Select Atleast one Comments for Approval.");
                    dgv.HeaderRow.FindControl("chkSelectAll").Focus();
                    return;
                }
                Save();
            }
        }
        catch (SqlException exp)
        {
            lblMsg.Text = eobj.ShowSQLErrorMsg(exp.Message.ToString(), "", exp);
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    private void Save()
    {
        DataSet ds = Dobj.GetSchema("ALM_News_Stories_Comments");
        foreach (GridViewRow gr in dgv.Rows)
        {
            CheckBox check = (CheckBox)(gr.FindControl("chkselect"));
            if (check.Checked)
            {
                Label lblId = (Label)(gr.FindControl("lblid"));
                DataRow dr = ds.Tables[0].NewRow();
                dr["pk_CommentID"] = Convert.ToInt32(lblId.Text.Trim());
                ds.Tables[0].Rows.Add(dr);
            }
        }
        string xml = ds.GetXml();

        if ((Alm_Alumni_Commect_Approval(xml, Session["userid"].ToString(), 0).Execute()) > 0)
        {
            Client_Messaging("Comments Approved Successfully!");
            Bind_Gridview();
            Bind_Gridview_Approved();
        }
    }

    public static StoredProcedure Alm_Alumni_Commect_Approval(string xmlDoc, string userid, int pk_CommentID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Comment_Approval_Posted News Stories", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@xmlDoc", xmlDoc, DbType.String);
        sp.Command.AddParameter("@userid", userid, DbType.String);
        sp.Command.AddParameter("@pk_CommentID", pk_CommentID, DbType.Int32);
        return sp;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Bind_Gridview();
        Bind_Gridview_Approved();
    }
}