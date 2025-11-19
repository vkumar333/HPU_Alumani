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

namespace CommonForms
{
    public class Location_Mst : I_Location_Mst
    {
        DataAccess DAobj = new DataAccess();
        ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

        #region Member

        int _pageindex, _pagesize, _fk_districid, _fk_officeid, _fk_holidaylocid;
        string _pk_locid, _locname, _address, _fk_plocid, _alias, _remarks, _fk_userid, _fk_locid;
        string _Filename, _ContentType, _UpdChange;
        byte[] _FileBytes, _timestamp;
        
        #endregion

        #region  Properties
        
        public int pageindex
        {
            get
            {
                return _pageindex;
            }
            set
            {
                _pageindex = value;
            }
        }

        public int pagesize
        {
            get
            {
                return _pagesize;
            }
            set
            {
                _pagesize = value;
            }
        }

        public int fk_districid
        {
            get
            {
                return _fk_districid;
            }
            set
            {
                _fk_districid = value;
            }
        }

        public int fk_officeid
        {
            get
            {
                return _fk_officeid;
            }
            set
            {
                _fk_officeid = value;
            }
        }

        public string pk_locid
        {
            get
            {
                return _pk_locid;
            }
            set
            {
                _pk_locid = value;
            }
        }

        public string locname
        {
            get
            {
                return _locname;
            }
            set
            {
                _locname = value;
            }
        }

        public string address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        public string fk_plocid
        {
            get
            {
                return _fk_plocid;
            }
            set
            {
                _fk_plocid = value;
            }
        }

        public string alias
        {
            get
            {
                return _alias;
            }
            set
            {
                _alias = value;
            }
        }

        public string remarks
        {
            get
            {
                return _remarks;
            }
            set
            {
                _remarks = value;
            }
        }

        public string fk_userid
        {
            get
            {
                return _fk_userid;
            }
            set
            {
                _fk_userid = value;
            }
        }

        public string fk_locid
        {
            get
            {
                return _fk_locid;
            }
            set
            {
                _fk_locid = value;
            }
        }

        public string Filename
        {
            get { return _Filename; }
            set
            {
                _Filename = value;
            }
        }

        public string ContentType
        {
            get { return _ContentType; }
            set
            {
                _ContentType = value;
            }
        }

        public byte[] FileBytes
        {
            get { return _FileBytes; }
            set
            {
                _FileBytes = value;
            }
        }

        public string UpdChange
        {
            get { return _UpdChange; }
            set
            {
                _UpdChange = value;
            }
        }

        public byte[] timestamp
        {
            get { return _timestamp; }
            set
            {
                _timestamp = value;
            }
        }

        public int fk_holidaylocid
        {
            get
            {
                return _fk_holidaylocid;
            }
            set
            {
                _fk_holidaylocid = value;
            }
        }








        #endregion

        #region Common Function
        void clearall()
        {
            names.Clear();
            values.Clear();
            types.Clear();
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
            clearall();
            names.Add("@locname"); types.Add(SqlDbType.VarChar); values.Add(this._locname);
            names.Add("@fk_districid"); types.Add(SqlDbType.Int); values.Add(this._fk_districid);
            names.Add("@address"); types.Add(SqlDbType.VarChar); values.Add(this._address);
            names.Add("@fk_officeid"); types.Add(SqlDbType.Int); values.Add(this._fk_officeid);
            names.Add("@fk_plocid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_plocid);
            names.Add("@locationAlias"); types.Add(SqlDbType.VarChar); values.Add(this._alias);
            names.Add("@remarks"); types.Add(SqlDbType.VarChar); values.Add(this._remarks);
            names.Add("@Filename"); types.Add(SqlDbType.VarChar); values.Add(this._Filename);
            names.Add("@ContentType"); types.Add(SqlDbType.VarChar); values.Add(this._ContentType);
            names.Add("@Attachment"); types.Add(SqlDbType.Image); values.Add(this._FileBytes);
            names.Add("@fk_userid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_userid);
            names.Add("@fk_locid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_locid);
            names.Add("@fk_holidaylocid"); types.Add(SqlDbType.Int); values.Add(this._fk_holidaylocid);
            if (DAobj.ExecuteTransactionMsg("Comm_Location_Ins", values, names, types, ref Message) > 0)
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
            clearall();
            names.Add("@pk_locid"); types.Add(SqlDbType.VarChar); values.Add(this._pk_locid);
            names.Add("@locname"); types.Add(SqlDbType.VarChar); values.Add(this._locname);
            names.Add("@fk_districid"); types.Add(SqlDbType.Int); values.Add(this._fk_districid);
            names.Add("@address"); types.Add(SqlDbType.VarChar); values.Add(this._address);
            names.Add("@fk_officeid"); types.Add(SqlDbType.Int); values.Add(this._fk_officeid);
            names.Add("@fk_plocid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_plocid);
            names.Add("@locationAlias"); types.Add(SqlDbType.VarChar); values.Add(this._alias);
            names.Add("@remarks"); types.Add(SqlDbType.VarChar); values.Add(this._remarks);
            names.Add("@Filename"); types.Add(SqlDbType.VarChar); values.Add(this._Filename);
            names.Add("@ContentType"); types.Add(SqlDbType.VarChar); values.Add(this._ContentType);
            names.Add("@Attachment"); types.Add(SqlDbType.Bit); values.Add(this._FileBytes);
            names.Add("@UpdChange"); types.Add(SqlDbType.VarChar); values.Add(this._UpdChange);
            names.Add("@fk_userid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_userid);
            names.Add("@fk_locid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_locid);
            names.Add("@timestamp"); types.Add(SqlDbType.Timestamp); values.Add(this._timestamp);
            if (DAobj.ExecuteTransactionMsg("Comm_Location_Upd", values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("U", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int DeleteCommand(ref string Message)
        {
            clearall();
            names.Add("@pk_locid"); types.Add(SqlDbType.VarChar); values.Add(this._pk_locid);
            if (DAobj.ExecuteTransactionMsg("Comm_Location_Del", values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("D", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public DataTable EditRecords()
        {
            clearall();
            names.Add("@pk_locid"); types.Add(SqlDbType.VarChar); values.Add(this._pk_locid);
            return DAobj.GetDataTable("Comm_Location_Edit", values, names, types);
        }

        public void PopulateGrid(DataGrid DgGrid)
        {
            clearall();
            names.Add("@pageindex"); types.Add(SqlDbType.Int); values.Add(this._pageindex);
            names.Add("@pagesize"); types.Add(SqlDbType.Int); values.Add(this._pagesize);
            DAobj.Grid_CustomPaging("Comm_Location_SelForGrid", values, names, types, DgGrid, this._pageindex);
        }

        public DataSet PopulateDistric()
        {
            return DAobj.GetRecords("DDO_Fill_District_DDL");
        }

        public DataSet PopulateOfficeType()
        {
            return DAobj.GetRecords("Location_OfficeTypeMaster_DDL");
        }

        public DataSet PopulateParentLocation()
        {
            return DAobj.GetRecords("Comm_PLocation_Selforddl");
        }
        

         public DataSet PopulateHolidayLocation()
        {
            return DAobj.GetRecords("SAL_SP_HolidayLoc_Mst_SelAll");
        }




        #endregion
    }
}