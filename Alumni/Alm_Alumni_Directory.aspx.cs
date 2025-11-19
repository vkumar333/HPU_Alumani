using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using Anthem;
using System.IO;
using System.Web.UI.WebControls;
using DataAccessLayer;
using SubSonic;
using System.Linq;
using System.Collections.Generic;

public partial class Alumni_Alm_Alumni_Directory : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    crypto crp = new crypto();
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable Getmemberprofile()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GetAlumniprofile", values, names, types);
    }

    private DataTable Getmtotalcount()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GettotalAlumni_Count", values, names, types);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clear();
            txtsearch.Text = "";
            BindPassingYears();
            BindPassingYearsDDL();
            ProfileRepeter();
            CountRepeter();
            BindDropdown();
        }
    }

    /// <summary>
    /// Get Image and other informations
    /// </summary>
    /// <param name="yesrofpassing"></param>
    /// <param name="subjectid"></param>
    private void ProfileRepeter()
    {
        DataTable dt = new DataTable();
        dt = Getmemberprofile();

        if (dt.Rows.Count > 0)
        {
            // string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
            // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
            RepProfile.DataSource = dt;
            RepProfile.DataBind();
        }
    }

    private void CountRepeter()
    {
        DataTable dt = new DataTable();
        dt = Getmtotalcount();

        if (dt.Rows.Count > 0)
        {
            RepCount.DataSource = dt;
            RepCount.DataBind();
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        ClientMessaging("Kindly login First ");
    }

    /// <summary>
    /// Message Pop-up
    /// </summary>
    /// <param name="msg"></param>
    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (R_txtLogin.Text.Trim() == "")
            {
                ClientMessaging("Please Enter User ID");
                String script = String.Format("alert('{0}');", "Please Enter Login Name!");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                R_txtLogin.Focus();
                return;
            }

            if (R_txtPass.Text.Trim() == "")
            {
                ClientMessaging("Please Enter Password");
                String script = String.Format("alert('{0}');", "Please Enter Password!");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                R_txtPass.Focus();
                return;
            }

            DataSet ds = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_LoginAuthentication(R_txtLogin.Text.Trim(), R_txtPass.Text.Trim()).GetDataSet();
            if (ds.Tables[0].Rows[0]["cnt"].ToString() != "0")
            {
                string type = "P";
                Session["AlumniID"] = ds.Tables[1].Rows[0]["pk_alumniid"].ToString();
                Session["Alumnino"] = ds.Tables[1].Rows[0]["alumnino"].ToString();
                Session["AlumniName"] = ds.Tables[1].Rows[0]["alumni_name"].ToString();
                Session["Alumnitype"] = type.ToString();
                //a = a.Replace("+", "%2B");
                //b = b.Replace("+", "%2B");
                string Query = "";
                string a = "";
                if (Session["pk_alumniid"] != null)
                {
                    a = Session["pk_alumniid"].ToString();
                }

                if (!string.IsNullOrEmpty(a))
                {
                    //Query = "~//Alumni//Alm_Alumni_Show_Alumni_Profile.aspx?Alumni=" + a.ToString();
                    Query = "~//Alumni//ALM_AlumniSearch.aspx";
                }
                else
                {
                    //Query = "~//Alumni//Alm_Alumni_ShowAll_Directory.aspx";
                    Query = "~//Alumni//ALM_AlumniSearch.aspx";
                }
                Response.Redirect(Query);
                //  Response.Redirect("Alm_Alumni_ShowAll_Directory.aspx");
            }
            else
            {
                Manager.IncludePageScripts = true;
                ClientMessaging("Invalid User Name or Password! Please contact your college for approval");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "id", "ShowRoomDetails()", true);
                Session["AlumniID"] = "";
                Session["Alumnino"] = "";
                Session["AlumniName"] = "";
                Session["Emailid"] = "";
            }
        }
        catch (Exception ex)
        {
            R_txtLogin.Text = "";
            R_txtPass.Text = "";
            R_txtLogin.Focus();
        }
    }

    protected void btnview_Click(object sender, EventArgs e)
    {
        Session["pk_alumniid"] = null;
    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {

    }

    protected void btnname_Click(object sender, EventArgs e)
    {
        //Get the reference of the clicked button.
        Anthem.LinkButton button = (sender as Anthem.LinkButton);

        //Get the command argument
        string commandArgument = button.CommandArgument;

        //Get the Repeater Item reference
        RepeaterItem item = button.NamingContainer as RepeaterItem;

        //Get the repeater item index
        int index = item.ItemIndex;
        Session["pk_alumniid"] = commandArgument;
    }

    public void clear()
    {
        txtsearch.Text = "";
        //TextBox8.Text = "";
        //TextBox2.Text = "";
        //TextBox7.Text = "";
        //TextBox6.Text = "";
        //TextBox5.Text = "";
        //TextBox4.Text = "";
        //TextBox3.Text = "";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void lnkNeRegs_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Alumni/ALM_AlumniRegistration.aspx");
    }

    protected void ddlYearOfPassing_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedYearOfPassing = ddlYearOfPassing.SelectedItem.Text.Trim().ToString();

        if (!string.IsNullOrEmpty(selectedYearOfPassing))
        {
            bool exists = false;

            foreach (ListItem item in chkPassingYear.Items)
            {
                if (item.Value == selectedYearOfPassing)
                {
                    item.Selected = true;
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                chkPassingYear.Items.Add(new ListItem(selectedYearOfPassing, selectedYearOfPassing) { Selected = true });
            }
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    private void BindPassingYearsDDL()
    {
        try
        {
            DataSet ds = GetAll_Alumni_YearsOfPassing().GetDataSet();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlYearOfPassing.DataSource = ds.Tables[0];
                ddlYearOfPassing.DataTextField = "PassYear_Name";
                ddlYearOfPassing.DataValueField = "PK_Pass_ID";
                ddlYearOfPassing.DataBind();
                ddlYearOfPassing.Items.Insert(0, new ListItem("-- Passing Year --", "0"));
            }
            Anthem.Manager.IncludePageScripts = true;
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message);
        }
    }

    private void BindPassingYears()
    {
        // Predefined degree list
        List<string> passYears = new List<string> { "1992", "2010", "2014", "2015" };

        // Bind CheckBoxList
        chkPassingYear.DataSource = passYears;
        chkPassingYear.DataBind();
    }

    public static StoredProcedure GetAll_Alumni_YearsOfPassing()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_GetAll_Alumni_PassingYears", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    #region "Stored Procedures"

    public static StoredProcedure ALM_SP_Alumni_GlobalSearch(string Keyword, int Mode)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_SearchAlumniby_Keywords", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Keyword", Keyword, DbType.String);
        sp.Command.AddParameter("@Mode", Mode, DbType.Int16);
        return sp;
    }

    public static StoredProcedure GetAll_ProfileRepeter_By_PassingYears(string passingYears, int mode)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_SearchAlumniby_Keywords", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Keyword", passingYears, DbType.String);
        sp.Command.AddParameter("@mode", mode, DbType.Int32);
        return sp;
    }

    #endregion

    protected void btnRoleSearch_Click(object sender, EventArgs e)
    {
        int Mode = 12;
        Anthem.Manager.IncludePageScripts = true;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(ddlRole.SelectedValue.Trim().ToString(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
        Anthem.Manager.IncludePageScripts = true;
    }


    protected void btnMemSearch_Click(object sender, EventArgs e)
    {
        int Mode = 13;
        Anthem.Manager.IncludePageScripts = true;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(ddlMembership.SelectedValue.Trim().ToString(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void btnGenderSearch_Click(object sender, EventArgs e)
    {
        int Mode = 14;
        Anthem.Manager.IncludePageScripts = true;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(ddlGender.SelectedValue.Trim().ToString(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void btnAlmSearch_Click(object sender, EventArgs e)
    {
        int Mode = 1;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(txtsearch.Text.Trim(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count.ToString() + " Profile Records";
            clear();
        }
    }

    protected void brnsearchpassing_Click(object sender, EventArgs e)
    {
        int Mode = 2;

        // Get selected passing years from CheckBoxList
        var selectedYears = chkPassingYear.Items.Cast<ListItem>()
                             .Where(i => i.Selected)
                             .Select(i => i.Value)
                             .ToList();

        // If no years are selected, return
        if (selectedYears.Count == 0)
        {
            lblProfileCnt.Text = "No records found.";
            return;
        }

        // Convert list to comma-separated string
        string passingYears = string.Join(",", selectedYears);

        // Call procedure with selected passing years
        DataSet ds = GetAll_ProfileRepeter_By_PassingYears(passingYears, Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds.Tables[0];
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
        }
        else
        {
            RepProfile.DataSource = null;
            RepProfile.DataBind();
            lblProfileCnt.Text = "No Profile Records Found";
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void btndegreesearch_Click(object sender, EventArgs e)
    {
        int Mode = 3;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(D_DrpDegree.SelectedItem.Text.Trim(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
    }

    protected void Btndeptsearch_Click(object sender, EventArgs e)
    {
        int Mode = 4;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Drp_Dept.SelectedValue.Trim().ToString(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
    }

    protected void btnaddress_Click(object sender, EventArgs e)
    {
        int Mode = 5;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Drp_DAddress.Text.Trim(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
    }

    protected void btncomp_Click(object sender, EventArgs e)
    {
        int Mode = 6;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Drp_Comp.SelectedItem.Text.Trim().ToString(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
    }

    protected void btndesig_Click(object sender, EventArgs e)
    {
        int Mode = 7;
        Anthem.Manager.IncludePageScripts = true;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Drp_Desig.SelectedItem.Text.Trim(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
    }

    protected void Btnskill_Click(object sender, EventArgs e)
    {
        int Mode = 8;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Drp_Skills.SelectedItem.Text.Trim(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
    }

    private void BindDropdown()
    {
        DataSet ds = new DataSet();
        //dt = Getmemberprofile(AlumniID);
        ds = Get_SelectionCriteria();
        if (ds.Tables[0].Rows.Count > 0)
        {
            //if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    D_ddlSearchAlumni.DataSource = ds.Tables[0];
            //    D_ddlSearchAlumni.DataTextField = "alumni_name";
            //    D_ddlSearchAlumni.DataValueField = "pk_alumniid";
            //    D_ddlSearchAlumni.DataBind();
            //    D_ddlSearchAlumni.Items.Insert(0, "-- Select alumni name -- ");
            //}
            //else
            //{
            //    D_ddlSearchAlumni.Items.Insert(0, "-- Select alumni name -- ");
            //}

            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                //D_ddlPassingYear.DataSource = ds.Tables[1];
                //D_ddlPassingYear.DataTextField = "PassYear_Name";
                //D_ddlPassingYear.DataValueField = "Pk_pass_id";
                //D_ddlPassingYear.DataBind();
                //D_ddlPassingYear.Items.Insert(0, "-- Select Passing Year -- ");
            }
            else
            {
                //D_ddlPassingYear.Items.Insert(0, "-- Select Passing Year -- ");
            }

            if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            {
                Drp_Dept.DataSource = ds.Tables[2];
                Drp_Dept.DataTextField = "description";
                Drp_Dept.DataValueField = "description";
                Drp_Dept.DataBind();
                Drp_Dept.Items.Insert(0, new ListItem("-- Department --", "0"));
            }
            else
            {
                Drp_Dept.Items.Insert(0, new ListItem("-- Department --", "0"));
            }

            if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
            {
                Drp_DAddress.DataSource = ds.Tables[3];
                Drp_DAddress.DataTextField = "Current_location";
                Drp_DAddress.DataValueField = "Current_location";
                Drp_DAddress.DataBind();
                Drp_DAddress.Items.Insert(0, new ListItem("-- Location --", "0"));
            }
            else
            {
                Drp_DAddress.Items.Insert(0, new ListItem("-- Location --", "0"));
            }

            if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
            {
                Drp_Desig.DataSource = ds.Tables[4];
                Drp_Desig.DataTextField = "designation";
                Drp_Desig.DataValueField = "designation";
                Drp_Desig.DataBind();
                Drp_Desig.Items.Insert(0, new ListItem("-- Designation --", "0"));
            }
            else
            {
                Drp_Desig.Items.Insert(0, new ListItem("-- Designation --", "0"));
            }

            if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
            {
                Drp_Skills.DataSource = ds.Tables[5];
                Drp_Skills.DataTextField = "special_interest";
                Drp_Skills.DataValueField = "special_interest";
                Drp_Skills.DataBind();
                Drp_Skills.Items.Insert(0, new ListItem("-- Skill --", "0"));
            }
            else
            {
                Drp_Skills.Items.Insert(0, new ListItem("-- Skill --", "0"));
            }


            if (ds.Tables[6] != null && ds.Tables[6].Rows.Count > 0)
            {
                D_DrpDegree.DataSource = ds.Tables[6];
                D_DrpDegree.DataTextField = "description";
                D_DrpDegree.DataValueField = "pk_degreeid";
                D_DrpDegree.DataBind();
                D_DrpDegree.Items.Insert(0, new ListItem("-- Degree --", "0"));
            }
            else
            {
                D_DrpDegree.Items.Insert(0, new ListItem("-- Degree --", "0"));
            }

            if (ds.Tables[7] != null && ds.Tables[7].Rows.Count > 0)
            {
                Drp_Comp.DataSource = ds.Tables[7];
                Drp_Comp.DataTextField = "currentoccupation";
                Drp_Comp.DataValueField = "currentoccupation";
                Drp_Comp.DataBind();
                Drp_Comp.Items.Insert(0, new ListItem("-- Company --", "0"));
            }
            else
            {
                Drp_Comp.Items.Insert(0, new ListItem("-- Company --", "0"));
            }
        }

    }

    private DataSet Get_SelectionCriteria()
    {
        ClearArrayLists();
        // return Dobj.GetDataTable("ALM_GetAlumniprofile", values, names, types);
        return Dobj.GetDataSet("Search_AlumniCriteria", values, names, types);
    }
}