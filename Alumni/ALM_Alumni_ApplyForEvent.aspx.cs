using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Alumni_ALM_Alumni_ApplyForEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string s = Session.SessionID;
        if (Session["EventPmtDtl"] != null)
        {
            if ((DataTable)Session["EventPmtDtl"] != null)
            {
                DataTable dt = (DataTable)Session["EventPmtDtl"];
                if(dt.Rows.Count>0)
                {
                    if (dt.Rows[0]["Pk_AlumniId"].ToString() == Session["AlumniId"].ToString())
                    {
                        lblEventName.Text = "<B>" + dt.Rows[0]["EventName"].ToString() + "</B>";
                        lblEventCharges.Text = "<B>" + dt.Rows[0]["EventAmount"].ToString() + " (INR) </B>";
                    }
                    else
                    {
                        Session["EventPmtDtl"] = null;
                        Response.Redirect("ALM_Alumni_Home.aspx");
                    }
                   
                }
            }
            else
            {
                Session["EventPmtDtl"] = null;
                Response.Redirect("ALM_Alumni_Home.aspx");
            }

            
        }
        else
        {
            Session["EventPmtDtl"] = null;
            Response.Redirect("ALM_Alumni_Home.aspx");
        }
    }
    protected void btnPay_Click(object sender, EventArgs e)
    {
        Session["EventPmtDtl"] = null;
        Response.Redirect("ALM_Alumni_Home.aspx");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["EventPmtDtl"] = null;
        Response.Redirect("ALM_Alumni_Home.aspx");
    }
}