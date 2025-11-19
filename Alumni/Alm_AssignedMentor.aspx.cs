//Created By: Aditya Sharma
//Created Date :01/05/2023
//Purpose : Do Action on Mentees Request
//Table Used : Alm_MenteeReq_Mst,alm_problem_facing_tran,Alm_communicate_tran
using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DataAccessLayer;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using System.Net;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Mime;

public partial class Alumni_Alm_AssignedMentor : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
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
    /// Sp of pending Request
    /// </summary>
    /// <returns></returns>
    private DataSet GetmenteeDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("Alm_GetMenteeReqDetails", values, names, types);
    }

    /// <summary>
    /// Sp of Rejected Request
    /// </summary>
    /// <returns></returns>
    private DataSet GetmenteeRejectDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("Alm_Get_RejectedMenteeReqDetails", values, names, types);
    }

    /// <summary>
    /// Sp of Approved Request
    /// </summary>
    /// <returns></returns>
    private DataSet Getmentor_assignedDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("Alm_Get_assignedMenteeReqDetails", values, names, types);
    }

    /// <summary>
    /// Bind Dropdown Sp
    /// </summary>
    /// <returns></returns>
    private DataSet Alm_Bind_alumniforDDL()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("Alm_GetMentors", values, names, types);
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
    public int ins_upd_delevents_dtls(ref string Message)
    {
        try
        {
            names.Clear(); values.Clear(); types.Clear();
            names.Add("@doc"); types.Add(SqlDbType.VarChar); values.Add(xmldoc);
            names.Add("@PK_Mentee_id"); values.Add(Pk_id); types.Add(SqlDbType.Int);
            if (Dobj.ExecuteTransactionMsg("ALM_UpdMentee_Status", values, names, types, ref Message) > 0)
            {
                Message = Dobj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch { throw; }
    }

    private Boolean IsPageRefresh = false;

    public string xmldoc { get; private set; }
    public object Pk_id { get; private set; }

    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        IsPageRefresh = false;
        if (!IsPostBack)
        {
            FillGrid();
            Get_AssignedDetails();
            GetRejectDetails();
        }
    }

    /// <summary>
    /// Bind gvDetails Gv
    /// </summary>
    void FillGrid()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = GetmenteeDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                btnSave.Visible = true;
            }
            else
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                btnSave.Visible = false;
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = ex.Message;
        }
    }

    /// <summary>
    /// Bind Gvapproved_Details GV
    /// </summary>
    void Get_AssignedDetails()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = Getmentor_assignedDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Gvapproved_Details.DataSource = ds;
                Gvapproved_Details.DataBind();
                btnSave.Visible = true;
            }
            else
            {
                Gvapproved_Details.DataSource = ds;
                Gvapproved_Details.DataBind();
                btnSave.Visible = false;
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = ex.Message;
        }
    }

    /// <summary>
    /// Bind GVRejectDetails GV
    /// </summary>
    void GetRejectDetails()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = GetmenteeRejectDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVRejectDetails.DataSource = ds;
                GVRejectDetails.DataBind();
                btnSave.Visible = true;
            }
            else
            {
                GVRejectDetails.DataSource = ds;
                GVRejectDetails.DataBind();
                btnSave.Visible = false;
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = ex.Message;
        }
    }

    /// <summary>
    /// Row Command of gvDetails GV
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";

        if (e.CommandName.ToUpper().ToString() == "SELECT")
        {
            //  ViewState["id"] = e.CommandArgument.ToString().Trim();
            //ViewCompDetails();
        }
    }

    /// <summary>
    /// On click Event on btnSave
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {

        bool lApproved = false; int isMentor;

        foreach (GridViewRow gr in gvDetails.Rows)
        {
            Anthem.DropDownList ddlMentors = (Anthem.DropDownList)gr.FindControl("ddlMentors");
            Anthem.RadioButtonList Rd_Approve = (Anthem.RadioButtonList)gr.FindControl("Rd_Approve");

            if (Rd_Approve.SelectedIndex != -1)
            {
                lApproved = true;
            }

            if (Rd_Approve.SelectedIndex == 0 && ddlMentors.SelectedIndex == 0)
            {
                ClientMessaging("Kindly Select Mentor!!");
                ddlMentors.Focus();
                return;
            }
        }
        if (!lApproved)
        {
            ClientMessaging("Kindly take atleast one action");
            return;
        }
        try
        {
            lblMsg.Text = "";
            // if (R_txtDescription.Text != "")
            {
                if (btnSave.CommandName.ToUpper().ToString() == "SAVE")
                {
                    string Message = "";
                    DataSet dsMain = new DataSet();
                    dsMain = Getmain();
                    string doc = "";
                    doc = dsMain.GetXml();
                    xmldoc = doc;

                    if (ins_upd_delevents_dtls(ref Message) > 0)
                    {
                        //Clear();
                        FillGrid();
                        ClientMessaging(" Approved/Rejected Successfully!");
                        Get_AssignedDetails();
                        GetRejectDetails();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }

    /// <summary>
    /// Getmain of Btn Saved
    /// </summary>
    /// <returns></returns>
    protected DataSet Getmain()
    {
        DataSet dsmain = Dobj.GetSchema("Alm_MenteeReq_Mst");
        dsmain.Tables[0].TableName = "Alm_MenteeReq_Mst";

        int i = 0;
        string str = "";
        string[] str1;
        foreach (GridViewRow gr in gvDetails.Rows)
        {
            DataRow dr_main = dsmain.Tables[0].NewRow();
            dr_main["Pk_Mentee_Reqid"] = Convert.ToInt32(gvDetails.DataKeys[gr.RowIndex].Value);
            Pk_id = dr_main["Pk_Mentee_Reqid"];
            Anthem.RadioButtonList Rd_Approve = (Anthem.RadioButtonList)gr.FindControl("Rd_Approve");
            if (Rd_Approve.SelectedValue == "" || Rd_Approve.SelectedValue == "False")
            {
                if (Rd_Approve.SelectedValue == "")
                    dr_main["isApproved"] = DBNull.Value;
                else
                {
                    dr_main["isApproved"] = Convert.ToBoolean(Rd_Approve.SelectedValue);

                }
                dr_main["Remarks"] = ((TextBox)gr.FindControl("txtremarks")).Text;
                dr_main["assign_Mentor"] = ((DropDownList)gr.FindControl("ddlMentors")).SelectedItem.Text;
            }
            else if (Rd_Approve.SelectedValue != "" && Rd_Approve.SelectedValue == "True")
            {
                dr_main["isApproved"] = Convert.ToBoolean(Rd_Approve.SelectedValue);
                dr_main["Remarks"] = ((TextBox)gr.FindControl("txtremarks")).Text;
                dr_main["assign_Mentor"] = ((DropDownList)gr.FindControl("ddlMentors")).SelectedItem.Text;
                dr_main["MentorID"] = Convert.ToInt32(((DropDownList)gr.FindControl("ddlMentors")).SelectedValue);
            }
            dsmain.Tables[0].Rows.Add(dr_main);
            i++;
        }
        return dsmain;
    }

    /// <summary>
    /// Bind Mentors Dropdown in Gvdetails Gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataSet ds = Alm_Bind_alumniforDDL();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DropDownList ddlMentors = (e.Row.FindControl("ddlMentors") as DropDownList);
                ddlMentors.DataSource = ds.Tables[0];
                ddlMentors.DataTextField = "alumni_name";
                ddlMentors.DataValueField = "pk_alumniid";
                ddlMentors.DataBind();
                ddlMentors.Items.Insert(0, new ListItem("-- Select --", "0"));
                //ddlMentors.SelectedIndex = 0;
            }
        }
    }

    /// <summary>
    /// Page Indexing on all GV
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Gvapproved_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gvapproved_Details.PageIndex = e.NewPageIndex;
        Get_AssignedDetails();
    }

    protected void GVRejectDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVRejectDetails.PageIndex = e.NewPageIndex;
        GetRejectDetails();
    }
    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    // End Section of Page Indexing

    /// <summary>
    /// View Button on Pending List
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void VIEW_Click(object sender, EventArgs e)
    {
        string a = crp.Encrypt(((LinkButton)sender).CommandArgument.ToString());
        a = a.Replace("+", "%2B");
        //  Response.Redirect("~//Placement///PLC_View_CompanyProfile.aspx?id=" + a + "");
        Response.Redirect("~//Alumni//Alm_viewMenteeRequestform_Admin.aspx?id=" + a + "");
    }
    /// <summary>
    /// View Button on Assigned List
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void VIEWs_Click(object sender, EventArgs e)
    {
        string a = crp.Encrypt(((LinkButton)sender).CommandArgument.ToString());
        a = a.Replace("+", "%2B");
        //  Response.Redirect("~//Placement///PLC_View_CompanyProfile.aspx?id=" + a + "");
        Response.Redirect("~//Alumni//Alm_viewMenteeRequestform_Admin.aspx?id=" + a + "");
    }

    /// <summary>
    /// View Button on Rejected List
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkrejectview_Click(object sender, EventArgs e)
    {
        string a = crp.Encrypt(((LinkButton)sender).CommandArgument.ToString());
        a = a.Replace("+", "%2B");
        //  Response.Redirect("~//Placement///PLC_View_CompanyProfile.aspx?id=" + a + "");
        Response.Redirect("~//Alumni//Alm_viewMenteeRequestform_Admin.aspx?id=" + a + "");
    }
}