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
                double price = 0;
                foreach(IngItem item in curlist)
                {
                    price += Convert.ToDouble(item.Price);
                }
                TotalLabel.Text = "Total is: $" + price;
                TotalLabel.Visible = true;
            }
        }

        protected void GroceryView_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroceryView.DeleteRow(GroceryView.SelectedIndex);
            List<IngItem> curlist = (List<IngItem>)Session["UserList"];
            curlist.Remove(curlist[GroceryView.SelectedIndex]);
            GroceryView.DataBind();
            if (curlist.Count == 0)
            {
                EmptyLabel.Text = "is empty";
                EmptyLabel.Visible = true;
                TotalLabel.Visible = false;
            }
            double price = 0;
            foreach (IngItem item in curlist)
            {
                price += Convert.ToDouble(item.Price);
            }
            TotalLabel.Text = "Total is: $" + price;
        }

        protected void GroceryView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}