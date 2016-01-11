using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class BlogEntries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int entryId = Convert.ToInt32(Page.RouteData.Values["EntryId"]);

            Classes.Blog b = new Classes.Blog();
            Tuple<int, DataTable> result = b.blogEntryInfo(entryId);

            int status = result.Item1;
            DataTable dt = result.Item2;

            if(status == 0)
            {
                Response.Redirect("~/Blog");
            }
            else
            {
                LabelDate.Text = Convert.ToDateTime(dt.Rows[0]["SubmitDate"].ToString()).ToShortDateString();

                LabelTitle.Text = dt.Rows[0]["Title"].ToString();
                LiteralBody.Text = dt.Rows[0]["Body"].ToString();
                Page.Title = "Blog : " + dt.Rows[0]["BrowserTitle"].ToString();
                status = 1;
            }
        }
    }
}