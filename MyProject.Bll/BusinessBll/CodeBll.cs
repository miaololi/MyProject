using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text;

namespace MyProject.Bll
{
    public class CodeBll : BaseBll
    {
        public static HttpResponseMessage GetCode(string guid)
        {
            int i;
            Color clr;
            var codeW = 114;
            var codeH = 40;
            var fontSize = 25;
            var chkCode = string.Empty;

            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Brown, Color.DarkBlue };
            string[] font = { "Times New Roman", "Verdana", "Arial", "Gungsuh", "Impact" };
            char[] character =
            {
                '1','2', '3', '4', '5', '6', '8', '9', '0'
            };

            //生成四位随机数
            var rnd = new Random();
            for (i = 0; i < 4; i++)
            {
                chkCode = chkCode + character[rnd.Next(character.Length)];
            }
            //RedisHelper.Set(guid, chkCode);
            //创建绘图图面
            var bmp = new Bitmap(codeW, codeH);
            var g = Graphics.FromImage(bmp);
            g.Clear(Color.LightYellow);

            for (i = 0; i < 1; i++)
            {
                //设置坐标,颜色,绘制线
                var x1 = rnd.Next(codeW);
                var y1 = rnd.Next(codeH);
                var x2 = rnd.Next(codeW);
                var y2 = rnd.Next(codeH);
                clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }

            //绘制随机数
            for (i = 0; i < chkCode.Length; i++)
            {
                var fnt = font[rnd.Next(font.Length)];
                var ft = new Font(fnt, fontSize);
                clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), i * 18f + 2f, 0f);
            }

            //绘制点
            for (i = 0; i < 100; i++)
            {
                var x = rnd.Next(bmp.Width);
                var y = rnd.Next(bmp.Height);
                clr = color[rnd.Next(color.Length)];
                bmp.SetPixel(x, y, clr);
            }
            try
            {
                var imgStream = new MemoryStream();
                bmp.Save(imgStream, ImageFormat.Jpeg);

                var resp = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    //Content = new StreamContent(imgStream)
                    //或者
                    Content = new ByteArrayContent(imgStream.ToArray())
                };
                resp.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpg");
                return resp;
            }
            finally
            {
                bmp.Dispose();
                g.Dispose();
            }
        }
    }
}
