using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public partial class ImCaptch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Clear();
        int height = 30;
        int width = 100;
        Bitmap bmp = new Bitmap(width, height);

        RectangleF rectf = new RectangleF(10, 5, 200, 60);

        Point p1 = new Point(-180, 5);
        Point p2 = new Point(150, 25);

        Graphics g = Graphics.FromImage(bmp);
        g.Clear(Color.White);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.DrawString(Session["captcha"].ToString(), new Font("Geneva", 15, FontStyle.Bold), Brushes.Black, rectf);
        // g.DrawRectangle(new Pen(Color.Red), 1, 1, width - 2, height - 2);
        g.DrawLine(new Pen(Color.Black), p1, p2);
        g.Flush();
        //DrawRandomLines(g);
        Response.ContentType = "image/jpeg";
        bmp.Save(Response.OutputStream, ImageFormat.Jpeg);
        g.Dispose();
        bmp.Dispose();

    }
}