using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataAccessLayer;
using System.Collections;

public class DbOperations
{
    DataAccess DAobj = new DataAccess();
    ArrayList name = new ArrayList(); ArrayList type = new ArrayList(); ArrayList value = new ArrayList();

    #region Member
    int _pageindex, _pagesize;
    string _BkpFileName;
    #endregion

    #region  Property

    public string BkpFileName
    {
        get { return _BkpFileName; }
        set { _BkpFileName = value; }
    }
    #endregion

    #region Common Function
    void clear()
    {
        name.Clear(); value.Clear(); type.Clear();
    }

    public string GetBtnCaption(string Key)
    {
        try
        {
            return DAobj.GetSysParaMessages(Key);
        }
        catch { throw; }
    }

    public string GetSysMessage(string Key)
    {
        try
        {
            return DAobj.GetSysParaMessages(Key);
        }
        catch { throw; }
    }
    #endregion

    #region  Method

    public DataTable ERPBackUp()
    {
        clear();
        name.Add("@BkpFileName"); value.Add(this._BkpFileName); type.Add(SqlDbType.VarChar);
        return DAobj.GetDataTable("UM_ERPDatabaseBackup", value, name, type);
    }

    public DataTable PortalBackUp()
    {
        clear();
        name.Add("@BkpFileName"); value.Add(this._BkpFileName); type.Add(SqlDbType.VarChar);
        return DAobj.GetDataTable("UM_ERPDatabaseBackup", value, name, type);
    }

    #endregion
}
