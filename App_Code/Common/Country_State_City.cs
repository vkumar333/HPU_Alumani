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

namespace CommonForms
{
    public class Country_State_City
    {
        DataAccess DAobj = new DataAccess();
        ArrayList name = new ArrayList(); ArrayList type = new ArrayList(); ArrayList value = new ArrayList();

        #region Member
        int _pageindex, _pagesize;
        string _Pk_Id, _Description, _Nationality, _OfficeCatId;
        int _ParentId, _Fk_OfficeTypeId;
        bool _IsDefault;
        #endregion

        #region  Property

        public int pageindex { get { return _pageindex; } set { _pageindex = value; } }
        public int pagesize { get { return _pagesize; } set { _pagesize = value; } }
        public string Pk_Id { get { return _Pk_Id; } set { _Pk_Id = value; } }
        public string Description { get { return _Description; } set { _Description = value; } }
        public string Nationality { get { return _Nationality; } set { _Nationality = value; } }
        public string OfficeCatId { get { return _OfficeCatId; } set { _OfficeCatId = value; } }
        public int ParentId { get { return _ParentId; } set { _ParentId = value; } }
        public int Fk_OfficeTypeId { get { return _Fk_OfficeTypeId; } set { _Fk_OfficeTypeId = value; } }
        public bool IsDefault { get { return _IsDefault; } set { _IsDefault = value; } }
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

        public int InsertRecord(ref string Message)
        {
            clear();
            name.Add("@Description"); value.Add(Description); type.Add(SqlDbType.DateTime);
            name.Add("@Nationality"); value.Add(Nationality); type.Add(SqlDbType.NVarChar);
            name.Add("@ParentId"); value.Add(ParentId); type.Add(SqlDbType.Int);
            name.Add("@Fk_OfficeTypeId"); value.Add(Fk_OfficeTypeId); type.Add(SqlDbType.Int);
            name.Add("@IsDefault"); value.Add(IsDefault); type.Add(SqlDbType.Bit);
            if (DAobj.ExecuteTransactionMsg("Comm_Country_State_City_Ins", value, name, type, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int UpdateRecord(ref string Message)
        {
            clear();
            name.Add("@Pk_Id"); value.Add(Pk_Id); type.Add(SqlDbType.VarChar);
            name.Add("@Description"); value.Add(Description); type.Add(SqlDbType.DateTime);
            name.Add("@Nationality"); value.Add(Nationality); type.Add(SqlDbType.NVarChar);
            name.Add("@ParentId"); value.Add(ParentId); type.Add(SqlDbType.Int);
            name.Add("@Fk_OfficeTypeId"); value.Add(Fk_OfficeTypeId); type.Add(SqlDbType.Int);
            name.Add("@IsDefault"); value.Add(IsDefault); type.Add(SqlDbType.Bit);
            if (DAobj.ExecuteTransactionMsg("Comm_Country_State_City_Upd", value, name, type, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("U", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public DataSet EditRecord()
        {
            name.Add("@Pk_Id"); value.Add(Pk_Id); type.Add(SqlDbType.VarChar);
            return DAobj.GetDataSet("Comm_Country_State_City_Edit", value, name, type);
        }

        public int DeleteRecord(ref string Message)
        {
            clear();
            name.Add("@Pk_Id"); value.Add(Pk_Id); type.Add(SqlDbType.VarChar);
            if (DAobj.ExecuteTransactionMsg("Comm_Country_State_City_Del", value, name, type, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("D", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void FillGrid(DataGrid DgGrid)
        {
            clear();
            name.Add("@Fk_OfficeTypeId"); value.Add(Fk_OfficeTypeId); type.Add(SqlDbType.Int);
            name.Add("@pageindex"); value.Add(pageindex); type.Add(SqlDbType.Int);
            name.Add("@pagesize"); value.Add(pagesize); type.Add(SqlDbType.Int);
            DAobj.Grid_CustomPaging("Comm_Country_State_City_SelAll", value, name, type, DgGrid, pageindex);
        }

        public DataSet PopulateParent()
        {
            return DAobj.GetRecords("Comm_Country_State_City_DDL");
        }

        public DataSet PopulateOfficeType()
        {
            name.Add("@OfficeCatId"); value.Add(OfficeCatId); type.Add(SqlDbType.VarChar);
            return DAobj.GetDataSet("Comm_OfficeType_OnCat_DDL", value, name, type);
        }
        #endregion
    }
}
