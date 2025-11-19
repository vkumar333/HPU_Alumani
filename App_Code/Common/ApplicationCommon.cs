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
using DataAccessLayer;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ApplicationCommon
/// </summary>
public class ApplicationCommon
{
    #region member declaration

    string IPaddress = "", computername = "", IEversion = "";
    public DataAccess DAobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList types = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList size = new ArrayList();
    public ArrayList outtype = new ArrayList();
    public ArrayList ar_Result = new ArrayList();

    private string _sal_bill_id, _DateAsOn;
    private string _userid;
    private string _locid;
    private string _fk_monthid;
    private string _fk_yearid;
    private string _Doc;
    private string _fromdate;
    private string _todate;
    private string _finid;
    private int _sectorid;
    private int _lcid;
    private int _anc;
    private string _pkid;
    private string _ddoid;
    private string _vtid;
    private int _billtypeid;
    private string _fundid;
    private string _lid;
    private string _paytolocation;
    private string _Clientcomputername;
    private string _Fk_InsUserId;
    private string _Fk_UpdUserId;
    private string _Fk_InsDateId;
    private string _Fk_UpdDateId;
    private int _BudgetHead;
    private int _Pk_ApplevelId;
    private decimal _amount;
    // private string _voucheramount;

    #endregion

    #region Property

    public string DateAsOn
    {
        get
        {
            return _DateAsOn;
        }
        set
        {
            _DateAsOn = value;
        }
    }
    public decimal amount
    {
        get
        {
            return _amount;
        }
        set
        {
            _amount = value;
        }
    }
    public string Fk_InsUserId
    {
        get
        {
            return _Fk_InsUserId;
        }

        set
        {
            _Fk_InsUserId = value;
        }
    }
    public string Fk_UpdUserId
    {
        get
        {
            return _Fk_UpdUserId;
        }

        set
        {
            _Fk_UpdUserId = value;
        }
    }
    public string Fk_InsDateId
    {
        get
        {
            return _Fk_InsDateId;
        }

        set
        {
            _Fk_InsDateId = value;
        }
    }
    public string Fk_UpdDateId
    {
        get
        {
            return _Fk_UpdDateId;
        }

        set
        {
            _Fk_UpdDateId = value;
        }
    }
    public string fk_yearid
    {
        get
        {
            return _fk_yearid;
        }

        set
        {
            _fk_yearid = value;
        }
    }
    public string fk_monthid
    {
        get
        {
            return _fk_monthid;
        }

        set
        {
            _fk_monthid = value;
        }
    }


    public string sal_bill_id
    {
        get
        {
            return _sal_bill_id;
        }

        set
        {
            _sal_bill_id = value;
        }
    }

    public string lid
    {
        get
        {
            return _lid;
        }
        set
        {
            _lid = value;
        }
    }

    public int billtypeid
    {
        get
        {
            return _billtypeid;
        }
        set
        {
            _billtypeid = value;
        }
    }

    public string fundid
    {
        get
        {
            return _fundid;
        }
        set
        {
            _fundid = value;
        }
    }
    public string vtid
    {
        get
        {
            return _vtid;
        }
        set
        {
            _vtid = value;
        }
    }

    public string Pk_Group_id { get; set; }

    public int anc
    {
        get
        {
            return _anc;
        }
        set
        {
            _anc = value;
        }
    }
    public int lcid
    {
        get
        {
            return _lcid;
        }
        set
        {
            _lcid = value;
        }
    }
    public int sectorid
    {
        get
        {
            return _sectorid;
        }
        set
        {
            _sectorid = value;
        }
    }

    public int fk_facultyid { get; set; }
    public int FkPDDOid { get; set; }
    public int fk_mdeptid { get; set; }

    public string ddoid
    {
        get
        {
            return _ddoid;
        }
        set
        {
            _ddoid = value;
        }
    }
    public string finid
    {
        get
        {
            return _finid;
        }
        set
        {
            _finid = value;
        }
    }
    public string pkid
    {
        get
        {
            return _pkid;
        }
        set
        {
            _pkid = value;
        }
    }

    public string Doc
    {
        get
        {
            return _Doc;
        }
        set
        {
            _Doc = value;
        }
    }
    public string userid
    {
        get
        {
            return _userid;
        }
        set
        {
            _userid = value;
        }
    }
    public string locid
    {
        get
        {
            return _locid;
        }
        set
        {
            _locid = value;
        }
    }

    public string paytolocation
    {
        get
        {
            return _paytolocation;
        }
        set
        {
            _paytolocation = value;
        }
    }

    public string fromdate
    {
        get { return _fromdate; }
        set
        {

            _fromdate = value;
        }
    }
    public string todate
    {
        get { return _todate; }
        set
        {

            _todate = value;
        }
    }
    public string Clientcomputername
    {
        get
        {
            return _Clientcomputername;
        }
        set
        {
            _Clientcomputername = value;
        }
    }
    public int BudgetHead
    {
        get
        {
            return _BudgetHead;
        }
        set
        {
            _BudgetHead = value;
        }
    }
    public int Pk_ApplevelId
    {
        get
        {
            return _Pk_ApplevelId;
        }
        set
        {
            _Pk_ApplevelId = value;
        }
    }
    #endregion

    #region strored Procedure
    private const string SP_Acct_Get_BillType = "Acct_Get_BillType";
    private const string SP_Acct_Get_FundType = "Acct_Get_FundType";
    private const string SP_Acct_Get_HeadCode = "Acct_Get_HeadCode";
    private const string SP_Acct_GetFinTag = "Acct_GetFinTag";
    private static string SP_Acct_CheckdateFromTo = "Acct_CheckdateFromTo";
    #endregion

    #region methods
    public ApplicationCommon()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// For Clear Arry List 
    /// </summary>
    public void ClearArrayLists()
    {
        names.Clear();
        types.Clear();
        values.Clear();
    }
    public int CommonSave(string procedurename, ref string Message)
    {
        try
        {
            //ClearArrayLists();
            names.Add("@locid"); values.Add(locid); types.Add(SqlDbType.VarChar);
            names.Add("@userid"); values.Add(userid); types.Add(SqlDbType.VarChar);
            if (DAobj.ExecuteTransactionMsg(procedurename, values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("S", "");
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
            names.Add("@locid"); values.Add(locid); types.Add(SqlDbType.VarChar);
            names.Add("@userid"); values.Add(userid); types.Add(SqlDbType.VarChar);
            if (DAobj.ExecuteTransactionMsg(procedurename, values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("U", "");
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

            names.Add("@pkid"); values.Add(pkid); types.Add(SqlDbType.VarChar);
            if (DAobj.ExecuteTransactionMsg(procedurename, values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("D", "");
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
            return DAobj.GetDataSet(procedurename, values, names, types);
        }
        catch { throw; }
    }
    public DataRow CommonEdit_DataRow(string procedurename)
    {
        try
        {
            names.Add("@pkid"); values.Add(pkid); types.Add(SqlDbType.VarChar);
            return DAobj.GetDataRow(procedurename, values, names, types);
        }
        catch { throw; }
    }


    public int CommonSaveWithAuditTrial(string procedurename, ref string Message)
    {
        try
        {
            IPaddress = HttpContext.Current.Request.UserHostAddress.ToString();
            IEversion = HttpContext.Current.Request.Browser.Browser.ToString() + "-" + HttpContext.Current.Request.Browser.MajorVersion.ToString();
            names.Add("@locid"); values.Add(locid); types.Add(SqlDbType.VarChar);
            names.Add("@userid"); values.Add(userid); types.Add(SqlDbType.VarChar);
            names.Add("@action"); values.Add("SAVE"); types.Add(SqlDbType.VarChar);
            names.Add("@computername"); values.Add(Clientcomputername); types.Add(SqlDbType.VarChar);
            names.Add("@IPaddress"); values.Add(IPaddress); types.Add(SqlDbType.VarChar);
            names.Add("@IEversion"); values.Add(IEversion); types.Add(SqlDbType.VarChar);
            if (DAobj.ExecuteTransactionMsg(procedurename, values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch { throw; }
    }

    // Common save  with return output
    public int CommonSaveOutWithAuditTrial(string procedurename, ref string Message, ref ArrayList ar_Result)
    {
        try
        {
            IPaddress = HttpContext.Current.Request.UserHostAddress.ToString();
            IEversion = HttpContext.Current.Request.Browser.Browser.ToString() + "-" + HttpContext.Current.Request.Browser.MajorVersion.ToString(); ;
            names.Add("@locid"); values.Add(locid); types.Add(SqlDbType.VarChar); size.Add(15); outtype.Add(false);
            names.Add("@userid"); values.Add(userid); types.Add(SqlDbType.VarChar); size.Add(15); outtype.Add(false);
            names.Add("@action"); values.Add("SAVE"); types.Add(SqlDbType.VarChar); size.Add(15); outtype.Add(false);
            names.Add("@computername"); values.Add(Clientcomputername); types.Add(SqlDbType.VarChar); size.Add(100); outtype.Add(false);
            names.Add("@IPaddress"); values.Add(IPaddress); types.Add(SqlDbType.VarChar); size.Add(25); outtype.Add(false);
            names.Add("@IEversion"); values.Add(IEversion); types.Add(SqlDbType.VarChar); size.Add(25); outtype.Add(false);
            names.Add("@voucherid"); values.Add("0"); types.Add(SqlDbType.VarChar); size.Add(15); outtype.Add("true");
            if (DAobj.ExecuteTransactionMsgIO(procedurename, values, names, types, size, outtype, ref Message, ref ar_Result) > 0)
            {
                Message = DAobj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch { throw; }
    }

    public int CommonUpdateWithAuditTrial(string procedurename, ref string Message)
    {
        try
        {
            IPaddress = HttpContext.Current.Request.UserHostAddress.ToString();
            IEversion = HttpContext.Current.Request.Browser.Browser.ToString() + "-" + HttpContext.Current.Request.Browser.MajorVersion.ToString(); ;
            names.Add("@pkid"); values.Add(pkid); types.Add(SqlDbType.VarChar);
            names.Add("@locid"); values.Add(locid); types.Add(SqlDbType.VarChar);
            names.Add("@userid"); values.Add(userid); types.Add(SqlDbType.VarChar);
            names.Add("@action"); values.Add("UPDATE"); types.Add(SqlDbType.VarChar);
            names.Add("@computername"); values.Add(Clientcomputername); types.Add(SqlDbType.VarChar);
            names.Add("@IPaddress"); values.Add(IPaddress); types.Add(SqlDbType.VarChar);
            names.Add("@IEversion"); values.Add(IEversion); types.Add(SqlDbType.VarChar);
            if (DAobj.ExecuteTransactionMsg(procedurename, values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("U", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch { throw; }
    }
    public int CommonDeleteWithAuditTrial(string procedurename, ref string Message)
    {
        try
        {
            IPaddress = HttpContext.Current.Request.UserHostAddress.ToString();
            IEversion = HttpContext.Current.Request.Browser.Browser.ToString() + "-" + HttpContext.Current.Request.Browser.MajorVersion.ToString(); ;
            names.Add("@pkid"); values.Add(pkid); types.Add(SqlDbType.VarChar);
            names.Add("@userid"); values.Add(userid); types.Add(SqlDbType.VarChar);
            names.Add("@locid"); values.Add(locid); types.Add(SqlDbType.VarChar);
            names.Add("@action"); values.Add("DELETE"); types.Add(SqlDbType.VarChar);
            names.Add("@computername"); values.Add(Clientcomputername); types.Add(SqlDbType.VarChar);
            names.Add("@IPaddress"); values.Add(IPaddress); types.Add(SqlDbType.VarChar);
            names.Add("@IEversion"); values.Add(IEversion); types.Add(SqlDbType.VarChar);
            if (DAobj.ExecuteTransactionMsg(procedurename, values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("D", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch { throw; }
    }

    /// <summary>
    /// To Get Schema Of Tables
    /// </summary>
    public DataSet GetSchema(string TableName)
    {
        try
        {
            ClearArrayLists();
            return DAobj.GetSchema(TableName);
        }
        catch { throw; }
    }

    public IDataReader Fill_Bill_Type()
    {
        return DAobj.GetDataReader(SP_Acct_Get_BillType);
    }
    public IDataReader Fill_Fund_Type()
    {
        return DAobj.GetDataReader(SP_Acct_Get_FundType);
    }
    public IDataReader Fill_HeadCode()
    {
        return DAobj.GetDataReader(SP_Acct_Get_HeadCode);
    }
    public DataRow Get_FinTag()
    {
        ClearArrayLists();
        names.Add("@finid"); types.Add(SqlDbType.VarChar); values.Add(finid);
        names.Add("@userid"); types.Add(SqlDbType.VarChar); values.Add(userid);
        return (DAobj.GetDataRow(SP_Acct_GetFinTag, values, names, types));
    }
    public DataSet checkdatefromtodate()
    {
        ClearArrayLists();
        names.Add("@fdate"); types.Add(SqlDbType.VarChar); values.Add(fromdate);
        names.Add("@tdate"); types.Add(SqlDbType.VarChar); values.Add(todate);
        names.Add("@finid"); types.Add(SqlDbType.VarChar); values.Add(finid);
        names.Add("@locid"); types.Add(SqlDbType.VarChar); values.Add(locid);
        names.Add("@ddoid"); types.Add(SqlDbType.VarChar); values.Add(ddoid);
        return DAobj.GetDataSet(SP_Acct_CheckdateFromTo, values, names, types);
    }
    public DataSet GetLocationOnDDOId()
    {
        ClearArrayLists();

        names.Add("@fk_ddoid"); types.Add(SqlDbType.VarChar); values.Add(ddoid);
        return DAobj.GetDataSet("Acct_Location_OnDDOID", values, names, types);
    }
    public DataSet CheckdateAsOn()
    {
        ClearArrayLists();
        names.Add("@date"); types.Add(SqlDbType.VarChar); values.Add(DateAsOn);
        names.Add("@FinID"); types.Add(SqlDbType.VarChar); values.Add(finid);
        names.Add("@locid"); types.Add(SqlDbType.VarChar); values.Add(locid);
        return DAobj.GetDataSet("Acct_CheckdateAsOn", values, names, types);
    }
    #endregion
}