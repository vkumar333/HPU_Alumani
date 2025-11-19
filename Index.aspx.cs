using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // Response.Redirect("https://alumni.hpushimla.in/Alumni/Alm_Default.aspx");
        Response.Redirect("http://localhost:52835/Alumni/Alm_Default.aspx");
    }
}