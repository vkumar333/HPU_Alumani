<%@ Application Language="C#" %>

<script RunAt="server">
    void Application_Start(object sender, EventArgs e)
    {
        Application["TotalUsers"] = 1;
    }

    void Application_End(object sender, EventArgs e)
    {
        //Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
       // Exception ex = Server.GetLastError();       
       //string exx=ex.Message;               
       // Response.Redirect("AppError.aspx");       
       
    }

    void Session_Start(object sender, EventArgs e)
    {    
       
        Session["SysUser"] = "";//For System User
        Session["ModuleID"] = "";
        Session["TempModID"] = "";
        Session["UserID"] = "";
        Session["RoleLevelID"] = "";
        Session["RoleID"] = "";
        Session["MappedAlias"] = "";
        Session["LoginName"] = "";
        Session["CampusName"] = "";
        Session["CampusID"] = "3";
        Session["LocationID"] = "";
        Session["LocationName"] = "";
        Session["cmodule"] = "";
        Session["AcctFinId"] = "";
        Session["LRSFinId"] = "";
        Session["SchemeID"] = "";//for maintaing schemeid
        Session["Division"] = "";
        Session["DivID"] = "";
        Session["DDOID"] = "";
        Session["DDO"] = "";
        Session["language"] = "en-US";
        Session["Pk_BedId"] = "";
        Session["fk_Unitid"] = ""; //By Varun in Veterinary
        Session["UserPages"] = "";//maintain user age rights
        Session["empID"] = "";
		Session["lblProfileCnt"] = "";
        
         Session["modulename"] = "";
         Session["moduleimage"] = "";
        Application.Lock();
        Application["TotalUsers"] = Convert.ToInt32(Application["TotalUsers"]) + 1;
        Application.UnLock();

    }

    void Session_End(object sender, EventArgs e)
    {
      
        Session["language"] = "";
        Session.Remove("RoleLevelID");
        Session.Remove("UserID");
        Session.Remove("LoginName");
        Session.Remove("ModuleID");
        Session.Remove("RoleID");
        Session.Remove("MappedAlias");
        Session.Remove("sta");
        Session.Remove("SysUser");
        Session.Remove("Studentid");
        Session.Remove("AcctFinId"); // Sangeet For Accounts
        Session.Remove("LRSFinId"); // Saurabh Jain For LRS
        Session.Remove("SchemeID"); // Saurabh Jain For LRS
        Session.Remove("Division");
        Session.Remove("DivID");
        Session.Remove("DDOID");
        Session.Remove("DDO");

        Session["cmodule"] = "";
        Session["TempModID"] = "";
        Session["CampusName"] = "";
        Session["CampusID"] = "";
        Session["UserPages"] = "";//maintain user age rights
        Session["empID"] = "";
        Session["modulename"] = "";
        Session["moduleimage"] = "";
        Session.Clear();

        Application.Lock();
        Application["TotalUsers"] = Convert.ToInt32(Application["TotalUsers"]) - 1;
        Application.UnLock();

        Session.Abandon();
        //Response.Redirect("SessionEndPage.html", true);
        //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
    }

   
  
</script>

