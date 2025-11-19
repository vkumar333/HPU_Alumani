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

/// <summary>
/// Summary description for Admin_Branch_Mst
/// </summary>
namespace UMM
{
    public class Admin_Branch_Mst
    {
        DataAccess DAobj = new DataAccess();
        ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

        #region Members
        string _xmlDoc;
        int _pk_branchid;
        string _fk_userid;
        string _fk_locid;
        #endregion
        #region Properties
        public string fk_locid
        {
            get { return _fk_locid; }
            set { _fk_locid = value; }
        }
        public string fk_userid
        {
            get { return _fk_userid; }
            set { _fk_userid = value; }
        }
        public int pk_branchid
        {
            get { return _pk_branchid; }
            set { _pk_branchid = value; }
        }
        public string XmlDoc
        {
            get { return _xmlDoc; }
            set { _xmlDoc = value; }
        }

        #endregion
        #region Common Function
        void clearall()
        {
            names.Clear();
            values.Clear();
            types.Clear();
        }
        #endregion
        #region methods
        public int InsBranch(ref string Message)
        {
            clearall();
            names.Add("@xmlDoc"); values.Add(this._xmlDoc); types.Add(SqlDbType.VarChar);
            names.Add("@Fk_UserID"); values.Add(this._fk_userid); types.Add(SqlDbType.VarChar);
            names.Add("@Fk_LocID"); values.Add(this._fk_locid); types.Add(SqlDbType.VarChar);
            if (DAobj.ExecuteTransactionMsg("UM_SP_Branch_Mst_Ins", values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int UpdateBranch(ref string Message)
        {
            clearall();
            names.Add("@xmlDoc"); values.Add(_xmlDoc); types.Add(SqlDbType.VarChar);
            names.Add("@pk_branchid"); values.Add(this._pk_branchid); types.Add(SqlDbType.Int);
            names.Add("@Fk_UserID"); values.Add(this._fk_userid); types.Add(SqlDbType.VarChar);
            names.Add("@Fk_LocID"); values.Add(this._fk_locid); types.Add(SqlDbType.VarChar);

            if (DAobj.ExecuteTransactionMsg("UM_SP_Branch_Mst_Upd", values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("U", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int DeleteBranch(ref string Message)
        {
            clearall();
            names.Add("@pk_branchid"); values.Add(this._pk_branchid); types.Add(SqlDbType.Int);
            if (DAobj.ExecuteTransactionMsg("UM_SP_Branch_Mst_Del", values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("D", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public DataSet FillBranchDatails()
        {
            return DAobj.GetRecords("UM_SP_Branch_Mst_SelForGrid");
        }
        public DataSet GetBranchDetails()
        {
            clearall();
            names.Add("@pk_branchid"); values.Add(this._pk_branchid); types.Add(SqlDbType.Int);
            return DAobj.GetDataSet("UM_Branch_Mst_Edit", values, names, types);
        }
        #endregion
    }
}