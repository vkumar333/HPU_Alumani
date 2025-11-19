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
using System.IO;
using DataAccessLayer;
using System.Drawing;
using System.Web.Hosting;
using System.Linq;
using System.Data.SqlClient;
using SubSonic;

public partial class Alumni_Alm_BecomeMentor : System.Web.UI.Page
{
    DataAccess DAobj = new DataAccess();
    DataAccess Dobj = new DataAccess();
    ArrayList name = new ArrayList(); ArrayList type = new ArrayList(); ArrayList value = new ArrayList();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

    void clear()
    {
        name.Clear(); value.Clear(); type.Clear();
    }

    void clears()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    public string dsxml { get; set; }
    public int pk_MentorID { get; set; }
    public int fk_alumnid { get; set; }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["AlumniID"] != null)
            {
                fk_alumnid = Convert.ToInt32(Session["AlumniID"].ToString());
                Labelname.Text = Session["alumniname"].ToString();
                BindToTopicsTextByFlagT(1);
                BindToTopicsTextByFlagTT(2);
                BindToTopicsTextByFlag_chktopic5(3);
                BindToTopicsTextByFlag_chktopic6(4);
                getRequests(fk_alumnid);

                if (pk_MentorID != 0)
                {
                    Edit(pk_MentorID);
                }
            }
        }
    }

    private void Edit(int mentorID)
    {
        DataSet ds = Get_mentor(mentorID);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //btnSave.Text = "UPDATE";
            //btnSave.CommandName = "UPDATE";
            //btnSave.TextDuringCallBack = "UPDATE...";

            btnSave.Enabled = false;
            btnSave.Visible = false;

            var topics = ds.Tables[0].Rows[0]["topicids"].ToString();
            string[] topicsArray = topics.Split(',');

            if (!string.IsNullOrEmpty(topics))
            {
                string[] selectedTopics = topics.Split(',');

                foreach (ListItem item in chktopic.Items)
                {
                    if (selectedTopics.Contains(item.Value))
                    {
                        string selectedOtherID = item.Value;
                        item.Selected = true;

                        if (item != null && item.Selected && item.Text == "Other")
                        {
                            selectedOtherID = item.Value;
                            pnl.Visible = true;
                            txtspcfy.Text = ds.Tables[0].Rows[0]["other_topic"].ToString();
                        }
                        else if (item != null && !item.Selected && item.Text != "Other")
                        {
                            pnl.Visible = false;
                            txtspcfy.Text = "";
                        }
                        else if (item != null && !item.Selected && item.Text != "")
                        {
                            txtspcfy.Text = "";
                        }
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
            }

            var chktopi2 = ds.Tables[0].Rows[0]["like_to_mentor"].ToString();
            string[] topicsArr = chktopi2.Split(',');

            if (!string.IsNullOrEmpty(chktopi2))
            {
                string[] selectedTopic = chktopi2.Split(',');

                foreach (ListItem item in chktopic2.Items)
                {
                    if (selectedTopic.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
            }

            //txtbckground.Text = ds.Tables[0].Rows[0]["domains_mentor"].ToString();            

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["skills_mentor"].ToString()))
            {
                txtbckground2.Text = ds.Tables[0].Rows[0]["skills_mentor"].ToString();
            }
            else
            {
                txtbckground2.Text = "";
            }

            //var chktopi5 = ds.Tables[0].Rows[0]["preferredtime_mentor_for"].ToString();
            //string[] topicsAr = chktopi5.Split(',');

            //if (!string.IsNullOrEmpty(chktopi5))
            //{
            //    string[] selectedTopic = chktopi5.Split(',');

            //    foreach (ListItem item in chktopic5.Items)
            //    {
            //        if (selectedTopic.Contains(item.Value))
            //        {
            //            string selectedOtherID = item.Value;
            //            item.Selected = true;

            //            if (item != null && item.Selected && item.Text == "Others")
            //            {
            //                selectedOtherID = item.Value;
            //                pnll2.Visible = true;
            //                txt_specify_pre.Text = ds.Tables[0].Rows[0]["other_preferred_time"].ToString();

            //            }
            //            else if (item != null && !item.Selected && item.Text != "Others")
            //            {
            //                pnll2.Visible = false;
            //                txt_specify_pre.Text = "";
            //            }
            //            else if (item != null && !item.Selected && item.Text != "")
            //            {
            //                txt_specify_pre.Text = "";
            //            }
            //        }
            //        else
            //        {
            //            item.Selected = false;
            //        }
            //    }
            //}

            var chktopi6 = ds.Tables[0].Rows[0]["conversation_by"].ToString();
            string[] topicsAr1 = chktopi6.Split(',');

            if (!string.IsNullOrEmpty(chktopi6))
            {
                string[] selectedTopic = chktopi6.Split(',');

                foreach (ListItem item in chktopic6.Items)
                {
                    if (selectedTopic.Contains(item.Value))
                    {
                        string selectedOtherID = item.Value;
                        item.Selected = true;

                        if (item != null && item.Selected && item.Text == "Other")
                        {
                            selectedOtherID = item.Value;
                            pnl3.Visible = true;
                            txt_specify3.Text = ds.Tables[0].Rows[0]["other_conversation"].ToString();
                        }
                        else if (item != null && !item.Selected && item.Text != "Other")
                        {
                            pnl3.Visible = false;
                            txt_specify3.Text = "";
                        }
                        else if (item != null && !item.Selected && item.Text != "")
                        {
                            txt_specify3.Text = "";
                        }
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
            }

            //txtbckground7.Text = ds.Tables[0].Rows[0]["message_mentor"].ToString();

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["message_mentor"].ToString()))
            {
                txtbckground7.Text = ds.Tables[0].Rows[0]["message_mentor"].ToString();
            }
            else
            {
                txtbckground7.Text = "";
            }

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isDeclarationAccepted"].ToString()))
            {
                chkDeclaration.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isDeclarationAccepted"].ToString().Trim());
                chkDeclaration.Enabled = false;
            }
            else
            {
                chkDeclaration.Checked = false;
                chkDeclaration.Enabled = false;
            }
        }
    }

    private DataSet Get_mentor(int mentorid)
    {
        clears();
        names.Add("@pk_MentorID"); values.Add(mentorid); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("ALM_Mentor_Mst_Edit", values, names, types);
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    public int InsertRecord(ref string Message)
    {
        clear();
        name.Add("@doc"); value.Add(dsxml); type.Add(SqlDbType.VarChar);
        if (DAobj.ExecuteTransactionMsg("Alm_insertMentor_Details", value, name, type, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    protected void Save()
    {
        try
        {
            string message = "";
            DataSet DsMain = new DataSet();
            DsMain = GetMain();
            dsxml = DsMain.GetXml();

            if (InsertRecord(ref message) > 0)
            {
                ClientMessaging("Request send Succesfully.!");
            }
            else
            {
                ClientMessaging("Something went wrong.!");
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
	
    protected bool validation()
    {
        try
        {
            string selectedCheckIds = ""; int count = 0;

            for (int i = 0; i < chktopic.Items.Count; i++)
            {
                ListItem item = new ListItem();
                item = chktopic.Items[i];

                if (item.Selected)
                {
                    count = count + 1;
                    if (string.IsNullOrEmpty(selectedCheckIds))
                    {
                        selectedCheckIds = item.Value;
                    }
                    else
                    {
                        selectedCheckIds += "," + item.Value;
                    }
                }
            }

            string[] selectedTopics = selectedCheckIds.Split(',');

            foreach (ListItem item in chktopic.Items)
            {
                if (selectedTopics.Contains(item.Value))
                {
                    string selectedOtherID = item.Value;
                    item.Selected = true;

                    if (item != null && item.Selected && item.Text == "Other" && txtspcfy.Text == "")
                    {
                        selectedOtherID = item.Value;
                        pnl.Visible = true;
                        ClientMessaging("You selected other option,Please specify other topics.!!!");
                        txtspcfy.Focus();
                        return false;
                    }
                    else if (item != null && !item.Selected && item.Text != "Other")
                    {
                        pnl.Visible = false;
                        txtspcfy.Text = "";
                    }
                    else if (item != null && !item.Selected && item.Text != "")
                    {
                        txtspcfy.Text = "";
                    }
                }
                else
                {
                    item.Selected = false;
                }
            }

            if (count == 0)
            {
                ClientMessaging("Please choose at least one topics.");
                return false;
            }

            if (chktopic2.SelectedIndex == -1)
            {
                ClientMessaging("Please choose who would you like to mentor.");
                chktopic2.Focus();
                return false;
            }

            //if (txtbckground.Text == "")
            //{
            //    ClientMessaging("Please fill Mention domains in which you can mentor others");
            //    txtbckground.Focus();
            //    return false;
            //}

            if (txtbckground2.Text == "")
            {
                ClientMessaging("Please mention your skills.");
                txtbckground2.Focus();
                return false;
            }

            //string selectedCheckId1 = "";

            //for (int i = 0; i < chktopic5.Items.Count; i++)
            //{
            //    ListItem item = new ListItem();
            //    item = chktopic5.Items[i];

            //    if (item.Selected)
            //    {
            //        if (string.IsNullOrEmpty(selectedCheckId1))
            //        {
            //            selectedCheckId1 = item.Value;
            //        }
            //        else
            //        {
            //            selectedCheckId1 += "," + item.Value;
            //        }
            //    }
            //}
            //string[] selectedTopicss = selectedCheckId1.Split(',');

            //foreach (ListItem item in chktopic5.Items)
            //{
            //    if (selectedTopicss.Contains(item.Value))
            //    {
            //        string selectedOtherID = item.Value;
            //        item.Selected = true;

            //        if (item != null && item.Selected && item.Text == "Others" && txt_specify_pre.Text == "")
            //        {
            //            selectedOtherID = item.Value;
            //            pnl.Visible = true;
            //            ClientMessaging("Please fill specify your preferred time for a mentoring session!");
            //            txt_specify_pre.Focus();
            //            return false;
            //        }
            //        else if (item != null && !item.Selected && item.Text != "Others")
            //        {
            //            pnl.Visible = false;
            //            txt_specify_pre.Text = "";
            //        }
            //        else if (item != null && !item.Selected && item.Text != "")
            //        {
            //            txt_specify_pre.Text = "";
            //        }
            //    }
            //    else
            //    {
            //        item.Selected = false;
            //    }
            //}

            string selectedCheckId6 = ""; int count1 = 0;

            for (int i = 0; i < chktopic6.Items.Count; i++)
            {
                ListItem item = new ListItem();
                item = chktopic6.Items[i];

                if (item.Selected)
                {
                    count1 = count1 + 1;
                    if (string.IsNullOrEmpty(selectedCheckId6))
                    {
                        selectedCheckId6 = item.Value;
                    }
                    else
                    {
                        selectedCheckId6 += "," + item.Value;
                    }
                }
            }

            string[] selectedTopices = selectedCheckId6.Split(',');

            foreach (ListItem item in chktopic6.Items)
            {
                if (selectedTopices.Contains(item.Value))
                {
                    string selectedOtherID = item.Value;
                    item.Selected = true;

                    if (item != null && item.Selected && item.Text == "Other" && txt_specify3.Text == "")
                    {
                        selectedOtherID = item.Value;
                        pnl.Visible = true;
                        ClientMessaging("You selected other, Please specify other preferred way to connect.");
                        txt_specify3.Focus();
                        return false;
                    }
                    else if (item != null && !item.Selected && item.Text != "Other")
                    {
                        pnl.Visible = false;
                        txt_specify3.Text = "";
                    }
                    else if (item != null && !item.Selected && item.Text != "")
                    {
                        txt_specify3.Text = "";
                    }
                }
                else
                {
                    item.Selected = false;
                }
            }

            if (count1 == 0)
            {
                ClientMessaging("Please choose at least one preferred way to connect.");
                return false;
            }

            if (txtbckground7.Text == "")
            {
                ClientMessaging("Please fill tell something about yourself and how you can help mentees.!!!");
                txtbckground7.Focus();
                return false;
            }

            if (!chkDeclaration.Checked)
            {
                ClientMessaging("Please accept the declaration before submitting.");
                return false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return true;
    }

    protected DataSet GetMain()
    {
        try
        {
            string value = string.Empty;
            DataSet ds = DAobj.GetSchema("ALM_Become_Mentor_Details");
            DataRow dr = ds.Tables[0].NewRow();
            dr["pk_mdtlid"] = "0";
            dr["alumniid"] = Session["AlumniID"];

            string selectedCheckIds = "";

            for (int i = 0; i < chktopic.Items.Count; i++)
            {
                ListItem item = new ListItem();
                item = chktopic.Items[i];

                if (item.Selected)
                {
                    if (string.IsNullOrEmpty(selectedCheckIds))
                    {
                        selectedCheckIds = item.Value;
                    }
                    else
                    {
                        selectedCheckIds += "," + item.Value;
                    }
                }
            }

            if (chktopic.SelectedItem != null && !string.IsNullOrEmpty(chktopic.SelectedItem.Text))
            {
                dr["topicids"] = selectedCheckIds.ToString();
            }
            else
            {
                dr["topicids"] = DBNull.Value;
            }

            if (chktopic.SelectedItem != null && !string.IsNullOrEmpty(chktopic.SelectedItem.Text))
            {
                dr["other_topic"] = txtspcfy.Text;
            }
            else
            {
                dr["other_topic"] = DBNull.Value;
            }

            string selectedCheckIds4 = "";

            for (int i = 0; i < chktopic2.Items.Count; i++)
            {
                ListItem item = new ListItem();
                item = chktopic2.Items[i];

                if (item.Selected)
                {
                    if (string.IsNullOrEmpty(selectedCheckIds4))
                    {
                        selectedCheckIds4 = item.Value;
                    }
                    else
                    {
                        selectedCheckIds4 += "," + item.Value;
                    }
                }
            }

            if (chktopic2.SelectedItem != null && !string.IsNullOrEmpty(chktopic2.SelectedItem.Text))
            {
                dr["like_to_mentor"] = selectedCheckIds4.ToString();
            }
            else
            {
                dr["like_to_mentor"] = DBNull.Value;
            }

            string selectedCheckId = "";

            for (int i = 0; i < chktopic2.Items.Count; i++)
            {
                ListItem item = new ListItem();
                item = chktopic2.Items[i];

                if (item.Selected)
                {
                    if (string.IsNullOrEmpty(selectedCheckId))
                    {
                        selectedCheckId = item.Value;
                    }
                    else
                    {
                        selectedCheckId += "," + item.Value;
                    }
                }
            }

            //string selectedCheckId1 = "";

            //for (int i = 0; i < chktopic5.Items.Count; i++)
            //{
            //    ListItem item = new ListItem();
            //    item = chktopic5.Items[i];

            //    if (item.Selected)
            //    {
            //        if (string.IsNullOrEmpty(selectedCheckId1))
            //        {
            //            selectedCheckId1 = item.Value;
            //        }
            //        else
            //        {
            //            selectedCheckId1 += "," + item.Value;
            //        }
            //    }
            //}

            //if (chktopic5.SelectedItem != null && !string.IsNullOrEmpty(chktopic5.SelectedItem.Text))
            //{
            //    dr["preferredtime_mentor_for"] = selectedCheckId1.ToString();
            //}
            //else
            //{
            //    dr["preferredtime_mentor_for"] = DBNull.Value;
            //}

            //if (chktopic5.SelectedItem != null && !string.IsNullOrEmpty(chktopic5.SelectedItem.Text))
            //{
            //    dr["other_preferred_time"] = txt_specify_pre.Text;
            //}
            //else
            //{
            //    dr["other_preferred_time"] = DBNull.Value;
            //}

            string selectedCheckId2 = "";

            for (int i = 0; i < chktopic6.Items.Count; i++)
            {
                ListItem item = new ListItem();
                item = chktopic6.Items[i];

                if (item.Selected)
                {
                    if (string.IsNullOrEmpty(selectedCheckId2))
                    {
                        selectedCheckId2 = item.Value;
                    }
                    else
                    {
                        selectedCheckId2 += "," + item.Value;
                    }
                }
            }

            if (chktopic6.SelectedItem != null && !string.IsNullOrEmpty(chktopic6.SelectedItem.Text))
            {
                dr["conversation_by"] = selectedCheckId2.ToString();
            }
            else
            {
                dr["conversation_by"] = DBNull.Value;
            }

            if (chktopic6.SelectedItem != null && !string.IsNullOrEmpty(chktopic6.SelectedItem.Text))
            {
                dr["other_conversation"] = txt_specify3.Text;
            }
            else
            {
                dr["other_conversation"] = DBNull.Value;
            }

            //if (txtbckground.Text != null && !string.IsNullOrEmpty(txtbckground.Text.ToString()))
            //{
            //    dr["domains_mentor"] = txtbckground.Text.ToString();
            //}
            //else
            //{
            //    dr["domains_mentor"] = DBNull.Value;
            //}

            if (txtbckground2.Text != null && !string.IsNullOrEmpty(txtbckground2.Text.ToString()))
            {
                dr["skills_mentor"] = txtbckground2.Text.ToString();
            }
            else
            {
                dr["skills_mentor"] = DBNull.Value;
            }

            if (txtbckground7.Text != null && !string.IsNullOrEmpty(txtbckground7.Text.ToString()))
            {
                dr["message_mentor"] = txtbckground7.Text.ToString();
            }
            else
            {
                dr["message_mentor"] = DBNull.Value;
            }

            dr["isDeclarationAccepted"] = chkDeclaration.Checked;

            ds.Tables[0].Rows.Add(dr);
            return ds;
        }
        catch (Exception exp)
        {
            lblmsg.Text = exp.Message;
            return null;
        }
    }

    public void getRequests(int alumniID)
    {
        DataSet ds = getallreq(alumniID);
        if (ds.Tables[0].Rows.Count > 0)
        {
            pk_MentorID = Convert.ToInt32(ds.Tables[0].Rows[0]["pk_mdtlid"]);
            Session["MentorID"] = pk_MentorID;
        }
    }

    private DataSet getallreq(int almID)
    {
        clears();
        names.Add("@alumniid"); values.Add(almID); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("Alm_GetmentorReq", values, names, types);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.CommandName.ToUpper().ToString() == "SUBMIT")
            {
                if (!validation())
                {
                    return;
                }

                string message = "";
                DataSet DsMain = new DataSet();
                DsMain = GetMain();
                dsxml = DsMain.GetXml();
                pk_MentorID = Convert.ToInt32(Session["AlumniID"]);

                if (InsertRecord(ref message) > 0)
                {
                    ClientMessaging("Request Submitted Successfully.!!!");
                    Response.Redirect("../Alumni/Alm_BecomeMentor.aspx");
                }
                else
                {
                    ClientMessaging("Something went wrong.!");
                }
            }
            //else
            //{
            //    try
            //    {
            //        //To update Records
            //        if (btnSave.CommandName.ToUpper().ToString() != "SUBMIT")
            //        {
            //            if (!validation())
            //            {
            //                return;
            //            }
            //            string message = "";
            //            DataSet DsMain = new DataSet();
            //            DsMain = GetMain();
            //            dsxml = DsMain.GetXml();
            //            pk_MentorID = Convert.ToInt32(Session["AlumniID"]);
            //            if (UpdateRecord(ref message) > 0)
            //            {
            //                ClientMessaging("Request Updated Successfully.!!!");
            //            }
            //            else
            //            {
            //                ClientMessaging("Something went wrong.!");
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        lblmsg.Text = ex.Message;
            //    }
            //}
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    public int UpdateRecord(ref string Message)
    {
        clear();
        name.Add("@doc"); value.Add(dsxml); type.Add(SqlDbType.VarChar);
        name.Add("@pk_MentorID"); value.Add(pk_MentorID); type.Add(SqlDbType.Int);
        if (DAobj.ExecuteTransactionMsg("Alm_insertMentor_Details_Upd", value, name, type, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    protected void BindToTopicsTextByFlagT(int flagT)
    {
        try
        {
            DataSet ds = FillToTopicsTextByFlagT(flagT).GetDataSet();
            chktopic.DataSource = ds;
            chktopic.DataTextField = "topic_text";
            chktopic.DataValueField = "pk_topicid";
            chktopic.DataBind();
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message.ToString());
        }
    }

    protected void BindToTopicsTextByFlagTT(int flagT)
    {
        DataSet ds = FillToTopicsTextByFlagTT(flagT).GetDataSet();
        chktopic2.DataSource = ds;
        chktopic2.DataTextField = "topic_text";
        chktopic2.DataValueField = "pk_topicid";
        chktopic2.DataBind();
    }

    protected void BindToTopicsTextByFlag_chktopic5(int flagT)
    {
        DataSet ds = FillToTopicsTextByFlagTT(flagT).GetDataSet();
        //chktopic5.DataSource = ds;
        //chktopic5.DataTextField = "topic_text";
        //chktopic5.DataValueField = "pk_topicid";
        //chktopic5.DataBind();
    }

    protected void BindToTopicsTextByFlag_chktopic6(int flagT)
    {
        DataSet ds = FillToTopicsTextByFlagTT(flagT).GetDataSet();
        chktopic6.DataSource = ds;
        chktopic6.DataTextField = "topic_text";
        chktopic6.DataValueField = "pk_topicid";
        chktopic6.DataBind();
    }

    public static StoredProcedure FillToTopicsTextByFlagT(int? flag)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_TopicsText_By_FlagT", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@flagT", flag, DbType.Int32);
        return sp;
    }

    public static StoredProcedure FillToTopicsTextByFlagTT(int? flag)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_TopicsText_By_FlagTT", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@flagT", flag, DbType.Int32);
        return sp;
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Alumni/ALM_Alumni_Home.aspx");
    }

    protected void chktopic_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string selectedOtherID = "";

            for (int i = 0; i < chktopic.Items.Count; i++)
            {
                ListItem item = new ListItem();
                item = chktopic.Items[i];

                if (item != null && item.Selected && item.Text == "Other")
                {
                    selectedOtherID = item.Value;
                    pnl.Visible = true;
                }
                else if (item != null && !item.Selected && item.Text != "Other")
                {
                    pnl.Visible = false;
                    txtspcfy.Text = "";
                }
                else if (item != null && !item.Selected && item.Text != "")
                {
                    pnl.Visible = false;
                    txtspcfy.Text = "";
                }

            }
        }
        catch (Exception Ex)
        {
            lblmsg.Text = Ex.Message;
        }
    }

    //protected void chktopic5_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string selectedOtherID = "";

    //        for (int i = 0; i < chktopic5.Items.Count; i++)
    //        {
    //            ListItem item = new ListItem();
    //            item = chktopic5.Items[i];

    //            if (item != null && item.Selected && item.Text == "Others")
    //            {
    //                selectedOtherID = item.Value;
    //                pnll2.Visible = true;
    //            }
    //            else if (item != null && !item.Selected && item.Text != "Others")
    //            {
    //                pnll2.Visible = false;
    //            }
    //            else if (item != null && !item.Selected && item.Text != "")
    //            {
    //                txt_specify_pre.Text = "";
    //            }
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        lblmsg.Text = Ex.Message;
    //    }
    //}

    // protected void chktopic6_SelectedIndexChanged(object sender, EventArgs e)
    // {
        // try
        // {
            // string selectedOtherID = "";

            // for (int i = 0; i < chktopic6.Items.Count; i++)
            // {
                // ListItem item = new ListItem();
                // item = chktopic6.Items[i];

                // if (item != null && item.Selected && item.Text == "Other")
                // {
                    // selectedOtherID = item.Value;
                    // pnl3.Visible = true;
                // }
                // else if (item != null && !item.Selected && item.Text != "Other")
                // {
                    // //pnl3.Visible = false;
                    // //txt_specify3.Text = "";
                // }
                // else if (item != null && !item.Selected && item.Text != "")
                // {
                    // //pnl3.Visible = false;
                    // //txt_specify3.Text = "";
                // }
            // }
        // }
        // catch (Exception Ex)
        // {
            // lblmsg.Text = Ex.Message;
        // }
    // }
	
	protected void chktopic6_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string anyOptionText = "Any of the above";
            string otherOptionText = "Other";

            int anyIndex = -1;
            int otherIndex = -1;

            // Identify indexes of "Any of the above" and "Other"
            for (int i = 0; i < chktopic6.Items.Count; i++)
            {
                string itemText = chktopic6.Items[i].Text.Trim();

                if (itemText == anyOptionText)
                    anyIndex = i;
                else if (itemText == otherOptionText)
                    otherIndex = i;
            }
			
            if (anyIndex == -1)
                return;

            bool isAnySelected = chktopic6.Items[anyIndex].Selected;

            if (isAnySelected)
            {
                // "Any of the above" is selected → select all individual items
                for (int i = 0; i < anyIndex; i++)
                {
                    if (chktopic6.Items[i].Text.Trim() != otherOptionText)
                    {
                        chktopic6.Items[i].Selected = true;
                    }
                }
            }

            // Show/hide panel for "Other"
            pnl3.Visible = (otherIndex != -1 && chktopic6.Items[otherIndex].Selected);
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error: " + ex.Message;
        }
    }
}