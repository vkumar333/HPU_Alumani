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
    public class DDO_Location_Mapping : I_DDO_Location_Mapping
    {
        DataAccess DAobj = new DataAccess();
        ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

        #region Member
        int _pageindex, _pagesize;
        string _pk_mapid, _fk_ddoid, _fk_locid_map, _fk_userid, _fk_locid;
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

        public string pk_mapid
        {
            get
            {
                return _pk_mapid;
            }
            set
            {
                _pk_mapid = value;
            }
        }

        public string fk_ddoid
        {
            get
            {
                return _fk_ddoid;
            }
            set
            {
                _fk_ddoid = value;
            }
        }

        public string fk_locid_map
        {
            get
            {
                return _fk_locid_map;
            }
            set
            {
                _fk_locid_map = value;
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
            names.Add("@fk_ddoid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_ddoid);
            names.Add("@fk_locid_map"); types.Add(SqlDbType.VarChar); values.Add(this._fk_locid_map);
            names.Add("@fk_userid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_userid);
            names.Add("@fk_locid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_locid);
            if (DAobj.ExecuteTransactionMsg("Comm_DDO_Location_Map_Ins", values, names, types, ref Message) > 0)
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
            names.Add("@pk_mapid"); types.Add(SqlDbType.VarChar); values.Add(this._pk_mapid);
            names.Add("@fk_ddoid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_ddoid);
            names.Add("@fk_locid_map"); types.Add(SqlDbType.VarChar); values.Add(this._fk_locid_map);
            names.Add("@fk_userid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_userid);
            names.Add("@fk_locid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_locid);
            if (DAobj.ExecuteTransactionMsg("Comm_DDO_Location_Map_Upd", values, names, types, ref Message) > 0)
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
            names.Add("@pk_mapid"); types.Add(SqlDbType.VarChar); values.Add(this._pk_mapid);
            if (DAobj.ExecuteTransactionMsg("Comm_DDO_Location_Map_Del", values, names, types, ref Message) > 0)
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
            names.Add("@pk_mapid"); types.Add(SqlDbType.VarChar); values.Add(this._pk_mapid);
            return DAobj.GetDataTable("Comm_DDO_Location_Map_Edit", values, names, types);
        }

        public void PopulateGrid(DataGrid DgGrid)
        {
                clearall();
                names.Add("@pageindex"); types.Add(SqlDbType.Int); values.Add(this._pageindex);
                names.Add("@pagesize"); types.Add(SqlDbType.Int); values.Add(this._pagesize);
                names.Add("@fk_ddoid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_ddoid);
                DAobj.Grid_CustomPaging("Comm_DDO_Location_Map_SelForGrid", values, names, types, DgGrid, this._pageindex);
        }

        public DataSet PopulateDDO()
        {
            return DAobj.GetRecords("SAL_DDO_Selforddl");
        }

        public DataSet PopulateLocation()
        {
            clearall();
            names.Add("@fk_ddoid"); types.Add(SqlDbType.VarChar); values.Add(this._fk_ddoid);
            return DAobj.GetDataSet("Comm_DDO_Location_Map_Location_Selforddl", values, names, types);
        }
        #endregion
    }
}