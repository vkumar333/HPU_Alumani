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
    public class OfficeType_Mst : I_OfficeType_Mst
    {
        DataAccess DAobj = new DataAccess();
        ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

        #region Member

        int _pageindex, _pagesize, _pk_offtypeid;
        string _code, _description, _fk_userid, _fk_locid;
        byte[] _timestamp;
        
        #endregion

        #region  Properties

        public int pk_offtypeid
        {
            get
            {
                return _pk_offtypeid;
            }
            set
            {
                _pk_offtypeid = value;
            }
        }

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

        public byte[] timestamp
        {
            get { return _timestamp; }
            set
            {
                _timestamp = value;
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
            if (DAobj.ExecuteTransactionMsg("Comm_OfficeType_Ins", values, names, types, ref Message) > 0)
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
            names.Add("@pk_offtypeid"); types.Add(SqlDbType.VarChar); values.Add(this._pk_offtypeid);
            names.Add("@code"); types.Add(SqlDbType.VarChar); values.Add(this._code);
            names.Add("@description"); types.Add(SqlDbType.VarChar); values.Add(this._description);
            names.Add("@timestamp"); types.Add(SqlDbType.Timestamp); values.Add(this._timestamp);
            if (DAobj.ExecuteTransactionMsg("Comm_OfficeType_Upd", values, names, types, ref Message) > 0)
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
            names.Add("@pk_offtypeid"); types.Add(SqlDbType.VarChar); values.Add(this._pk_offtypeid);
            if (DAobj.ExecuteTransactionMsg("Comm_OfficeType_Del", values, names, types, ref Message) > 0)
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
            names.Add("@pk_offtypeid"); types.Add(SqlDbType.VarChar); values.Add(this._pk_offtypeid);
            return DAobj.GetDataTable("Comm_OfficeType_Edit", values, names, types);
        }

        public void PopulateGrid(DataGrid DgGrid)
        {
            clearall();
            names.Add("@pageindex"); types.Add(SqlDbType.Int); values.Add(this._pageindex);
            names.Add("@pagesize"); types.Add(SqlDbType.Int); values.Add(this._pagesize);
            DAobj.Grid_CustomPaging("Comm_OfficeType_SelForGrid", values, names, types, DgGrid, this._pageindex);
        }

        #endregion
    }
}