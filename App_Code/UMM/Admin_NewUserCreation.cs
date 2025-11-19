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
/// Summary description for Admin_NewUserCreation
/// </summary>
namespace UMM
{
    public class Admin_NewUserCreation
    {
        DataAccess DAobj = new DataAccess();
        ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
        #region Members
        string _pk_empid;
        string _fk_ddoid, _empcode, _empcodemanual, _empname, _fk_locationid, _fk_classid, _fk_departmentid, _fk_designationid, _fk_nature, _fk_cityid, _fk_officetypeid, _status, _sortby, _sepstatus;
        string _pageindex;
        string _pagesize;
        string _UserId;
        string _fk_userid;
        string _token;
        string _session;
        #endregion

        #region Properties
        public string pageindex
        {
            get { return _pageindex; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Page Index cannot be set to null");
                }
                _pageindex = value;
            }
        }
        public string pagesize
        {
            get { return _pagesize; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Pagesize cannot be set to null");
                }
                _pagesize = value;
            }
        }
        public string pk_empid
        {
            get { return _pk_empid; }
            set { _pk_empid = value; }
        }
        public string fk_ddoid
        {
            get { return _fk_ddoid; }
            set { _fk_ddoid = value; }
        }
        public string empcode
        {
            get { return _empcode; }
            set { _empcode = value; }
        }
        public string empcodemanual
        {
            get { return _empcodemanual; }
            set { _empcodemanual = value; }
        }
        public string empname
        {
            get { return _empname; }
            set { _empname = value; }
        }
        public string fk_locationid
        {
            get { return _fk_locationid; }
            set { _fk_locationid = value; }
        }
        public string fk_departmentid
        {
            get { return _fk_departmentid; }
            set { _fk_departmentid = value; }
        }
        public string fk_designationid
        {
            get { return _fk_designationid; }
            set { _fk_designationid = value; }
        }
        public string fk_nature
        {
            get { return _fk_nature; }
            set { _fk_nature = value; }
        }
        public string fk_cityid
        {
            get { return _fk_cityid; }
            set { _fk_cityid = value; }
        }
        public string fk_officetypeid
        {
            get { return _fk_officetypeid; }
            set { _fk_officetypeid = value; }
        }
        public string fk_userid
        {
            get { return _fk_userid; }
            set { _fk_userid = value; }
        }
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string sepstatus
        {
            get { return _sepstatus; }
            set { _sepstatus = value; }
        }
        public string sortby
        {
            get { return _sortby; }
            set { _sortby = value; }
        }
        public string UserId
        {
            get { return _UserId; }
            set
            {
                if (value == null || value == "")
                {
                    throw new ArgumentNullException("User Id cannot be set to null");
                }
                _UserId = value;
            }
        }
        public string fk_classid
        {
            get { return _fk_classid; }
            set { _fk_classid = value; }
        }
        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
        public string Session_value
        {
            get { return _session; }
            set { _session = value; }
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

        #region Methods
        public DataSet FillBand()
        {
            try
            {
                return DAobj.GetRecords("SAL_Band_SelForddl");
            }
            catch { throw; }
        }

        public DataSet FillEntity()
        {
            try
            {
                clearall();
                names.Add("@fk_userid"); values.Add(UserId); types.Add(SqlDbType.VarChar);
                return DAobj.GetDataSet("SAL_DDO_SelForddl_Userwise", values, names, types);
            }
            catch { throw; }
        }
        public DataSet FillFunction()
        {
            try
            {
                return DAobj.GetRecords("SAL_Function_SelFordll");
            }
            catch { throw; }
        }

        public DataSet FillLocation()
        {
            try
            {
                clearall();
                names.Add("@fk_ddoid"); values.Add(fk_ddoid); types.Add(SqlDbType.VarChar);
                names.Add("@UserID"); values.Add(fk_userid); types.Add(SqlDbType.VarChar);
                names.Add("@fk_officeid"); values.Add(fk_officetypeid); types.Add(SqlDbType.VarChar);
                return DAobj.GetDataSet("SAL_Location_SelForddl_OnOfficenDDO", values, names, types);
            }
            catch { throw; }
        }

        public DataSet FillDepartment()
        {
            try
            {
                return DAobj.GetRecords("SAL_Department_SelForddl");
            }
            catch { throw; }
        }

        public DataSet FillDesignation()
        {
            try
            {
                return DAobj.GetRecords("SAL_Designation_SelForddl");
            }
            catch { throw; }
        }

        public DataSet FillNatureType()
        {
            try
            {
                return DAobj.GetRecords("SAL_Nature_SelForddl");
            }
            catch { throw; }
        }

        public DataSet FillPostingCity()
        {
            try
            {
                return DAobj.GetRecords("SAL_City_SelForddl");
            }
            catch { throw; }
        }

        public int DeleteEmployee()
        {
            clearall();
            names.Add("@Pk_Empid"); values.Add(pk_empid); types.Add(SqlDbType.VarChar);
            if (DAobj.ExecuteTransaction("SAL_Employee_Del", values, names, types) > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Insert_Module_Session()
        {
            clearall();
            names.Add("@token"); values.Add(Token); types.Add(SqlDbType.VarChar);
            names.Add("@session"); values.Add(Session_value); types.Add(SqlDbType.VarChar);

            if (DAobj.ExecuteTransaction("Session_Details_Ins", values, names, types) > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public DataSet GetDesignation_OnClass()
        {
            try
            {
                clearall();
                names.Add("@fk_classid"); values.Add(fk_classid); types.Add(SqlDbType.VarChar);
                return DAobj.GetDataSet("SAL_GetDesigntion_OnClass", values, names, types);
            }
            catch { throw; }

        }
        public void getEmployeeList_UserMaster(DataGrid dgDetails)
        {
            clearall();
            names.Add("@pageindex"); values.Add(pageindex); types.Add(SqlDbType.VarChar);
            names.Add("@pagesize"); values.Add(pagesize); types.Add(SqlDbType.VarChar);
            names.Add("@empcode"); values.Add(empcode); types.Add(SqlDbType.VarChar);
            names.Add("@empcodemanual"); values.Add(empcodemanual); types.Add(SqlDbType.VarChar);
            names.Add("@empname"); values.Add(empname); types.Add(SqlDbType.VarChar);
            names.Add("@fk_ddoid"); values.Add(fk_ddoid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_locationid"); values.Add(fk_locationid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_departmentid"); values.Add(fk_departmentid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_classid"); values.Add(fk_classid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_designationid"); values.Add(fk_designationid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_nature"); values.Add(fk_nature); types.Add(SqlDbType.VarChar);
            names.Add("@fk_cityid"); values.Add(fk_cityid); types.Add(SqlDbType.VarChar);
            names.Add("@fk_officetypeid"); values.Add(fk_officetypeid); types.Add(SqlDbType.VarChar);
            names.Add("@status"); values.Add(status); types.Add(SqlDbType.VarChar);
            names.Add("@shortby"); values.Add(sortby); types.Add(SqlDbType.VarChar);
            names.Add("@sepstatus"); values.Add(sepstatus); types.Add(SqlDbType.VarChar);
            names.Add("@fk_userid"); values.Add(UserId); types.Add(SqlDbType.VarChar);
            DAobj.Grid_CustomPaging("SAL_EmployeeSearch_User_Mst", values, names, types, dgDetails, Convert.ToInt16(pageindex));
        }

        // For Manage Page Rights 


        public DataSet UM_UserPageRights_Rpt()
        {
            try
            {
                clearall();
                names.Add("@fk_userid"); values.Add(fk_userid); types.Add(SqlDbType.VarChar);
                return DAobj.GetDataSet("UM_UserPageRights_Rpt", values, names, types);
            }
            catch { throw; }

        }


        #endregion
    }
}