using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace CommonForms
{
    public interface I_DDO_Location_Mapping
    {
        #region Property
        int pageindex { get;set;}
        int pagesize { get;set;}
        string pk_mapid { get;set;}
        string fk_ddoid { get;set;}
        string fk_locid_map { get;set;}

        string fk_userid { get;set;}
        string fk_locid { get;set;}
        #endregion

        #region Method
        int InsertRecord(ref string Message);
        int UpdateRecord(ref string Message);
        int DeleteCommand(ref string Message);
        DataTable EditRecords();
        void PopulateGrid(DataGrid DgGrid);
        DataSet PopulateDDO();
        DataSet PopulateLocation();

        string GetBtnCaption(string Key);
        string GetSysMessage(string Key);
        #endregion
    }
}
