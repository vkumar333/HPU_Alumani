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
        /// <summary>
        /// Creates an object wrapper for the AAE_Degree_Mst_Ins Procedure
        /// 08/07/2009
        /// </summary>
        //public static StoredProcedure AAE_Degree_Mst_Ins(string degname, string degdesc, int? minsem, int? maxsem, string degtype)
        //{
        //    SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("AAE_Degree_Mst_Ins", DataService.GetInstance("IUMSNXG"), "");

        //    sp.Command.AddParameter("@degname", degname, DbType.String);
        //    sp.Command.AddParameter("@degdesc", degdesc, DbType.String);
        //    sp.Command.AddParameter("@minsem", minsem, DbType.Int16);
        //    sp.Command.AddParameter("@maxsem", maxsem, DbType.Int16);
        //    sp.Command.AddParameter("@degtype", degtype, DbType.String);

        //    return sp;
        //}


        /// 08/07/2009
        /// </summary>
        public static StoredProcedure ALM_SP_Degree_Mst_SelAll()
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_Degree_Mst_SelAll", DataService.GetInstance("IUMSNXG"), "");

            return sp;
        }
        /// <summary>
        /// Creates an object wrapper for the ALM_SP_AlumniRegistration_ChangePwd Procedure
        /// 16/11/2010
        /// </summary>
        public static StoredProcedure ALM_SP_AlumniRegistration_ChangePwd(int pk_alumniid, string pwd)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_ChangePwd", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
            sp.Command.AddParameter("@pwd", pwd, DbType.String);

            return sp;
        }

        /// <summary>
        /// Creates an object wrapper for the ALM_SP_AlumniRegistration_InsByImport Procedure
        /// 15/11/2010
        /// </summary>
        public static StoredProcedure ALM_SP_AlumniRegistration_InsByImport(string alumni_name, string gender, int yearofpassing, string currentaddress, string per_address, string contactno,
            string email, string currentoccupation, string amount, string regno, string receiptno, string logname, string pwd,int collegeid)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_InsByImport", DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@alumni_name", alumni_name, DbType.String);
            sp.Command.AddParameter("@gender", gender, DbType.String);
            sp.Command.AddParameter("@yearofpassing", yearofpassing, DbType.Int32);
            sp.Command.AddParameter("@currentaddress", currentaddress, DbType.String);
            sp.Command.AddParameter("@per_address", per_address, DbType.String);
            sp.Command.AddParameter("@contactno", contactno, DbType.String);
            sp.Command.AddParameter("@email", email, DbType.String);
            sp.Command.AddParameter("@currentoccupation", currentoccupation, DbType.String);
            sp.Command.AddParameter("@amount", amount, DbType.String);
            sp.Command.AddParameter("@regno", regno, DbType.String);
            sp.Command.AddParameter("@receiptno", receiptno, DbType.String);
            sp.Command.AddParameter("@logname", logname, DbType.String);
            sp.Command.AddParameter("@pwd", pwd, DbType.String);
           // sp.Command.AddParameter("@fk_degreeid", degreeid, DbType.Int32);
            sp.Command.AddParameter("@fk_collegeid", collegeid, DbType.Int32);
            return sp;
        }
        /// <summary>
        /// Creates an object wrapper for the ALM_SP_ALM_AlumniRegistration_Ins Procedure
        /// 08/02/2010
        /// </summary>
        public static StoredProcedure ALM_SP_AlumniRegistration_Ins(string docu, byte[] imgattach)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_Ins", DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@doc", docu, DbType.String);
            sp.Command.AddParameter("@imgattach", imgattach, DbType.Binary);

            return sp;
        }

        /// <summary>
        /// Creates an object wrapper for the ALM_SP_AlumniRegistration_Upd Procedure
        /// 08/02/2010
        /// </summary>
        public static StoredProcedure ALM_SP_AlumniRegistration_Upd(int pk_alumniid, string docu, byte[] imgattach)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_Upd", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
            sp.Command.AddParameter("@doc", docu, DbType.String);
            sp.Command.AddParameter("@imgattach", imgattach, DbType.Binary);

            return sp;
        }
        /// <summary>
        /// Creates an object wrapper for the ALM_SP_AlumniRegistration_ChkLoginName Procedure
        /// 26/03/2010
        /// </summary>
        public static StoredProcedure ALM_SP_AlumniRegistration_ChkLoginName(string loginname)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_ChkLoginName", DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@loginname", loginname, DbType.String);

            return sp;
        }

        /// <summary>
        /// Creates an object wrapper for the ALM_SP_AlumniRegistration_LoginAuthentication Procedure
        /// 26/03/2010
        /// </summary>
        public static StoredProcedure ALM_SP_AlumniRegistration_LoginAuthentication(string loginname, string pwd)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_LoginAuthentication", DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@loginname", loginname, DbType.String);

            sp.Command.AddParameter("@pwd", pwd, DbType.String);

            return sp;
        }



        /// <summary>
        /// Creates an object wrapper for the ALM_SP_AlumniRegistration_Edit Procedure
        /// 08/02/2010
        /// </summary>
        public static StoredProcedure ALM_SP_AlumniRegistration_Edit(int? pk_alumniid)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_Edit", DataService.GetInstance("IUMSNXG"), "");


            sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);

            return sp;
        }

        /// <summary>
        /// Creates an object wrapper for the ALM_SP_AlumniRegistration_Del Procedure
        /// 08/02/2010
        /// </summary>
        public static StoredProcedure ALM_SP_AlumniRegistration_Del(int? pk_alumniid)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_Del", DataService.GetInstance("IUMSNXG"), "");


            sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);

            return sp;
        }

        /// <summary>
        /// Creates an object wrapper for the ALM_SP_AlumniRegistration_Selall Procedure
        /// 08/02/2010
        /// </summary>
        public static StoredProcedure ALM_SP_AlumniRegistration_Selall()
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_Selall", DataService.GetInstance("IUMSNXG"), "");

            return sp;
        }
        /// <summary>
        /// Creates an object wrapper for the ALM_SP_Alumni_Search Procedure
        /// 08/02/2010
        /// </summary>
        public static StoredProcedure ALM_SP_Alumni_Search(string alumni_name, string year, string gender, string occupation, string fk_degreeid, string fk_collegeid)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_Alumni_Search", DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@alumni_name", alumni_name, DbType.String);
            sp.Command.AddParameter("@year", year, DbType.String);
            sp.Command.AddParameter("@gender", gender, DbType.String);
            sp.Command.AddParameter("@occupation", @occupation, DbType.String);
            sp.Command.AddParameter("@fk_degreeid", fk_degreeid, DbType.String);
            sp.Command.AddParameter("@fk_collegeid", fk_collegeid, DbType.String);
            return sp;
        }
        public static StoredProcedure ALM_SP_approvealumni(string docu,string smode)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_getalumnifor_approval", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@doc", docu, DbType.String);
            sp.Command.AddParameter("@smode", smode, DbType.String);
            return sp;
        }

        //public static StoredProcedure ALM_SP_viewprofile(int alumniId)
        //{
        //    SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_viewprofile", DataService.GetInstance("IUMSNXG"), "");
        //    sp.Command.AddParameter("@alumniId", alumniId, DbType.Int32);
        //    return sp;
        //}

        public static StoredProcedure ALM_SP_viewprofile(string doc)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_viewprofile", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@doc", doc, DbType.String);
            return sp;
        }

        //Delete Record from Pending Request Alumni 13/02/2015

        public static StoredProcedure ALM_SP_PendingRequest_Del(int alumniId)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_PendingRequest_Del", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@alumniId", alumniId, DbType.Int32);
            return sp;
        }

        //Delete Record from Approved Request Alumni  13/02/2015

             public static StoredProcedure ALM_SP_ApprovedRequest_Del(int alumniId)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_ApprovedRequest_Del", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@alumniId", alumniId, DbType.Int32);
            return sp;
        }

        //Sending Mail For Forgot Password

             public static StoredProcedure ALM_SP_Alumni_Forgotpassword(string Alumni_Name, string EMail)
             {
                 SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("SPALM_AlumniRegistration1", DataService.GetInstance("IUMSNXG"), "");

                 sp.Command.AddParameter("@Alumni_Name", Alumni_Name, DbType.String);

                 sp.Command.AddParameter("@EMail", EMail, DbType.String);

                 return sp;
             }


    }
}