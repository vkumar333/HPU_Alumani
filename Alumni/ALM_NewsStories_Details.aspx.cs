using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using SubSonic;
using System.Data.SqlClient;

public partial class Alumni_ALM_NewsStories_Details : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    crypto cpt = new crypto();

    public object description { get; private set; }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable NewsStoriesDetails(int Pk_Stories_id)
    {
        ClearArrayLists();
        names.Add("@Pk_Stories_id"); types.Add(SqlDbType.NVarChar); values.Add(Pk_Stories_id);
        return Dobj.GetDataTable("Show_NewsStories_Details_By_ID", values, names, types);
    }

    public static StoredProcedure alm_alumni_check(int pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_alumni_check", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure Get_ALM_Alumni_Details(int pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Alumni_Details_Get_By_ID", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_AlumniId", pk_alumniid, DbType.Int32);
        return sp;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AlumniID"].ToString() == "" || Session["AlumniID"] == null)
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }

        int check = int.Parse(alm_alumni_check(int.Parse(Session["AlumniID"].ToString())).GetDataSet().Tables[0].Rows[0][0].ToString());

        if (check < 1)
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }

        if (Request.QueryString["ID"] != null)
        {
            string postID = cpt.DecodeString(Request.QueryString["ID"].ToString());
            int pk_Storiesid = Convert.ToInt32(postID);

            if (pk_Storiesid != 0)
            {
                NewsStoriesRepeter(pk_Storiesid);
                BindPostComments(pk_Storiesid);
                BindPostLikesCount(pk_Storiesid);
                getSocialURLLinks();
            }
        }
    }

    /// <summary>
    /// Bind Repeater
    /// </summary>
    /// <param name="Pk_Stories_Id"></param>
    private void NewsStoriesRepeter(int Pk_Stories_Id)
    {
        DataTable dt = new DataTable();
        dt = NewsStoriesDetails(Pk_Stories_Id);
        if (dt.Rows.Count > 0)
        {
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            //RepeventsAll.DataSource = dt;
            //RepeventsAll.DataBind();

            string fileName = "";

            fileName = dt.Rows[0]["File_name"].ToString().Trim();

            if (fileName != "")
            {
                if (dt.Rows[0]["File_name"].ToString() != "None")
                {
                    Imge.Src = dt.Rows[0]["File_name"].ToString();
                }
            }
            else
            {
                Imge.Src = "~/alumni/stuimage/No_image.png";
            }

            lblHeading.Text = dt.Rows[0]["Heading"].ToString();
            lblStartDate.Text = dt.Rows[0]["Start_date"].ToString();
            lblDescription.Text = dt.Rows[0]["Description"].ToString();
        }
    }

    //protected void RepeventsAll_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    int postID = Convert.ToInt32(e.CommandArgument);

    //    if (e.CommandName == "Like")
    //    {
    //        crypto cpt = new crypto();
    //        string decId = cpt.DecodeString(Request.QueryString["ID"].ToString());
    //        int pk_Storiesid = Convert.ToInt32(decId);
    //        int createdBy = Convert.ToInt32(Session["AlumniID"].ToString());

    //        // Insert or delete like based on current state
    //        ToggleLike(postID, createdBy);

    //        // Reload posts
    //        NewsStoriesRepeter(pk_Storiesid);
    //    }
    //    else if (e.CommandName == "Comment")
    //    {
    //        crypto cpt = new crypto();
    //        string decId = cpt.DecodeString(Request.QueryString["ID"].ToString());
    //        int createdBy = Convert.ToInt32(Session["AlumniID"].ToString());

    //        string commentText = ((TextBox)e.Item.FindControl("txtComment")).Text;
    //        int pk_Storiesid = Convert.ToInt32(decId);

    //        Repeater rptComments = (Repeater)e.Item.FindControl("rptComments");
    //        //InsertComments(postID, createdBy, commentText);

    //        if ((InsertComments(postID, createdBy, commentText).Execute()) > 0)
    //        {
    //            ClientMessaging("Comments is Posted Successfully!");
    //        }
    //        LoadComments(postID, rptComments);

    //        //Button btnComment = (Button)sender;
    //        //RepeaterItem item = (RepeaterItem)btnComment.NamingContainer;
    //        //Repeater rptComments = (Repeater)item.FindControl("rptComments");
    //        //Anthem.TextBox txtComment = (Anthem.TextBox)item.FindControl("txtComment");

    //        //if ((InsertComments(postID, createdBy, commentText.ToString()).Execute()) > 0)
    //        //{
    //        //    ClientMessaging("Comments is Posted Successfully!");
    //        //}
    //        //LoadComments(postID, rptComments);

    //        // Reload posts
    //        NewsStoriesRepeter(pk_Storiesid);
    //    }
    //}

    protected void ToggleLike(int postID, int createdBy)
    {
        bool userLiked = CheckIfUserLikedPost(postID, createdBy);

        if (userLiked)
        {
            UnlikePost(postID, createdBy);
        }
        else
        {
            LikePost(postID, createdBy);
        }
    }

    private bool CheckIfUserLikedPost(int postID, int createdBy)
    {
        DataTable dt = CheckIfUserLikedPosts(postID, createdBy);
        int count = Convert.ToInt32(dt.Rows[0]["Cnt"].ToString());
        if (count > 0)
            return true;
        else
            return false;
    }

    //private void LikePost(int postID, int createdBy)
    //{
    //    // Insert a new like record into the Likes table
    //    string query = "INSERT INTO ALM_News_Stories_Likes (PostID, CreatedBy, CreatedAt) VALUES (@PostID, @CreatedBy, GETDATE())";
    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        using (SqlCommand command = new SqlCommand(query, connection))
    //        {
    //            command.Parameters.AddWithValue("@PostID", postID);
    //            command.Parameters.AddWithValue("@CreatedByUserID", createdBy);
    //            connection.Open();
    //            command.ExecuteNonQuery();
    //        }
    //    }
    //}

    //private void UnlikePost(int postID, int createdBy)
    //{
    //    // Delete the like record from the Likes table
    //    string query = "DELETE FROM ALM_News_Stories_Likes WHERE PostID = @PostID AND CreatedBy = @CreatedBy";
    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        using (SqlCommand command = new SqlCommand(query, connection))
    //        {
    //            command.Parameters.AddWithValue("@PostID", postID);
    //            command.Parameters.AddWithValue("@CreatedBy", createdByUserID);
    //            connection.Open();
    //            command.ExecuteNonQuery();
    //        }
    //    }
    //}

    private DataTable LikePost(int Pk_Stories_id, int CreatedBy)
    {
        ClearArrayLists();
        names.Add("@PostID"); types.Add(SqlDbType.NVarChar); values.Add(Pk_Stories_id);
        names.Add("@CreatedBy"); types.Add(SqlDbType.NVarChar); values.Add(CreatedBy);
        return Dobj.GetDataTable("ALM_NewsStories_Details_likePost", values, names, types);
    }

    private DataTable UnlikePost(int Pk_Stories_id, int CreatedBy)
    {
        ClearArrayLists();
        names.Add("@PostID"); types.Add(SqlDbType.NVarChar); values.Add(Pk_Stories_id);
        names.Add("@CreatedBy"); types.Add(SqlDbType.NVarChar); values.Add(CreatedBy);
        return Dobj.GetDataTable("ALM_NewsStories_Details_UnlikePost", values, names, types);
    }

    private DataTable CheckIfUserLikedPosts(int Pk_Stories_id, int CreatedBy)
    {
        ClearArrayLists();
        names.Add("@PostID"); types.Add(SqlDbType.NVarChar); values.Add(Pk_Stories_id);
        names.Add("@CreatedBy"); types.Add(SqlDbType.NVarChar); values.Add(CreatedBy);
        return Dobj.GetDataTable("ALM_NewsStories_Details_Like_UnlikePost_Check", values, names, types);
    }

    public static StoredProcedure DetailsEdit(int Pk_Stories_id)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_DetailsEdit", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Pk_Stories_id", Pk_Stories_id, DbType.Int32);
        return sp;
    }

    public static StoredProcedure LikePost(int? PostID, int? CreatedBy)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_NewsStories_Details_likePost", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@PostID", PostID, DbType.Int32);
        sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.Int32);
        return sp;
    }

    public static StoredProcedure UnlikePost(int? PostID, int? CreatedBy)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_NewsStories_Details_UnlikePost", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@PostID", PostID, DbType.Int32);
        sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.Int32);
        return sp;
    }

    public static StoredProcedure InsertComments(int? PostID, int? CreatedBy, string commentText)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_NewsStories_Details_Comment_Ins", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@PostID", PostID, DbType.Int32);
        sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.Int32);
        sp.Command.AddParameter("@CommentText", commentText, DbType.String);
        return sp;
    }

    public static StoredProcedure LoadStoriesComments(int? PostID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_News_Stories_Comments_Select", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@PostID", PostID, DbType.Int32);
        return sp;
    }

    public static StoredProcedure AddPostComments(int? PostID, string commentText, int? CreatedBy)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_NewsStories_Details_Comments_Ins", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@PostID", PostID, DbType.Int32);
        sp.Command.AddParameter("@CommentText", commentText, DbType.String);
        sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.Int32);
        return sp;
    }

    public static StoredProcedure GetAllLikesCount(int? PostID)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_NewsStories_Details_LikesPost_Count", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@PostID", PostID, DbType.Int32);
        return sp;
    }

    //protected void btnComment_Click(object sender, EventArgs e)
    //{
    //    crypto cpt = new crypto();
    //    string decId = cpt.DecodeString(Request.QueryString["ID"].ToString());
    //    int postID = Convert.ToInt32(decId);
    //    int createdBy = Convert.ToInt32(Session["AlumniID"].ToString());

    //    Button btnComment = (Button)sender;
    //    RepeaterItem item = (RepeaterItem)btnComment.NamingContainer;
    //    Repeater rptComments = (Repeater)item.FindControl("rptComments");
    //    Anthem.TextBox txtComment = (Anthem.TextBox)item.FindControl("txtComment");

    //    if ((InsertComments(postID, createdBy, txtComment.Text.Trim().ToString()).Execute()) > 0)
    //    {
    //        ClientMessaging("Comments is Posted Successfully!");
    //    }
    //    LoadComments(postID, rptComments);
    //}

    protected void LoadComments(int postID, Repeater rptCmt)
    {
        DataSet dsS = LoadStoriesComments(postID).GetDataSet();

        if (dsS.Tables[0].Rows.Count > 0)
        {
            rptCmt.DataSource = null;
            rptCmt.DataBind();

            rptCmt.DataSource = dsS.Tables[0];
            rptCmt.DataBind();
        }
        else
        {
            rptCmt.DataSource = null;
            rptCmt.DataBind();
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void BindPostComments(int postId)
    {
        using (DataSet dsPostCmts = LoadStoriesComments(postId).GetDataSet())
        {
            int commentsCount = dsPostCmts.Tables[0].Rows.Count;
            lblCommentsCount.Text = commentsCount == 0 ? "Comments " : "Comments (" + commentsCount + ")";

            if (dsPostCmts.Tables[0].Rows.Count > 0)
            {
                rptPostComments.DataSource = dsPostCmts.Tables[0];
                rptPostComments.DataBind();
            }
        }
    }

    protected void BindPostLikesCount(int postId)
    {
        using (DataSet dsCnts = GetAllLikesCount(postId).GetDataSet())
        {
            if (dsCnts.Tables[0].Rows.Count > 0)
            {
                var likesCount = dsCnts.Tables[0].Rows[0]["Cnt"].ToString();
                lblPostLikesCount.Text = likesCount != "0" ? "Likes: " + likesCount : "";
            }
            else
            {
                lblPostLikesCount.Text = "";
            }
        }
    }

    protected void btnPostLike_Click(object sender, EventArgs e)
    {
        int postId = Convert.ToInt32(cpt.DecodeString(Request.QueryString["ID"].ToString()));
        int userId = GetCurrentUserId();
        //LikePost(postId, userId);
        ToggleLike(postId, userId);
        BindPostLikesCount(postId);
    }

    protected void btnPostComment_Click(object sender, EventArgs e)
    {
        int postId = Convert.ToInt32(cpt.DecodeString(Request.QueryString["ID"].ToString()));
        int userId = GetCurrentUserId();
        string commentText = txtPostComment.Text.Trim();
        if (!string.IsNullOrEmpty(commentText))
        {
            AddPostComment(postId, commentText, userId);
            BindPostComments(postId);
            txtPostComment.Text = "";
        }
    }

    private int GetCurrentUserId()
    {
        int alumniuser = Convert.ToInt32(Session["AlumniID"].ToString());
        return alumniuser;
    }

    protected void AddPostComment(int postId, string commentText, int userId)
    {
        if ((AddPostComments(postId, txtPostComment.Text.Trim().ToString(), userId).Execute()) > 0)
        {
            ClientMessaging("Comments is Posted Successfully!");
        }
    }

    public List<Comment> GetCommentsForPost(int postId)
    {
        List<Comment> comments = new List<Comment>();

        using (DataSet dsCmts = LoadStoriesComments(postId).GetDataSet())
        {
            if (dsCmts != null && dsCmts.Tables.Count > 0)
            {
                foreach (DataRow row in dsCmts.Tables[0].Rows)
                {
                    Comment comment = new Comment
                    {
                        pk_CommentID = Convert.ToInt32(row["pk_CommentID"]),
                        PostID = Convert.ToInt32(row["PostID"]),
                        CommentText = row["CommentText"].ToString(),
                        CreatedBy = Convert.ToInt32(row["CreatedBy"])
                    };
                    comments.Add(comment);
                }
            }
        }
        return comments;
    }

    public class Comment
    {
        public int pk_CommentID { get; set; }
        public int PostID { get; set; }
        public string CommentText { get; set; }
        public int CreatedBy { get; set; }
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
}