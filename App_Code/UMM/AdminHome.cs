using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;
using DataAccessLayer;

/// <summary>
/// Summary description for AdminHome
/// </summary>
public class AdminHome
{
    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
    private string _fk_sessionid, _fk_collegetypeid, _fk_collegeid, _fk_dtypeid, _fk_degreeid, _fk_cutoffid, _collegecat, _fromdate, _todate;

    public string fk_sessionid { get { return _fk_sessionid; } set { _fk_sessionid = value; } }
    public string fk_collegetypeid { get { return _fk_collegetypeid; } set { _fk_collegetypeid = value; } }
    public string fk_collegeid { get { return _fk_collegeid; } set { _fk_collegeid = value; } }
    public string fk_dtypeid { get { return _fk_dtypeid; } set { _fk_dtypeid = value; } }
    public string fk_degreeid { get { return _fk_degreeid; } set { _fk_degreeid = value; } }
    public string fk_cutoffid { get { return _fk_cutoffid; } set { _fk_cutoffid = value; } }
    public string collegecat { get { return _collegecat; } set { _collegecat = value; } }
    public string fromdate { get { return _fromdate; } set { _fromdate = value; } }
    public string todate { get { return _todate; } set { _todate = value; } }

    private void clearall()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    public DataTable PopulateSession()
    {
        clearall();
        return DAobj.GetDataReaderRetdt("ACD_AcademicSession_SelForddl");
    }

    public DataTable PopulateCollegeType()
    {
        clearall();
        return DAobj.GetDataReaderRetdt("ACD_CollegeType_Mst_SelForddl");
    }

    public DataTable PopulateCollege()
    {
        clearall();
        names.Add("@fk_collegetypeid"); values.Add(this._fk_collegetypeid); types.Add(SqlDbType.VarChar);
        return DAobj.GetDataReaderRetdt("ACD_College_SelForddl_OnType", values, names, types);
    }

    public DataTable PopulateDegreeType()
    {
        clearall();
        return DAobj.GetDataReaderRetdt("ACD_DegreeType_DDL_UGPG");
    }

    public DataTable PopulateDegree()
    {
        clearall();
        names.Add("@fk_dtypeid"); values.Add(this._fk_dtypeid); types.Add(SqlDbType.VarChar);
        return DAobj.GetDataReaderRetdt("ACD_Degree_DDL_OnDtypeid_Open", values, names, types);
    }

    public DataTable PopulateCutOff()
    {
        clearall();
        names.Add("@fk_sessionid"); values.Add(this._fk_sessionid); types.Add(SqlDbType.VarChar);
        return DAobj.GetDataReaderRetdt("PA_MeritList_Cutoff_Mst_DDL", values, names, types);
    }

    public DataSet GetCollegeDegreeRegistrationAdmissionDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            clearall();
            names.Add("@fk_sessionid"); values.Add(_fk_sessionid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_collegetypeid"); values.Add(this._fk_collegetypeid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_collegeid"); values.Add(_fk_collegeid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_dtypeid"); values.Add(this._fk_dtypeid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_degreeid"); values.Add(_fk_degreeid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_cutoffid"); values.Add(this._fk_cutoffid); types.Add(SqlDbType.VarChar);
            ds = DAobj.GetDataSet("PA_College_Degree_Registration_Admission_Graph", values, names, types);
        }
        catch
        {
        }
        return ds;
    }

    public DataTable PopulateCollege_OnCatType()
    {
        clearall();
        names.Add("@collegecat"); values.Add(this._collegecat); types.Add(SqlDbType.VarChar);
        names.Add("@fk_collegetypeid"); values.Add(this._fk_collegetypeid); types.Add(SqlDbType.VarChar);
        return DAobj.GetDataReaderRetdt("ACD_College_SelForddl_OnCatType", values, names, types);
    }

    public DataSet GetCollegeDegreeRegistrationDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            clearall();
            names.Add("@fk_sessionid"); values.Add(_fk_sessionid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_collegetypeid"); values.Add(this._fk_collegetypeid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_collegeid"); values.Add(_fk_collegeid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_dtypeid"); values.Add(this._fk_dtypeid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_degreeid"); values.Add(_fk_degreeid); types.Add(SqlDbType.VarChar);
            names.Add("@collegecat"); values.Add(this._collegecat); types.Add(SqlDbType.VarChar);
            names.Add("@from_date"); values.Add(this._fromdate); types.Add(SqlDbType.VarChar);
            names.Add("@to_date"); values.Add(this._todate); types.Add(SqlDbType.VarChar);
            ds = DAobj.GetDataSet("PA_College_Degree_Registration_Details", values, names, types);
        }
        catch
        {
        }
        return ds;
    }
}