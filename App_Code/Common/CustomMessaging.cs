using System;
using System.Data;
using System.Web;
using System.Data.SqlClient;
/// <summary>
/// Summary description for EException
/// </summary>
public class CustomMessaging
{
    public HttpApplication WebcurrAppContext = new HttpApplication();
    public CustomMessaging()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //To Show User Specific Error Message
    public  string ShowSQLErrorMsg(string SqlErrMsg, string ErrMsg, SqlException ex)
    {
        string strErrorMsg = "";
        string[] strsplit;
        strsplit = SqlErrMsg.Split(' ');
        if (strsplit[0].ToUpper().ToString() == "VIOLATION")
        {
            //Duplicate Record Error
            strErrorMsg = ShowMessage("L").ToString();
        }
        else if (strsplit[1].ToUpper().ToString() == "DELETE")
        {
            //Reference Error
            strErrorMsg = ShowMessage("F").ToString();
        }
        else
        {
            strErrorMsg = ex.Message.ToString();
            WebcurrAppContext.Context.Response.Write("<b>Error in " + ErrMsg + " : </b><i>" + ex.Message.ToString() + "</i><br>");
            WebcurrAppContext.Context.Response.End();
        }
        return strErrorMsg;
    }

    public  string ShowMessage(string Tag)
    {
        string strMsg = "";
        //Tag Details
        //M For User Messages
        //S For Insert Record
        //U For Update Record
        //D For Delete Record
        //C For The confirmation of Deletion(Use In Javascript Message)
        //L For Duplicate Record found
        //F For Delete fail Message
        //R For Required Field Message
        //SB For Save Button
        //UB for Update Button
        //RB for Update Button

        try
        {
            strMsg = GetSysParaMessages(Tag);
           
        }
        catch (Exception ex)
        {
           // ShowErrorMsg("Error in Displaying Message (ShowMessage)", ex);
        }
        return strMsg;
    }

  
    public string GetSysParaMessages(string Key)
    {
        DataSet ds; 
        DataTable dt = new DataTable();
        string Msg = "";
        try
        {
            ds = IUMSNXG.SP.SpSelSystemParameter().GetDataSet();
            dt = (DataTable)ds.Tables[0];
            string str = "";
            str = "en-US";// HttpContext.Current.Session["language"].ToString();

            if (dt.Rows.Count > 0)
            {
                if (str == "en-US")
                {
                    if (Key == "S")
                    {
                        Msg = dt.Rows[0]["E_Save_Msg"].ToString();
                    }
                    else if (Key == "U")
                    {
                        Msg = dt.Rows[0]["E_Update_Msg"].ToString();
                    }
                    else if (Key == "D")
                    {
                        Msg = dt.Rows[0]["E_DeleteSucess_Msg"].ToString();
                    }
                    else if (Key == "C")
                    {
                        Msg = dt.Rows[0]["E_DeleteConfirm_Msg"].ToString();
                    }
                    else if (Key == "L")
                    {
                        Msg = dt.Rows[0]["E_DuplicateRecord_Msg"].ToString();
                    }
                    else if (Key == "F")
                    {
                        Msg = dt.Rows[0]["E_DeleteFail_Msg"].ToString();
                    }
                    else if (Key == "R")
                    {
                        Msg = dt.Rows[0]["E_RequiredField_Msg"].ToString();
                    }
                    else if (Key == "SB")
                    {
                        Msg = dt.Rows[0]["E_btnSaveCaption"].ToString();
                    }
                    else if (Key == "UB")
                    {
                        Msg = dt.Rows[0]["E_btnUpdateCaption"].ToString();
                    }
                    else if (Key == "RB")
                    {
                        Msg = dt.Rows[0]["E_btnResetCaption"].ToString();
                    }
                    else if (Key == "NF")
                    {
                        Msg = dt.Rows[0]["E_NF"].ToString();
                    }
                }
                else
                {
                    if (Key == "S")
                    {
                        Msg = dt.Rows[0]["H_Save_Msg"].ToString();
                    }
                    else if (Key == "U")
                    {
                        Msg = dt.Rows[0]["H_Update_Msg"].ToString();
                    }
                    else if (Key == "D")
                    {
                        Msg = dt.Rows[0]["H_DeleteSucess_Msg"].ToString();
                    }
                    else if (Key == "C")
                    {
                        Msg = dt.Rows[0]["H_DeleteConfirm_Msg"].ToString();
                    }
                    else if (Key == "L")
                    {
                        Msg = dt.Rows[0]["H_DuplicateRecord_Msg"].ToString();
                    }
                    else if (Key == "F")
                    {
                        Msg = dt.Rows[0]["H_DeleteFail_Msg"].ToString();
                    }
                    else if (Key == "R")
                    {
                        Msg = dt.Rows[0]["H_RequiredField_Msg"].ToString();
                    }
                    else if (Key == "SB")
                    {
                        Msg = dt.Rows[0]["H_btnSaveCaption"].ToString();
                    }
                    else if (Key == "UB")
                    {
                        Msg = dt.Rows[0]["H_btnUpdateCaption"].ToString();
                    }
                    else if (Key == "RB")
                    {
                        Msg = dt.Rows[0]["H_btnResetCaption"].ToString();
                    }
                }
            }
            else
            {
                Msg = "Message not found!";
            }
        }
        catch (Exception ex)
        {
            // ShowErrorMsg("Error in getting message from table(GetSysParaMessages)", ex);
        }
        finally
        {

        }
        return Msg;
    }
}
