using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace crocusProject
{
    public partial class AdminMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string page = System.IO.Path.GetFileName(Request.Path);

            if (page == "AddProducts.aspx")
                lnkAddProduct.Attributes["class"] = "active";

            else if (page == "AddCategory.aspx")
                lnkAddCategory.Attributes["class"] = "active";

            else if (page == "AddSubCategory.aspx")
                lnkAddSubCategory.Attributes["class"] = "active";

            

            else if (page == "Dashboard.aspx")
                lnkdashboard.Attributes["class"] = "active";
            else if (page == "order.aspx")
                lnkAddOrder.Attributes["class"] = "active";

            else if (page == "login.aspx")
                lnkAddlogout.Attributes["class"] = "active";
        }
    }
}