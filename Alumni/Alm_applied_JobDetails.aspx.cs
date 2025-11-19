using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using System.IO;
using DataAccessLayer;
using CrystalDecisions.CrystalReports.Engine;
using NPOI.HSSF.Util;
using NPOI.HSSF.UserModel;
using System.Xml;

public partial class Alumni_Alm_applied_JobDetails : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public string xmldoc { get; private set; }
    public object Pk_JobPostedId { get; private set; }
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    crypto crp = new crypto();
    /// <summary>
    /// Clear ArrayList of Sp
    /// </summary>
    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    /// <summary>
    /// Sp of Rejected Request
    /// </summary>
    /// <returns></returns>
    private DataSet Candidate_List()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("ALM_GetAppliedCandidateList", values, names, types);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetList();
    }

    public void GetList()
    {
        DataSet ds = Candidate_List();
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
            }
        }
    }

    public string SetServiceDoc(string FileName)
    {
        string FolderName = @"/PLACEMENT_DATA/Company_Profile/PLACEMENT_DATA/Company_Profile/";
        string host = HttpContext.Current.Request.Url.Host;
        string FilePath = "";
        DataSet dsFilepath = new DataSet();
        XmlTextReader xmlreader = new XmlTextReader(Server.MapPath("~/UMM/IO_Config.xml"));
        dsFilepath.ReadXml(xmlreader);
        xmlreader.Close();

        foreach (DataRow dr in dsFilepath.Tables[0].Rows)
        {
            if (host == dr["Server_Ip"].ToString().Trim())
            {
                if (host != "localhost")
                {
                    //FilePath = dr["http_Add"].ToString().Trim() + "//Alumni//StuImage// "+ FileName;
                    FilePath = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/" + FileName;
                }
                else
                {
                    //FilePath = dr["http_Add"].ToString().Trim();
                    FilePath = @FilePath + FolderName + FileName;
                }
                return FilePath;
            }
        }
        return FilePath;
    }

    public string ReturnPath()
    {
        try
        {
            string host = HttpContext.Current.Request.Url.Host;
            DataSet dsFilepath = new DataSet();
            dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
            foreach (DataRow dr in dsFilepath.Tables[0].Rows)
            {
                if (host == dr["Server_Ip"].ToString().Trim())
                {
                    return dr["http_Add"].ToString().Trim();
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}