using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Blog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Classes.Blog b = new Classes.Blog();
            DataTable dt = b.blogEntries();

            RepeaterBlog.DataSource = dt;
            RepeaterBlog.DataBind();
        }
    }
}