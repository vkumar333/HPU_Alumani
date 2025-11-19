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
    public class DDO_Mst:I_DDO_Mst
    {
        DataAccess DAobj = new DataAccess();
        ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

        #region Member

        int _pageindex, _pagesize, _fk_districid;
        string _pk_ddoid, _code, _description, _address1, _address2, _tanno, _alias, _remarks, _fk_userid, _fk_locid,_displayorder;
        DateTime _dated;
        byte[] _timestamp;
        
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

        public string pk_ddoid
        {
            get
            {
                return _pk_ddoid;
            }
            set
            {
                _pk_ddoid = value;
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

        public string code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }

        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public string address1
        {
            get
            {
                return _address1;
            }
            set
            {
                _address1 = value;
            }
        }

        public string address2
        {
            get
            {
                return _address2;
            }
            set
            {
                _address2 = value;
            }
        }

        public string tanno
        {
            get
            {
                return _tanno;
            }
            set
            {
                _tanno = value;
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
        public DateTime dated
        {
            get { return _dated; }
            set
            {
                _dated = value;
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

        public string displayorder
        {
            get
            {
                return _displayorder;
            }
            set
            {
                _displayorder = value;
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
            names.Add("@code"); types.Add(SqlDbType.VarChar); values.Add(this._code);
            names.Add("@description"); types.Add(SqlDbType.VarChar); values.Add(this._description);
            names.Add("@dated"); types.Add(SqlDbType.DateTime); values.Add(this._dated);
            names.Add("@address1"); types.Add(SqlDbType.VarChar); values.Add(this._address1);
            names.Add("@address2"); types.Add(SqlDbType.VarChar); values.Add(this._address2);
            names.Add("@tan"); types.Add(SqlDbType.VarChar); values.Add(this._tanno);
            names.Add("@fk_districid"); types.Add(SqlDbType.Int); values.Add(this._fk_districid);
            names.Add("@alias"); types.Add(SqlDbType.VarChar); values.Add(this._alias);
            names.Add("@remarks"); types.Add(SqlDbType.VarChar); values.Add(this._remarks);
            names.Add("@fk_userid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_userid);
            names.Add("@fk_locid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_locid);
            names.Add("@displayorder"); types.Add(SqlDbType.VarChar); values.Add(this._displayorder);
            if (DAobj.ExecuteTransactionMsg("Comm_DDO_Ins", values, names, types, ref Message) > 0)
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
            names.Add("@pk_ddoid"); types.Add(SqlDbType.VarChar); values.Add(this._pk_ddoid);
            names.Add("@code"); types.Add(SqlDbType.VarChar); values.Add(this._code);
            names.Add("@description"); types.Add(SqlDbType.VarChar); values.Add(this._description);
            names.Add("@dated"); types.Add(SqlDbType.DateTime); values.Add(this._dated);
            names.Add("@address1"); types.Add(SqlDbType.VarChar); values.Add(this._address1);
            names.Add("@address2"); types.Add(SqlDbType.VarChar); values.Add(this._address2);
            names.Add("@tan"); types.Add(SqlDbType.VarChar); values.Add(this._tanno);
            names.Add("@fk_districid"); types.Add(SqlDbType.Int); values.Add(this._fk_districid);
            names.Add("@alias"); types.Add(SqlDbType.VarChar); values.Add(this._alias);
            names.Add("@remarks"); types.Add(SqlDbType.VarChar); values.Add(this._remarks);
            names.Add("@fk_userid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_userid);
            names.Add("@fk_locid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_locid);
            names.Add("@timestamp"); types.Add(SqlDbType.Timestamp); values.Add(this._timestamp);
            names.Add("@displayorder"); types.Add(SqlDbType.VarChar); values.Add(this._displayorder);
            if (DAobj.ExecuteTransactionMsg("Comm_DDO_Upd", values, names, types, ref Message) > 0)
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
            names.Add("@pk_ddoid"); types.Add(SqlDbType.VarChar); values.Add(this._pk_ddoid);
            if (DAobj.ExecuteTransactionMsg("Comm_DDO_Del", values, names, types, ref Message) > 0)
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
            names.Add("@pk_ddoid"); types.Add(SqlDbType.VarChar); values.Add(this._pk_ddoid);
            return DAobj.GetDataTable("Comm_DDO_Edit", values, names, types);
        }

        public void PopulateGrid(DataGrid DgGrid)
        {
                clearall();
                names.Add("@pageindex"); types.Add(SqlDbType.Int); values.Add(this._pageindex);
                names.Add("@pagesize"); types.Add(SqlDbType.Int); values.Add(this._pagesize);
                DAobj.Grid_CustomPaging("Comm_DDO_SelForGrid", values, names, types, DgGrid, this._pageindex);
        }

        public DataSet PopulateDistric()
        {
            return DAobj.GetRecords("DDO_Fill_District_DDL");
        }

        #endregion
    }
}