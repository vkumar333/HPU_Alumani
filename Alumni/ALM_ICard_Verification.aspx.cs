using DataAccessLayer;
using SubSonic;
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

public partial class Alumni_ALM_ICard_Verification : System.Web.UI.Page
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
        if (Request.QueryString["id"].ToString() != "" && Request.QueryString["id"] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    string decID = cpt.Decrypt(Request.QueryString["id"].ToString());
                    Session["AlumniID"] = decID;

                    DataSet dsDA = ALM_SP_AlumniICard_Verify(Convert.ToInt32(Session["AlumniID"])).GetDataSet();

                    if (dsDA != null && dsDA.Tables[0].Rows.Count > 0)
                    {
                        bool isVisible = Convert.ToBoolean(dsDA.Tables[0].Rows[0]["ICardApprBtnIsVisible"]);

                        if (isVisible)
                        {
                            pnlICardVerified.Visible = true;
                            pnlICardNotVerified.Visible = false;

                            lblAlumni.Text = dsDA.Tables[0].Rows[0]["alumni_name"].ToString();
                            ViewState["Alumni"] = lblAlumni.Text.ToString();
                            lblPassingYear.Text = dsDA.Tables[0].Rows[0]["yearofpassing"].ToString();
                            ViewState["yearofpassing"] = lblPassingYear.Text.ToString();
                            lblDegree.Text = dsDA.Tables[0].Rows[0]["degreename"].ToString();
                            ViewState["degreename"] = lblDegree.Text.ToString();
                            lblDept.Text = dsDA.Tables[0].Rows[0]["DeptName"].ToString();
                            ViewState["DeptName"] = lblDept.Text.ToString();
                            //lblCardID.Text = string.IsNullOrEmpty(dsDA.Tables[0].Rows[0]["CardId"].ToString()) ? "" : dsDA.Tables[0].Rows[0]["CardId"].ToString();

                            string fileName = "";

                            fileName = dsDA.Tables[0].Rows[0]["Files_Unique_Name"].ToString().Trim();

                            if (fileName != "")
                            {
                                if (dsDA.Tables[0].Rows[0]["Files_Unique_Name"].ToString() != "None")
                                {
                                    Imge1.Src = dsDA.Tables[0].Rows[0]["FileUrl"].ToString();
                                }
                            }
                            else
                            {
                                Imge1.Src = "~/alumni/stuimage/No_image.png";
                            }
                        }
                        else
                        {
                            pnlICardVerified.Visible = false;
                            pnlICardNotVerified.Visible = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
                }
            }
        }
        else
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
    }

    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        Session["dtArchive"] = null;
        Anthem.Manager.IncludePageScripts = true;
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", @"window.open(htmlContent, '_blank', 'toolbar=yes, scrollbars=yes, resizable=yes, top=500, left=500, width=400, height=400');", true);
    }

    public static StoredProcedure ALM_SP_AlumniICard_Verify(int? pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniCard_Details", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure GetAutoProjectCode()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_GetAuto_GeneratedCode", DataService.GetInstance("IUMSNXG"), "");
        return sp;
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

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Alumin_Loginpage.aspx");
    }

    protected void btnCardScannerV_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://qrsnapper.com");
        //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "window.location.href = 'https://qrsnapper.com';", true);
    }

    protected void btnScannerNV_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://qrsnapper.com");
        //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "window.location.href = 'https://qrsnapper.com';", true);
    }
}