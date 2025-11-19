<%@ WebHandler Language="C#" Class="CrowdFundingfileUploader" %>

using System;
using System.Web;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using SubSonic;
using IUMSNXG;
using DataAccessLayer;



public class CrowdFundingfileUploader : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string msg = "";
        if (context.Request.Files.Count > 0)
        {
            var Categories = context.Request.Form.GetValues("Categories")[0];
            var Heading = context.Request.Form.GetValues("Heading")[0];
            var Description = context.Request.Form.GetValues("Description")[0];
            var Amount = context.Request.Form.GetValues("Amount")[0];
            //validation data and file
            if (string.IsNullOrEmpty(Categories))
            {
                msg = "Categories is Required";
            }
            if (string.IsNullOrEmpty(Heading))
            {
                msg = "Categories is Required";
            }
            if (string.IsNullOrEmpty(Description))
            {
                msg = "Categories is Required";
            }
            if (string.IsNullOrEmpty(Amount))
            {
                msg = "Categories is Required";
            }
            if (string.IsNullOrEmpty(Amount))
            {
                msg = "Categories is Required";
            }

            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                //file save
                //HttpPostedFile file = files[i];
                //string fname = context.Server.MapPath("~/ALM_uploadimg/" + file.FileName);
                //file.SaveAs(fname);
                HttpPostedFile file = files[i];
                Random r = new Random();
                int n = r.Next();
                string fname = FU_physicalPath(file, "PLACEMENT_DATA/Company_Profile", "BRO" + n.ToString() + file.FileName);
                //file.SaveAs(fname);
                msg = "data save sucess";

            }

            //Db logic

            //Response
            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


    public string FU_physicalPath(HttpPostedFile file , string FolderName, string FileName)
    {
        try
        {
            string host = HttpContext.Current.Request.Url.Host;
            string upldPath = "";
            DataSet dsFilepath = new DataSet();
            dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
            foreach (DataRow dr in dsFilepath.Tables[0].Rows)
            {
                if (host == dr["Server_Ip"].ToString().Trim())
                {

                    upldPath = dr["Physical_Path"].ToString().Trim();
                    upldPath = upldPath + "\\PLACEMENT_DATA\\Company_Profile\\" + "";
                    //Session["filepath"] = upldPath;
                    upldPath = upldPath + FileName;
                    file.SaveAs(upldPath);
                    return FileName;
                }
            }

            return "";
        }
        catch (Exception ex)
        {
            throw new Exception("Upload Fails!");
        }
    }
    //public bool Validation()
    //{

    //    if (Categories.Text == "")
    //    {
    //        ClientMessaging("Event Name is Required");
    //        R_txtEventTitle.Focus();
    //        return false;
    //    }

    //    else if (R_txtEventDescription.Text == "" || R_txtEventDescription.Text == null)
    //    {
    //        ClientMessaging("Description is Required");
    //        R_txtEventDescription.Focus();
    //        return false;
    //    }
    //    else if (V_txtStartDate.Text == "" || V_txtStartDate.Text == null)
    //    {
    //        ClientMessaging("Start Date id Required");
    //        V_txtStartDate.Focus();
    //        return false;
    //    }

    //    else if (V_txtEndDate.Text == "" || V_txtEndDate.Text == null)
    //    {
    //        ClientMessaging("End Date id Required");
    //        V_txtEndDate.Focus();
    //        return false;
    //    }
    //    string startDate = CommonCode.DateFormats.Date_FrontToDB_R(V_txtStartDate.Text.Trim());
    //    string EndDate = CommonCode.DateFormats.Date_FrontToDB_R(V_txtEndDate.Text.Trim());
    //    if (Convert.ToDateTime(startDate) > Convert.ToDateTime(EndDate))
    //    {
    //        Client_Messaging("Event Start Date Can not be greated than End Date.");
    //        // Anthem.Manager.IncludePageScripts = true;
    //        V_txtStartDate.Focus();
    //        return false;
    //    }
    //    else if (TextAddress.Text == "" || TextAddress.Text == null)
    //    {
    //        ClientMessaging("Address is Required");
    //        TextAddress.Focus();
    //        return false;
    //    }
    //    else if (flUploadLogo.FileName == null || flUploadLogo.FileName == "")
    //    {
    //        ClientMessaging("Please Upload File...!");
    //        flUploadLogo.Focus();
    //        return false;
    //    }
    //    if (flUploadLogo.HasFile && (System.IO.Path.GetExtension(flUploadLogo.FileName) != ".jpg")
    //        && System.IO.Path.GetExtension(flUploadLogo.FileName) != ".jpeg" && System.IO.Path.GetExtension(flUploadLogo.FileName) != ".png")
    //    {
    //        ClientMessaging("Only JPG, JPEG and PNG files can be uploaded...!");
    //        flUploadLogo.Focus();
    //        return false;
    //    }
    //    return true;
    //}

}