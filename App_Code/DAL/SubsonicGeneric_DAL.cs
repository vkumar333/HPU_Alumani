using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using SubSonic;
using SubSonic.Utilities;

namespace IUMSNXG
{
    public partial class SP
    {
             
        public static StoredProcedure SMS_SP_Executor(string spname,ArrayList values, ArrayList names, ArrayList types)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure(spname, DataService.GetInstance("IUMSNXG"), "");
              for (int i = 0; i < Convert.ToInt32(values.Count); i++)
                {
                    sp.Command.AddParameter(names[i].ToString(), values[i], (DbType)types[i]);
                    sp.CommandTimeout = 360000;
                }
            return sp;
        }

        public static StoredProcedure SMS_SP_Executor(string spname)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure(spname, DataService.GetInstance("IUMSNXG"), ""); 
          
            return sp;
        }

        public static StoredProcedure SpGetTableStructure(string TableName)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("sp_GetTableStructure", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@TableName", TableName, DbType.String);
            sp.CommandTimeout = 360000;
            return sp;
        }
        public static StoredProcedure SpGetchklist(string fk_ddoid)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Sal_DDO_Dept_Map_Mst_FillchkLIST", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@fk_ddoid", fk_ddoid, DbType.String);
            sp.CommandTimeout = 360000;
            return sp;
        }
        public static StoredProcedure SpGetGrade(string fk_gradeid)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("sal_designation_fillgrade", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@fk_gradeid", fk_gradeid, DbType.String);
            sp.CommandTimeout = 360000;
            return sp;
        }

    


       /// <summary>
        /// Creates an object wrapper for the Sp_Sel_System_Parameter Procedure
        /// </summary>
        public static StoredProcedure SpSelSystemParameter()
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Sp_Sel_System_Parameter", DataService.GetInstance("IUMSNXG"), "");
            sp.CommandTimeout = 360000;
            return sp;
        }

}
}