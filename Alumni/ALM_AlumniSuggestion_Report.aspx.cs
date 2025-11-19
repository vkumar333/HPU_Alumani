using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using System.Web.UI;
using SubSonic;
using System.Web.UI.WebControls;
using NPOI.HSSF.Util;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;
using System.Text;

public partial class Alumni_ALM_AlumniSuggestion_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDefaultDate();
            Clear();
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void btn_Suggestion_Click(object sender, EventArgs e)
    {
        DateTime F_Date = Convert.ToDateTime(From_Date.Text);
        DateTime T_Date = Convert.ToDateTime(To_Date.Text);

        if (F_Date > T_Date)
        {
            ClientMessaging("From date is less than To date.");
            From_Date.Focus();
            return;
        }
        try
        {
            //string fromDate = CommonCode.DateFormats.Date_FrontToDB_R(From_Date.Text);
            //string toDate = CommonCode.DateFormats.Date_FrontToDB_R(To_Date.Text);

            //DataSet ds = ALM_AlumniSuggestion(fromDate, toDate).GetDataSet();
            DataSet ds = ALM_AlumniSuggestion(F_Date, T_Date).GetDataSet();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lblSuggestion.Text = "Lists of Alumni Suggestion's";
                dgv.DataSource = ds;
                dgv.DataBind();
                btnExportToExcel.Visible = true;
                btnExportToExcel.Enabled = true;
            }
            else
            {
                lblSuggestion.Text = "";
                dgv.DataSource = null;
                dgv.DataBind();
                btnExportToExcel.Visible = false;
                btnExportToExcel.Enabled = false;
                ClientMessaging("No Record Found !!");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    public StoredProcedure ALM_AlumniSuggestion(DateTime fromdate, DateTime todate)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_AlumniSuggestions_Report", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@From_Date", fromdate, DbType.DateTime);
        sp.Command.AddParameter("@To_Date", todate, DbType.DateTime);
        return sp;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        lblMsg.Text = "";
        //lblSuggestion.Text = "";
        SetDefaultDate();
        dgv.DataSource = null;
        dgv.DataBind();
        btnExportToExcel.Enabled = false;
        btnExportToExcel.Visible = false;
    }

    protected void SetDefaultDate()
    {
        try
        {
            //Setting Default values for From Date and To Date
            string FromDate, ToDate;
            FromDate = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
            int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            ToDate = DateTime.Now.ToString("MM/dd/yyyy");
            From_Date.Text = CommonCode.DateFormats.Date_DBToFront(FromDate).Replace("-", "/");
            To_Date.Text = CommonCode.DateFormats.Date_DBToFront(ToDate).Replace("-", "/");
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        string fromDate = CommonCode.DateFormats.Date_FrontToDB_O(From_Date.Text);
        string toDate = CommonCode.DateFormats.Date_FrontToDB_O(To_Date.Text);

        try
        {
            DateTime F_Date = Convert.ToDateTime(From_Date.Text);
            DateTime T_Date = Convert.ToDateTime(To_Date.Text);

            //DataSet ds = ALM_AlumniSuggestion(fromDate, toDate).GetDataSet();
            DataSet ds = ALM_AlumniSuggestion(F_Date, T_Date).GetDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ExportGridToExcel(ds);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    void ExportGridToExcel(DataSet ds)
    {
        int i, j, k, l, m, o, p, q; int pagecounter = 0; int counter = 0; int srno = 0; DataRow dr;
        int tdcount; int etdcount = 0; int dtdcount = 0; int pagesize = 0;
        int lineno = 0;

        counter = 0; etdcount = 0; tdcount = 5;

        HSSFWorkbook xlsWorkbook = new HSSFWorkbook(); //Make a new npoi workbook
        MemoryStream memoryStream = new MemoryStream();

        HSSFSheet xlsWorksheet = xlsWorkbook.CreateSheet("Sheet1"); //make a new sheet 
        HSSFRow headerRow = xlsWorksheet.CreateRow(0);//make a header row
                                                      //---------------------------------------------
        HSSFCell headerCell;
        HSSFCellStyle headerCellStyle = xlsWorkbook.CreateCellStyle();
        HSSFCellStyle headerCellStyle_font_Bold = xlsWorkbook.CreateCellStyle();  //for Bold
        NPOI.HSSF.Util.CellRangeAddress cra;

        var boldFont = xlsWorkbook.CreateFont();
        boldFont.FontHeightInPoints = 11;
        boldFont.FontName = "Calibri";
        boldFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        int row;
        string strFile = "Alumni Suggestion Report.xls";
        string filename = Server.MapPath("~/XLSFiles/" + strFile);
        string path = Server.MapPath("~/Images/hpu-logo.jpg");

        string compname = ds.Tables[1].Rows[0]["compname"].ToString().Trim();
        string subheading1 = ds.Tables[1].Rows[0]["SubHeading1"].ToString().Trim();
        string subheading3 = ds.Tables[1].Rows[0]["SubHeading3"].ToString().Trim();

        #region Create Company/Heading/SubHeading Header

        row = 0;
        headerCellStyle_font_Bold.SetFont(boldFont);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(compname);
        //Add Style to Cell
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.VerticalAlignment = 1;
        cra = new CellRangeAddress(row, row, 0, tdcount + 3);
        xlsWorksheet.AddMergedRegion(cra);

        row += 1;
        headerRow = xlsWorksheet.CreateRow(row);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(subheading1);
        //Add Style to Cell
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.VerticalAlignment = 1;
        cra = new CellRangeAddress(row, row, 0, tdcount + 3);
        xlsWorksheet.AddMergedRegion(cra);

        row += 1;
        headerRow = xlsWorksheet.CreateRow(row);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(subheading3);
        // Add Style to Cell
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.VerticalAlignment = 1;
        cra = new CellRangeAddress(row, row, 0, tdcount + 3);
        xlsWorksheet.AddMergedRegion(cra);

        #endregion

        int cellno = 0;
        row += 1;

        headerRow = xlsWorksheet.CreateRow(row);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue("S.No.");
        xlsWorksheet.SetColumnWidth(0, "S.No.".ToString().Length * 200);
        // Add Style to Cell
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(1);
        headerCell.SetCellValue("Alumni Name");
        xlsWorksheet.SetColumnWidth(1, "Alumni_Name".ToString().Length * 300);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        //---------------Cell 1----------------------------------
        headerCell = headerRow.CreateCell(2);
        headerCell.SetCellValue("Suggestion");
        xlsWorksheet.SetColumnWidth(1, "Suggestion".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(3);
        headerCell.SetCellValue("Suggestion Date");
        xlsWorksheet.SetColumnWidth(1, "Suggestiondate".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            srno = srno + 1;
            row += 1;
            headerRow = xlsWorksheet.CreateRow(row); //make a new row 
            headerCell = headerRow.CreateCell(0);
            // Set Cell Value
            headerCell.SetCellValue(srno);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(1);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["Alumni_Name"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["Alumni_Name"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(2);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["Suggestion"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["Suggestion"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(3);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["Suggestiondate"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["Suggestiondate"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;
        }

        xlsWorkbook.Write(memoryStream);
        memoryStream.Flush();

        HttpResponse response = HttpContext.Current.Response;
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", strFile));
        response.Clear();
        response.BinaryWrite(memoryStream.GetBuffer());
        response.End();
        response.Close();
        response.Flush();
    }
}