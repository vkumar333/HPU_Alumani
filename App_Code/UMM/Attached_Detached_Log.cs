//create by Sakshi Singh on 31/OCT/2023

using DataAccessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Attached_Detached_Log
/// </summary>
public class Attached_Detached_Log
{
    public DataAccess DAobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList types = new ArrayList();
    public ArrayList values = new ArrayList();
    public void ClearArrayLists()
    {
        names.Clear();
        types.Clear();
        values.Clear();
    }
    public string finid { get; set; }
    public string fromdate { get; set; }
    public string todate { get; set; }
    public string empid { get; set; }

    public DataSet Loader()
    {
        ClearArrayLists();
        names.Add("@finid"); values.Add(finid); types.Add(SqlDbType.VarChar);
        return DAobj.GetDataSet("UM_Attached_detached_loader", values, names, types);
    }
    public DataSet GetAttached_detachedExcel()
    {
        try
        {
            ClearArrayLists();
            names.Add("@fromdate"); values.Add(fromdate); types.Add(SqlDbType.VarChar);
            names.Add("@todate"); values.Add(todate); types.Add(SqlDbType.VarChar);
            names.Add("@empid"); values.Add(empid); types.Add(SqlDbType.VarChar);
            return DAobj.GetDataSet("UM_Attached_detached_Exel", values, names, types);
        }
        catch { throw; }
    }

}