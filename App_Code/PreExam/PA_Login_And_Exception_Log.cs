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
using System.Net;
using System.IO;
using System.Text;
using System.Diagnostics;

/// <summary>
/// This class can be used to maintain the log of exception and Login Logout log.
/// </summary>

namespace PA_Log
{
    public class PA_Login_And_Exception_Log
    {

        DataAccess DAobj = new DataAccess();
        ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
        ArrayList size = new ArrayList(); ArrayList outtype = new ArrayList();

        string _fk_regid, _IpAddress, _DegreeType, _Trnsaction, _UserType;

        public string UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }

        public string Trnsaction
        {
            get { return _Trnsaction; }
            set { _Trnsaction = value; }
        }

        public string DegreeType
        {
            get { return _DegreeType; }
            set { _DegreeType = value; }
        }

        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        public string Fk_regid
        {
            get { return _fk_regid; }
            set { _fk_regid = value; }
        }

        public PA_Login_And_Exception_Log()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        void Clear()
        {
            names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
        }
        /// <summary>
        /// This Will be used to maintain the Exception Log in DB
        /// </summary>
        /// <param name="StudentOrAdminRegId"> Student RegistrationId or Admin RegistrationID</param>
        /// <param name="DegreeType">If Student Log is Maintain then Put DegreeType from Session else for Admin pass Null</param>
        /// <param name="ex">Exception reference</param>
        /// <param name="UserIP">Ip Address of User</param>
        public static int Insert_ExceptionLog(string StudentOrAdminRegId, string DegreeType, Exception ex, string UserIP, ref string Message)
        {
            try
            {
                ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
                ArrayList size = new ArrayList(); ArrayList outtype = new ArrayList();
                names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
                //Getting here Exception Details
                string stackTraceDtl = ex.StackTrace.ToString();
               // path.Substring(path.LastIndexOf("/");
                string PageName = (ex.StackTrace.ToString()).Substring(ex.StackTrace.ToString().LastIndexOf('\\')).ToString();
                string ExcepMsg = ex.Message.ToString();
                string ExcepOccurInMethod = ex.TargetSite.ToString();
                StackTrace st = new StackTrace(ex);
                string ExcepOccurInClass = st.GetFrame(0).GetMethod().DeclaringType.ToString();
                //end
                
                DataAccess DAobj = new DataAccess();
                names.Add("@Pk_regid"); values.Add(StudentOrAdminRegId); types.Add(SqlDbType.VarChar);
                names.Add("@DegreeType"); values.Add(DegreeType); types.Add(SqlDbType.VarChar);
                names.Add("@IpAddress"); values.Add(UserIP); types.Add(SqlDbType.VarChar);
                names.Add("@stackTraceDtl"); values.Add(stackTraceDtl); types.Add(SqlDbType.VarChar);
                names.Add("@ExcepMsg"); values.Add(ExcepMsg); types.Add(SqlDbType.VarChar);
                names.Add("@ExcepOccurInMethod"); values.Add(ExcepOccurInMethod); types.Add(SqlDbType.VarChar);
                names.Add("@ExcepOccurInClass"); values.Add(ExcepOccurInClass + " Or " + PageName); types.Add(SqlDbType.VarChar);
                if (DAobj.ExecuteTransactionMsg("PA_Exception_Log_Ins", values, names, types, ref Message) > 0)
                {
                    Message = DAobj.ShowMessage("S", "");
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }








        public int Insert_Login_Log(ref string Message)
        {
            Clear();
            names.Add("@fk_regid"); values.Add(_fk_regid); types.Add(SqlDbType.BigInt);
            names.Add("@IpAddress"); values.Add(_IpAddress); types.Add(SqlDbType.VarChar);
            names.Add("@DegreeType"); values.Add(_DegreeType); types.Add(SqlDbType.VarChar);
            names.Add("@Trnsaction"); values.Add(_Trnsaction); types.Add(SqlDbType.VarChar);
            names.Add("@UserType"); values.Add(_UserType); types.Add(SqlDbType.VarChar);

            if (DAobj.ExecuteTransactionMsg("PA_Login_Log_Ins", values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}






