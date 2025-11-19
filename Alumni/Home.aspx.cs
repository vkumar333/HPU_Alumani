using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SD = System.Drawing;

public partial class Home : System.Web.UI.Page
{
    public string UserName = "admin";
    public string UserImage = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/HPU_Alumni_PP__bc0e7d1f-1e80-4bd9-8f83-12cb1be46360_854602_20240529114405050PP_426818499_4384725.jpg";
    ConnClass ConnC = new ConnClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AlumniName"] != null)
        {           
            UserName = Session["AlumniName"].ToString();
            GetUserImage(UserName);
        }
        else
            Response.Redirect("Default.aspx");
    }

    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Login.aspx");
    }

    public void GetUserImage(string Username)
    {
        ////if (Username != null)
        ////{
        ////    string query = "select Photo from tbl_Users where UserName='" + Username + "'";

        ////    string ImageName = ConnC.GetColumnVal(query, "Photo");
        ////    if (ImageName != "")
        ////        UserImage = "images/DP/" + ImageName;
        ////}
        UserImage = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/HPU_Alumni_PP__bc0e7d1f-1e80-4bd9-8f83-12cb1be46360_854602_20240529114405050PP_426818499_4384725.jpg";

    }

  


}