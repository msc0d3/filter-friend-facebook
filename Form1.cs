using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using xNet;
//Code by Nguyễn Đình Quyết
//Nhận Code Tool Theo Yêu Cầu Giá Học Sinh Sinh Viên
//Liên Hệ fb.com/quyetpaylak1
//Donate little cafe - momo 0346444659
//Donate little cafe - techcombank 19036318903013 
namespace locfriend
{
    public partial class Form1 : Form
    {
    
        public Form1()
        {
            InitializeComponent();
        }
        string reac;
        string cmt;
        bool _isstop = false;
        private void  data(string cookie = null) 
        {
            while (_isstop)
            {
                Thread.Sleep(500);
            }
            HttpRequest http = new HttpRequest();
            http.Cookies = new CookieDictionary();
            http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4157.0 Safari/537.36 Edg/85.0.531.1";
            string html = "";
            http.AddHeader("accept-language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
            http.AddHeader("origin", "https://www.facebook.com");
            http.AddHeader("sec-fetch-dest", "empty");
            http.AddHeader("sec-fetch-mode", "cors");
            http.AddHeader("sec-fetch-site", "same-origin");
            var temp = cookie.Split(';');
            string id = Regex.Match(cookie, "c_user=(\\d+);").Groups[1].Value;
            while (_isstop)
            {
                Thread.Sleep(500);
            }
            foreach (var item in temp)
            {
                var temp2 = item.Split('=');
                if (temp2.Count() > 1)
                {
                    http.Cookies.Add(temp2[0], temp2[1]);
                }
            }
            while (_isstop)
            {
                Thread.Sleep(500);
            }
            html = http.Get("https://d.facebook.com").ToString();
            while (_isstop)
            {
                Thread.Sleep(500);
            }
            string fb_dtsg = Regex.Match(html, "name=\"fb_dtsg\" value=\"(.*?)\"").Groups[1].Value;
            while (_isstop)
            {
                Thread.Sleep(500);
            }
            //data của api lấy bạn bè k tương tác //
            string data = "fb_dtsg=" + fb_dtsg + "&q=node("+ id + "){timeline_feed_units.first(500).after(){page_info,edges{node{id,creation_time,feedback{reactors{nodes{id}},commenters{nodes{id}}}}}}}";
            string api = "https://www.facebook.com/api/graphql/"; //api lấy bạn bè k tương tác //
            while (_isstop)
            {
                Thread.Sleep(500);
            }
            string html2 = http.Post(api, data, "application/x-www-form-urlencoded; charset=UTF-8").ToString();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var obj = jss.Deserialize<dynamic>(html2);
            while (_isstop)
            {
                Thread.Sleep(500);
            }
            for (int i = 0; i < obj[id]["timeline_feed_units"]["edges"].Length - 1 ; i++)
            {
                while (_isstop)
                {
                    Thread.Sleep(500);
                }
                try
                {
                    int p = 0;
                    try { 
                    p = obj[id]["timeline_feed_units"]["edges"][i]["node"]["feedback"]["reactors"]["nodes"].Length ;
                    }
                    catch
                    {
                    }
                    if (p > 0)
                    {
                        while (_isstop)
                        {
                            Thread.Sleep(500);
                        }
                        for (int c = 0; c < p; c++)
                        {
                            while (_isstop)
                            {
                                Thread.Sleep(500);
                            }

                            reac += obj[id]["timeline_feed_units"]["edges"][i]["node"]["feedback"]["reactors"]["nodes"][c]["id"] + "-";
                        }
                    }
                }
                catch
                {
                    while (_isstop)
                    {
                        Thread.Sleep(500);
                    }
                }
                try
                {
                    int p = obj[id]["timeline_feed_units"]["edges"][i]["node"]["feedback"]["commenters"]["nodes"].Length;
                    if (p > 0)
                    {
                        while (_isstop)
                        {
                            Thread.Sleep(500);
                        }
                        for (int c = 0; c < p; c++)
                        {
                            while (_isstop)
                            {
                                Thread.Sleep(500);
                            }
                            cmt += obj[id]["timeline_feed_units"]["edges"][i]["node"]["feedback"]["commenters"]["nodes"][c]["id"] + "-";
                        }
                    }
                }
                catch
                {

                }

            }
            

            return; 
        }
        JavaScriptSerializer jssx = new JavaScriptSerializer();

        private string getFr(string cookie = null)
        { while (_isstop)
            {
                Thread.Sleep(500);
            }
            HttpRequest http = new HttpRequest();
            http.Cookies = new CookieDictionary();
            http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4157.0 Safari/537.36 Edg/85.0.531.1";
            string html = "";
            var temp = cookie.Split(';');
            foreach (var item in temp)
            {
                while (_isstop)
                {
                    Thread.Sleep(500);
                }
                var temp2 = item.Split('=');
                if (temp2.Count() > 1)
                {
                    http.Cookies.Add(temp2[0], temp2[1]);
                }
            }
            while (_isstop)
            {
                Thread.Sleep(500);
            }
            html = http.Get("https://m.facebook.com/composer/ocelot/async_loader/?publisher=feed").ToString();
            string name = Regex.Match(html, "\">(.*?)u003C\\/div").Groups[1].Value;
            this.Invoke((MethodInvoker)delegate ()
            {
                while (_isstop)
                {
                    Thread.Sleep(500);
                }
                label3.Text = "Đang Check Live Cookie ";
            });
            string lsd2 = Regex.Match(html, @"EAAAAZ(.*?)\"",").Groups[1].Value;
            if (lsd2 != "") 
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    while (_isstop)
                    {
                        Thread.Sleep(500);
                    }
                    label3.Text = "Cookie Live Check Tương Tác";
                });
                return  html = http.Get("https://graph.facebook.com/me/friends?access_token=EAAAAZ" + lsd2.Replace("\\", "") + "&limit=5000").ToString();
            
            } else
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    while (_isstop)
                    {
                        Thread.Sleep(500);
                    }
                    label3.Text =  "Cookie die";
                });
                return "ERROR";

            }
        }
        private void check(string friend = null)
        {
            int tt = 0;

            try
            {
                while (_isstop)
                {
                    Thread.Sleep(500);
                }
                var datax = reac.Split('-'); while (_isstop)
                {
                    Thread.Sleep(500);
                }
                var datay = cmt.Split('-'); while (_isstop)
                {
                    Thread.Sleep(500);
                }
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var obj = jss.Deserialize<dynamic>(friend);
                this.Invoke((MethodInvoker)delegate ()
                {
                    dataGridView1.Rows.Clear();
                    while (_isstop)
                    {
                        Thread.Sleep(500);
                    }
                    label3.Text = "Đang Check Tương Tác";
                });
                for (int i = 0; i < obj["data"].Length; i++)
                {
                    while (_isstop)
                    {
                        Thread.Sleep(500);
                    }
                    int cx = 0;
                    int bl = 0;
                    string idx = obj["data"][i]["id"];
                    if (reac.Contains(idx))
                    {
                        while (_isstop)
                        {
                            Thread.Sleep(500);
                        }
                        foreach (string data in datax)
                        {
                            if(idx == data)
                            {
                                while (_isstop)
                                {
                                    Thread.Sleep(500);
                                }
                                cx++;
                            }
                        }
                    }
                    if (cmt.Contains(idx))
                    {
                        while (_isstop)
                        {
                            Thread.Sleep(500);
                        }
                        foreach (string data in datay)
                        {
                            if (idx == data)
                            {
                                bl++;
                            }
                        }
                    }
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        while (_isstop)
                        {
                            Thread.Sleep(500);
                        }
                        int c = dataGridView1.Rows.Add();
                        dataGridView1.Rows[c].Cells[0].Value = c;
                        dataGridView1.Rows[c].Cells[1].Value = obj["data"][i]["id"];
                        dataGridView1.Rows[c].Cells[2].Value = obj["data"][i]["name"];
                        dataGridView1.Rows[c].Cells[3].Value = cx;
                        dataGridView1.Rows[c].Cells[4].Value = bl;
                        dataGridView1.Rows[c].Cells[5].Value = "Check Hoàn Tất";
                    if (cx == 0 && bl == 0)
                        {
                            tt++;
                        }
                        label4.Text = "Số người Không tương tác : " + tt.ToString()+"/"+ dataGridView1.RowCount.ToString();
                    });
                    Thread.Sleep(1);
                }
            }
            catch
            {

            }
           

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Nhập Cookie Đã Rồi Tính");
                return;
            }
            textBox1.Enabled = false;
            button1.Enabled = false;
            Thread test = new Thread(() =>
            {
                while (_isstop)
                {
                    Thread.Sleep(500);
                }
                string cookie = textBox1.Text;
                string friend = getFr(cookie);
                if(friend == "ERROR")
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        textBox1.Enabled = true;
                        button1.Enabled = true;
                    });
                    return;
                }
                data(cookie);
                check(friend);
                this.Invoke((MethodInvoker)delegate ()
                {
                    while (_isstop)
                    {
                        Thread.Sleep(500);
                    }
                    label3.Text = "Quét Tương Tác Thành Công";
                    button2.Enabled = true;
                    button3.Enabled = true;

                });

            });
            test.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            Thread test = new Thread(() =>
            {
                while (_isstop)
                {
                    Thread.Sleep(500);
                }
                this.Invoke((MethodInvoker)delegate ()
                {
                    label3.Text = "Khởi Động Chrome";
                }); while (_isstop)
                {
                    Thread.Sleep(500);
                }
                string cookie = textBox1.Text;
                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--window-size=400,600");
                options.AddArgument("--disable-gpu");
                if (cbHide.Checked)
                {
                    options.AddArgument("--headless");
    
                }
                options.AddArgument("--disable-infobars");
                options.AddArgument("--disable-extensions");
                options.AddArguments("--disable-notifications");
                options.AddArguments("--enable-automation");
                options.AddArgument("--disable-blink-features=AutomationControlled");
                options.AddArguments("--disable-popup-blocking");
                ChromeDriver batchrome = new ChromeDriver(service, options); while (_isstop)
                {
                    Thread.Sleep(500);
                }
                batchrome.Url = "https://mbasic.facebook.com/"; while (_isstop)
                {
                    Thread.Sleep(500);
                }
                var temp = cookie.Split(';');
                string nn = "";
                foreach (var item in temp)
                {
                    while (_isstop)
                    {
                        Thread.Sleep(500);
                    }
                    if (item != null)
                    {
                        while (_isstop)
                        {
                            Thread.Sleep(500);
                        }
                        var temp2 = item.Split('=');
                        if (temp2.Count() > 1)
                        {
                            nn += "var d = new Date(); d.setTime(d.getTime() + (360*24*60*60*1000)); var expires = \"expires = \" + d.toGMTString(); document.cookie = \"" + temp2[0] + "=" + temp2[1] + ";\"+ expires + \";path=/\";";

                        }
                    }
                } while (_isstop)
                {
                    Thread.Sleep(500);
                }
                batchrome.ExecuteScript(nn);
                batchrome.Navigate().Refresh();
                batchrome.Url = "https://mbasic.facebook.com/"; while (_isstop)
                {
                    Thread.Sleep(500);
                }
                if (batchrome.PageSource.Contains("Đăng Nhập"))
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        label3.Text = "Cookie Die";
                    }); return;
                }
                this.Invoke((MethodInvoker)delegate ()
                {
                    label3.Text = "Cookie Live Bắt Đầu Xóa Bạn Bè";
                });
                int o = dataGridView1.RowCount;
                for (int i = o - 1; i > 0; i--)
                {
                    while (_isstop)
                    {
                        Thread.Sleep(500);
                    }
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value) < 1 && Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value) < 1)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            label3.Text = "Đang Xóa Bạn Bè "+ dataGridView1.Rows[i].Cells[1].Value;
                        }); while (_isstop)
                        {
                            Thread.Sleep(500);
                        }
                        batchrome.Url = "https://mbasic.facebook.com/removefriend.php?friend_id=" + dataGridView1.Rows[i].Cells[1].Value+ "&unref=profile_gear";
                        if(batchrome.PageSource.Contains("Xóa bạn bè"))
                        {
                            while (_isstop)
                            {
                                Thread.Sleep(500);
                            }
                            dataGridView1.Rows[i].Cells[5].Value = "Đang Xóa";
                            batchrome.FindElementByName("confirm").Click();
                            Thread.Sleep(2000);
                            if(batchrome.PageSource.Contains("Bạn không còn là bạn của"))
                            {
                                while (_isstop)
                                {
                                    Thread.Sleep(500);
                                }
                                this.Invoke((MethodInvoker)delegate ()
                                {
                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        label3.Text = "Xóa Bạn Bè Thành Công " + dataGridView1.Rows[i].Cells[1].Value;
                                    });
                                    DataGridViewRow dgvDelRow = dataGridView1.Rows[Convert.ToInt32(i)];
                                    dataGridView1.Rows.Remove(dgvDelRow);
                                });
                                Thread.Sleep(decimal.ToInt32(numericUpDown1.Value)*1000);
                            }
                            else
                            {
                                while (_isstop)
                                {
                                    Thread.Sleep(500);
                                }
                                this.Invoke((MethodInvoker)delegate ()
                                {
                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        label3.Text = "Xóa Bạn Bè không Thành Công " + dataGridView1.Rows[i].Cells[1].Value;
                                    });
                                    DataGridViewRow dgvDelRow = dataGridView1.Rows[Convert.ToInt32(i)];
                                    dataGridView1.Rows.Remove(dgvDelRow);
                                });

                            }
                            Thread.Sleep(5000);
                        }

                    }
                }
                this.Invoke((MethodInvoker)delegate ()
                {
                    while (_isstop)
                    {
                        Thread.Sleep(500);
                    }
                    button2.Enabled = true;
                    label3.Text = "Job Done! ";
                });
            });
            test.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_isstop){
                _isstop = false;
            return; }
            if (_isstop == false) _isstop = true;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);

        }
        public string GetHardDiskSerialNo()
        {
            ManagementClass mangnmt = new ManagementClass("Win32_LogicalDisk");
            ManagementObjectCollection mcol = mangnmt.GetInstances();
            string result = "";
            foreach (ManagementObject strt in mcol)
            {
                result += Convert.ToString(strt["VolumeSerialNumber"]);
            }
            return result;
        }
        public static string GetMD5Hash(string text)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] computedHash = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
                return new System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary(computedHash).ToString();
            }
        }
        string key;
        string printDate;
        private bool check()
        {
            //key = GetMD5Hash(GetMD5Hash(GetMD5Hash(GetMD5Hash(GetMD5Hash(GetMD5Hash(GetHardDiskSerialNo()))))));      
            //HttpRequest http = new HttpRequest();
            //http.Cookies = new CookieDictionary();
            //http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4157.0 Safari/537.36 Edg/85.0.531.1";
            //int html = Convert.ToInt32(http.Get("http://quyet-vimaru.me/get.php?id=" + key).ToString());
            //if(html == 0)
            //{
            //    this.Invoke((MethodInvoker)delegate ()
            //    {
            //        label6.Text = "OKE BẠN ƠIIIII";
            //        label5.Text = "HSD : 31/2/2021";
            //    });
            //    return true;
            //}
            //double timestamp = html;
            //// First make a System.DateTime equivalent to the UNIX Epoch.
            //System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);

            //// Add the number of seconds in UNIX timestamp to be converted.
            //dateTime = dateTime.AddSeconds(timestamp);

            //// The dateTime now contains the right date/time so to format the string,
            //// use the standard formatting methods of the DateTime object.
            //printDate = dateTime.ToShortDateString();
            //Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;  
            //if(unixTimestamp < html)
            //{
            //    this.Invoke((MethodInvoker)delegate ()
            //    {
            //        label6.Text = "OKE BẠN ƠIIIII";
            //        label5.Text = "HSD : " + printDate;
            //    });
            //    return true;
            //}
            return true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread x = new Thread(() => { 
            if (check())
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        panel1.Hide();
                        pictureBox1.Hide();
                        textBox1.Enabled = true;
                        button1.Enabled = true;
                    });

            }
            else
            {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        this.pictureBox1.Image = global::locfriend.Properties.Resources.bye;
                        label5.Text = "ĐÉO OKE BẠN ƠIIIII";
                        label6.Text = "                  ĐÉO OKE BẠN ƠIIIII";
                        MessageBox.Show("Mã máy của bạn đã hết hạn hoặc chưa đăng kí");
                        Clipboard.SetText(key);
                        MessageBox.Show("Đã Coppy Key Vui Lòng Liên Hệ Admin Để Được Kích Hoạt");
                        Process.Start("https://www.facebook.com/quyetpaylak1/");
                        Environment.Exit(0);
                    });
                    return;
            }
            });
            x.Start();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.facebook.com/quyetpaylak1/");

        }     

        private void label5_Click(object sender, EventArgs e)
        {
            Process.Start("http://quyet-vimaru.me/donate.php");
        }
    }
}
