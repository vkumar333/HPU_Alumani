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
    public interface I_OfficeType_Mst
    {
        #region Property
        
        int pageindex { get;set;}
        int pagesize { get;set;}
        int pk_offtypeid { get;set;}
        string code { get;set;}
        string description { get;set;}
        byte[] timestamp { get;set; }

        #endregion

        #region Method

        int InsertRecord(ref string Message);
        int UpdateRecord(ref string Message);
        int DeleteCommand(ref string Message);
        DataTable EditRecords();
        void PopulateGrid(DataGrid DgGrid);
        
        string GetBtnCaption(string Key);
        string GetSysMessage(string Key);

        #endregion
    }
}
