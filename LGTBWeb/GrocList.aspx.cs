using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LGTBWeb
{
    public partial class GrocList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<IngItem> curlist = (List<IngItem>)Session["UserList"];
            if(curlist.Count==0)
            {
                EmptyLabel.Text = "is empty";
                EmptyLabel.Visible = true;
            }
            else 
            {
                GroceryView.DataSource = curlist;
                GroceryView.DataBind();
                GroceryView.Visible = true;
                //int price = 0;
                //for(int i = 0; i < curList.size() - 1; i++) {
                //  price += curList.get(i).price
                //}
            }
        }
    }
}