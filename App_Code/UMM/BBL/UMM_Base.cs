using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Web;

using SubSonic;

/// <summary>
/// Summary description for UMM_Base
/// </summary>
namespace UMM_BASECLASSES
{


    #region UMM_Base_UserSmtp_Dtls

    public abstract class UMM_Base_UserSmtp_Dtls : IBaseinterface
    {

        #region Class Members and ProcList

        public int Fk_SmtpId;
        public string pk_userId, email, smtppassword;
        public string InsertSP = "",
            UpdateSP = "UMM_SP_UserSmtp_Dtls_Upd",
            DeleteSP = "",
            EDITSP = "UMM_SP_UserSmtp_Dtls_Edit",
            SelectAllSP = "UMM_SP_Smtp_Mst_SelAll";
        public ArrayList ArrTypes = new ArrayList();
        public ArrayList ArrValues = new ArrayList();
        public ArrayList ArrNames = new ArrayList();

        #endregion

        #region IBaseinterface Members
        /// <summary>
        /// Implementing All arraylist of base interface
        /// </summary>
        ArrayList IBaseinterface.ArrNamesI { get { return ArrNames; } set { ArrNames = value; } }
        ArrayList IBaseinterface.ArrTypesI { get { return ArrTypes; } set { ArrTypes = value; } }
        ArrayList IBaseinterface.ArrValuesI { get { return ArrValues; } set { ArrValues = value; } }

        /// <summary>
        /// Implementing all properties of base interface
        /// </summary>
        string IBaseinterface.InsertSPI { get { return InsertSP; } set { InsertSP = value; } }
        string IBaseinterface.UpdateSPI { get { return UpdateSP; } set { UpdateSP = value; } }
        string IBaseinterface.EditSPI { get { return EDITSP; } set { EDITSP = value; } }
        string IBaseinterface.DeleteSPI { get { return DeleteSP; } set { DeleteSP = value; } }
        string IBaseinterface.SelectAllSPI { get { return SelectAllSP; } set { SelectAllSP = value; } }

        /// <summary>
        /// Clears all ArrayLists.
        /// </summary>
        public void ClearAll() { ArrNames.Clear(); ArrTypes.Clear(); ArrValues.Clear(); }

        #endregion

        #region Setting Values of the Class
        /// <summary>
        /// Setbase Members of the user master class.
        /// </summary>
        public void Setbase()
        {
            ArrNames.Add("@Fk_SmtpId"); ArrNames.Add("@email"); ArrNames.Add("@smtppassword");
            ArrValues.Add(Fk_SmtpId); ArrValues.Add(email); ArrValues.Add(smtppassword);
            ArrTypes.Add(SqlDbType.Int); ArrTypes.Add(SqlDbType.VarChar); ArrTypes.Add(SqlDbType.VarChar);

        }

        /// <summary>
        /// Setbase Id the user master class
        /// </summary>
        public void Setbase_Id()
        {
            ArrNames.Add("@pk_userId");
            ArrValues.Add(pk_userId);
            ArrTypes.Add(SqlDbType.VarChar);
        }

        //public virtual DataTable SMS_SP_AssessmentMarks_Cld_Sel()
        //{
        //    this.ClearAll();
        //    Setbase();
        //    IDataReader rdr = IUMSNXG.SP.SMS_SP_Executor("SMS_SP_AssessmentMarks_Cld_Sel", ArrValues, ArrNames, ArrTypes).GetReader();
        //    DataTable dt = new DataTable();
        //    dt.Load(rdr);
        //    rdr.Dispose(); rdr.Close();
        //    return dt;
        //}

        //public virtual int SMS_SP_AssessmentMarks_Cld_Upd()
        //{
        //    this.ClearAll();
        //    Setbase();
        //    int n = IUMSNXG.SP.SMS_SP_Executor("SMS_SP_AssessmentMarks_Cld_Upd", ArrValues, ArrNames, ArrTypes).Execute();
        //    return n;
        //}
        
        /// <summary>
        /// By: Anand on 20 Sep 2012, to generate BreadCumbs on pageid
        /// </summary>
        /// <param name="pageid"></param>
        /// <returns></returns>
        public virtual DataSet GetBreadCumbsOn_Pageid(int pageid)
        {
            this.ClearAll();
            ArrNames.Add("@pk_webpageid");
            ArrValues.Add(pageid);
            ArrTypes.Add(SqlDbType.Int);
            DataSet dss = IUMSNXG.SP.SMS_SP_Executor("UM_Sp_GetBreadCumbs", ArrValues, ArrNames, ArrTypes).GetDataSet();
            return dss;
        }

        /// <summary>
        /// By: Anand on 20 Sep 2012, to generate BreadCumbs on page name and moduleid
        /// </summary>
        /// <param name="pageid"></param>
        /// <returns></returns>
        public virtual DataSet GetBreadCumbsOn_Pagename_Moduleid(string Pagename,int Moduleid)
        {
            this.ClearAll();
            ArrNames.Add("@webpagename");
            ArrValues.Add(Pagename);
            ArrTypes.Add(SqlDbType.VarChar);

            ArrNames.Add("@Fk_moduleid");
            ArrValues.Add(Moduleid);
            ArrTypes.Add(SqlDbType.Int);

            DataSet dss = IUMSNXG.SP.SMS_SP_Executor("UM_Sp_GetBreadCumbs_onPagename_Moduleid", ArrValues, ArrNames, ArrTypes).GetDataSet();
            return dss;
        }

        /// <summary>
        /// By: Anand on 11 Oct 2012, to save password policies..
        /// </summary>
        /// <param name="pageid"></param>
        /// <returns></returns>
        public virtual DataSet SavePasswordPolicies(int MinLength, Boolean MinUpperCaseChar, Boolean MinNumericChar, Boolean MinSpecialChar, Boolean Allow_Pass_equal_userid)
        {
            this.ClearAll();          
            ArrNames.Add("@MinLength");ArrValues.Add(MinLength);ArrTypes.Add(SqlDbType.Int);
            ArrNames.Add("@MinUpperCaseChar"); ArrValues.Add(MinUpperCaseChar); ArrTypes.Add(SqlDbType.Bit);
            ArrNames.Add("@MinNumericChar"); ArrValues.Add(MinNumericChar); ArrTypes.Add(SqlDbType.Bit);
            ArrNames.Add("@MinSpecialChar"); ArrValues.Add(MinSpecialChar); ArrTypes.Add(SqlDbType.Bit);
            ArrNames.Add("@Allow_Pass_equal_userid"); ArrValues.Add(Allow_Pass_equal_userid); ArrTypes.Add(SqlDbType.Bit);
            DataSet dss = IUMSNXG.SP.SMS_SP_Executor("UM_sp_SavePasswordPolicies", ArrValues, ArrNames, ArrTypes).GetDataSet();
            return dss;
        }

        public virtual DataSet GetPasswordPolicies()
        {

            DataSet dss = IUMSNXG.SP.SMS_SP_Executor("UM_sp_GetPasswordPolicies").GetDataSet();
            return dss;
        }


        public virtual DataSet GetModuleDtl(int mid)
        {
            this.ClearAll();
            ArrNames.Add("@Mid"); ArrValues.Add(mid); ArrTypes.Add(SqlDbType.Int);
            DataSet dss = IUMSNXG.SP.SMS_SP_Executor("UM_sp_GetModuledtl", ArrValues, ArrNames, ArrTypes).GetDataSet();
            return dss;
        }

     
        #endregion


    }

    #endregion


}