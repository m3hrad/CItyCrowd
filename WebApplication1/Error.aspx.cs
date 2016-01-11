using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string errorType = "unknown";

            try
            {
                if (Page.RouteData.Values["Code"].ToString() != "") // redirect user to a page with item id
                {
                    errorType = Page.RouteData.Values["Code"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            Classes.Done d = new Classes.Done();

            switch (errorType.ToLower())
            {
                case "404":
                    {
                        Page.Title = "Error";
                        HiddenFieldTitle.Value = "Error";
                        HiddenFieldSmiley.Value = ":|";
                        HiddenFieldMessage.Value = "Page not found!";
                        HiddenFieldLinksNumber.Value = "4";

                        Tuple<string, string, string, string> result = d.doneItem("G", "");
                        HiddenFieldLink1Text.Value = result.Item1;
                        HiddenFieldLink1Url.Value = result.Item2;
                        HiddenFieldLink1Image.Value = result.Item3;
                        HiddenFieldLink1Color.Value = result.Item4;

                        result = d.doneItem("B", "");
                        HiddenFieldLink2Text.Value = result.Item1;
                        HiddenFieldLink2Url.Value = result.Item2;
                        HiddenFieldLink2Image.Value = result.Item3;
                        HiddenFieldLink2Color.Value = result.Item4;

                        result = d.doneItem("K", "");
                        HiddenFieldLink3Text.Value = result.Item1;
                        HiddenFieldLink3Url.Value = result.Item2;
                        HiddenFieldLink3Image.Value = result.Item3;
                        HiddenFieldLink3Color.Value = result.Item4;

                        result = d.doneItem("E", "");
                        HiddenFieldLink4Text.Value = result.Item1;
                        HiddenFieldLink4Url.Value = result.Item2;
                        HiddenFieldLink4Image.Value = result.Item3;
                        HiddenFieldLink4Color.Value = result.Item4;

                        break;
                    }
                case "eventnotfound":
                    {
                        Page.Title = "Error";
                        HiddenFieldTitle.Value = "Error";
                        HiddenFieldSmiley.Value = ":|";
                        HiddenFieldMessage.Value = "We were unable to find this event!";
                        HiddenFieldLinksNumber.Value = "4";

                        Tuple<string, string, string, string> result = d.doneItem("A", "");
                        HiddenFieldLink1Text.Value = result.Item1;
                        HiddenFieldLink1Url.Value = result.Item2;
                        HiddenFieldLink1Image.Value = result.Item3;
                        HiddenFieldLink1Color.Value = result.Item4;

                        result = d.doneItem("E", "");
                        HiddenFieldLink2Text.Value = result.Item1;
                        HiddenFieldLink2Url.Value = result.Item2;
                        HiddenFieldLink2Image.Value = result.Item3;
                        HiddenFieldLink2Color.Value = result.Item4;

                        result = d.doneItem("B", "");
                        HiddenFieldLink3Text.Value = result.Item1;
                        HiddenFieldLink3Url.Value = result.Item2;
                        HiddenFieldLink3Image.Value = result.Item3;
                        HiddenFieldLink3Color.Value = result.Item4;

                        result = d.doneItem("K", "");
                        HiddenFieldLink4Text.Value = result.Item1;
                        HiddenFieldLink4Url.Value = result.Item2;
                        HiddenFieldLink4Image.Value = result.Item3;
                        HiddenFieldLink4Color.Value = result.Item4;

                        break;
                    }
                case "usernoteventowner":
                    {
                        Page.Title = "Error";
                        HiddenFieldTitle.Value = "Error";
                        HiddenFieldSmiley.Value = ":|";
                        HiddenFieldMessage.Value = "You are not the owner of this event!";
                        HiddenFieldLinksNumber.Value = "4";

                        Tuple<string, string, string, string> result = d.doneItem("A", "");
                        HiddenFieldLink1Text.Value = result.Item1;
                        HiddenFieldLink1Url.Value = result.Item2;
                        HiddenFieldLink1Image.Value = result.Item3;
                        HiddenFieldLink1Color.Value = result.Item4;

                        result = d.doneItem("E", "");
                        HiddenFieldLink2Text.Value = result.Item1;
                        HiddenFieldLink2Url.Value = result.Item2;
                        HiddenFieldLink2Image.Value = result.Item3;
                        HiddenFieldLink2Color.Value = result.Item4;

                        result = d.doneItem("B", "");
                        HiddenFieldLink3Text.Value = result.Item1;
                        HiddenFieldLink3Url.Value = result.Item2;
                        HiddenFieldLink3Image.Value = result.Item3;
                        HiddenFieldLink3Color.Value = result.Item4;

                        result = d.doneItem("K", "");
                        HiddenFieldLink4Text.Value = result.Item1;
                        HiddenFieldLink4Url.Value = result.Item2;
                        HiddenFieldLink4Image.Value = result.Item3;
                        HiddenFieldLink4Color.Value = result.Item4;

                        break;
                    }
                case "prequestnotfound":
                    {
                        Page.Title = "Error";
                        HiddenFieldTitle.Value = "Error";
                        HiddenFieldSmiley.Value = ":|";
                        HiddenFieldMessage.Value = "Your verification code for recovering your password was not found in the system!";
                        HiddenFieldLinksNumber.Value = "2";

                        Tuple<string, string, string, string> result = d.doneItem("L", "");
                        HiddenFieldLink1Text.Value = result.Item1;
                        HiddenFieldLink1Url.Value = result.Item2;
                        HiddenFieldLink1Image.Value = result.Item3;
                        HiddenFieldLink1Color.Value = result.Item4;

                        result = d.doneItem("M", "");
                        HiddenFieldLink2Text.Value = result.Item1;
                        HiddenFieldLink2Url.Value = result.Item2;
                        HiddenFieldLink2Image.Value = result.Item3;
                        HiddenFieldLink2Color.Value = result.Item4;

                        break;
                    }
            }
        }
    }
}