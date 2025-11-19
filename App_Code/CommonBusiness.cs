using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;
using DataAccessLayer;
using System.Net;

/// <summary>
/// Summary description for CommonBusiness
/// </summary>
public class CommonBusiness
{
	public CommonBusiness()
	{
		//
		// TODO: Add constructor logic here 
		//
	}

    #region Common member declaration
    //Array Parameter Declared
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    public DataAccessLayer.DataAccess DAObj = new DataAccessLayer.DataAccess();
    private string _pkid;
    private string _userid;
    private string _locid;
    private string _Doc;
    private int _grid;
    private int _pageindex, _pagesize;

    //audit trail properties
    private string _a_fk_userid;
    private string _a_formname;
    private string _a_fk_action;
    private string _a_fk_IPaddress;
    private string _a_computername;
    private string _a_IEversion;
    private string _a_actiondate;
    private string _a_primarykey;
    private string _a_tablename;
    private string _a_locid;

    #endregion

    #region Common Property  
     
    public int grid { get { return _grid; } set { _grid = value; } }
    public string pkid { get { return _pkid; } set { _pkid = value; } }
    public string Doc { get { return _Doc; } set { _Doc = value; } }
    public string userid { get { return _userid; } set { _userid = value; } }
    public string  locid { get { return _locid; } set { _locid = value; } }
    public int pageindex { get { return _pageindex; } set { _pageindex = value; } }
    public int pagesize { get { return _pagesize; } set { _pagesize = value; } }

    //audit trail properties
    public string afk_userid { get { return _a_fk_userid; } set { _a_fk_userid = value; } }
    public string aformname { get { return _a_formname; } set { _a_formname = value; } }
    public string aaction { get { return _a_fk_action; } set { _a_fk_action = value; } }
    public string aIPaddress { get { return _a_fk_IPaddress; } set { _a_fk_IPaddress = value; } }
    public string acomputername { get { return _a_computername; } set { _a_computername = value; } }
    public string aIEversion { get { return _a_IEversion; } set { _a_IEversion = value; } }
    public string aactiondate { get { return _a_actiondate; } set { _a_actiondate = value; } }
    public string aprimarykey { get { return _a_primarykey; } set { _a_primarykey = value; } }
    public string atablename { get { return _a_tablename; } set { _a_tablename = value; } }
    public string alocid { get { return _a_locid; } set { _a_locid = value; } }


    #endregion

    #region strored Procedure
    
     

    #endregion

    #region Common methods
    /// <summary>
    /// For Clear Array List 
    /// </summary>
    public void ClearArrayLists()
    {
        names.Clear();
        types.Clear();
        values.Clear();
    }

    //to get the common Academic session drop down
    public DataTable PopulateSession()
    {
        ClearArrayLists();
        return DAObj.GetDataTable("Admin_AcademicSession_DDL", values, names, types);
    }

    // to get the Audit trail data
   public DataSet GetAuditTrailDtl()
    { 
       //to get the auditTrail data
        _a_fk_IPaddress = HttpContext.Current.Request.UserHostAddress.ToString();
        _a_IEversion = HttpContext.Current.Request.Browser.Browser.ToString() + "-" + HttpContext.Current.Request.Browser.MajorVersion.ToString(); 
        DataSet dsAudit = new DataSet();
        dsAudit = GetSchema("AuditTrial");
        DataRow drAudit;
        drAudit = dsAudit.Tables[0].NewRow();
        drAudit["pk_auditid"] = "0";
        drAudit["fk_userid"] = _a_fk_userid;
        drAudit["formname"] = _a_formname;
        drAudit["action"] = _a_fk_action;
        drAudit["IPaddress"] = _a_fk_IPaddress;
        drAudit["computername"] = _a_computername;
        drAudit["IEversion"] = _a_IEversion;
        drAudit["actiondate"] = _a_actiondate;
        drAudit["primarykey"] = _a_primarykey;
        drAudit["tablename"] = _a_tablename;
        drAudit["locid"] = _a_locid;
        dsAudit.Tables[0].Rows.Add(drAudit);
        return dsAudit;
    }


    public int CommonSave(string procedurename, ref string Message)
    {
        try
        {
            names.Add("@doc"); values.Add(Doc); types.Add(SqlDbType.VarChar);
            if (DAObj.ExecuteTransactionMsg(procedurename, values, names, types, ref Message) > 0)
            {
                Message = DAObj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch { throw; }
    }       
    public int CommonUpdate(string procedurename, ref string Message)
    {
        try
        {
            names.Add("@pkid"); values.Add(pkid); types.Add(SqlDbType.VarChar);
            names.Add("@doc"); values.Add(Doc); types.Add(SqlDbType.VarChar);
            if (DAObj.ExecuteTransactionMsg(procedurename, values, names, types, ref Message) > 0)
            {
                Message = DAObj.ShowMessage("U", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch { throw; }
    }
    public int CommonDelete(string procedurename, ref string Message)
    {
        try
        {
            names.Add("@doc"); values.Add(Doc); types.Add(SqlDbType.VarChar);
            names.Add("@pkid"); values.Add(pkid); types.Add(SqlDbType.VarChar);
            if (DAObj.ExecuteTransactionMsg(procedurename, values, names, types, ref Message) > 0)
            {
                Message = DAObj.ShowMessage("D", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch { throw; }
    }
    public DataSet CommonEdit_DataSet(string procedurename)
    {
        try
        {
            ClearArrayLists();
            names.Add("@pkid"); values.Add(pkid); types.Add(SqlDbType.VarChar);
            return DAObj.GetDataSet(procedurename, values, names, types);
        }
        catch { throw; }
    }
    public DataRow CommonEdit_DataRow(string procedurename)
    {
        try
        {

            names.Add("@pkid"); values.Add(pkid); types.Add(SqlDbType.VarChar);
            return DAObj.GetDataRow(procedurename, values, names, types);
        }
        catch { throw; }
    }
    public DataSet CommonFillGrid(string procedurename, DataGrid dgGrid)
    {
        try
        {
            ClearArrayLists();
            names.Add("@pageindex"); values.Add(pageindex); types.Add(SqlDbType.Int);
            names.Add("@pagesize"); values.Add(pagesize); types.Add(SqlDbType.Int);
            return DAObj.Grid_CustomPagingRet(procedurename, values, names, types, dgGrid, pageindex);
        }
        catch { throw; }
    }
    public DataSet FillGrid(string procedurename)
    {
       
        names.Add("@locid"); values.Add(locid); types.Add(SqlDbType.VarChar);
        return DAObj.GetDataSet(procedurename, values, names, types);
    }
    /// <summary>
    /// To Get Schema Of Tables
    /// </summary>
    public DataSet GetSchema(string TableName)
    {
        try
        {
            ClearArrayLists();
            return DAObj.GetSchema(TableName);
        }
        catch { throw; }
    }
    public string GetBtnCaption(string Key)
    {
        try
        {
            return DAObj.GetSysParaMessages(Key);
        }
        catch { throw; }
    }
    public string GetSysMessage(string Key)
    {
        try
        {
            return DAObj.GetSysParaMessages(Key);
        }
        catch { throw; }
    }
    public void DownLoadfile(string FName)
    {
        string path = FName;
        System.IO.FileInfo file = new System.IO.FileInfo(path);
        if (file.Exists)
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.WriteFile(file.FullName);
            HttpContext.Current.Response.End();

        }
        else
        {
            HttpContext.Current.Response.Write("This file does not exist.");
        }

    }
    #endregion
}