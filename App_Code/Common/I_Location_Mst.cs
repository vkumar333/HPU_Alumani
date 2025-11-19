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
    public interface I_Location_Mst
    {
        #region Property
        
        int pageindex { get;set;}
        int pagesize { get;set;}
        int fk_districid { get;set;}
        int fk_officeid { get;set;}
        string pk_locid { get;set;}
        string locname { get;set;}
        string address { get;set;}
        string fk_plocid { get;set;}
        string alias { get;set;}
        string remarks { get;set;}
        string fk_userid { get;set;}
        string fk_locid { get;set;}
        string Filename { get;set;}
        string ContentType { get;set;}
        string UpdChange { get;set;}
        byte[] FileBytes { get;set; }
        byte[] timestamp { get;set; }
        int fk_holidaylocid { get; set; }

        #endregion

        #region Method

        int InsertRecord(ref string Message);
        int UpdateRecord(ref string Message);
        int DeleteCommand(ref string Message);
        DataTable EditRecords();
        void PopulateGrid(DataGrid DgGrid);
        DataSet PopulateDistric();
        DataSet PopulateOfficeType();
        DataSet PopulateParentLocation();
        DataSet PopulateHolidayLocation();
        string GetBtnCaption(string Key);
        string GetSysMessage(string Key);

        #endregion
    }
}
