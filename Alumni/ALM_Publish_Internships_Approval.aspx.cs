using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using System.IO;
using DataAccessLayer;
using CrystalDecisions.CrystalReports.Engine;
using NPOI.HSSF.Util;
using NPOI.HSSF.UserModel;

public partial class Alumni_ALM_Publish_Internships_Approval : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public string xmldoc { get; private set; }
    public object pk_InternshipId { get; private set; }
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    crypto crp = new crypto();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    private DataSet Job_List_for_Approval()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("ALM_Get_Published_Internships_ByAdmin", values, names, types);
    }

    public DataTable Ins_Upd_Del_Events_Dtls()
    {
        try
        {
            names.Clear(); values.Clear(); types.Clear();
            names.Add("@doc"); types.Add(SqlDbType.VarChar); values.Add(xmldoc);
            names.Add("@pk_InternshipId"); values.Add(pk_InternshipId); types.Add(SqlDbType.Int);
            return Dobj.GetDataTable("ALM_Upd_Internships_Status", values, names, types);
        }
        catch { throw; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGrid();
        }
    }

    void FillGrid()
    {
        try
        {
            DataSet ds;
            ds = Job_List_for_Approval();
            gvDetails.DataSource = ds.Tables[0];
            gvDetails.DataBind();
            gvDetails.SelectedIndex = -1;
            gvApprovedJobs.DataSource = ds.Tables[1];
            gvApprovedJobs.DataBind();
            gvApprovedJobs.SelectedIndex = -1;
            gvRejectedJobs.DataSource = ds.Tables[2];
            gvRejectedJobs.DataBind();
            gvRejectedJobs.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    protected void lnkView1_Click(object sender, EventArgs e)
    {
        string a = crp.Encrypt(((LinkButton)sender).CommandArgument.ToString());
        a = a.Replace("+", "%2B");
        Response.Redirect("~//Alumni//ALM_ShowPublished_Internship_Details.aspx?id=" + a + "");
    }

    protected void lnkView2_Click(object sender, EventArgs e)
    {
        string a = crp.Encrypt(((LinkButton)sender).CommandArgument.ToString());
        a = a.Replace("+", "%2B");
        Response.Redirect("~//Alumni//ALM_ShowPublished_Internship_Details.aspx?id=" + a + "");
    }

    protected void lnkViewApproved_Click(object sender, EventArgs e)
    {
        string a = crp.Encrypt(((LinkButton)sender).CommandArgument.ToString());
        a = a.Replace("+", "%2B");
        Response.Redirect("~//Alumni//ALM_ShowPublished_Internship_Details.aspx?id=" + a + "");
    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper().ToString() == "SELECT")
        {

        }
        else if (e.CommandName.ToUpper().ToString() == "SUBMIT")
        {
            foreach (GridViewRow gr in gvDetails.Rows)
            {
                Anthem.RadioButtonList Rd_Approve = (Anthem.RadioButtonList)gr.FindControl("Rd_Approve");
                Anthem.Label txtCompanyReqId = (Anthem.Label)gr.FindControl("txtCompanyReqId");
                if (e.CommandArgument.ToString() == txtCompanyReqId.Text)

                    if (Rd_Approve.SelectedValue == "")
                    {
                        ClientMessaging("Select Approve/Reject from List !");
                        return;
                    }
            }
            try
            {
                string Message = "";
                DataSet dsMain = new DataSet();
                dsMain = GetAppmain(e.CommandArgument.ToString());
                string doc = "";

                doc = dsMain.GetXml();
                xmldoc = doc;
                DataTable dt;
                dt = Ins_Upd_Del_Events_Dtls();
                {
                    FillGrid();
                    ClientMessaging("Interships Approved/Rejected Successfully!");
                }
                if (dt.Rows.Count >= 0)
                {
                    if (Convert.ToBoolean(dsMain.Tables[0].Rows[0]["IsApprovedByPCell"]) == true)
                    {
                        ClientMessaging("Interships Approved Successfully");
                    }
                    else
                    {
                        ClientMessaging("Interships Rejected Successfully");
                    }
                    FillGrid();
                }

            }
            catch (Exception ex)
            {
                // lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
            }
        }
    }

    protected DataSet GetAppmain(string argCompanyReqId)
    {
        DataSet dsmain = Dobj.GetSchema("ALM_Alumni_Posted_Internships");
        dsmain.Tables[0].TableName = "ALM_Alumni_Posted_Internships";
        //int i = 0;
        foreach (GridViewRow gr in gvDetails.Rows)
        {
            Anthem.Label txtCompanyReqId = (Anthem.Label)gr.FindControl("txtCompanyReqId");

            if (argCompanyReqId == txtCompanyReqId.Text)
            {
                DataRow dr_main = dsmain.Tables[0].NewRow();
                dr_main["pk_InternshipId"] = Convert.ToInt32(gvDetails.DataKeys[gr.RowIndex].Value);
                pk_InternshipId = dr_main["pk_InternshipId"];
                dr_main["pk_InternshipId"] = Convert.ToInt32(gvDetails.DataKeys[gr.RowIndex].Value);
                Anthem.RadioButtonList Rd_Approve = (Anthem.RadioButtonList)gr.FindControl("Rd_Approve");
                if (Rd_Approve.SelectedValue == "" || Rd_Approve.SelectedValue == "False")
                {

                    if (Rd_Approve.SelectedValue == "")
                        dr_main["IsApprovedByPCell"] = DBNull.Value;
                    else
                        dr_main["IsApprovedByPCell"] = Convert.ToBoolean(Rd_Approve.SelectedValue);
                }
                else if (Rd_Approve.SelectedValue != "" && Rd_Approve.SelectedValue == "True")
                {
                    dr_main["IsApprovedByPCell"] = Convert.ToBoolean(Rd_Approve.SelectedValue);

                }
                dsmain.Tables[0].Rows.Add(dr_main);
            }
        }
        return dsmain;
    }

    protected void gvApprovedJobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void gvRejectedJobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void btnAppExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = Job_List_for_Approval();

            if (ds.Tables[1].Rows.Count > 0)
            {
                ExportAppGridToExcel(ds);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    void ExportAppGridToExcel(DataSet ds)
    {
        int i; int srno = 0;
        int tdcount;

        tdcount = 5;

        HSSFWorkbook xlsWorkbook = new HSSFWorkbook(); //Make a new npoi workbook
        MemoryStream memoryStream = new MemoryStream();

        HSSFSheet xlsWorksheet = xlsWorkbook.CreateSheet("Approved Internships"); //make a new sheet 
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
        string strFile = "List of Approved Internships.xls";
        string filename = Server.MapPath("~/XLSFiles/" + strFile);
        string path = Server.MapPath("~/Images/hpu-logo.jpg");

        string compname = ds.Tables[3].Rows[0]["compname"].ToString().Trim();
        string subheading1 = ds.Tables[3].Rows[0]["SubHeading1"].ToString().Trim();
        string subheading3 = ds.Tables[3].Rows[0]["SubHeading3"].ToString().Trim();

        #region Create Company/Heading/SubHeading Header

        row = 0;
        headerCellStyle_font_Bold.SetFont(boldFont);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(compname);
        //Add Style to Cell
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.VerticalAlignment = 1;
        cra = new CellRangeAddress(row, row, 0, tdcount + 6);
        xlsWorksheet.AddMergedRegion(cra);

        row += 1;
        headerRow = xlsWorksheet.CreateRow(row);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(subheading1);
        //Add Style to Cell
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.VerticalAlignment = 1;
        cra = new CellRangeAddress(row, row, 0, tdcount + 6);
        xlsWorksheet.AddMergedRegion(cra);

        row += 1;
        headerRow = xlsWorksheet.CreateRow(row);
        headerCell = headerRow.CreateCell(0);
        headerCell.SetCellValue(subheading3);
        // Add Style to Cell
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.VerticalAlignment = 1;
        cra = new CellRangeAddress(row, row, 0, tdcount + 6);
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
        headerCell.SetCellValue("Company Name");
        xlsWorksheet.SetColumnWidth(1, "Company Name".ToString().Length * 300);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        //---------------Cell 1----------------------------------
        headerCell = headerRow.CreateCell(2);
        headerCell.SetCellValue("Designation");
        xlsWorksheet.SetColumnWidth(1, "Designation".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(3);
        headerCell.SetCellValue("Job Category");
        xlsWorksheet.SetColumnWidth(1, "Job Category".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(4);
        headerCell.SetCellValue("Internship Details");
        xlsWorksheet.SetColumnWidth(1, "Internship Details".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(5);
        headerCell.SetCellValue("Skill Required");
        xlsWorksheet.SetColumnWidth(1, "Skill Required".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(6);
        headerCell.SetCellValue("Selection Procedure");
        xlsWorksheet.SetColumnWidth(1, "Selection Procedure".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(7);
        headerCell.SetCellValue("Internship URL");
        xlsWorksheet.SetColumnWidth(1, "Internship URL".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(8);
        headerCell.SetCellValue("Start Date");
        xlsWorksheet.SetColumnWidth(1, "Start Date".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(9);
        headerCell.SetCellValue("End Date");
        xlsWorksheet.SetColumnWidth(1, "End Date".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(10);
        headerCell.SetCellValue("Stipend");
        xlsWorksheet.SetColumnWidth(1, "Stipend".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(11);
        headerCell.SetCellValue("Duration");
        xlsWorksheet.SetColumnWidth(1, "Duration".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        for (i = 0; i < ds.Tables[1].Rows.Count; i++)
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
            headerCell.SetCellValue(ds.Tables[1].Rows[i]["CompanyName"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[1].Rows[i]["CompanyName"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(2);
            headerCell.SetCellValue(ds.Tables[1].Rows[i]["Designation"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[1].Rows[i]["Designation"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(3);
            headerCell.SetCellValue(ds.Tables[1].Rows[i]["Jobcategory"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[1].Rows[i]["Jobcategory"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(4);
            headerCell.SetCellValue(ds.Tables[1].Rows[i]["VacancyDtl"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[1].Rows[i]["VacancyDtl"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(5);
            headerCell.SetCellValue(ds.Tables[1].Rows[i]["SkillsReq"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[1].Rows[i]["SkillsReq"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(6);
            headerCell.SetCellValue(ds.Tables[1].Rows[i]["SelectionProcess"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[1].Rows[i]["SelectionProcess"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(7);
            headerCell.SetCellValue(ds.Tables[1].Rows[i]["JobVacancyUrl"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[1].Rows[i]["JobVacancyUrl"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(8);
            headerCell.SetCellValue(ds.Tables[1].Rows[i]["JobOpenFrom"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[1].Rows[i]["JobOpenFrom"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(9);
            headerCell.SetCellValue(ds.Tables[1].Rows[i]["JobOpenTo"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[1].Rows[i]["JobOpenTo"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(10);
            headerCell.SetCellValue(ds.Tables[1].Rows[i]["Stipend"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[1].Rows[i]["Stipend"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(11);
            headerCell.SetCellValue(ds.Tables[1].Rows[i]["Duration"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[1].Rows[i]["Duration"].ToString().Length * 700);
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

    protected void btnRejExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = Job_List_for_Approval();

            if (ds.Tables[2].Rows.Count > 0)
            {
                ExportRejGridToExcel(ds);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    void ExportRejGridToExcel(DataSet ds)
    {
        int i; int pagecounter = 0; int counter = 0; int srno = 0;
        int tdcount; int etdcount = 0; int dtdcount = 0; int pagesize = 0;
        int lineno = 0;

        counter = 0; etdcount = 0; tdcount = 5;

        HSSFWorkbook xlsWorkbook = new HSSFWorkbook(); //Make a new npoi workbook
        MemoryStream memoryStream = new MemoryStream();

        HSSFSheet xlsWorksheet = xlsWorkbook.CreateSheet("Rejected Jobs"); //make a new sheet 
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
        string strFile = "List of Rejected Jobs.xls";
        string filename = Server.MapPath("~/XLSFiles/" + strFile);
        string path = Server.MapPath("~/Images/hpu-logo.jpg");

        string compname = ds.Tables[3].Rows[0]["compname"].ToString().Trim();
        string subheading1 = ds.Tables[3].Rows[0]["SubHeading1"].ToString().Trim();
        string subheading3 = ds.Tables[3].Rows[0]["SubHeading3"].ToString().Trim();

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
        headerCell.SetCellValue("Company Name");
        xlsWorksheet.SetColumnWidth(1, "Company Name".ToString().Length * 300);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        //---------------Cell 1----------------------------------
        headerCell = headerRow.CreateCell(2);
        headerCell.SetCellValue("Designation");
        xlsWorksheet.SetColumnWidth(1, "Designation".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(3);
        headerCell.SetCellValue("Job Apply From");
        xlsWorksheet.SetColumnWidth(1, "Job Apply From".ToString().Length * 600);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        headerCell = headerRow.CreateCell(4);
        headerCell.SetCellValue("Last Date for Applying for a Job");
        xlsWorksheet.SetColumnWidth(1, "Last Date for Applying for a Job".ToString().Length * 800);
        headerCell.CellStyle = headerCellStyle_font_Bold;
        headerCell.CellStyle.Alignment = 1;

        for (i = 0; i < ds.Tables[2].Rows.Count; i++)
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
            headerCell.SetCellValue(ds.Tables[2].Rows[i]["CompanyName"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[2].Rows[i]["CompanyName"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(2);
            headerCell.SetCellValue(ds.Tables[2].Rows[i]["Designation"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[2].Rows[i]["Designation"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(3);
            headerCell.SetCellValue(ds.Tables[2].Rows[i]["JobOpenFrom"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[2].Rows[i]["JobOpenFrom"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(4);
            headerCell.SetCellValue(ds.Tables[2].Rows[i]["JobOpenTo"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[2].Rows[i]["JobOpenTo"].ToString().Length * 700);
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

    protected void btnPrintPDF_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewReport();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void ViewReport()
    {
        try
        {
            lblMsg.Text = "";
            ReportDocument objRptDoc = new ReportDocument();
            //string xmldoc = Getdoc().GetXml();
            string filename = "";

            try
            {
                DataSet dsAppJobs = ALM_Get_Approved_PublishJobs_ByAdmin();

                if (dsAppJobs.Tables[1].Rows.Count > 0)
                {
                    dsAppJobs.Tables[0].TableName = "CompanyDetails";
                    dsAppJobs.Tables[1].TableName = "ApprovedPublishedInternshipsList";
                    filename = Server.MapPath("~/Alumni/ALM_Reports/ALM_Approved_Published_Internships_Rpt.rpt");
                    dsAppJobs.WriteXml(Server.MapPath("~/XMLReports/ALM_Approved_Published_Internships_Rpt.xml"));
                    objRptDoc.Load(filename);
                    objRptDoc.SetDataSource(dsAppJobs);
                    objRptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Approved Publish Internships Reports");
                }
                else
                {
                    lblMsg.Text = "No Records Founds!";
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

    private DataSet ALM_Get_Approved_PublishJobs_ByAdmin()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("ALM_Get_Approved_PublishJobs_ByAdmin", values, names, types);
    }
}