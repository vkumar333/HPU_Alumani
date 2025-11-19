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
/// Summary description for Main
/// </summary>
public class MainCls
{
    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
    private string _fk_regid, _xmldoc, _mobileno, _trnpassword;
    private long _pk_purchaseid;

    public string Trnpassword{get { return _trnpassword; }set { _trnpassword = value; }}

    public string Mobileno { get { return _mobileno; } set { _mobileno = value; } }

    public long Pk_purchaseid{get { return _pk_purchaseid; }set { _pk_purchaseid = value; }}
    public string fk_regid { get { return _fk_regid; } set { _fk_regid = value; } }
    public string XmlDoc { get { return _xmldoc; } set { _xmldoc = value; } }

    void clearall()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    public DataSet PopulateLatestNews()
    {
        clearall();
        return DAobj.GetDataSet("ACD_GET_LATEST_NEWS", values, names, types);
    }
    public DataSet PopulateLatestNewsEvents()
    {
        clearall();
        return DAobj.GetDataSet("Exam_ExamNews_Sel", values, names, types);
    }
    public DataSet eCouponVerify()
    {
        clearall();
        names.Add("@fk_regid"); values.Add(_fk_regid); types.Add(SqlDbType.BigInt);
        names.Add("@xmlDoc"); values.Add(_xmldoc); types.Add(SqlDbType.VarChar);
        return DAobj.GetDataSet("PA_eCoupon_SerailNo_Verify", values, names, types);
    }
    public DataSet GetPurchaseDetails()
    {
        DataSet ds = new DataSet();
        DataAccess obj = new DataAccess();
        try
        {
            clearall();
            names.Add("@mobileno"); values.Add(_mobileno); types.Add(SqlDbType.VarChar);
            names.Add("@trnpassword"); values.Add(_trnpassword); types.Add(SqlDbType.VarChar);

            ds = obj.GetDataSet("PA_Forgot_eCoupon_SelAll", values, names, types);
        }
        catch
        {
        }
        return ds;
    }
    public DataSet GetPurchaseTrnDetails()
    {
        DataSet ds = new DataSet();
        DataAccess obj = new DataAccess();
        try
        {
            clearall();
            names.Add("@pk_purchaseid"); values.Add(_pk_purchaseid); types.Add(SqlDbType.BigInt);
            ds = obj.GetDataSet("PA_Forgot_eCoupon_PrchsDtl_SelAll", values, names, types);
        }
        catch
        {
        }
        return ds;
    }
}