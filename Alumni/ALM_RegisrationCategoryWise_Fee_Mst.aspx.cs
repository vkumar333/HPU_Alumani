using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DataAccessLayer;
using System.IO;
using System.Data.SqlClient;
using System.Xml;
using SubSonic;
using System.Data;

public partial class Alumni_ALM_RegisrationCategoryWise_Fee_Mst : System.Web.UI.Page
{
    private Boolean IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGrid();
        }
    }

    protected void FillGrid()
    {
        try
        {
            DataSet ds = Fill_Grid().GetDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds.Tables[0];
                gvDetails.DataBind();
                Clear();
            }
            else
            {
                gvDetails.DataSource = null;
                gvDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsPageRefresh)
            {
                if (btnSave.CommandName.ToString().ToUpper() == "SAVE")
                {
                    Save();
                    FillGrid();
                    gvDetails.Visible = true;
                    Clear();
                }
                else
                {
                    Update();
                    FillGrid();
                    gvDetails.Visible = true;
                    btnSave.Text = "SAVE";
                    Clear();
                }
            }
        }
        catch (SqlException exp)
        {
            //lblMsg.Text = eobj.ShowSQLErrorMsg(exp.Message.ToString(), "", exp);
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    private void Update()
    {
        if (Validation())
        {
            try
            {
                string Message = "";
                string Category = Convert.ToString(txtCategory.Text);
                string description = txtDescrition.Text.Trim().ToString();
                int Amount = Convert.ToInt32(txtFee.Text);
                bool isActive = chkIsActive.Checked == true ? true : false;
                string UpdatedBy = Session["UserID"].ToString();

                int pk_feeid = Convert.ToInt32(ViewState["id"].ToString());
                DataSet ds = new DataSet();
                ds = Registration_CategoryWise_Fee_Upd(Category, description, Amount, isActive, UpdatedBy, pk_feeid).GetDataSet();
                if (ds.Tables.Count == 0)
                {
                    ClientMessaging("Record Update Successfully.");
                    Clear();
                }
                else
                {
                    ClientMessaging("Record not Update.");
                }

            }
            catch (SqlException ex)
            {
                lblMsg.Text = CommonCode.ExceptionHandling.SqlExceptionHandling(ex.Message);
            }
        }
    }

    public void Save()
    {
        if (Validation())
        {
            try
            {
                string Message = "";
                string Category = Convert.ToString(txtCategory.Text);
                string description = txtDescrition.Text.Trim().ToString();
                int Amount = Convert.ToInt32(txtFee.Text);
                bool isActive = chkIsActive.Checked == true ? true : false;
                string insertBy = Session["UserID"].ToString();

                DataSet ds = new DataSet();
                ds = Registration_CategoryWise_Fee_Ins(Category, description, Amount, isActive, insertBy).GetDataSet();

                if (ds.Tables.Count == 0)
                {
                    ClientMessaging("Record Save Successfully.");
                    Clear();
                }
                else
                {
                    ClientMessaging("Record not saved......!");
                }
            }
            catch (Exception Ex)
            {
                lblMsg.Text = CommonCode.ExceptionHandling.SqlExceptionHandling(Ex.Message);
            }
        }
    }

    public bool Validation()
    {
        if (txtCategory.Text == "")
        {
            ClientMessaging("Category can not be blank!");
            txtCategory.Focus();
            return false;
        }

        if (txtFee.Text == "")
        {
            ClientMessaging("Fee can not be blank!");
            txtFee.Focus();
            return false;
        }

        if (txtDescrition.Text == "")
        {
            ClientMessaging("Fee can not be blank!");
            txtDescrition.Focus();
            return false;
        }
        return true;
    }

    protected void Clear()
    {
        try
        {
            txtCategory.Text = "";
            txtDescrition.Text = "";
            txtFee.Text = "";
            chkIsActive.Checked = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    private void Edit()
    {
        DataSet ds = Registration_CategoryWise_Fee_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtCategory.Text = ds.Tables[0].Rows[0]["Category"].ToString();
            txtDescrition.Text = ds.Tables[0].Rows[0]["Descriptions"].ToString();
            txtFee.Text = ds.Tables[0].Rows[0]["Fees"].ToString();
            chkIsActive.Checked = false;
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Active"]) == true)
                chkIsActive.Checked = true;
            btnReset.Visible = true;
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            //   D_ddlCategories.Enabled = false;
            ViewState["id"] = e.CommandArgument.ToString().Trim();
            int pk_feeid = Convert.ToInt32(e.CommandArgument.ToString());
            gvDetails.SelectedIndex = -1;

            if (e.CommandName.ToUpper().ToString() == "EDITREC")
            {
                Edit();
                btnSave.Text = "UPDATE";
                btnSave.CommandName = "UPDATE";

                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int RowIndexS = gvr.RowIndex;

                foreach (GridViewRow row in gvDetails.Rows)
                {
                    row.BackColor = System.Drawing.Color.Transparent;
                }
                gvDetails.Rows[RowIndexS].BackColor = System.Drawing.Color.LightCyan;
                gvDetails.Rows[RowIndexS].Style.Add("Font-Weight", "Bold");

            }
            else if (e.CommandName.ToUpper().ToString() == "DELETEREC")
            {
                IDataReader rdr = Registration_CategoryWise_Fee_Edit(Convert.ToInt32(ViewState["id"])).GetReader();
                DataTable dtt = new DataTable();
                dtt.Load(rdr);
                rdr.Close();

                if ((Registration_CategoryWise_Fee_Del(Convert.ToInt32(ViewState["id"]))).Execute() > 0)
                {
                    txtCategory.Focus();
                    FillGrid();
                    ClientMessaging("Record Deleted Successfully");
                    Clear();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
        //   D_ddlCategories.Focus();
        Anthem.Manager.IncludePageScripts = true;
    }

    #region Stored Procedure

    public static StoredProcedure Registration_CategoryWise_Fee_Edit(int pk_feeid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Registration_CategoryWise_Fee_Mst_Edit", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_feeid", pk_feeid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure Registration_CategoryWise_Fee_Del(int pk_feeid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Registration_CategoryWise_Fee_Mst_Del", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_feeid", pk_feeid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure Registration_CategoryWise_Fee_Ins(string Category, string Descriptions, int Amount, bool Active, string insertBy)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Registration_CategoryWise_Fee_Mst_Ins", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Category", Category, DbType.String);
        sp.Command.AddParameter("@Descriptions", Descriptions, DbType.String);
        sp.Command.AddParameter("@Amount", Amount, DbType.Int32);
        sp.Command.AddParameter("@IsActive", Active, DbType.Boolean);
        sp.Command.AddParameter("@InsertBy", insertBy, DbType.String);
        return sp;
    }

    public static StoredProcedure Registration_CategoryWise_Fee_Upd(string Category, string Descriptions, int Amount, bool Active, string UpdatedBy, int pk_feeid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Registration_CategoryWise_Fee_Mst_Upd", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Category", Category, DbType.String);
        sp.Command.AddParameter("@Descriptions", Descriptions, DbType.String);
        sp.Command.AddParameter("@Amount", Amount, DbType.Int32);
        sp.Command.AddParameter("@IsActive", Active, DbType.Boolean);
        sp.Command.AddParameter("@UpdatedBy", UpdatedBy, DbType.String);
        sp.Command.AddParameter("@pk_feeid", pk_feeid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure Fill_Grid()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Registration_CategoryWise_Fee_Mst_SelAll", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    #endregion

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Clear();
        gvDetails.DataSource = null;
        gvDetails.DataBind();
        btnSave.Text = "SAVE";
    }
}