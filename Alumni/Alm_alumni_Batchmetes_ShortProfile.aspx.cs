using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using Anthem;
using System.IO;
using DataAccessLayer;

public partial class Alumni_Alm_alumni_Batchmetes_ShortProfile : System.Web.UI.Page
{
    crypto crp = new crypto();
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    /// <summary>
    /// Stored Procedure Section Start..
    /// </summary>
    /// <param name="yesrofpassing"></param>
    /// <param name="subjectid"></param>
    /// <returns></returns>
    private DataTable Getmemberbyyear(string yesrofpassing, int subjectid)
    {
        ClearArrayLists();
        names.Add("@yearofpassing"); types.Add(SqlDbType.NVarChar); values.Add(yesrofpassing);
        names.Add("@subjectid"); types.Add(SqlDbType.NVarChar); values.Add(subjectid);
        return Dobj.GetDataTable("ALM_GetMemberbySubject", values, names, types);
    }

    private DataTable Getmemberprofile(string yesrofpassing, int subjectid)
    {
        ClearArrayLists();
        names.Add("@yearofpassing"); types.Add(SqlDbType.NVarChar); values.Add(yesrofpassing);
        names.Add("@subjectid"); types.Add(SqlDbType.NVarChar); values.Add(subjectid);
        return Dobj.GetDataTable("ALM_GetMemberprofile", values, names, types);
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Alumnitype"].ToString() == "P")
        {
            if (Session["AlumniID"] != null)
            {
                if (Request.QueryString["degree"] != null && Request.QueryString["subject"] != null && Request.QueryString["Year"] != null)
                {
                    try
                    {
                        //string yesrofpassing = Request.QueryString["Year"].ToString();
                        //int subjectid = Convert.ToInt32(Request.QueryString["subject"]);

                        string yesrofpassing = crp.DecodeString(Request.QueryString["Year"].ToString());
                        int subjectid = Convert.ToInt32(crp.DecodeString(Request.QueryString["subject"].ToString()));

                        DataSet ds = new DataSet();
                        // ds = getbatchmates();

                        headingRepeter(yesrofpassing, subjectid);
                        ProfileRepeter(yesrofpassing, subjectid);
                    }
                    catch (Exception Ex)
                    {
                        string message = Ex.Message;
                        Response.Redirect(Page.ResolveUrl("Alm_Alumni_Batchmates_member.aspx"));
                    }
                }
                else
                {
                    Response.Redirect(Page.ResolveUrl("Alm_Alumni_Batchmates_member.aspx"));
                }
            }
            else
            {
                Response.Redirect("Alm_Alumni_Batchmates_member.aspx");
            }
        }
        else
        {
            Response.Redirect("Alm_Alumni_Batchmates_member.aspx");
        }
    }

    /// <summary>
    /// Repeater for Heading
    /// </summary>
    /// <param name="yesrofpassing"></param>
    /// <param name="subjectid"></param>
    private void headingRepeter(string yesrofpassing, int subjectid)
    {
        DataTable dt = new DataTable();
        dt = Getmemberbyyear(yesrofpassing, subjectid);
        if (dt.Rows.Count > 0)
        {
            headingrep.DataSource = dt;
            headingrep.DataBind();
        }        
    }

    /// <summary>
    /// Get Image and other informations
    /// </summary>
    /// <param name="yesrofpassing"></param>
    /// <param name="subjectid"></param>
    private void ProfileRepeter(string yesrofpassing, int subjectid)
    {
        DataTable dt = new DataTable();
        dt = Getmemberprofile(yesrofpassing, subjectid);

        //if (dt.Rows.Count > 0)
        //{
        //    // string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
        //    // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
        //    profileRepeater.DataSource = dt;
        //    profileRepeater.DataBind();
        //}

        dt.Columns.Add("encId");

        if (dt.Rows.Count > 0)
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                string pkid = dt.Rows[x]["pk_alumniid"].ToString();
                string encId = crp.EncodeString(Convert.ToInt32(pkid));
                dt.Rows[x]["encId"] = encId;
            }
            profileRepeater.DataSource = dt;
            profileRepeater.DataBind();
        }
    }
}