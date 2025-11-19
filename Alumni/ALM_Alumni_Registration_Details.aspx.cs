using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SubSonic;
using IUMSNXG;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using NPOI.HSSF.Util;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;

public partial class Alumni_ALM_Alumni_Registration_Details : System.Web.UI.Page
{
    CustomMessaging cm = new CustomMessaging();
    DataAccess DAobj = new DataAccess();
    private Boolean IsPageRefresh = false;
    CommonFunction Cfobj = new CommonFunction();
    crypto crp = new crypto();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //AlumniRegistration_RegistrationGrid();
            clear();
        }
    }

    public StoredProcedure compnyapprovedgrid()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("PLC_SP_ApprovedCompanyRegistration_List", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    public StoredProcedure companyRejectedGrid()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("PLC_SP_RejectedCompanyRegistration_List", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    public StoredProcedure PLC_Manage_companies_Ins(string doc)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("PLC_CompanyRegistration_ApprovedDtl_SP", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@xmlDoc", doc, DbType.String);
        return sp;
    }

    public StoredProcedure ALM_AlumniRegistrationDetals(string alumni, string gender, string fromdate, string todate, string report)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_AlumniRegistrationDetails_Filtered", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@alumni", alumni, DbType.String);
        sp.Command.AddParameter("@gender", gender, DbType.String);
        sp.Command.AddParameter("@RegsDateFrom", fromdate, DbType.String);
        sp.Command.AddParameter("@RegsDateTo", todate, DbType.String);
        sp.Command.AddParameter("@report", report, DbType.String);
        return sp;
    }

    void AlumniRegistration_RegistrationGrid()
    {
        try
        {
            string alumni = txtAlumni.Text.ToString() == "" ? null : txtAlumni.Text.ToString();
            string selectedGender = rblGender.SelectedValue == "" ? null : rblGender.Text.ToString();
            string selectedMemShipType = rblMemShipType.SelectedValue == "" ? null : rblMemShipType.SelectedValue.ToString();
            string selectedIsOldPortal = rblIsOldPortal.SelectedValue == "" ? null : rblIsOldPortal.SelectedItem.Text.ToString();

            string fromdate = V_txtStartDate.Text.ToString() == "" ? null : V_txtStartDate.Text.ToString();
            string todate = V_txtEndDate.Text.ToString() == "" ? null : V_txtEndDate.Text.ToString();
            string report = btnView.Text.ToString();
            string txtRegistration = txtRegistrationNo.Text.ToString() == "" ? null : txtRegistrationNo.Text.ToString();
            string Mobile = Txt_Mobile.Text.ToString() == "" ? null : Txt_Mobile.Text.ToString();
            string Email = Txt_Email.Text.ToString() == "" ? null : Txt_Email.Text.ToString();
            DataSet ds = Alumni_Details_Report(alumni, selectedGender, fromdate, todate, report, selectedMemShipType, selectedIsOldPortal, txtRegistration, Mobile, Email).GetDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dgv.DataSource = ds;
                dgv.DataBind();
                btnExportToExcel.Visible = true;
                btnExportToExcel.Enabled = true;
            }
            else
            {
                dgv.DataSource = null;
                dgv.DataBind();
                btnExportToExcel.Visible = false;
                btnExportToExcel.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    protected void dgv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";
        string aumniID = e.CommandArgument.ToString();
        if (e.CommandName.ToString().ToUpper() == "PRINT")
        {
            ViewReport(Convert.ToInt32(aumniID));
        }
    }

    protected void ViewReport(int aumniID)
    {
        try
        {
            lblMsg.Text = "";
            ReportDocument objRptDoc = new ReportDocument();
            string filename = "";

            try
            {
                DataSet dsAR = ALM_SP_AlumniRegistration_Details_Report_Print_By_Alumni(aumniID).GetDataSet();

                if (dsAR.Tables[0].Rows.Count > 0)
                {
                    dsAR.Tables[0].TableName = "Alumni_Registration_Details";
                    dsAR.Tables[1].TableName = "Company_Details";
                    dsAR.WriteXml(Server.MapPath("~/Alumni/ALM_XML/Alumni_Registration_Details_Report.xml"));
                    filename = Server.MapPath("~/Alumni/ALM_Reports/Alumni_Registration_Details_Report.rpt");
                    objRptDoc.Load(filename);
                    objRptDoc.SetDataSource(dsAR);

                    objRptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Alumni Registration Details Report");
                }
                else
                {
                    lblMsg.Text = "No Records Found!";
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
            }
            catch (Exception ex)
            {
                lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
            }
            finally
            {
                objRptDoc.Close();
                objRptDoc.Dispose();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    public StoredProcedure ALM_SP_AlumniRegistration_Details_Report_Print_By_Alumni(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_Details_Report", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@alumniid", alumniid, DbType.Int32);
        return sp;
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void dgv_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void dgv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //dgv.PageIndex = e.NewPageIndex;
        //AlumniRegistration_RegistrationGrid();
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string startDate = CommonCode.DateFormats.Date_FrontToDB_R(V_txtStartDate.Text.Trim());
            string EndDate = CommonCode.DateFormats.Date_FrontToDB_R(V_txtEndDate.Text.Trim());

            if (Convert.ToDateTime(startDate) > Convert.ToDateTime(EndDate))
            {
                ClientMessaging("From Date Can not be greater than To Date.");
                V_txtStartDate.Focus();
            }

            string alumni = txtAlumni.Text.ToString() == "" ? null : txtAlumni.Text.ToString();
            string selectedGender = rblGender.SelectedValue == "" ? null : rblGender.Text.ToString();
            string selectedMemShipType = rblMemShipType.SelectedValue == "" ? null : rblMemShipType.SelectedValue.ToString();
            string selectedIsOldPortal = rblIsOldPortal.SelectedValue == "" ? null : rblIsOldPortal.SelectedItem.Text.ToString();

            string fromdate = V_txtStartDate.Text.ToString() == "" ? null : V_txtStartDate.Text.ToString();
            string todate = V_txtEndDate.Text.ToString() == "" ? null : V_txtEndDate.Text.ToString();
            string report = btnView.Text.ToString();
            string txtRegistration = txtRegistrationNo.Text.ToString() == "" ? null : txtRegistrationNo.Text.ToString();
            string Mobile = Txt_Mobile.Text.ToString() == "" ? null : Txt_Mobile.Text.ToString();
            string Email = Txt_Email.Text.ToString() == "" ? null : Txt_Email.Text.ToString();

            DataSet ds = Alumni_Details_Report(alumni, selectedGender, fromdate, todate, report, selectedMemShipType, selectedIsOldPortal, txtRegistration, Mobile, Email).GetDataSet();

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

        //double[] STypeEtotAmt = new double[100]; double[] STypeDtotAmt = new double[100];
        //double[] FundTypeEtotAmt = new double[100]; double[] FundTypeDtotAmt = new double[100]; // Newly added : FundType
        //double[] BudEtotAmt = new double[100]; double[] BudDtotAmt = new double[100];
        //double[] grandEtotAmt = new double[100]; double[] grandDtotAmt = new double[100];

        //double STypetotgross = 0; double Budtotgross = 0; double grandtotgross = 0; double Fundtotgross = 0;
        //double STypetotIT = 0; double BudtotIT = 0; double grandtotIT = 0; double FundtotIT = 0;
        //double STypetotPTax = 0; double BudtotPTax = 0; double grandtotPTax = 0; double FundtotPTax = 0;
        //double STypetotded = 0; double Budtotded = 0; double grandtotded = 0; double Fundtotded = 0;
        //double STypetotnet = 0; double Budtotnet = 0; double grandtotnet = 0; double Fundtotnet = 0;

        //double OthAll = 0; double OthDed = 0;
        //double STypeOthAll = 0; double BudOthAll = 0; double grandtotOthAll = 0; double FundOthAll = 0;
        //double STypeOthDed = 0; double BudOthDed = 0; double grandtotOthDed = 0; double FundOthDed = 0;

        //double[] pageEtotAmt = new double[100]; double[] pageDtotAmt = new double[100];
        //double[] pagetotloanAmt = new double[100]; double[] pagetotInsAmt = new double[100];

        //double pagetotgross = 0; double pagetotded = 0; double pagetotIT = 0; double pagetotnet = 0;
        //double pagetotedcharge = 0;

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
        string strFile = "Alumni Regisration Report.xls";
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
        headerCell.SetCellValue("Alumni Registation No");
        xlsWorksheet.SetColumnWidth(1, "Alumni Registation No".ToString().Length * 300);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        //---------------Cell 1----------------------------------
        headerCell = headerRow.CreateCell(2);
        headerCell.SetCellValue("Salutation");
        xlsWorksheet.SetColumnWidth(1, "Salutation".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(3);
        headerCell.SetCellValue("Alumni Name");
        xlsWorksheet.SetColumnWidth(1, "Alumni Name".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(4);
        headerCell.SetCellValue("Membership Type");
        xlsWorksheet.SetColumnWidth(1, "Membership Type".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(5);
        headerCell.SetCellValue("Gender");
        xlsWorksheet.SetColumnWidth(1, "Gender".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(6);
        headerCell.SetCellValue("Year of Passing");
        xlsWorksheet.SetColumnWidth(1, "Year of Passing".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(7);
        headerCell.SetCellValue("Degree");
        xlsWorksheet.SetColumnWidth(1, "Degree".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(8);
        headerCell.SetCellValue("Mobile_No");
        xlsWorksheet.SetColumnWidth(1, "Mobile_No".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(9);
        headerCell.SetCellValue("Email");
        xlsWorksheet.SetColumnWidth(1, "Email".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(10);
        headerCell.SetCellValue("DOB");
        xlsWorksheet.SetColumnWidth(1, "DOB".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(11);
        headerCell.SetCellValue("Amount Paid");
        xlsWorksheet.SetColumnWidth(1, "Pay Amount".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(12);
        headerCell.SetCellValue("TransactionNo");
        xlsWorksheet.SetColumnWidth(1, "TransactionNo".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(13);
        headerCell.SetCellValue("Transaction Date");
        xlsWorksheet.SetColumnWidth(1, "Transaction Date".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(14);
        headerCell.SetCellValue("Payment Mode");
        xlsWorksheet.SetColumnWidth(1, "Payment Mode".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(15);
        headerCell.SetCellValue("Transaction Status");
        xlsWorksheet.SetColumnWidth(1, "Transaction Status".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(16);
        headerCell.SetCellValue("Registration Date");
        xlsWorksheet.SetColumnWidth(1, "Registration Date".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(17);
        headerCell.SetCellValue("Is Old Portal");
        xlsWorksheet.SetColumnWidth(1, "Is Old Portal".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            //cellno = 5;
            srno = srno + 1;

            row += 1;
            headerRow = xlsWorksheet.CreateRow(row); //make a new row 
            headerCell = headerRow.CreateCell(0);
            // Set Cell Value
            headerCell.SetCellValue(srno);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(1);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["alumnino"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["alumnino"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(2);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["ASal"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["ASal"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(3);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["Name"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["Name"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(4);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["alumniMemtype"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["alumniMemtype"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(5);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["gender"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["gender"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(6);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["yearofpassing"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["yearofpassing"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(7);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["DegreeName"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["DegreeName"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(8);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["Mobile_no"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["Mobile_no"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(9);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["email"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["email"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(10);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["dob"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["dob"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(11);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["amount"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["amount"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(12);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["TransactionID"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["TransactionID"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(13);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["TransactionDate"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["TransactionDate"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(14);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["PaymentMode"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["PaymentMode"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(15);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["TransactionStatus"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["TransactionStatus"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(16);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["RegsDate"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["RegsDate"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(17);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["isOld"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["isOld"].ToString().Length * 700);
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

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            string startDate = CommonCode.DateFormats.Date_FrontToDB_R(V_txtStartDate.Text.Trim());
            string EndDate = CommonCode.DateFormats.Date_FrontToDB_R(V_txtEndDate.Text.Trim());

            if (Convert.ToDateTime(startDate) > Convert.ToDateTime(EndDate))
            {
                ClientMessaging("From Date Can not be greater than To Date.");
                V_txtStartDate.Focus();
            }

            string alumni = txtAlumni.Text.ToString() == "" ? null : txtAlumni.Text.ToString();
            string selectedGender = rblGender.SelectedValue == "" ? null : rblGender.Text.ToString();
            string selectedMemShipType = rblMemShipType.SelectedValue == "" ? null : rblMemShipType.SelectedValue.ToString();
            string selectedIsOldPortal = rblIsOldPortal.SelectedValue == "" ? null : rblIsOldPortal.SelectedItem.Text.ToString();
            string fromdate = V_txtStartDate.Text.ToString() == "" ? null : V_txtStartDate.Text.ToString();
            string todate = V_txtEndDate.Text.ToString() == "" ? null : V_txtEndDate.Text.ToString();
            string report = btnView.Text.ToString();
            string txtRegistration = txtRegistrationNo.Text.ToString() == "" ? null : txtRegistrationNo.Text.ToString();
            string Mobile = Txt_Mobile.Text.ToString() == "" ? null : Txt_Mobile.Text.ToString();
            string Email = Txt_Email.Text.ToString() == "" ? null : Txt_Email.Text.ToString();

            DataSet ds = Alumni_Details_Report(alumni, selectedGender, fromdate, todate, report, selectedMemShipType, selectedIsOldPortal, txtRegistration, Mobile, Email).GetDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg1.Text = ds.Tables[0].Rows.Count + " Record Found.";
                dgv.DataSource = ds;
                dgv.DataBind();
                btnExportToExcel.Visible = true;
                btnExportToExcel.Enabled = true;
            }
            else
            {
                dgv.DataSource = null;
                dgv.DataBind();
                btnExportToExcel.Visible = false;
                btnExportToExcel.Enabled = false;
                lblMsg1.Text = "";
                ClientMessaging("No record found!");
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    //public static StoredProcedure Alumni_Details_Report(string alumni, string gender, string fromdate, string todate, string report)
    //{
    //    SubSonic.StoredProcedure sp = new StoredProcedure("ALM_AlumniRegistrationDetails_Filtered", DataService.GetInstance("IUMSNXG"), "");
    //    sp.Command.AddParameter("@alumni", alumni, DbType.String);
    //    sp.Command.AddParameter("@gender", gender, DbType.String);
    //    sp.Command.AddParameter("@RegsDateFrom", fromdate, DbType.String);
    //    sp.Command.AddParameter("@RegsDateTo", todate, DbType.String);
    //    sp.Command.AddParameter("@report", report, DbType.String);
    //    return sp;
    //}

    public static StoredProcedure Alumni_Details_Report(string alumni, string gender, string fromdate, string todate, string report, string MemShipType, string IsOldPortal,string Regno, string mobileNo, string EmailAdd)
    {
        SubSonic.StoredProcedure sp = new StoredProcedure("ALM_AlumniRegistrationDetails_Filtered", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@alumni", alumni, DbType.String);
        sp.Command.AddParameter("@gender", gender, DbType.String);
        sp.Command.AddParameter("@RegsDateFrom", fromdate, DbType.String);
        sp.Command.AddParameter("@RegsDateTo", todate, DbType.String);
        sp.Command.AddParameter("@report", report, DbType.String);
        sp.Command.AddParameter("@memshipType", MemShipType, DbType.String);
        sp.Command.AddParameter("@isOldAlm", IsOldPortal, DbType.String);
        sp.Command.AddParameter("@Regno", Regno, DbType.String);
        sp.Command.AddParameter("@mobileNo", mobileNo, DbType.String);
        sp.Command.AddParameter("@EmailAdd", EmailAdd, DbType.String);

        return sp;
    }

    private void clear()
    {
        txtAlumni.Text = ""; lblMsg1.Text = "";
        rblGender.SelectedIndex = -1; rblMemShipType.SelectedIndex = -1; rblIsOldPortal.SelectedIndex = -1;
        DateTime now = DateTime.Now;
        V_txtStartDate.Text = "01" + "/" + now.ToString("MM", CultureInfo.InvariantCulture) + "/" + "2021"; //now.ToString("yyyy", CultureInfo.InvariantCulture);
        V_txtEndDate.Text = now.ToString("dd", CultureInfo.InvariantCulture) + "/" + now.ToString("MM", CultureInfo.InvariantCulture) + "/" + now.ToString("yyyy", CultureInfo.InvariantCulture);
        //AlumniRegistration_RegistrationGrid();
        dgv.DataSource = null;
        dgv.DataBind();
        btnExportToExcel.Visible = false;
        btnExportToExcel.Enabled = false;
        txtRegistrationNo.Text = "";
        Txt_Email.Text = "";
        Txt_Mobile.Text = "";
        rblMemShipType.SelectedIndex = -1;
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string startDate = CommonCode.DateFormats.Date_FrontToDB_R(V_txtStartDate.Text.Trim());
            string EndDate = CommonCode.DateFormats.Date_FrontToDB_R(V_txtEndDate.Text.Trim());

            if (Convert.ToDateTime(startDate) > Convert.ToDateTime(EndDate))
            {
                ClientMessaging("From Date Can not be greater than To Date.");
                V_txtStartDate.Focus();
            }
            string alumni = txtAlumni.Text.ToString() == "" ? null : txtAlumni.Text.ToString();
            string selectedGender = rblGender.SelectedValue == "" ? null : rblGender.Text.ToString();
            string selectedMemShipType = rblMemShipType.SelectedValue == "" ? null : rblMemShipType.SelectedValue.ToString();
            string selectedIsOldPortal = rblIsOldPortal.SelectedValue == "" ? null : rblIsOldPortal.SelectedItem.Text.ToString();
            string fromdate = V_txtStartDate.Text.ToString() == "" ? null : V_txtStartDate.Text.ToString();
            string todate = V_txtEndDate.Text.ToString() == "" ? null : V_txtEndDate.Text.ToString();
            string report = btnSearch.Text.ToString();
            string txtRegistration = txtRegistrationNo.Text.ToString() == "" ? null : txtRegistrationNo.Text.ToString();
            string Mobile = Txt_Mobile.Text.ToString() == "" ? null : Txt_Mobile.Text.ToString();
            string Email = Txt_Email.Text.ToString() == "" ? null : Txt_Email.Text.ToString();
           DataSet ds = Alumni_Details_Report(alumni, selectedGender, fromdate, todate, report, selectedMemShipType, selectedIsOldPortal, txtRegistration, Mobile, Email).GetDataSet();
           if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg1.Text = ds.Tables[0].Rows.Count + " Record Found.";
                dgv.DataSource = ds;
                dgv.DataBind();
                btnExportToExcel.Visible = true;
                btnExportToExcel.Enabled = true;
            }
            else
            {
                dgv.DataSource = null;
                dgv.DataBind();
                btnExportToExcel.Visible = false;
                btnExportToExcel.Enabled = false;
                lblMsg1.Text = "";
                ClientMessaging("No record found!");
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        lblMsg1.Text = "";
        if (pnlSearch.Visible == true)
        {
            pnlSearch.Visible = false;
            Anthem.Manager.IncludePageScripts = true;
            txtAlumni.Focus();
        }
        else
        {
            pnlSearch.Visible = true;
            Anthem.Manager.IncludePageScripts = true;
            txtAlumni.Focus();
        }
    }

    public void Searchbydetail()
    {
        string alumni = txtAlumni.Text.ToString() == "" ? null : txtAlumni.Text.ToString();
        string selectedGender = rblGender.SelectedValue == "" ? null : rblGender.Text.ToString();
        string selectedMemShipType = rblMemShipType.SelectedValue == "" ? null : rblMemShipType.SelectedValue.ToString();
        string selectedIsOldPortal = rblIsOldPortal.SelectedValue == "" ? null : rblIsOldPortal.SelectedItem.Text.ToString();
        string fromdate = V_txtStartDate.Text.ToString() == "" ? null : V_txtStartDate.Text.ToString();
        string todate = V_txtEndDate.Text.ToString() == "" ? null : V_txtEndDate.Text.ToString();
        string report = btnSearch.Text.ToString();
        string txtRegistration = txtRegistrationNo.Text.ToString() == "" ? null : txtRegistrationNo.Text.ToString();
        string Mobile = Txt_Mobile.Text.ToString() == "" ? null : Txt_Mobile.Text.ToString();
        string Email = Txt_Email.Text.ToString() == "" ? null : Txt_Email.Text.ToString();

        DataSet ds = Alumni_Details_Report(alumni, selectedGender, fromdate, todate, report, selectedMemShipType, selectedIsOldPortal, txtRegistration, Mobile, Email).GetDataSet();

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMsg1.Text = ds.Tables[0].Rows.Count + " Record Found.";
            dgv.DataSource = ds;
            dgv.DataBind();
            btnExportToExcel.Visible = true;
            btnExportToExcel.Enabled = true;
        }
        else
        {
            dgv.DataSource = null;
            dgv.DataBind();
            btnExportToExcel.Visible = false;
            btnExportToExcel.Enabled = false;
            lblMsg1.Text = "";
            ClientMessaging("No record found!");
        }
    }
}