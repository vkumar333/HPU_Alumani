using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using DataAccessLayer;
using SubSonic;
using System.Text.RegularExpressions;
using CuteWebUI;

public partial class Alumni_ALM_AlumniMentorProfileShowAtAdm : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    crypto crp = new crypto();

    public int pk_degreeid { get; private set; }

    protected void Page_PreInit()
    {
        if ((Session["DDOID"] != null || Session["LocationID"] != null) && (Session["UserID"] != null && Session["UserID"].ToString() != "") && (Request.QueryString["id"] != null && Request.QueryString["id"] != ""))
        {
            Page.MasterPageFile = "~/UMM/MasterPage.master";
            int check = int.Parse(alm_alumni_check(Convert.ToInt32(crp.DecodeString(Request.QueryString["id"].ToString()))).GetDataSet().Tables[0].Rows[0][0].ToString());

            if (check < 1)
            {
                Response.Redirect("..//Alumin_Loginpage.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                Session["EmpView_AlumniID"] = Request.QueryString["ID"].ToString();
                Fill_PassingYear();
                FillDepartment();
                Anthem.Manager.IncludePageScripts = true;
                txtAlumniName.Focus();
                FillDegree();
                pk_degreeid = Convert.ToInt32(ddldegree.SelectedValue);
                getsubject(pk_degreeid);
                FillSalution();
                FillForUpdate();
            }
            else
            {
                Response.Redirect("../Modules.aspx");
            }
            // FillCollege();
            // FillDegree();
            //Fill_PassingYear();
            ////   FillDepartment();
            //Anthem.Manager.IncludePageScripts = true;
            //txtAlumniName.Focus();
            //FillForUpdate();
        }
    }

    private void FillDegree()
    {
        DataSet ds = new DataSet();
        ds = ALM_ACD_Degree_Mst_sel();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldegree.DataValueField = "pk_degreeid";
            ddldegree.DataTextField = "degree";
            ddldegree.DataSource = ds;
            ddldegree.DataBind();
            ddldegree.Items.Insert(0, new ListItem("-- Select Degree --", "0"));
        }
        else
        {
            ddldegree.DataSource = null;
            ddldegree.DataBind();
        }
    }

    public StoredProcedure Bind_subject()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_Bind_subject", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_degreeid", pk_degreeid, DbType.Int32);
        return sp;
    }

    public DataSet ALM_ACD_Degree_Mst_sel()
    {
        Clear();
        return DAobj.GetDataSet("ALM_ACD_Degree_Mst_sel", values, names, types);
    }

    private void getsubject(int pk_degreeid)
    {
        DataSet ds = Bind_subject().GetDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            subjectlist.DataValueField = "pk_subjectid";
            subjectlist.DataTextField = "subject";
            subjectlist.DataSource = ds;
            subjectlist.DataBind();
            subjectlist.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
        }
        else
        {
            subjectlist.DataSource = null;
            subjectlist.DataBind();
        }
    }

    private void FillDepartment()
    {
        DataSet ds = new DataSet();
        ds = ALM_Department_Mst_sel();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepartment.DataValueField = "Pk_deptId";
            ddlDepartment.DataTextField = "description";
            ddlDepartment.DataSource = ds;
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "0"));
        }
        else
        {
            ddlDepartment.DataSource = null;
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "0"));
        }
    }

    public DataSet ALM_Department_sel()
    {
        Clear();
        return DAobj.GetDataSet("ALM_Department_sel", values, names, types);
    }

    public DataSet ALM_Department_Mst_sel()
    {
        Clear();
        return DAobj.GetDataSet("ALM_Department_Mst_sel", values, names, types);
    }

    protected void Fill_PassingYear()
    {
        //D_ddlYeofPass.Items.Clear();
        //DataSet ds = IUMSNXG.SP.ALM_Sp_AlumniBatchYear_Passing_Year().GetDataSet();
        //if (ds.Tables[1].Rows.Count > 0)
        //{
        //    D_ddlYeofPass.DataSource = ds.Tables[1];
        //    D_ddlYeofPass.DataTextField = "PassingYear";
        //    D_ddlYeofPass.DataValueField = "PassingYear";
        //    D_ddlYeofPass.DataBind();
        //    D_ddlYeofPass.Items.Insert(0, "-- Select Year of Passing -- ");
        //}
        //else
        //{
        //    D_ddlYeofPass.Items.Insert(0, "-- Select Year of Passing -- ");
        //}

        D_ddlYeofPass.Items.Clear();
        DataSet ds = IUMSNXG.SP.ALM_Sp_AlumniBatchYear_Passing_Year().GetDataSet();
        if (ds != null && ds.Tables[2].Rows.Count > 0)
        {
            D_ddlYeofPass.DataSource = ds.Tables[2];
            D_ddlYeofPass.DataTextField = "PassYear_Name";
            D_ddlYeofPass.DataValueField = "Pk_pass_id";
            D_ddlYeofPass.DataBind();
            D_ddlYeofPass.Items.Insert(0, "-- Select Passing Year -- ");
        }
        else
        {
            D_ddlYeofPass.Items.Insert(0, "-- Select Passing Year -- ");
        }
    }

    protected void FillForUpdate()
    {
        try
        {
            Session["AlumniID"] = Convert.ToInt32(crp.DecodeString(Request.QueryString["id"].ToString()));
            //Convert.ToInt32(Session["EmpView_AlumniID"].ToString());
            //lblMsg.Text = "";
            btnRegister.Text = "UPDATE";
            btnRegister.CommandName = "UPDATE";
            int alumniId = 0;
            if (Session["AlumniID"].ToString() == null || Session["AlumniID"].ToString() == "")
            {
                alumniId = Convert.ToInt32(Session["EmpView_AlumniID"].ToString());
            }

            else
            {
                alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
            }

            DataSet ds = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(alumniId).GetDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //   GetImageDetail(ds.Tables[0]);//change home page image as per latest image
                R_txtAlumnino.Text = ds.Tables[0].Rows[0]["alumnino"].ToString();

                ddlAlmPrefix.SelectedValue = ds.Tables[0].Rows[0]["Alumni_Sal"].ToString();
                txtAlumniName.Text = ds.Tables[0].Rows[0]["alumni_name"].ToString();

                ddlMPrefix.SelectedValue = ds.Tables[0].Rows[0]["Mother_Sal"].ToString();
                R_txtMotherName.Text = ds.Tables[0].Rows[0]["mothername"].ToString();

                ddlFPrefix.SelectedValue = ds.Tables[0].Rows[0]["Father_Sal"].ToString();
                R_txtFatherName.Text = ds.Tables[0].Rows[0]["fathername"].ToString();

                //txtdegree.Text = ds.Tables[0].Rows[0]["fk_degreeid"].ToString();
                //string pk_alumniid = ds.Tables[0].Rows[0]["pk_alumniid"].ToString();
                ddldegree.SelectedValue = ds.Tables[0].Rows[0]["fk_degreeid"].ToString();
                ddldegree.Enabled = false;

                ddldegree_SelectedIndexChanged(null, null);
                subjectlist.SelectedValue = ds.Tables[0].Rows[0]["Fk_subjectid"].ToString();
                subjectlist.Enabled = false;

                FillDepartment();
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["fk_Deptid"].ToString();
                ddlDepartment.Enabled = false;

                //if (ds.Tables[0].Rows[0]["alumnitype"].ToString() == "F")
                //{
                //    TxtDepartment.Text = ds.Tables[0].Rows[0]["fk_Deptid"].ToString();
                //    TxtDepartment.Enabled = true;
                //}
                //else
                //{
                //    TxtDepartment.Enabled = false;
                //}

                ViewState["id"] = ds.Tables[0].Rows[0]["pk_alumniid"].ToString();

                //if (ds.Tables[0].Rows[0]["gender"].ToString() == "M")
                //{
                //    rdbmale.Checked = true;
                //    rdbfemale.Checked = false;
                //}
                //else if (ds.Tables[0].Rows[0]["gender"].ToString() == "F")
                //{
                //    rdbfemale.Checked = true;
                //    rdbmale.Checked = false;
                //}

                if (ds.Tables[0].Rows[0]["gender"].ToString() == "M")
                {
                    rdbGender.SelectedValue = "M";
                }
                else if (ds.Tables[0].Rows[0]["gender"].ToString() == "F")
                {
                    rdbGender.SelectedValue = "F";
                }
                else if (ds.Tables[0].Rows[0]["gender"].ToString() == "O")
                {
                    rdbGender.SelectedValue = "O";
                }

                if (ds.Tables[0].Rows[0]["isDisabled"].ToString() == "Y")
                {
                    rdbIsPersonDisability.SelectedValue = "Y";
                }
                else
                {
                    rdbIsPersonDisability.SelectedValue = "N";
                }

                if (ds.Tables[0].Rows[0]["isDisabled"].ToString() == "Y" && Convert.ToBoolean(ds.Tables[0].Rows[0]["isDisabilityPercentage"].ToString()) == true)
                {
                    isChkDisability.Checked = true;

                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        if (ds.Tables[5].Rows[0]["Files_Unique_Name"] != null && ds.Tables[5].Rows[0]["Files_Unique_Name"].ToString() != "")
                        {
                            ViewState["File3"] = ds.Tables[5].Rows[0]["Files_Unique_Name"].ToString();
                            Session["DDFile"] = ds.Tables[5].Rows[0]["Files_Unique_Name"].ToString();
                            Session["filePath"] = ds.Tables[5].Rows[0]["file_Path"].ToString();
                            lnkDisabilityDoc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
                            lnkDisabilityDoc.CommandName = ds.Tables[5].Rows[0]["Files_Unique_Name"].ToString();
                            lnkDisabilityDoc.Visible = true;
                            getDDocFileName();
                        }
                    }
                    else
                    {
                        Session["DDFile"] = null;
                        Session["filePath"] = null;
                        lnkDisabilityDoc.Text = "";
                        lnkDisabilityDoc.Visible = false;
                    }
                }
                else
                {
                    rdbIsPersonDisability.SelectedValue = "N";
                    Session["DDFile"] = null;
                    Session["filePath"] = null;
                    lnkDisabilityDoc.Text = "";
                    lnkDisabilityDoc.Visible = false;
                }

                rdbIsPersonDisability_SelectedIndexChanged(null, null);

                if (ds.Tables[0].Rows[0]["isDisabled"].ToString() != "" || ds.Tables[0].Rows[0]["isDisabilityPercentage"].ToString() == "true")
                {
                    isChkDisability.Checked = true;
                    txtDisabilityRemark.Text = ds.Tables[0].Rows[0]["disabiltyRemarks"].ToString();
                }
                else
                {
                    isChkDisability.Checked = false;
                    txtDisabilityRemark.Text = "";
                }

                Fill_PassingYear();
                //D_ddlYeofPass.SelectedValue = ds.Tables[0].Rows[0]["yearofpassing"].ToString();
                D_ddlYeofPass.SelectedValue = ds.Tables[0].Rows[0]["fk_pyearid"].ToString();
                E_txtEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
                txtCurrentAddress.Text = ds.Tables[0].Rows[0]["currentaddress"].ToString();
                txtperadd.Text = ds.Tables[0].Rows[0]["per_address"].ToString();
                txtCurrentOccupation.Text = ds.Tables[0].Rows[0]["currentoccupation"].ToString();
                R_txtContactno.Text = ds.Tables[0].Rows[0]["contactno"].ToString();
                txtsplinterest.Text = ds.Tables[0].Rows[0]["special_interest"].ToString();
                txtachievement.Text = ds.Tables[0].Rows[0]["Achievement"].ToString();
                R_txtPostedDate.Text = CommonCode.DateFormats.Date_DBToFront(ds.Tables[0].Rows[0]["dob"].ToString()).Replace("-", "/");
                txtremarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
                // Added by manoj

                txtDesignation.Text = ds.Tables[0].Rows[0]["designation"].ToString();

                chkmentor.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isMentor"]);

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["isMentor"]) == false)
                {
                    chkmentor.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isMentor"]);
                }
                else if (Convert.ToBoolean(ds.Tables[0].Rows[0]["isMentor"]) == true)
                {
                    chkmentor.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isMentor"]);
                    chkmentor.Enabled = false;
                }

                // txttelephoneno.Text = ds.Tables[0].Rows[0]["telephoneno"].ToString();

                string fileName = ""; string contenttype = ""; byte[] fileBytes = null;
                fileName = ds.Tables[1].Rows[0]["Files_Name"].ToString().Trim();
                if (ds.Tables[1].Rows[0]["Files_Name"].ToString().Trim() != "")
                {
                    Session["IsProfilec"] = "1";
                    imgProfileimg.ImageUrl = "https://backoffice.hpushimla.in/FTPSITE/HPU_DOC/Alumni/StuImage/" + ds.Tables[1].Rows[0]["Files_Name"].ToString();
                }
                else {
                    Session["IsProfilec"] = "0";
                    imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";
                    hdPath.Text = "";
                }

                txtCurrentOccupation.Text = ds.Tables[0].Rows[0]["currentoccupation"].ToString();
                R_txtLoginName1.Text = ds.Tables[0].Rows[0]["loginname"].ToString();
                if (ds.Tables[0].Rows[0]["password"].ToString() != "")
                {
                    R_txtPassword.Attributes.Add("value", ds.Tables[0].Rows[0]["password"].ToString());
                    R_txtPassword.Text = ds.Tables[0].Rows[0]["password"].ToString();
                }
                ViewState["File2"] = ds.Tables[2].Rows[0]["Files_Name"].ToString();
                if (ds.Tables[2].Rows[0]["Files_Name"] != null && ds.Tables[2].Rows[0]["Files_Name"].ToString() != "")
                {
                    lnkDoc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
                    lnkDoc.CommandName = ds.Tables[2].Rows[0]["Files_Name"].ToString();
                    lnkDoc.Visible = true;
                    getFileName();
                }
                ViewState["File1"] = ds.Tables[1].Rows[0]["Files_Name"].ToString();
                if (ds.Tables[1].Rows[0]["Files_Name"] != null && ds.Tables[1].Rows[0]["Files_Name"].ToString() != "")
                {
                    lnkprofile.CommandArgument = (ViewState["id"]) == null ? "1" : (ViewState["id"].ToString());
                    lnkprofile.CommandName = ds.Tables[1].Rows[0]["Files_Name"].ToString();
                    lnkprofile.Visible = true;
                    getProfileName();
                }

                //Photo

                //if (fileName != "" || fileName != null)
                //{
                //    //if (ds.Tables[0].Rows[0]["imgattach_p"].ToString().Trim() != "")
                //    if (ds.Tables[1].Rows[0]["Files_Name"].ToString().Trim() != "")
                //    {
                //        //contenttype = ds.Tables[0].Rows[0]["contenttype_p"].ToString().Trim();
                //        // fileBytes = null;// (byte[])ds.Tables[0].Rows[0]["imgattach_p"];
                //        // fileName = Session["RegNo"].ToString() + "_p" + fileName.Substring(fileName.LastIndexOf("."));
                //        setimageonedit(fileName);
                //    }
                //    else
                //    {
                //        imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";
                //        hdPath.Text = "";
                //    }
                //}
                //else
                //{
                //    imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";
                //    hdPath.Text = "";
                //}

                //if (fileName != "")
                //{
                //    contenttype = ds.Tables[1].Rows[0]["FileExtension"].ToString().Trim();
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
                //            showimgPath = upldPath.ToString().Trim();
                //        }
                //    }
                //    imgProfileimg.Src = upldPath + fileName;
                //    imgProfileimg.ImageUrl = upldPath + fileName;
                //    //imgProfileimg.ImageUrl = "https://backoffice.hpushimla.in/FTPSITE/HPU_DOC/Alumni/StuImage/" + fileName;
                //}
                //else
                //{
                //    //imgProfilePhoto.Src = "~/alumni/stuimage/noimage.png";
                //    imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";
                //    hdPath.Text = "";
                //}

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[4].Rows.Count > 0)
                {
                    lblMsgPMode.Text = ds.Tables[4].Rows[0]["PaymentMode"].ToString();
                    lblTransID.Text = ds.Tables[4].Rows[0]["TransactionID"].ToString();
                    lblMsgPAmount.Text = ds.Tables[4].Rows[0]["amount"].ToString();
                    lblTransStatus.Text = ds.Tables[4].Rows[0]["status"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }

    protected void getProfileName()
    {
        string FileName = lnkprofile.CommandName;
        string FileUrl = ReturnPath();
        string FileDisplayName = "";
        string FileRealName = "";
        //if (FileName.Contains("/"))
        //{
        FileDisplayName = FileName;

        FileRealName = "https://backoffice.hpushimla.in/FTPSITE/HPU_DOC/Alumni/StuImage/" + FileName.Substring(FileName.IndexOf("/") + 1);
        //   }
        //FileUrl = FileUrl + FileName;
        lnkprofile.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
    }

    protected void getFileName()
    {
        string FileName = lnkDoc.CommandName;
        string FileUrl = ReturnPath();
        string FileDisplayName = "";
        string FileRealName = "";
        //if (FileName.Contains("/"))
        //{
        FileDisplayName = FileName;

        FileRealName = "https://backoffice.hpushimla.in/FTPSITE/HPU_DOC/Alumni/StuImage/" + FileName.Substring(FileName.IndexOf("/") + 1);
        //   }
        // FileUrl = FileUrl + FileName;
        lnkDoc.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
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

    public void setimageonedit(byte[] fileBytes, string contenttype, string filename)
    {
        string host = HttpContext.Current.Request.Url.Host;
        string upldPath = "";
        string showimgPath = "";
        DataSet dsFilepath = new DataSet();
        dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/IO_Config.xml"));
        foreach (DataRow dr in dsFilepath.Tables[0].Rows)
        {
            if (host == dr["Server_Ip"].ToString().Trim())
            {
                upldPath = dr["Physical_Path"].ToString().Trim();
                upldPath = upldPath + "\\" + "Alumni\\StuImage" + "\\" + filename.ToString().Trim();
                showimgPath = dr["http_Add"].ToString().Trim() + "\\" + "Alumni\\StuImage" + "\\" + filename.ToString().Trim();
                //filepath = upldPath;
            }
        }
    }

    public void setimageonedit(string filename)
    {
        string upldPath = "";
        string upldPathName = "";
        string showimgPath = "";
        string host = HttpContext.Current.Request.Url.Host;
        DataSet dsFilepath = new DataSet();
        dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/IO_Config.xml"));
        foreach (DataRow dr in dsFilepath.Tables[0].Rows)
        {
            if (host == dr["Server_Ip"].ToString().Trim())
            {
                //filepath = dr["http_Add"].ToString().Trim() + "StuImg/";
                //filePathName = dr["Physical_Path"].ToString().Trim() + "/StuImg/";
                //break;

                upldPath = dr["Physical_Path"].ToString().Trim();
                upldPath = upldPath + "\\" + "Alumni\\StuImage" + "\\" + filename.ToString().Trim();
                //showimgPath = dr["http_Add"].ToString().Trim() + "\\" + "Alumni\\StuImage" + "\\" + filename.ToString().Trim();
                //showimgPath = dr["Physical_Path"].ToString().Trim() + "\\" + "Alumni\\StuImage" + "\\" + filename.ToString().Trim();
            }
        }
        //stuimage.ImageUrl = "~/Online/StuImage/" + filename;
        imgProfileimg.ImageUrl = upldPath;
        hdPath.Text = upldPath;
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
        // Anthem.Manager.AddScriptForClientSideEval("alert('" + msg + "');");
    }

    private string getUniqueNo()
    {
        string sId = "";
        try
        {
            string sGuid = System.Guid.NewGuid().ToString();
            int idx = sGuid.LastIndexOf('-');
            sId = (sGuid.Substring(idx + 1)).Substring(0, 6).ToUpper().ToString();
        }
        catch (Exception)
        {
            sId = "";
        }
        return sId;
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = Dobj.GetSchema("ALM_AlumniRegistration");
            DataRow dr = ds.Tables[0].NewRow();
            dr["alumnino"] = R_txtAlumnino.Text.Trim();
            dr["alumni_name"] = txtAlumniName.Text.Trim();
            //  dr["regno"] = 0;// R_txtReg.Text.Trim();
            dr["fk_collegeid"] = 0;
            // dr["fk_collegeid"] = Convert.ToInt32(D_ddlCollege.SelectedValue);
            // dr["Batchyear"] = Convert.ToInt32(D_ddlByear.SelectedValue);
            //dr["fk_degreeid"] = txtdegree.Text.Trim();// Convert.ToInt32(ddldegree.SelectedValue);
            dr["fk_degreeid"] = Convert.ToInt32(ddldegree.SelectedValue);
            dr["Fk_subjectid"] = Convert.ToInt32(subjectlist.SelectedValue);
            dr["yearofpassing"] = Convert.ToInt32(D_ddlYeofPass.SelectedValue);
            dr["fk_pyearid"] = Convert.ToInt32(D_ddlYeofPass.SelectedValue);
            dr["email"] = E_txtEmail.Text.Trim();
            dr["currentaddress"] = txtCurrentAddress.Text.Trim();
            dr["per_address"] = txtperadd.Text.Trim();
            dr["currentoccupation"] = txtCurrentOccupation.Text.Trim();
            dr["contactno"] = R_txtContactno.Text.Trim();
            dr["special_interest"] = txtsplinterest.Text.Trim();
            dr["Achievement"] = txtachievement.Text.Trim();
            dr["remarks"] = txtremarks.Text.Trim();
            //dr["amount"] = txtamt.Text.Trim();
            //dr["receiptno"] = txtreceiptno.Text.Trim();
            dr["dob"] = R_txtPostedDate.Text.Trim();//CommonCode.DateFormats.Date_FrontToDB_R(R_txtPostedDate.Text.Trim());
            // Added By Manoj
            // dr["fk_Deptid"] = Convert.ToInt32(ddldegree.SelectedValue);
            //dr["fk_Deptid"] = TxtDepartment.Text.Trim();
            dr["fk_Deptid"] = Convert.ToInt32(ddlDepartment.SelectedValue);
            dr["designation"] = txtDesignation.Text.Trim();
            dr["mothername"] = R_txtMotherName.Text.Trim();
            dr["fathername"] = R_txtFatherName.Text.Trim();
            //dr["telephoneno"] = txttelephoneno.Text.Trim();
            dr["loginname"] = R_txtAlumnino.Text.Trim();
            dr["password"] = R_txtPostedDate.Text.Trim();//crp.Encrypt(R_txtPassword.Text.Trim());

            //if (rdbmale.Checked == true)
            //{
            //    dr["gender"] = "M";
            //}
            //else if (rdbfemale.Checked == true)
            //{
            //    dr["gender"] = "F";
            //}

            if (rdbGender.SelectedValue.ToString() == "M")
            {
                dr["gender"] = "M";
            }
            else if (rdbGender.SelectedValue.ToString() == "F")
            {
                dr["gender"] = "F";
            }

            dr["Alumni_Sal"] = ddlAlmPrefix.SelectedValue;
            dr["Father_Sal"] = ddlFPrefix.SelectedValue;
            dr["Mother_Sal"] = ddlMPrefix.SelectedValue;

            //Code Added By Santosh sah
            string filepath = ""; string fileName = ""; string contenttype = ""; byte[] fileBytes = null;

            #region" "For Profile Pic"

            Random randomNo = new Random();
            string Message = "";
            string FileType = "";
            double filesize = 0;
            DataRow drimg = null;
            string Fileuniquename = string.Empty;

            //  dsImg = null;

            DataSet dsImg = Dobj.GetSchema("ALM_AlumniRegistration_File_dtl");
            dsImg.Tables[0].TableName = "ALM_AlumniRegistration_File_dtl";

            if (flUpload.HasFile == true)
            {
                filesize = flUpload.PostedFile.ContentLength;
                string Name = flUpload.PostedFile.FileName;

                //filesize = Math.Round((filesize / 1024), 0);

                if (Name.Length > 100)
                {
                    ClientMessaging("Photo File Name Should not be more than 50 characters!");
                    flUpload.Focus();
                    return;
                }

                //if (filesize > 800)
                //{
                //    ClientMessaging("Photo size is " + filesize.ToString("0") + " kb, it should not be more than 50 kb !");
                //    return;
                //}

                if (filesize > (1 * 1024 * 1024))
                {
                    ClientMessaging("Please upload photo of upto 1MB size only.");
                    flUpload.Focus();
                    return;
                }

                FileType = Path.GetExtension(flUpload.PostedFile.FileName);
                //if (FileType != null)
                //{
                //if (FileType != ".jpg" && FileType != ".JPG" && FileType != ".jpeg" && FileType != ".JPEG"
                //&& FileType != ".pjpeg" && FileType != ".PJPEG" && FileType != ".bmp" && FileType != ".BMP"
                //&& FileType != ".gif" && FileType != ".GIF" && FileType != ".png" && FileType != ".PNG")
                //{
                //    ClientMessaging("Photo is not in proper format!");
                //    return;
                //}

                UploadFiles();//to get the physical path of server 
                string upldPath = "";
                string currDir = System.IO.Directory.GetCurrentDirectory();
                upldPath = this.upldPath;

                drimg = dsImg.Tables[0].NewRow();

                //drimg["Fk_Regid"] = Convert.ToInt64(Session["regid"].ToString());

                drimg["IsProfilePicOrDoc"] = 1;
                //string filetype = aItem.FileName.Substring(aItem.FileName.LastIndexOf("."));
                drimg["Files_Name"] = Name;
                drimg["Files_Unique_Name"] = "HPU_Alumni_PP_" + "_" + Guid.NewGuid().ToString() + "_" + randomNo.Next(50000, 1000000) + "_" +
                    DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PP" + "_" + randomNo.Next(33333, 454545454) + "_" + randomNo.Next(999999, 15215454) + FileType;
                Fileuniquename = "HPU_Alumni_PP_" + "_" + Guid.NewGuid().ToString() + "_" + randomNo.Next(50000, 1000000) + "_" +
                   DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PP" + "_" + randomNo.Next(33333, 454545454) + "_" + randomNo.Next(999999, 15215454) + FileType;
                drimg["FileExtension"] = FileType;
                drimg["File_Path"] = upldPath + drimg["Files_Unique_Name"].ToString();

                // save only file unique name so that we can upload this seperately
                dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds

                bool IsExistPath = System.IO.Directory.Exists(upldPath);
                if (!IsExistPath)
                    System.IO.Directory.CreateDirectory(upldPath);
                flUpload.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
            }
            //  }
            else
            {
                //get pic details from viewstate
                //if (ViewState["StudentPicDtls"] != null)
                //{
                //    DataTable dt = ViewState["StudentPicDtls"] as DataTable;
                //    if (dt.Rows.Count > 0)
                //    {
                //        var PP = dt.AsEnumerable()
                //                .Where(r => r.Field<long>("Fk_Regid") == Convert.ToInt64(Session["regid"].ToString()) && r.Field<string>("Img_Type") == "PP").SingleOrDefault();
                //        if (PP != null)
                //        {
                //            drimg = dsImg.Tables[0].NewRow();
                //            drimg["Fk_Regid"] = Convert.ToInt64(Session["regid"].ToString());
                //            drimg["Img_Type"] = "PP";
                //            drimg["Files_Name"] = PP[4].ToString();
                //            drimg["Files_Unique_Name"] = PP[5].ToString(); ;
                //            drimg["FileExtension"] = PP[6].ToString(); ;
                //            drimg["File_Path"] = PP[7].ToString();
                //            dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds
                //        }
                //    }
                //}

                if (ViewState["id"] != null)
                {
                    DataSet dsS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

                    if (dsS != null && dsS.Tables[1].Rows.Count > 0)
                    {
                        drimg = dsImg.Tables[0].NewRow();
                        drimg["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
                        drimg["IsProfilePicOrDoc"] = dsS.Tables[1].Rows[0]["IsProfilePicOrDoc"].ToString();
                        drimg["Files_Name"] = dsS.Tables[1].Rows[0]["Files_Name"].ToString();
                        drimg["Files_Unique_Name"] = dsS.Tables[1].Rows[0]["Files_Unique_Name"].ToString(); ;
                        drimg["FileExtension"] = dsS.Tables[1].Rows[0]["FileExtension"].ToString(); ;
                        drimg["File_Path"] = dsS.Tables[1].Rows[0]["File_Path"].ToString();
                        dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds
                                                        //}
                    }
                }

            }
            //}
            //else
            //{
            //    dr["Files_Name"] = (string)ViewState["File1"].ToString();
            //}
            #endregion

            #region "Document Upload"

            if (uploadDocuments.HasFile == true)
            {

                filesize = uploadDocuments.PostedFile.ContentLength;
                string DocumentName = uploadDocuments.PostedFile.FileName;

                //filesize = Math.Round((filesize / 1024), 0);

                if (DocumentName.Length > 100)
                {
                    ClientMessaging("HPU Document File Name Should not be more than 50 characters!");
                    uploadDocuments.Focus();
                    return;
                }

                //if (filesize > 5120)
                //{
                //    ClientMessaging("HPU Document size is " + filesize.ToString("0") + " kb, it should not be more than 5120 kb !");
                //    return;
                //}

                if (filesize > (2 * 1024 * 1024))
                {
                    ClientMessaging("Please upload HPU document file upto 2MB size only.");
                    uploadDocuments.Focus();
                    return;
                }

                FileType = Path.GetExtension(uploadDocuments.PostedFile.FileName);

                if (FileType != null)
                {
                    if (FileType != ".pdf" && FileType != ".PDF")
                    {
                        ClientMessaging("HPU Document Should be in pdf format!");
                        uploadDocuments.Focus();
                        return;
                    }
                }

                drimg = dsImg.Tables[0].NewRow();
                //drFile = dtFileDtl.NewRow();

                // drimg["Fk_Regid"] = Convert.ToInt64(Session["regid"].ToString());

                // drimg["Img_Type"] = "DOC";
                drimg["IsProfilePicOrDoc"] = 0;
                UploadFiles();//to get the physical path of server 

                string upldPath = "";
                string currDir = System.IO.Directory.GetCurrentDirectory();
                upldPath = this.upldPath;

                Random randomNo1 = new Random();


                drimg["Files_Name"] = DocumentName;
                drimg["Files_Unique_Name"] = "HPU_Alumni_DOC_" + "_" + Guid.NewGuid().ToString() + "_" + randomNo1.Next(50000, 1000000) + "_" +
                    DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PH" + "_" + randomNo1.Next(33333, 454545454) + "_" + randomNo1.Next(999999, 15215454) + FileType;
                drimg["FileExtension"] = FileType;
                drimg["File_Path"] = upldPath + drimg["Files_Unique_Name"].ToString();
                //drFile["Files_Unique_Name"] = drimg["Files_Unique_Name"].ToString();// to upload on server seperately


                dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds

                bool IsExistPath = System.IO.Directory.Exists(upldPath);
                if (!IsExistPath)
                    System.IO.Directory.CreateDirectory(upldPath);
                uploadDocuments.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
            }

            else
            {
                // dr["Files_Name"] = (string)ViewState["File2"].ToString();
                //if (ViewState["StudentPicDtls"] != null)
                //{
                //    DataTable dt = ViewState["StudentPicDtls"] as DataTable;
                //    if (dt.Rows.Count > 0)
                //    {
                //        var PH = dt.AsEnumerable()
                //                    .Where(r => r.Field<long>("Fk_Regid") == Convert.ToInt64(Session["regid"].ToString()) && r.Field<string>("Img_Type") == "PH").SingleOrDefault();
                //        if (PH != null)
                //        {
                //            drimg = dsImg.Tables[0].NewRow();
                //            drimg["Fk_Regid"] = Convert.ToInt64(Session["regid"].ToString());
                //            drimg["Img_Type"] = "PH";
                //            drimg["Files_Name"] = PH[4].ToString();
                //            drimg["Files_Unique_Name"] = PH[5].ToString(); ;
                //            drimg["FileExtension"] = PH[6].ToString(); ;
                //            drimg["File_Path"] = PH[7].ToString();
                //            dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds
                //        }
                //    }
                //}

                if (ViewState["id"] != null)
                {
                    DataSet dsSS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

                    if (dsSS != null && dsSS.Tables[2].Rows.Count > 0)
                    {
                        //var PH = dsSS.Tables[2].AsEnumerable()
                        //        .Where(r => r.Field<long>("Fk_alumniid") == Convert.ToInt64(ViewState["id"].ToString()) && r.Field<string>("IsProfilePicOrDoc") == "0").SingleOrDefault();
                        //if (PH != null)
                        //{
                        drimg = dsImg.Tables[0].NewRow();
                        drimg["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
                        drimg["IsProfilePicOrDoc"] = dsSS.Tables[2].Rows[0]["IsProfilePicOrDoc"].ToString();
                        drimg["Files_Name"] = dsSS.Tables[2].Rows[0]["Files_Name"].ToString();
                        drimg["Files_Unique_Name"] = dsSS.Tables[2].Rows[0]["Files_Unique_Name"].ToString(); ;
                        drimg["FileExtension"] = dsSS.Tables[2].Rows[0]["FileExtension"].ToString(); ;
                        drimg["File_Path"] = dsSS.Tables[2].Rows[0]["File_Path"].ToString();
                        dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds
                        //}
                    }
                }

            }

            #endregion

            #region "Disability Document Upload"

            if (fileDisabilityDoc.HasFile == true && checkValidDocOrNot())
            {
                //filesize = uploadDocuments.PostedFile.ContentLength;
                string DocumentName = fileDisabilityDoc.PostedFile.FileName;
                ///filesize = Math.Round((filesize / 1024), 0);

                FileType = Path.GetExtension(fileDisabilityDoc.PostedFile.FileName);

                drimg = dsImg.Tables[0].NewRow();
                drimg["IsProfilePicOrDoc"] = 0;
                UploadFiles();//to get the physical path of server 

                string upldPath = "";
                string currDir = System.IO.Directory.GetCurrentDirectory();
                upldPath = this.upldPath;
                Random randomNo1 = new Random();
                drimg["Files_Name"] = DocumentName;
                drimg["Files_Unique_Name"] = "HPU_Alumni_DD_" + "_" + Guid.NewGuid().ToString() + "_" + randomNo1.Next(50000, 1000000) + "_" +
                    DateTime.Now.ToString("yyyyMMddHHmmssfff") + "DD" + "_" + randomNo1.Next(33333, 454545454) + "_" + randomNo1.Next(999999, 15215454) + FileType;
                drimg["FileExtension"] = FileType;
                drimg["File_Path"] = upldPath;
                drimg["FilesFor"] = "DD";
                dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds
                bool IsExistPath = System.IO.Directory.Exists(upldPath);
                if (!IsExistPath)
                    System.IO.Directory.CreateDirectory(upldPath);
                fileDisabilityDoc.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
            }
            else
            {
                if (ViewState["id"] != null)
                {
                    DataSet dsSS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

                    if (dsSS != null && dsSS.Tables[2].Rows.Count > 0)
                    {
                        drimg = dsImg.Tables[0].NewRow();
                        drimg["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
                        drimg["IsProfilePicOrDoc"] = dsSS.Tables[2].Rows[0]["IsProfilePicOrDoc"].ToString();
                        drimg["Files_Name"] = dsSS.Tables[2].Rows[0]["Files_Name"].ToString();
                        drimg["Files_Unique_Name"] = dsSS.Tables[2].Rows[0]["Files_Unique_Name"].ToString(); ;
                        drimg["FileExtension"] = dsSS.Tables[2].Rows[0]["FileExtension"].ToString(); ;
                        drimg["File_Path"] = dsSS.Tables[2].Rows[0]["File_Path"].ToString();
                        drimg["FilesFor"] = "DD";
                        dsImg.Tables[0].Rows.Add(drimg);
                    }
                }
            }

            #endregion

            //}

            ds.Tables[0].Rows.Add(dr);
            if (dsImg != null)
            {
                ds.Merge(dsImg);
            }

            //string Message = "";
            ArrayList Result = new ArrayList();
            //IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
            string XmlDoc1 = ds.GetXml();


            //string xmlstr = ds.GetXml();
            int AlumniI = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Upd(Convert.ToInt32(ViewState["id"]), XmlDoc1, fileBytes).Execute();

            if (AlumniI > 0)
            {
                lblMsg.Text = "Record Updated Successfully!";
                ClientMessaging("Record Update Successfully");
                //clear1();
                FillForUpdate();
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

    public static StoredProcedure alm_alumni_check(int pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_alumni_check", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
        //return 0;
    }

    DataAccess DAobj = new DataAccess();
    ArrayList size = new ArrayList(); ArrayList outtype = new ArrayList();

    void Clear()
    {
        names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
    }

    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

    protected void E_txtEmail_TextChanged(object sender, EventArgs e)
    {
        lblEmailMsg.Text = "";
        if (E_txtEmail.Text.Trim() != "")
        {
            DataSet ds = IUMSNXG.SP.ALM_SP_Check_Duplicate_Email_AtUpdate(Convert.ToInt32(Session["AlumniId"].ToString()), E_txtEmail.Text.Trim()).GetDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["IsExists"].ToString() == "1")
                {
                    lblEmailMsg.Text = "";
                    E_txtEmail.Text = "";
                    ClientMessaging("Email Id Already Exists!");
                    Anthem.Manager.IncludePageScripts = true;
                    E_txtEmail.Focus();
                    return;
                }
            }
        }
    }

    protected bool IsEmailIdValid(string EmailId)
    {
        string strEmailReg = @"^([\w-\.]+)@((\[[0-9]{2,3}\.[0-9]{2,3}\.[0-9]{2,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{2,3})(\]?)$";
        if (Regex.IsMatch(EmailId, strEmailReg))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    string upldPath = "";
    DataSet dsFile = null;
    public string ApprovedStatus { get; private set; }
    public object alumniId { get; private set; }
    public string userID { get; set; }
    public bool isMentor { get; private set; }

    protected void Assign_Uploaded_File_Dts(CuteWebUI.UploadAttachments Fl_UploadFiles, bool IsProfilePicOrDoc)
    {
        dsFile = null;
        dsFile = Dobj.GetSchema("ALM_AlumniRegistration_File_dtl");
        dsFile.Tables[0].TableName = "ALM_AlumniRegistration_File_dtl";

        DataRow drf;
        int J = Fl_UploadFiles.Items.Count;
        if (Fl_UploadFiles.Items.Count > 0)
        {
            for (int i = 0; i < Fl_UploadFiles.Items.Count; i++)
            {
                J--;
                UploadFiles();
                drf = dsFile.Tables[0].NewRow();
                AttachmentItem aItem = Fl_UploadFiles.Items[i];

                string filetype = aItem.FileName.Substring(aItem.FileName.LastIndexOf("."));

                drf["Fk_alumniid"] = 0;
                drf["IsProfilePicOrDoc"] = IsProfilePicOrDoc == true ? 1 : 0;
                drf["Files_Name"] = aItem.FileName;
                drf["Files_Unique_Name"] = "File_Alumni" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + i + "_" + J + "_" + filetype;
                drf["FileExtension"] = filetype;
                drf["File_Path"] = upldPath + drf["Files_Unique_Name"].ToString();
                dsFile.Tables[0].Rows.Add(drf);
            }
            ViewState["FileDtl"] = null;
            ViewState["FileDtl"] = dsFile;
        }
    }

    protected void UploadFiles()
    {
        string host = HttpContext.Current.Request.Url.Host;
        // string upldPath = "";
        DataSet dsFilepath = new DataSet();
        dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
        foreach (DataRow dr in dsFilepath.Tables[0].Rows)
        {
            if (host == dr["Server_Ip"].ToString().Trim())
            {
                upldPath = dr["Physical_Path"].ToString().Trim();
                upldPath = upldPath + "/" + "Alumni\\StuImage" + "/";// +"\\" + FileName;
                break;
            }
        }
    }

    void UploadDocumentFiles(DataTable dt, string upldPath, CuteWebUI.UploadAttachments Fl_UploadFiles)
    {
        string currDir = System.IO.Directory.GetCurrentDirectory();
        upldPath = this.upldPath;
        bool IsExistPath = System.IO.Directory.Exists(upldPath);
        if (!IsExistPath)
            System.IO.Directory.CreateDirectory(upldPath);
        string filename = "";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            filename = dt.Rows[i]["Files_Unique_Name"].ToString();
            AttachmentItem aItem = Fl_UploadFiles.Items[i];
            aItem.CopyTo(upldPath + filename);
        }
    }

    //it will return file based on file unique name
    public string SetServiceDoc(string FileName)
    {
        string FolderName = @"/Alumni";
        string host = HttpContext.Current.Request.Url.Host;
        string FilePath = "";
        DataSet dsFilepath = new DataSet();
        dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
        foreach (DataRow dr in dsFilepath.Tables[0].Rows)
        {
            if (host == dr["Server_Ip"].ToString().Trim())
            {
                if (host != "localhost")
                {
                    FilePath = dr["http_Add"].ToString().Trim();
                    FilePath = @FilePath + FolderName + @"/" + FileName;
                    // FilePath = FolderName + FileName;
                }
                else
                {
                    FilePath = dr["Physical_Path"].ToString().Trim();
                    FilePath = @FilePath + FolderName + @"/" + FileName;
                    //  FilePath = FolderName  + FileName;
                }
                //return FolderName+FileName;
                return FilePath;
            }
        }
        return FilePath;
    }

    protected void R_txtContactno_TextChanged(object sender, EventArgs e)
    {
        lblMobleNoMsg.Text = "";
        if (R_txtContactno.Text.Trim() != "")
        {
            DataSet ds = IUMSNXG.SP.ALM_SP_GetDuplicate_Email_or_MobileNo("", R_txtContactno.Text.Trim()).GetDataSet();
            if (ds.Tables[1].Rows.Count > 0)
            {
                lblMobleNoMsg.Text = "Already Exists!";
                R_txtContactno.Text = "";
                Anthem.Manager.IncludePageScripts = true;
                R_txtContactno.Focus();
                return;
            }
        }
    }

    //public static StoredProcedure AlumniAprInsert()
    //{
    //    SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_Approvand_reject_Insert", DataService.GetInstance("IUMSNXG"), "");
    //    sp.Command.AddParameter("@isapproved", isapproved, DbType.String);
    //    return sp;
    //}

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    //private DataSet AlumniMentorAprInsert()
    //{
    //    ClearArrayLists();
    //    names.Add("@isMentor"); values.Add(isMentor); types.Add(SqlDbType.Bit);
    //    names.Add("@pk_alumniid"); values.Add(alumniId); types.Add(SqlDbType.Int);
    //    names.Add("@userID"); values.Add(userID); types.Add(SqlDbType.NVarChar);
    //    return Dobj.GetDataSet("ALM_Alumni_Mentor_Approval_Ins", values, names, types);
    //}

    public static StoredProcedure ALM_Alumni_Mentor_Approval(bool isMentor, int AlumniId, string UserID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Alumni_Mentor_Approval_Ins", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@isMentor", isMentor, DbType.Boolean);
        sp.Command.AddParameter("@pk_alumniid", AlumniId, DbType.Int32);
        sp.Command.AddParameter("@userID", UserID, DbType.String);
        return sp;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            //ApprovedStatus = rbtnlst_approveRej.SelectedValue;
            isMentor = chkmentor.Checked;
            alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
            userID = Session["UserID"].ToString().Trim();
            //ds = AlumniMentorAprInsert();
            ArrayList Result = new ArrayList();

            if (ALM_Alumni_Mentor_Approval(isMentor, (int)alumniId, userID).Execute() > 0)
            {
                lblMsg.Text = "Record Approved Successfully!";
                ClientMessaging("Record Approved Successfully");
                Clear();
            }
            else
            {
                Response.Redirect("Approve_Mentor_Alumni_At_Admin.aspx");
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Approve_Mentor_Alumni_At_Admin.aspx");
    }

    protected void FillSalution()
    {
        DataSet ds = ALM_ALM_ACD_Salutation_fill().GetDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlAlmPrefix.DataValueField = "PK_Salutation_ID";
            ddlAlmPrefix.DataTextField = "Salutation_Name";
            ddlAlmPrefix.DataSource = ds;
            ddlAlmPrefix.DataBind();
            ddlAlmPrefix.Items.Insert(0, new ListItem("- Title -", "0"));

            ddlMPrefix.DataValueField = "PK_Salutation_ID";
            ddlMPrefix.DataTextField = "Salutation_Name";
            ddlMPrefix.DataSource = ds;
            ddlMPrefix.DataBind();
            ddlMPrefix.Items.Insert(0, new ListItem("- Title -", "0"));

            ddlFPrefix.DataTextField = "Salutation_Name";
            ddlFPrefix.DataValueField = "PK_Salutation_ID";
            ddlFPrefix.DataSource = ds;
            ddlFPrefix.DataBind();
            ddlFPrefix.Items.Insert(0, new ListItem("- Title -", "0"));
        }
    }

    public StoredProcedure ALM_ALM_ACD_Salutation_fill()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_ACD_Salutation_Sel", DataService.GetInstance("IUMSNXG"), "");
        //sp.Command.AddParameter("@C_id", C_id, DbType.Int32);
        return sp;
    }

    protected void rdbGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbGender.SelectedValue.ToString() == "M")
        {
            rdbGender.SelectedValue = "M";
        }
        else if (rdbGender.SelectedValue.ToString() == "F")
        {
            rdbGender.SelectedValue = "F";
        }
        else if (rdbGender.SelectedValue.ToString() == "F")
        {
            rdbGender.SelectedValue = "O";
        }
    }

    protected void ddldegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        pk_degreeid = Convert.ToInt32(ddldegree.SelectedValue);
        getsubject(pk_degreeid);
    }

    string FileType = "";
    double filesize = 0;
    DataRow drimg = null;
    string Fileuniquename = string.Empty;

    protected void btnDisabilityDoc_Click(object sender, EventArgs e)
    {
        if (fileDisabilityDoc.HasFile)
        {
            FileType = Path.GetExtension(fileDisabilityDoc.PostedFile.FileName);
            if (FileType != ".pdf" && FileType != ".PDF" && FileType != ".jpeg" && FileType != ".JPEG" && FileType != ".jpg" && FileType != ".JPG" && FileType != ".png")
            {
                ClientMessaging("This should be in pdf, jpeg, jpg!");
                return;
            }
            else
            {
                string lFileName = "";
                Random rand = new Random();
                int n = rand.Next();

                string filename = "HPU_Alumni_DD_" + "_" + Guid.NewGuid().ToString() + "_" + rand.Next(50000, 1000000) + "_" +
                DateTime.Now.ToString("yyyyMMddHHmmssfff") + "DD" + "_" + rand.Next(33333, 454545454) + "_" + rand.Next(999999, 15215454) + FileType;

                lFileName = FU_physicalPath(fileDisabilityDoc, "\\Alumni\\StuImage\\", filename);
                Session["ref_Filename"] = lFileName;
                fileDisabilityDoc.SaveAs(Server.MapPath("ALM_Reports//" + lFileName));
                ClientMessaging("file uploaded Successfully..");
                string filePathforhyperlink = "ALM_Reports//" + Session["ref_Filename"].ToString();
                string filePath = Server.MapPath("ALM_Reports/") + Session["ref_Filename"].ToString();
                Session["filePath_refletter_delete"] = filePath;
                lnkDisabilityDoc.Visible = true;
                //lnkDisabilityDoc.href = filePathforhyperlink;
                lnkDisabilityDoc.Text = fileDisabilityDoc.FileName;
            }
        }
        else
        {
            ClientMessaging("Please Upload Special Disabilities Documents.!!!");
        }
    }

    protected void rdbIsPersonDisability_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbIsPersonDisability.SelectedValue == "Y")
        {
            isChkDisabilityPnl.Visible = true;
            disabilitySection.Visible = true;
            Anthem.Manager.IncludePageScripts = true;
            isChkDisability_CheckedChanged(null, null);

        }
        else if (rdbIsPersonDisability.SelectedValue == "N")
        {
            isChkDisabilityPnl.Visible = false;
            disabilitySection.Visible = false;
            isChkDisability.Checked = false;
            txtDisabilityRemark.Text = "";
            Anthem.Manager.IncludePageScripts = true;
            isChkDisability_CheckedChanged(null, null);

        }
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void isChkDisability_CheckedChanged(object sender, EventArgs e)
    {
        if (isChkDisability.Checked == true && rdbIsPersonDisability.SelectedValue == "Y")
        {
            Anthem.Manager.IncludePageScripts = true;
            isChkDisabilityPnl.Visible = true;
            disabilityRemarkSection.Style["display"] = "block";
            isChkDisability.Checked = true;
            //disabilityDocuments.Visible = true;
            fileDisabilityDoc.Visible = true;
            btnDisabilityDoc.Visible = true;
            //divDisabilityDoc.Style["display"] = "block";
            // lblOnlineFees.Text = "0";
            Session["RegFees"] = "0";
            //hdnId.Value = "0";
        }
        else
        {
            Anthem.Manager.IncludePageScripts = true;
            disabilityRemarkSection.Style["display"] = "none";
            isChkDisability.Checked = false;
            //disabilityDocuments.Visible = false;
            fileDisabilityDoc.Visible = false;
            btnDisabilityDoc.Visible = false;
            //divDisabilityDoc.Style["display"] = "none";
            txtDisabilityRemark.Text = "";
            //FeeDetails();

            if (Session["filePath_refletter_delete"] != null && Session["filePath_refletter_delete"].ToString() != "")
            {
                // Check if the file exists before attempting to delete it
                string filePathToDelete = Session["filePath_refletter_delete"].ToString();
                if (System.IO.File.Exists(filePathToDelete))
                {
                    try
                    {
                        System.IO.File.Delete(filePathToDelete);
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = "Error deleting file: " + ex.Message;
                    }
                }
            }
            lnkDisabilityDoc.Text = "";
        }
        Anthem.Manager.IncludePageScripts = true;
    }
    public string FU_physicalPath(Anthem.FileUpload flu, string FolderName, string FileName)
    {
        try
        {
            if (flu.PostedFile != null && flu.HasFile && flu.PostedFile.ContentLength > 0)
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
                        upldPath = upldPath + "Alumni\\StuImage\\";
                        Session["filepath"] = upldPath;
                        // upldPath = upldPath + "\\" + FolderName + "\\" + FileName;
                        upldPath = upldPath + FileName;
                        flu.SaveAs(upldPath);
                        return FileName;
                    }
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            throw new Exception("Upload Fails!");
        }
    }

    private bool checkValidDocOrNot()
    {
        if (!uploadDocuments.HasFile)
        {
            ClientMessaging("Please upload the required HPU documents.!!!");
            uploadDocuments.Focus();
            return false;
        }

        double filesizes = uploadDocuments.PostedFile.ContentLength;
        string Names = uploadDocuments.PostedFile.FileName;
        //filesizes = Math.Round((filesizes / 1024), 0);

        if (Names.Length > 100)
        {
            ClientMessaging("Documents File Name Should not be more than 100 characters!");
            uploadDocuments.Focus();
            return false;
        }
        if (filesizes > (2 * 1024 * 1024))
        {
            ClientMessaging("HPU Document size should not be more than 2 MB !");
            uploadDocuments.Focus();
            return false;
        }

        string FileTypes = uploadDocuments.PostedFile.FileName != "" ? Path.GetExtension(uploadDocuments.PostedFile.FileName) : System.IO.Path.GetExtension(ViewState["File2"].ToString());

        switch (FileTypes.ToLower())
        {
            case ".jpg":
                return true;
            case ".jpeg":
                return true;
            case ".png":
                return true;
            case ".pdf":
                return true;
            case ".bmp":
                return true;
            case ".gif":
                return true;
            default:
                ClientMessaging("Only files .jpg, .jpeg, .png, .pdf, .bmp, and .gif extension are allowed.");
                return false;
        }
        return true;
    }

    protected void getDDocFileName()
    {
        string FileName = lnkDisabilityDoc.CommandName;
        string FileUrl = ReturnPath();
        string FileDisplayName = "";
        string FileRealName = "";
        //if (FileName.Contains("/"))
        //{
        FileDisplayName = FileName;
        FileRealName = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/" + FileName.Substring(FileName.IndexOf("/") + 1);
        //   }
        // FileUrl = FileUrl + FileName;
        lnkDisabilityDoc.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
    }
}