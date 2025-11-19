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

public partial class Alumni_Alm_approvePublish_Jobs : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public string xmldoc { get; private set; }
    public object Pk_JobPostedId { get; private set; }
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    crypto crp = new crypto();

    /// <summary>
    /// Clear ArrayList of Sp
    /// </summary>
    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    /// <summary>
    /// Pop-up Message
    /// </summary>
    /// <param name="msg"></param>
    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    /// <summary>
    /// Sp of Rejected Request
    /// </summary>
    /// <returns></returns>
    private DataSet Job_List_for_Approval()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("Alm_Get_publishJobs_ByAdmin", values, names, types);
    }

    public DataTable ins_upd_delevents_dtls()
    {
        try
        {
            names.Clear(); values.Clear(); types.Clear();
            names.Add("@doc"); types.Add(SqlDbType.VarChar); values.Add(xmldoc);
            names.Add("@Pk_JobPostedId"); values.Add(Pk_JobPostedId); types.Add(SqlDbType.Int);
            return Dobj.GetDataTable("ALM_UpdJobs_Status", values, names, types);
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

    /// <summary>
    /// Bind Pending List Grid
    /// </summary>
    void FillGrid()
    {
        try
        {
            DataSet ds;
            ds = Job_List_for_Approval(); //CRUD_BASECLASSPLC.GetSTable<PLC_Child_class.PLC_Child_Company>(obj);           
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

    /// <summary>
    /// Query Srting On view
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkView1_Click(object sender, EventArgs e)
    {
        string a = crp.Encrypt(((LinkButton)sender).CommandArgument.ToString());
        a = a.Replace("+", "%2B");
        Response.Redirect("~//Alumni//Alm_ViewPublishJob_Form.aspx?id=" + a + "");
    }

    protected void lnkView2_Click(object sender, EventArgs e)
    {
        string a = crp.Encrypt(((LinkButton)sender).CommandArgument.ToString());
        a = a.Replace("+", "%2B");
        Response.Redirect("~//Alumni//Alm_ViewPublishJob_Form.aspx?id=" + a + "");
    }

    protected void lnkViewApproved_Click(object sender, EventArgs e)
    {
        string a = crp.Encrypt(((LinkButton)sender).CommandArgument.ToString());
        a = a.Replace("+", "%2B");
        Response.Redirect("~//Alumni//Alm_ViewPublishJob_Form.aspx?id=" + a + "");
    }

    /// <summary>
    /// Gvdetails on row Command
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                dt = ins_upd_delevents_dtls();
                {
                    FillGrid();
                    ClientMessaging(" Approved/Rejected Successfully!Please Provide Loginid and Password to Recruiter");
                }
                if (dt.Rows.Count >= 0)
                {
                    if (Convert.ToBoolean(dsMain.Tables[0].Rows[0]["IsApprovedByPCell"]) == true)
                    {
                        ClientMessaging("Job Approved Successfully");
                    }
                    else
                    {
                        ClientMessaging("Job Rejected Successfully");
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

    /// <summary>
    /// Insert Detail On processs Button
    /// </summary>
    /// <param name="argCompanyReqId"></param>
    /// <returns></returns>
    protected DataSet GetAppmain(string argCompanyReqId)
    {
        DataSet dsmain = Dobj.GetSchema("ALM_Alumni_Posted_job");
        dsmain.Tables[0].TableName = "ALM_Alumni_Posted_job";
        //int i = 0;
        foreach (GridViewRow gr in gvDetails.Rows)
        {
            Anthem.Label txtCompanyReqId = (Anthem.Label)gr.FindControl("txtCompanyReqId");

            if (argCompanyReqId == txtCompanyReqId.Text)
            {
                DataRow dr_main = dsmain.Tables[0].NewRow();
                dr_main["Pk_JobPostedId"] = Convert.ToInt32(gvDetails.DataKeys[gr.RowIndex].Value);
                Pk_JobPostedId = dr_main["Pk_JobPostedId"];
                dr_main["Pk_JobPostedId"] = Convert.ToInt32(gvDetails.DataKeys[gr.RowIndex].Value);
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

    /// <summary>
    /// Page indexing Of all Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        int i; int pagecounter = 0; int counter = 0; int srno = 0;
        int tdcount; int etdcount = 0; int dtdcount = 0; int pagesize = 0;
        int lineno = 0;

        counter = 0; etdcount = 0; tdcount = 5;

        HSSFWorkbook xlsWorkbook = new HSSFWorkbook(); //Make a new npoi workbook
        MemoryStream memoryStream = new MemoryStream();

        HSSFSheet xlsWorksheet = xlsWorkbook.CreateSheet("Approved Jobs"); //make a new sheet 
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
        string strFile = "List of Approved Jobs.xls";
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

        headerCell = headerRow.CreateCell(5);
        headerCell.SetCellValue("Published By");
        xlsWorksheet.SetColumnWidth(1, "Published By".ToString().Length * 800);
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
            headerCell.SetCellValue(ds.Tables[1].Rows[i]["JobOpenFrom"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[1].Rows[i]["JobOpenFrom"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(4);
            headerCell.SetCellValue(ds.Tables[1].Rows[i]["JobOpenTo"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[1].Rows[i]["JobOpenTo"].ToString().Length * 700);
            headerCell.CellStyle = headerCellStyle;
            headerCell.CellStyle.Alignment = 1;

            headerCell = headerRow.CreateCell(5);
            headerCell.SetCellValue(ds.Tables[1].Rows[i]["alumni_name"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[1].Rows[i]["alumni_name"].ToString().Length * 700);
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

        headerCell = headerRow.CreateCell(5);
        headerCell.SetCellValue("Published By");
        xlsWorksheet.SetColumnWidth(1, "Published By".ToString().Length * 800);
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

            headerCell = headerRow.CreateCell(5);
            headerCell.SetCellValue(ds.Tables[2].Rows[i]["alumni_name"].ToString().Trim());
            xlsWorksheet.SetColumnWidth(1, ds.Tables[2].Rows[i]["alumni_name"].ToString().Length * 700);
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
            //int pk_breedid = 0, pk_stageid = 0, pk_speciesid = 0;
            //if (ddlBreed.SelectedIndex != 0)
            //{
            //    pk_breedid = Convert.ToInt32(ddlBreed.SelectedValue);
            //}
            //else
            //{
            //    pk_breedid = 0;
            //}
            //if (ddlSpeciesName.SelectedIndex != 0)
            //{
            //    pk_speciesid = Convert.ToInt32(ddlSpeciesName.SelectedValue);
            //}
            //else
            //{
            //    pk_speciesid = 0;
            //}
            //if (ddlStage.SelectedIndex != 0)
            //{
            //    pk_stageid = Convert.ToInt32(ddlStage.SelectedValue);
            //}
            //else
            //{
            //    pk_stageid = 0;
            //}

            try
            {
                DataSet dsAppJobs = ALM_Get_Approved_PublishJobs_ByAdmin();

                if (dsAppJobs.Tables[1].Rows.Count > 0)
                {
                    dsAppJobs.Tables[0].TableName = "CompanyDetails";
                    dsAppJobs.Tables[1].TableName = "ApprovedPublishedJobsList";
                    filename = Server.MapPath("~/Alumni/ALM_Reports/ALM_Approved_Published_Jobs_Rpt.rpt");
                    dsAppJobs.WriteXml(Server.MapPath("~/XMLReports/ALM_Approved_Published_Jobs_Rpt.xml"));

                    objRptDoc.Load(filename);
                    objRptDoc.SetDataSource(dsAppJobs);

                    objRptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Approved Publish Jobs Reports");

                    //if (rbttype.SelectedValue == "PD")
                    //{
                    //    objRptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Approved Publish Jobs Reports");
                    //}
                    //else
                    //{
                    //    objRptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Response, true, "Approved Publish Jobs Reports");
                    //}
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