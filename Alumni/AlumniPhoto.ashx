<%@ WebHandler Language="C#" Class="AlumniPhoto" %>
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
using SubSonic;
using System.IO;
using System.Drawing;

public class AlumniPhoto : System.Web.UI.Page, IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        int alumniId = 0;
        if (Session["AlumniID"].ToString() == "")
            alumniId = Convert.ToInt32(Session["EmpView_AlumniID"].ToString());
        else
            alumniId = Convert.ToInt32(Session["AlumniID"].ToString());

        //Getting Photo details
        IDataReader rdr = IUMSNXG.SP.ALM_SP_AlumniRegistration_Edit(alumniId).GetReader();
        DataTable dtt = new DataTable();
        dtt.Load(rdr);

        rdr.Close();


        Stream str = new MemoryStream((Byte[])dtt.Rows[0]["imgattach"]);

        Bitmap loBMP = new Bitmap(str);
        Bitmap bmpOut = new Bitmap(45, 45);

        Graphics g = Graphics.FromImage(bmpOut);

        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

        g.FillRectangle(Brushes.White, 0, 0, 45, 45);

        g.DrawImage(loBMP, 0, 0, 45, 45);


        MemoryStream ms = new MemoryStream();

        bmpOut.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

        byte[] bmpBytes = ms.GetBuffer();

        bmpOut.Dispose();

        ms.Close();
        context.Response.BinaryWrite(bmpBytes);
        context.Response.End();

    }

}