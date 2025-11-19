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
        public static StoredProcedure AlumniNewsEventInsert(string newsTitle, string newsdetail, bool isactive, DateTime publishdate)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_AlumniNewsEventInsert", DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@newsTitle", newsTitle, DbType.String);
            sp.Command.AddParameter("@newsdetail", newsdetail, DbType.String);
            sp.Command.AddParameter("@isactive", isactive, DbType.Boolean);
            sp.Command.AddParameter("@publishdate", publishdate, DbType.DateTime);
            // sp.Command.AddParameter("@degtype", degtype, DbType.String);

            return sp;
        }
        //Alumni_NewsEvent_Update
        public static StoredProcedure AlumniNewsEventUpdate(int newsid, string newsTitle, string newsdetail, bool isactive, DateTime publishdate)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_NewsEvent_Update", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@newsid ", newsid, DbType.Int32);
            sp.Command.AddParameter("@newstitle", newsTitle, DbType.String);
            sp.Command.AddParameter("@newsdtl", newsdetail, DbType.String);
            sp.Command.AddParameter("@isactive", isactive, DbType.Boolean);
            sp.Command.AddParameter("@publishdate", publishdate, DbType.DateTime);
            // sp.Command.AddParameter("@degtype", degtype, DbType.String);

            return sp;
        }

        // AlumniNewsEventsGetById

        public static StoredProcedure AlumniNewsEventsGetById(int newstitleid)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_NewsEvents_GetById", DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@newsid", newstitleid, DbType.Int32);
            return sp;
        }


        public static StoredProcedure AlumniNewsEventsDelete(int newstitleid)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_NewsEvents_Delete", DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@newsid", newstitleid, DbType.Int32);
            return sp;
        }

        public static StoredProcedure SMS_DegreeOnCollegeID(int pk_collegeid)
        {

            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("SMS_DegreeOnCollegeID", DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@pk_collegeid", pk_collegeid, DbType.Int32);
            return sp;
        }
        public static StoredProcedure SMS_DegreeOnCollegeID1(int pk_collegeid)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("SMS_DegreeOnCollegeID1", DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@pk_collegeid", pk_collegeid, DbType.Int32);
            return sp;
        }
        public static StoredProcedure ALM_DepartmentOnDegreeID(int pk_degreeid)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_DepartmentOnDegreeID", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@degree_id", pk_degreeid, DbType.Int32);
            return sp;
        }
        public static StoredProcedure SMS_SP_Session_Mst_SelForDDL(int pk_collegeid, int fk_degree_id)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("SMS_SP_Session_Mst_SelForDDL", DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@college_id", pk_collegeid, DbType.Int32);
            sp.Command.AddParameter("@degree_id", fk_degree_id, DbType.Int32);
            return sp;
        }
        public static StoredProcedure alm_sp_alumni_view_passout_students(int pk_collegeid, int fk_degree_id, int session_id, string name, string enroll)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_sp_alumni_view_passout_students", DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@collegeid", pk_collegeid, DbType.Int32);
            sp.Command.AddParameter("@degreeid", fk_degree_id, DbType.Int32);
            sp.Command.AddParameter("@sessionid", session_id, DbType.Int32);
            sp.Command.AddParameter("@Name", name, DbType.String);
            sp.Command.AddParameter("@enroll", enroll, DbType.String);
            return sp;
        }

        public static StoredProcedure alm_sp_del_alumni(string enroll)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_sp_del_alumni", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@enrol", enroll, DbType.String);
            return sp;
        }
        public static StoredProcedure alm_sp_max_pk_ALM_AlumniRegistration()
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_sp_max_pk_ALM_AlumniRegistration", DataService.GetInstance("IUMSNXG"), "");
            return sp;
        }
        public static StoredProcedure alm_sp_new_inserted_records(int pk_id)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_sp_new_inserted_records", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@pk_id", pk_id, DbType.String);
            return sp;
        }
        //
        public static StoredProcedure SMS_SP_College_Mst_SelForDDL()
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("SMS_SP_College_Mst_SelForDDL", DataService.GetInstance("IUMSNXG"), "");
            return sp;
        }
        public static StoredProcedure SMS_SP_College_Mst_SelForDDL1()
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("SMS_SP_College_Mst_SelForDDL1", DataService.GetInstance("IUMSNXG"), "");
            return sp;
        }
        public static StoredProcedure ALM_AlumniSuggestion_Ins(int fk_alumniid, string Suggestion)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_AlumniSuggestion_Ins", DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@fk_alumniid", fk_alumniid, DbType.Int32);

            sp.Command.AddParameter("@Suggestion", Suggestion, DbType.String);

            return sp;
        }

        public static StoredProcedure ALM_AlumniSuggestion_Edit(int fk_alumniid)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_AlumniSuggestion_Edit", DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@fk_alumniid", fk_alumniid, DbType.Int32);

            return sp;
        }

        public static StoredProcedure ALM_AlumniSuggestion_SelAll()
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_AlumniSuggestion_SelAll", DataService.GetInstance("IUMSNXG"), "");

            return sp;
        }


        public static StoredProcedure AlumniNewsEventsSelectAll()
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_NewsEvents_SelectAll", DataService.GetInstance("IUMSNXG"), "");

            // sp.Command.AddParameter("@newsid", newsTitle, DbType.Int32);
            return sp;
        }

        public static StoredProcedure AlumniNewsEventsSelectIsActiveOnly()
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_NewsEvents_SelectIsActiveOnly", DataService.GetInstance("IUMSNXG"), "");

            // sp.Command.AddParameter("@newsid", newsTitle, DbType.Int32);
            return sp;
        }

        public static StoredProcedure ALM_SP_AlumniRegistration_acode()
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AutoGenrateANO", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddOutputParameter("@almno", DbType.String);
            return sp;
        }


        public static StoredProcedure ALM_Sp_AlumniBatchYear_Passing_Year()
        {
            SubSonic.StoredProcedure sp = new StoredProcedure("ALM_Sp_AlumniBatchYear_Passing_Year", DataService.GetInstance("IUMSNXG"), "");
            return sp;
        }
        public static StoredProcedure FMS_BankMst_Sel()
        {
            SubSonic.StoredProcedure sp = new StoredProcedure("FMS_BankMst_Sel", DataService.GetInstance("IUMSNXG"), "");
            return sp;
        }


        public static StoredProcedure ALM_SP_GetDuplicate_Email_or_MobileNo(string Email, string MobileNo)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_GetDuplicate_Email_or_MobileNo",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@Email", Email, DbType.String);
            sp.Command.AddParameter("@Mobile", MobileNo, DbType.String);
            return sp;
        }


        public static StoredProcedure ALM_SP_SendEmail_Confirmation_To_Verified_Alumni(string xmlDoc, string Mode)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_SendEmail_Confirmation_To_Verified_Alumni",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@Doc", xmlDoc, DbType.String);
            sp.Command.AddParameter("@Mode", Mode, DbType.String);

            return sp;
        }

        public static StoredProcedure ALM_Sp_Alumni_EventDetails_SelectAll()
        {
            SubSonic.StoredProcedure sp = new StoredProcedure("ALM_Sp_Alumni_EventDetails_SelectAll", DataService.GetInstance("IUMSNXG"), "");
            return sp;
        }


        public static StoredProcedure Alm_Alumni_EventInsert(string EventTitle, string EventDiscription, DateTime StartDate, DateTime EndDate, string Createdby, bool IsEventPaid, double? EventCharge)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Alumni_EventInsert",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@EventTitle", EventTitle, DbType.String);
            sp.Command.AddParameter("@EventDis", EventDiscription, DbType.String);
            sp.Command.AddParameter("@E_Start", StartDate, DbType.DateTime);
            sp.Command.AddParameter("@E_End", EndDate, DbType.DateTime);
            sp.Command.AddParameter("@E_Created_by", Createdby, DbType.String);
            sp.Command.AddParameter("@IsEventPaid", IsEventPaid, DbType.Boolean);
            sp.Command.AddParameter("@EventCharge", EventCharge, DbType.Decimal);

            return sp;
        }


        public static StoredProcedure Alm_Alumni_Event_Update(int pk_EventId, string EventTitle, string EventDiscription, DateTime StartDate, DateTime EndDate, string UpdatedBy, bool IsEventPaid, double? EventCharge)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Alumni_Event_Update",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@Pk_Eventid", pk_EventId, DbType.Int32);
            sp.Command.AddParameter("@EventTitle", EventTitle, DbType.String);
            sp.Command.AddParameter("@EventDis", EventDiscription, DbType.String);
            sp.Command.AddParameter("@E_Start", StartDate, DbType.DateTime);
            sp.Command.AddParameter("@E_End", EndDate, DbType.DateTime);
            sp.Command.AddParameter("@E_Updated_by", UpdatedBy, DbType.String);
            sp.Command.AddParameter("@IsEventPaid", IsEventPaid, DbType.Boolean);
            sp.Command.AddParameter("@EventCharge", EventCharge, DbType.Decimal);

            return sp;
        }


        public static StoredProcedure Alm_Events_GetById(int Pk_EventId)
        {
            SubSonic.StoredProcedure sp = new StoredProcedure("Alm_Events_GetById", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@Pk_Eventid", Pk_EventId, DbType.Int32);
            return sp;
        }


        public static StoredProcedure ALM_Events_Delete(int Pk_EventId)
        {
            SubSonic.StoredProcedure sp = new StoredProcedure("ALM_Events_Delete", DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@Pk_EventId", Pk_EventId, DbType.Int32);
            return sp;
        }


        public static StoredProcedure Alm_Alumni_Acheivers_Processed_Unprocess_upd(string xmlDoc, string ProcessedBy, int pk_alumniid,
            string Mode_P_Or_U)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Alumni_Acheivers_Processed_Unprocess_upd",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@xmlDoc", xmlDoc, DbType.String);
            sp.Command.AddParameter("@ProcessedBy", ProcessedBy, DbType.String);
            sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
            sp.Command.AddParameter("@Mode_P_Or_U", Mode_P_Or_U, DbType.String);
            return sp;
        }

        public static StoredProcedure ALM_SP_GetAcheiversAnd_Procesed_Acheivers()
        {
            SubSonic.StoredProcedure sp = new StoredProcedure("ALM_SP_GetAcheiversAnd_Procesed_Acheivers", DataService.GetInstance("IUMSNXG"), "");
            return sp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pk_alumniid"></param>
        /// <param name="NewMobileNo">New Mobile No</param>
        /// <param name="Mode_I_Or_S">I for Insert The Record and S for Select the Record</param>
        /// <returns></returns>
        public static StoredProcedure Alm_Alumni_ChangeMobileNo_Request_ins_sel(int pk_alumniid, string NewMobileNo, string Mode_I_Or_S)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Alumni_ChangeMobileNo_Request_ins_sel",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
            sp.Command.AddParameter("@NewMobileNo", NewMobileNo, DbType.String);
            sp.Command.AddParameter("@Mode_I_Or_S", Mode_I_Or_S, DbType.String);
            return sp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="UpdatedBy">Updated by User</param>
        /// <param name="Mode">Sp Mode S: select Pending New mobile no.Request,U: Update new mobile No ,D: Display List after Updation</param>
        /// <returns></returns>
        public static StoredProcedure Alm_Alumni_NewMobileNo_Sel_Update_Display(string xmlDoc, string UpdatedBy, string Mode)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Alumni_NewMobileNo_Sel_Update_Display",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@xmlDoc", xmlDoc, DbType.String);
            sp.Command.AddParameter("@UpdatedBy", UpdatedBy, DbType.String);
            sp.Command.AddParameter("@Mode", Mode, DbType.String);
            return sp;
        }

        public static StoredProcedure ALM_SP_JobCategory_SelForDDL()
        {
            SubSonic.StoredProcedure sp = new StoredProcedure("ALM_SP_JobCategory_SelForDDL", DataService.GetInstance("IUMSNXG"), "");
            return sp;
        }
        /// <summary>
        /// To insert,select,update and show all record in grid
        /// </summary>
        /// <param name="xmlDoc">xml data is required while insert and update</param>
        /// <param name="Pk_JobPostedId">it is required while selecting single record from grid and while updating record</param>
        /// <param name="Pk_AlumniId">Required while displaying all jobs posted by a Alumni</param>
        /// <param name="Mode">Mode : I: To Insert, S: To Select Single Record, U: To update record,D: To display All record of this alumni</param>
        /// <returns></returns>
        public static StoredProcedure Alm_Alumni_PublishJob_ins_sel_Upd(string xmlDoc, int Pk_JobPostedId, int Pk_AlumniId, string Mode)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Alumni_PublishJob_ins_sel_Upd",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@xmlDoc", xmlDoc, DbType.String);
            sp.Command.AddParameter("@Pk_JobPostedId", Pk_JobPostedId, DbType.Int32);
            sp.Command.AddParameter("@Pk_AlumniId", Pk_AlumniId, DbType.Int32);
            sp.Command.AddParameter("@Mode", Mode, DbType.String);
            return sp;
        }


        public static StoredProcedure ALM_SP_Check_Duplicate_Email_AtUpdate(int Pk_AlumniId, string EmailId)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_Check_Duplicate_Email_AtUpdate",
                DataService.GetInstance("IUMSNXG"), "");

            sp.Command.AddParameter("@PK_AlumiID", Pk_AlumniId, DbType.Int32);
            sp.Command.AddParameter("@EmailID", EmailId, DbType.String);
            return sp;
        }

        public static StoredProcedure ALM_Select_Alumni_Degree_and_Batch_year()
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Select_Alumni_Degree_and_Batch_year",
                DataService.GetInstance("IUMSNXG"), "");

            return sp;
        }

        public static StoredProcedure ALM_Select_Alumni_List(int fk_degreeId, int Fk_sessionId, string AlumniName)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Select_Alumni_List",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@fk_degreeid", fk_degreeId, DbType.Int32);
            sp.Command.AddParameter("@fk_SessionId", Fk_sessionId, DbType.Int32);
            sp.Command.AddParameter("@AlumniName", AlumniName, DbType.String);
            return sp;
        }

        public static StoredProcedure Alm_Alumni_Sms_Email_Sent_ins(string xmlDoc)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Alumni_Sms_Email_Sent_ins",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@xmlDoc", xmlDoc, DbType.String);


            return sp;
        }

        public static StoredProcedure ALM_Select_Alumni_DashBoard(int Pk_UserId)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Select_Alumni_DashBoard",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@Pk_UserId", Pk_UserId, DbType.Int32);
            return sp;
        }


        public static StoredProcedure ALM_Select_Alumni_DashBoard_Dtls(int Pk_Id, string DtlsOf, int Pk_AlumniId)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Select_Alumni_DashBoard_Dtls",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@Pk_Id", Pk_Id, DbType.Int32);
            sp.Command.AddParameter("@DtlsOf", DtlsOf, DbType.String);
            sp.Command.AddParameter("@Pk_AlumniId", Pk_AlumniId, DbType.Int32);
            return sp;
        }


        public static StoredProcedure ALM_Select_Alumni_rpt(int Pk_CollegeId, int Pk_Degreeid, int YearOfPassing, string Status)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Select_Alumni_rpt",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@Pk_CollegeId", Pk_CollegeId, DbType.Int32);
            sp.Command.AddParameter("@Pk_DegreeId", Pk_Degreeid, DbType.Int32);
            sp.Command.AddParameter("@YearOfpass", YearOfPassing, DbType.Int32);
            sp.Command.AddParameter("@RegStatus", Status, DbType.String);
            return sp;
        }


        public static StoredProcedure Alm_Alumni_UnpaidEventApply(int Pk_EventId, int Pk_AlumniId)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Alumni_UnpaidEventApply",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@Pk_EventId", Pk_EventId, DbType.Int32);
            sp.Command.AddParameter("@Pk_AlumniId", Pk_AlumniId, DbType.Int32);


            return sp;
        }


        public static StoredProcedure Alm_Alumni_paidEventApply(int Pk_EventId, int Pk_AlumniId)
        {

            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Alumni_paidEventApply",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@Pk_EventId", Pk_EventId, DbType.Int32);
            sp.Command.AddParameter("@Pk_AlumniId", Pk_AlumniId, DbType.Int32);

            return sp;
        }


        public static StoredProcedure Alm_Alumni_Admin_HomeDashBoard_sel(DateTime StartDate, DateTime EndDate)
        {

            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Alumni_Admin_HomeDashBoard_sel",
                DataService.GetInstance("IUMSNXG"), "");
            sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
            sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);

            return sp;
        }
        public static StoredProcedure FeeDetails()
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_FeeCollection",
                DataService.GetInstance("IUMSNXG"), "");
            //sp.Command.AddParameter("@DegreeId", DegreeId, DbType.Int32);
            //sp.Command.AddParameter("@PaySession", PaySession, DbType.Int32);
            return sp;
        }
    }
}