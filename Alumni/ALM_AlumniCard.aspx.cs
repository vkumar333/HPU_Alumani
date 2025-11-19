using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;
using ZXing.Common;
using SubSonic;
using DataAccessLayer;
using System.Net;
using System.Text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using CrystalDecisions.CrystalReports.Engine;

public partial class Alumni_ALM_AlumniCard : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    ArrayList names = new ArrayList();
    ArrayList types = new ArrayList();
    ArrayList values = new ArrayList();
    DataAccess DAobj = new DataAccess();
    crypto cpt = new crypto();

    private void ClearArrayList()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    public string Cardid { get; set; }
    public int AlumniId { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AlumniID"].ToString() != "" && Session["AlumniID"] != null)
        {
            if (!IsPostBack)
            {
                cardPreview();
            }
        }
        else
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
    }

    protected void cardPreview()
    {
        try
        {
            DataSet dsDA = ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(Session["AlumniID"])).GetDataSet();

            StringBuilder str = new StringBuilder();

            if (dsDA != null && dsDA.Tables[0].Rows.Count > 0)
            {
                bool isVisible = Convert.ToBoolean(dsDA.Tables[0].Rows[0]["ICardBtnPreviewIsVisible"]);
                bool isCardAppVisible = Convert.ToBoolean(dsDA.Tables[0].Rows[0]["ICardApprBtnIsVisible"]);

                if (isVisible)
                {
                    btnIssueCard.Enabled = false;
                    btnIssueCard.Visible = false;

                    btnDownloadCard.Enabled = isCardAppVisible;
                    btnDownloadCard.Visible = isCardAppVisible;

                    lblAleardyAppliedForCard.Visible = isCardAppVisible;
                    lblAleardyAppliedForCard.Text = isCardAppVisible == true ? "You have already applied for digital alumni card but not approved by admin. Kindly contact with  alumni administration." : "";

                    if (isCardAppVisible)
                    {
                        lblAleardyAppliedForCard.Visible = true;
                        lblAleardyAppliedForCard.Text = "Your digital alumni card has been approved by admin. Kindly download digital alumni card.";
                    }
                    else
                    {
                        lblAleardyAppliedForCard.Visible = true;
                        lblAleardyAppliedForCard.Text = "You have already applied for digital alumni card but not approved by admin. Kindly contact with alumni administration.";
                    }

                    lblAlumni.Text = dsDA.Tables[0].Rows[0]["alumni_name"].ToString();
                    ViewState["Alumni"] = lblAlumni.Text.ToString();
                    lblPassingYear.Text = dsDA.Tables[0].Rows[0]["yearofpassing"].ToString();
                    ViewState["yearofpassing"] = lblPassingYear.Text.ToString();
                    lblDegree.Text = dsDA.Tables[0].Rows[0]["degreename"].ToString();
                    ViewState["degreename"] = lblDegree.Text.ToString();
                    lblDept.Text = dsDA.Tables[0].Rows[0]["DeptName"].ToString();
                    ViewState["DeptName"] = lblDept.Text.ToString();
                    lblCardID.Text = string.IsNullOrEmpty(dsDA.Tables[0].Rows[0]["CardId"].ToString()) ? "" : dsDA.Tables[0].Rows[0]["CardId"].ToString();

                    alumnilogo.Src = "../alumni/alumin-default-theme/images/alumni-card-logo.jpg";

                    string fileName = "";

                    fileName = dsDA.Tables[0].Rows[0]["Files_Unique_Name"].ToString().Trim();

                    if (fileName != "")
                    {
                        string imgPath = ReturnPhysicalPath() + "\\Alumni\\StuImage\\" + fileName;
                        string base64Image = GetBase64Image(imgPath);
                        Imge1.Src = base64Image;
                    }
                    else
                    {
                        Imge1.Src = "~/alumni/stuimage/No_image.png";
                    }

                    string QRImg = dsDA.Tables[0].Rows[0]["QRImg"].ToString().Trim();

                    if (QRImg != "")
                    {
                        string imgQRPath = ReturnPhysicalPath() + "\\Alumni\\StuImage\\" + dsDA.Tables[0].Rows[0]["CardId"].ToString() + ".jpeg";
                        string base64imgQR = GetBase64Image(imgQRPath);
                        imgQR.Src = base64imgQR;
                    }
                    else
                    {
                        imgQR.Src = "~/alumni/stuimage/No_image.png";
                    }
                }
                else
                {
                    btnIssueCard.Enabled = true;
                    btnIssueCard.Visible = true;

                    btnDownloadCard.Enabled = isCardAppVisible;
                    btnDownloadCard.Visible = isCardAppVisible;

                    lblAleardyAppliedForCard.Visible = false;
                    lblAleardyAppliedForCard.Text = "";

                    lblAlumni.Text = dsDA.Tables[0].Rows[0]["alumni_name"].ToString();
                    ViewState["Alumni"] = lblAlumni.Text.ToString();
                    lblPassingYear.Text = dsDA.Tables[0].Rows[0]["yearofpassing"].ToString();
                    ViewState["yearofpassing"] = lblPassingYear.Text.ToString();
                    lblDegree.Text = dsDA.Tables[0].Rows[0]["degreename"].ToString();
                    ViewState["degreename"] = lblDegree.Text.ToString();
                    lblDept.Text = dsDA.Tables[0].Rows[0]["DeptName"].ToString();
                    ViewState["DeptName"] = lblDept.Text.ToString();
                    lblCardID.Text = string.IsNullOrEmpty(dsDA.Tables[0].Rows[0]["CardId"].ToString()) ? "" : dsDA.Tables[0].Rows[0]["CardId"].ToString();

                    alumnilogo.Src = "../alumni/alumin-default-theme/images/alumni-card-logo.jpg";

                    string fileName = "";

                    fileName = dsDA.Tables[0].Rows[0]["Files_Unique_Name"].ToString().Trim();

                    if (fileName != "")
                    {
                        string imgPath = ReturnPhysicalPath() + "\\Alumni\\StuImage\\" + fileName;
                        string base64Image = GetBase64Image(imgPath);
                        Imge1.Src = base64Image;
                    }
                    else
                    {
                        Imge1.Src = "~/alumni/stuimage/No_image.png";
                    }
                    GetAutoGeneratedCode();
                }
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }

    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        Session["dtArchive"] = null;
        Anthem.Manager.IncludePageScripts = true;
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", @"window.open(htmlContent, '_blank', 'toolbar=yes, scrollbars=yes, resizable=yes, top=500, left=500, width=400, height=400');", true);
    }

    public static StoredProcedure ALM_SP_AlumniRegistration_Edit(int? pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniCard_Details", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    private void GetAutoGeneratedCode()
    {
        string alumni = "";
        string passng = "";
        string degree = "";
        string dept = "";
        if (ViewState["Alumni"] != null)
        {
            alumni = ViewState["Alumni"].ToString();
        }
        if (ViewState["yearofpassing"] != null)
        {
            passng = ViewState["yearofpassing"].ToString();
        }
        if (ViewState["degreename"] != null)
        {
            degree = ViewState["degreename"].ToString();
        }
        if (ViewState["DeptName"] != null)
        {
            dept = ViewState["DeptName"].ToString();
        }

        DataSet dsGCode = GetAutoProjectCode().GetDataSet();

        if (dsGCode.Tables[0].Rows.Count > 0)
        {
            lblCardID.Text = dsGCode.Tables[0].Rows[0]["GeneratedCode"].ToString();

            if (Session["AlumniID"].ToString() != "" && Session["AlumniID"] != null)
            {
                string aid = cpt.Encrypt(Session["AlumniID"].ToString());
                string baseUrl = "https://alumni.hpushimla.in/alumni/ALM_ICard_Verification.aspx";
                string qrCodeText = baseUrl + "?id=" + aid;
                GenerateMyQRCode(qrCodeText, lblCardID.Text.ToString());
            }
        }
    }

    public static StoredProcedure GetAutoProjectCode()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_GetAuto_GeneratedCode", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    protected void GenerateQRCode(string data, string filePath)
    {
        BarcodeWriter barcodeWriter = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new EncodingOptions
            {
                Width = 300,
                Height = 300,
                Margin = 1
            }
        };

        string alumni = ""; string passng = ""; string degree = ""; string dept = "";

        if (ViewState["Alumni"] != null)
        {
            alumni = ViewState["Alumni"].ToString();
        }
        if (ViewState["yearofpassing"] != null)
        {
            passng = ViewState["yearofpassing"].ToString();
        }
        if (ViewState["degreename"] != null)
        {
            degree = ViewState["degreename"].ToString();
        }
        if (ViewState["DeptName"] != null)
        {
            dept = ViewState["DeptName"].ToString();
        }

        DataSet dsGCode = GetAutoProjectCode().GetDataSet();

        if (dsGCode != null && dsGCode.Tables[0].Rows.Count > 0)
        {
            lblCardID.Text = dsGCode.Tables[0].Rows[0]["GeneratedCode"].ToString();
            string qrCodeText = alumni + ", " + passng + ", " + degree + ", " + dept + ", " + lblCardID.Text + "";
        }
    }

    private void GenerateMyQCCode(string QCText, string Cardid)
    {
        string QR = Cardid;
        UploadFiles();
        string currDir = System.IO.Directory.GetCurrentDirectory();
        string fpath = this.upldPath;
        var QCwriter = new BarcodeWriter();
        QCwriter.Format = BarcodeFormat.QR_CODE;
        QCwriter.Options = new EncodingOptions
        {
            Width = 300,
            Height = 300,
            Margin = 1
        };
        var result = QCwriter.Write(QCText);
        string path = fpath + QR + ".jpeg";
        var barcodeBitmap = new Bitmap(result);

        using (MemoryStream memory = new MemoryStream())
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                bool IsExistPath = System.IO.Directory.Exists(fpath);
                if (!IsExistPath)
                    System.IO.Directory.CreateDirectory(fpath);

                barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                byte[] bytes = memory.ToArray();
                fs.Write(bytes, 0, bytes.Length);
            }
        }
        imgQR.Visible = true;
        string imgQRPath = ReturnPhysicalPath() + "\\Alumni\\StuImage\\" + QR + ".jpeg";
        string base64imgQR = GetBase64Image(imgQRPath);
        imgQR.Src = base64imgQR;

        ViewState["QRImgPath"] = imgQRPath;
    }

    #region "Global Variable"
    string upldPath = "";
    DataSet dsFile = null;

    #endregion

    public void UploadFiles()
    {
        try
        {
            string host = HttpContext.Current.Request.Url.Host;
            DataSet dsFilepath = new DataSet();
            dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
            foreach (DataRow dr in dsFilepath.Tables[0].Rows)
            {
                if (host == dr["Server_Ip"].ToString().Trim())
                {
                    upldPath = dr["Physical_Path"].ToString().Trim();
                    upldPath = upldPath + "\\Alumni\\StuImage" + "\\";
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }

    public string ReturnPath()
    {
        try
        {
            string host = HttpContext.Current.Request.Url.Host;
            DataSet dsFilepath = new DataSet();
            dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
            foreach (DataRow dr in dsFilepath.Tables[0].Rows)
            {
                if (host == dr["Server_Ip"].ToString().Trim())
                {
                    return dr["http_Add"].ToString().Trim();
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string ReturnPhysicalPath()
    {
        try
        {
            string host = HttpContext.Current.Request.Url.Host;
            DataSet dsFilepath = new DataSet();
            dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
            foreach (DataRow dr in dsFilepath.Tables[0].Rows)
            {
                if (host == dr["Server_Ip"].ToString().Trim())
                {
                    return dr["Physical_Path"].ToString().Trim();
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    public int InsertRecord(ref string Message)
    {
        ClearArrayList();
        names.Add("@pk_alumniid"); values.Add(AlumniId); types.Add(SqlDbType.VarChar);
        names.Add("@Cardid"); values.Add(Cardid); types.Add(SqlDbType.VarChar);
        if (DAobj.ExecuteTransactionMsg("ALM_SaveCardid", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    protected void btnIssueCard_Click(object sender, EventArgs e)
    {
        try
        {
            //string Message = "";
            DataSet ds = new DataSet();
            ds = DAobj.GetSchema("ALM_AlumniRegistration");
            DataRow dr = ds.Tables[0].NewRow();

            if (Session["AlumniID"] == null || Request.QueryString["ID"] == "")
            {
                return;
            }

            dr["isIssueRequestedAlmCard"] = Convert.ToBoolean(1);

            dr["CardId"] = lblCardID.Text.Trim().ToString();

            if (Session["AlumniID"].ToString() != "" && Session["AlumniID"] != null)
            {
                string aid = cpt.Encrypt(Session["AlumniID"].ToString());
                string baseUrl = "https://alumni.hpushimla.in/alumni/ALM_ICard_Verification.aspx";
                string qrCodeText = baseUrl + "?id=" + aid;
                SaveQRCodeImage(qrCodeText, lblCardID.Text.ToString());
            }

            ds.Tables[0].Rows.Add(dr);
            ArrayList Result = new ArrayList();
            string xmlDocs = ds.GetXml();
            int AlumniI = ALM_SP_Alumni_IssueAlmCardRequested(Convert.ToInt32(Session["AlumniID"].ToString()), xmlDocs).Execute();

            if (AlumniI > 0)
            {
                lblMsg.Text = "Alumni Card Requested Saved Successfully!";
                ClientMessaging("Alumni Card Requested Saved Successfully!");
                cardPreview();
            }
            else
            {
                lblMsg.Text = "Retry!";
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }

    private void deleteQRImgIfNotRequestedForCard()
    {
        if (ViewState["QRImgPath"] != null && ViewState["QRImgPath"].ToString() != "")
        {
            string QRImgfileDeleteFromPath = ViewState["QRImgPath"].ToString();
            if (System.IO.File.Exists(QRImgfileDeleteFromPath))
            {
                try
                {
                    System.IO.File.Delete(QRImgfileDeleteFromPath);
                }
                catch (Exception ex)
                {
                    //Label1.Text = "Error deleting file: " + ex.Message;
                }
            }
        }
    }

    #region "STORED PROCEDURES"

    public static StoredProcedure ALM_SP_Alumni_IssueAlmCardRequested(int pk_alumniid, string xmlDoc)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_Alumni_IssueAlmCardRequested", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        sp.Command.AddParameter("@xmlDoc", xmlDoc, DbType.String);
        return sp;
    }

    #endregion

    #region "CREATE HTML ALUMNI CARD"

    private string gethtmlContent()
    {
        string htmlContent = @"
        <div id='GFG' class='mt-60'>
            <div class='container-fluid'>
                <div class=''>
                    <div class='custom-center'>
                        <div class='alumni-card alumni-card-default'>
                            <div class='alumni-card-header'>
                                <img class='img-responsive' src='alumin-default-theme/images/alumni-card-logo.jpg' alt='Alumni Card Logo' />
                            </div>
                            <div class='alumni-card-body'>
                                <div class='card-image'>
                                    <img id='Imge1' class='img-responsive' src='<%# Eval(&quot;FileUrl&quot;) %>' alt='Alumni Image' />
                                </div>
                                <h4 class='mt-30'>&nbsp;</h4>
                                <p>Alumni, Class of</p>
                                <p class='p-left'>
                                    <strong>Course/Degree : </strong> <br />
                                    <strong>Division/Department : </strong>
                                </p>
                            </div>
                            <div class='alumni-card-footer'>
                                <div class='col-md-8' style='float: left;'>
                                    <p>CARDID: <br />ALUMNI.HPUNIV.AC.IN</p>
                                </div>
                                <div class='col-md-4'>
                                    <img id='imgQR' class='img-responsive' style='float: right; height: 70px; width: 70px;' alt='QR Code' />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class='custom-center'>&nbsp;</div>
                    <div class='custom-center'>&nbsp;</div>
                </div>
            </div>
        </div>";

        // Set the HTML content to the Literal control
        //LiteralAlumniCard.Text = htmlContent;
        return htmlContent;
    }

    #endregion

    protected void btnDownloadCard_Click(object sender, EventArgs e)
    {
        try
        {
            String script;
            script = String.Format("printContent();");
            Anthem.Manager.IncludePageScripts = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alumni Card Download", script, true);
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.ToString());
        }
    }

    #region "Crystal Report"

    protected void btnCardDownload_Click(object sender, EventArgs e)
    {
        ViewReport(Convert.ToInt32(Session["AlumniID"]));
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
                DataSet dsAR = ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(Session["AlumniID"])).GetDataSet();

                if (dsAR.Tables[0].Rows.Count > 0)
                {
                    dsAR.Tables[0].TableName = "Alumni_Details";
                    //dsAR.Tables[1].TableName = "Company_Details";
                    dsAR.WriteXml(Server.MapPath("~/Alumni/ALM_XML/AlumniCardReport.xml"));
                    filename = Server.MapPath("~/Alumni/ALM_Reports/AlumniCardReport.rpt");
                    objRptDoc.Load(filename);
                    objRptDoc.SetDataSource(dsAR);

                    objRptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AlumniCardReport");
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

    #endregion


    #region "For restricted file url access"

    public string GetBase64Image(string imagePath)
    {
        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
        string base64String = Convert.ToBase64String(imageBytes);
        string mimeType = System.Web.MimeMapping.GetMimeMapping(imagePath);
        return string.Format("data:{0};base64,{1}", mimeType, base64String);
    }

    #endregion    

    #region "Method to generate and display the QR code"

    private void GenerateMyQRCode(string qrCodeText, string CardID)
    {
        try
        {
            var QCwriter = new BarcodeWriter();
            QCwriter.Format = BarcodeFormat.QR_CODE;
            QCwriter.Options = new EncodingOptions
            {
                Width = 300,
                Height = 300,
                Margin = 1
            };
            var qrImage = QCwriter.Write(qrCodeText);
            imgQR.Src = "data:image/jpeg;base64," + ImageToBase64(qrImage, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        catch (Exception ex)
        {
            ClientMessaging("Error generating QR code: " + ex.Message);
        }
    }

    #endregion

    #region "Method to save the generated QR code image"

    private void SaveQRCodeImage(string qrCodeText, string cardID)
    {
        try
        {
            UploadFiles();

            string qrCodeImgFolderPath = this.upldPath;
            string QR = cardID;

            if (!Directory.Exists(qrCodeImgFolderPath))
            {
                Directory.CreateDirectory(qrCodeImgFolderPath);
            }

            var QCwriter = new BarcodeWriter();
            QCwriter.Format = BarcodeFormat.QR_CODE;
            QCwriter.Options = new EncodingOptions
            {
                Width = 300,
                Height = 300,
                Margin = 1
            };
            var result = QCwriter.Write(qrCodeText);
            string path = qrCodeImgFolderPath + QR + ".jpeg";
            var barcodeBitmap = new Bitmap(result);

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    bool IsExistPath = System.IO.Directory.Exists(qrCodeImgFolderPath);
                    if (!IsExistPath)
                        System.IO.Directory.CreateDirectory(qrCodeImgFolderPath);

                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            imgQR.Visible = true;
            string imgQRPath = ReturnPhysicalPath() + "\\Alumni\\StuImage\\" + QR + ".jpeg";
            string base64imgQR = GetBase64Image(imgQRPath);
            imgQR.Src = base64imgQR;
        }
        catch (Exception ex)
        {
            //Console.WriteLine("Error saving QR code: " + ex.Message);
        }
    }

    #endregion

    #region "Convert Image to Base64 string for displaying on the page"

    private string ImageToBase64(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
    {
        using (var ms = new System.IO.MemoryStream())
        {
            image.Save(ms, format);
            byte[] imageBytes = ms.ToArray();
            return Convert.ToBase64String(imageBytes);
        }
    }

    #endregion
}