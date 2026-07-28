using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.Public
{
    public partial class CreateCaptcha : System.Web.UI.Page
    {
        string code;
        Random rand = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "image/gif";

            CreateCaptchaImage();
        }

        private void CreateCaptchaImage()
        {
            if (Session["CaptchaCode"] == null)
            {
                Session["CaptchaCode"] = GenerateCaptchaText();
            }

            code = Session["CaptchaCode"].ToString();

            Bitmap bitmap = new Bitmap(200, 60, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bitmap);

            Pen pen = new Pen(Color.Yellow);

            Rectangle rect = new Rectangle(0, 0, 200, 60);

            SolidBrush blue = new SolidBrush(Color.LightCyan);
            SolidBrush green = new SolidBrush(Color.Green);

            g.DrawRectangle(pen, rect);
            g.FillRectangle(blue, rect);

            int counter = 0;

            for (int i = 0; i < code.Length; i++)
            {
                g.DrawString(code[i].ToString(),
                    new Font("Tahoma", rand.Next(16, 20), FontStyle.Bold),
                    green,
                    new PointF(10 + counter, 10));

                counter += 28;
            }

            DrawRandomLines(g);

            Response.Clear();
            Response.ContentType = "image/gif";

            bitmap.Save(Response.OutputStream, ImageFormat.Gif);

            g.Dispose();
            bitmap.Dispose();
        }

        //private void CreateCaptchaImage()
        //{
        //    code = GetRandomText();
        //    Bitmap bitmap = new Bitmap(200, 60, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        //    Graphics g = Graphics.FromImage(bitmap);
        //    Pen pen = new Pen(Color.Yellow);
        //    Rectangle rect = new Rectangle(0, 0, 200, 60);
        //    SolidBrush blue = new SolidBrush(Color.LightCyan);
        //    SolidBrush black = new SolidBrush(Color.Green);
        //    int counter = 0;
        //    g.DrawRectangle(pen, rect);
        //    g.FillRectangle(blue, rect);
        //    for (int i = 0; i < code.Length; i++)
        //    {
        //        g.DrawString(code[i].ToString(), new Font("Tahoma", 10 + rand.Next(15, 20), FontStyle.Bold), black, new PointF(10 + counter, 10));
        //        counter += 28;
        //    }
        //    DrawRandomLines(g);
        //    bitmap.Save(Response.OutputStream, ImageFormat.Gif);
        //    g.Dispose();
        //    bitmap.Dispose();
        //}
        private void DrawRandomLines(Graphics g)
        {
            SolidBrush yellow = new SolidBrush(Color.Black);
            for (int i = 0; i < 20; i++)
            {
                g.DrawLines(new Pen(yellow, 1), GetRandomPoints());
            }
        }
        private Point[] GetRandomPoints()
        {
            Point[] points = { new Point(rand.Next(0, 150), rand.Next(1, 150)), new Point(rand.Next(0, 200), rand.Next(1, 190)) };
            return points;
        }

        //private string GetRandomText()
        //{
        //    StringBuilder randomText = new StringBuilder();
        //    Random r = new Random();
        //    string create = "";
        //    string number = "0123456789";
        //    string alphabetsC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //    string alphabetsS = "abcdefghijklmnopqrstuvwxyz";
        //    create = create + number[r.Next(number.Length)];
        //    create = create + alphabetsC[r.Next(alphabetsC.Length)];
        //    create = create + alphabetsS[r.Next(alphabetsS.Length)];
        //    string alphabets = "12345679ABDEFGHQRTabcdefghjkmnpqrstwxyz";

        //    for (int j = 0; j <= 2; j++)
        //    {
        //        create = create + alphabets[r.Next(alphabets.Length)];
        //    }


        //    Session["CaptchaCode"] = ShuffleString(create);
        //    return Session["CaptchaCode"] as String;
        //}

        private string GenerateCaptchaText()
        {
            Random r = new Random();

            string number = "0123456789";
            string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lower = "abcdefghijklmnopqrstuvwxyz";
            string chars = "12345679ABDEFGHQRTabcdefghjkmnpqrstwxyz";

            string captcha = "";

            captcha += number[r.Next(number.Length)];
            captcha += upper[r.Next(upper.Length)];
            captcha += lower[r.Next(lower.Length)];

            for (int i = 0; i < 3; i++)
            {
                captcha += chars[r.Next(chars.Length)];
            }

            return ShuffleString(captcha);
        }

        static string ShuffleString(string str)
        {
            char[] chars = str.ToCharArray();
            Random random = new Random();
            for (int i = 0; i < chars.Length; i++)
            {
                int randomIndex = random.Next(i, chars.Length);
                char temp = chars[i];
                chars[i] = chars[randomIndex];
                chars[randomIndex] = temp;
            }
            return new string(chars);
        }
    }
}