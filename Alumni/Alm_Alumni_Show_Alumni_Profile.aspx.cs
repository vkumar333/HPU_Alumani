//==================================================================================    
//Modified By                                               :Aditya Sharma   
//On Date                                                   :5 Mar 2023    
//Name                                                      :Alm_alumni_Batchmates_fullProfile.aspx.aspx    
//Purpose                                                   :Get full info of Batchmates
//Tables used                                               :ALM_AlumniRegistration    
//Stored Procedures used                                    :    
//Modules                                                   :Alumni    
//Form                                                      :Alm_alumni_Batchmetes_ShortProfile.aspx  
//Last Updated Date                                         :    
//Last Updated By                                           :    
//========================================================================================
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using Anthem;
using System.IO;
using DataAccessLayer;

public partial class Alumni_Alm_Alumni_Show_Alumni_Profile : System.Web.UI.Page
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
    private DataTable Getmemberfullprofile(int alumniID)
    {
        ClearArrayLists();
        names.Add("@id"); types.Add(SqlDbType.Int); values.Add(alumniID);
        return Dobj.GetDataTable("ALM_GetBatchmateFullProfile", values, names, types);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "" || Request.QueryString["Alumni"] != null && Request.QueryString["Alumni"] != "")
            {
                try
                {
                    if (Request.QueryString["ID"] != null)
                    {
                        int alumniID = Convert.ToInt32(Request.QueryString["ID"]);
                        ProfileImgRepeter(alumniID);
                        BasicDetailsRepeaters(alumniID);
                        headingRepeter(alumniID);
                        BasicdetailsRepeter(alumniID);
                        EducationRepeter(alumniID);
                        WorkExpRepeter(alumniID);
                        contactRepeter(alumniID);
                        membershipRepeter(alumniID);
                    }
                    else if (Request.QueryString["Alumni"] != null)
                    {
                        int alumniID = Convert.ToInt32(Request.QueryString["Alumni"]);
                        ProfileImgRepeter(alumniID);
                        BasicDetailsRepeaters(alumniID);
                        headingRepeter(alumniID);
                        BasicdetailsRepeter(alumniID);
                        EducationRepeter(alumniID);
                        WorkExpRepeter(alumniID);
                        contactRepeter(alumniID);
                        membershipRepeter(alumniID);
                    }
                }
                catch (Exception Ex)
                {
                    string message = Ex.Message;
                    //Response.Redirect(Page.ResolveUrl("~/UserLogin.html"));
                }
            }
            else
            {
                //Response.Redirect(Page.ResolveUrl("~/UserLogin.html"));
            }
        }
    }
    /// <summary>
    /// Repeater of heading
    /// </summary>
    /// <param name="alumniID"></param>
    private void headingRepeter(int alumniID)
    {
        DataTable dt = new DataTable();
        dt = Getmemberfullprofile(alumniID);
        if (dt.Rows.Count > 0)
        {
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
    }
    /// <summary>
    /// BasicdetailsRepeter
    /// </summary>
    /// <param name="alumniID"></param>
    private void BasicdetailsRepeter(int alumniID)
    {
        DataTable dt = new DataTable();
        dt = Getmemberfullprofile(alumniID);
        if (dt.Rows.Count > 0)
        {
            basicdetailsRep.DataSource = dt;
            basicdetailsRep.DataBind();
        }
    }

    /// <summary>
    /// Education details Repeter
    /// </summary>
    /// <param name="alumniID"></param>
    private void EducationRepeter(int alumniID)
    {
        DataTable dt = new DataTable();
        dt = Getmemberfullprofile(alumniID);
        if (dt.Rows.Count > 0)
        {
            RepEducation.DataSource = dt;
            RepEducation.DataBind();
        }
    }
    /// <summary>
    /// WorkExp details Repeter
    /// </summary>
    /// <param name="alumniID"></param>
    private void WorkExpRepeter(int alumniID)
    {
        DataTable dt = new DataTable();
        dt = Getmemberfullprofile(alumniID);
        if (dt.Rows.Count > 0)
        {
            workRepeater.DataSource = dt;
            workRepeater.DataBind();
        }
    }

    /// <summary>
    /// contact details Repeter
    /// </summary>
    /// <param name="alumniID"></param>
    private void contactRepeter(int alumniID)
    {
        DataTable dt = new DataTable();
        dt = Getmemberfullprofile(alumniID);
        if (dt.Rows.Count > 0)
        {
            ContactRepeater.DataSource = dt;
            ContactRepeater.DataBind();
        }
    }

    /// <summary>
    /// membership details Repeter
    /// </summary>
    /// <param name="alumniID"></param>
    private void membershipRepeter(int alumniID)
    {
        DataTable dt = new DataTable();
        dt = Getmemberfullprofile(alumniID);
        if (dt.Rows.Count > 0)
        {
            ContactRepeater.DataSource = dt;
            ContactRepeater.DataBind();
        }
    }

    /// <summary>
    /// Basic details Repeter
    /// </summary>
    /// <param name="alumniID"></param>
    private void BasicDetailsRepeaters(int alumniID)
    {
        DataTable dt = new DataTable();
        dt = Getmemberfullprofile(alumniID);
        if (dt.Rows.Count > 0)
        {
            profileRepeater.DataSource = dt;
            profileRepeater.DataBind();
        }
    }

    /// <summary>
    /// ProfileImg details Repeter
    /// </summary>
    /// <param name="alumniID"></param>
    private void ProfileImgRepeter(int alumniID)
    {
        DataTable dt = new DataTable();
        dt = Getmemberfullprofile(alumniID);

        if (dt.Rows.Count > 0)
        {
            // string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
            // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
            ReprofileImg.DataSource = dt;
            ReprofileImg.DataBind();
        }
    }

    protected void lnkPay_Click(object sender, EventArgs e)
    {

    }
}