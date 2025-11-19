using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Net.Security;
using DataAccessLayer;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Specialized;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using SubSonic;
using System.Data.SqlClient;
using System.Configuration;

public partial class Alumni_PendingAlumniMasterPage : System.Web.UI.MasterPage
{
    #region DB_Work

    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["IUMSNXG"].ConnectionString);

    private Boolean IsPageRefresh = false;

    public StoredProcedure CompanyShow(int cmpId)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("PLC_SP_RejectedCompanyRegistration_List1", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@cmpId", cmpId, DbType.Int32);
        return sp;
    }

    public StoredProcedure Alumsessionvalueget(int pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_sessionvalueget", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    public StoredProcedure HPU_ALM_Paymentcheck(int pk_Regid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Paymentcheck", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
        return sp;
    }

    public StoredProcedure HPU_ALM_Statuscheck(int pk_Regid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Approvestatuscheck", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
        return sp;
    }

    public StoredProcedure HPU_ALM_Mentorcheck(int pk_Regid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_ApproveMentorcheck", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_Regid", pk_Regid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure ALM_SP_AlumniRegistration_Edit(int? pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_EditNew", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure ALM_AlumniUser_OnlineStatus_Upd(int? pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_AlumniUser_OnlineStatus_Upd", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    #endregion

    public string UserName = "";
    public string UserImage = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["AlumniName"] != null)
                {
                    //  dsave.Visible = false;
                    UserName = Session["AlumniName"].ToString();
                    GetUserImage(UserName);
                    //Set_AlumniImg();
                }
                if (Session["AlumniID"] != null && Session["AlumniID"].ToString() != "")
                {
                    //getprofile();
                    CheckApprovestatus();
                    //CheckMentor();
                    //Set_AlumniImg();
                    //btnLogout.Enabled = true;
                    //lnkprofile.Text = "My Profile";
                    CheckStatusOfAllPreviousTransaction();
                    checkbuttonenable();
                }
                else
                {
                    btnLogout.Enabled = false;
                    Response.Redirect("../Alumin_Loginpage.aspx");
                }
            }
            catch (Exception ex)
            {
                btnLogout.Enabled = false;
                Response.Redirect("../Alumin_Loginpage.aspx");
            }
        }
    }

    public void GetUserImage(string Username)
    {
        ////if (Username != null)
        ////{
        ////    string query = "select Photo from tbl_Users where UserName='" + Username + "'";

        ////    string ImageName = ConnC.GetColumnVal(query, "Photo");
        ////    if (ImageName != "")
        ////        UserImage = "images/DP/" + ImageName;
        ////}
        UserImage = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/HPU_Alumni_PP__bc0e7d1f-1e80-4bd9-8f83-12cb1be46360_854602_20240529114405050PP_426818499_4384725.jpg";

    }
    protected void checkbuttonenable()
    {
        if (Session["AlumniID"] != null)
        {
            if (Session["AlumniID"].ToString() != "")
            {
                int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
                DataSet ds = HPU_ALM_Paymentcheck(alumniId).GetDataSet();
                var count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {
                    //lnkPay.Visible = false;
                }
                else
                {
                    //lnkPay.Visible = true;
                    //Response.Redirect("../Alumni/ALM_AlumniProfileDetails.aspx");
                }
            }
        }
    }

    protected void CheckApprovestatus()
    {
        if (Session["AlumniID"] != null)
        {
            if (Session["AlumniID"].ToString() != "")
            {
                int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
                DataSet ds = HPU_ALM_Statuscheck(alumniId).GetDataSet();
                var count = ds.Tables[0].Rows.Count;

                if (count > 0)
                {
                    Session["AlumniName"] = ds.Tables[0].Rows[0]["alumni_name"].ToString().Trim();
                    // lnkpay.Visible = true;
                    //LinkNews.Visible = true;
                    //LinkEvents.Visible = true;
                    //LinkSuggestion.Visible = true;
                    //LinkSearch.Visible = true;
                    //LinkPublishJobs.Visible = true;
                    //lnkInternships.Visible = true;
                    //LinkChangePass.Visible = true;
                    //LinkMentee.Visible = true;
                }
                else
                {
                    //LinkNews.Visible = false;
                    //LinkEvents.Visible = false;
                    //LinkSuggestion.Visible = false;
                    //LinkSearch.Visible = false;
                    //LinkPublishJobs.Visible = false;
                    //lnkInternships.Visible = false;
                    //LinkChangePass.Visible = false;
                    //LinkMentee.Visible = false;
                    //Lnkmeneeassigned.Visible = false;
                }
            }
        }
    }

    protected void CheckMentor()
    {
        if (Session["AlumniID"] != null)
        {
            if (Session["AlumniID"].ToString() != "")
            {
                int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
                DataSet ds = HPU_ALM_Mentorcheck(alumniId).GetDataSet();
                var count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {
                    // lnkpay.Visible = true;
                    //LinkNews.Visible = true;
                    //LinkEvents.Visible = true;
                    //LinkSuggestion.Visible = true;
                    //LinkSearch.Visible = true;
                    //LinkPublishJobs.Visible = true;
                    //lnkInternships.Visible = true;
                    //LinkChangePass.Visible = true;
                    //LinkMentee.Visible = false;
                    //Lnkmeneeassigned.Visible = true;
                }
                else
                {
                    //LinkPublishJobs.Visible = false;
                    //Lnkmeneeassigned.Visible = false;
                    //lnkInternships.Visible = false;
                }
            }
        }
        //else
        //{
        //  lnkpay.Visible = true;
        //}
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        if (Session["AlumniID"] != null)
        {
            int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
            DataSet dsUser = ALM_AlumniUser_OnlineStatus_Upd(alumniId).GetDataSet();
        }
        Session.Abandon();
        Response.Redirect("../Alumin_Loginpage.aspx");
    }

    /// <summary>
    /// Modified Aditya Sharma
    /// date: 16-02-2023
    /// </summary>
    public void Set_AlumniImg()
    {
        if (Session["AlumniImg"] != null)
        {
            int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
            DataSet ds = ALM_SP_AlumniRegistration_Edit(alumniId).GetDataSet();
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            //if (dt.ToString() == string.Empty)
            //{
            if (dt.Rows.Count > 0)
            {
                string fileName = ""; string contenttype = ""; byte[] fileBytes = null;
                fileName = dt.Rows[0]["Files_Unique_Name"].ToString().Trim();
                if (ds.Tables[0].Rows[0]["Files_Unique_Name"].ToString() != "None")
                {
                    img_mast_user.Src = ds.Tables[0].Rows[0]["Files_Unique_Name"].ToString();
                    UserImage = ds.Tables[0].Rows[0]["Files_Unique_Name"].ToString();
                }
                else
                {
                    img_mast_user.Src = "~/alumni/stuimage/noimage.png";
                    UserImage = "~/alumni/stuimage/noimage.png";
                }
            }

            //  }
            //if (fileName != "")
            //{
            //    contenttype = dt.Rows[0]["fileextension"].ToString().Trim();
            //    string host = HttpContext.Current.Request.Url.Host;
            //    string upldPath = "";
            //    string showimgPath = "";
            //    DataSet dsFilepath = new DataSet();
            //    dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
            //    foreach (DataRow dr in dsFilepath.Tables[0].Rows)
            //    {
            //        if (host == dr["Server_Ip"].ToString().Trim())
            //        {
            //            upldPath = dr["Physical_Path"].ToString().Trim();
            //            upldPath = upldPath + "/Alumni/StuImage/" + fileName.ToString().Trim();
            //            //img_mast_user.ImageUrl = "~/alumni/stuimage/No_image.png";
            //            showimgPath = upldPath.ToString().Trim();
            //            //filepath = upldPath;
            //        }
            //    }
            //    //img_mast_user.ImageUrl = "~/alumni/stuimage/No_image.png";
            //    img_mast_user.ImageUrl = upldPath; //"~/alumni/stuimage/" + fileName;
            //    //upldPath = upldPath + "\\" + "Alumni\\StuImage" + "\\" + fileName.ToString().Trim();
            //    ////img_mast_user.ImageUrl = upldPath + dt.Rows[0]["Files_Unique_Name"].ToString();
            //    //img_mast_user.ImageUrl = upldPath.ToString().Trim();
            //}

            else
            {
                img_mast_user.Src = null;
                img_mast_user.Src = "~/alumni/stuimage/No_image.png";
            }
        }
        else
        {
            img_mast_user.Src = null;
            img_mast_user.Src = "~/alumni/stuimage/No_image.png";
        }
    }

    //protected void getsessionvalue()
    //{
    //    if(Session["AlumniID"]!=null)
    //  {
    //        int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
    //        DataSet ds = Alumsessionvalueget(alumniId).GetDataSet();
    //        Session["regid"] = ds.Tables[0].Rows[0]["regid"].ToString();
    //    }
    //}

    //protected void Pay()
    //{
    //    int loginId = Convert.ToInt32(Session["LoginId"].ToString());
    //    DataSet ds = CompanyShow(loginId).GetDataSet();
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        DataRow dr = ds.Tables[0].Rows[0];
    //        {
    //            if (Convert.ToBoolean(dr["Cnt"]) == true) /* && (Convert.ToBoolean(dr["Cnt"] = 1));*/
    //            {
    //                lnkpay.Visible = true;
    //            }
    //            else
    //            {
    //                lnkpay.Visible = false;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        lnkpay.Visible = false;
    //    }
    //}

    public void getprofile()
    {
        if (Session["AlumniID"] != null || Session["AlumniID"].ToString() != "")
        {
            btnLogout.Enabled = true;
            int alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
            DataSet ds = ALM_SP_AlumniRegistration_Edit(alumniId).GetDataSet();
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            string fileName = ""; string contenttype = ""; byte[] fileBytes = null;

            if (dt.ToString() == string.Empty)
            {
                fileName = dt.Rows[0]["Files_Unique_Name"].ToString().Trim();

                if (fileName != "")
                {
                    contenttype = dt.Rows[0]["fileextension"].ToString().Trim();
                    string host = HttpContext.Current.Request.Url.Host;
                    string upldPath = "";
                    string showimgPath = "";
                    DataSet dsFilepath = new DataSet();
                    dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
                    foreach (DataRow dr in dsFilepath.Tables[0].Rows)
                    {
                        if (host == dr["Server_Ip"].ToString().Trim())
                        {
                            upldPath = dr["Physical_Path"].ToString().Trim();
                            showimgPath = upldPath.ToString().Trim();
                        }
                    }
                    img_mast_user.Src = upldPath; //"~/alumni/stuimage/" + fileName;
                }
                else
                {
                    img_mast_user.Src = "~/alumni/stuimage/noimage.png";
                }
            }
        }
        else
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
    }

    protected void lnkpay_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_AlumniPayLater.aspx");
    }

    protected void LinkNews_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_Alumni_NewsStories_Lists.aspx");
    }

    protected void LinkEvents_Click(object sender, EventArgs e)
    {
        Response.Redirect("Alumni_EventsGallery_View.aspx");
    }

    protected void LinkSuggestion_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_AlumniSuggestion.aspx");
    }

    protected void LinkSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_AlumniSearch.aspx");
    }

    protected void LinkPublishJobs_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_Alumni_PublishJobs.aspx");
    }

    protected void LinkChangePass_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_AlumniChangePassword.aspx");
    }

    protected void LinkMentee_Click(object sender, EventArgs e)
    {
        Response.Redirect("Alm_BecomeMentee.aspx");
    }

    protected void Lnkmeneeassigned_Click(object sender, EventArgs e)
    {
        Response.Redirect("Alm_Mentees_Assigned.aspx");
    }

    protected void lnkprofile_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_AlumniProfileShow.aspx");
    }

    //protected void lnkbtnContribution_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("ALM_FundDonationlist_Contributions.aspx");
    //}

    protected void lnkContribtion_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_Fund_DonationLists.aspx");
    }

    protected void lnkInternships_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_Alumni_Publish_Internships.aspx");
    }

    protected void lnkGenAlumniCard_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Alumni/ALM_AlumniCard.aspx");
    }

    protected void lnkFailedRegisterAlumni_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_AlumniProfileDetails.aspx");
    }

    public void CheckStatusOfAllPreviousTransaction()
    {
        try
        {
            string Msg = "";

            long regid = Convert.ToInt64(Session["AlumniID"]);

            DataSet ds = Get_All_PreviousTransaction(regid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtGetAllStatus = new DataTable();
                dtGetAllStatus = ds.Tables[0].Copy();
                dtGetAllStatus.Columns.Add("CurrentStatus", typeof(string));
                dtGetAllStatus.Columns.Add("Reference_No", typeof(string));
                dtGetAllStatus.TableName = "Status_Table";

                foreach (DataRow dr in dtGetAllStatus.Rows)
                {
                    //first check here for which gateway this transaction is initiated like if Fk_PaymentGId=1 then for HDFC gateway if Fk_PaymentGId =2 then for SBI gateway if Fk_PaymentGId=3 then for HPSCB Bank
                    string UniqueCode = "";

                    // "Get Responce From SBI"
                    if (dr["Fk_PaymentGId"].ToString() == "2")//SBI
                    {
                        if (dr["status"].ToString().ToUpper() != "FAILURE")
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //checked here the double verification responce from SBI bank
                                string StatusOfTran = "";
                                //Dictionary<string, string> DoubleVerification_Live_FromSBI_Gateway_Res = DoubleVerification_Live_FromSBI_Gateway(dr["RegFees"].ToString(), dr["TransactionID"].ToString(), ref StatusOfTran);
                                Dictionary<string, string> DoubleVerification_Live_FromSBI_Gateway_Res = DoubleVerification_Live_FromSBI_Gateway(dr["amount"].ToString(), dr["TransactionID"].ToString(), ref StatusOfTran);

                                //step 3 Pk_TranId is sent to SBI gateway for double verification
                                //Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);

                                if (DoubleVerification_Live_FromSBI_Gateway_Res.Count > 0 && StatusOfTran.Length > 0) //status of tran will be greated than 0 if record exists in HPSCB bank
                                {
                                    //now put here all logic to update the db as per current status of transaction
                                    //check here transaction status and update to database 
                                    //string Msg = "";
                                    if (dr["status"].ToString() == "" || dr["status"].ToString().ToUpper().Contains("PENDING"))
                                    {
                                        Pk_TranId = dr["TransactionID"].ToString();
                                        //PK_PurchaseId = Convert.ToInt64(dr["fk_purchaseid"].ToString());
                                        IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                                        PK_PaymentGatewayId = 2; // as responce is coming from HPSCB gateway
                                        DataSet dsmain = GetSchema("ALM_eCoupon_Purchase_Payment_Trn");
                                        DataRow drMain = dsmain.Tables[0].NewRow();
                                        drMain["status"] = DoubleVerification_Live_FromSBI_Gateway_Res["status"].ToString();
                                        drMain["bank_ref_num"] = DoubleVerification_Live_FromSBI_Gateway_Res["sbi_ref_no"].ToString();
                                        drMain["status_description"] = DoubleVerification_Live_FromSBI_Gateway_Res["status_desc"].ToString();
                                        drMain["fk_purchaseid"] = Convert.ToInt64(dr["Fk_purchaseid"].ToString());
                                        //drMain["sbi_chksum"] = DoubleVerification_Live_FromSBI_Gateway_Res["checkSum"].ToString();

                                        dsmain.Tables[0].Rows.Add(drMain);

                                        XmlDoc = dsmain.GetXml();

                                        if (UpdateTransactionRecord(ref Msg) > 0)
                                        {
                                            //step 4 got the latest update from SBI gateway and Transaction table is updated with Pk_tranId= dr["pk_TranId"].ToString() id
                                            Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
                                            Pk_TranId = dr["TransactionID"].ToString();
                                            Fk_regid = Session["FK_RegId"].ToString();
                                            //DataSet dsCode = Get_Payment_UniqueCode();
                                            //if (dsCode.Tables[0].Rows.Count > 0)
                                            //{
                                            //    UniqueCode = dsCode.Tables[0].Rows[0]["uniqueCode"].ToString();
                                            //    //step 5 reference no is generated as previous payment is confirmed
                                            //    Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 5 reference no is generated (SBI) as previous payment is confirmed", ref Msg);
                                            //}
                                        }
                                    }
                                    //now add here status and refno if created in db
                                    dr["CurrentStatus"] = DoubleVerification_Live_FromSBI_Gateway_Res["status"].ToString();
                                    dr["Reference_No"] = UniqueCode;
                                    if (dr["CurrentStatus"].ToString().ToUpper() == "SUCCESS")//&& UniqueCode.Length > 10 check here unique cod is generated or not
                                    {
                                        lblPaymentMsg.Text = "Your Previous payment is <B>Success</B> Now visit to the status page.";
                                        //btnPay.Visible = false;
                                        ViewState["IsShowPayButton"] = "0";
                                        //dgDetails.DataSource = dtGetAllStatus;
                                        //dgDetails.DataBind();
                                        //step 6 current status of payment is success user is blocked from making new payment
                                        Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
                                        return;
                                    }
                                    else if (dr["CurrentStatus"].ToString().ToUpper() == "PENDING")
                                    {
                                        string Trndate = dr["transactiondate"].ToString();
                                        TimeSpan varTime = (DateTime)DateTime.Now - (DateTime)Convert.ToDateTime(Trndate);
                                        double fractionalMinutes = varTime.TotalMinutes;
                                        int wholeMinutes = (int)fractionalMinutes;
                                        if (wholeMinutes < 1440) //if 24 hours is completed then show button to make payment again
                                        {
                                            lblPaymentMsg.Text = "Wait for atleast 24 Hrs to get previous transaction Status. as Transaction status is  pending. Then try again!";
                                            //btnPay.Visible = false;
                                            ViewState["IsShowPayButton"] = "0";
                                            //step 7 user is still waiting for 24 hrs as current status of this transaction is pending
                                            Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
                                        }
                                        else
                                        {
                                            //btnPay.Visible = true;
                                            ViewState["IsShowPayButton"] = "1";
                                            //step 8 user is allowed to make payment after 24 hours of waiting
                                            Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
                                        }
                                    }
                                    else if (dr["CurrentStatus"].ToString().ToUpper() == "FAILURE")
                                    {
                                        // btnPay.Visible = true;
                                        ViewState["IsShowPayButton"] = "1";
                                        //step 9 current status of this transaction is Failed
                                        Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
                                    }
                                    dr["CurrentStatus"] = dr["CurrentStatus"].ToString().Length == 0 ? "N/A" : dr["CurrentStatus"].ToString();
                                }
                                else
                                {
                                    //btnPay.Visible = true;// as there is no record of this txnid in bankdb so allow user to pay
                                    //step 10 user tried to go to payment gateway but disconnect so HPSCB gateway is not having any info so user is allowd to pay again
                                    Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
                                    ViewState["IsShowPayButton"] = "1";
                                }
                                //ViewState["IsShowPayButton"] = "1";// remove it when code is written inside above method for double verification
                            }
                        }
                        else
                        {
                            //in case if status is failure  then 
                            dr["CurrentStatus"] = dr["status"].ToString();
                            ViewState["IsShowPayButton"] = "1";
                            //step2 alreay Transaction table is updated with Failure
                            Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
                        }
                    }

                }
                //now bind the grid with latest status of transaction
                //dgDetails.DataSource = dtGetAllStatus;
                //dgDetails.DataBind();
            }
            else
            {
                //setp 1 first time user is going to pay
                //btnPay.Visible = true;
                ViewState["IsShowPayButton"] = "1";
                Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), "0", 0, "setp 1 first time user is going to pay", ref Msg);
            }
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }

    public DataSet Get_All_PreviousTransaction(long Pk_Regid)//,string Degreeyear,int Examtype
    {
        Clear();
        names.Add("@pk_regid"); values.Add(Pk_Regid); types.Add(SqlDbType.BigInt);
        // names.Add("@DegreeYear"); values.Add(Degreeyear); types.Add(SqlDbType.NVarChar);
        // names.Add("@ExamTypeid"); values.Add(Examtype); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("HPU_ALM_Select_AllTransactionOf_Alumni", values, names, types);
    }

    public int Insert_DoubleVerification_log(long fk_regid, string fk_TranId, int fk_GatewayId, string Descriptions, ref string Message)
    {
        Clear();

        names.Add("@fk_regid"); values.Add(fk_regid); types.Add(SqlDbType.BigInt);
        names.Add("@fk_TranId"); values.Add(fk_TranId); types.Add(SqlDbType.VarChar);
        names.Add("@fk_GatewayId"); values.Add(fk_GatewayId); types.Add(SqlDbType.Int);
        names.Add("@Descriptions"); values.Add(Descriptions); types.Add(SqlDbType.VarChar);
        if (DAobj.ExecuteTransactionMsg("[HPU_ALM_DoubleVerification_log_ins]", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int UpdateTransactionRecord(ref string Message)
    {
        Clear();
        names.Add("@xmlDoc"); values.Add(_xmlDoc); types.Add(SqlDbType.VarChar);
        names.Add("@IpAddress"); values.Add(_IpAddress); types.Add(SqlDbType.VarChar);
        //names.Add("@pk_purchaseid"); values.Add(_PK_PurchaseId); types.Add(SqlDbType.BigInt);
        names.Add("@Pk_Tranid"); values.Add(_Pk_TranId); types.Add(SqlDbType.VarChar);
        names.Add("@Pk_GatewayId"); values.Add(_PK_PaymentGatewayId); types.Add(SqlDbType.Int);
        if (DAobj.ExecuteTransactionMsg("ALM_Online_Payment_Upd_ForAlumni", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("U", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    //=================================SBI DOUBLE VERIFICATION CODE============================================
    //  "Code of SBI payment Gateway For Double Verification"
    public Dictionary<string, string> DoubleVerification_Live_FromSBI_Gateway(string total_amt, string ref_no, ref string CurrentStatusOfTran)
    {
        //string merchant_code = "HIMACHAL_UNIV";
        //string merchant_code = "HIMA_UNIV";
        //string url_string = "total_amt=" + total_amt.ToString().Trim() + "|" + "transaction_id=" + ref_no;
        //byte[] byteArray = Encoding.UTF8.GetBytes(url_string);
        //MemoryStream stream = new MemoryStream(byteArray);
        //string checkSum = GetSHA256(url_string);//GetMD5Hash(stream);
        //string encdata = url_string + "|" + "checkSum=" + checkSum;
        //encdata = EncryptWithKey(encdata, Server.MapPath("~/Key/HIMACHAL_UNIV.key"));
        //ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        //string post_url = "https://www.onlinesbi.sbi/thirdparties/doubleverification.htm";

        string merchant_code = "HPUNIV_ALUMNI";
        string url_string = "total_amt=" + total_amt.ToString().Trim() + "|" + "transaction_id=" + ref_no;
        // byte[] byteArray = Encoding.UTF8.GetBytes(url_string);
        // MemoryStream stream = new MemoryStream(byteArray);
        string checkSum = GetSHA256(url_string);//GetMD5Hash(stream);
        string encdata = url_string + "|" + "checkSum=" + checkSum;
        encdata = EncryptWithKey(encdata, Server.MapPath("~/Key/HIMACHAL_UNIV.key"));
        //ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        //string post_url = "https://www.onlinesbi.sbi/thirdparties/doubleverification.htm";
        string post_url = "https://merchant.onlinesbi.sbi/thirdparties/doubleverification.htm";
        using (var client = new WebClient())
        {
            var values = new NameValueCollection();
            values["encdata"] = encdata;
            values["merchant_code"] = merchant_code;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            var response = client.UploadValues(post_url, values);
            var responseString = Encoding.Default.GetString(response);
            string DecryptStr = "";
            //DecryptStr = Decrypt(Request.Form["encdata"], Server.MapPath("~/Key/TNAU.key"));
            DecryptStr = DecryptWithKey(responseString, Server.MapPath("~/Key/HIMACHAL_UNIV.key"));
            string sbi_ref_no1 = "", transaction_id = "", status = "", status_desc = "", checkSum1 = "";
            //Splitting The response code received from the SBI Bank Gateway
            string[] ArrStr1 = DecryptStr.Split('|');
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < ArrStr1.Length; i++)
            {
                if (i == 0)
                {
                    sbi_ref_no1 = Convert.ToString(ArrStr1[i].ToString()).Replace("sbi_ref_no=", "").Trim();
                    dic.Add("sbi_ref_no", sbi_ref_no1);
                }
                else if (i == 1)
                {
                    transaction_id = Convert.ToString(ArrStr1[i]).Replace("transaction_id=", "").Trim();
                    dic.Add("transaction_id", transaction_id);
                }
                else if (i == 2)
                {
                    total_amt = Convert.ToString(ArrStr1[i]).Replace("total_amt=", "").Trim();
                    dic.Add("total_amt", total_amt);
                }
                else if (i == 3)
                {
                    status = Convert.ToString(ArrStr1[i]).Replace("status=", "").Trim();
                    dic.Add("status", status);
                }
                else if (i == 4)

                {
                    status_desc = Convert.ToString(ArrStr1[i]).Replace("status_desc=", "").Trim();
                    dic.Add("status_desc", status_desc);
                }
                else if (i == 5)
                {
                    checkSum1 = Convert.ToString(ArrStr1[i]).Replace("checkSum=", "").Trim();
                    dic.Add("checkSum", checkSum1);
                }
            }
            CurrentStatusOfTran = dic["status"].ToString();
            return dic;
        }
    }

    public string DecryptWithKey(string encrypteddata, string FilePath, int nonSecretPayloadLength = 0)
    {
        byte[] key = File.ReadAllBytes(FilePath);
        byte[] encryptedMessage = Convert.FromBase64String(encrypteddata);
        if (encryptedMessage == null || encryptedMessage.Length == 0)
        {
            throw new ArgumentException("Encrypted Message Required!", "encryptedMessage");
        }
        using (var cipherStream = new MemoryStream(encryptedMessage))
        using (var cipherReader = new BinaryReader(cipherStream))
        {
            //Grab Payload   
            //Grab Nonce
            var nonce = cipherReader.ReadBytes(16);
            //Decrypt Cipher Text
            var cipherText = cipherReader.ReadBytes(encryptedMessage.Length);
            var nonSecretPayload = cipherReader.ReadBytes(nonSecretPayloadLength);
            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(key), 128, nonce, nonSecretPayload);
            cipher.Init(false, parameters);
            var plainText = new byte[cipher.GetOutputSize(cipherText.Length)];
            var len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0);
            try
            {
                cipher.DoFinal(plainText, len);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //  return plainText;
            return Encoding.UTF8.GetString(plainText);
        }
    }

    public string EncryptWithKey(String messageToEncrypt, string FilePath, byte[] nonSecretPayload = null)
    {
        byte[] key = File.ReadAllBytes(FilePath);
        if (string.IsNullOrEmpty(messageToEncrypt))
        {
            throw new ArgumentException("Secret Message Required!", "messageToEncrypt");
        }
        byte[] msgToEncryptByte = Encoding.UTF8.GetBytes(messageToEncrypt);
        //Non-secret Payload Optional
        nonSecretPayload = nonSecretPayload ?? new byte[] { };
        //Using random nonce large enough not to repeat
        byte[] cipherText = null;
        byte[] nonce = null;
        var cipher = new GcmBlockCipher(new AesEngine());
        try
        {
            Random rnd = new Random();
            nonce = new byte[16];
            rnd.NextBytes(nonce);
            var parameters = new AeadParameters(new KeyParameter(key), 128, nonce, nonSecretPayload);
            cipher.Init(true, parameters);
            cipherText = new byte[cipher.GetOutputSize(msgToEncryptByte.Length)];
            var len = cipher.ProcessBytes(msgToEncryptByte, 0, msgToEncryptByte.Length, cipherText, 0);
            cipher.DoFinal(cipherText, len);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error {0}", ex.Message);
            Console.ReadKey();
        }
        //Assemble Message
        using (var combinedStream = new MemoryStream())
        {
            using (var binaryWriter = new BinaryWriter(combinedStream))
            {
                //Prepend Nonce            
                binaryWriter.Write(nonce);
                //Write Cipher Text
                binaryWriter.Write(cipherText);
            }
            return Convert.ToBase64String(combinedStream.ToArray());
        }
    }

    protected string GetSHA256(string name)
    {
        SHA256 SHA256 = new SHA256CryptoServiceProvider();
        byte[] ba = SHA256.ComputeHash(Encoding.Default.GetBytes(name));
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }

    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
    ArrayList size = new ArrayList(); ArrayList outtype = new ArrayList();
    DataTable dsmain = new DataTable();

    void Clear()
    {
        names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
    }

    string _xmlDoc, _IpAddress, _Fee, _fk_regid, _ProductInfo, _Ret_Pk_Tranid, _Pk_TranId;
    int _MisId, _PK_PaymentGatewayId;
    long _PK_PurchaseId;
    public long PK_PurchaseId { get { return _PK_PurchaseId; } set { _PK_PurchaseId = value; } }
    public string ProductInfo { get { return _ProductInfo; } set { _ProductInfo = value; } }
    public int FK_Inter_Coll_Mig_Id { get; set; }
    public int FK_RegId { get; set; }
    public string XmlDoc
    {
        get { return _xmlDoc; }
        set { _xmlDoc = value; }
    }
    public string IpAddress
    {
        get { return _IpAddress; }
        set { _IpAddress = value; }
    }
    public int MisId
    {
        get { return _MisId; }
        set { _MisId = value; }
    }
    public int PK_PaymentGatewayId
    {
        get { return _PK_PaymentGatewayId; }
        set { _PK_PaymentGatewayId = value; }
    }

    public DataSet GetSchema(string TableName)
    {
        try
        {
            Clear();
            return DAobj.GetSchema(TableName);
        }
        catch { throw; }
    }

    public string Pk_TranId { get { return _Pk_TranId; } set { _Pk_TranId = value; } }
    public string RegNo { get; set; }
    public string RefNo { get; set; }
    public string Fk_regid
    {
        get { return _fk_regid; }
        set { _fk_regid = value; }
    }
}