using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using System.Collections;
using System.Data;
using System.IO;
using System.Activities.Statements;

public partial class Alumni_FundDonation : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
        
    }

    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    public string Contributor_Name { get; set; }
    public string Email { get; set; }
    public string Mobile_No { get; set; }
    public string Donation_amount { get; set; }
    public int @Fk_contribution_ID { get; set; }


    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }
    private DataTable FundDetails(int pk_contribution_ID)
    {
        ClearArrayLists();
        names.Add("@pk_contribution_ID"); types.Add(SqlDbType.NVarChar); values.Add(pk_contribution_ID);
        return Dobj.GetDataTable("Show_Crowd_Fund_Details_by_id", values, names, types);
    }
    private DataTable FundDetailsCat(int pk_contribution_ID)
    {
        ClearArrayLists();
        names.Add("@pk_contribution_ID"); types.Add(SqlDbType.NVarChar); values.Add(pk_contribution_ID);
        return Dobj.GetDataTable("Crowd_Fund_Details", values, names, types);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                //Page.Form.Attributes.Add("enctype", "multipart/form-data");
                int pk_contribution_ID = Convert.ToInt32(Request.QueryString["ID"].ToString());
                ViewState["funddetails"] = Request.QueryString["ID"].ToString();
                if (pk_contribution_ID != 0)
                {
                    ALLRepeter(pk_contribution_ID);
                    RepeterWithcategories(pk_contribution_ID);

                }
            }
            //Details_Repeter();
        }
    }

    /// <summary>
    /// Bind Repeater
    /// </summary>
    /// <param name="pk_contribution_ID"></param>
    private void ALLRepeter(int pk_contribution_ID)
    {
        DataTable dt = new DataTable();
        dt = FundDetails(pk_contribution_ID);
        if (dt.Rows.Count > 0)
        {
            string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            RepeventsAll.DataSource = dt;
            RepeventsAll.DataBind();
        }
    }
    private void RepeterWithcategories(int pk_contribution_ID)
    {
        DataTable dt = new DataTable();
        dt = FundDetailsCat(pk_contribution_ID);
        if (dt.Rows.Count > 0)
        {
            string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            RepOnlydetails.DataSource = dt;
            RepOnlydetails.DataBind();
            coordinatorsDetails.DataSource = dt;
            coordinatorsDetails.DataBind();
        }
    }
    public DataSet Getmain()
    {
        
         
         DataSet ds_master = Dobj.GetSchema("ALM_Contributors_Details");
        DataRow dr = ds_master.Tables[0].NewRow();
        dr["Donation_amount"] = Txt_Amount.Text.ToString();
        dr["Contributor_Name"] = txtname.Text.ToString();
        dr["Email"] = Textemail.Text.ToString();
        dr["Mobile_No"] = mobile.Text.ToString();
        //dr["fk_cat_id"] = D_ddlCategories.SelectedValue;
        //dr["fk_userid"] = Session["UserID"].ToString();
        ds_master.Tables[0].Rows.Add(dr);
 
        return ds_master;
    }
    //private void Details_Repeter()
    //{
    //    DataTable dt = new DataTable();
    //    dt = FundDetails();
    //    //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");       
    //    if (dt.Rows.Count > 0)
    //    {
    //        string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
    //        //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
    //        rep.DataSource = dt;
    //        rep.DataBind();
    //    }
    //}
    #region "Other common Events"

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }
    #endregion
    public int Insfunddata(ref string Message)
    {
        try
        {
            names.Add("@Contributor_Name"); values.Add(Contributor_Name); types.Add(SqlDbType.VarChar);
            names.Add("@Email"); values.Add(Email); types.Add(SqlDbType.VarChar);
            names.Add("@Mobile_No"); values.Add(Mobile_No); types.Add(SqlDbType.VarChar);
            names.Add("@Donation_amount"); values.Add(Donation_amount); types.Add(SqlDbType.VarChar);
            names.Add("@Fk_contribution_ID"); values.Add(ViewState["funddetails"]); types.Add(SqlDbType.Int);
     
            if (Dobj.ExecuteTransactionMsg("Alm_Fund_Data_Ins", values, names, types, ref Message) > 0)
            {
                Message = Dobj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception e)
        {

            throw ;
        }
        
    }
    public bool Validation()
    {

        //if (D_ddlCategories.SelectedIndex == 0)
        //{
        //    ClientMessaging("Categories cannot be blank!");
        //    D_ddlCategories.Focus();
        //    return false;
        //}
        if (string.IsNullOrEmpty(Txt_Amount.Text.ToString()))
        {
            ClientMessaging("Amount cannot be blank!");
            Txt_Amount.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtname.Text.ToString()))
        {
            ClientMessaging("Name cannot be blank!");
            txtname.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(Textemail.Text.ToString()))
        {
            ClientMessaging("Email  cannot be blank!..");
            Textemail.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(mobile.Text.ToString()))
        {
            ClientMessaging("Mobile  cannot be blank!..");
            mobile.Focus();
            return false;
        }

        return true;
    }
    public void Save()
    {
        if (Validation())
        {
            try
            {
                string Message = "";
            //    DataSet ds_details = new DataSet();
            //    ds_details = Getmain();
            ////string doc = "";
            ////doc = ds_details.GetXml();
            ////xmldoc = doc;
            ////mode = 1;
            //int a= Convert.ToInt32(mobile.Text);
            //string b= cars.

            @Donation_amount = Txt_Amount.Text.ToString();
            @Contributor_Name = txtname.Text.ToString();
            @Email = Textemail.Text.ToString();
            @Mobile_No = mobile.Text.ToString();
            @Fk_contribution_ID =Convert.ToInt32(ViewState["funddetails"]);
            if (Insfunddata(ref Message) > 0)
            {
                ClientMessaging("Record Save Successfully.");
            }
            
        }

            catch (Exception Ex)
            {
            Throw E;
                
            }
        }
    }

    //protected void donate_Click(object sender, EventArgs e)
    //{
    //    if (donate.CommandName.ToString().ToUpper() == "SAVE")
    //    {
    //        Save();
    //        //Clear();
    //        //GrdFile.Visible = false;

    //    }
    //    //else
    //    //{
    //    //    Update();
    //    //    GrdFile.Visible = false;
    //    //    //Fill_grid();

    //    //}
    //}

    protected void donate_Click(object sender, EventArgs e)
    {
        Save();
    }

     
}