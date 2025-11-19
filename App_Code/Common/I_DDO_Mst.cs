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
    public interface I_DDO_Mst
    {
        #region Property
        
        int pageindex { get;set;}
        int pagesize { get;set;}
        int fk_districid { get;set;}
        string pk_ddoid { get;set;}
        string code { get;set;}
        string description { get;set;}
        string address1 { get;set;}
        string address2 { get;set;}
        string tanno { get;set;}
        string alias { get;set;}
        string remarks { get;set;}
        string fk_userid { get;set;}
        string fk_locid { get;set;}
        DateTime dated { get;set;}
        byte[] timestamp { get;set; }
        string displayorder { get; set; }

        #endregion

        #region Method

        int InsertRecord(ref string Message);
        int UpdateRecord(ref string Message);
        int DeleteCommand(ref string Message);
        DataTable EditRecords();
        void PopulateGrid(DataGrid DgGrid);
        DataSet PopulateDistric();

        string GetBtnCaption(string Key);
        string GetSysMessage(string Key);

        #endregion
    }
}
