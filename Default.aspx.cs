using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DVR;
namespace DVR
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //fileRead "C:\\wwwroot\\DVR\\Text\\DVR.txt"
            //fileWrite "C:\\wwwroot\\DVR\\Text\\DVROutput.txt"
            DVRSearch test = new DVRSearch();
            test.getInput("C:\\wwwroot\\DVR\\Text\\DVR.txt", "C:\\wwwroot\\DVR\\Text\\DVROutput.txt");
            
        }
    }
}