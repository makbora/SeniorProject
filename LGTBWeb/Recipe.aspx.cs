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
            DirDS.SelectCommand = "SELECT direction FROM instructions WHERE recid=" + recid;
            DirDS.DataBind();
            InstrList.DataBind();
            ReqDS.SelectCommand = "SELECT amount,measurement,name,price FROM requirements,ingredients where recid=" + recid + " AND requirements.ingid=ingredients.ingid";
            ReqDS.DataBind();
        }
    }
}