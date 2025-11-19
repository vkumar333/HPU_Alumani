using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using SubSonic;


/// <summary>
/// Summary description for um_land_base
/// </summary>
    namespace um_land_base
    {

        #region umm_Base_getlandpageurl


        public abstract class umm_Base_getlandpageurl : IBaseinterface
    {

        #region Class Members and ProcList

            public string xmldoc, userid, stts;
            public int Pk_ULinks;
        public string InsertSP = "KRC_SP_LandingPageURL_Mst_Ins",
          UpdateSP = "KRC_SP_LandingPageURL_Mst_Upd",
          DeleteSP = "KRC_SP_LandingPageURL_Mst_Del",
          EDITSP = "KRC_SP_LandingPageURL_Mst_Edit",
          SelectAllSP = "KRC_SP_LandingPageURL_Mst_SelAll";
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
        /// Setbase Members of the Base_Course  class.
        /// </summary>
        public void Setbase()
        {
            ArrNames.Add("@doc");
            ArrValues.Add(xmldoc);
            ArrTypes.Add(SqlDbType.VarChar);
            
           ArrNames.Add("@stts");
            ArrValues.Add(stts);
            ArrTypes.Add(SqlDbType.VarChar);
        }

        /// <summary>
        /// Setbase Id the Base_Course Class
        /// </summary>
        public void Setbase_Id()
        {
            ArrNames.Add("@Pk_ULinks");
            ArrValues.Add(Pk_ULinks);
            ArrTypes.Add(SqlDbType.Int);
        }

        /// <summary>
        /// Setbase Id the Base_Course Class
        /// </summary>
        public void Setbase_userid()
        {
            ArrNames.Add("@userid");
            ArrValues.Add(userid);
            ArrTypes.Add(SqlDbType.VarChar);
        }



        public DataSet UMM_getlandpagelnk_userwise()
        {
            this.ClearAll();
           
            Setbase_userid();
            DataSet ds = IUMSNXG.SP.SMS_SP_Executor("KRC_SP_LandingPageURL_Mst_SelAll", ArrValues, ArrNames, ArrTypes).GetDataSet();
            return ds;
        }
        #endregion

    }

    #endregion
}