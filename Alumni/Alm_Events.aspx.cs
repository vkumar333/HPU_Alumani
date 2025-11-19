using DataAccessLayer;
using SubSonic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Alumni_Alm_Events : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    crypto cpto = new crypto();

    public object description { get; private set; }

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }
    private DataTable EventsDetails(int Pk_Eventsid)
    {
        ClearArrayLists();
        names.Add("@Pk_Eventsid"); types.Add(SqlDbType.NVarChar); values.Add(Pk_Eventsid);
        return Dobj.GetDataTable("ALM_Show_Events_Details_by_id", values, names, types);
    }

    private DataTable EventslatestDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_Show_Events_DetailsS", values, names, types);
    }

    private DataTable CountEventsDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_Count_Events_Detailss", values, names, types);
    }
    private DataTable SearchEventsDetails()
    {
        ClearArrayLists();
        names.Add("@eventsname"); types.Add(SqlDbType.NVarChar); values.Add(description);
        return Dobj.GetDataTable("ALM_Search_Events_Detailss", values, names, types);
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            int Pk_Eventsid = Convert.ToInt32(cpto.DecodeString(Request.QueryString["ID"].ToString()));
            ViewState["Eventid"] = Pk_Eventsid;

            if (Pk_Eventsid != 0)
            {
                EventsRepeter(Pk_Eventsid);
                getSocialURLLinks();
            }
        }
    }

    #region "Get Social URL Links"

    private void getSocialURLLinks()
    {
        try
        {
            using (DataSet dsURL = getSocialMediaURLLinks().GetDataSet())
            {
                if (dsURL.Tables.Count > 0 && dsURL.Tables[0].Rows.Count > 0)
                {
                    int count = dsURL.Tables[0].Rows.Count;

                    if (dsURL.Tables[0].Rows.Count > 0)
                    {
                        facebookLink.HRef = dsURL.Tables[0].Rows[0]["facebookLink"].ToString();
                        facebookLink.Target = "_blank";
                        twitterLink.HRef = dsURL.Tables[0].Rows[0]["twitterLink"].ToString();
                        twitterLink.Target = "_blank";
                        linkedInLink.HRef = dsURL.Tables[0].Rows[0]["linkedInLink"].ToString();
                        linkedInLink.Target = "_blank";
                        youtubeLink.HRef = dsURL.Tables[0].Rows[0]["youtubeLink"].ToString();
                        youtubeLink.Target = "_blank";
                    }
                    else
                    {
                        facebookLink.HRef = "#";
                        twitterLink.HRef = "#";
                        linkedInLink.HRef = "#";
                        youtubeLink.HRef = "#";
                    }
                }
            }
        }
        catch (SqlException sqlEx)
        {
            // Handle SQL related errors here
            ClientMessaging("SQL Error: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            // Handle any other errors here
            ClientMessaging("Error: " + ex.Message);
        }
    }

    public StoredProcedure getSocialMediaURLLinks()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Social_Media_Links_Info", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    #endregion

    #region "Bind Repeater"

    private void EventsRepeter(int Pk_Eventsid)
    {
        DataTable dt = new DataTable();
        dt = EventsDetails(Pk_Eventsid);
        dt.Columns.Add("encId");
        if (dt.Rows.Count > 0)
        {
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                string pkid = dt.Rows[x]["PK_Events_id"].ToString();
                string encId = cpto.EncodeString(Convert.ToInt32(pkid));
                dt.Rows[x]["encId"] = encId;
            }
            RepeventsAll.DataSource = dt;
            RepeventsAll.DataBind();
        }
    }

    public DataTable getSocialMediaLinks()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_Get_Social_Media_Links_Info", values, names, types);
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    #endregion

    protected void RepeventsAll_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView row = (DataRowView)e.Item.DataItem;

            string eventName = row["Event_name"].ToString();
            string eventImage = row["Filepath"].ToString();
            string eventDesc = row["Description"].ToString();

            string encId = row["encId"] != DBNull.Value ? row["encId"].ToString() : string.Empty;
            string eventLink = row["eventRegistrationLink"] != DBNull.Value ? row["eventRegistrationLink"].ToString() : string.Empty;

            Anthem.Label lblEventLink = (Anthem.Label)e.Item.FindControl("lblEventLink");
            Anthem.Label lblColon = (Anthem.Label)e.Item.FindControl("lblColon");
            HtmlAnchor anchorEventLink = (HtmlAnchor)e.Item.FindControl("anchorEventLink");

            if (!string.IsNullOrEmpty(eventLink))
            {
                lblEventLink.Visible = true;
                lblEventLink.Text = "Event Registration Link";
                lblColon.Visible = true;
                lblColon.Text = " : ";
                lblEventLink.Style.Add("text-decoration", "underline");

                anchorEventLink.HRef = eventLink;
                anchorEventLink.Target = "_blank";
                anchorEventLink.InnerText = eventLink;
            }
            else
            {
                lblEventLink.Visible = false;
                lblColon.Visible = false;
                anchorEventLink.HRef = "#";
                anchorEventLink.InnerText = string.Empty;
            }
        }
    }
}