using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LGTBWeb
{
    public partial class Recipe : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int recid = (int)Session["RecID"];
            string rec = (string)Session["Rec"];
            Label1.Text = rec;
            DirDS.SelectCommand = "SELECT direction FROM instructions WHERE recid=" + recid;
            DirDS.DataBind();
            InstrList.DataBind();
            ReqDS.SelectCommand = "SELECT amount,measurement,name,price FROM requirements,ingredients where recid=" + recid + " AND requirements.ingid=ingredients.ingid";
            ReqDS.DataBind();
        }

        protected void IngView_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableCellCollection cells = IngView.Rows[IngView.SelectedIndex].Cells;
            IngItem item = new IngItem();
            if(cells[0].Text=="&nbsp;")
            {
                item.Amount = " ";
            }
            else
            {
                item.Amount = cells[0].Text;
            }
            if (cells[1].Text == "&nbsp;")
            {
                item.Measurement = " ";
            }
            else
            {
                item.Measurement = cells[1].Text;
            }
            item.Ingredient = cells[2].Text;
            item.Price = cells[3].Text;
            List<IngItem> curlist = (List<IngItem>)Session["UserList"];
            /*foreach(IngItem ing in curlist)
            {
                if(ing.ingredient.Equals(item.ingredient))
                {
                    ing.amount
                }
            }*/
            curlist.Add(item);
        }
    }
}