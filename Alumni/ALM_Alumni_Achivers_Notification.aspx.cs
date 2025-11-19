using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataAccessLayer;
//using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Drawing;
using SubSonic;
using System.Text;
using System.Net.Mail;
using System.Data.SqlClient;


public partial class Alumni_ALM_Alumni_Achivers_Notification : System.Web.UI.Page
{

    #region "Common Objects"
    DataAccess Dobj = new DataAccess();
    crypto crp = new crypto();
    CustomMessaging eobj = new CustomMessaging();
    CommonFunction cfObj = new CommonFunction();
    #endregion
    private bool IspageReferesh = false;

    #region "Page Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["postid"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postid"].ToString();
            Bind_Acheivers_Dtls();
        }
        else
        {
            if (ViewState["postid"].ToString() != Session["postid"].ToString())
            {
                IspageReferesh = true;
            }
            Session["postid"] = System.Guid.NewGuid().ToString();
            ViewState["postid"] = Session["postid"];
        }


    }
    #endregion


    #region "Bind Grid of Acheivers and Processed Acheivers"
    void Bind_Acheivers_Dtls()
    {
        DataSet ds = IUMSNXG.SP.ALM_SP_GetAcheiversAnd_Procesed_Acheivers().GetDataSet();

        //Acheivers data
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvAchievers.SelectedIndex = -1;
            gvAchievers.DataSource = ds.Tables[0];
            gvAchievers.DataBind();
        }
        else
        {
            gvAchievers.DataSource = null;
            gvAchievers.DataBind();
        }

        //Processed Acheivers data
        if (ds.Tables[1].Rows.Count > 0)
        {
            gvProcAchievers.SelectedIndex = -1;
            gvProcAchievers.DataSource = ds.Tables[1];
            gvProcAchievers.DataBind();
        }
        else
        {
            gvProcAchievers.DataSource = null;
            gvProcAchievers.DataBind();
        }
       
    }
    #endregion

    #region "Process Event"
    protected void btnprocess_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IspageReferesh)
            {
                int CountCheckedItem = 0;
                foreach (GridViewRow gr in gvAchievers.Rows)
                {
                    
                    CheckBox check = (CheckBox)(gr.FindControl("chkselect"));
                    if (check.Checked)
                    {
                        CountCheckedItem++;
                    }

                }

                if (CountCheckedItem==0)
                {
                    Client_Messaging("Select Atleast one Acheiver");
                    gvAchievers.HeaderRow.FindControl("chkSelectAll").Focus();
                    return;
                }
                Save();
            }
        }
        catch (SqlException exp) 
        {
            lblMsg.Text = eobj.ShowSQLErrorMsg(exp.Message.ToString(), "", exp);
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }


    private void Save()
    {
           DataSet ds=Dobj.GetSchema("ALM_AlumniRegistration");
           foreach(GridViewRow gr in gvAchievers.Rows)
           {
               CheckBox check=(CheckBox)(gr.FindControl("chkselect"));
               if(check.Checked)
               {
                   Label lblId=(Label)(gr.FindControl("lblid"));
                   DataRow dr=ds.Tables[0].NewRow();
                   dr["pk_alumniid"]=Convert.ToInt32(lblId.Text.Trim());
                   ds.Tables[0].Rows.Add(dr);
               }

           }
        string xml=ds.GetXml();


        if ((IUMSNXG.SP.Alm_Alumni_Acheivers_Processed_Unprocess_upd(xml, Session["userid"].ToString(),0,"P").Execute()) > 0)
        {
            //lblMsg.Text = eobj.ShowMessage("S");

            Client_Messaging("Achievers Processed Successfully!");

            Clear();
        }
    }
    #endregion
    #region "Common Methods and Reset Event"
    private void Clear()
    {
        lblMsg.Text = "";
        Bind_Acheivers_Dtls();
        // chkbxactive.Checked = false;
        Anthem.Manager.IncludePageScripts = true;
     

    }
    protected void Client_Messaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }
   
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Clear();
       
       
    }
    #endregion

    #region "GridView Row Event To delete Record"
    protected void gvProcAchievers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "DELETEREC")
        {
            if ((IUMSNXG.SP.Alm_Alumni_Acheivers_Processed_Unprocess_upd("", Session["userid"].ToString(), Convert.ToInt32(e.CommandArgument), "U").Execute()) > 0)
            {

                Client_Messaging("Achievers Deleted Successfully!");
                Clear();
            }
        }
    }
    #endregion
}