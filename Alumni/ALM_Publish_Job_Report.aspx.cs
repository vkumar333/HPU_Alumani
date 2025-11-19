using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessLayer;
using System.Collections;
using NPOI.HSSF.Util;
using System.Web;
using NPOI.HSSF.UserModel;
using System.IO;
using SubSonic;
using CrystalDecisions.CrystalReports.Engine;

public partial class Alumni_ALM_Publish_Job_Report : System.Web.UI.Page
{
    DataAccess DAobj = new DataAccess();
    ArrayList Result = new ArrayList();
    ArrayList size = new ArrayList();
    DataAccess Dobj = new DataAccess();
    crypto crp = new crypto();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

    #region Properties

    public string alias { get; set; }
    public int jobCategory { get; set; }
    public int ProgramID { get; set; }
    public int Pk_CollegeID { get; set; }
    public int semester { get; set; }
    public int SemesterId { get; set; }
    public int ExamYearId { get; set; }
    public int ExamTypeId { get; set; }
    public int CapacityID { get; set; }
    public int Semestertype { get; set; }
    public int SubjectId { get; set; }
    public int ISActive { get; set; }
    public string AlumniName { get; set; }
    public int CompanyName { get; set; }
    public string TxtCompname { get; set; }
    public string TxtDesgn { get; set; }
    public string txtvacancy { get; set; }
    public int pkpublisjJobid { get; private set; }

    #endregion

    #region Stored Procedure

    private DataSet SP_Get_Student_Data()
    {
        names.Add("@CompanyName"); values.Add(TxtCompname); types.Add(SqlDbType.NVarChar);
        names.Add("@alumni_name"); values.Add(AlumniName); types.Add(SqlDbType.NVarChar);
        names.Add("@JobCategory"); values.Add(jobCategory); types.Add(SqlDbType.Int);
        names.Add("@Designation"); values.Add(TxtDesgn); types.Add(SqlDbType.NVarChar);
        names.Add("@VacancyDtl"); values.Add(txtvacancy); types.Add(SqlDbType.NVarChar);
        return Dobj.GetDataSet("ALM_PublishJobVacancy_Rpt", values, names, types);
    }

    private DataSet SP_Get_Student_jobdetail()
    {
        return Dobj.GetDataSet("ALM_PublishJobVacancy_GetDetail", values, names, types);
    }

    private DataSet SP_Get_Student_Data_excel()
    {
        names.Add("@CompanyName"); values.Add(CompanyName); types.Add(SqlDbType.NVarChar);
        names.Add("@alumni_name"); values.Add(AlumniName); types.Add(SqlDbType.NVarChar);
        names.Add("@JobCategory"); values.Add(jobCategory); types.Add(SqlDbType.NVarChar);
        names.Add("@Designation"); values.Add(TxtDesgn); types.Add(SqlDbType.NVarChar);
        names.Add("@VacancyDtl"); values.Add(txtvacancy); types.Add(SqlDbType.NVarChar);
        return Dobj.GetDataSet("PLC_Consolidated_Data_lastSyear_new_excel", values, names, types);
    }

    public static StoredProcedure Bind_SP_Alumni()
    {
        SubSonic.StoredProcedure sp = new StoredProcedure("PLC_FillAlumniDetail", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    private DataSet FillJobDetails(int pkpublisjJobid)
    {
        clear();
        names.Add("@pkpublisjJobid"); values.Add(pkpublisjJobid); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("ALM_Getjobapply_Candidate", values, names, types);
    }

    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clear();
        }
    }

    protected void btnexcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            if (ViewState["Excel"] != null)
            {
                string xmlData = ViewState["Excel"].ToString();
                if (!string.IsNullOrEmpty(xmlData))
                {
                    ds.ReadXml(new StringReader(xmlData));
                }
                else
                {
                    // Handle empty XML data
                }
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                ExportGridToExcel(ds);
            }
        }
        catch (Exception ex)
        {
            lblMsg1.Text = ex.Message;
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtDesignation.Text == "" && TxtCompanyname.Text == "" && TxtVacancy.Text == "" && txtAlumni.Text == "")
            {
                getData_noparameter();
            }
            else
            {
                FillGrid();
                gv_Getdata.SelectedIndex = -1;
                Anthem.Manager.IncludePageScripts = true;
                btnexcel.Enabled = true;
                btn_print.Visible = true;
            }
        }
        catch (Exception Ex)
        {
            lblMsg1.Text = Ex.ToString();
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void gv_Getdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Getdata.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void lnkViewApproved_Click(object sender, EventArgs e)
    {
        string a = crp.Encrypt(((LinkButton)sender).CommandArgument.ToString());
        a = a.Replace("+", "%2B");
        Response.Redirect("~//Alumni//ALM_PublishedViewJob_Details.aspx?id=" + a + "");
    }

    protected void btn_print_Click(object sender, EventArgs e)
    {
        ReportDocument rpt = new ReportDocument();
        try
        {
            string filename = "";

            DataSet ds = new DataSet();
            if (ViewState["Excel"] != null)
            {
                string xmlData = ViewState["Excel"].ToString();
                if (!string.IsNullOrEmpty(xmlData))
                {
                    ds.ReadXml(new StringReader(xmlData));
                }
                else
                {
                    // Handle empty XML data
                }
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].TableName = "PublishJobReport";
                ds.Tables[1].TableName = "CompanyDetails";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.WriteXml(Server.MapPath("~/XMLReports/PublishJobReport.xml"));
                    filename = Server.MapPath("~/Alumni/ALM_Reports/PublishJobReport_rpt.rpt");
                    rpt.Load(filename);
                    rpt.SetDataSource(ds);
                    rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PublishJobReport");
                }
            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            lblMsg1.Text = (CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message));
        }
        catch (Exception ex)
        {
            lblMsg1.Text = (CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message));
        }
        finally
        {
            // rpt.Close();
            //rpt.Dispose();
        }
    }

    protected void gv_Getdata_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper().ToString() == "PRINTREC")
        {
            ReportDocument rpt = new ReportDocument();
            DataSet dsptout;
            try
            {
                dsptout = FillJobDetails(Convert.ToInt32(Convert.ToString(e.CommandArgument)));
                dsptout.Tables[0].TableName = "CandidateDetail";
                dsptout.Tables[1].TableName = "ClientDetails";
                if (dsptout.Tables[0].Rows.Count > 0)
                {
                    dsptout.WriteXml(Server.MapPath("~/XMLReports/CandidateApply.xml"));
                    string filename = Server.MapPath("~/Alumni/ALM_Reports/ALM_CandidateApply.rpt");
                    rpt.Load(filename);
                    rpt.SetDataSource(dsptout);
                    rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "ExportedReport");
                }
                else
                {
                    //lblMsg.Text = "There is no record";
                }
            }
            catch (Exception ex)
            {
                // lblMsg.Text = ex.Message;
            }
            finally
            {
                rpt.Close();
                rpt.Dispose();
            }
        }
    }

    #endregion

    #region Methods

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void getData_noparameter()
    {
        DataSet ds = new DataSet();
        AlumniName = txtAlumni.Text.ToString();

        ds = SP_Get_Student_jobdetail();

        if (ds.Tables[0].Rows.Count > 0)
        {
            gv_Getdata.DataSource = ds.Tables[0];
            gv_Getdata.DataBind();
            gv_Getdata.PageIndex = 0;
            gv_Getdata.Visible = true;
            lblMsg1.Text = "Total" + " " + ds.Tables[0].Rows.Count.ToString() + " " + "Records Found. ".ToString();
            tbl_showdetail.Visible = true;
            btn_print.Visible = true;
            btnexcel.Visible = true;
        }
        else
        {
            ClientMessaging("No Record Found..!");
            lblMsg1.Text = ds.Tables[0].Rows.Count.ToString() + " " + "Records Found. ".ToString();
            gv_Getdata.DataSource = null;
            gv_Getdata.DataBind();
            tbl_showdetail.Visible = false;
            btn_print.Visible = false;
            btnexcel.Visible = false;
        }
    }

    protected void getData()
    {
        AlumniName = txtAlumni.Text.ToString();

        if (D_ddlJobCat.SelectedIndex == 0)
        {
            jobCategory = 0;
        }
        else
        {
            jobCategory = Convert.ToInt32(D_ddlJobCat.SelectedValue);
        }
        
        TxtCompname = TxtCompanyname.Text.ToString();
        TxtDesgn = TxtDesignation.Text.ToString();
        txtvacancy = TxtVacancy.Text.ToString();

        DataSet ds = new DataSet();
        ds = SP_Get_Student_Data();
        StringWriter sw = new StringWriter();
        ds.WriteXml(sw);
        string xmlData = sw.ToString();
        ViewState["Excel"] = xmlData;

        if (ds.Tables[0].Rows.Count > 0)
        {
            gv_Getdata.DataSource = ds.Tables[0];
            gv_Getdata.DataBind();
            lblMsg1.Text = "Total" + " " + ds.Tables[0].Rows.Count.ToString() + " " + "Records Found. ".ToString();
            tbl_showdetail.Visible = true;
        }
        else
        {
            ClientMessaging("No Record Found..!");
            lblMsg1.Text = "";
            gv_Getdata.DataSource = null; ;
            gv_Getdata.DataBind();
            tbl_showdetail.Visible = false;
        }
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
        boldFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        int row;
        string strFile = "ALM Publish Job Report.xls";
        string filename = Server.MapPath("~/XLSFiles/" + strFile);

        string path = Server.MapPath("~/Images/hpu-logo.jpg");

        // int totrowinpage = Convert.ToInt16(Session["Rows"].ToString().Trim());

        string compname = ds.Tables[1].Rows[0]["compname"].ToString().Trim();
        //string monthyear = "SALARY BILL AND ACQUITTANCE ROLL FOR THE MONTH OF " + ds.Tables[0].Rows[0]["monthyear"].ToString().Trim();
        string subheading1 = ds.Tables[1].Rows[0]["SubHeading1"].ToString().Trim();
        //string subheading2 = ds.Tables[1].Rows[0]["SubHeading2"].ToString().Trim();
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
        xlsWorksheet.SetColumnWidth(1, "Alumni Name".ToString().Length * 300);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        //---------------Cell 1----------------------------------
        headerCell = headerRow.CreateCell(2);
        headerCell.SetCellValue("Company Name");
        xlsWorksheet.SetColumnWidth(1, "Company Name".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(3);
        headerCell.SetCellValue("Designation");
        xlsWorksheet.SetColumnWidth(1, "Designation".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(4);
        headerCell.SetCellValue("Job Category");
        xlsWorksheet.SetColumnWidth(1, "Job Category".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(5);
        headerCell.SetCellValue("Vacancy");
        xlsWorksheet.SetColumnWidth(1, "Vacancy".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
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
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["Alumniname"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["Alumniname"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(2);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["CompanyName"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["CompanyName"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(3);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["Designation"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["Designation"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(4);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["JobCategory"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["JobCategory"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(5);
            headerCell.SetCellValue(ds.Tables[0].Rows[i]["Vacancy"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[0].Rows[i]["Vacancy"].ToString().Length * 700);
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

    protected void FillGrid()
    {
        getData();
    }

    public void clear()
    {
        txtAlumni.Text = "";
        TxtCompanyname.Text = "";
        TxtDesignation.Text = "";
        D_ddlJobCat.ClearSelection();
        TxtVacancy.Text = "";
        lblMsg1.Text = "";
        btn_print.Visible = false;
        btnexcel.Visible = false;
        tbl_showdetail.Visible = false;
        gv_Getdata.DataSource = null;
        gv_Getdata.DataBind();
        gv_Getdata.Visible = false;
        tbl_showdetail.Visible = false;
        BindJobCategory();
        Anthem.Manager.IncludePageScripts = true;
    }

    private void BindJobCategory()
    {
        D_ddlJobCat.Items.Clear();
        DataSet ds = IUMSNXG.SP.ALM_SP_JobCategory_SelForDDL().GetDataSet();
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                D_ddlJobCat.DataSource = ds.Tables[0];
                D_ddlJobCat.DataValueField = "Pk_JobCId";
                D_ddlJobCat.DataTextField = "Name";
                D_ddlJobCat.DataBind();
            }
        }
        D_ddlJobCat.Items.Insert(0, "--Select Job Category--");
    }

    //private void BindAlumni()
    //{
    //    txtAlumni.Items.Clear();
    //    DataSet ds = Bind_SP_Alumni().GetDataSet();
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            txtAlumni.DataSource = ds.Tables[0];
    //            txtAlumni.DataValueField = "pk_alumniid";
    //            txtAlumni.DataTextField = "alumni_name";
    //            txtAlumni.DataBind();
    //        }
    //    }
    //    txtAlumni.Items.Insert(0, "--Select Alumni Name--");
    //}

    #endregion
}