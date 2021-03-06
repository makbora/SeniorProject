using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LGTBWeb
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack&&ViewState.Count!=0)
            {
                string v = (string)ViewState["My SQL"];
                string query = v;
                RecipesDS.SelectCommand = query;
            }   
        }

        protected void GenreSelect_Click(object sender, EventArgs e)
        {
            int[] genres = GenreBox.GetSelectedIndices();
            string query = "SELECT * FROM recipes ";
            Boolean all = false;
            if(genres.Length>0)
            {
                for (int i = 0; i < genres.Length; i++)
                {
                    if (i == 0)
                    {
                        if (genres[i]==0)
                        {
                            if (genres.Length != 1)
                            {
                                query = query+"where ";
                            }
                            all = true;
                        }
                        else
                        {
                            query = query + "where kind like '" + GenreBox.Items[genres[i]] + "%'";
                        }
                    }
                    else
                    {
                        if(all)
                        {
                            query = query + "kind like '" + GenreBox.Items[genres[i]] + "%'";
                            all = false;
                        }
                        else
                        {
                            query = query + " or kind like '" + GenreBox.Items[genres[i]] + "%'";
                        } 
                    }
                }
                query = query + " ORDER BY title";
                RecipesDS.SelectCommand = query;
                RecipesDS.DataBind();
                RecipeTable.DataBind();
                ViewState.Add("My SQL", query);
                RecipeTable.PageIndex = 0;
            }
        }

        protected void RecipeTable_SelectedIndexChanged(object sender, EventArgs e)
        { 
            TableCellCollection cells = RecipeTable.Rows[RecipeTable.SelectedIndex].Cells;
            string rec = cells[1].Text;
            int recid = Int32.Parse(cells[4].Text);
            Session["RecID"] = recid;
            Session["Rec"] = rec;
            Response.Redirect("Recipe.aspx");
        }
    }
}