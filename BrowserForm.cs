using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliver
{
    public partial class BrowserForm : Form
    {
        public BrowserForm()
        {
            InitializeComponent();

            //browser.ScriptErrorsSuppressed = true;
            //browser.IsWebBrowserContextMenuEnabled = false;
            //browser.AllowWebBrowserDrop = false; 
            browser.ProgressChanged += Browser_ProgressChanged;
            browser.ObjectForScripting = this;
            //browser.DocumentText = PageHtml;
            browser.Url = new Uri(get_path("index.html"));
        }

        static string get_path(string file_name)
        {
            return "file:///" + PathRoutines.GetDirFromPath(Application.ExecutablePath) + file_name;
        }

        static string get_content(string file_name)
        {
            return System.IO.File.ReadAllText(get_path(file_name));
        }

        private void Browser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs el)
        {
            //if (browser.ReadyState == WebBrowserReadyState.Interactive)
            //{
            //    HtmlElement s = browser.Document.CreateElement("script");
            //    s.SetAttribute("src", "https://code.jquery.com/jquery-2.2.0.min.js");
            //    browser.Document.Body.AppendChild(s);
            //}
            if (browser.ReadyState == WebBrowserReadyState.Complete)
            {
                //browser.Document.OpenNew(true);
                for (int i = 0; i < 4; i++)
                {

                }
                //                string script = @"
                //$('.col-lg-4.col-md-4.col-sm-4.col-xs-4.text-right').each(function (index, value) {
                ////alert($(this).find('._added').length);
                //    if($(this).find('._added').length > 0) return;
                ////alert($(this).find('a:first').attr('href'));
                //    var pid = $(this).find('a:first').attr('href');
                //    var image_src = $(this).closest('.media').find('img:first').attr('src');
                //    $(this).append(""<a class='btn btn-default _added' onclick='window.external.EditProduct(\"""" + pid + ""\"", \"""" + image_src + ""\""); ' href='#'>prices</a>"");
                //}); ";
                //                browser.Document.InvokeScript("eval", new object[] { script });

            }
        }

        static void AddNotification(string title, string text, string action_name, Action action)
        {
            This.Show();
            //this.title.Text = title;
            //this.text.Text = text;
            //this.action.Text = action_name;
            //this.action.Click += (object sender, EventArgs e) => { action?.Invoke(); };
            var e = This.browser.Document.CreateElement("div");
            e.InnerHtml = get_content("notification.html");
            This.browser.Document.Body.AppendChild(e);
        }

        static BrowserForm This = new BrowserForm();

        readonly string PageHtml = @"<html>
<head>
  <meta http-equiv='X-UA-Compatible' content='IE=edge'>
  <link rel = 'stylesheet' href='chrome-search://local-ntp/local-ntp.css'>
  <script src = 'jquery-2.2.0.min.js' ></ script >
</head>
<body> 
<script>
//alert(1);
    //alert($('body'));
</script>
</body>
</html>";

        readonly string NotificationHtml = @"
<div>
  <img src='https://www.google.com/logos/doodles/2016/holidays-2016-day-1-5727116688621568.2-res.png' class='image'/>
<div class='title'>dfgdfg</div>
<div class='message'>gdfgdfgd</div>
<a href = '#' class='button'>bcvbcbfbf</a>
</div>
";
    }
}
