using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Review : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check login
            int UserId = 0;
            if (Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(Session["UserId"]);
            }
            else
            {
                if (Request.Cookies["VC"] != null)
                {
                    string VC = Request.Cookies["VC"].Values["VC"];
                    Classes.LoginSession ls = new Classes.LoginSession();
                    UserId = ls.getUserId(VC);
                    if (UserId == 0) //if user not logged in redirect to login
                    {
                        Response.Redirect("~/Login/Review");
                    }
                    else
                    {
                        Session["UserId"] = UserId.ToString();
                    }
                }
                else
                {
                    Response.Redirect("~/Login/Review");
                }
            }

            //check user status
            string completionValue = Session["DoneCompletion"] as string; if (String.IsNullOrEmpty(completionValue))
            {
                Classes.UserInfo ui = new Classes.UserInfo();
                int userStatus = ui.getUserStatus(UserId);
                switch (userStatus)
                {
                    case 1:
                        Session["DoneCompletion"] = "1";
                        break;
                    case 0:
                    case 4:
                        Response.Redirect("~/Completion");
                        break;
                    case 2:
                        Response.Redirect("~/Error/UserDisabled");
                        break;
                    case 3:
                        Response.Redirect("~/Error/UserDeactivated");
                        break;
                }
            }

            if (!IsPostBack)
            {
                Classes.Reviews r = new Classes.Reviews();
                WebApplication1.Classes.Reviews.ReviewRequest myReviewRequest = r.reviewInfo(Convert.ToInt32(Session["UserId"]));

                //if (dt.Rows.Count == 0) //doesn't exist
                //{
                //    HiddenFieldStatus.Value = "0";
                //    HiddenFieldSmiley.Value = ":)";
                //    HiddenFieldMessage.Value = "You have no review request now!";
                //    HiddenFieldLinksNumber.Value = "4";

                //    Classes.Done d = new Classes.Done();
                //    Tuple<string, string, string, string> result2 = d.doneItem("A", "");
                //    HiddenFieldLink1Text.Value = result2.Item1;
                //    HiddenFieldLink1Url.Value = result2.Item2;
                //    HiddenFieldLink1Image.Value = result2.Item3;
                //    HiddenFieldLink1Color.Value = result2.Item4;

                //    result2 = d.doneItem("B", "");
                //    HiddenFieldLink2Text.Value = result2.Item1;
                //    HiddenFieldLink2Url.Value = result2.Item2;
                //    HiddenFieldLink2Image.Value = result2.Item3;
                //    HiddenFieldLink2Color.Value = result2.Item4;

                //    result2 = d.doneItem("C", "");
                //    HiddenFieldLink3Text.Value = result2.Item1;
                //    HiddenFieldLink3Url.Value = result2.Item2;
                //    HiddenFieldLink3Image.Value = result2.Item3;
                //    HiddenFieldLink3Color.Value = result2.Item4;

                //    result2 = d.doneItem("E", "");
                //    HiddenFieldLink4Text.Value = result2.Item1;
                //    HiddenFieldLink4Url.Value = result2.Item2;
                //    HiddenFieldLink4Image.Value = result2.Item3;
                //    HiddenFieldLink4Color.Value = result2.Item4;
                //}
                //else //review request exists
                //{
                //    HiddenFieldReviewRequestId.Value = myReviewRequest.ReviewRequestId.ToString();
                //    //LabelEventName.Text = dt.Rows[0]["EventName"].ToString();
                //    //HiddenFieldTypeId.Value = dt.Rows[0]["TypeId"].ToString();
                //    //HiddenFieldCoverId.Value = dt.Rows[0]["CoverId"].ToString();
                //    //HyperLinkUser.Text = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
                //    //HyperLinkUser.NavigateUrl = "~/Profile/" + dt.Rows[0]["UserId"].ToString();

                //    //// Show profile's photo
                //    //if (Convert.ToBoolean(dt.Rows[0]["HasPhoto"].ToString()))
                //    //{
                //    //    HiddenFieldUserPhotoUrl.Value = "Files/ProfilesPhotos/" + dt.Rows[0]["UserId"].ToString() + "-100.jpg";
                //    //}
                //    //else
                //    //{
                //    //    HiddenFieldUserPhotoUrl.Value = "Images/nophoto.png";
                //    //}
                //}
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            Classes.Reviews r = new Classes.Reviews();
            int status = r.reviewAdd(Convert.ToInt32(Session["UserId"]), Convert.ToInt32(HiddenFieldReviewRequestId.Value), TextBoxComment.Text, Convert.ToInt16(HiddenFieldRate.Value));

            if (status == -1)
            {
                //not found
                Response.Redirect("~/Error/NotFound");
            }
            else
            {
                Response.Redirect("~/Review");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Classes.Reviews r = new Classes.Reviews();
            r.reviewCancel(Convert.ToInt32(Session["UserId"]), Convert.ToInt64(HiddenFieldReviewRequestId.Value));

            Response.Redirect("~/Review");
        }
    }
}