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
using System.Data.SqlClient;

public partial class Alumni_ALM_AlumniProfileApprovedbyadm : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    crypto cpt = new crypto();

    public int pk_degreeid { get; private set; }
    public string alumniType { get; private set; }
    public string memType { get; private set; }

    protected void Page_PreInit()
    {
        if ((Session["DDOID"] != null || Session["LocationID"] != null) && (Session["UserID"] != null && Session["UserID"].ToString() != "") && (Request.QueryString["id"] != null && Request.QueryString["id"] != ""))
        {
            Page.MasterPageFile = "~/UMM/MasterPage.master";
            int check = int.Parse(alm_alumni_check(int.Parse(cpt.DecodeString(Request.QueryString["id"]))).GetDataSet().Tables[0].Rows[0][0].ToString());
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
                int pk_alumniid = Convert.ToInt32(cpt.DecodeString(Request.QueryString["ID"].ToString()));
                Session["EmpView_AlumniID"] = pk_alumniid;

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
        ds = ALM_Department_sel();
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
        }
    }

    protected void Fill_PassingYear()
    {
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
            ////Session["AlumniID"] = Convert.ToInt32(Session["EmpView_AlumniID"].ToString());
            //////lblMsg.Text = "";
            ////btnRegister.Text = "UPDATE";
            ////btnRegister.CommandName = "UPDATE";
            ////int alumniId = 0;
            ////if (Session["AlumniID"].ToString() == null || Session["AlumniID"].ToString() == "")
            ////{
            ////    alumniId = Convert.ToInt32(Session["EmpView_AlumniID"].ToString());
            ////}

            ////else
            ////{
            ////    alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
            ////}

            ////DataSet ds = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(alumniId).GetDataSet();

            ////if (ds.Tables[0].Rows.Count > 0)
            ////{
            ////    //   GetImageDetail(ds.Tables[0]);//change home page image as per latest image
            ////    R_txtAlumnino.Text = ds.Tables[0].Rows[0]["alumnino"].ToString();

            ////    ddlAlmPrefix.SelectedValue = ds.Tables[0].Rows[0]["Alumni_Sal"].ToString();
            ////    txtAlumniName.Text = ds.Tables[0].Rows[0]["alumni_name"].ToString();

            ////    ddlMPrefix.SelectedValue = ds.Tables[0].Rows[0]["Mother_Sal"].ToString();
            ////    R_txtMotherName.Text = ds.Tables[0].Rows[0]["mothername"].ToString();

            ////    ddlFPrefix.SelectedValue = ds.Tables[0].Rows[0]["Father_Sal"].ToString();
            ////    R_txtFatherName.Text = ds.Tables[0].Rows[0]["fathername"].ToString();

            ////    //txtdegree.Text = ds.Tables[0].Rows[0]["fk_degreeid"].ToString();
            ////    //string pk_alumniid = ds.Tables[0].Rows[0]["pk_alumniid"].ToString();
            ////    ddldegree.SelectedValue = ds.Tables[0].Rows[0]["fk_degreeid"].ToString();
            ////    ddldegree.Enabled = false;

            ////    ddldegree_SelectedIndexChanged(null, null);
            ////    subjectlist.SelectedValue = ds.Tables[0].Rows[0]["Fk_subjectid"].ToString();
            ////    subjectlist.Enabled = false;

            ////    FillDepartment();
            ////    ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["fk_Deptid"].ToString();
            ////    ddlDepartment.Enabled = false;

            ////    //if (ds.Tables[0].Rows[0]["alumnitype"].ToString() == "F")
            ////    //{
            ////    //    TxtDepartment.Text = ds.Tables[0].Rows[0]["fk_Deptid"].ToString();
            ////    //    TxtDepartment.Enabled = true;
            ////    //}
            ////    //else
            ////    //{
            ////    //    TxtDepartment.Enabled = false;
            ////    //}

            ////    ViewState["id"] = ds.Tables[0].Rows[0]["pk_alumniid"].ToString();

            ////    //if (ds.Tables[0].Rows[0]["gender"].ToString() == "M")
            ////    //{
            ////    //    rdbmale.Checked = true;
            ////    //    rdbfemale.Checked = false;
            ////    //}
            ////    //else if (ds.Tables[0].Rows[0]["gender"].ToString() == "F")
            ////    //{
            ////    //    rdbfemale.Checked = true;
            ////    //    rdbmale.Checked = false;
            ////    //}

            ////    if (ds.Tables[0].Rows[0]["gender"].ToString() == "M")
            ////    {
            ////        rdbGender.SelectedValue = "M";
            ////    }
            ////    else if (ds.Tables[0].Rows[0]["gender"].ToString() == "F")
            ////    {
            ////        rdbGender.SelectedValue = "F";
            ////    }

            ////    if (ds.Tables[0].Rows[0]["isDisabled"].ToString() == "Y")
            ////    {
            ////        rdbIsPersonDisability.SelectedValue = "Y";
            ////    }
            ////    else
            ////    {
            ////        rdbIsPersonDisability.SelectedValue = "N";
            ////    }

            ////    if (ds.Tables[0].Rows[0]["isDisabled"].ToString() == "Y" && Convert.ToBoolean(ds.Tables[0].Rows[0]["isDisabilityPercentage"].ToString()) == true)
            ////    {
            ////        isChkDisability.Checked = true;

            ////        if (ds.Tables[5].Rows.Count > 0)
            ////        {
            ////            if (ds.Tables[5].Rows[0]["Files_Unique_Name"] != null && ds.Tables[5].Rows[0]["Files_Unique_Name"].ToString() != "")
            ////            {
            ////                ViewState["File3"] = ds.Tables[5].Rows[0]["Files_Unique_Name"].ToString();
            ////                Session["DDFile"] = ds.Tables[5].Rows[0]["Files_Unique_Name"].ToString();
            ////                Session["filePath"] = ds.Tables[5].Rows[0]["file_Path"].ToString();
            ////                lnkDisabilityDoc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
            ////                lnkDisabilityDoc.CommandName = ds.Tables[5].Rows[0]["Files_Unique_Name"].ToString();
            ////                lnkDisabilityDoc.Visible = true;
            ////                getDDocFileName();
            ////            }
            ////        }
            ////        else
            ////        {
            ////            Session["DDFile"] = null;
            ////            Session["filePath"] = null;
            ////            lnkDisabilityDoc.Text = "";
            ////            lnkDisabilityDoc.Visible = false;
            ////        }
            ////    }
            ////    else
            ////    {
            ////        rdbIsPersonDisability.SelectedValue = "N";
            ////        Session["DDFile"] = null;
            ////        Session["filePath"] = null;
            ////        lnkDisabilityDoc.Text = "";
            ////        lnkDisabilityDoc.Visible = false;
            ////    }

            ////    rdbIsPersonDisability_SelectedIndexChanged(null, null);

            ////    if (ds.Tables[0].Rows[0]["isDisabled"].ToString() != "" || ds.Tables[0].Rows[0]["isDisabilityPercentage"].ToString() == "true")
            ////    {
            ////        isChkDisability.Checked = true;
            ////        txtDisabilityRemark.Text = ds.Tables[0].Rows[0]["disabiltyRemarks"].ToString();
            ////    }
            ////    else
            ////    {
            ////        isChkDisability.Checked = false;
            ////        txtDisabilityRemark.Text = "";
            ////    }

            ////    Fill_PassingYear();
            ////    //D_ddlYeofPass.SelectedValue = ds.Tables[0].Rows[0]["yearofpassing"].ToString();
            ////    D_ddlYeofPass.SelectedValue = ds.Tables[0].Rows[0]["fk_pyearid"].ToString();
            ////    E_txtEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
            ////    txtCurrentAddress.Text = ds.Tables[0].Rows[0]["currentaddress"].ToString();
            ////    txtperadd.Text = ds.Tables[0].Rows[0]["per_address"].ToString();
            ////    txtCurrentOccupation.Text = ds.Tables[0].Rows[0]["currentoccupation"].ToString();
            ////    R_txtContactno.Text = ds.Tables[0].Rows[0]["contactno"].ToString();
            ////    txtsplinterest.Text = ds.Tables[0].Rows[0]["special_interest"].ToString();
            ////    txtachievement.Text = ds.Tables[0].Rows[0]["Achievement"].ToString();
            ////    R_txtPostedDate.Text = CommonCode.DateFormats.Date_DBToFront(ds.Tables[0].Rows[0]["dob"].ToString()).Replace("-", "/");
            ////    txtremarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
            ////    // Added by manoj

            ////    txtDesignation.Text = ds.Tables[0].Rows[0]["designation"].ToString();
            ////    chkmentor.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isMentor"]);
            ////    // txttelephoneno.Text = ds.Tables[0].Rows[0]["telephoneno"].ToString();

            ////    string fileName = ""; string contenttype = ""; byte[] fileBytes = null;
            ////    fileName = ds.Tables[1].Rows[0]["Files_Name"].ToString().Trim();
            ////    if (ds.Tables[1].Rows[0]["Files_Name"].ToString().Trim() != "")
            ////    {
            ////        Session["IsProfilec"] = "1";
            ////        imgProfileimg.ImageUrl = "https://backoffice.hpushimla.in/FTPSITE/HPU_DOC/Alumni/StuImage/" + ds.Tables[1].Rows[0]["Files_Name"].ToString();
            ////    }
            ////    else {
            ////        Session["IsProfilec"] = "0";
            ////        imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";
            ////        hdPath.Text = "";
            ////    }

            ////    txtCurrentOccupation.Text = ds.Tables[0].Rows[0]["currentoccupation"].ToString();
            ////    R_txtLoginName1.Text = ds.Tables[0].Rows[0]["loginname"].ToString();
            ////    if (ds.Tables[0].Rows[0]["password"].ToString() != "")
            ////    {
            ////        R_txtPassword.Attributes.Add("value", ds.Tables[0].Rows[0]["password"].ToString());
            ////        R_txtPassword.Text = ds.Tables[0].Rows[0]["password"].ToString();
            ////    }
            ////    ViewState["File2"] = ds.Tables[2].Rows[0]["Files_Name"].ToString();
            ////    if (ds.Tables[2].Rows[0]["Files_Name"] != null && ds.Tables[2].Rows[0]["Files_Name"].ToString() != "")
            ////    {
            ////        lnkDoc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
            ////        lnkDoc.CommandName = ds.Tables[2].Rows[0]["Files_Name"].ToString();
            ////        lnkDoc.Visible = true;
            ////        getFileName();
            ////    }
            ////    ViewState["File1"] = ds.Tables[1].Rows[0]["Files_Name"].ToString();
            ////    if (ds.Tables[1].Rows[0]["Files_Name"] != null && ds.Tables[1].Rows[0]["Files_Name"].ToString() != "")
            ////    {
            ////        lnkprofile.CommandArgument = (ViewState["id"]) == null ? "1" : (ViewState["id"].ToString());
            ////        lnkprofile.CommandName = ds.Tables[1].Rows[0]["Files_Name"].ToString();
            ////        lnkprofile.Visible = true;
            ////        getProfileName();
            ////    }

            ////    //Photo

            ////    //if (fileName != "" || fileName != null)
            ////    //{
            ////    //    //if (ds.Tables[0].Rows[0]["imgattach_p"].ToString().Trim() != "")
            ////    //    if (ds.Tables[1].Rows[0]["Files_Name"].ToString().Trim() != "")
            ////    //    {
            ////    //        //contenttype = ds.Tables[0].Rows[0]["contenttype_p"].ToString().Trim();
            ////    //        // fileBytes = null;// (byte[])ds.Tables[0].Rows[0]["imgattach_p"];
            ////    //        // fileName = Session["RegNo"].ToString() + "_p" + fileName.Substring(fileName.LastIndexOf("."));
            ////    //        setimageonedit(fileName);
            ////    //    }
            ////    //    else
            ////    //    {
            ////    //        imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";
            ////    //        hdPath.Text = "";
            ////    //    }
            ////    //}
            ////    //else
            ////    //{
            ////    //    imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";
            ////    //    hdPath.Text = "";
            ////    //}

            ////    //if (fileName != "")
            ////    //{
            ////    //    contenttype = ds.Tables[1].Rows[0]["FileExtension"].ToString().Trim();
            ////    //    string host = HttpContext.Current.Request.Url.Host;
            ////    //    string upldPath = "";
            ////    //    string showimgPath = "";
            ////    //    DataSet dsFilepath = new DataSet();
            ////    //    dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
            ////    //    foreach (DataRow dr in dsFilepath.Tables[0].Rows)
            ////    //    {
            ////    //        if (host == dr["Server_Ip"].ToString().Trim())
            ////    //        {
            ////    //            upldPath = dr["Physical_Path"].ToString().Trim();
            ////    //            showimgPath = upldPath.ToString().Trim();
            ////    //        }
            ////    //    }
            ////    //    imgProfileimg.Src = upldPath + fileName;
            ////    //    imgProfileimg.ImageUrl = upldPath + fileName;
            ////    //    //imgProfileimg.ImageUrl = "https://backoffice.hpushimla.in/FTPSITE/HPU_DOC/Alumni/StuImage/" + fileName;
            ////    //}
            ////    //else
            ////    //{
            ////    //    //imgProfilePhoto.Src = "~/alumni/stuimage/noimage.png";
            ////    //    imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";
            ////    //    hdPath.Text = "";
            ////    //}

            ////    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[4].Rows.Count > 0)
            ////    {
            ////        lblMsgPMode.Text = ds.Tables[4].Rows[0]["PaymentMode"].ToString();
            ////        lblTransID.Text = ds.Tables[4].Rows[0]["TransactionID"].ToString();
            ////        lblMsgPAmount.Text = ds.Tables[4].Rows[0]["amount"].ToString();
            ////        lblTransStatus.Text = ds.Tables[4].Rows[0]["status"].ToString();
            ////    }
            ////}            

            Session["AlumniID"] = Convert.ToInt32(Session["EmpView_AlumniID"].ToString());

            btnSubmit.Text = "UPDATE";
            btnSubmit.CommandName = "UPDATE";
            int alumniId = 0;

            if (Session["AlumniID"].ToString() == null || Session["AlumniID"].ToString() == "")
                alumniId = Convert.ToInt32(Session["EmpView_AlumniID"].ToString());
            else
                alumniId = Convert.ToInt32(Session["AlumniID"].ToString());

            DataSet ds = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(alumniId).GetDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["isapproved"]) == 1)
                {
                    chkmentor.Enabled = false;
                }
                else
                {
                    chkmentor.Enabled = false;
                }

                if (ds.Tables[0].Rows[0]["alumnitype"].ToString() != "" || ds.Tables[0].Rows[0]["alumnitype"].ToString() != null)
                {
                    alumniType = ds.Tables[0].Rows[0]["alumnitype"].ToString();
                    rdalumnitype.SelectedValue = ds.Tables[0].Rows[0]["alumnitype"].ToString();
                }
                else
                {
                    alumniType = string.Empty;
                    rdalumnitype.SelectedValue = "";
                }

                if (ds.Tables[0].Rows[0]["Membership_Type"].ToString() != "" || ds.Tables[0].Rows[0]["Membership_Type"].ToString() != null)
                {
                    memType = ds.Tables[0].Rows[0]["Membership_Type"].ToString();
                    rbdAlumniMemtype.SelectedValue = ds.Tables[0].Rows[0]["Membership_Type"].ToString();
                }
                else
                {
                    memType = string.Empty;
                    rbdAlumniMemtype.SelectedValue = "";
                }

                R_txtAlumnino.Text = ds.Tables[0].Rows[0]["alumnino"].ToString();

                FillSalution();

                if (ds.Tables[0].Rows[0]["Alumni_Sal"].ToString() != null || ds.Tables[0].Rows[0]["Alumni_Sal"].ToString() != "")
                {
                    ddlAlmPrefix.SelectedValue = ds.Tables[0].Rows[0]["Alumni_Sal"].ToString();
                }
                else
                {
                    ddlAlmPrefix.SelectedIndex = 0;
                }
                txtAlumniName.Text = ds.Tables[0].Rows[0]["alumni_name"].ToString();

                ViewState["id"] = ds.Tables[0].Rows[0]["pk_alumniid"].ToString();

                if (ds.Tables[0].Rows[0]["Father_Sal"].ToString() != null || ds.Tables[0].Rows[0]["Father_Sal"].ToString() != "")
                {
                    ddlFPrefix.SelectedValue = ds.Tables[0].Rows[0]["Father_Sal"].ToString();
                }
                else
                {
                    ddlFPrefix.SelectedIndex = 0;
                }

                R_txtFatherName.Text = ds.Tables[0].Rows[0]["fathername"].ToString();

                if (ds.Tables[0].Rows[0]["Mother_Sal"].ToString() != null || ds.Tables[0].Rows[0]["Mother_Sal"].ToString() != "")
                {
                    ddlMPrefix.SelectedValue = ds.Tables[0].Rows[0]["Mother_Sal"].ToString();
                }
                else
                {
                    ddlMPrefix.SelectedIndex = 0;
                }

                R_txtMotherName.Text = ds.Tables[0].Rows[0]["mothername"].ToString();

                R_txtPostedDate.Text = ds.Tables[0].Rows[0]["dob"].ToString();
                E_txtEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
                R_txtContactno.Text = ds.Tables[0].Rows[0]["contactno"].ToString();
                txtremarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
                chkmentor.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isMentor"]);
                txtCurrentOccupation.Text = ds.Tables[0].Rows[0]["currentoccupation"].ToString();
                txtDesignation.Text = ds.Tables[0].Rows[0]["designation"].ToString();
                txtperadd.Text = ds.Tables[0].Rows[0]["per_address"].ToString();
                txtCurrentAddress.Text = ds.Tables[0].Rows[0]["currentaddress"].ToString();
                txtsplinterest.Text = ds.Tables[0].Rows[0]["special_interest"].ToString();
                txtachievement.Text = ds.Tables[0].Rows[0]["Achievement"].ToString();

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
                            ViewState["DDFile"] = ds.Tables[5].Rows[0]["Files_Unique_Name"].ToString();
                            ViewState["filePath"] = ds.Tables[5].Rows[0]["file_Path"].ToString();
                            //lnkDisabilityDoc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
                            //lnkDisabilityDoc.CommandName = ds.Tables[5].Rows[0]["Files_Unique_Name"].ToString();
                            //lnkDisabilityDoc.Visible = true;
                            //getDDocFileName();
                            //lblDDDoc.Visible = true;
                            //lblDDDoc.Text = ds.Tables[5].Rows[0]["oFiles"].ToString();
                        }
                    }
                    else
                    {
                        ViewState["DDFile"] = null;
                        ViewState["filePath"] = null;
                        lnkDisabilityDoc.Text = "";
                        lnkDisabilityDoc.Visible = false;
                        //lblDDDoc.Text = "";
                        //lblDDDoc.Visible = false;
                    }
                }
                else
                {
                    rdbIsPersonDisability.SelectedValue = "N";
                    ViewState["DDFile"] = null;
                    ViewState["filePath"] = null;
                    lnkDisabilityDoc.Text = "";
                    lnkDisabilityDoc.Visible = false;
                    //lblDDDoc.Text = "";
                    //lblDDDoc.Visible = false;
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

                //D_ddlYeofPass.SelectedValue = ds.Tables[0].Rows[0]["yearofpassing"].ToString();
                Fill_PassingYear();
                D_ddlYeofPass.SelectedValue = ds.Tables[0].Rows[0]["fk_pyearid"].ToString();

                ddldegree.SelectedValue = ds.Tables[0].Rows[0]["fk_degreeid"].ToString();
                ddldegree.CssClass = "ChosenSelector";
                ddldegree_SelectedIndexChanged(null, null);
                subjectlist.SelectedValue = ds.Tables[0].Rows[0]["Fk_subjectid"].ToString();
                FillDepartment();
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["fk_Deptid"].ToString();

                string fileName = ""; string contenttype = ""; byte[] fileBytes = null;
                //fileName = ds.Tables[1].Rows[0]["Files_Name"].ToString().Trim();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString().Trim()))
                    {
                        Session["IsProfilec"] = "1";
                        imgProfileimg.ImageUrl = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/" + ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString();
                    }
                    else
                    {
                        Session["IsProfilec"] = "0";
                        imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";
                        hdPath.Text = "";
                    }
                }
                else
                {
                    Session["IsProfilec"] = "0";
                    imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";
                    hdPath.Text = "";
                }

                //lblPPDoc.Visible = true;
                //lblPPDoc.Text = "";

                //lblADDoc.Visible = true;
                //lblADDoc.Text = "";

                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString()))
                    {
                        ViewState["File1"] = ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString();
                        ViewState["PPFile"] = ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString();
                        //lnkprofile.CommandArgument = (ViewState["id"]) == null ? "1" : (ViewState["id"].ToString());
                        //lnkprofile.CommandName = ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString();
                        //lnkprofile.Visible = true;
                        //getProfileName();
                        //lblPPDoc.Visible = true;
                        //lblPPDoc.Text = ds.Tables[1].Rows[0]["oFiles"].ToString();
                    }
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString()))
                    {
                        ViewState["File2"] = ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString();
                        ViewState["ADFile"] = ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString();
                        //lnkDoc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
                        //lnkDoc.CommandName = ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString();
                        //lnkDoc.Visible = true;
                        //getADocFileName();
                        //lblADDoc.Visible = true;
                        //lblADDoc.Text = ds.Tables[2].Rows[0]["oFiles"].ToString();
                    }
                }

                if (ds.Tables[6].Rows.Count > 0)
                {
                    GrdFile.DataSource = ds.Tables[6];
                    GrdFile.DataBind();
                    GrdFile.Visible = true;
                }
                else
                {
                    GrdFile.DataSource = null;
                    GrdFile.DataBind();
                    GrdFile.Visible = false;
                }

                R_txtLoginName1.Text = ds.Tables[0].Rows[0]["email"].ToString();

                if (ds.Tables[0].Rows[0]["password"].ToString() != "")
                {
                    R_txtPassword.Attributes.Add("value", ds.Tables[0].Rows[0]["password"].ToString());
                    R_txtPassword.Text = ds.Tables[0].Rows[0]["password"].ToString();
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
        ////string FileName = lnkprofile.CommandName;
        ////string FileUrl = ReturnPath();
        ////string FileDisplayName = "";
        ////string FileRealName = "";
        //////if (FileName.Contains("/"))
        //////{
        ////FileDisplayName = FileName;

        ////FileRealName = "https://backoffice.hpushimla.in/FTPSITE/HPU_DOC/Alumni/StuImage/" + FileName.Substring(FileName.IndexOf("/") + 1);
        //////   }
        //////FileUrl = FileUrl + FileName;
        ////lnkprofile.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
    }

    protected void getFileName()
    {
        //string FileName = lnkDoc.CommandName;
        //string FileUrl = ReturnPath();
        //string FileDisplayName = "";
        //string FileRealName = "";
        ////if (FileName.Contains("/"))
        ////{
        //FileDisplayName = FileName;

        //FileRealName = "https://backoffice.hpushimla.in/FTPSITE/HPU_DOC/Alumni/StuImage/" + FileName.Substring(FileName.IndexOf("/") + 1);
        ////   }
        //// FileUrl = FileUrl + FileName;
        //lnkDoc.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
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
                clearDocumentFiles();
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

    protected void clearDocumentFiles()
    {
        try
        {
            ViewState["ODDFile"] = null;
            ViewState["OPPFile"] = null;
            ViewState["OADFile"] = null;

            ViewState["DDFile"] = null;
            ViewState["PPFile"] = null;
            ViewState["ADFile"] = null;

            GrdFile.DataSource = null;
            GrdFile.DataBind();
            GrdFile.Visible = false;

            flUpload.Dispose(); uploadDocuments.Dispose(); fileDisabilityDoc.Dispose();
            //lblPPDoc.Text = ""; //lblADDoc.Text = ""; //lblDDDoc.Text = "";

            if (ViewState["DDfilePath_refletter_delete"] != null && ViewState["DDfilePath_refletter_delete"].ToString() != "")
            {
                string DDfilePathToDelete = ViewState["DDfilePath_refletter_delete"].ToString();
                if (System.IO.File.Exists(DDfilePathToDelete))
                {
                    try
                    {
                        System.IO.File.Delete(DDfilePathToDelete);
                    }
                    catch (Exception ex)
                    {
                        //Label1.Text = "Error deleting file: " + ex.Message;
                    }
                }
            }
            hyperLinkDDoc.Text = "";
            ViewState["DDfilePath_refletter_delete"] = null;
            btnRemoveDD.Enabled = false;
            btnRemoveDD.Visible = false;

            if (ViewState["PPfilePath_refletter_delete"] != null && ViewState["PPfilePath_refletter_delete"].ToString() != "")
            {
                string PPfilePathToDelete = ViewState["PPfilePath_refletter_delete"].ToString();
                if (System.IO.File.Exists(PPfilePathToDelete))
                {
                    try
                    {
                        System.IO.File.Delete(PPfilePathToDelete);
                    }
                    catch (Exception ex)
                    {
                        //Label1.Text = "Error deleting file: " + ex.Message;
                    }
                }
            }
            hyperLinkProfile.Text = "";
            ViewState["PPfilePath_refletter_delete"] = null;
            btnRemovePP.Enabled = false;
            btnRemovePP.Visible = false;
            imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";

            if (ViewState["ADfilePath_refletter_delete"] != null && ViewState["ADfilePath_refletter_delete"].ToString() != "")
            {
                string ADfilePathToDelete = ViewState["ADfilePath_refletter_delete"].ToString();
                if (System.IO.File.Exists(ADfilePathToDelete))
                {
                    try
                    {
                        System.IO.File.Delete(ADfilePathToDelete);
                    }
                    catch (Exception ex)
                    {
                        //Label1.Text = "Error deleting file: " + ex.Message;
                    }
                }
            }
            hyperLinkADoc.Text = "";
            ViewState["ADfilePath_refletter_delete"] = null;
            btnRemoveAD.Enabled = false;
            btnRemoveAD.Visible = false;
        }
        catch (Exception)
        {
            throw;
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

    //void UploadDocumentFiles(DataTable dt, string upldPath, CuteWebUI.UploadAttachments Fl_UploadFiles)
    //{
    //    string currDir = System.IO.Directory.GetCurrentDirectory();
    //    upldPath = this.upldPath;
    //    bool IsExistPath = System.IO.Directory.Exists(upldPath);
    //    if (!IsExistPath)
    //        System.IO.Directory.CreateDirectory(upldPath);
    //    string filename = "";

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        filename = dt.Rows[i]["Files_Unique_Name"].ToString();
    //        AttachmentItem aItem = Fl_UploadFiles.Items[i];
    //        aItem.CopyTo(upldPath + filename);
    //    }
    //}

    void UploadDocumentFiles(DataTable dt, string upldPath, FileUpload UploadItems)
    {
        string currDir = System.IO.Directory.GetCurrentDirectory(); // upldPath = @"D:/Published_apps/FTP_Site/Support_Doc/";

        bool IsExistPath = System.IO.Directory.Exists(upldPath);

        if (!IsExistPath)
            System.IO.Directory.CreateDirectory(upldPath);
        string filename = "";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            filename = dt.Rows[i]["uploadfile"].ToString();
            UploadItems.SaveAs(upldPath + filename);
        }
    }

    //it will return file based on file unique name
    public string SetServiceDoc(string FileName)
    {
        string FolderName = @"/Alumni/StuImage";
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

    protected void rbtnlst_approveRej_SelectedIndexChanged(object sender, EventArgs e)
    {

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

    private DataSet AlumniAprInsert()
    {
        ClearArrayLists();
        names.Add("@isapproved"); values.Add(ApprovedStatus); types.Add(SqlDbType.Int);
        names.Add("@pk_alumniid"); values.Add(alumniId); types.Add(SqlDbType.Int);
        names.Add("@userID"); values.Add(userID); types.Add(SqlDbType.NVarChar);
        return Dobj.GetDataSet("Alm_Approvand_reject_Insert", values, names, types);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //DataSet ds = new DataSet();
            //ApprovedStatus = rbtnlst_approveRej.SelectedValue;
            //alumniId = Convert.ToInt32(Session["AlumniID"].ToString());
            //userID = Session["UserID"].ToString().Trim();
            //ds = AlumniAprInsert();
            //ArrayList Result = new ArrayList();
            //int AlumniI = AlumniAprInsert(Convert.ToInt32(ViewState["id"])).Execute();

            //if (ds.Tables.Count > 0)
            //{
            //if (ApprovedStatus == "1")
            //{
            //    lblMsg.Text = "Record Approved Successfully!";
            //    ClientMessaging("Record Approved Successfully");
            //    //clear1();                  
            //}
            //if (ApprovedStatus == "2")
            //{
            //    //lblMsg.Text = "Record Rejected Successfully!";
            //    //ClientMessaging("Record Rejected Successfully");
            //    Response.Redirect("ApproveAlumnibyadmin.aspx");
            //}
            //FillForUpdate();
            // }
            //else
            //{
            //    Response.Redirect("ApproveAlumnibyadmin.aspx");
            //    //lblMsg.Text = "Retry!";
            //}

            //if (btnSubmit.CommandName.ToUpper().ToString() == "SUBMIT")
            //{
            //    if ((AlumniAprInsert(rbtnlst_approveRej.SelectedValue).Execute()) > 0) ;
            //    {
            //        ClientMessaging("Record Saved Successfully");
            //        //FillGrid();
            //        Clear();
            //    }
            //}

            if (!Validation())
            {
                return;
            }

            DataSet dsmain = getMain(sender); //dsmain.Merge(GetUploadFile());
            string xmlDoc = dsmain.GetXml();

            if (ViewState["id"] == null)
            {
                return;
            }

            int AlumniI = ALM_SP_AlumniRegistration_Update_By_Admin(Convert.ToInt32(ViewState["id"]), xmlDoc).Execute();

            if (AlumniI > 0)
            {
                //if (flUpload.HasFile || uploadDocuments.HasFile || fileDisabilityDoc.HasFile)
                //{
                //    DataSet dsi = (DataSet)ViewState["DSI"];
                //    DataView dv = dsi.Tables[0].DefaultView;
                //    dv.RowFilter = String.Format("FilesFor = '{0}'", "PP");
                //    if (dv.Count > 0)
                //    {
                //        UploadDocumentFiles(dv.ToTable(), upldPath, flUpload);
                //    }
                //    dv.RowFilter = String.Format("FilesFor = '{0}'", "AD");
                //    if (dv.Count > 0)
                //    {
                //        UploadDocumentFiles(dv.ToTable(), upldPath, uploadDocuments);
                //    }
                //    dv.RowFilter = String.Format("FilesFor = '{0}'", "DD");
                //    if (dv.Count > 0)
                //    {
                //        UploadDocumentFiles(dv.ToTable(), upldPath, fileDisabilityDoc);
                //    }
                //}

                Button btn = (Button)sender;
                if (btn.Text.ToUpper() == "SUBMIT")
                {
                    //lblMsg.Text = "Record Submitted Successfully!";
                    ClientMessaging("Record Submitted Successfully!");
                    btnSubmit.Enabled = false;
                }
                else if (btn.Text.ToUpper() == "UPDATE")
                {
                    //lblMsg.Text = "Record Updated Successfully!";
                    ClientMessaging("Record Updated Successfully!");
                    btnSubmit.Enabled = true;
                    clearDocumentFiles();
                    FillForUpdate();
                    //Response.Redirect("~/Alumni/ALM_AlumniRegistration_Admin_Profile_Updation.aspx");
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        //Response.Redirect("ApproveAlumnibyadmin.aspx");
        Response.Redirect("ALM_AlumniRegistration_Admin_Profile_Updation.aspx");
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
        else if (rdbGender.SelectedValue.ToString() == "O")
        {
            rdbGender.SelectedValue = "O";
        }
    }

    protected void ddldegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        pk_degreeid = Convert.ToInt32(ddldegree.SelectedValue);
        getsubject(pk_degreeid);
    }

    protected void rdbIsPersonDisability_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string dectype2 = cpt.Decrypt(Session["RBValue"].ToString());
        //alumniType = dectype2.ToString();
        if (rdbIsPersonDisability.SelectedValue == "Y")
        {
            isChkDisabilityPnl.Visible = true;
            disabilitySection.Visible = true;
            Anthem.Manager.IncludePageScripts = true;
            isChkDisability_CheckedChanged(null, null);
            //btnRegister.Text = "REGISTER";
        }
        else if (rdbIsPersonDisability.SelectedValue == "N")
        {
            isChkDisabilityPnl.Visible = false;
            disabilitySection.Visible = false;
            isChkDisability.Checked = false;
            txtDisabilityRemark.Text = "";
            Anthem.Manager.IncludePageScripts = true;
            isChkDisability_CheckedChanged(null, null);
            //btnRegister.Text = "REGISTER AND PAY";
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
            btnDisabilityDoc.Enabled = true;
            btnDisabilityDoc.Visible = true;
            //btnRemoveDD.Enabled = true;
            //btnRemoveDD.Visible = true;
            //divDisabilityDoc.Style["display"] = "block";
            //lblOnlineFees.Text = "0";
            Session["RegFees"] = "0";
            //hdnId.Value = "0";

            if (ViewState["DDfilePath_refletter_delete"] != null && ViewState["DDfilePath_refletter_delete"].ToString() != "")
            {
                btnRemoveDD.Enabled = true;
                btnRemoveDD.Visible = true;
            }
            else
            {
                btnRemoveDD.Enabled = false;
                btnRemoveDD.Visible = false;
            }
        }
        else
        {
            Anthem.Manager.IncludePageScripts = true;
            disabilityRemarkSection.Style["display"] = "none";
            isChkDisability.Checked = false;
            //disabilityDocuments.Visible = false;
            fileDisabilityDoc.Visible = false;
            btnDisabilityDoc.Enabled = false;
            btnDisabilityDoc.Visible = false;

            //divDisabilityDoc.Style["display"] = "none";
            txtDisabilityRemark.Text = "";
            FeeDetails();

            if (ViewState["DDfilePath_refletter_delete"] != null && ViewState["DDfilePath_refletter_delete"].ToString() != "")
            {
                // Check if the file exists before attempting to delete it
                string filePathToDelete = ViewState["DDfilePath_refletter_delete"].ToString();
                if (System.IO.File.Exists(filePathToDelete))
                {
                    try
                    {
                        System.IO.File.Delete(filePathToDelete);
                    }
                    catch (Exception ex)
                    {
                        //Label1.Text = "Error deleting file: " + ex.Message;
                    }
                }
            }
            lnkDisabilityDoc.Text = "";
            btnRemoveDD.Enabled = false;
            btnRemoveDD.Visible = false;
            ViewState["DDfilePath_refletter_delete"] = null;
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    private void FeeDetails()
    {
        if (alumniType == "F" || alumniType == "ExStu")
        {
            if (rdbIsPersonDisability.SelectedValue.ToString() == "N")
            {
                rbdAlumniMemtype.Items.FindByValue("LM").Selected = true;
                memType = rbdAlumniMemtype.SelectedValue;
                DataSet Ds = Alm_GetMembershipFee().GetDataSet();
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    lblOnlineFees.Text = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    Session["RegFees"] = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    hdnId.Value = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                }
            }
            else if (rdbIsPersonDisability.SelectedValue.ToString() == "Y")
            {
                rbdAlumniMemtype.Items.FindByValue("LM").Selected = true;
                memType = rbdAlumniMemtype.SelectedValue;
                lblOnlineFees.Text = "0";
                Session["RegFees"] = "0";
                hdnId.Value = "0";
            }
        }
        else if (alumniType == "S")
        {
            if (rdbIsPersonDisability.SelectedValue.ToString() == "N")
            {
                rbdAlumniMemtype.Items.FindByValue("SM").Selected = true;
                memType = rbdAlumniMemtype.SelectedValue;
                DataSet Ds = Alm_GetMembershipFee().GetDataSet();
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    lblOnlineFees.Text = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    Session["RegFees"] = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    hdnId.Value = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                }
            }
            else if (rdbIsPersonDisability.SelectedValue.ToString() == "Y")
            {
                rbdAlumniMemtype.Items.FindByValue("SM").Selected = true;
                memType = rbdAlumniMemtype.SelectedValue;
                lblOnlineFees.Text = "0";
                Session["RegFees"] = "0";
                hdnId.Value = "0";
            }
        }
        Anthem.Manager.IncludePageScripts = true;
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

    public StoredProcedure Alm_GetMembershipFee()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_FeeCollection", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@membershipType", memType, DbType.String);
        return sp;
    }

    protected void rbdAlumniMemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FeeDetails();
    }

    protected void btnDisabilityDoc_Click(object sender, EventArgs e)
    {
        if (fileDisabilityDoc.HasFile)
        {
            FileType = Path.GetExtension(fileDisabilityDoc.PostedFile.FileName);
            if (FileType != ".pdf" && FileType != ".PDF" && FileType != ".jpeg" && FileType != ".JPEG" && FileType != ".jpg" && FileType != ".JPG" && FileType != ".png" && FileType != ".PNG" && FileType != ".bmp" && FileType != ".BMP")
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
                ViewState["DDFile"] = lFileName;
                fileDisabilityDoc.SaveAs(Server.MapPath("ALM_Reports//" + lFileName));
                ClientMessaging("Specially Abled Document Uploaded Successfully.");
                string DDfilePathforhyperlink = "ALM_Reports//" + ViewState["DDFile"].ToString();
                string DDfilePath = Server.MapPath("ALM_Reports/") + ViewState["DDFile"].ToString();
                ViewState["DDfilePath_refletter_delete"] = DDfilePath;
                //lnkDisabilityDoc.Visible = true;
                //lnkDisabilityDoc.Text = fileDisabilityDoc.FileName;
                hyperLinkDDoc.Visible = true;
                hyperLinkDDoc.NavigateUrl = DDfilePathforhyperlink;
                hyperLinkDDoc.Text = fileDisabilityDoc.FileName;
                //lblDDDoc.Visible = true;
                //lblDDDoc.Text = fileDisabilityDoc.FileName;
                ViewState["ODDFile"] = fileDisabilityDoc.FileName;
                isChkDisability_CheckedChanged(null, null);
                //btnDisabilityDoc.Enabled = true;
                //btnDisabilityDoc.Visible = true;
                //btnRemoveDD.Enabled = true;
                //btnRemoveDD.Enabled = true;
            }
        }
        else
        {
            btnRemoveDD.Enabled = false;
            btnRemoveDD.Enabled = false;
            ClientMessaging("Please Upload Special Disabilities Documents.!!!");
        }
    }

    string FileType = "";
    double filesize = 0;
    DataRow drimg = null;
    string Fileuniquename = string.Empty;

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

    public string FU_physicalPaths(FileUpload flu, string FolderName, string FileName)
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
                        Session["filepaths"] = upldPath;
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

    protected void getADocFileName()
    {
        string FileName = lnkDoc.CommandName;
        string FileUrl = ReturnPath();
        string FileDisplayName = "";
        string FileRealName = "";
        //if (FileName.Contains("/"))
        //{
        FileDisplayName = FileName;
        FileRealName = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/" + FileName.Substring(FileName.IndexOf("/") + 1);
        //   }
        // FileUrl = FileUrl + FileName;
        lnkDoc.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
    }

    protected bool Validation()
    {
        if (rdalumnitype.SelectedValue != "F" && rdalumnitype.SelectedValue != "S" && rdalumnitype.SelectedValue != "ExStu")
        {
            ClientMessaging("Please Choose Alumni Type.!");
            rdalumnitype.Focus();
            return false;
        }

        if (rbdAlumniMemtype.SelectedValue != "LM" && rbdAlumniMemtype.SelectedValue != "SM")
        {
            ClientMessaging("Please Choose Membership Type.!");
            rbdAlumniMemtype.Focus();
            return false;
        }
        if (txtAlumniName.Text == "")
        {
            ClientMessaging("Please enter Alumni Name.!");
            txtAlumniName.Focus();
            return false;
        }
        if (R_txtFatherName.Text == "")
        {
            ClientMessaging("Please Enter Father Name.!");
            R_txtFatherName.Focus();
            return false;
        }
        if (R_txtMotherName.Text == "")
        {
            ClientMessaging("Please Enter Mother Name.!");
            R_txtMotherName.Focus();
            return false;
        }
        if (R_txtPostedDate.Text.Trim() == "")
        {
            ClientMessaging("Please Enter Date of Birth.!");
            R_txtPostedDate.Focus();
            return false;
        }

        if (E_txtEmail.Text == "")
        {
            ClientMessaging("Please Enter Email.!");
            E_txtEmail.Focus();
            return false;
        }
        else
        {
            E_txtEmail_TextChanged(null, null);
        }

        if (R_txtContactno.Text.Trim().Length < 10)
        {
            ClientMessaging("Please Enter Valid Mobile No.!");
            R_txtContactno.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(rdbGender.SelectedValue))
        {
            ClientMessaging("Please Choose Gender.!");
            rdbGender.Focus();
            return false;
        }

        #region "Special Ability Validation"

        if (rdbIsPersonDisability.SelectedValue == "Y" && isChkDisability.Checked)
        {
            if (!isChkDisability.Checked)
            {
                ClientMessaging("Please Check Special Ability More than 40% ?.");
                Anthem.Manager.IncludePageScripts = true;
                isChkDisability.Focus();
                return false;
            }

            if (!fileDisabilityDoc.HasFile && (ViewState["DDFile"] == null || ViewState["DDFile"].ToString() == ""))
            {
                ClientMessaging("Please Choose a Special Ability Document.");
                return false;
            }

            string fileExtension = !fileDisabilityDoc.HasFile ? Path.GetExtension(ViewState["DDFile"].ToString()) : Path.GetExtension(fileDisabilityDoc.FileName).ToLower();

            if (fileDisabilityDoc.PostedFile.ContentLength > 2097152) // 2MB in bytes
            {
                ClientMessaging("File Size Should Not Exceed 2MB.");
                return false;
            }

            if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".bmp" && fileExtension != ".png" && fileExtension != ".pdf")
            {
                ClientMessaging("Supported File Types Are .jpg, .jpeg, .bmp, .png, And .pdf.");
                return false;
            }

            // If Special Ability is Yes, check if remark and file are provided
            if (string.IsNullOrWhiteSpace(txtDisabilityRemark.Text))
            {
                ClientMessaging("Please Enter Special Ability Description.");
                return false;
            }
        }

        #endregion

        if (ddldegree.SelectedIndex == 0)
        {
            ClientMessaging("Please Enter Degree.!");
            Anthem.Manager.IncludePageScripts = true;
            ddldegree.Focus();
            return false;
        }
        if (subjectlist.SelectedIndex == 0)
        {
            ClientMessaging("Please Enter Subject.!");
            Anthem.Manager.IncludePageScripts = true;
            subjectlist.Focus();
            return false;
        }
        if (D_ddlYeofPass.SelectedIndex == 0)
        {
            ClientMessaging("Please Select Passing Year.!");
            Anthem.Manager.IncludePageScripts = true;
            D_ddlYeofPass.Focus();
            return false;
        }

        if (R_txtPostedDate.Text.Trim() != "")
        {
            DateTime dtDob = Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(R_txtPostedDate.Text.Trim()));
            int DobYear = dtDob.Year;
            int PassYear = Convert.ToInt32(D_ddlYeofPass.SelectedItem.Text.ToString());
            PassYear = PassYear - 18;
            if (DobYear > PassYear)
            {
                ClientMessaging("Alumni Age Should Be Greater Than 18 Years As Per Passing Year.!");
                R_txtPostedDate.Focus();
                return false;
            }
        }

        if (ddlDepartment.SelectedIndex == 0)
        {
            ClientMessaging("Please Select Department.!");
            ddlDepartment.Focus();
            return false;
        }

        if (!flUpload.HasFile && ViewState["PPFile"] == null)
        {
            ClientMessaging("Please Select The Required Photo.!");
            flUpload.Focus();
            return false;
        }

        if (flUpload.HasFile && ViewState["PPFile"] == null)
        {
            if (flUpload.PostedFile.FileName.Length > 500)
            {
                ClientMessaging("Photo File Name Should Not Be More Than 500 Characters.!");
                flUpload.Focus();
                return false;
            }

            if (flUpload.PostedFile.ContentLength > 1 * 1024 * 1024)
            {
                ClientMessaging("Photo Size Should Not Be More Than 1 MB.");
                flUpload.Focus();
                return false;
            }

            string fileExtensionPic = Path.GetExtension(flUpload.FileName).ToLower();

            string[] allowedExtensionsPic = { ".jpg", ".jpeg", ".bmp", ".png" };

            if (Array.Exists(allowedExtensionsPic, ext => ext == fileExtensionPic))
            {
                return true;
            }
            else
            {
                ClientMessaging("Photo Should Be In Required Format.!");
                flUpload.Focus();
                return false;
            }
        }

        if (uploadDocuments.HasFile && (ViewState["ADFile"] == null || ViewState["ADFile"].ToString() == ""))
        {
            if (uploadDocuments.PostedFile.FileName.Length > 500)
            {
                ClientMessaging("Document File Name Should Not Be More Than 500 Characters.!");
                uploadDocuments.Focus();
                return false;
            }

            if (flUpload.PostedFile.ContentLength > 2 * 1024 * 1024)
            {
                ClientMessaging("Document Size Should Not Be More Than 2 MB.");
                uploadDocuments.Focus();
                return false;
            }

            string fileExtensionDoc = Path.GetExtension(flUpload.FileName).ToLower();

            string[] allowedExtensionsDoc = { ".jpg", ".jpeg", ".bmp", ".png", ".pdf" };

            if (Array.Exists(allowedExtensionsDoc, ext => ext == fileExtensionDoc))
            {
                return true;
            }
            else
            {
                ClientMessaging("Document Should Be In Required Format.!");
                uploadDocuments.Focus();
                return false;
            }
        }
        return true;
    }

    DataSet getMain(object obj)
    {
        DataSet dsMain = new DataSet();
        dsMain = Dobj.GetSchema("ALM_AlumniRegistration");
        DataRow drMain = dsMain.Tables[0].NewRow();

        if (rdalumnitype.SelectedValue.ToString() == "F")
        {
            drMain["alumnitype"] = "F";
        }
        else if (rdalumnitype.SelectedValue.ToString() == "S")
        {
            drMain["alumnitype"] = "S";
        }
        else if (rdalumnitype.SelectedValue.ToString() == "ExStu")
        {
            drMain["alumnitype"] = "ExStu";
        }

        if (rbdAlumniMemtype.SelectedValue.ToString() == "LM")
        {
            drMain["Membership_Type"] = "LM";
        }
        else if (rbdAlumniMemtype.SelectedValue.ToString() == "SM")
        {
            drMain["Membership_Type"] = "SM";
        }

        drMain["alumnino"] = R_txtAlumnino.Text.Trim();

        if (ddlAlmPrefix.SelectedIndex > 0)
        {
            drMain["Alumni_Sal"] = ddlAlmPrefix.SelectedValue.ToString().Trim();
        }
        else
        {
            drMain["Alumni_Sal"] = DBNull.Value;
        }

        drMain["alumni_name"] = txtAlumniName.Text.Trim().ToString();

        if (ddlFPrefix.SelectedIndex > 0)
        {
            drMain["Father_Sal"] = ddlFPrefix.SelectedValue.ToString().Trim();
        }
        else
        {
            drMain["Father_Sal"] = DBNull.Value;
        }

        drMain["fathername"] = R_txtFatherName.Text.Trim().ToString();

        if (ddlMPrefix.SelectedIndex > 0)
        {
            drMain["Mother_Sal"] = ddlMPrefix.SelectedValue.ToString().Trim();
        }
        else
        {
            drMain["Mother_Sal"] = DBNull.Value;
        }

        drMain["mothername"] = R_txtMotherName.Text.Trim().ToString();
        drMain["fk_collegeid"] = 0;
        drMain["fk_degreeid"] = Convert.ToInt32(ddldegree.SelectedValue);
        drMain["Fk_subjectid"] = Convert.ToInt32(subjectlist.SelectedValue);
        drMain["yearofpassing"] = Convert.ToInt32(D_ddlYeofPass.SelectedItem.Text);
        drMain["fk_pyearid"] = Convert.ToInt32(D_ddlYeofPass.SelectedValue);
        drMain["email"] = E_txtEmail.Text.Trim();
        drMain["currentaddress"] = txtCurrentAddress.Text.Trim();
        drMain["per_address"] = txtperadd.Text.Trim();
        drMain["currentoccupation"] = txtCurrentOccupation.Text.Trim();
        drMain["contactno"] = R_txtContactno.Text.Trim();
        drMain["special_interest"] = txtsplinterest.Text.Trim();
        drMain["Achievement"] = txtachievement.Text.Trim();
        drMain["remarks"] = txtremarks.Text.Trim();
        drMain["dob"] = R_txtPostedDate.Text.Trim();
        drMain["fk_Deptid"] = Convert.ToInt32(ddlDepartment.SelectedValue);
        drMain["designation"] = txtDesignation.Text.Trim();
        drMain["mothername"] = R_txtMotherName.Text.Trim();
        drMain["fathername"] = R_txtFatherName.Text.Trim();
        drMain["loginname"] = R_txtAlumnino.Text.Trim();
        drMain["password"] = R_txtPostedDate.Text.Trim();

        if (rdbIsPersonDisability.SelectedValue == "Y" && isChkDisability.Checked)
        {
            drMain["isapproved"] = 1;
        }

        if (rdbGender.SelectedValue.ToString() == "M")
        {
            drMain["gender"] = "M";
        }
        else if (rdbGender.SelectedValue.ToString() == "F")
        {
            drMain["gender"] = "F";
        }
        else if (rdbGender.SelectedValue.ToString() == "O")
        {
            drMain["gender"] = "O";
        }

        drMain["isMentor"] = chkmentor.Checked;

        if (rdbIsPersonDisability.SelectedValue.ToString() == "Y")
        {
            drMain["isDisabled"] = "Y";
        }
        else if (rdbIsPersonDisability.SelectedValue.ToString() == "N")
        {
            drMain["isDisabled"] = "N";
            drMain["isDisabilityPercentage"] = isChkDisability.Checked;
            drMain["disabiltyRemarks"] = "";
        }

        if (isChkDisability.Checked)
        {
            drMain["isDisabilityPercentage"] = isChkDisability.Checked;
            drMain["disabiltyRemarks"] = txtDisabilityRemark.Text.Trim().ToString();
        }
        else
        {
            drMain["isDisabilityPercentage"] = isChkDisability.Checked;
            drMain["disabiltyRemarks"] = "";
        }

        drMain["UpdatedBy_Admin"] = Session["UserID"].ToString();

        dsMain.Tables[0].Rows.Add(drMain);

        #region "For Profile Pic"

        Random randomNo = new Random();
        string Message = "";
        string FileType = "";
        double filesize = 0;
        DataRow drDocss = null;
        string Fileuniquename = string.Empty;

        DataSet dsDocss = Dobj.GetSchema("ALM_AlumniRegistration_File_dtl");
        dsDocss.Tables[0].TableName = "ALM_AlumniRegistration_File_dtl";

        //if (flUpload.HasFile == true && checkValidPhotoOrNot())
        //{
        //    string Name = flUpload.PostedFile.FileName;
        //    FileType = Path.GetExtension(flUpload.PostedFile.FileName);

        //    UploadFiles();
        //    string upldPath = "";
        //    string currDir = System.IO.Directory.GetCurrentDirectory();
        //    upldPath = this.upldPath;

        //    drDocss = dsDocss.Tables[0].NewRow();
        //    drDocss["IsProfilePicOrDoc"] = 1;
        //    drDocss["Files_Name"] = Name;
        //    drDocss["Files_Unique_Name"] = "HPU_Alumni_PP" + "_" + Guid.NewGuid().ToString() + "_" + randomNo.Next(50000, 1000000) + "_" +
        //        DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PP" + "_" + randomNo.Next(33333, 454545454) + "_" + randomNo.Next(999999, 15215454) + FileType;
        //    Fileuniquename = "HPU_Alumni_PP_" + "_" + Guid.NewGuid().ToString() + "_" + randomNo.Next(50000, 1000000) + "_" +
        //       DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PP" + "_" + randomNo.Next(33333, 454545454) + "_" + randomNo.Next(999999, 15215454) + FileType;
        //    drDocss["FileExtension"] = FileType;
        //    drDocss["File_Path"] = upldPath;
        //    drDocss["FilesFor"] = "PP";
        //    dsDocss.Tables[0].Rows.Add(drDocss);

        //    bool IsExistPath = System.IO.Directory.Exists(upldPath);
        //    if (!IsExistPath)
        //        System.IO.Directory.CreateDirectory(upldPath);
        //    flUpload.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
        //}
        //else
        //{
        //if (ViewState["id"] != null && ViewState["PPFile"] != null)
        //{
        //    DataSet dsS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

        //    if (dsS != null && dsS.Tables[1].Rows.Count > 0)
        //    {
        //        drDocss = dsDocss.Tables[0].NewRow();
        //        drDocss["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
        //        drDocss["IsProfilePicOrDoc"] = dsS.Tables[1].Rows[0]["IsProfilePicOrDoc"].ToString();
        //        drDocss["Files_Name"] = dsS.Tables[1].Rows[0]["Files_Name"].ToString();
        //        drDocss["Files_Unique_Name"] = dsS.Tables[1].Rows[0]["Files_Unique_Name"].ToString(); ;
        //        drDocss["FileExtension"] = dsS.Tables[1].Rows[0]["FileExtension"].ToString(); ;
        //        drDocss["File_Path"] = dsS.Tables[1].Rows[0]["File_Path"].ToString();
        //        drDocss["FilesFor"] = dsS.Tables[1].Rows[0]["FilesFor"].ToString();
        //        dsDocss.Tables[0].Rows.Add(drDocss);
        //    }
        //}
        //}

        if (ViewState["id"] != null && ViewState["PPFile"] != null)
        {
            string fileExtensionPP = Path.GetExtension(ViewState["PPFile"].ToString());
            drDocss = dsDocss.Tables[0].NewRow();
            drDocss["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
            drDocss["IsProfilePicOrDoc"] = 1;
            drDocss["Files_Name"] = ViewState["OPPFile"] == null ? ViewState["PPFile"].ToString() : ViewState["OPPFile"].ToString();
 
            drDocss["Files_Unique_Name"] = ViewState["PPFile"] == null ? "" : ViewState["PPFile"].ToString();
            drDocss["FileExtension"] = fileExtensionPP;
            if (Session["filepaths"] != null)
            {
                drDocss["File_Path"] = Session["filepaths"].ToString();

            }
            else
            {
                drDocss["File_Path"] = "";
            }
            drDocss["FilesFor"] = "PP";
            dsDocss.Tables[0].Rows.Add(drDocss);
        }

        #endregion

        #region "Document Upload"

        //if (uploadDocuments.HasFile == true && checkValidDocOrNot())
        //{
        //    string DocumentName = uploadDocuments.PostedFile.FileName;

        //    FileType = Path.GetExtension(uploadDocuments.PostedFile.FileName);

        //    drDocss = dsDocss.Tables[0].NewRow();
        //    drDocss["IsProfilePicOrDoc"] = 0;
        //    UploadFiles();

        //    string upldPath = "";
        //    string currDir = System.IO.Directory.GetCurrentDirectory();
        //    upldPath = this.upldPath;
        //    Random randomNo1 = new Random();
        //    drDocss["Files_Name"] = DocumentName;
        //    drDocss["Files_Unique_Name"] = "HPU_Alumni_AD" + "_" + Guid.NewGuid().ToString() + "_" + randomNo1.Next(50000, 1000000) + "_" +
        //        DateTime.Now.ToString("yyyyMMddHHmmssfff") + "AD" + "_" + randomNo1.Next(33333, 454545454) + "_" + randomNo1.Next(999999, 15215454) + FileType;
        //    drDocss["FileExtension"] = FileType;
        //    drDocss["File_Path"] = upldPath;
        //    drDocss["FilesFor"] = "AD";
        //    dsDocss.Tables[0].Rows.Add(drDocss);
        //    bool IsExistPath = System.IO.Directory.Exists(upldPath);
        //    if (!IsExistPath)
        //        System.IO.Directory.CreateDirectory(upldPath);
        //    uploadDocuments.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
        //}
        //else
        //{
        //if (ViewState["id"] != null && ViewState["ADFile"] != null)
        //    {
        //        DataSet dsSS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

        //        if (dsSS != null && dsSS.Tables[2].Rows.Count > 0)
        //        {
        //            drDocss = dsDocss.Tables[0].NewRow();
        //            drDocss["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
        //            drDocss["IsProfilePicOrDoc"] = dsSS.Tables[2].Rows[0]["IsProfilePicOrDoc"].ToString();
        //            drDocss["Files_Name"] = dsSS.Tables[2].Rows[0]["Files_Name"].ToString();
        //            drDocss["Files_Unique_Name"] = dsSS.Tables[2].Rows[0]["Files_Unique_Name"].ToString(); ;
        //            drDocss["FileExtension"] = dsSS.Tables[2].Rows[0]["FileExtension"].ToString(); ;
        //            drDocss["File_Path"] = dsSS.Tables[2].Rows[0]["File_Path"].ToString();
        //            drDocss["FilesFor"] = dsSS.Tables[2].Rows[0]["FilesFor"].ToString();
        //            dsDocss.Tables[0].Rows.Add(drDocss);
        //        }
        //    }
        //}

        if (ViewState["id"] != null && ViewState["ADFile"] != null)
        {
            string fileExtensionAD = Path.GetExtension(ViewState["ADFile"].ToString());
            drDocss = dsDocss.Tables[0].NewRow();
            drDocss["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
            drDocss["IsProfilePicOrDoc"] = 0;
            drDocss["Files_Name"] = ViewState["OADFile"] == null ? ViewState["ADFile"].ToString() : ViewState["OADFile"].ToString();
            drDocss["Files_Unique_Name"] = ViewState["ADFile"] == null ? "" : ViewState["ADFile"].ToString();
            drDocss["FileExtension"] = fileExtensionAD;
            if (Session["filepaths"] != null)
            {
                drDocss["File_Path"] = Session["filepaths"].ToString();
            }
            else
            {
                drDocss["File_Path"] = "";
            }
           
            drDocss["FilesFor"] = "AD";
            dsDocss.Tables[0].Rows.Add(drDocss);
        }

        #endregion

        #region "Disability Document Upload"

        //if (fileDisabilityDoc.HasFile == true && checkValidDocOrNot())
        //{
        //    string DocumentName = fileDisabilityDoc.PostedFile.FileName;

        //    FileType = Path.GetExtension(fileDisabilityDoc.PostedFile.FileName);

        //    drDocss = dsDocss.Tables[0].NewRow();
        //    drDocss["IsProfilePicOrDoc"] = 0;
        //    UploadFiles();

        //    string upldPath = "";
        //    string currDir = System.IO.Directory.GetCurrentDirectory();
        //    upldPath = this.upldPath;
        //    Random randomNo1 = new Random();
        //    drDocss["Files_Name"] = DocumentName;
        //    drDocss["Files_Unique_Name"] = "HPU_Alumni_DD" + "_" + Guid.NewGuid().ToString() + "_" + randomNo1.Next(50000, 1000000) + "_" +
        //        DateTime.Now.ToString("yyyyMMddHHmmssfff") + "DD" + "_" + randomNo1.Next(33333, 454545454) + "_" + randomNo1.Next(999999, 15215454) + FileType;
        //    drDocss["FileExtension"] = FileType;
        //    drDocss["File_Path"] = upldPath;
        //    drDocss["FilesFor"] = "DD";
        //    dsDocss.Tables[0].Rows.Add(drDocss);
        //    bool IsExistPath = System.IO.Directory.Exists(upldPath);
        //    if (!IsExistPath)
        //        System.IO.Directory.CreateDirectory(upldPath);
        //    fileDisabilityDoc.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
        //}
        //else
        //{
        //if (ViewState["id"] != null && ViewState["DDFile"] != null)
        //    {
        //        DataSet dsSS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

        //        if (dsSS != null && dsSS.Tables[5].Rows.Count > 0)
        //        {
        //            drDocss = dsDocss.Tables[0].NewRow();
        //            drDocss["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
        //            drDocss["IsProfilePicOrDoc"] = dsSS.Tables[5].Rows[0]["IsProfilePicOrDoc"].ToString();
        //            drDocss["Files_Name"] = dsSS.Tables[5].Rows[0]["Files_Name"].ToString();
        //            drDocss["Files_Unique_Name"] = dsSS.Tables[5].Rows[0]["Files_Unique_Name"].ToString(); ;
        //            drDocss["FileExtension"] = dsSS.Tables[5].Rows[0]["FileExtension"].ToString(); ;
        //            drDocss["File_Path"] = dsSS.Tables[5].Rows[0]["File_Path"].ToString();
        //            drDocss["FilesFor"] = dsSS.Tables[5].Rows[0]["FilesFor"].ToString();
        //            dsDocss.Tables[0].Rows.Add(drDocss);
        //        }
        //    }
        //}

        if (ViewState["id"] != null && ViewState["DDFile"] != null)
        {
            string fileExtensionDD = Path.GetExtension(ViewState["DDFile"].ToString());
            drDocss = dsDocss.Tables[0].NewRow();
            drDocss["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
            drDocss["IsProfilePicOrDoc"] = 0;
            drDocss["Files_Name"] = ViewState["ODDFile"] == null ? ViewState["DDFile"].ToString() : ViewState["ODDFile"].ToString();
            drDocss["Files_Unique_Name"] = ViewState["DDFile"] == null ? "" : ViewState["DDFile"].ToString();
            drDocss["FileExtension"] = fileExtensionDD;
            if (Session["filepath"] != null)
            {
                drDocss["File_Path"] = Session["filepath"].ToString();
            }
            else
            {
                drDocss["File_Path"] = "";
            }
           
            drDocss["FilesFor"] = "DD";
            dsDocss.Tables[0].Rows.Add(drDocss);
        }

        #endregion

        if (dsDocss != null)
        {
            dsMain.Merge(dsDocss);
        }
        return dsMain;
    }

    private bool checkValidPhotoOrNot()
    {
        if (!flUpload.HasFile)
        {
            ClientMessaging("Please upload the required photo.!!!");
            flUpload.Focus();
            return false;
        }

        double filesize = flUpload.PostedFile.ContentLength;
        string Name = flUpload.PostedFile.FileName;

        if (Name.Length > 500)
        {
            ClientMessaging("Photo File Name Should not be more than 500 characters!");
            flUpload.Focus();
            return false;
        }

        if (filesize > (1 * 1024 * 1024))
        {
            ClientMessaging("Photo size should not be more than 1 MB !");
            flUpload.Focus();
            return false;
        }

        string FileType = flUpload.PostedFile.FileName != "" ? Path.GetExtension(flUpload.PostedFile.FileName) : System.IO.Path.GetExtension(ViewState["File2"].ToString());

        switch (FileType.ToLower())
        {
            case ".jpg":
                return true;
            case ".jpeg":
                return true;
            case ".bmp":
                return true;
            case ".png":
                return true;
            default:
                ClientMessaging("Only files .jpg, .jpeg, .bmp and .png extension are allowed.");
                return false;
        }
        return true;
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

        if (Names.Length > 500)
        {
            ClientMessaging("Documents File Name Should not be more than 500 characters!");
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
            case ".bmp":
                return true;
            case ".png":
                return true;
            case ".pdf":
                return true;
            default:
                ClientMessaging("Only files .jpg, .jpeg, .bmp, .png and .pdf extension are allowed.");
                return false;
        }
        return true;
    }

    #region Filedetail

    string upldPaths = "";

    protected DataSet GetUploadFile()
    {
        DataSet dsDtl = Dobj.GetSchema("ALM_AlumniRegistration_File_dtl");
        dsDtl.Tables[0].TableName = "ALM_AlumniRegistration_File_dtl";

        DataSet dsDocs = new DataSet();
        dsDocs.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
        string host = HttpContext.Current.Request.Url.Host;
        foreach (DataRow dr in dsDocs.Tables[0].Rows)
        {
            if (host == dr["Server_Ip"].ToString().Trim())
            {
                upldPath = dr["Physical_Path"].ToString().Trim();
                upldPath = upldPath + "/Alumni/StuImage/" + "";
                break;
            }
        }

        if (btnSubmit.CommandName.ToString().ToUpper() == "UPDATE")
        {
            if (flUpload.HasFile && flUpload.PostedFile.ContentLength <= 1048576)
            {
                if (ViewState["PPFile"].ToString() != null || ViewState["PPFile"].ToString() != "")
                {
                    DeleteFile(ViewState["PPFile"].ToString());
                }

                string Name = flUpload.PostedFile.FileName;
                FileType = Path.GetExtension(flUpload.PostedFile.FileName);

                UploadFiles();
                string upldPath = "";
                string currDir = System.IO.Directory.GetCurrentDirectory();
                upldPath = this.upldPath;
                Random randomNo = new Random();
                drimg = dsDocs.Tables[0].NewRow();
                drimg["IsProfilePicOrDoc"] = 1;
                drimg["Files_Name"] = Name;
                drimg["Files_Unique_Name"] = "HPU_Alumni_PP_" + Guid.NewGuid().ToString() + "_" + randomNo.Next(50000, 1000000) + "_" +
                    DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PP" + "_" + randomNo.Next(33333, 454545454) + "_" + randomNo.Next(999999, 15215454) + FileType;
                Fileuniquename = "HPU_Alumni_PP_" + "_" + Guid.NewGuid().ToString() + "_" + randomNo.Next(50000, 1000000) + "_" +
                   DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PP" + "_" + randomNo.Next(33333, 454545454) + "_" + randomNo.Next(999999, 15215454) + FileType;
                drimg["FileExtension"] = FileType;
                drimg["File_Path"] = upldPath;
                drimg["FilesFor"] = "PP";
                dsDocs.Tables[0].Rows.Add(drimg);

                bool IsExistPath = System.IO.Directory.Exists(upldPath);
                if (!IsExistPath)
                    System.IO.Directory.CreateDirectory(upldPath);
                flUpload.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());

            }
            else if (lnkprofile.Text != "" && (ViewState["PPFile"].ToString() != null || ViewState["PPFile"].ToString() != ""))
            {
                DataSet dsS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

                if (dsS != null && dsS.Tables[1].Rows.Count > 0)
                {
                    drimg = dsDocs.Tables[0].NewRow();
                    drimg["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
                    drimg["IsProfilePicOrDoc"] = dsS.Tables[1].Rows[0]["IsProfilePicOrDoc"].ToString();
                    drimg["Files_Name"] = dsS.Tables[1].Rows[0]["Files_Name"].ToString();
                    drimg["Files_Unique_Name"] = dsS.Tables[1].Rows[0]["Files_Unique_Name"].ToString(); ;
                    drimg["FileExtension"] = dsS.Tables[1].Rows[0]["FileExtension"].ToString(); ;
                    drimg["File_Path"] = dsS.Tables[1].Rows[0]["File_Path"].ToString();
                    drimg["FilesFor"] = dsS.Tables[1].Rows[0]["FilesFor"].ToString();
                    dsDocs.Tables[0].Rows.Add(drimg);
                }
            }

            if (uploadDocuments.HasFile && uploadDocuments.PostedFile.ContentLength <= 2097152)
            {
                if (ViewState["ADFile"].ToString() != null || ViewState["ADFile"].ToString() != "")
                {
                    DeleteFile(ViewState["ADFile"].ToString());
                }
                string DocumentName = uploadDocuments.PostedFile.FileName;

                FileType = Path.GetExtension(uploadDocuments.PostedFile.FileName);

                drimg = dsDocs.Tables[0].NewRow();
                drimg["IsProfilePicOrDoc"] = 0;
                UploadFiles();

                string upldPath = "";
                string currDir = System.IO.Directory.GetCurrentDirectory();
                upldPath = this.upldPath;
                Random randomNo1 = new Random();
                drimg["Files_Name"] = DocumentName;
                drimg["Files_Unique_Name"] = "HPU_Alumni_AD_" + Guid.NewGuid().ToString() + "_" + randomNo1.Next(50000, 1000000) + "_" +
                    DateTime.Now.ToString("yyyyMMddHHmmssfff") + "AD" + "_" + randomNo1.Next(33333, 454545454) + "_" + randomNo1.Next(999999, 15215454) + FileType;
                drimg["FileExtension"] = FileType;
                drimg["File_Path"] = upldPath;
                drimg["FilesFor"] = "AD";
                dsDocs.Tables[0].Rows.Add(drimg);

                bool IsExistPath = System.IO.Directory.Exists(upldPath);
                if (!IsExistPath)
                    System.IO.Directory.CreateDirectory(upldPath);
                uploadDocuments.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
            }
            else if (lnkDoc.Text != "" && (ViewState["ADFile"].ToString() != null || ViewState["ADFile"].ToString() != ""))
            {
                DataSet dsSS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

                if (dsSS != null && dsSS.Tables[2].Rows.Count > 0)
                {
                    drimg = dsDocs.Tables[0].NewRow();
                    drimg["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
                    drimg["IsProfilePicOrDoc"] = dsSS.Tables[2].Rows[0]["IsProfilePicOrDoc"].ToString();
                    drimg["Files_Name"] = dsSS.Tables[2].Rows[0]["Files_Name"].ToString();
                    drimg["Files_Unique_Name"] = dsSS.Tables[2].Rows[0]["Files_Unique_Name"].ToString(); ;
                    drimg["FileExtension"] = dsSS.Tables[2].Rows[0]["FileExtension"].ToString(); ;
                    drimg["File_Path"] = dsSS.Tables[2].Rows[0]["File_Path"].ToString();
                    drimg["FilesFor"] = dsSS.Tables[2].Rows[0]["FilesFor"].ToString();
                    dsDocs.Tables[0].Rows.Add(drimg);
                }
            }

            if (fileDisabilityDoc.HasFile && fileDisabilityDoc.PostedFile.ContentLength <= 2097152)
            {
                if (ViewState["DDFile"].ToString() != null || ViewState["DDFile"].ToString() != "")
                {
                    DeleteFile(ViewState["DDFile"].ToString());
                }
                string DocumentName = uploadDocuments.PostedFile.FileName;

                FileType = Path.GetExtension(uploadDocuments.PostedFile.FileName);

                drimg = dsDocs.Tables[0].NewRow();
                drimg["IsProfilePicOrDoc"] = 0;
                UploadFiles();

                string upldPath = "";
                string currDir = System.IO.Directory.GetCurrentDirectory();
                upldPath = this.upldPath;
                Random randomNo1 = new Random();
                drimg["Files_Name"] = DocumentName;
                drimg["Files_Unique_Name"] = "HPU_Alumni_DD_" + Guid.NewGuid().ToString() + "_" + randomNo1.Next(50000, 1000000) + "_" +
                    DateTime.Now.ToString("yyyyMMddHHmmssfff") + "DD" + "_" + randomNo1.Next(33333, 454545454) + "_" + randomNo1.Next(999999, 15215454) + FileType;
                drimg["FileExtension"] = FileType;
                drimg["File_Path"] = upldPath;
                drimg["FilesFor"] = "DD";
                dsDocs.Tables[0].Rows.Add(drimg);

                bool IsExistPath = System.IO.Directory.Exists(upldPath);
                if (!IsExistPath)
                    System.IO.Directory.CreateDirectory(upldPath);
                uploadDocuments.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
            }
            else if (lnkDisabilityDoc.Text != "" && (ViewState["DDFile"].ToString() != null || ViewState["DDFile"].ToString() != ""))
            {
                DataSet dsSS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

                if (dsSS != null && dsSS.Tables[2].Rows.Count > 0)
                {
                    drimg = dsDocs.Tables[0].NewRow();
                    drimg["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
                    drimg["IsProfilePicOrDoc"] = dsSS.Tables[5].Rows[0]["IsProfilePicOrDoc"].ToString();
                    drimg["Files_Name"] = dsSS.Tables[5].Rows[0]["Files_Name"].ToString();
                    drimg["Files_Unique_Name"] = dsSS.Tables[5].Rows[0]["Files_Unique_Name"].ToString(); ;
                    drimg["FileExtension"] = dsSS.Tables[5].Rows[0]["FileExtension"].ToString(); ;
                    drimg["File_Path"] = dsSS.Tables[5].Rows[0]["File_Path"].ToString();
                    drimg["FilesFor"] = dsSS.Tables[5].Rows[0]["FilesFor"].ToString();
                    dsDocs.Tables[0].Rows.Add(drimg);
                }
            }
        }
        ViewState["DSI"] = dsDtl;
        return dsDtl;
    }

    public string SetServiceDocs(string FileName)
    {
        string FolderName = @"/Alumni/StuImage";
        string host = HttpContext.Current.Request.Url.Host;
        string FilePath = ""; //"http://52.172.15.68/Published_App/FTPsite";
        DataSet dsFilepath = new DataSet();
        dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
        foreach (DataRow dr in dsFilepath.Tables[0].Rows)
        {
            if (host == dr["Server_Ip"].ToString().Trim())
            {
                if (host != "localhost")
                {
                    FilePath = Server.MapPath("~/Published_App/FTPsite");
                    FilePath = @FilePath + FolderName + @"/" + FileName;
                }
                else
                {
                    FilePath = dr["Physical_Path"].ToString().Trim();
                    FilePath = @FilePath + FolderName + @"/" + FileName;
                }
                return FilePath;
            }
        }
        return FilePath;
    }

    void DeleteFile(string file)
    {
        try
        {
            string path = SetServiceDocs(file);
            if (!string.IsNullOrEmpty(path))
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
        }
        catch
        {

        }
    }

    void UploadDocumentFiles(DataTable dt, string upldPaths, Anthem.FileUpload Uploaditem)
    {
        string currDir = System.IO.Directory.GetCurrentDirectory();
        bool IsExistPath = System.IO.Directory.Exists(upldPaths);

        if (!IsExistPath)
            System.IO.Directory.CreateDirectory(upldPaths);
        string filename = "";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            filename = dt.Rows[i]["uploadfile"].ToString();
            Uploaditem.SaveAs(upldPaths + filename);
        }
    }

    #endregion

    public StoredProcedure ALM_SP_AlumniRegistration_Update_By_Admin(int alumniid, string xmlDoc)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_Update_By_Admin", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", alumniid, DbType.Int32);
        sp.Command.AddParameter("@xmlDoc", xmlDoc, DbType.String);
        return sp;
    }

    protected void btnImgUpload_Click(object sender, EventArgs e)
    {
        if (!flUpload.HasFile)
        {
            ClientMessaging("Please select required photo.!!!");
            flUpload.Focus();
            return;
        }

        if (flUpload.HasFile)
        {
            FileType = Path.GetExtension(flUpload.PostedFile.FileName);

            if (flUpload.PostedFile.ContentLength > 1048576) // 1MB in bytes
            {
                ClientMessaging("Photo size should not be more than 1 MB.!");
                return;
            }

            if (flUpload.PostedFile.FileName.Length > 500)
            {
                ClientMessaging("Photo file name should not be more than 500 characters.!");
                flUpload.Focus();
                return;
            }

            if (FileType != ".jpg" && FileType != ".JPG" && FileType != ".jpeg" && FileType != ".JPEG"
            && FileType != ".bmp" && FileType != ".BMP" && FileType != ".png" && FileType != ".PNG")
            {
                ClientMessaging("Photo should be in required format.!");
                return;
            }
            else
            {
                string lFileName = "";
                Random rand = new Random();
                int n = rand.Next();

                string filename = "HPU_Alumni_PP_" + Guid.NewGuid().ToString() + "_" + rand.Next(50000, 1000000) + "_" +
                DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PP" + "_" + rand.Next(33333, 454545454) + "_" + rand.Next(999999, 15215454) + FileType;

                lFileName = FU_physicalPaths(flUpload, "Alumni\\StuImage\\", filename);
                ViewState["PPFile"] = lFileName;
                flUpload.SaveAs(Server.MapPath("ALM_Reports//" + lFileName));
                ClientMessaging("Photo Uploaded Successfully!!!.");
                string PPfilePathforhyperlink = "ALM_Reports//" + ViewState["PPFile"].ToString();
                string PPfilePath = Server.MapPath("ALM_Reports/") + ViewState["PPFile"].ToString();
                ViewState["PPfilePath_refletter_delete"] = PPfilePath;
                //hyperDisabilityDoc.Visible = true;
                //hyperDisabilityDoc.NavigateUrl = filePathforhyperlink;
                //hyperDisabilityDoc.Text = flUpload.FileName;
                imgProfileimg.ImageUrl = PPfilePathforhyperlink;
                hyperLinkProfile.Visible = true;
                hyperLinkProfile.NavigateUrl = PPfilePathforhyperlink;
                hyperLinkProfile.Text = flUpload.FileName;
                //lblPPDoc.Visible = true;
                //lblPPDoc.Text = flUpload.FileName;
                ViewState["OPPFile"] = flUpload.FileName;
                btnImgUpload.Enabled = true;
                btnImgUpload.Visible = true;
                btnRemovePP.Enabled = true;
                btnRemovePP.Visible = true;
            }
        }
        else
        {
            btnRemovePP.Enabled = false;
            btnRemovePP.Visible = false;
            ClientMessaging("Please select required photo.!!!");
        }
    }

    protected void btnDocUpload_Click(object sender, EventArgs e)
    {
        if (uploadDocuments.HasFile)
        {
            FileType = Path.GetExtension(uploadDocuments.PostedFile.FileName);

            if (uploadDocuments.PostedFile.ContentLength > 2097152) // 2MB in bytes
            {
                ClientMessaging("Documents size should not be more than 2 MB.!");
                return;
            }

            if (uploadDocuments.PostedFile.FileName.Length > 500)
            {
                ClientMessaging("Document file name should not be more than 500 characters.!");
                uploadDocuments.Focus();
                return;
            }

            if (FileType != ".pdf" && FileType != ".PDF" && FileType != ".jpg" && FileType != ".JPG" && FileType != ".jpeg" && FileType != ".JPEG"
                && FileType != ".bmp" && FileType != ".BMP" && FileType != ".png" && FileType != ".PNG")
            {
                ClientMessaging("Document should be in required format.!");
                return;
            }
            else
            {
                string lFileName = "";
                Random rand = new Random();
                int n = rand.Next();

                string filename = "HPU_Alumni_AD_" + Guid.NewGuid().ToString() + "_" + rand.Next(50000, 1000000) + "_" +
                DateTime.Now.ToString("yyyyMMddHHmmssfff") + "AD" + "_" + rand.Next(33333, 454545454) + "_" + rand.Next(999999, 15215454) + FileType;

                lFileName = FU_physicalPaths(uploadDocuments, "Alumni\\StuImage\\", filename);
                ViewState["ADFile"] = lFileName;
                uploadDocuments.SaveAs(Server.MapPath("ALM_Reports//" + lFileName));
                ClientMessaging("Documents Uploaded Successfully!!.");
                string ADfilePathforhyperlink = "ALM_Reports//" + ViewState["ADFile"].ToString();
                string ADfilePath = Server.MapPath("ALM_Reports/") + ViewState["ADFile"].ToString();
                ViewState["ADfilePath_refletter_delete"] = ADfilePath;
                hyperLinkADoc.Visible = true;
                hyperLinkADoc.NavigateUrl = ADfilePathforhyperlink;
                hyperLinkADoc.Text = uploadDocuments.FileName;
                //lblADDoc.Visible = true;
                //lblADDoc.Text = uploadDocuments.FileName;
                ViewState["OADFile"] = uploadDocuments.FileName;
                btnDocUpload.Enabled = true;
                btnDocUpload.Visible = true;
                btnRemoveAD.Enabled = true;
                btnRemoveAD.Visible = true;
            }
        }
        else
        {
            btnRemoveAD.Enabled = false;
            btnRemoveAD.Visible = false;
        }
    }

    protected void GrdFile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper().ToString() == "DELETEREC")
        {
            ViewState["idd"] = e.CommandArgument.ToString().Trim();
            string filePaths = ""; string fileNames = "";
            DataSet dsSS = new DataSet();
            dsSS = GetSelectedFileDetails(Convert.ToInt32(ViewState["idd"])).GetDataSet();
            filePaths = ReturnPath();

            if (dsSS != null && dsSS.Tables[0].Rows.Count > 0)
            {
                fileNames = dsSS.Tables[0].Rows[0]["Files_Unique_Name"].ToString();
                filePaths = filePaths + "\\Alumni\\StuImage\\" + fileNames;
                DeleteFile(fileNames);
            }
            Delete();
            GrdFile.Visible = true;
        }
    }

    private void Delete()
    {
        try
        {
            if ((DeleteSelectedFileDetails(Convert.ToInt32(ViewState["idd"].ToString()), Session["UserID"].ToString()).Execute()) > 0)
            {
                ClientMessaging("Record Deleted Successfully!");
                clearDocumentFiles();
                FillForUpdate();
            }
        }
        catch (SqlException ex)
        {
            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }

    public static StoredProcedure GetSelectedFileDetails(int fileID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_AlumniRegistration_File_Info_Select", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Pk_FileID", fileID, DbType.Int32);
        return sp;
    }

    public static StoredProcedure DeleteSelectedFileDetails(int fileID, string userID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_AlumniRegistration_File_Info_Delete", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Pk_FileID", fileID, DbType.Int32);
        sp.Command.AddParameter("@UserID", userID, DbType.String);
        return sp;
    }

    protected void btnRemoveDD_Click(object sender, EventArgs e)
    {
        if (ViewState["DDfilePath_refletter_delete"] != null && ViewState["DDfilePath_refletter_delete"].ToString() != "")
        {
            string DDfilePathToDelete = ViewState["DDfilePath_refletter_delete"].ToString();
            if (System.IO.File.Exists(DDfilePathToDelete))
            {
                try
                {
                    System.IO.File.Delete(DDfilePathToDelete);
                    ClientMessaging("Specially Abled Document Removed Successfully.!!!");
                }
                catch (Exception ex)
                {
                    //Label1.Text = "Error deleting file: " + ex.Message;
                }
            }
        }
        hyperLinkDDoc.Text = "";
        ViewState["DDfilePath_refletter_delete"] = null;
    }

    protected void btnRemovePP_Click(object sender, EventArgs e)
    {
        if (ViewState["PPfilePath_refletter_delete"] != null && ViewState["PPfilePath_refletter_delete"].ToString() != "")
        {
            string PPfilePathToDelete = ViewState["PPfilePath_refletter_delete"].ToString();
            if (System.IO.File.Exists(PPfilePathToDelete))
            {
                try
                {
                    System.IO.File.Delete(PPfilePathToDelete);
                    ClientMessaging("Photo Removed Successfully.!!!");
                }
                catch (Exception ex)
                {
                    //Label1.Text = "Error deleting file: " + ex.Message;
                }
            }
        }
        hyperLinkProfile.Text = "";
        ViewState["PPfilePath_refletter_delete"] = null;
        imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";
    }

    protected void btnRemoveAD_Click(object sender, EventArgs e)
    {
        if (ViewState["ADfilePath_refletter_delete"] != null && ViewState["ADfilePath_refletter_delete"].ToString() != "")
        {
            string ADfilePathToDelete = ViewState["ADfilePath_refletter_delete"].ToString();
            if (System.IO.File.Exists(ADfilePathToDelete))
            {
                try
                {
                    System.IO.File.Delete(ADfilePathToDelete);
                    ClientMessaging("Document Removed Successfully.!!!");
                }
                catch (Exception ex)
                {
                    //Label1.Text = "Error deleting file: " + ex.Message;
                }
            }
        }
        hyperLinkADoc.Text = "";
        ViewState["ADfilePath_refletter_delete"] = null;
    }
}