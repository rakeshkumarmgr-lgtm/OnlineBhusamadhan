using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.LandDispute.Entry
{
    public partial class AddMettingApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool CheckImage(object url)
        {
            if (url.ToString() != "")
            {
                string p = (url.ToString()).Replace("~", "");
                url = "http://localhost:8080" + p;
                try
                {
                    using (var webClient = new WebClient())
                    {
                        byte[] imageBytes = webClient.DownloadData(url.ToString());
                        string imreBase64Data = Convert.ToBase64String(imageBytes);
                        string imgDataURL = string.Format("data:Application/pdf;base64,{0}", imreBase64Data);

                    }
                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
            }


            else
            {
                return false;
            }


        }
    }
}