using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for CRUD_BASECLASS
/// </summary>
public partial class CRUD_BASECLASS
{
    //Return Data Table
    public static DataTable SaveRec<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase();
        IDataReader rdr = IUMSNXG.SP.SMS_SP_Executor(refo.InsertSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).GetReader();
        DataTable dt = new DataTable();
        dt.Load(rdr); rdr.Dispose(); rdr.Close();
        return dt;
    }


    //Created by Yogeshwar Singh  
    //Pass Master table pk for Transaction Table and Return Data Table
    public static DataTable SaveRec_withIDForTran<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase();
        refo.Setbase_Id();
        IDataReader rdr = IUMSNXG.SP.SMS_SP_Executor(refo.InsertSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).GetReader();
        DataTable dt = new DataTable();
        dt.Load(rdr); rdr.Dispose(); rdr.Close();
        return dt;
    }


    //Return Integer Value
    public static int SaveRec_RetInt<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase();
        int ret = IUMSNXG.SP.SMS_SP_Executor(refo.InsertSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).Execute();
        return ret;
    }

    //Return Integer Value
    public static int SaveRec_RetIntE<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase_Id();
        int ret = IUMSNXG.SP.SMS_SP_Executor(refo.InsertSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).Execute();
        return ret;
    }

    //Return Data Table
    public static DataTable UpdateRec<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase_Id();
        refo.Setbase();
        IDataReader rdr = IUMSNXG.SP.SMS_SP_Executor(refo.UpdateSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).GetReader();
        DataTable dt = new DataTable();
        dt.Load(rdr); rdr.Dispose(); rdr.Close();
        return dt;
    }

    //Return Integer Value
    public static int UpdateRec_RetInt<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase_Id();
        refo.Setbase();
        int ret = IUMSNXG.SP.SMS_SP_Executor(refo.UpdateSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).Execute();
        return ret;
    }

    public static int UpdateRec_RetIntE<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase();
        int ret = IUMSNXG.SP.SMS_SP_Executor(refo.UpdateSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).Execute();
        return ret;
    }

    public static int UpdateRec_Ret<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase_Id();
        int ret = IUMSNXG.SP.SMS_SP_Executor(refo.UpdateSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).Execute();
        return ret;
    }

    /// Sending only xml doc without identity to update the record
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    public static DataTable UpdateRec_withoutID<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase();
        IDataReader rdr = IUMSNXG.SP.SMS_SP_Executor(refo.UpdateSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).GetReader();
        DataTable dt = new DataTable();
        dt.Load(rdr); rdr.Dispose(); rdr.Close();
        return dt;
    }

    public static DataSet EditRec<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase_Id();
        DataSet ds = IUMSNXG.SP.SMS_SP_Executor(refo.EditSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).GetDataSet();
        DataTable dt = new DataTable();
        return ds;
    }


    public static DataTable DeleteRec<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase_Id();
        IDataReader rdr = IUMSNXG.SP.SMS_SP_Executor(refo.DeleteSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).GetReader();
        DataTable dt = new DataTable();
        dt.Load(rdr); rdr.Dispose(); rdr.Close();
        return dt;
    }
    public static DataSet DeleteRec1<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase_Id();
        DataSet ds = new DataSet();
        ds = IUMSNXG.SP.SMS_SP_Executor(refo.DeleteSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).GetDataSet();

        return ds;
    }

    public static int DeleteRec_RetInt<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase_Id();
        int ret = IUMSNXG.SP.SMS_SP_Executor(refo.DeleteSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).Execute();
        return ret;
    }

    public static DataTable GetSTable<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.Setbase_Id();
        IDataReader rdr = IUMSNXG.SP.SMS_SP_Executor(refo.SelectAllSPI).GetReader();
        DataTable dt = new DataTable();
        dt.Load(rdr); rdr.Dispose(); rdr.Close();
        return dt;
    }

    public static DataSet GetMTable<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.Setbase_Id();
        DataSet ds = IUMSNXG.SP.SMS_SP_Executor(refo.SelectAllSPI).GetDataSet();
        return ds;
    }

    // Created by Atul 
    public static DataSet GetDsWC<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase();
        DataSet ds = IUMSNXG.SP.SMS_SP_Executor(refo.SelectAllSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).GetDataSet();
        return ds;
    }
    // Atul END

    public static DataTable EditRecDatatable<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase_Id();
        IDataReader rdr = IUMSNXG.SP.SMS_SP_Executor(refo.EditSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).GetReader();
        DataTable dt = new DataTable();
        dt.Load(rdr); rdr.Dispose(); rdr.Close();
        return dt;
    }

    //Added by Darshna [18th jan 2012]
    // only for updating table and not to return any data
    public static int OnlyUpdateRecord<T>(T obj) where T : class,IBaseinterface
    {
        IBaseinterface refo = obj;
        refo.ClearAll();
        refo.Setbase_Id();
        refo.Setbase();
        int msg = IUMSNXG.SP.SMS_SP_Executor(refo.UpdateSPI, refo.ArrValuesI, refo.ArrNamesI, refo.ArrTypesI).Execute();
        return msg;
    }
}