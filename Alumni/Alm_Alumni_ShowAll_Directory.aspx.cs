//==================================================================================    
//Modified By                                               : Aditya Sharma   
//On Date                                                   : 5 Mar 2023    
//Name                                                      : Alm_alumni_Batchmetes_ShortProfile.aspx.aspx    
//Purpose                                                   : Alm_alumni_Batchmetes_ShortProfile.aspx  
//Tables used                                               : ALM_AlumniRegistration    
//Stored Procedures used                                    :     
//Modules                                                   : Alumni    
//Form                                                      : Alm_alumni_Batchmetes_ShortProfile.aspx  
//Last Updated Date                                         :    
//Last Updated By                                           :    
//========================================================================================
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using Anthem;
using System.IO;
using SubSonic;
using DataAccessLayer;

public partial class Alumni_Alm_Alumni_ShowAll_Directory : System.Web.UI.Page
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
        return Dobj.GetDataTable("ALM_Get_All_Alumniprofile", values, names, types);
    }
	
    private DataTable getAlumnibyId()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_Get_All_Alumniprofile", values, names, types);
    }
	
    private DataTable Getmemberprofile(int AlumniID)
    {
        ClearArrayLists();
        names.Add("@alumni_ID"); types.Add(SqlDbType.NVarChar); values.Add(AlumniID);
        return Dobj.GetDataTable("ALM_GetprofileID", values, names, types);
    }
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Alumnitype"].ToString() == "P")
        {
            if (Session["AlumniID"] != null)
            {
                if (Request.QueryString["Alumni"] != null)
                {
                    try
                    {
                        int AlumniID = Convert.ToInt32(Request.QueryString["Alumni"]);
                        DataSet ds = new DataSet();
                        clear();
                        ProfileRepeter(AlumniID);
                        Fill_PassingYear();
                    }
                    catch (Exception Ex)
                    {
                        string message = Ex.Message;
                        Response.Redirect(Page.ResolveUrl("Alm_Alumni_Directory.aspx"));
                    }
                }
                else
                {
                    if (!IsPostBack)
                    {
                        clear();
						CountRepeter();
                        ProfileRepeter();
                        Fill_PassingYear();
                    }
                    //Response.Redirect(Page.ResolveUrl("Alm_Alumni_Directory.aspx"));
                }
            }
            else
            {
                Response.Redirect("Alm_Alumni_Directory.aspx");
            }
        }
        else
        {
            Response.Redirect("Alm_Alumni_Directory.aspx");
        }
    }

    protected void Fill_PassingYear()
    {
        //D_ddlYeofPass.Items.Clear();
        //DataSet ds = IUMSNXG.SP.ALM_Sp_AlumniBatchYear_Passing_Year().GetDataSet();
        //if (ds.Tables[1].Rows.Count > 0)
        //{
        //    D_ddlYeofPass.DataSource = ds.Tables[1];
        //    D_ddlYeofPass.DataTextField = "description";
        //    D_ddlYeofPass.DataValueField = "pk_yearID";
        //    D_ddlYeofPass.DataBind();
        //    D_ddlYeofPass.Items.Insert(0, "-- Select a Year of Passing -- ");
        //}
        //else
        //{
        //    D_ddlYeofPass.Items.Insert(0, "-- Select a Year of Passing -- ");
        //}
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
			//string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
            RepProfile.DataSource = dt;
            RepProfile.DataBind();
        }
    }
	
    // <summary>
    /// Get Image and other informations
    /// </summary>
    /// <param name="yesrofpassing"></param>
    /// <param name="subjectid"></param>
    private void ProfileRepeter(int AlumniID)
    {
        DataTable dt = new DataTable();
        dt = Getmemberprofile(AlumniID);

        if (dt.Rows.Count > 0)
        {
            // string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
            // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
            RepProfile.DataSource = dt;
            RepProfile.DataBind();
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        int Mode = 1;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(txtsearch.Text.Trim(),Mode).GetDataSet();
        if (ds.Tables.Count > 0)
        {
            // string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
            // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            clear();
        }
    }
	
    public static StoredProcedure ALM_SP_Alumni_GlobalSearch(string Keyword, int Mode)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_SearchAlumniby_Keywords", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Keyword", Keyword, DbType.String);
        sp.Command.AddParameter("@Mode", Mode, DbType.Int16);       
        return sp;
    }

    public static StoredProcedure ALM_SP_Alumni_ddl_search(string yearofpassing, string alumni_name, string currentoccupation, string fk_Deptid, string fk_degreeid, string designation)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_SearchAlumniby_DDl", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@yearofpassing", yearofpassing, DbType.String);
        sp.Command.AddParameter("@alumni_name", alumni_name, DbType.String);
        sp.Command.AddParameter("@currentoccupation", currentoccupation, DbType.String);
        sp.Command.AddParameter("@fk_Deptid", fk_Deptid, DbType.String);
        sp.Command.AddParameter("@fk_degreeid", fk_degreeid, DbType.String);
        sp.Command.AddParameter("@designation", designation, DbType.String); 
        return sp;
    }
    
    protected void brnsearchpassing_Click(object sender, EventArgs e)
    {
        int Mode = 2;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Txtyearofpassing.Text.Trim(), Mode).GetDataSet();
        if (ds.Tables.Count > 0)
        {
            // string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
            // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            clear();
        }
    }

    protected void btndegreesearch_Click(object sender, EventArgs e)
    {
        int Mode = 3;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Txtdegree.Text.Trim(), Mode).GetDataSet();
        if (ds.Tables.Count > 0)
        {
			// string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
			// string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            clear();
        }
    }

    protected void Btndeptsearch_Click(object sender, EventArgs e)
    {
        int Mode = 4;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Txtdept.Text.Trim(), Mode).GetDataSet();
        if (ds.Tables.Count > 0)
        {
           // string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
		   // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            clear();
        }
    }

    protected void btnaddress_Click(object sender, EventArgs e)
    {
        int Mode = 5;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Txtaddress.Text.Trim(), Mode).GetDataSet();
        if (ds.Tables.Count > 0)
        {
           // string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
		   // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            clear();
        }
    }

    protected void btncomp_Click(object sender, EventArgs e)
    {
        int Mode = 6;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Txtcomp.Text.Trim(), Mode).GetDataSet();
        if (ds.Tables.Count > 0)
        {
            // string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
		    // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            clear();
        }        
    }

    protected void btndesig_Click(object sender, EventArgs e)
    {
        int Mode = 7;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Txtdesig.Text.Trim(), Mode).GetDataSet();
        if (ds.Tables.Count > 0)
        {
			// string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
			// string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            clear();
        }
    }

    protected void Btnskill_Click(object sender, EventArgs e)
    {
        int Mode = 8;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Txtskill.Text.Trim(), Mode).GetDataSet();
        if (ds.Tables.Count > 0)
        {
			// string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
			// string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            clear();
        }
    }

    public void clear()
    {
        txtsearch.Text = "";
        Txtaddress.Text = "";
        Txtcomp.Text = "";
        Txtdegree.Text = "";
        Txtdept.Text = "";
        Txtdesig.Text = "";
        Txtskill.Text = "";
        Txtyearofpassing.Text = "";
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

    private DataTable Getmtotalcount()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GettotalAlumni_Count", values, names, types);
    }
}


