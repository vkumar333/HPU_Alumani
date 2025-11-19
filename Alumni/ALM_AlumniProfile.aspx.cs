using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Routing;
using System.Data;
using Anthem;
using System.IO;
using SubSonic;
using System.Drawing;
using ZXing;
using System.Drawing.Imaging;
using DataAccessLayer;
using System.Text;

public partial class Alumni_ALM_AlumniProfile : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    crypto crp = new crypto();
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable Getmemberfullprofile(int alumniID)
    {
        ClearArrayLists();
        names.Add("@id"); types.Add(SqlDbType.Int); values.Add(alumniID);
        return Dobj.GetDataTable("ALM_GetBatchmateFullProfile", values, names, types);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "" || Request.QueryString["Alumni"] != null && Request.QueryString["Alumni"] != "")
            {
                try
                {
                    if (Request.QueryString["ID"] != null)
                    {
                        // int alumniID = Convert.ToInt32(Request.QueryString["ID"]);
                        int alumniID = Convert.ToInt32(crp.DecodeString(Request.QueryString["ID"].ToString()));

                        ProfileImgRepeter(alumniID);
                        BasicDetailsRepeaters(alumniID);
                        headingRepeter(alumniID);
                        BasicdetailsRepeter(alumniID);
                        EducationRepeter(alumniID);
                        WorkExpRepeter(alumniID);
                        contactRepeter(alumniID);
                        membershipRepeter(alumniID);
                    }
                    else if (Request.QueryString["Alumni"] != null)
                    {
                        // int alumniID = Convert.ToInt32(Request.QueryString["Alumni"]);
                        int alumniID = Convert.ToInt32(crp.DecodeString(Request.QueryString["Alumni"].ToString()));

                        ProfileImgRepeter(alumniID);
                        BasicDetailsRepeaters(alumniID);
                        headingRepeter(alumniID);
                        BasicdetailsRepeter(alumniID);
                        EducationRepeter(alumniID);
                        WorkExpRepeter(alumniID);
                        contactRepeter(alumniID);
                        membershipRepeter(alumniID);
                    }
                }
                catch (Exception Ex)
                {
                    string message = Ex.Message;
                    Response.Redirect("../Alumin_Loginpage.aspx");
                }
            }
            else
            {
                Response.Redirect("../Alumin_Loginpage.aspx");
            }
        }
    }

    private void headingRepeter(int alumniID)
    {
        DataTable dt = new DataTable();
        dt = Getmemberfullprofile(alumniID);
        if (dt.Rows.Count > 0)
        {
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
    }

    /// <summary>
    /// BasicdetailsRepeter
    /// </summary>
    /// <param name="alumniID"></param>
    private void BasicdetailsRepeter(int alumniID)
    {
        DataTable dt = new DataTable();
        dt = Getmemberfullprofile(alumniID);
        if (dt.Rows.Count > 0)
        {
            basicdetailsRep.DataSource = dt;
            basicdetailsRep.DataBind();
        }
    }

    /// <summary>
    /// Education details Repeter
    /// </summary>
    /// <param name="alumniID"></param>
    private void EducationRepeter(int alumniID)
    {
        //DataTable dt = new DataTable();
        //dt = Getmemberfullprofile(alumniID);
        //if (dt.Rows.Count > 0)
        //{
        //    RepEducation.DataSource = dt;
        //    RepEducation.DataBind();
        //}

        using (DataSet dsQual = ALM_SP_Get_EducationalDetails_By_AlumniID(alumniID).GetDataSet())
        {
            if (dsQual.Tables[0].Rows.Count > 0)
            {
                RepEducation.DataSource = dsQual.Tables[0];
                RepEducation.DataBind();
            }
            else
            {
                RepEducation.DataSource = null;
                RepEducation.DataBind();
            }
        }
    }

    /// <summary>
    /// WorkExp details Repeter
    /// </summary>
    /// <param name="alumniID"></param>
    private void WorkExpRepeter(int alumniID)
    {
        DataTable dt = new DataTable();
        dt = Getmemberfullprofile(alumniID);
        if (dt.Rows.Count > 0)
        {
            workRepeater.DataSource = dt;
            workRepeater.DataBind();
        }
    }

    /// <summary>
    /// contact details Repeter
    /// </summary>
    /// <param name="alumniID"></param>
    private void contactRepeter(int alumniID)
    {
        DataTable dtContact = new DataTable();
        dtContact = Getmemberfullprofile(alumniID);

        //if (dt.Rows.Count > 0)
        //{
        //    ContactRepeater.DataSource = dt;
        //    ContactRepeater.DataBind();
        //}

        // ✅ Ensure column exists (and not duplicated)
        if (!dtContact.Columns.Contains("shortUrl"))
            dtContact.Columns.Add("shortUrl", typeof(string));

        string longUrl = Request.RawUrl;

        //int n = longUrl.LastIndexOf('/');
        //string pagename = longUrl.Substring(n + 1, longUrl.Length - n - 1);
        //pagename = longUrl.Substring(n + 1, longUrl.Length - n - 1);

        //if (pagename.Contains("?"))
        //{
        //    string[] ss = pagename.Split('?');
        //    pagename = ss[0];
        //}

        //string shortUrl = AlumniUrlShortener.GetShortUrl(longUrl);        

        if (dtContact.Rows.Count > 0)
        {
            for (int x = 0; x < dtContact.Rows.Count; x++)
            {
                string encId = crp.EncodeString(Convert.ToInt32(alumniID));
                string shortUrl = AlumniUrlShortener.GetShortUrl(longUrl);
                dtContact.Rows[x]["shortUrl"] = shortUrl; //encId;
            }
            ContactRepeater.DataSource = dtContact;
            ContactRepeater.DataBind();
        }
    }

    /// <summary>
    /// membership details Repeter
    /// </summary>
    /// <param name="alumniID"></param>
    private void membershipRepeter(int alumniID)
    {
        DataTable dtMembership = new DataTable();
        dtMembership = Getmemberfullprofile(alumniID);

        //if (dt.Rows.Count > 0)
        //{
        //    ContactRepeater.DataSource = dt;
        //    ContactRepeater.DataBind();
        //}

        // ✅ Ensure column exists (and not duplicated)
        if (!dtMembership.Columns.Contains("shortUrl"))
            dtMembership.Columns.Add("shortUrl", typeof(string));

        string longUrl = Request.RawUrl;

        //int n = longUrl.LastIndexOf('/');
        //string pagename = longUrl.Substring(n + 1, longUrl.Length - n - 1);
        //pagename = longUrl.Substring(n + 1, longUrl.Length - n - 1);

        //if (pagename.Contains("?"))
        //{
        //    string[] ss = pagename.Split('?');
        //    pagename = ss[0];
        //}

        //string shortUrl = AlumniUrlShortener.GetShortUrl(longUrl);        

        if (dtMembership.Rows.Count > 0)
        {
            for (int x = 0; x < dtMembership.Rows.Count; x++)
            {
                string encId = crp.EncodeString(Convert.ToInt32(alumniID));
                string shortUrl = AlumniUrlShortener.GetShortUrl(longUrl);
                dtMembership.Rows[x]["shortUrl"] = shortUrl; //encId;
            }
            ContactRepeater.DataSource = dtMembership;
            ContactRepeater.DataBind();
        }
    }

    /// <summary>
    /// Basic details Repeter
    /// </summary>
    /// <param name="alumniID"></param>
    private void BasicDetailsRepeaters(int alumniID)
    {
        DataTable dt = new DataTable();
        dt = Getmemberfullprofile(alumniID);
        if (dt.Rows.Count > 0)
        {
            profileRepeater.DataSource = dt;
            profileRepeater.DataBind();
        }
    }

    /// <summary>
    /// ProfileImg details Repeter
    /// </summary>
    /// <param name="alumniID"></param>
    private void ProfileImgRepeter(int alumniID)
    {
        DataTable dt = new DataTable();
        dt = Getmemberfullprofile(alumniID);

        if (dt.Rows.Count > 0)
        {
            // string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
            // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
            ReprofileImg.DataSource = dt;
            ReprofileImg.DataBind();
        }
    }

    //protected void lnkCard_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int alumniID;
    //        if (Request.QueryString["ID"] != null || Request.QueryString["ID"] != "")
    //        {
    //            alumniID = Convert.ToInt32(Request.QueryString["ID"]);
    //        }
    //        else
    //        {
    //            return;
    //        }

    //        DataSet ds = ALM_SP_AlumniRegistration_Edit(alumniID).GetDataSet();

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            lblAlumni.Text = ds.Tables[0].Rows[0]["alumni_name"].ToString();
    //            ViewState["Alumni"] = lblAlumni.Text.ToString();
    //            lblPassingYear.Text = ds.Tables[0].Rows[0]["yearofpassing"].ToString();
    //            ViewState["yearofpassing"] = lblPassingYear.Text.ToString();
    //            lblDegree.Text = ds.Tables[0].Rows[0]["degreename"].ToString();
    //            ViewState["degreename"] = lblDegree.Text.ToString();
    //            lblCardID.Text = ds.Tables[0].Rows[0]["CardId"].ToString();
    //            lblDept.Text = ds.Tables[0].Rows[0]["DeptName"].ToString();
    //            ViewState["DeptName"] = lblDept.Text.ToString();

    //            string fileName = "";

    //            fileName = ds.Tables[0].Rows[0]["Files_Unique_Name"].ToString().Trim();
    //            GetAutoGeneratedCode();
    //            if (fileName != "")
    //            {
    //                if (ds.Tables[0].Rows[0]["Files_Unique_Name"].ToString() != "None")
    //                {
    //                    Imge1.Src = ds.Tables[0].Rows[0]["FileUrl"].ToString();
    //                }
    //            }
    //            else
    //            {
    //                Imge1.Src = "https://alumni.hpushimla.in/Alumni/StuImage/default-user.jpg"; //"~/alumni/stuimage/default_user.jpg";
    //            }
    //        }
    //        //string script = "openModal();";
    //        string script1 = String.Format("openModal();");
    //        Anthem.Manager.IncludePageScripts = true;
    //        ClientScript.RegisterStartupScript(this.GetType(), "OpenModalScript", script1, true);
    //    }
    //    catch (Exception ex)
    //    {
    //        //lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
    //    }
    //}

    public static StoredProcedure ALM_SP_AlumniRegistration_Edit(int? pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniCard_Details", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure ALM_SP_Get_EducationalDetails_By_AlumniID(int? pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_Get_EducationalDetails_By_AlumniID", DataService.GetInstance("IUMSNXG"), "");
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

            //string qrCodeText = $"{alumni}, {passng}, {degree}, {dept}"$;
            string qrCodeText = alumni + "," + passng + "," + degree + "," + lblCardID.Text + "";

            //string qrCodeUrl = "http://13.71.116.17/QRServices/api/ORCode/GenrateQRCode?QRCodeText='"+ qrCodeText + "'&Size=150";
            //imgQR.Src = qrCodeUrl;
            GenerateMyQCCode(qrCodeText, lblCardID.Text.ToString());
        }
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

    private void GenerateMyQCCode(string QCText, string Cardid)
    {
        string QR = Cardid;
        UploadFiles();
        string upldPath = "";
        string currDir = System.IO.Directory.GetCurrentDirectory();
        string fpath = this.upldPath;
        var QCwriter = new BarcodeWriter();
        QCwriter.Format = BarcodeFormat.QR_CODE;
        var result = QCwriter.Write(QCText);
        string path = fpath + QR + ".jpeg" + "";
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
        //imgQR.Src = ReturnPath() + "Alumni\\StuImage\\" + QR + ".jpeg";
        imgQR.Src = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/" + QR + ".jpeg";
    }

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

    protected void btnIssueCard_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = Dobj.GetSchema("ALM_AlumniRegistration");
            DataRow dr = ds.Tables[0].NewRow();

            if (Session["AlumniID"] == null || Request.QueryString["ID"] == "")
            {
                return;
            }

            dr["isIssueRequestedAlmCard"] = Convert.ToBoolean(1);
            ds.Tables[0].Rows.Add(dr);
            //string Message = "";
            ArrayList Result = new ArrayList();
            string xmlDocs = ds.GetXml();
            int AlumniI = ALM_SP_Alumni_IssueAlmCardRequested(Convert.ToInt32(Session["AlumniID"].ToString()), xmlDocs).Execute();

            if (AlumniI > 0)
            {
                lblMsg.Text = "Alumni Card Requested Saved Successfully!";
                ClientMessaging("Alumni Card Requested Saved Successfully!");
            }
            else
            {
                lblMsg.Text = "Retry!";
            }
        }
        catch (Exception ex)
        {
            //   lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }

    /// <summary>
    /// Creates an object wrapper for the ALM_SP_Alumni_IssueAlmCardRequested Procedure
    /// 23/01/2025
    /// </summary>
    public static StoredProcedure ALM_SP_Alumni_IssueAlmCardRequested(int pk_alumniid, string xmlDoc)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_Alumni_IssueAlmCardRequested", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        sp.Command.AddParameter("@xmlDoc", xmlDoc, DbType.String);
        return sp;
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
        // Anthem.Manager.AddScriptForClientSideEval("alert('" + msg + "');");
    }    
}

public static class AlumniUrlShortener
{
    // Base62 characters (10 digits + 26 uppercase + 26 lowercase)
    private const string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    // Dictionary to store the short ↔ long mapping (for demo; use DB in real app)
    private static Dictionary<string, string> urlMap = new Dictionary<string, string>();

    // 🔹 Encode numeric ID to Base62 (e.g., 1613 → Gd)
    public static string EncodeId(int id)
    {
        StringBuilder sb = new StringBuilder();
        while (id > 0)
        {
            int remainder = id % 62;
            sb.Insert(0, Base62Chars[remainder]);
            id /= 62;
        }
        return sb.ToString();
    }

    // 🔹 Decode Base62 back to integer (Gd → 1613)
    public static int DecodeId(string code)
    {
        int id = 0;
        foreach (char c in code)
        {
            id = id * 62 + Base62Chars.IndexOf(c);
        }
        return id;
    }

    // 🔹 Generate a short URL for given alumni URL
    public static string GetShortUrl(string longUrl)
    {
        // Extract alumni numeric ID from encoded query (for demo)
        // e.g., MTYxMw== → 1613
        string encodedId = ExtractEncodedId(longUrl);
        int numericId = DecodeBase64ToInt(encodedId);

        // Convert to Base62 short code
        string shortCode = EncodeId(numericId);

        // Build final short URL 
        // string shortUrl = "https://alumni.hpushimla.in/alumni/" + shortCode;
        string shortUrl = "http://localhost:53991/alumni/" + shortCode;

        // Save mapping (for demo; normally store in DB)
        urlMap[shortCode] = longUrl;

        return shortUrl;
    }

    private static string ExtractEncodedId(string url)
    {
        // Extract after "ID=" part
        int index = url.IndexOf("ID=");
        if (index == -1) return null;

        string encoded = url.Substring(index + 3);
        return Uri.UnescapeDataString(encoded);
    }

    // Decode base64 ID like "MTYxMw==" → 1613
    private static int DecodeBase64ToInt(string encoded)
    {
        try
        {
            byte[] data = Convert.FromBase64String(encoded);
            string decodedStr = Encoding.UTF8.GetString(data);
            int id;
            if (int.TryParse(decodedStr, out id))
                return id;
        }
        catch { }
        return 0;
    }

    // For reverse lookup (optional)
    public static string ResolveShortCode(string shortCode)
    {
        return urlMap.ContainsKey(shortCode) ? urlMap[shortCode] : null;
    }
}