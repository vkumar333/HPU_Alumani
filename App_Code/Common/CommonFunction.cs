using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using System.Globalization;
using Microsoft.VisualBasic;
using System.Web.Mail;
using System.Net.Mail;
using System.Net;
using SubSonic;
using IUMSNXG;
using DataAccessLayer;
using System.IO;

class MainClass
{
    public static void Main()
    {

    }
}
/// <summary>
/// Created By:Mukund
/// Date: 
/// This file is Created for IUMS for providing Common functions for entire project 
/// </summary>
///
public sealed class CommonFunction
{
    public static System.Configuration.AppSettingsReader configSetting = new System.Configuration.AppSettingsReader();
    public static string ConnStr = ((string)(configSetting.GetValue("dbConnString", typeof(string))));

    /// <summary>
    /// Created By: Manish
    /// Modified By: Mukund
    /// Purpose: For Getting the Connection
    /// Date:20/June/2006
    /// </summary>
    /// <returns>SqlConnection</returns>

    public static System.Data.SqlClient.SqlConnection Connect()
    {
        return (new SqlConnection(ConnStr));
    }

    public string ReturnDrivepath()
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
                    return dr["Physical_Path"].ToString().Trim();
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Created by:Vandana 
    /// MOdified:
    /// Dated:12 nov 2008
    /// Description:To fill from date and to date in search form
    /// </summary>
    /// <param name="Text"></param>
    /// <returns>bool</returns>
    public static void fiidate(TextBox frmdate, TextBox todate)
    {
        frmdate.Text = "01" + "/" + DateTime.Now.ToString("MM/yyyy");
        frmdate.Text = frmdate.Text.Substring(0, 2) + "/" + frmdate.Text.Substring(3, 2) + "/" + frmdate.Text.Substring(6, 4);
        todate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        todate.Text = todate.Text.Substring(0, 2) + "/" + todate.Text.Substring(3, 2) + "/" + todate.Text.Substring(6, 4);
    }



    /// <summary>
    /// Created By: Mukund

    /// Purpose: For Getting the date in mm/dd/yyyy format and checking the validity of the date for compulsory field
    /// Date:31/Aug/2006
    /// </summary>
    /// <param name="Date"></param>
    /// <returns>string date</returns>
    public static string MessageAfterTransaction(char type)
    {

        try
        {
            switch (type)
            {
                case 'D':
                    return "<script>alert('Record deleted successfully')</script>";
                    break;
                case 'S':
                    return "<script>alert('Record saved successfully')</script>";
                    break;
                case 'U':
                    return "<script>alert('Record updated successfully')</script>";
                    break;
                case 'E':
                    return "<script>alert('Record not found')</script>";
                    break;
                default:
                    return "";
            }

        }

        catch (FormatException e)
        {
            throw (new FormatException("Invalid Date"));
        }
        catch (Exception e)
        {
            throw (new Exception("Date is blank"));
        }

    }
   public static  ArrayList name = new ArrayList();
    public static ArrayList type = new ArrayList();
    public static ArrayList value = new ArrayList();

    public static void clear()
    {
        name.Clear(); value.Clear(); type.Clear();

    }

    public static DataSet GetFinYear(string Date )
    {
        DataSet ds = new DataSet();
        DataAccess dobj = new DataAccess();
        clear();
        name.Add("@Date"); value.Add(Date); type.Add(SqlDbType.VarChar);
        return dobj.GetDataSet("Asst_Financial_Year_GetDates",value,name,type);
    }


    public static bool CheckDateWithinFY(TextBox date)
    {
        try
        {
            DataAccess dobj = new DataAccess();

           
           

            IDataReader rdr = dobj.GetDataReader("Asst_Financial_Year_GetDates");
            DataTable dt = new DataTable();

            dt.Load(rdr);


            if (date.Text.Trim().ToString() != "")
            {
                System.IFormatProvider cultinfo = new CultureInfo("fr-FR", true);

                string date1 = DateTime.Parse(date.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");

                string [] dateformate = date.Text.ToString().Split('/');

                 date1 = (dateformate[2] + '/' + dateformate[1] + '/' + dateformate[0]).ToString();


                //string ltodate1 = DateTime.Parse(todate.Text.ToString(), cultinfo).ToString("yyyy/MM/dd");
                int d1; //ltdate1;
                d1 = Convert.ToInt32(date1.Replace("/", ""));
                // ltdate1 = Convert.ToInt32(ltodate1.Replace("/", ""));

                //DateTime fromdate = DateTime.Parse((frmdate.Text.ToString()));
                //DateTime tdate = DateTime.Parse((todate.Text.ToString()));

                string lfromdate2 = dt.Rows[0]["Date1"].ToString();// DateTime.Parse(CommonCode.DateFormats.Date_DBToFront(dt.Rows[0]["Date1"].ToString()), System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                string ltodate2 = dt.Rows[0]["Date2"].ToString();//DateTime.Parse(CommonCode.DateFormats.Date_DBToFront(dt.Rows[0]["Date2"].ToString()), System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");

                int lfrd2, ltdate2;
                lfrd2 = Convert.ToInt32(lfromdate2.Replace("-", ""));
                ltdate2 = Convert.ToInt32(ltodate2.Replace("-", ""));

                //DateTime date1 = DateTime.Parse(CommonCode.DateFormats.Date_FrontToDB_R(ds.Tables[0].Rows[0]["Date1"].ToString()));
                //DateTime date2 = DateTime.Parse(CommonCode.DateFormats.Date_FrontToDB_R(ds.Tables[0].Rows[0]["Date2"].ToString()));

                if (d1 < lfrd2 || d1 > ltdate2)
                {
                    return true;
                }
                else
                {
                    return false;
                }





            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            return true;
        }
    }

    public static string MessageReport()
    {
        return "<script>alert('Record not found')</script>";
    }

    /// <summary>
    /// Created By: Mukund

    /// Purpose: For Getting the date in mm/dd/yyyy format and checking the validity of the date for compulsory field
    /// Date:31/Aug/2006
    /// </summary>
    /// <param name="Date"></param>
    /// <returns>string date</returns>
    public static string SetDateFormat_R(TextBox R_TBox)
    {
        System.IFormatProvider cultinfo = new CultureInfo("fr-FR", true);
        try
        {
            if (R_TBox.Text.Trim().ToString() == "")
            {
                throw (new Exception());
            }
            else
            {
                string NewDate = DateTime.Parse(R_TBox.Text.Trim().ToString(), cultinfo).ToString("MM/dd/yyyy");
                //  System.DateTime dt = System.DateTime.Parse(NewDate);
                return NewDate;
            }

        }
        catch (FormatException e)
        {
            throw (new FormatException("Invalid Date"));
        }
        catch (Exception e)
        {
            throw (new Exception("Date is blank"));
        }

    }
    /// <summary>
    /// Created By: Vikrant
    /// Modified By: Mukund
    /// Purpose: For Clearing all the textboxes, setting the drop down on index 0 for that page
    /// Date:20/June/2006
    /// </summary>
    /// <param name="webpage"></param>
    public void ClearTextSetDropDown(System.Web.UI.Page webpage)
    {
        for (int i = 0; i < webpage.Form.Controls.Count; i++)
        {
            if (webpage.Form.Controls[i].GetType().Name == "TextBox")
            {
                TextBox txt = ((TextBox)webpage.Form.Controls[i]);
                txt.Text = "";
                if (txt.ID.Substring(0, 2) == "V_")
                {
                    txt.Text = CommonCode.DateFormats.Date_DBToFront(System.DateTime.Today.ToShortDateString());
                }
            }
            if (webpage.Form.Controls[i].GetType().Name == "DropDownList")
                if (((DropDownList)webpage.Form.Controls[i]).SelectedIndex != -1)
                    ((DropDownList)webpage.Form.Controls[i]).SelectedIndex = 0;
            if (webpage.Form.Controls[i].GetType().Name == "ContentPlaceHolder")
            {
                for (int j = 0; j < webpage.Form.Controls[i].Controls.Count; j++)
                {
                    if (webpage.Form.Controls[i].Controls[j].GetType().Name == "TextBox")
                    {
                        TextBox txt = ((TextBox)webpage.Form.Controls[i].Controls[j]);
                        txt.Text = "";
                        if (txt.ID.Substring(0, 2) == "V_")
                        {
                            txt.Text = CommonCode.DateFormats.Date_DBToFront(System.DateTime.Today.ToShortDateString());
                        }

                    }
                    if (webpage.Form.Controls[i].Controls[j].GetType().Name == "DropDownList")
                        if (((DropDownList)webpage.Form.Controls[i].Controls[j]).SelectedIndex != -1)
                            ((DropDownList)webpage.Form.Controls[i].Controls[j]).SelectedIndex = 0;
                    if (webpage.Form.Controls[i].Controls[j].GetType().Name == "Panel")
                    {
                        for (int k = 0; k < webpage.Form.Controls[i].Controls[j].Controls.Count; k++)
                        {
                            if (webpage.Form.Controls[i].Controls[j].Controls[k].GetType().Name == "TextBox")
                            {
                                TextBox txt = ((TextBox)webpage.Form.Controls[i].Controls[j].Controls[k]);
                                txt.Text = "";
                                if (txt.ID.Substring(0, 2) == "V_")
                                {
                                    txt.Text = CommonCode.DateFormats.Date_DBToFront(System.DateTime.Today.ToShortDateString());
                                }

                            }
                            if (webpage.Form.Controls[i].Controls[j].Controls[k].GetType().Name == "DropDownList")
                                if (((DropDownList)webpage.Form.Controls[i].Controls[j].Controls[k]).SelectedIndex != -1)
                                    ((DropDownList)webpage.Form.Controls[i].Controls[j].Controls[k]).SelectedIndex = 0;
                        }
                    }
                }
            }


        }
    }


    /// <summary>
    /// Created By: Vikrant
    /// Modified By: Mukund
    /// Purpose: For Clearing all the textboxes, setting the drop down on index 0 for that page
    /// Date:20/June/2006
    /// </summary>
    /// <param name="webpage"></param>
    public void ClearTextBox(System.Web.UI.Page webpage)
    {
        for (int i = 0; i < webpage.Form.Controls.Count; i++)
        {
            if (webpage.Form.Controls[i].GetType().Name == "TextBox")
            {
                TextBox txt = ((TextBox)webpage.Form.Controls[i]);
                txt.Text = "";
                if (txt.ID.Substring(0, 2) == "V_")
                {
                    txt.Text = CommonCode.DateFormats.Date_DBToFront(System.DateTime.Today.ToShortDateString());
                }
            }
            if (webpage.Form.Controls[i].GetType().Name == "DropDownList")
                if (((DropDownList)webpage.Form.Controls[i]).SelectedIndex != -1)
                    ((DropDownList)webpage.Form.Controls[i]).SelectedIndex = 0;
            if (webpage.Form.Controls[i].GetType().Name == "ContentPlaceHolder")
            {
                for (int j = 0; j < webpage.Form.Controls[i].Controls.Count; j++)
                {
                    if (webpage.Form.Controls[i].Controls[j].GetType().Name == "TextBox")
                    {
                        TextBox txt = ((TextBox)webpage.Form.Controls[i].Controls[j]);
                        txt.Text = "";
                        if (txt.ID.Substring(0, 2) == "V_")
                        {
                            txt.Text = CommonCode.DateFormats.Date_DBToFront(System.DateTime.Today.ToShortDateString());
                        }

                    }

                    if (webpage.Form.Controls[i].Controls[j].GetType().Name == "DropDownList")
                        if (((DropDownList)webpage.Form.Controls[i].Controls[j]).SelectedIndex != -1)
                            ((DropDownList)webpage.Form.Controls[i].Controls[j]).SelectedIndex = 0;
                }
            }


        }
    }

    /// <summary>
    /// Created By: Mukund
    /// Modified By: 
    /// Purpose: For Clearing all the textboxes only
    /// Date:07/Aug/2006
    /// </summary>
    /// <param name="webpage"></param>
    public void ClearTextOnly(System.Web.UI.Page webpage)
    {
        for (int i = 0; i < webpage.Form.Controls.Count; i++)
        {
            if (webpage.Form.Controls[i].GetType().Name == "TextBox")
            {
                TextBox txt = ((TextBox)webpage.Form.Controls[i]);
                txt.Text = "";
                if (txt.ID.Substring(0, 2) == "V_")
                {
                    txt.Text = CommonCode.DateFormats.Date_DBToFront(System.DateTime.Today.ToShortDateString());
                }
            }

            if (webpage.Form.Controls[i].GetType().Name == "ContentPlaceHolder")
            {
                for (int j = 0; j < webpage.Form.Controls[i].Controls.Count; j++)
                {
                    if (webpage.Form.Controls[i].Controls[j].GetType().Name == "TextBox")
                    {
                        TextBox txt = ((TextBox)webpage.Form.Controls[i].Controls[j]);
                        txt.Text = "";
                        if (txt.ID.Substring(0, 2) == "V_")
                        {
                            txt.Text = CommonCode.DateFormats.Date_DBToFront(System.DateTime.Today.ToShortDateString());
                        }

                    }

                    if (webpage.Form.Controls[i].Controls[j].GetType().Name == "Panel")
                    {
                        for (int k = 0; k < webpage.Form.Controls[i].Controls[j].Controls.Count; k++)
                        {
                            if (webpage.Form.Controls[i].Controls[j].Controls[k].GetType().Name == "TextBox")
                            {
                                TextBox txt = ((TextBox)webpage.Form.Controls[i].Controls[j].Controls[k]);
                                txt.Text = "";
                                if (txt.ID.Substring(0, 2) == "V_")
                                {
                                    txt.Text = CommonCode.DateFormats.Date_DBToFront(System.DateTime.Today.ToShortDateString());
                                }

                            }

                        }
                    }
                }

            }


        }
    }

    /// <summary>
    /// For Checking Duplicacy in Masters Table
    /// </summary>
    /// <param name="tblName"></param>
    /// <returns></returns>
    public static bool DuplicayCheck(DataSet ds, string value, string fieldname, Label lblmsg)
    {

        //Query q = new Query(ab.Schema);

        //DataSet ds = new DataSet();

        // ds = q.ExecuteDataSet();

        DataView dvDataView;
        dvDataView = new DataView();

        if (ds.Tables.Count > 0)

            dvDataView = new DataView(ds.Tables[0]);
        else
        {
            lblmsg.Text = "Data is not available";
            return false;
        }

        dvDataView.Sort = fieldname;
        int foundOrNot = dvDataView.Find(value);

        if (foundOrNot == -1)
        {
            return true;

            // lblmsg.Text = ("Row not Found");
        }
        else
        {
            lblmsg.Text = "Trying To Insert Duplicate Value" + " " + (dvDataView[foundOrNot][fieldname].ToString());

            return false;



        }


    }

    /// <summary>
    /// Created By: Mukund

    /// Purpose: For Getting the date in mm/dd/yyyy format and checking the validity of the date for NOT compulsory field
    /// Date:31/Aug/2006
    /// </summary>
    /// <param name="Date"></param>
    /// <returns>string date</returns>
    public static string SetDateFormat_O(TextBox TBox)
    {
        System.IFormatProvider cultinfo = new CultureInfo("fr-FR", true);
        try
        {
            if (TBox.Text.Trim().ToString() == "")
            {
                return "";
            }
            else
            {
                string NewDate = DateTime.Parse(TBox.Text.Trim().ToString(), cultinfo).ToString("MM/dd/yyyy");
                //System.DateTime dt = System.DateTime.Parse(NewDate);
                return NewDate;
            }

        }
        catch (FormatException e)
        {
            throw (new FormatException("Invalid Date"));
        }
        catch (Exception e)
        {
            throw (new Exception("Date is blank"));
        }

    }
    /// <summary>
    /// For Getting table schema
    /// </summary>
    /// <param name="tblName"></param>
    /// <returns></returns>
    public static DataSet GetSchema(string tblName)
    {
        SqlConnection Conn = CommonFunction.Connect();
        SqlDataAdapter da = new SqlDataAdapter("BEGIN SET DATEFORMAT dMy SELECT TOP 1 * FROM " + tblName + " END ", Conn);
        //SqlDataAdapter da = new SqlDataAdapter("BEGIN SET DATEFORMAT dMy SELECT UserID, WebPageID, CAST(AllowAdd AS int) AS AllowAdd, CAST(AllowUpdate AS int) AS AllowUpdate, CAST(AllowDelete AS int) AS AllowDelete,CAST(AllowView AS int) AS AllowView FROM " + tblName + "  where 1 <> 1 END ", Conn);

        DataSet ds = new DataSet();
        da.FillSchema(ds, SchemaType.Source, tblName);
        return ds;
    }
    /// <summary>
    /// Created By: Mukund 
    /// Purpose: For Inserting data in Xml format
    /// </summary>
    /// <param name="sp_name"></param>
    /// <param name="str_xmlvalue"></param>
    /// <returns>string</returns>
    public static string SaveXMLRecord(string sp_name, string str_xmlvalue)
    {
        SqlConnection Conn = CommonFunction.Connect();
        string errStr = "";
        try
        {
            Conn.Open();
            SqlCommand sqlCmd = new SqlCommand("EXEC " + sp_name + " '" + str_xmlvalue + "'", Conn);
            sqlCmd.ExecuteNonQuery().ToString();
            Conn.Close();
        }
        catch (SqlException e)
        {
            errStr = e.Message;
        }
        return errStr;

    }
    /// <summary>
    /// Created By:Mukund
    /// Purpose:Overloaded SaveXMLRecord for deleting all records based on provided id 
    /// Usage:Used for Inserting User Rights
    /// </summary>
    /// <param name="sp_name"></param>
    /// <param name="str_xmlvalue"></param>
    /// <param name="del_value"></param>
    /// <returns></returns>
    public static int SaveXMLRecord(string sp_name, string str_xmlvalue, string del_value)
    {
        SqlConnection Conn = CommonFunction.Connect();
        //string errStr = "";
        try
        {
            Conn.Open();
            SqlCommand sqlCmd = new SqlCommand("EXEC " + sp_name + " '" + str_xmlvalue + "'," + del_value + "", Conn);
            int queryreturn = sqlCmd.ExecuteNonQuery();
            Conn.Close();
            return queryreturn;
        }
        catch (SqlException e)
        {
            throw (new Exception(e.Message));

        }


    }
    /// Purpose: For Getting the date in mm/dd/yyyy format and checking the validity of the date for compulsory field
    /// Date:31/Aug/2006
    /// </summary>
    /// <param name="Date"></param>
    /// <returns>string date</returns>
    public static string DDMM_Format(string Dt)
    {
        System.IFormatProvider cultinfo = new CultureInfo("fr-FR", true);
        try
        {
            if (Dt.ToString() == "")
            {
                throw (new Exception());
            }
            else
            {
                string NewDate = DateTime.Parse(Dt.ToString(), cultinfo).ToString("MM/dd/yyyy");
                //System.DateTime dt = System.DateTime.Parse(NewDate);
                return NewDate;
            }

        }
        catch (FormatException e)
        {
            throw (new FormatException("Invalid Date"));
        }
        catch (Exception e)
        {
            throw (new Exception("Date is blank"));
        }

    }
    /// <summary>
    /// Created by:Mukund
    /// Dated:28 Aug 2006
    /// Description:Used to check and return the value of dropdown if selected index !=0
    /// </summary>
    /// <param name="Text"></param>
    /// <returns>string</returns>
    public static string ReturnDDValueifSelect(DropDownList DD)
    {
        try
        {
            if (DD.SelectedIndex == 0)
            {

                throw (new Exception());
            }
            else
            {
                return DD.SelectedValue.ToString();
            }
        }
        catch (Exception ex)
        {
            throw (new Exception("No valid value was selected in the dropdown" + " " + DD.ID));
        }
    }
    /// <summary>
    /// Created by:Mukund
    /// Dated:28 Aug 2006
    /// Description:Used to check and return the text if textbox is not blank
    /// </summary>
    /// <param name="Text"></param>
    /// <returns>string</returns>
    public static string ReturnTextifNotBlank(TextBox TBox)
    {
        try
        {
            if (TBox.Text.Trim().ToString() == "")
            {

                throw (new Exception());
            }
            else
            {
                return TBox.Text;
            }
        }
        catch (Exception ex)
        {
            throw (new Exception("Text Box" + " " + TBox.ID + "was blank"));
        }
    }
    /// <summary>
    /// Created by:Anil
    /// Dated:12 Oct 2006
    /// Description:Used to check enter date should not be less than current date, where ever is required
    /// </summary>
    /// <param name="Text"></param>
    /// <returns>bool</returns>
    public static bool CheckEnterDateNotSmaller(TextBox E_date)
    {
        System.IFormatProvider cultinfo = new CultureInfo("fr-FR", true);
        try
        {
            string enterdate = DateTime.Parse(E_date.Text.ToString(), cultinfo).ToString("dd/MMM/yyyy");

            DateTime EnteredDate = System.DateTime.Parse(enterdate);

            if (EnteredDate.CompareTo(System.DateTime.Today) < 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        catch (FormatException e)
        {
            throw (new FormatException("Invalid Date"));
        }


    }
    /// <summary>
    /// Created by:Anil
    /// Dated:12 Oct 2006
    /// Description:Used to check enter date should not be greater than current date, where ever is required
    /// </summary>
    /// <param name="Text"></param>
    /// <returns>bool</returns>
    public static bool CheckEnterDateNotGreater(TextBox E_date)
    {
        System.IFormatProvider cultinfo = new CultureInfo("fr-FR", true);
        try
        {
            string enterdate = DateTime.Parse(E_date.Text.ToString(), cultinfo).ToString("dd/MMM/yyyy");
            DateTime EnteredDate = DateTime.Parse(enterdate);
            //DateTime.Parse(DateTime.Parse(E_date.Text.Trim().ToString(), cultinfo).ToString("MM/dd/yyyy"));
            //string EnteredDate = DateTime.Parse(E_date.Text.Trim().ToString(), cultinfo).ToString("yyyy/MM/dd");
            //string CurrentDate = System.DateTime.Now.ToShortDateString();
            //CurrentDate = DateTime.Parse(System.DateTime.Now.ToString(), cultinfo).ToString("yyyy/MM/dd");
            //int ed, cd;
            //ed = Convert.ToInt32(EnteredDate.Replace("/", ""));
            //cd = Convert.ToInt32(CurrentDate.Replace("/", ""));
            if (EnteredDate.CompareTo(System.DateTime.Today) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        catch (FormatException e)
        {
            throw (new FormatException("Invalid Date"));
        }


    }
    /// <summary>
    /// Created by:Saurabh Jain
    /// Dated:15 Dec 2008
    /// Description:checking if entered date in between from date and to date if not return false
    /// </summary>
    /// <param name="Text"></param>
    /// <returns>bool</returns>
    public static bool CheckEDateOnFAndTDate(TextBox edate, TextBox frmdate, TextBox todate)
    {
        try
        {
            if (frmdate.Text.Trim().ToString() != "" && todate.Text.Trim().ToString() != "" && edate.Text.Trim().ToString() != "")
            {
                System.IFormatProvider cultinfo = new CultureInfo("fr-FR", true);

                DateTime dtcleardate = DateTime.Parse(CommonCode.DateFormats.Date_FrontToDB_R(edate.Text.ToString()));
                DateTime dtFromDate = DateTime.Parse(CommonCode.DateFormats.Date_FrontToDB_R(frmdate.Text.ToString()));
                DateTime dtToDate = DateTime.Parse(CommonCode.DateFormats.Date_FrontToDB_R(todate.Text.ToString()));
                if (dtcleardate >= dtFromDate && dtcleardate <= dtToDate)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Created By: Anand
    /// Modified By: 
    /// Purpose:Set Gridview Sorting With Data Table
    /// Date:18/March/2009
    /// </summary>
    /// <param name="webpage"></param>

    public static DataView SorterWithDataTable(string _SortExp, SortDirection m_SortDirection, DataTable dtt)
    {
        String strSort = String.Empty;
        if (null != _SortExp &&
            String.Empty != _SortExp)
        {
            strSort = String.Format("{0} {1}", _SortExp, (m_SortDirection == SortDirection.Descending) ? "DESC" : "ASC");
        }
        DataView dv = new DataView(dtt, String.Empty, strSort, DataViewRowState.CurrentRows);
        return dv;
    }

    /// <summary>
    /// Created by:Anil
    /// MOdified:Mukund
    /// Dated:07 Sept 2006
    /// Description:if todate is greater than from date returns true
    /// </summary>
    /// <param name="Text"></param>
    /// <returns>bool</returns>
    public static bool CheckFromTDate(TextBox frmdate, TextBox todate)
    {
        try
        {
            if (frmdate.Text.Trim().ToString() != "" && todate.Text.Trim().ToString() != "")
            {
                System.IFormatProvider cultinfo = new CultureInfo("fr-FR", true);
                //string fromdate = DateTime.Parse(CommonCode.DateFormats.Date_FrontToDB_R(frmdate.Text.ToString()), cultinfo).ToString("yyyy/MM/dd");
                //string tonewdate = DateTime.Parse(CommonCode.DateFormats.Date_FrontToDB_R(todate.Text.ToString()), cultinfo).ToString("yyyy/MM/dd");
                DateTime fromdate = DateTime.Parse(CommonCode.DateFormats.Date_FrontToDB_R(frmdate.Text.ToString()));
                DateTime tonewdate = DateTime.Parse(CommonCode.DateFormats.Date_FrontToDB_R(todate.Text.ToString()));

                //int frd, tdate;
                //frd = Convert.ToInt32(fromdate.Replace("/", ""));
                // tdate = Convert.ToInt32(tonewdate.Replace("/", ""));
                if (fromdate.CompareTo(tonewdate) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
        catch
        {
            return true;
        }
    }


    /// <summary>
    /// Created by:Anil
    /// Dated:07 Sept 2006
    /// Description:Used to check from date & to date and true or false
    /// </summary>
    /// <param name="Text"></param>
    /// 
    /// <returns>bool</returns>
    public static bool CheckFromTDateInLabel(Label lfrmdate, Label ltodate)
    {
        try
        {
            if (lfrmdate.Text.Trim().ToString() != "" && ltodate.Text.Trim().ToString() != "")
            {
                System.IFormatProvider cultinfo = new CultureInfo("fr-FR", true);
                string lfromdate = DateTime.Parse(lfrmdate.Text.ToString(), cultinfo).ToString("yyyy/MM/dd");
                string ltonewdate = DateTime.Parse(ltodate.Text.ToString(), cultinfo).ToString("yyyy/MM/dd");

                int lfrd, ltdate;
                lfrd = Convert.ToInt32(lfromdate.Replace("/", ""));
                ltdate = Convert.ToInt32(ltonewdate.Replace("/", ""));
                if (lfrd > ltdate)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
        catch
        {
            return true;
        }
    }
    /// <summary>
    /// Created by:Anil
    /// Dated:07 Sept 2006
    /// Description:Used to bind the current & prevoius year in the drop down list
    /// </summary>
    /// <param name="Text"></param>
    /// <returns>void</returns>

    public static void BindYearDrp(DropDownList drpYear)
    {

        int yr = System.DateTime.Now.Year;
        for (int i = 0; i < 2; i++)
        {
            drpYear.Items.Add(Convert.ToString(yr));
            yr--;
        }
        drpYear.Items.Insert(0, "-Select-");
    }
    /// <summary>
    /// Created by:Anil
    /// Dated:13 Sept 2006
    /// Description:Used to bind the drop down list from year 2005 to current year
    /// </summary>
    /// <param name="Text"></param>
    /// <returns>void</returns>

    public static void BindYear2005toCurrent(DropDownList drpYear)
    {

        int yr = System.DateTime.Now.Year;
        for (int i = yr; i >= 2005; i--)
        {
            drpYear.Items.Add(Convert.ToString(i));

        }
        drpYear.Items.Insert(0, "-Select-");
    }
    /// <summary>
    /// Created by:Mukund
    /// Dated:21 Aug 2006
    /// Description:Used to check and return the text if it is not dbnull
    /// </summary>
    /// <param name="Text"></param>
    /// <returns>string</returns>
    public static string TextifNotNull(string Text)
    {
        if (Text.ToString().Equals(System.DBNull.Value))
        {
            return "";
        }
        else
        {
            return Text;
        }
    }
    /// <summary>
    /// Created By: Mukund
    /// Purpose: 
    /// Date:20/June/2006
    /// </summary>
    /// <returns>DataSet</returns>

    public static DataSet GetRights(string CommandText, ref ArrayList values, ref ArrayList names, ref ArrayList types)
    {
        SqlConnection Conn = CommonFunction.Connect();
        try
        {

            SqlCommand cmdPSelect = new SqlCommand();

            for (int i = 0; i < Convert.ToInt32(values.Count); i++)
            {
                SqlParameter IntPara = cmdPSelect.Parameters.Add(names[i].ToString(), types[i]);
                IntPara.Direction = ParameterDirection.Input;
                IntPara.Value = values[i];
            }

            cmdPSelect.Connection = Conn;
            cmdPSelect.CommandType = CommandType.StoredProcedure;
            cmdPSelect.CommandText = CommandText;

            if (Conn.State == ConnectionState.Closed) { Conn.Open(); }
            SqlDataAdapter DASelect = new SqlDataAdapter(cmdPSelect);
            DataSet DSSelect = new DataSet();
            DASelect.Fill(DSSelect);
            cmdPSelect.Parameters.Clear();
            return DSSelect;

        }
        catch (System.Exception ex)
        {
            string Str = ex.Message;
            return null;
        }
        finally { if (Conn.State == ConnectionState.Open) { Conn.Close(); } }
    }

    /// <summary>
    /// Created By: Mukund 
    /// Purpose: For Setting the visibility of the buttons on every page according to the button clicked
    /// Date:20/June/2006
    /// </summary>
    /// <param name="webpage"></param>
    /// <param name="clicked"></param>
    public static void ButtonVisibility(System.Web.UI.Page webpage, string clicked)
    {
        for (int i = 0; i < webpage.Form.Controls.Count; i++)
        {
            if (webpage.Form.Controls[i].GetType().Name == "ContentPlaceHolder")
            {
                for (int j = 0; j < webpage.Form.Controls[i].Controls.Count; j++)
                {
                    if (webpage.Form.Controls[i].Controls[j].GetType().Name == "Button")
                    {
                        switch (clicked.ToUpper())
                        {
                            case "EDIT":
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnSave")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = false;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "buttonfalse";

                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnDelete")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnEdit")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = false;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "buttonfalse";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnUpdate")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnCancel")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    //((Button)webpage.Form.Controls[i].Controls[j]).CausesValidation = false;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                }
                                break;

                            case "CANCEL":
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnSave")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnDelete")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = false;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "buttonfalse";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnEdit")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnUpdate")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = false;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "buttonfalse";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnCancel")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                    //((Button)webpage.Form.Controls[i].Controls[j]).CausesValidation = false;
                                }
                                break;

                            case "SAVE":
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnSave")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnDelete")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = false;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "buttonfalse";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnEdit")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnUpdate")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = false;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "buttonfalse";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnCancel")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                    // ((Button)webpage.Form.Controls[i].Controls[j]).CausesValidation = false;
                                }
                                break;
                            case "UPDATE":
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnSave")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnDelete")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = false;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "buttonfalse";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnEdit")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnUpdate")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = false;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "buttonfalse";
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnCancel")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                    //((Button)webpage.Form.Controls[i].Controls[j]).CausesValidation = false;
                                }
                                break;
                            case "DELETE":
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnSave")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                    //((Button)webpage.Form.Controls[i].Controls[j]).CausesValidation = true;
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnDelete")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = false;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "buttonfalse";
                                    //((Button)webpage.Form.Controls[i].Controls[j]).CausesValidation = true;

                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnEdit")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                    //((Button)webpage.Form.Controls[i].Controls[j]).CausesValidation = false;
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnUpdate")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = false;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "buttonfalse";
                                    //((Button)webpage.Form.Controls[i].Controls[j]).CausesValidation = true;
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnCancel")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                    //((Button)webpage.Form.Controls[i].Controls[j]).CausesValidation = false;
                                }
                                break;
                            case "LOAD":
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnSave")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CausesValidation = true;
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnDelete")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = false;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "buttonfalse";
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CausesValidation = true;

                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnEdit")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CausesValidation = false;
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnUpdate")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = false;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "buttonfalse";
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CausesValidation = true;
                                }
                                if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnCancel")
                                {
                                    ((Button)webpage.Form.Controls[i].Controls[j]).Enabled = true;
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CssClass = "button";
                                    ((Button)webpage.Form.Controls[i].Controls[j]).CausesValidation = false;
                                }
                                break;

                        }


                    }


                }
            }
        }
    }

    /// <summary>
    /// Created By: Mukund 
    /// Purpose: For Authenticating Login ID and Password
    /// Date:20/June/2006
    /// </summary>
    /// <param name="CommandText"></param>
    /// <param name="values"></param>
    /// <param name="names"></param>
    /// <param name="types"></param>
    /// <returns>0 for failed and 1 for succeeded</returns>
    public static int UserAuthentication(string CommandText, ref ArrayList values, ref ArrayList names, ref ArrayList types)
    {
        SqlConnection Conn = CommonFunction.Connect();
        try
        {

            SqlCommand cmdPSelect = new SqlCommand();
            int i = 0;
            for (i = 0; i < Convert.ToInt32(values.Count); i++)
            {
                SqlParameter IntPara = cmdPSelect.Parameters.Add(names[i].ToString(), types[i]);
                IntPara.Direction = ParameterDirection.Input;
                IntPara.Value = values[i];
            }
            SqlParameter RetPara = cmdPSelect.Parameters.Add("@ret", SqlDbType.Int);
            RetPara.Direction = ParameterDirection.ReturnValue;


            cmdPSelect.Connection = Conn;
            cmdPSelect.CommandType = CommandType.StoredProcedure;
            cmdPSelect.CommandText = CommandText;

            if (Conn.State == ConnectionState.Closed) { Conn.Open(); }
            cmdPSelect.ExecuteNonQuery();

            return (int)RetPara.Value;
        }
        catch (System.Exception ex)
        {
            string Str = ex.Message;
            return 0;
        }
        finally { if (Conn.State == ConnectionState.Open) { Conn.Close(); } }

    }
    /// <summary>
    /// Created By: Mukund 
    /// Purpose: For Getting Rights for a particular page of the logged in user
    /// Date:20/June/2006
    /// </summary>
    /// <param name="Stored Procedure"></param>
    /// <param name="values"></param>
    /// <param name="names"></param>
    /// <param name="types"></param>
    /// <returns>A list of Data Set containing rights </returns>
    public static DataSet GetPageRights(ref ArrayList values, ref ArrayList names, ref ArrayList types)
    {
        SqlConnection Conn = CommonFunction.Connect();
        try
        {

            SqlCommand cmdPSelect = new SqlCommand();
            int i = 0;
            for (i = 0; i < Convert.ToInt32(values.Count); i++)
            {
                SqlParameter IntPara = cmdPSelect.Parameters.Add(names[i].ToString(), types[i]);
                IntPara.Direction = ParameterDirection.Input;
                IntPara.Value = values[i];
            }

            cmdPSelect.Connection = Conn;
            cmdPSelect.CommandType = CommandType.StoredProcedure;
            cmdPSelect.CommandText = "USP_GetPageRights";

            if (Conn.State == ConnectionState.Closed) { Conn.Open(); }
            SqlDataAdapter DASelect = new SqlDataAdapter(cmdPSelect);
            DataSet DSSelect = new DataSet();
            DASelect.Fill(DSSelect);
            cmdPSelect.Parameters.Clear();
            return DSSelect;
        }
        catch (System.Exception ex)
        {
            string Str = ex.Message;
            return null;
        }
        finally { if (Conn.State == ConnectionState.Open) { Conn.Close(); } }
    }

    // by anand
    public bool IsDate(string sdate)
    {
        DateTime dt;
        bool isDate = true;
        try
        {
            dt = DateTime.Parse(sdate);
        }
        catch
        {
            isDate = false;
        }
        return isDate;
    }

    /// <summary>
    /// Created By: Mukund 
    /// Purpose: For Setting the buttons on the web page according to the rights of the user on that page
    /// Date:20/June/2006
    /// </summary>
    /// <param name="webpage"></param>
    /// <param name="Save"></param>
    /// <param name="Edit"></param>
    /// <param name="Delete"></param>
    /// <param name="View"></param>
    public static void SetPageRights(System.Web.UI.Page webpage, bool Save, bool Edit, bool Delete, bool View)
    {
        for (int i = 0; i < webpage.Form.Controls.Count; i++)
        {
            if (webpage.Form.Controls[i].GetType().Name == "ContentPlaceHolder")
            {
                for (int j = 0; j < webpage.Form.Controls[i].Controls.Count; j++)
                {
                    if (webpage.Form.Controls[i].Controls[j].GetType().Name == "Button")
                    {
                        if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnSave")
                        {
                            if (Save == false && Delete == false && Edit == false && View == true)
                            {
                                ((Button)webpage.Form.Controls[i].Controls[j]).Visible = false;
                            }
                            else
                            {
                                ((Button)webpage.Form.Controls[i].Controls[j]).Visible = (bool)Save;
                            }
                        }
                        if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnDelete")
                        {
                            if (Save == false && Delete == false && Edit == false && View == true)
                            {
                                ((Button)webpage.Form.Controls[i].Controls[j]).Visible = false;
                            }
                            else
                            {
                                ((Button)webpage.Form.Controls[i].Controls[j]).Visible = (bool)Delete;
                            }
                        }
                        if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnEdit")
                        {
                            if (Save == false && Delete == false && Edit == false && View == true)
                            {
                                ((Button)webpage.Form.Controls[i].Controls[j]).Visible = false;
                            }
                            else if (Edit == true || Delete == true)
                            {
                                ((Button)webpage.Form.Controls[i].Controls[j]).Visible = true;
                            }
                            else
                            {
                                ((Button)webpage.Form.Controls[i].Controls[j]).Visible = false;
                            }
                        }
                        if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnCancel")
                        {
                            if (Save == false && Delete == false && Edit == false && View == true)
                            {
                                ((Button)webpage.Form.Controls[i].Controls[j]).Visible = false;
                            }
                        }
                        if (((Button)webpage.Form.Controls[i].Controls[j]).ID.ToString() == "btnUpdate")
                        {
                            if (Save == false && Delete == false && Edit == false && View == true)
                            {
                                ((Button)webpage.Form.Controls[i].Controls[j]).Visible = false;
                            }
                            else
                            {
                                ((Button)webpage.Form.Controls[i].Controls[j]).Visible = (bool)Edit;
                            }

                        }


                    }

                }
            }


        }

    }

    /// <summary>
    /// Created By: Vinod

    /// Purpose: For Getting the current month id after passing date in dd/mm/yyyy format and return the month id
    /// Date:12/Apr/2007
    /// </summary>
    /// <param name="Date">in dd/mm/yyyy format</param>
    /// <returns>int month id </returns>

    public static int GetCurrentMonthID(string Date)
    {
        int k = 0;
        try
        {
            k = Convert.ToInt32(Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O((Date))).Month.ToString());
            return k;
        }
        catch (Exception ex)
        {
            return 0;
        }


    }

    /// <summary>
    /// Created By: Vinod

    /// Purpose: For Getting the current year after passing date in dd/mm/yyyy format and return the month id
    /// Date:12/Apr/2007
    /// </summary>
    /// <param name="Date">in dd/mm/yyyy format</param>
    /// <returns>int month id </returns>

    public static string Getyear(DateTime Date)
    {
        string k = "";
        try
        {
            k = Date.ToString("yyyy");
            return k;
        }
        catch (Exception ex)
        {
            return "0";
        }


    }

    /// <summary>
    /// Created By: Vinod

    /// Purpose: For Getting the diffrence of from date and todate after passing date in dd/mm/yyyy format and return the month id
    /// Date:26/Apr/2007
    /// </summary>
    /// <param name="Date">in dd/mm/yyyy format</param>
    /// <returns>int diffrence of two month </returns>

    public static int DateDiff(string fromdate, string todate)
    {
        int k = 0;
        try
        {

            DateTime FromDate;
            DateTime ToDate;
            FromDate = Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(fromdate));
            ToDate = Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(todate));

            k = Int32.Parse(DateAndTime.DateDiff(DateInterval.Month, FromDate, ToDate, Microsoft.VisualBasic.FirstDayOfWeek.System, Microsoft.VisualBasic.FirstWeekOfYear.System).ToString());

            return k;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    /*public static string EmailSending(string ToEamilAdd, string EmailSubject, string EmailBody)
    {

        /// <summary>
        /// Created By: Bhartendu K Jha

        /// Purpose:Sending Email 
        /// Date:17/May/2007
        /// </summary>
        /// <param name="ToEamilAdd">string</param>
        /// <param name="EmailSubject">string</param>
        /// <param name="EmailBody">string</param>
        /// <returns>string message</returns>

        string s1 = "mukund@expedien.net";
        ///string s2 = "anand@expedien.net";

        MailMessage mail = new MailMessage();
        mail.From = s1;
        mail.To = ToEamilAdd;
        mail.Subject = EmailSubject;
        mail.Body = EmailBody;
        mail.BodyFormat = MailFormat.Html;

        try
        {
            SmtpMail.SmtpServer = "192.168.10.33";
            SmtpMail.Send(mail);
        }
        catch (Exception ex)
        {
            throw;
        }
        return ("Mail send successfully");

    }
    */
    /// <summary>
    /// Created By: Sangeet

    /// Purpose: For Checking where input is valid numeric or not
    /// Date:20/Feb/2008
    /// </summary>

    /// <returns>bool as true false </returns>

    public static bool checknumeric(string str)
    {
        try
        {
            Convert.ToDecimal(str);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    /// Created By: Sangeet

    /// Purpose: For Checking where input is valid int or not
    /// Date:20/Feb/2008
    /// </summary>

    /// <returns>bool as true false </returns>
    public static bool checkint(string str)
    {
        try
        {
            Convert.ToInt64(str);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    //For Assignment for training sending mails to employee
    public static bool SendMail(string to, string from, string subject, string body, string attachRelPath)
    {

        string host = "smtp.gmail.com";
        int port = 587;
        string userName = "salaryrajuvas@gmail.com";
        string pswd = "rajuvas2010";
        bool sslEnabled = true;
        string fromAddress = from;

        //string host = "smtp.bizmail.yahoo.com";
        //int port = 25;
        //string userName = "salary@expedien.net";
        //string pswd = "123456";
        //bool sslEnabled = false;
        //string fromAddress = from;

        //MailMessage msg = new MailMessage(new MailAddress(fromAddress), new MailAddress(toAddress));
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

        msg.From = new System.Net.Mail.MailAddress(from);
        msg.To.Add(to);
        msg.Subject = subject;
        msg.SubjectEncoding = System.Text.Encoding.UTF8;
        msg.Body = body;
        msg.BodyEncoding = System.Text.Encoding.UTF8;
        msg.IsBodyHtml = true; //  Does the body contain html 
        SmtpClient client = new SmtpClient(host, port); //  Create an instance of SmtpClient with your smtp host and port        
        client.Credentials = new NetworkCredential(userName, pswd); //  Assign your username and password to connect to gmail        
        client.EnableSsl = sslEnabled;  //  Enable SSL

        if (attachRelPath != "")
        {
            string apath = attachRelPath.ToString();
            System.Net.Mail.Attachment at = new System.Net.Mail.Attachment(attachRelPath);
            msg.Attachments.Add(at);
        }
        try
        {
            client.Send(msg);
        }
        catch (SmtpException ex)
        {
            return false;
        }
        return true;
    }
    public static bool SendMailUniversity(string toAddress, string body, string subject, string attachPath)
    {
        string host = "smtp.gmail.com";
        int port = 587;
        string userName = "salaryrajuvas@gmail.com";
        string pswd = "rajuvas2010";
        bool sslEnabled = true;
        string fromAddress = "salaryrajuvas@gmail.com"; ;

        //string host = "smtp.gmail.com";
        //int port = 587;
        //string userName = "hr.expedien@gmail.com";
        //string pswd = "hr123456";
        //bool sslEnabled = true;
        //string fromAddress = "hr.expedien@gmail.com";

        //string host = "smtp.bizmail.yahoo.com";
        //int port = 25;
        //string userName = "salary@expedien.net";
        //string pswd = "123456";
        //bool sslEnabled = false;
        //string fromAddress = "salary@expedien.net";

        //MailMessage msg = new MailMessage(new MailAddress(fromAddress), new MailAddress(toAddress));
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

        msg.From = new System.Net.Mail.MailAddress(fromAddress, "University");
        msg.To.Add(toAddress);
        msg.Subject = subject;
        msg.SubjectEncoding = System.Text.Encoding.UTF8;
        msg.Body = body;
        msg.BodyEncoding = System.Text.Encoding.UTF8;
        msg.IsBodyHtml = true; //  Does the body contain html 
        SmtpClient client = new SmtpClient(host, port); //  Create an instance of SmtpClient with your smtp host and port        
        client.Credentials = new NetworkCredential(userName, pswd); //  Assign your username and password to connect to gmail        
        client.EnableSsl = sslEnabled;  //  Enable SSL

        if (attachPath != "")
        {
            string apath = attachPath.ToString();
            //System.Net.Mail.Attachment at = new System.Net.Mail.Attachment(apath);
            //msg.Attachments.Add(at);
        }
        try
        {
            client.Send(msg);
        }
        catch (SmtpException ex)
        {
            return false;
        }
        return true;
    }
    public static bool SendMail(string toAddress, string body, string subject, string attachPath)
    {
        string host = "smtp.gmail.com";
        int port = 587;
        string userName = "salaryrajuvas@gmail.com";
        string pswd = "rajuvas2010";
        bool sslEnabled = true;
        string fromAddress = "salaryrajuvas@gmail.com";

        //string host = "smtp.gmail.com";
        //int port = 587;
        //string userName = "hr.expedien@gmail.com";
        //string pswd = "hr123456";
        //bool sslEnabled = true;
        //string fromAddress = "hr.expedien@gmail.com";

        //string host = "smtp.bizmail.yahoo.com";
        //int port = 25;
        //string userName = "salary@expedien.net";
        //string pswd = "123456";
        //bool sslEnabled = false;
        //string fromAddress = "salary@expedien.net";

        //MailMessage msg = new MailMessage(new MailAddress(fromAddress), new MailAddress(toAddress));
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

        msg.From = new System.Net.Mail.MailAddress(fromAddress, "Salary");
        msg.To.Add(toAddress);
        msg.Subject = subject;
        msg.SubjectEncoding = System.Text.Encoding.UTF8;
        msg.Body = body;
        msg.BodyEncoding = System.Text.Encoding.UTF8;
        msg.IsBodyHtml = true; //  Does the body contain html 
        SmtpClient client = new SmtpClient(host, port); //  Create an instance of SmtpClient with your smtp host and port        
        client.Credentials = new NetworkCredential(userName, pswd); //  Assign your username and password to connect to gmail        
        client.EnableSsl = sslEnabled;  //  Enable SSL

        if (attachPath != "")
        {
            //string apath = attachPath.ToString();
            string apath = HttpContext.Current.Server.MapPath("~/" + attachPath.ToString());
            System.Net.Mail.Attachment at = new System.Net.Mail.Attachment(apath);
            msg.Attachments.Add(at);
        }
        try
        {
            client.Send(msg);
        }
        catch (SmtpException ex)
        {
            return false;
        }
        return true;
    }

    public void setMenu(int qstr, System.Web.UI.Page obj, System.Web.UI.HtmlControls.HtmlInputButton btn)
    {
        if (qstr == 1)
        {
            Menu mn;
            mn = (Menu)obj.Master.FindControl("MainMenu");
            mn.Visible = false;
            btn.Visible = true;
        }
    }

    /// <summary>
    /// Created By:Anand
    /// Date:28/8/08
    /// Remove more than one white spaces within the string
    /// </summary>
    ///
    public static string RemoveWhitespaceWithSplit(string inputString)
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string[] parts = inputString.Split(new char[] { ' ', '\n', '\t', '\r', '\f', '\v' }, StringSplitOptions.RemoveEmptyEntries);
        int size = parts.Length;
        for (int i = 0; i < size; i++)
            sb.AppendFormat("{0} ", parts[i]);
        return sb.ToString();

    }
    /// <summary>
    /// Created By:Anand
    /// Modified by:Rajesh Kumar S
    /// Date:06/9/13
    /// To Remove all the white spaces within the string
    /// </summary>
    ///
    public static string RemoveWhitespace(string inputString)
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string[] parts = inputString.Split(new char[] { ' ', '\n', '\t', '\r', '\f', '\v' }, StringSplitOptions.RemoveEmptyEntries);
        int size = parts.Length;
        for (int i = 0; i < size; i++)
            sb.AppendFormat("{0}", parts[i]);
        return sb.ToString();

    }

    /// <summary>
    /// Created by:Vandana 
    /// MOdified:
    /// Dated:12 nov 2008
    /// Description:To fill from date and to date in search form
    /// </summary>
    /// <param name="Text"></param>
    /// <returns>bool</returns>
    public static void filldate(TextBox frmdate, TextBox todate)
    {
        frmdate.Text = "01" + "/" + DateTime.Now.ToString("MM/yyyy");
        frmdate.Text = frmdate.Text.Substring(0, 2) + "/" + frmdate.Text.Substring(3, 2) + "/" + frmdate.Text.Substring(6, 4);
        todate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        todate.Text = todate.Text.Substring(0, 2) + "/" + todate.Text.Substring(3, 2) + "/" + todate.Text.Substring(6, 4);
    }

    public static bool Send_BIADA_Mails(string to, string from, string bcc, string subject, string body, string attachRelPath)
    {
        SqlDataReader rdr = null;
        SqlConnection con = null;
        SqlCommand cmd = null;

        //FETCHING MAIL CONFIG DETAILS FROM MAIL_CONFIG TABLE
        if (to.Trim() != "")
        {
            string lto = to;
            string lfrom = from;
            string lbcc = bcc;
            string lsubject = subject;
            string lbody = body;
            string lsmtpserver = "";
            string smtpusername = "";
            string smtppassword = "";
            string smtpport = "";
            string lsubjectprefix = "";
            string lattachment = attachRelPath;

            try
            {

                con = Connect();
                con.Open();

                string CommandText = "SELECT subjectprefix,smtpserver,mailfrom,body,priority,subject,bcc,smtpusername,smtppassword,smtpport" +
                                     " FROM Mail_Config";
                cmd = new SqlCommand(CommandText);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    if (from.Trim() == "")
                        lfrom = rdr["mailfrom"].ToString();
                    if (bcc.Trim() == "")
                        lbcc = rdr["bcc"].ToString();
                    if (subject.Trim() == "")
                        lsubject = rdr["subject"].ToString();
                    if (body.Trim() == "")
                        lbody = rdr["body"].ToString();
                    lsmtpserver = rdr["smtpserver"].ToString();
                    smtpusername = rdr["smtpusername"].ToString();
                    smtppassword = rdr["smtppassword"].ToString();
                    smtpport = rdr["smtpport"].ToString();
                    lsubjectprefix = rdr["subjectprefix"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                con.Dispose();
                rdr.Dispose();
            }

            //END FETCHING MAIL CONFIG DETAILS FROM MAIL_CONFIG TABLE


            //CREATING THE HTML BODY
            System.Text.StringBuilder strMessage = new System.Text.StringBuilder();
            strMessage.Append("<table width='50%' border='1' cellpadding='0' cellspacing='0' align='center'>");

            strMessage.Append("<tr>");
            strMessage.Append("<td>");

            strMessage.Append("<table width='100%' bgcolor='#FFFFFF' bordercolordark='#FF6600' border='0' cellpadding='0' cellspacing='0' align='center'>");

            strMessage.Append("<tr>");
            strMessage.Append("<td>");
            strMessage.Append("<img alt='' hspace=0 src='cid:uniqueid' width='147' height='86' border=0 />");
            strMessage.Append("</td>");
            strMessage.Append("</tr>");

            strMessage.Append("<tr>");
            strMessage.Append("<td colspan='2' bgcolor='#CCCCCC'>");
            strMessage.Append("<H2 align='center' style='font:Verdana, Arial, Helvetica, sans-serif; color:#FFFFFF'; padding='0' margin='0'><b>BIADA Information Center</b></H2>");
            strMessage.Append("</td>");
            strMessage.Append("</tr>");

            strMessage.Append("<tr>");
            strMessage.Append("<td colspan='2' font='11px'>");
            strMessage.Append("<p style='font:Verdana, Arial, Helvetica, sans-serif color:#666666';>");
            strMessage.Append(lbody);
            strMessage.Append("</p");
            strMessage.Append("</td>");
            strMessage.Append("</tr>");

            strMessage.Append("</table>");

            strMessage.Append("</td>");
            strMessage.Append("</tr>");

            strMessage.Append("</table>");

            //END HTML BODY

            //SENDING MAIL
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(lsmtpserver, Convert.ToInt32(smtpport));
            client.Credentials = new System.Net.NetworkCredential(smtpusername, smtppassword);
            System.Net.Mail.AlternateView htmlview = System.Net.Mail.AlternateView.CreateAlternateViewFromString(strMessage.ToString(), null, "text/html");
            System.Net.Mail.AlternateView imageview =
                new System.Net.Mail.AlternateView(HttpContext.Current.Server.MapPath("~/Images/biada-company.jpg").ToString(), System.Net.Mime.MediaTypeNames.Image.Jpeg);
            try
            {
                //client.EnableSsl = true;
                mail.From = new System.Net.Mail.MailAddress(lfrom);
                mail.To.Add(lto);
                mail.Subject = lsubjectprefix + lsubject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = strMessage.ToString();
                mail.Priority = System.Net.Mail.MailPriority.Normal;
                imageview.ContentId = "uniqueid";
                imageview.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                mail.AlternateViews.Add(htmlview);
                mail.AlternateViews.Add(imageview);


                if (attachRelPath != "")
                {
                    string apath = HttpContext.Current.Server.MapPath("~/" + attachRelPath.ToString());
                    System.Net.Mail.Attachment at = new System.Net.Mail.Attachment(apath);
                    mail.Attachments.Add(at);

                }
                client.Send(mail);
                mail.Dispose();
                htmlview.Dispose();
                imageview.Dispose();
                return true;


            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw new Exception("Mail Sending Failed SMTP Error");
            }
            catch (Exception ex)
            {
                throw new Exception("Mail Sending Failed due to some reason");
            }
            finally
            {
                mail.Dispose();
                htmlview.Dispose();
                imageview.Dispose();

            }
        }
        return false;

    }

    public static bool Send_BIADA_Mails(string to, string from, string bcc, string subject, string body, string attachRelPath1, string attachRelPath2)
    {
        SqlDataReader rdr = null;
        SqlConnection con = null;
        SqlCommand cmd = null;

        //FETCHING MAIL CONFIG DETAILS FROM MAIL_CONFIG TABLE
        if (to.Trim() != "")
        {
            string lto = to;
            string lfrom = from;
            string lbcc = bcc;
            string lsubject = subject;
            string lbody = body;
            string lsmtpserver = "";
            string smtpusername = "";
            string smtppassword = "";
            string smtpport = "";
            string lsubjectprefix = "";
            string lattachment1 = attachRelPath1;
            string lattachment2 = attachRelPath2;
            try
            {

                con = Connect();
                con.Open();

                string CommandText = "SELECT subjectprefix,smtpserver,mailfrom,body,priority,subject,bcc,smtpusername,smtppassword,smtpport" +
                                     " FROM Mail_Config";
                cmd = new SqlCommand(CommandText);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (from.Trim() == "")
                        lfrom = rdr["mailfrom"].ToString();
                    if (bcc.Trim() == "")
                        lbcc = rdr["bcc"].ToString();
                    if (subject.Trim() == "")
                        lsubject = rdr["subject"].ToString();
                    if (body.Trim() == "")
                        lbody = rdr["body"].ToString();
                    lsmtpserver = rdr["smtpserver"].ToString();
                    smtpusername = rdr["smtpusername"].ToString();
                    smtppassword = rdr["smtppassword"].ToString();
                    smtpport = rdr["smtpport"].ToString();
                    lsubjectprefix = rdr["subjectprefix"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                con.Dispose();
                rdr.Dispose();
            }

            //END FETCHING MAIL CONFIG DETAILS FROM MAIL_CONFIG TABLE


            //CREATING THE HTML BODY
            System.Text.StringBuilder strMessage = new System.Text.StringBuilder();
            strMessage.Append("<table width='50%' border='1' cellpadding='0' cellspacing='0' align='center'>");

            strMessage.Append("<tr>");
            strMessage.Append("<td>");

            strMessage.Append("<table width='100%' bgcolor='#FFFFFF' bordercolordark='#FF6600' border='0' cellpadding='0' cellspacing='0' align='center'>");

            strMessage.Append("<tr>");
            strMessage.Append("<td>");
            strMessage.Append("<img alt='' hspace=0 src='cid:uniqueid' width='147' height='86' border=0 />");
            strMessage.Append("</td>");
            strMessage.Append("</tr>");

            strMessage.Append("<tr>");
            strMessage.Append("<td colspan='2' bgcolor='#CCCCCC'>");
            strMessage.Append("<H2 align='center' style='font:Verdana, Arial, Helvetica, sans-serif; color:#FFFFFF'; padding='0' margin='0'><b>BIADA Information Center</b></H2>");
            strMessage.Append("</td>");
            strMessage.Append("</tr>");

            strMessage.Append("<tr>");
            strMessage.Append("<td colspan='2' font='11px'>");
            strMessage.Append("<p style='font:Verdana, Arial, Helvetica, sans-serif color:#666666';>");
            strMessage.Append(lbody);
            strMessage.Append("</p");
            strMessage.Append("</td>");
            strMessage.Append("</tr>");

            strMessage.Append("</table>");

            strMessage.Append("</td>");
            strMessage.Append("</tr>");

            strMessage.Append("</table>");

            //END HTML BODY
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(lsmtpserver);
            System.Net.Mail.AlternateView htmlview = System.Net.Mail.AlternateView.CreateAlternateViewFromString(strMessage.ToString(), null, "text/html");
            System.Net.Mail.AlternateView imageview =
                new System.Net.Mail.AlternateView(HttpContext.Current.Server.MapPath("~/Images/biada-company.jpg").ToString(), System.Net.Mime.MediaTypeNames.Image.Jpeg);
            //SENDING MAIL
            try
            {



                client.Port = Convert.ToInt32(smtpport);
                client.Credentials = new System.Net.NetworkCredential(smtpusername, smtppassword);
                mail.From = new System.Net.Mail.MailAddress(lfrom);
                mail.To.Add(lto);
                mail.Subject = lsubjectprefix + lsubject;
                mail.Body = strMessage.ToString();
                mail.Priority = System.Net.Mail.MailPriority.Normal;
                imageview.ContentId = "uniqueid";
                imageview.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                mail.AlternateViews.Add(htmlview);
                mail.AlternateViews.Add(imageview);


                if (attachRelPath1 != "")
                {
                    string apath = HttpContext.Current.Server.MapPath("~/" + attachRelPath1.ToString());
                    System.Net.Mail.Attachment at1 = new System.Net.Mail.Attachment(apath);
                    mail.Attachments.Add(at1);

                }

                if (attachRelPath2 != "")
                {
                    string apath = HttpContext.Current.Server.MapPath("~/" + attachRelPath2.ToString());
                    System.Net.Mail.Attachment at2 = new System.Net.Mail.Attachment(apath);
                    mail.Attachments.Add(at2);

                }
                //client.SendAsync(mail, mail.To.ToString());
                client.Send(mail);
                mail.Dispose();
                htmlview.Dispose();
                imageview.Dispose();
                return true;


            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw new Exception("Mail Sending Failed SMTP Error");
            }
            catch (Exception ex)
            {
                throw new Exception("Mail Sending Failed due to some reason");
            }
            finally
            {

            }
        }
        return false;

    }

    public static bool Send_BIADA_Mails(string to, string from, string subject, string body, string attachRelPath)
    {
        SqlDataReader rdr = null;
        SqlConnection con = null;
        SqlCommand cmd = null;

        //FETCHING MAIL CONFIG DETAILS FROM MAIL_CONFIG TABLE
        if (to.Trim() != "")
        {
            string lto = to;
            string lfrom = from;
            string lsubject = subject;
            string lbody = body;
            string lsmtpserver = "";
            string smtpusername = "";
            string smtppassword = "";
            string smtpport = "";
            string lsubjectprefix = "";
            string lattachment = attachRelPath;

            try
            {

                con = Connect();
                con.Open();

                string CommandText = "SELECT subjectprefix,smtpserver,mailfrom,body,priority,subject,bcc,smtpusername,smtppassword,smtpport" +
                                     " FROM Mail_Config";
                cmd = new SqlCommand(CommandText);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    if (from.Trim() == "")
                        lfrom = rdr["mailfrom"].ToString();
                    if (subject.Trim() == "")
                        lsubject = rdr["subject"].ToString();
                    if (body.Trim() == "")
                        lbody = rdr["body"].ToString();
                    lsmtpserver = rdr["smtpserver"].ToString();
                    smtpusername = rdr["smtpusername"].ToString();
                    smtppassword = rdr["smtppassword"].ToString();
                    smtpport = rdr["smtpport"].ToString();
                    lsubjectprefix = rdr["subjectprefix"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                con.Dispose();
                rdr.Dispose();
            }

            //END FETCHING MAIL CONFIG DETAILS FROM MAIL_CONFIG TABLE


            //CREATING THE HTML BODY
            System.Text.StringBuilder strMessage = new System.Text.StringBuilder();
            strMessage.Append("<table width='50%' border='1' cellpadding='0' cellspacing='0' align='center'>");

            strMessage.Append("<tr>");
            strMessage.Append("<td>");

            strMessage.Append("<table width='100%' bgcolor='#FFFFFF' bordercolordark='#FF6600' border='0' cellpadding='0' cellspacing='0' align='center'>");

            strMessage.Append("<tr>");
            strMessage.Append("<td>");
            strMessage.Append("<img alt='' hspace=0 src='cid:uniqueid' width='147' height='86' border=0 />");
            strMessage.Append("</td>");
            strMessage.Append("</tr>");

            strMessage.Append("<tr>");
            strMessage.Append("<td colspan='2' bgcolor='#CCCCCC'>");
            strMessage.Append("<H2 align='center' style='font:Verdana, Arial, Helvetica, sans-serif; color:#FFFFFF'; padding='0' margin='0'><b>BIADA Information Center</b></H2>");
            strMessage.Append("</td>");
            strMessage.Append("</tr>");

            strMessage.Append("<tr>");
            strMessage.Append("<td colspan='2' font='11px'>");
            strMessage.Append("<p style='font:Verdana, Arial, Helvetica, sans-serif color:#666666';>");
            strMessage.Append(lbody);
            strMessage.Append("</p");
            strMessage.Append("</td>");
            strMessage.Append("</tr>");

            strMessage.Append("</table>");

            strMessage.Append("</td>");
            strMessage.Append("</tr>");

            strMessage.Append("</table>");

            //END HTML BODY

            //SENDING MAIL
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(lsmtpserver, Convert.ToInt32(smtpport));
            client.Credentials = new System.Net.NetworkCredential(smtpusername, smtppassword);
            System.Net.Mail.AlternateView htmlview = System.Net.Mail.AlternateView.CreateAlternateViewFromString(strMessage.ToString(), null, "text/html");
            System.Net.Mail.AlternateView imageview =
                new System.Net.Mail.AlternateView(HttpContext.Current.Server.MapPath("~/Images/biada-company.jpg").ToString(), System.Net.Mime.MediaTypeNames.Image.Jpeg);
            try
            {
                //client.EnableSsl = true;
                mail.From = new System.Net.Mail.MailAddress(lfrom);
                mail.To.Add(lto);
                mail.Subject = lsubjectprefix + lsubject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = strMessage.ToString();
                mail.Priority = System.Net.Mail.MailPriority.Normal;
                imageview.ContentId = "uniqueid";
                imageview.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                mail.AlternateViews.Add(htmlview);
                mail.AlternateViews.Add(imageview);


                if (attachRelPath != "")
                {
                    string apath = HttpContext.Current.Server.MapPath("~/" + attachRelPath.ToString());
                    System.Net.Mail.Attachment at = new System.Net.Mail.Attachment(apath);
                    mail.Attachments.Add(at);

                }
                client.Send(mail);
                mail.Dispose();
                htmlview.Dispose();
                imageview.Dispose();
                return true;


            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw new Exception("Mail Sending Failed SMTP Error");
            }
            catch (Exception ex)
            {
                throw new Exception("Mail Sending Failed due to some reason");
            }
            finally
            {
                mail.Dispose();
                htmlview.Dispose();
                imageview.Dispose();

            }
        }
        return false;

    }
    public string CheckFraction(decimal val)
    {

        string value = val.ToString();

        int index = value.IndexOf(".");

        int fval = Convert.ToInt32(value.Substring(index + 1));

        if (fval > 0)

            return value;

        else

            return value.Remove(index);

    }

    public static bool CheckValiddate(string Cdate)
    {
        try
        {
            string temp = CommonCode.DateFormats.Date_FrontToDB_R(Cdate);
            Convert.ToDateTime(temp);
            return true;

        }
        catch (System.Net.Mail.SmtpException ex)
        {
            return false;
        }

    }

    public string FileUpload(FileUpload flu, string FolderName)
    {
        try
        {
            if (flu.HasFile)
            {
                string host = HttpContext.Current.Request.Url.Host;
                string upldPath = "";
                if (host == "117.239.77.7")
                {
                    System.Configuration.AppSettingsReader configSetting = new System.Configuration.AppSettingsReader();
                    upldPath = ((string)(configSetting.GetValue("117.239.77.7", typeof(string))));
                }
                string fn = DateTime.Now.ToString("yyyyMMddHHmmssfff") + flu.FileName.Substring(flu.FileName.LastIndexOf("."));
                upldPath = upldPath + "\\" + FolderName + "\\" + fn;
                flu.SaveAs(upldPath);
                return fn;

            }
            return "";
        }
        catch (Exception ex)
        {
            throw new Exception("Upload Fails!");
        }

    }
    public string ReturnphysicalPath()
    {
        try
        {
            string host = HttpContext.Current.Request.Url.Host;
            string upldPath = "";
            if (host == "localhost")
            {
                System.Configuration.AppSettingsReader configSetting = new System.Configuration.AppSettingsReader();
                upldPath = ((string)(configSetting.GetValue("Local", typeof(string))));
            }
            else if (host == "192.168.10.66")
            {
                System.Configuration.AppSettingsReader configSetting = new System.Configuration.AppSettingsReader();
                upldPath = ((string)(configSetting.GetValue("192.168.10.66", typeof(string))));
            }
            else if (host == "192.168.10.99")
            {
                System.Configuration.AppSettingsReader configSetting = new System.Configuration.AppSettingsReader();
                upldPath = ((string)(configSetting.GetValue("192.168.10.99", typeof(string))));
            }
            else if (host == "117.239.77.7")
            {
                System.Configuration.AppSettingsReader configSetting = new System.Configuration.AppSettingsReader();
                upldPath = ((string)(configSetting.GetValue("117.239.77.7", typeof(string))));
            }
            return upldPath;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public byte[] StreamFile(string filename)
    {
        try
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            // Create a byte array of file stream length
            byte[] ImageData = new byte[fs.Length];

            //Read block of bytes from stream into the byte array
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));

            //Close the File Stream
            fs.Close();
            return ImageData; //return the byte data
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string FU_physicalPath(FileUpload flu, string FolderName, string FileName)
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
                        upldPath = upldPath + "\\" + FolderName + "\\" + FileName;
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

    public string ReturnPhysicalPath_ForFileDeleting()
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
                    return dr["Physical_Path"].ToString().Trim();
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /*
    public static bool CheckFinancialYearForAssetMgnt(TextBox frmdate, TextBox todate)
    {
        try
        {
            DataSet ds = ASST.ASST_RequisitionOfItems.ASST_FinancialYear_Mst_Sel().GetDataSet();
            if (frmdate.Text.Trim().ToString() != "" && todate.Text.Trim().ToString() != "")
            {
                System.IFormatProvider cultinfo = new CultureInfo("fr-FR", true);
                string lfromdate1 = DateTime.Parse(frmdate.Text.ToString(), cultinfo).ToString("yyyy/MM/dd");
                string ltodate1 = DateTime.Parse(todate.Text.ToString(), cultinfo).ToString("yyyy/MM/dd");
                int lfrd1, ltdate1;
                lfrd1 = Convert.ToInt32(lfromdate1.Replace("/", ""));
                ltdate1 = Convert.ToInt32(ltodate1.Replace("/", ""));

                string lfromdate2 = DateTime.Parse(CommonCode.DateFormats.Date_DBToFront(ds.Tables[0].Rows[0]["FromDate"].ToString()), cultinfo).ToString("yyyy/MM/dd");
                string ltodate2 = DateTime.Parse(CommonCode.DateFormats.Date_DBToFront(ds.Tables[0].Rows[0]["toDate"].ToString()), cultinfo).ToString("yyyy/MM/dd");

                int lfrd2, ltdate2;
                lfrd2 = Convert.ToInt32(lfromdate2.Replace("/", ""));
                ltdate2 = Convert.ToInt32(ltodate2.Replace("/", ""));

                if (lfrd1 < lfrd2 || ltdate1 > ltdate2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return true;
        }
    }

    public static bool CheckFinancialYearForAssetMgnt(TextBox txtDate)
    {
        try
        {
            DataSet ds = ASST.ASST_RequisitionOfItems.ASST_FinancialYear_Mst_Sel().GetDataSet();
            if (txtDate.Text.Trim().ToString() != "")
            {
                System.IFormatProvider cultinfo = new CultureInfo("fr-FR", true);
                string ldate1 = DateTime.Parse(txtDate.Text.ToString(), cultinfo).ToString("yyyy/MM/dd");
                int ld1;
                ld1 = Convert.ToInt32(ldate1.Replace("/", ""));

                string ldate2 = DateTime.Parse(CommonCode.DateFormats.Date_DBToFront(ds.Tables[0].Rows[0]["FromDate"].ToString()), cultinfo).ToString("yyyy/MM/dd");

                int ld2;
                ld2 = Convert.ToInt32(ldate2.Replace("/", ""));

                if (ld1 < ld2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return true;
        }
    }
     */
}

