using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UMMBusinessLayer;
using InfoSoftGlobal;
using System.Data;
using SubSonic;
public partial class UMM_UC_ALM_DashBoard : System.Web.UI.UserControl
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            V_txtfrmdate.Text = DateTime.Now.AddYears(-1).ToString("dd/MM/yyyy");
            V_txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            Create_Chart_StudentEventSession_Wise_Count();
           Create_Chart_EventParticipation_GenderWise();
        }
    }
    protected void ddlChartType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Create_Chart_StudentEventSession_Wise_Count();
        Create_Chart_EventParticipation_GenderWise();
        
    }

    public string Create_Chart_StudentEventSession_Wise_Count()
    {
        //DateTime dtf = String.IsNullOrEmpty(R_txtfrmdate.Text) ? DateTime.Now : Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(R_txtfrmdate.Text));
        // DateTime dte = String.IsNullOrEmpty(R_txttodate.Text) ? DateTime.Now : Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(R_txttodate.Text));
        string strXML;
        strXML = "";
        string charttype = "";
        charttype = ddlChartType.SelectedValue;
        strXML += "<graph caption='' xAxisName='' yAxisName='Count'  bgColor='99CCFF,FFFFFF' bgAlpha='40,100' bgRatio='0,100'  >";

        DateTime startdate = String.IsNullOrEmpty(V_txtfrmdate.Text) ? DateTime.Now : Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(V_txtfrmdate.Text));
        DateTime enddate = String.IsNullOrEmpty(V_txttodate.Text) ? DateTime.Now : Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(V_txttodate.Text));
        DataSet ds = IUMSNXG.SP.Alm_Alumni_Admin_HomeDashBoard_sel(startdate,enddate).GetDataSet();

        string lbl = "", data = "";

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
             
                strXML += "<set name='Unpaid Event' value='" + ds.Tables[0].Rows[0]["TotalUnpaidEventCount"].ToString() + "' />";
                strXML += "<set name='Participated In Unpaid event' value='" + ds.Tables[0].Rows[0]["No_Of_Applied_UnPaidEvent"].ToString() + "' />";
                strXML += "<set name='Paid Event' value='" + ds.Tables[0].Rows[0]["TotalPaidEvent"].ToString() + "' />";
                strXML += "<set name='Participated in Paid Event' value='" + ds.Tables[0].Rows[0]["No_Of_Applied_PaidEvent"].ToString() + "' />";
            }
            strXML += "</graph>";
            return FusionCharts.RenderChartHTML("../FCharts/" + charttype, "", strXML, "Count", "550", "400", false, false, true);

        }
        strXML += "</graph>";
        return FusionCharts.RenderChartHTML("../FCharts/" + charttype, "", strXML, "Count", "550", "400", false, false, true);

    }

    public string Create_Chart_EventParticipation_GenderWise()
    {
        //DateTime dtf = String.IsNullOrEmpty(R_txtfrmdate.Text) ? DateTime.Now : Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(R_txtfrmdate.Text));
        // DateTime dte = String.IsNullOrEmpty(R_txttodate.Text) ? DateTime.Now : Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(R_txttodate.Text));
        string strXML;
        strXML = "";
        string charttype = "";
        charttype = ddlChartType.SelectedValue;
        strXML += "<graph caption='' xAxisName='' yAxisName='Count'  bgColor='99CCFF,FFFFFF' bgAlpha='40,100' bgRatio='0,100'  >";

        DateTime startdate = String.IsNullOrEmpty(V_txtfrmdate.Text) ? DateTime.Now : Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(V_txtfrmdate.Text));
        DateTime enddate = String.IsNullOrEmpty(V_txttodate.Text) ? DateTime.Now : Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(V_txttodate.Text));
        DataSet ds = IUMSNXG.SP.Alm_Alumni_Admin_HomeDashBoard_sel(startdate, enddate).GetDataSet();

        string lbl = "", data = "";

        if (ds.Tables[1].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {


                strXML += "<set name='Male' value='" + ds.Tables[1].Rows[0]["Total_Male"].ToString() + "' />";
                strXML += "<set name='Female' value='" + ds.Tables[1].Rows[0]["Total_FeMale"].ToString() + "' />";
            }
            strXML += "</graph>";
            return FusionCharts.RenderChartHTML("../FCharts/" + charttype, "", strXML, "Count", "400", "400", false, false, true);

        }
        strXML += "</graph>";
        return FusionCharts.RenderChartHTML("../FCharts/" + charttype, "", strXML, "Count", "400", "400", false, false, true);

    }
    
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        DateTime dtf = String.IsNullOrEmpty(V_txtfrmdate.Text) ? DateTime.Now : Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(V_txtfrmdate.Text));
        DateTime dte = String.IsNullOrEmpty(V_txttodate.Text) ? DateTime.Now : Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(V_txttodate.Text));
        if (DateTime.Compare(dtf, dte) < 1)
        {
            Create_Chart_StudentEventSession_Wise_Count();
           Create_Chart_EventParticipation_GenderWise();
            
        } 
        else
        {
            lblMsg.Text = "From Date should be less then or equal to To Date ";
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        V_txtfrmdate.Text = DateTime.Now.AddYears(-1).ToString("dd/MM/yyyy");
        V_txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        ddlChartType.SelectedIndex = 0;
        Create_Chart_StudentEventSession_Wise_Count();
        Create_Chart_EventParticipation_GenderWise();
       


        //R_txtfrmdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //R_txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

    }

}