using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using DataAccessLayer;

public partial class Alumni_ALM_Country_Mst : System.Web.UI.Page
{
    ALM_CountryMaster Obj = new ALM_CountryMaster();
    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList();
    ArrayList types = new ArrayList();
    ArrayList Values = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillgridData();
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    private bool ValidateData()
    {
        if (txtCountry.Text.ToString() == "" || txtCountry.Text.ToString() == null)
        {
            ClientMessaging("Please Enter Country Name");
            txtCountry.Focus();
            return false;
        }
        if (txtCountryCode.Text.ToString() == "" || txtCountryCode.Text.ToString() == null)
        {
            ClientMessaging("Please Enter Country Code");
            txtCountryCode.Focus();
            return false;
        }
        return true;
    }

    private void Reset()
    {
        txtCountryCode.Text = string.Empty;
        txtCountry.Text = string.Empty;
        btnSave.Text = "SAVE";
    }

    private void FillgridData()
    {
        try
        {
            DataSet ds = Obj.FillGrid();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCountry.DataSource = ds.Tables[0];
                gvCountry.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }

    }
    private void FillgridData_search(int pageIndex, int pagesize)
    {
        try
        {
            DataSet ds = FillGrid_S(pageIndex, pagesize, txtCountryS.Text.ToString().Trim(), txtCountryCodeS.Text.ToString().Trim());
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCountry.DataSource = ds.Tables[0];
                gvCountry.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        lblMsg1.Text = "";
        if (pnlSearch.Visible == true)
        {
            pnlSearch.Visible = false;
            Anthem.Manager.IncludePageScripts = true;
            txtCountry.Focus();
        }
        else
        {
            pnlSearch.Visible = true;
            Anthem.Manager.IncludePageScripts = true;
            txtCountry.Focus();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg1.Text = "";

        string Country = txtCountryS.Text.Trim().ToString();
        string Code = txtCountryCodeS.Text.Trim().ToString();

        DataSet ds = FillGrid_S(gvCountry.PageIndex = 0, gvCountry.PageSize = 10, Country, Code);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvCountry.DataSource = ds.Tables[1];
            gvCountry.DataBind();
        }
        else
        {
            gvCountry.DataSource = null;
            gvCountry.DataBind();
            lblMsg1.Text = "No Record Found!";
        }
    }

    private void ClearArrayList()
    {
        names.Clear(); types.Clear(); Values.Clear();
    }
    public DataSet FillGrid_S(int pageIndex, int pagesize, string country, string code)
    {
        ClearArrayList();
        names.Add("@pageindex"); Values.Add(pageIndex); types.Add(SqlDbType.Int);
        names.Add("@pagesize"); Values.Add(pagesize); types.Add(SqlDbType.Int);
        names.Add("@countryS"); Values.Add(country); types.Add(SqlDbType.VarChar);
        names.Add("@codeS"); Values.Add(code); types.Add(SqlDbType.VarChar);
        return DAobj.GetDataSet("ALM_Country_Mst_Search_FillGrid", Values, names, types);
    }

    protected void D_ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void gvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string Message = "";
            if (e.CommandName == "DELETEREC")
            {
                Obj.pk_CountryID = Convert.ToInt32(e.CommandArgument);
                if (Obj.DeleteRecord(ref Message) > 0)
                {
                    ClientMessaging("Record Delete Succesfully");
                    FillgridData();
                    Reset();
                }
            }
            else
            {
                Obj.pk_CountryID = Convert.ToInt32(e.CommandArgument);
                ViewState["ID"] = Convert.ToInt32(e.CommandArgument);
                DataSet ds = Obj.Edit();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCountry.Text = ds.Tables[0].Rows[0]["Country_Name"].ToString();
                    txtCountryCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                    btnSave.Text = "UPDATE";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void gvCountry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCountry.PageIndex = e.NewPageIndex;
        FillgridData();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg1.Text = "";
        txtCountryS.Text = "";
        txtCountryCodeS.Text = "";
        Anthem.Manager.IncludePageScripts = true;
        txtCountryS.Focus();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Message = "";
        try
        {
            if (ValidateData())
            {
                if (btnSave.Text == "SAVE")
                {
                    Obj.CountryCode = txtCountryCode.Text.ToString().Trim();
                    Obj.CountryName = txtCountry.Text.ToString().Trim();

                    if (Obj.InsertRecord(ref Message) > 0)
                    {
                        ClientMessaging("Record Save Succesfully");
                        FillgridData();
                        Reset();
                    }
                    else
                    {
                        ClientMessaging("Duplicate Record Found...");
                    }
                }
                if (btnSave.Text == "UPDATE")
                {
                    Obj.pk_CountryID = Convert.ToInt32(ViewState["ID"]);
                    Obj.CountryCode = txtCountryCode.Text.ToString().Trim();
                    Obj.CountryName = txtCountry.Text.ToString().Trim();

                    if (Obj.UpdateRecord(ref Message) > 0)
                    {
                        ClientMessaging("Record Update Succesfully");
                        FillgridData();
                        Reset();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
        FillgridData();
    }
}

public class ALM_CountryMaster
{
    DataAccess DAobj = new DataAccess();

    ArrayList names = new ArrayList();
    ArrayList types = new ArrayList();
    ArrayList values = new ArrayList();

    public int pk_CountryID { get; set; }
    public string CountryName { get; set; }
    public string CountryCode { get; set; }
    public string Remarks { get; set; }

    private void ClearArrayList()
    {
        names.Clear(); types.Clear(); values.Clear();
    }

    public DataSet FillGrid()
    {
        ClearArrayList();
        return DAobj.GetDataSet("ALM_FillCountry_Mst", values, names, types);
    }

    public DataSet Edit()
    {
        ClearArrayList();
        names.Add("@pk_CountryID"); values.Add(this.pk_CountryID); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("ALM_Country_Mst_Edit", values, names, types);
    }

    public int InsertRecord(ref string Message)
    {
        ClearArrayList();
        names.Add("@CountryCode"); values.Add(this.CountryCode); types.Add(SqlDbType.VarChar);
        names.Add("@CountryName"); values.Add(this.CountryName); types.Add(SqlDbType.VarChar);
        if (DAobj.ExecuteTransactionMsg("ALM_Country_Mst_Ins", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int DeleteRecord(ref string Message)
    {
        ClearArrayList();
        names.Add("@pk_CountryID"); values.Add(this.pk_CountryID); types.Add(SqlDbType.Int);
        if (DAobj.ExecuteTransactionMsg("ALM_Country_Mst_Del", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("D", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int UpdateRecord(ref string Message)
    {
        ClearArrayList();
        names.Add("@pk_CountryID"); values.Add(this.pk_CountryID); types.Add(SqlDbType.Int);
        names.Add("@CountryCode"); values.Add(this.CountryCode); types.Add(SqlDbType.VarChar);
        names.Add("@CountryName"); values.Add(this.CountryName); types.Add(SqlDbType.VarChar);
        if (DAobj.ExecuteTransactionMsg("ALM_Country_Mst_Upd", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("U", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }
}