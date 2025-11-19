/*
==================================================================================
Created By                                                : Ayush Tyagi
On Date                                                   :28 feb 2023
Name                                                      :ApproveAlumnibyadmin 
Purpose                                                   :  
Tables used                                               :ALM_AlumniRegistration  
Stored Procedures used                                    :Alm_AlumniReqAprrovalRej
Modules                                                   :Alumni
Form                                                      :ApproveAlumnibyadmin.aspx
Last Updated Date                                         :
Last Updated By                                           :
==================================================================================
 */
using System;
using System.Web.UI.WebControls;
using SubSonic;
using System.Data;
using DataAccessLayer;
using System.Collections;
using System.Web;
using NPOI.HSSF.Util;
using NPOI.HSSF.UserModel;
using System.IO;

public partial class Alumni_ApproveAlumnibyadmin : System.Web.UI.Page
{
    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    public DataAccess DAobj = new DataAccess();
    //Array Parameter Declared
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            almRequest_Action();
        }
    }

    // Using
    public static StoredProcedure AlumniPendendingReq()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_AlumniReqAprrovalRej", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    // To Bind Alumni Requests
    protected void almRequest_Action()
    {
        DataSet ds = AlumniPendendingReq().GetDataSet();
        gvpendinreq.DataSource = ds.Tables[0];
        gvpendinreq.DataBind();
        gvapproved.DataSource = ds.Tables[1];
        gvapproved.DataBind();
        gvrejected.DataSource = ds.Tables[2];
        gvrejected.DataBind();
        //gv_Waitinglist.DataSource = ds.Tables[3];
        //gv_Waitinglist.DataBind();

    }

    protected void gvpendinreq_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void btnapprove_Click(object sender, EventArgs e)
    {

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

    }

    protected void btnView_Click(object sender, EventArgs e)
    {

    }

    protected void gvapproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void btnexcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds;
            ds = GetPendingAlumini();
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

    public DataSet GetPendingAlumini()
    {
        try
        {
            ClearArrayLists();
            return DAobj.GetDataSet("Alm_Pending_Excel", values, names, types);
        }
        catch { throw; }
    }

    void ExportGridToExcel(DataSet ds)
    {
        int i, j, k, l, m, o, p, q; int pagecounter = 0; int counter = 0; int srno = 0; DataRow dr;
        int tdcount; int etdcount = 0; int dtdcount = 0; int pagesize = 0;
        int lineno = 0;

        double[] STypeEtotAmt = new double[100]; double[] STypeDtotAmt = new double[100];
        double[] FundTypeEtotAmt = new double[100]; double[] FundTypeDtotAmt = new double[100]; // Newly added : FundType
        double[] BudEtotAmt = new double[100]; double[] BudDtotAmt = new double[100];
        double[] grandEtotAmt = new double[100]; double[] grandDtotAmt = new double[100];

        double STypetotgross = 0; double Budtotgross = 0; double grandtotgross = 0; double Fundtotgross = 0;
        double STypetotIT = 0; double BudtotIT = 0; double grandtotIT = 0; double FundtotIT = 0;
        double STypetotPTax = 0; double BudtotPTax = 0; double grandtotPTax = 0; double FundtotPTax = 0;
        double STypetotded = 0; double Budtotded = 0; double grandtotded = 0; double Fundtotded = 0;
        double STypetotnet = 0; double Budtotnet = 0; double grandtotnet = 0; double Fundtotnet = 0;

        double OthAll = 0; double OthDed = 0;
        double STypeOthAll = 0; double BudOthAll = 0; double grandtotOthAll = 0; double FundOthAll = 0;
        double STypeOthDed = 0; double BudOthDed = 0; double grandtotOthDed = 0; double FundOthDed = 0;

        double[] pageEtotAmt = new double[100]; double[] pageDtotAmt = new double[100];
        double[] pagetotloanAmt = new double[100]; double[] pagetotInsAmt = new double[100];

        double pagetotgross = 0; double pagetotded = 0; double pagetotIT = 0; double pagetotnet = 0;
        double pagetotedcharge = 0;

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
        boldFont.Boldweight = NPOI.HSSF.UserModel.HSSFFont.BOLDWEIGHT_BOLD;
        int row;
        string strFile = "Index Report.xls";
        string filename = Server.MapPath("~/XLSFiles/" + strFile);
        string path = Server.MapPath("~/Images/hpu-logo.jpg");

        //  int totrowinpage = Convert.ToInt16(Session["Rows"].ToString().Trim());

        string compname = ds.Tables[1].Rows[0]["compname"].ToString().Trim();
        //string monthyear = "SALARY BILL AND ACQUITTANCE ROLL FOR THE MONTH OF " + ds.Tables[0].Rows[0]["monthyear"].ToString().Trim();
        string subheading1 = ds.Tables[1].Rows[0]["SubHeading1"].ToString().Trim();
        string subheading2 = ds.Tables[1].Rows[0]["SubHeading2"].ToString().Trim();
        string subheading3 = ds.Tables[1].Rows[0]["SubHeading3"].ToString().Trim();

        #region Create Company/Heading/SubHeading Header
        row = 0;
        headerCellStyle_font_Bold.SetFont(boldFont);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(compname);
        // Add Style to Cell
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.VerticalAlignment = 1;
        cra = new CellRangeAddress(row, row, 0, tdcount + 3);
        xlsWorksheet.AddMergedRegion(cra);

        row += 1;
        headerRow = xlsWorksheet.CreateRow(row);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(subheading1);
        // Add Style to Cell
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.VerticalAlignment = 1;
        cra = new CellRangeAddress(row, row, 0, tdcount + 3);
        xlsWorksheet.AddMergedRegion(cra);

        row += 1;
        headerRow = xlsWorksheet.CreateRow(row);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(subheading2);
        // Add Style to Cell
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
        headerCell.SetCellValue("Enrollment No.");
        xlsWorksheet.SetColumnWidth(1, "Enrollment No.".ToString().Length * 300);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        //-------------------------------Cell 1----------------------------------//
        headerCell = headerRow.CreateCell(2);
        headerCell.SetCellValue("Alumni Name");
        xlsWorksheet.SetColumnWidth(1, "Alumni Name".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(3);
        headerCell.SetCellValue("Father Name");
        xlsWorksheet.SetColumnWidth(1, "Father Name".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(4);
        headerCell.SetCellValue("Mother Name");
        xlsWorksheet.SetColumnWidth(1, "Mother Name".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(5);
        headerCell.SetCellValue("Date of Birth");
        xlsWorksheet.SetColumnWidth(1, "Date of Birth".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        //-----------------------------Cell 2----------------------------------//
        headerCell = headerRow.CreateCell(6);
        headerCell.SetCellValue("Department of the University/College/Inst");
        xlsWorksheet.SetColumnWidth(1, "Department of the University/College/Inst".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(7);
        headerCell.SetCellValue("Highest Degree");
        xlsWorksheet.SetColumnWidth(1, "Highest Degree".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(8);
        headerCell.SetCellValue("Year of Passing");
        xlsWorksheet.SetColumnWidth(1, "Year of Passing".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(9);
        headerCell.SetCellValue("Email ID");
        xlsWorksheet.SetColumnWidth(1, "Email ID".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(10);
        headerCell.SetCellValue("Mobile No.");
        xlsWorksheet.SetColumnWidth(1, "Mobile No.".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(11);
        headerCell.SetCellValue("Presently Working In (Organization/Institution)");
        xlsWorksheet.SetColumnWidth(1, "Presently Working In (Organization/Institution)".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(12);
        headerCell.SetCellValue("Designation");
        xlsWorksheet.SetColumnWidth(1, "Designation".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(13);
        headerCell.SetCellValue("Permanent Address");
        xlsWorksheet.SetColumnWidth(1, "Permanent Address".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(14);
        headerCell.SetCellValue("Present Address");
        xlsWorksheet.SetColumnWidth(1, "Present Address".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(15);
        headerCell.SetCellValue("Expertise");
        xlsWorksheet.SetColumnWidth(1, "Expertise".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(16);
        headerCell.SetCellValue("Outstanding Achievements/ Honor/ Award/Position");
        xlsWorksheet.SetColumnWidth(1, "Outstanding Achievements/ Honor/ Award/Position".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(17);
        headerCell.SetCellValue("Gender");
        xlsWorksheet.SetColumnWidth(1, "Gender".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(18);
        headerCell.SetCellValue("Remarks");
        xlsWorksheet.SetColumnWidth(1, "Remarks".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(19);
        headerCell.SetCellValue("Is Mentor");
        xlsWorksheet.SetColumnWidth(1, "Is Mentor".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        for (m = 0; m < ds.Tables[0].Rows.Count; m++)
        {
            DataRow[] drBud = ds.Tables[0].Select("pk_alumniid= '" + ds.Tables[0].Rows[m]["pk_alumniid"].ToString().Trim() + "'");

            if (drBud.Length > 0)
            {
                for (i = 0; i < drBud.Length; i++)
                {
                    //cellno = 5;
                    srno = srno + 1;

                    row += 1;
                    headerRow = xlsWorksheet.CreateRow(row);//make a new row 
                    headerCell = headerRow.CreateCell(0);
                    // Set Cell Value
                    headerCell.SetCellValue(srno);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(1);
                    headerCell.SetCellValue(drBud[i]["alumnino"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["alumnino"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(2);
                    headerCell.SetCellValue(drBud[i]["alumni_name"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["alumni_name"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(3);
                    headerCell.SetCellValue(drBud[i]["fathername"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["fathername"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(4);
                    headerCell.SetCellValue(drBud[i]["mothername"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["mothername"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(5);
                    headerCell.SetCellValue(drBud[i]["dob"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["dob"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(6);
                    headerCell.SetCellValue(drBud[i]["fk_collegeid"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["fk_collegeid"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(7);
                    headerCell.SetCellValue(drBud[i]["fk_degreeid"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["fk_degreeid"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(8);
                    headerCell.SetCellValue(drBud[i]["yearofpassing"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["yearofpassing"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(9);
                    headerCell.SetCellValue(drBud[i]["email"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["email"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(10);
                    headerCell.SetCellValue(drBud[i]["contactno"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["contactno"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(11);
                    headerCell.SetCellValue(drBud[i]["currentoccupation"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["currentoccupation"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(12);
                    headerCell.SetCellValue(drBud[i]["designation"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["designation"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(13);
                    headerCell.SetCellValue(drBud[i]["per_address"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["per_address"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(14);
                    headerCell.SetCellValue(drBud[i]["currentaddress"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["currentaddress"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(15);
                    headerCell.SetCellValue(drBud[i]["special_interest"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["special_interest"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(16);
                    headerCell.SetCellValue(drBud[i]["Achievement"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["Achievement"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(17);
                    headerCell.SetCellValue(drBud[i]["gender"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["gender"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(18);
                    headerCell.SetCellValue(drBud[i]["remarks"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["remarks"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(19);
                    headerCell.SetCellValue(drBud[i]["isMentor"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["isMentor"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;
                }
            }
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

    protected void ButtonApproved_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds;
            ds = GetApprovedAlumini();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ExportApprovedGridToExcel(ds);
            }
        }
        catch (Exception ex)
        {
            Labelmsg.Text = ex.Message;
        }
    }

    public DataSet GetApprovedAlumini()
    {
        try
        {
            ClearArrayLists();
            return DAobj.GetDataSet("Alm_ApprovedAlumniExcel", values, names, types);
        }
        catch { throw; }
    }

    void ExportApprovedGridToExcel(DataSet ds)
    {
        int i, j, k, l, m, o, p, q; int pagecounter = 0; int counter = 0; int srno = 0; DataRow dr;
        int tdcount; int etdcount = 0; int dtdcount = 0; int pagesize = 0;
        int lineno = 0;

        double[] STypeEtotAmt = new double[100]; double[] STypeDtotAmt = new double[100];
        double[] FundTypeEtotAmt = new double[100]; double[] FundTypeDtotAmt = new double[100]; // Newly added : FundType
        double[] BudEtotAmt = new double[100]; double[] BudDtotAmt = new double[100];
        double[] grandEtotAmt = new double[100]; double[] grandDtotAmt = new double[100];

        double STypetotgross = 0; double Budtotgross = 0; double grandtotgross = 0; double Fundtotgross = 0;
        double STypetotIT = 0; double BudtotIT = 0; double grandtotIT = 0; double FundtotIT = 0;
        double STypetotPTax = 0; double BudtotPTax = 0; double grandtotPTax = 0; double FundtotPTax = 0;
        double STypetotded = 0; double Budtotded = 0; double grandtotded = 0; double Fundtotded = 0;
        double STypetotnet = 0; double Budtotnet = 0; double grandtotnet = 0; double Fundtotnet = 0;

        double OthAll = 0; double OthDed = 0;
        double STypeOthAll = 0; double BudOthAll = 0; double grandtotOthAll = 0; double FundOthAll = 0;
        double STypeOthDed = 0; double BudOthDed = 0; double grandtotOthDed = 0; double FundOthDed = 0;

        double[] pageEtotAmt = new double[100]; double[] pageDtotAmt = new double[100];
        double[] pagetotloanAmt = new double[100]; double[] pagetotInsAmt = new double[100];

        double pagetotgross = 0; double pagetotded = 0; double pagetotIT = 0; double pagetotnet = 0;
        double pagetotedcharge = 0;

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
        boldFont.Boldweight = NPOI.HSSF.UserModel.HSSFFont.BOLDWEIGHT_BOLD;
        int row;
        string strFile = "Index Report.xls";
        string filename = Server.MapPath("~/XLSFiles/" + strFile);
        string path = Server.MapPath("~/Images/hpu-logo.jpg");

        // int totrowinpage = Convert.ToInt16(Session["Rows"].ToString().Trim());

        string compname = ds.Tables[1].Rows[0]["compname"].ToString().Trim();
        // string monthyear = "SALARY BILL AND ACQUITTANCE ROLL FOR THE MONTH OF " + ds.Tables[1].Rows[0]["monthyear"].ToString().Trim();
        string subheading1 = ds.Tables[1].Rows[0]["SubHeading1"].ToString().Trim();
        string subheading2 = ds.Tables[1].Rows[0]["SubHeading2"].ToString().Trim();
        string subheading3 = ds.Tables[1].Rows[0]["SubHeading3"].ToString().Trim();

        #region Create Company/Heading/SubHeading Header
        row = 0;
        headerCellStyle_font_Bold.SetFont(boldFont);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(compname);
        //  Add Style to Cell
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.VerticalAlignment = 1;
        cra = new CellRangeAddress(row, row, 0, tdcount + 3);
        xlsWorksheet.AddMergedRegion(cra);

        row += 1;
        headerRow = xlsWorksheet.CreateRow(row);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(subheading1);
        // Add Style to Cell
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.VerticalAlignment = 1;
        cra = new CellRangeAddress(row, row, 0, tdcount + 3);
        xlsWorksheet.AddMergedRegion(cra);

        row += 1;
        headerRow = xlsWorksheet.CreateRow(row);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(subheading2);
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
        headerCell.SetCellValue("Enrollment No.");
        xlsWorksheet.SetColumnWidth(1, "Enrollment No.".ToString().Length * 300);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        //---------------------------Cell 1----------------------------------//
        headerCell = headerRow.CreateCell(2);
        headerCell.SetCellValue("Alumni Name");
        xlsWorksheet.SetColumnWidth(1, "Alumni Name".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(3);
        headerCell.SetCellValue("Father Name");
        xlsWorksheet.SetColumnWidth(1, "Father Name".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(4);
        headerCell.SetCellValue("D.O.B");
        xlsWorksheet.SetColumnWidth(1, "D.O.B".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(5);
        headerCell.SetCellValue("Alumni Nature");
        xlsWorksheet.SetColumnWidth(1, "Alumni Nature".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(6);
        headerCell.SetCellValue("Department of the University/College/Inst");
        xlsWorksheet.SetColumnWidth(1, "Department of the University/College/Inst".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(7);
        headerCell.SetCellValue("Highest Degree");
        xlsWorksheet.SetColumnWidth(1, "Highest Degree".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(8);
        headerCell.SetCellValue("Year of Passing");
        xlsWorksheet.SetColumnWidth(1, "Year of Passing".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(9);
        headerCell.SetCellValue("Email ID");
        xlsWorksheet.SetColumnWidth(1, "Email ID".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(10);
        headerCell.SetCellValue("Mobile No.");
        xlsWorksheet.SetColumnWidth(1, "Mobile No.".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(11);
        headerCell.SetCellValue("Presently Working In (Organization/Institution)");
        xlsWorksheet.SetColumnWidth(1, "Presently Working In (Organization/Institution)".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(12);
        headerCell.SetCellValue("Designation");
        xlsWorksheet.SetColumnWidth(1, "Designation".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(13);
        headerCell.SetCellValue("Permanent Address");
        xlsWorksheet.SetColumnWidth(1, "Permanent Address".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(14);
        headerCell.SetCellValue("Present Address");
        xlsWorksheet.SetColumnWidth(1, "Present Address".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(15);
        headerCell.SetCellValue("Expertise");
        xlsWorksheet.SetColumnWidth(1, "Expertise".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(16);
        headerCell.SetCellValue("Outstanding Achievements/ Honor/ Award/Position");
        xlsWorksheet.SetColumnWidth(1, "Outstanding Achievements/ Honor/ Award/Position".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(17);
        headerCell.SetCellValue("Gender");
        xlsWorksheet.SetColumnWidth(1, "Gender".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(18);
        headerCell.SetCellValue("Remarks");
        xlsWorksheet.SetColumnWidth(1, "Remarks".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(19);
        headerCell.SetCellValue("Is Mentor");
        xlsWorksheet.SetColumnWidth(1, "Is Mentor".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;


        for (m = 0; m < ds.Tables[0].Rows.Count; m++)
        {
            DataRow[] drBud = ds.Tables[0].Select("pk_alumniid= '" + ds.Tables[0].Rows[m]["pk_alumniid"].ToString().Trim() + "'");

            if (drBud.Length > 0)
            {
                for (i = 0; i < drBud.Length; i++)
                {
                    //cellno = 5;
                    srno = srno + 1;

                    row += 1;
                    headerRow = xlsWorksheet.CreateRow(row);//make a new row 
                    headerCell = headerRow.CreateCell(0);
                    // Set Cell Value
                    headerCell.SetCellValue(srno);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(1);
                    headerCell.SetCellValue(drBud[i]["alumnino"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["alumnino"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(2);
                    headerCell.SetCellValue(drBud[i]["alumni_name"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["alumni_name"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(3);
                    headerCell.SetCellValue(drBud[i]["fathername"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["fathername"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(4);
                    headerCell.SetCellValue(drBud[i]["dob"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["dob"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(5);
                    headerCell.SetCellValue(drBud[i]["alumnitype"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["alumnitype"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(6);
                    headerCell.SetCellValue(drBud[i]["fk_collegeid"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["fk_collegeid"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(7);
                    headerCell.SetCellValue(drBud[i]["fk_degreeid"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["fk_degreeid"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(8);
                    headerCell.SetCellValue(drBud[i]["yearofpassing"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["yearofpassing"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(9);
                    headerCell.SetCellValue(drBud[i]["email"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["email"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(10);
                    headerCell.SetCellValue(drBud[i]["contactno"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["contactno"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(11);
                    headerCell.SetCellValue(drBud[i]["currentoccupation"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["currentoccupation"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(12);
                    headerCell.SetCellValue(drBud[i]["designation"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["designation"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(13);
                    headerCell.SetCellValue(drBud[i]["per_address"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["per_address"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(14);
                    headerCell.SetCellValue(drBud[i]["currentaddress"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["currentaddress"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(15);
                    headerCell.SetCellValue(drBud[i]["special_interest"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["special_interest"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(16);
                    headerCell.SetCellValue(drBud[i]["Achievement"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["Achievement"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(17);
                    headerCell.SetCellValue(drBud[i]["gender"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["gender"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(18);
                    headerCell.SetCellValue(drBud[i]["remarks"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["remarks"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(19);
                    headerCell.SetCellValue(drBud[i]["isMentor"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["isMentor"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;
                }
            }
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

    protected void ButtonReject_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds;
            ds = GetRejectAlumini();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ExportRejectGridToExcel(ds);
            }
        }
        catch (Exception ex)
        {
            lblmsgs.Text = ex.Message;
        }
    }

    public DataSet GetRejectAlumini()
    {
        try
        {
            ClearArrayLists();
            return DAobj.GetDataSet("Alm_RejectAlumniExcel", values, names, types);
        }
        catch { throw; }
    }

    void ExportRejectGridToExcel(DataSet ds)
    {
        int i, j, k, l, m, o, p, q; int pagecounter = 0; int counter = 0; int srno = 0; DataRow dr;
        int tdcount; int etdcount = 0; int dtdcount = 0; int pagesize = 0;
        int lineno = 0;

        double[] STypeEtotAmt = new double[100]; double[] STypeDtotAmt = new double[100];
        double[] FundTypeEtotAmt = new double[100]; double[] FundTypeDtotAmt = new double[100]; // Newly added : FundType
        double[] BudEtotAmt = new double[100]; double[] BudDtotAmt = new double[100];
        double[] grandEtotAmt = new double[100]; double[] grandDtotAmt = new double[100];

        double STypetotgross = 0; double Budtotgross = 0; double grandtotgross = 0; double Fundtotgross = 0;
        double STypetotIT = 0; double BudtotIT = 0; double grandtotIT = 0; double FundtotIT = 0;
        double STypetotPTax = 0; double BudtotPTax = 0; double grandtotPTax = 0; double FundtotPTax = 0;
        double STypetotded = 0; double Budtotded = 0; double grandtotded = 0; double Fundtotded = 0;
        double STypetotnet = 0; double Budtotnet = 0; double grandtotnet = 0; double Fundtotnet = 0;

        double OthAll = 0; double OthDed = 0;
        double STypeOthAll = 0; double BudOthAll = 0; double grandtotOthAll = 0; double FundOthAll = 0;
        double STypeOthDed = 0; double BudOthDed = 0; double grandtotOthDed = 0; double FundOthDed = 0;

        double[] pageEtotAmt = new double[100]; double[] pageDtotAmt = new double[100];
        double[] pagetotloanAmt = new double[100]; double[] pagetotInsAmt = new double[100];

        double pagetotgross = 0; double pagetotded = 0; double pagetotIT = 0; double pagetotnet = 0;
        double pagetotedcharge = 0;

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
        boldFont.Boldweight = NPOI.HSSF.UserModel.HSSFFont.BOLDWEIGHT_BOLD;
        int row;
        string strFile = "Index Report.xls";
        string filename = Server.MapPath("~/XLSFiles/" + strFile);

        string path = Server.MapPath("~/Images/hpu-logo.jpg");

        //   int totrowinpage = Convert.ToInt16(Session["Rows"].ToString().Trim());

        string compname = ds.Tables[1].Rows[0]["compname"].ToString().Trim();
        // string monthyear = "SALARY BILL AND ACQUITTANCE ROLL FOR THE MONTH OF " + ds.Tables[1].Rows[0]["monthyear"].ToString().Trim();
        string subheading1 = ds.Tables[1].Rows[0]["SubHeading1"].ToString().Trim();
        string subheading2 = ds.Tables[1].Rows[0]["SubHeading2"].ToString().Trim();
        string subheading3 = ds.Tables[1].Rows[0]["SubHeading3"].ToString().Trim();

        #region Create Company/Heading/SubHeading Header
        row = 0;
        headerCellStyle_font_Bold.SetFont(boldFont);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(compname);
        //  Add Style to Cell
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.VerticalAlignment = 1;
        cra = new CellRangeAddress(row, row, 0, tdcount + 3);
        xlsWorksheet.AddMergedRegion(cra);

        row += 1;
        headerRow = xlsWorksheet.CreateRow(row);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(subheading1);
        // Add Style to Cell
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.VerticalAlignment = 1;
        cra = new CellRangeAddress(row, row, 0, tdcount + 3);
        xlsWorksheet.AddMergedRegion(cra);

        row += 1;
        headerRow = xlsWorksheet.CreateRow(row);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(subheading2);
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
        headerCell.SetCellValue("Enrollment No.");
        xlsWorksheet.SetColumnWidth(1, "Enrollment No.".ToString().Length * 300);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        //---------------Cell 1----------------------------------
        headerCell = headerRow.CreateCell(2);
        headerCell.SetCellValue("Alumni Name");
        xlsWorksheet.SetColumnWidth(1, "Alumni Name".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(3);
        headerCell.SetCellValue("Father Name");
        xlsWorksheet.SetColumnWidth(1, "Father Name".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(4);
        headerCell.SetCellValue("D.O.B");
        xlsWorksheet.SetColumnWidth(1, "D.O.B".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(5);
        headerCell.SetCellValue("Alumni Nature");
        xlsWorksheet.SetColumnWidth(1, "Alumni Nature".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(6);
        headerCell.SetCellValue("Department of the University/College/Inst");
        xlsWorksheet.SetColumnWidth(1, "Department of the University/College/Inst".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(7);
        headerCell.SetCellValue("Highest Degree");
        xlsWorksheet.SetColumnWidth(1, "Highest Degree".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(8);
        headerCell.SetCellValue("Year of Passing");
        xlsWorksheet.SetColumnWidth(1, "Year of Passing".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(9);
        headerCell.SetCellValue("Email ID");
        xlsWorksheet.SetColumnWidth(1, "Email ID".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(10);
        headerCell.SetCellValue("Mobile No.");
        xlsWorksheet.SetColumnWidth(1, "Mobile No.".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(11);
        headerCell.SetCellValue("Presently Working In (Organization/Institution)");
        xlsWorksheet.SetColumnWidth(1, "Presently Working In (Organization/Institution)".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(12);
        headerCell.SetCellValue("Designation");
        xlsWorksheet.SetColumnWidth(1, "Designation".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(13);
        headerCell.SetCellValue("Permanent Address");
        xlsWorksheet.SetColumnWidth(1, "Permanent Address".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(14);
        headerCell.SetCellValue("Present Address");
        xlsWorksheet.SetColumnWidth(1, "Present Address".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(15);
        headerCell.SetCellValue("Expertise");
        xlsWorksheet.SetColumnWidth(1, "Expertise".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(16);
        headerCell.SetCellValue("Outstanding Achievements/ Honor/ Award/Position");
        xlsWorksheet.SetColumnWidth(1, "Outstanding Achievements/ Honor/ Award/Position".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(17);
        headerCell.SetCellValue("Gender");
        xlsWorksheet.SetColumnWidth(1, "Gender".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(18);
        headerCell.SetCellValue("Remarks");
        xlsWorksheet.SetColumnWidth(1, "Remarks".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(19);
        headerCell.SetCellValue("Is Mentor");
        xlsWorksheet.SetColumnWidth(1, "Is Mentor".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        for (m = 0; m < ds.Tables[0].Rows.Count; m++)
        {
            DataRow[] drBud = ds.Tables[0].Select("pk_alumniid= '" + ds.Tables[0].Rows[m]["pk_alumniid"].ToString().Trim() + "'");

            if (drBud.Length > 0)
            {
                for (i = 0; i < drBud.Length; i++)
                {
                    //cellno = 5;
                    srno = srno + 1;

                    row += 1;
                    headerRow = xlsWorksheet.CreateRow(row);//make a new row 
                    headerCell = headerRow.CreateCell(0);
                    // Set Cell Value
                    headerCell.SetCellValue(srno);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(1);
                    headerCell.SetCellValue(drBud[i]["alumnino"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["alumnino"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(2);
                    headerCell.SetCellValue(drBud[i]["alumni_name"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["alumni_name"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(3);
                    headerCell.SetCellValue(drBud[i]["fathername"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["fathername"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(4);
                    headerCell.SetCellValue(drBud[i]["dob"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["dob"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(5);
                    headerCell.SetCellValue(drBud[i]["alumnitype"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["alumnitype"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(6);
                    headerCell.SetCellValue(drBud[i]["fk_collegeid"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["fk_collegeid"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(7);
                    headerCell.SetCellValue(drBud[i]["fk_degreeid"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["fk_degreeid"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(8);
                    headerCell.SetCellValue(drBud[i]["yearofpassing"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["yearofpassing"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(9);
                    headerCell.SetCellValue(drBud[i]["email"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["email"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(10);
                    headerCell.SetCellValue(drBud[i]["contactno"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["contactno"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(11);
                    headerCell.SetCellValue(drBud[i]["currentoccupation"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["currentoccupation"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(12);
                    headerCell.SetCellValue(drBud[i]["designation"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["designation"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(13);
                    headerCell.SetCellValue(drBud[i]["per_address"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["per_address"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(14);
                    headerCell.SetCellValue(drBud[i]["currentaddress"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["currentaddress"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;


                    headerCell = headerRow.CreateCell(15);
                    headerCell.SetCellValue(drBud[i]["special_interest"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["special_interest"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(16);
                    headerCell.SetCellValue(drBud[i]["Achievement"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["Achievement"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(17);
                    headerCell.SetCellValue(drBud[i]["gender"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["gender"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(18);
                    headerCell.SetCellValue(drBud[i]["remarks"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["remarks"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;

                    headerCell = headerRow.CreateCell(19);
                    headerCell.SetCellValue(drBud[i]["isMentor"].ToString().Trim());
                    xlsWorksheet.SetColumnWidth(1, drBud[i]["isMentor"].ToString().Length * 700);
                    headerCell.CellStyle = headerCellStyle;
                    headerCell.CellStyle.Alignment = 1;
                }
            }
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