using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Classes
{
    public class Done
    {
        public class DoneObject
        {
            public Boolean DoneStatus { get; private set; }
            public string Smiley { get; private set; }
            public string Title { get; private set; }
            public string Message { get; private set; }
            public Int16 Number { get; private set; }
            public string Item1Text { get; private set; }
            public string Item1Url { get; private set; }
            public string Item1Image { get; private set; }
            public string Item1Color { get; private set; }
            public string Item2Text { get; private set; }
            public string Item2Url { get; private set; }
            public string Item2Image { get; private set; }
            public string Item2Color { get; private set; }
            public string Item3Text { get; private set; }
            public string Item3Url { get; private set; }
            public string Item3Image { get; private set; }
            public string Item3Color { get; private set; }
            public string Item4Text { get; private set; }
            public string Item4Url { get; private set; }
            public string Item4Image { get; private set; }
            public string Item4Color { get; private set; }

            public DoneObject(Boolean donestatus, string smiley, string title, string message, Int16 number,
                string item1text, string item1url, string item1image, string item1color,
                string item2text, string item2url, string item2image, string item2color,
                string item3text, string item3url, string item3image, string item3color,
                string item4text, string item4url, string item4image, string item4color)
            {
                DoneStatus = donestatus;
                Smiley = smiley;
                Title = title;
                Message = message;
                Number = number;
                Item1Text = item1text;
                Item1Url = item1url;
                Item1Image = item1image;
                Item1Color = item1color;
                Item2Text = item2text;
                Item2Url = item2url;
                Item2Image = item2image;
                Item2Color = item2color;
                Item3Text = item3text;
                Item3Url = item3url;
                Item3Image = item3image;
                Item3Color = item3color;
                Item4Text = item4text;
                Item4Url = item4url;
                Item4Image = item4image;
                Item4Color = item4color;
            }
        }

        public DoneObject getDone(string type, int userId, Int64 eventId)
        {
            Boolean donestatus = false;
            string smiley = "";
            string title = "";
            string message = "";
            Int16 number = 0;
            string item1text = "";
            string item1url = "";
            string item1image = "";
            string item1color = "";
            string item2text = "";
            string item2url = "";
            string item2image = "";
            string item2color = "";
            string item3text = "";
            string item3url = "";
            string item3image = "";
            string item3color = "";
            string item4text = "";
            string item4url = "";
            string item4image = "";
            string item4color = "";

            switch (type.ToLower())
            {
                case "welcome":
                    {
                        title = "Welcome!";
                        smiley = ":)";
                        message = doneMessage("welcome");
                        number = 4;

                        Tuple<string, string, string, string> result = doneItem("A", "");
                        item1text = result.Item1;
                        item1url = result.Item2;
                        item1image = result.Item3;
                        item1color = result.Item4;

                        result = doneItem("K", "");
                        item2text = result.Item1;
                        item2url = result.Item2;
                        item2image = result.Item3;
                        item2color = result.Item4;

                        result = doneItem("B", "");
                        item3text = result.Item1;
                        item3url = result.Item2;
                        item3image = result.Item3;
                        item3color = result.Item4;

                        result = doneItem("F", "");
                        item4text = result.Item1;
                        item4url = result.Item2;
                        item4image = result.Item3;
                        item4color = result.Item4;

                        break;
                    }
                case "register":
                    {
                        title = "Congratulations!";
                        smiley = ":)";
                        message = "Congratulations! You are now a CityCrowder!";
                        number = 4;

                        Tuple<string, string, string, string> result = doneItem("B", "");
                        item1text = result.Item1;
                        item1url = result.Item2;
                        item1image = result.Item3;
                        item1color = result.Item4;

                        result = doneItem("A", "");
                        item2text = result.Item1;
                        item2url = result.Item2;
                        item2image = result.Item3;
                        item2color = result.Item4;

                        result = doneItem("F", "");
                        item3text = result.Item1;
                        item3url = result.Item2;
                        item3image = result.Item3;
                        item3color = result.Item4;

                        result = doneItem("C", "");
                        item4text = result.Item1;
                        item4url = result.Item2;
                        item4image = result.Item3;
                        item4color = result.Item4;

                        break;
                    }
                case "eventadded":
                    {
                        Classes.UserInfo ui = new Classes.UserInfo();
                        Int64 value = ui.getLastEvent(userId);

                        title = "Done!";
                        smiley = ":)";
                        message = doneMessage("eventadded");
                        number = 4;

                        Tuple<string, string, string, string> result = doneItem("H", value.ToString());
                        item1text = result.Item1;
                        item1url = result.Item2;
                        item1image = result.Item3;
                        item1color = result.Item4;

                        result = doneItem("A", "");
                        item2text = result.Item1;
                        item2url = result.Item2;
                        item2image = result.Item3;
                        item2color = result.Item4;

                        result = doneItem("B", "");
                        item3text = result.Item1;
                        item3url = result.Item2;
                        item3image = result.Item3;
                        item3color = result.Item4;

                        result = doneItem("J", value.ToString());
                        item4text = result.Item1;
                        item4url = result.Item2;
                        item4image = result.Item3;
                        item4color = result.Item4;

                        break;
                    }
                case "eventmodified":
                    {
                        Classes.UserInfo ui = new Classes.UserInfo();
                        Int64 value = ui.getLastEvent(userId);

                        title = "Done!";
                        smiley = ":)";
                        message = doneMessage("eventmodified");
                        number = 4;

                        Tuple<string, string, string, string> result = doneItem("A", "");
                        item1text = result.Item1;
                        item1url = result.Item2;
                        item1image = result.Item3;
                        item1color = result.Item4;

                        result = doneItem("H", "");
                        item2text = result.Item1;
                        item2url = result.Item2;
                        item2image = result.Item3;
                        item2color = result.Item4;

                        result = doneItem("E", "");
                        item3text = result.Item1;
                        item3url = result.Item2;
                        item3image = result.Item3;
                        item3color = result.Item4;

                        result = doneItem("J", value.ToString());
                        item4text = result.Item1;
                        item4url = result.Item2;
                        item4image = result.Item3;
                        item4color = result.Item4;

                        break;
                    }
                case "eventdeleted":
                    {
                        title = "Done!";
                        smiley = ":)";
                        message = doneMessage("eventdeleted");
                        number = 4;

                        Tuple<string, string, string, string> result = doneItem("A", "");
                        item1text = result.Item1;
                        item1url = result.Item2;
                        item1image = result.Item3;
                        item1color = result.Item4;

                        result = doneItem("B", "");
                        item2text = result.Item1;
                        item2url = result.Item2;
                        item2image = result.Item3;
                        item2color = result.Item4;

                        result = doneItem("E", "");
                        item3text = result.Item1;
                        item3url = result.Item2;
                        item3image = result.Item3;
                        item3color = result.Item4;

                        result = doneItem("F", "");
                        item4text = result.Item1;
                        item4url = result.Item2;
                        item4image = result.Item3;
                        item4color = result.Item4;

                        break;
                    }
                case "reportsent":
                    {
                        title = "Done!";
                        smiley = ":)";
                        message = doneMessage("reportsent");
                        number = 4;

                        Tuple<string, string, string, string> result = doneItem("A", "");
                        item1text = result.Item1;
                        item1url = result.Item2;
                        item1image = result.Item3;
                        item1color = result.Item4;

                        result = doneItem("B", "");
                        item2text = result.Item1;
                        item2url = result.Item2;
                        item2image = result.Item3;
                        item2color = result.Item4;

                        result = doneItem("E", "");
                        item3text = result.Item1;
                        item3url = result.Item2;
                        item3image = result.Item3;
                        item3color = result.Item4;

                        result = doneItem("F", "");
                        item4text = result.Item1;
                        item4url = result.Item2;
                        item4image = result.Item3;
                        item4color = result.Item4;

                        break;
                    }
                case "reviewsubmited":
                    {
                        title = "Done!";
                        smiley = ":)";
                        message = doneMessage("reviewsubmited");
                        number = 4;

                        Tuple<string, string, string, string> result = doneItem("A", "");
                        item1text = result.Item1;
                        item1url = result.Item2;
                        item1image = result.Item3;
                        item1color = result.Item4;

                        result = doneItem("B", "");
                        item2text = result.Item1;
                        item2url = result.Item2;
                        item2image = result.Item3;
                        item2color = result.Item4;

                        result = doneItem("C", "");
                        item3text = result.Item1;
                        item3url = result.Item2;
                        item3image = result.Item3;
                        item3color = result.Item4;

                        result = doneItem("F", "");
                        item4text = result.Item1;
                        item4url = result.Item2;
                        item4image = result.Item3;
                        item4color = result.Item4;

                        break;
                    }
                case "feedbacksubmited":
                    {
                        title = "Done!";
                        smiley = ":)";
                        message = doneMessage("feedbacksubmited");
                        number = 4;

                        Tuple<string, string, string, string> result = doneItem("A", "");
                        item1text = result.Item1;
                        item1url = result.Item2;
                        item1image = result.Item3;
                        item1color = result.Item4;

                        result = doneItem("C", "");
                        item2text = result.Item1;
                        item2url = result.Item2;
                        item2image = result.Item3;
                        item2color = result.Item4;

                        result = doneItem("B", "");
                        item3text = result.Item1;
                        item3url = result.Item2;
                        item3image = result.Item3;
                        item3color = result.Item4;

                        result = doneItem("K", "");
                        item4text = result.Item1;
                        item4url = result.Item2;
                        item4image = result.Item3;
                        item4color = result.Item4;

                        break;
                    }
                case "reviewsempty":
                    {
                        donestatus = true;
                        smiley = ":)";
                        title = "Reviews";
                        message = doneMessage(type);
                        number = 4;

                        Tuple<string, string, string, string> result1 = doneItem("A", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;

                        Tuple<string, string, string, string> result2 = doneItem("B", "");
                        item2text = result2.Item1;
                        item2url = result2.Item2;
                        item2image = result2.Item3;
                        item2color = result2.Item4;

                        Tuple<string, string, string, string> result3 = doneItem("C", "");
                        item3text = result3.Item1;
                        item3url = result3.Item2;
                        item3image = result3.Item3;
                        item3color = result3.Item4;

                        Tuple<string, string, string, string> result4 = doneItem("E", "");
                        item4text = result4.Item1;
                        item4url = result4.Item2;
                        item4image = result4.Item3;
                        item4color = result4.Item4;
                        break;
                    }
                case "explore":
                    {
                        donestatus = true;
                        smiley = ":)";
                        title = "Explore";
                        message = doneMessage(type);
                        number = 4;

                        Tuple<string, string, string, string> result1 = doneItem("A", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;

                        Tuple<string, string, string, string> result2 = doneItem("B", "");
                        item2text = result2.Item1;
                        item2url = result2.Item2;
                        item2image = result2.Item3;
                        item2color = result2.Item4;

                        Tuple<string, string, string, string> result3 = doneItem("C", "");
                        item3text = result3.Item1;
                        item3url = result3.Item2;
                        item3image = result3.Item3;
                        item3color = result3.Item4;

                        Tuple<string, string, string, string> result4 = doneItem("E", "");
                        item4text = result4.Item1;
                        item4url = result4.Item2;
                        item4image = result4.Item3;
                        item4color = result4.Item4;
                        break;
                    }
                case "nearby":
                    {
                        donestatus = true;
                        smiley = ":)";
                        title = "Nearby";
                        message = doneMessage(type);
                        number = 4;

                        Tuple<string, string, string, string> result1 = doneItem("A", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;

                        Tuple<string, string, string, string> result2 = doneItem("C", "");
                        item2text = result2.Item1;
                        item2url = result2.Item2;
                        item2image = result2.Item3;
                        item2color = result2.Item4;

                        Tuple<string, string, string, string> result3 = doneItem("E", "");
                        item3text = result3.Item1;
                        item3url = result3.Item2;
                        item3image = result3.Item3;
                        item3color = result3.Item4;

                        Tuple<string, string, string, string> result4 = doneItem("F", "");
                        item4text = result4.Item1;
                        item4url = result4.Item2;
                        item4image = result4.Item3;
                        item4color = result4.Item4;
                        break;
                    }
                case "requests0":
                case "requests2":
                    {
                        donestatus = true;
                        smiley = ":)";
                        title = "Requests";
                        message = doneMessage(type);
                        number = 1;

                        Tuple<string, string, string, string> result1 = doneItem("A", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;
                        break;
                    }
                case "eventscreated":
                    {
                        donestatus = true;
                        smiley = ":)";
                        title = "Events";
                        message = doneMessage(type);
                        number = 1;

                        Tuple<string, string, string, string> result1 = doneItem("A", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;
                        break;
                    }
                case "eventsaccepted":
                    {
                        donestatus = true;
                        smiley = ":)";
                        title = "Events";
                        message = doneMessage(type);
                        number = 1;

                        Tuple<string, string, string, string> result1 = doneItem("A", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;
                        break;
                    }
                case "eventsrequested":
                    {
                        donestatus = true;
                        smiley = ":)";
                        title = "Events";
                        message = doneMessage(type);
                        number = 1;

                        Tuple<string, string, string, string> result1 = doneItem("A", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;
                        break;
                    }
                case "eventsbookmarked":
                    {
                        donestatus = true;
                        smiley = ":)";
                        title = "Events";
                        message = doneMessage(type);
                        number = 1;

                        Tuple<string, string, string, string> result1 = doneItem("A", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;
                        break;
                    }
                case "emailverified":
                    {
                        donestatus = true;
                        smiley = ":)";
                        title = "Email Verified";
                        message = doneMessage(type);
                        number = 4;

                        Tuple<string, string, string, string> result1 = doneItem("A", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;

                        Tuple<string, string, string, string> result2 = doneItem("B", "");
                        item2text = result2.Item1;
                        item2url = result2.Item2;
                        item2image = result2.Item3;
                        item2color = result2.Item4;

                        Tuple<string, string, string, string> result3 = doneItem("C", "");
                        item3text = result3.Item1;
                        item3url = result3.Item2;
                        item3image = result3.Item3;
                        item3color = result3.Item4;

                        Tuple<string, string, string, string> result4 = doneItem("F", "");
                        item4text = result4.Item1;
                        item4url = result4.Item2;
                        item4image = result4.Item3;
                        item4color = result4.Item4;
                        break;
                    }
                    /////////////////////////////// ERROR
                case "404":
                    {
                        donestatus = true;
                        smiley = ":|";
                        title = "Error";
                        message = doneMessage(type);
                        number = 4;

                        Tuple<string, string, string, string> result1 = doneItem("G", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;

                        Tuple<string, string, string, string> result2 = doneItem("B", "");
                        item2text = result2.Item1;
                        item2url = result2.Item2;
                        item2image = result2.Item3;
                        item2color = result2.Item4;

                        Tuple<string, string, string, string> result3 = doneItem("K", "");
                        item3text = result3.Item1;
                        item3url = result3.Item2;
                        item3image = result3.Item3;
                        item3color = result3.Item4;

                        Tuple<string, string, string, string> result4 = doneItem("E", "");
                        item4text = result4.Item1;
                        item4url = result4.Item2;
                        item4image = result4.Item3;
                        item4color = result4.Item4;
                        break;
                    }
                case "emailverificationfailed":
                    {
                        donestatus = true;
                        smiley = ":)";
                        title = "Verification Failed";
                        message = doneMessage(type);
                        number = 4;

                        Tuple<string, string, string, string> result1 = doneItem("A", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;

                        Tuple<string, string, string, string> result2 = doneItem("B", "");
                        item2text = result2.Item1;
                        item2url = result2.Item2;
                        item2image = result2.Item3;
                        item2color = result2.Item4;

                        Tuple<string, string, string, string> result3 = doneItem("C", "");
                        item3text = result3.Item1;
                        item3url = result3.Item2;
                        item3image = result3.Item3;
                        item3color = result3.Item4;

                        Tuple<string, string, string, string> result4 = doneItem("F", "");
                        item4text = result4.Item1;
                        item4url = result4.Item2;
                        item4image = result4.Item3;
                        item4color = result4.Item4;
                        break;
                    }
                case "noprofile":
                    {
                        donestatus = true;
                        smiley = ":|";
                        title = "Error";
                        message = doneMessage(type);
                        number = 4;

                        Tuple<string, string, string, string> result1 = doneItem("G", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;

                        Tuple<string, string, string, string> result2 = doneItem("B", "");
                        item2text = result2.Item1;
                        item2url = result2.Item2;
                        item2image = result2.Item3;
                        item2color = result2.Item4;

                        Tuple<string, string, string, string> result3 = doneItem("K", "");
                        item3text = result3.Item1;
                        item3url = result3.Item2;
                        item3image = result3.Item3;
                        item3color = result3.Item4;

                        Tuple<string, string, string, string> result4 = doneItem("E", "");
                        item4text = result4.Item1;
                        item4url = result4.Item2;
                        item4image = result4.Item3;
                        item4color = result4.Item4;
                        break;
                    }
                case "restrictedprofile":
                    {
                        donestatus = true;
                        smiley = ":|";
                        title = "Error";
                        message = doneMessage(type);
                        number = 4;

                        Tuple<string, string, string, string> result1 = doneItem("A", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;

                        Tuple<string, string, string, string> result2 = doneItem("B", "");
                        item2text = result2.Item1;
                        item2url = result2.Item2;
                        item2image = result2.Item3;
                        item2color = result2.Item4;

                        Tuple<string, string, string, string> result3 = doneItem("K", "");
                        item3text = result3.Item1;
                        item3url = result3.Item2;
                        item3image = result3.Item3;
                        item3color = result3.Item4;

                        Tuple<string, string, string, string> result4 = doneItem("E", "");
                        item4text = result4.Item1;
                        item4url = result4.Item2;
                        item4image = result4.Item3;
                        item4color = result4.Item4;
                        break;
                    }
                case "eventnotfound":
                    {
                        donestatus = true;
                        smiley = ":|";
                        title = "Error";
                        message = doneMessage(type);
                        number = 4;

                        Tuple<string, string, string, string> result1 = doneItem("A", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;

                        Tuple<string, string, string, string> result2 = doneItem("E", "");
                        item2text = result2.Item1;
                        item2url = result2.Item2;
                        item2image = result2.Item3;
                        item2color = result2.Item4;

                        Tuple<string, string, string, string> result3 = doneItem("B", "");
                        item3text = result3.Item1;
                        item3url = result3.Item2;
                        item3image = result3.Item3;
                        item3color = result3.Item4;

                        Tuple<string, string, string, string> result4 = doneItem("K", "");
                        item4text = result4.Item1;
                        item4url = result4.Item2;
                        item4image = result4.Item3;
                        item4color = result4.Item4;
                        break;
                    }
                case "usernoteventowner":
                    {
                        donestatus = true;
                        smiley = ":|";
                        title = "Error";
                        message = doneMessage(type);
                        number = 4;

                        Tuple<string, string, string, string> result1 = doneItem("A", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;

                        Tuple<string, string, string, string> result2 = doneItem("E", "");
                        item2text = result2.Item1;
                        item2url = result2.Item2;
                        item2image = result2.Item3;
                        item2color = result2.Item4;

                        Tuple<string, string, string, string> result3 = doneItem("B", "");
                        item3text = result3.Item1;
                        item3url = result3.Item2;
                        item3image = result3.Item3;
                        item3color = result3.Item4;

                        Tuple<string, string, string, string> result4 = doneItem("K", "");
                        item4text = result4.Item1;
                        item4url = result4.Item2;
                        item4image = result4.Item3;
                        item4color = result4.Item4;
                        break;
                    }
                case "prequestnotfound":
                    {
                        donestatus = true;
                        smiley = ":|";
                        title = "Error";
                        message = doneMessage(type);
                        number = 2;

                        Tuple<string, string, string, string> result1 = doneItem("L", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;

                        Tuple<string, string, string, string> result2 = doneItem("M", "");
                        item2text = result2.Item1;
                        item2url = result2.Item2;
                        item2image = result2.Item3;
                        item2color = result2.Item4;
                        break;
                    }
                case "following":
                    {
                        donestatus = true;
                        smiley = ":)";
                        title = "Following";
                        message = doneMessage(type);
                        number = 4;

                        Tuple<string, string, string, string> result1 = doneItem("A", "");
                        item1text = result1.Item1;
                        item1url = result1.Item2;
                        item1image = result1.Item3;
                        item1color = result1.Item4;

                        Tuple<string, string, string, string> result2 = doneItem("B", "");
                        item2text = result2.Item1;
                        item2url = result2.Item2;
                        item2image = result2.Item3;
                        item2color = result2.Item4;

                        Tuple<string, string, string, string> result3 = doneItem("K", "");
                        item3text = result3.Item1;
                        item3url = result3.Item2;
                        item3image = result3.Item3;
                        item3color = result3.Item4;

                        Tuple<string, string, string, string> result4 = doneItem("C", "");
                        item4text = result4.Item1;
                        item4url = result4.Item2;
                        item4image = result4.Item3;
                        item4color = result4.Item4;
                        break;
                    }
            }

            DoneObject myDoneObject = new DoneObject(
                donestatus, smiley, title, message, number,
                item1text, item1url, item1image, item1color,
                item2text, item2url, item2image, item2color,
                item3text, item3url, item3image, item3color,
                item4text, item4url, item4image, item4color);

            return myDoneObject;
        }

        public Tuple<string, string, string, string> doneItem(string letter, string value)
        {
            string title = "";
            string link = "";
            string image = "";
            string color = "";

            switch (letter)
            {
                case "A":
                    {
                        title = "Add an event";
                        link = "Events/Add";
                        image = "Images/Menu/add32.png";
                        color = "d7432e";
                        break;
                    }
                case "B":
                    {
                        title = "See events in your city";
                        link = "Nearby";
                        image = "Images/Menu/nearby32.png";
                        color = "1dd2ff";
                        break;
                    }
                case "C":
                    {
                        title = "Invite your friends to the app";
                        link = "Invite";
                        image = "Images/Menu/invite32.png";
                        color = "ff1d2d";
                        break;
                    }
                case "D":
                    {
                        title = "Give feedback";
                        link = "Feedback";
                        image = "Images/Menu/feedback32.png";
                        color = "cccc00";
                        break;
                    }
                case "E":
                    {
                        title = "Go to your events";
                        link = "Events";
                        image = "Images/Menu/evvents32.png";
                        color = "65e510";
                        break;
                    }
                case "F":
                    {
                        title = "Go to your profile";
                        link = "Profile";
                        image = "Images/Icons/Profile-32.png";
                        color = "003300";
                        break;
                    }
                case "G":
                    {
                        title = "Report a bug";
                        link = "Report/Error";
                        image = "Images/Icons/Bug-32.png";
                        color = "666666";
                        break;
                    }
                case "H":
                    {
                        title = "Share your event in Facebook";
                        link = "https://www.facebook.com/sharer/sharer.php?u=http://citycrowd.cc/Events/" + value;
                        image = "Images/Icons/Facebook-32.png";
                        color = "3366ff";
                        break;
                    }
                case "I":
                    {
                        title = "Modify event";
                        link = "Events/Modify/" + value;
                        image = "Images/Icons/pen48.png";
                        color = "ff33cc";
                        break;
                    }
                case "J":
                    {
                        title = "Go to the event profile";
                        link = "Events/" + value;
                        image = "Images/Icons/Next-32.png";
                        color = "ff991d";
                        break;
                    }
                case "K":
                    {
                        title = "Explore events";
                        link = "Explore";
                        image = "Images/Menu/explore32.png";
                        color = "3333ff";
                        break;
                    }
                case "L":
                    {
                        title = "Go to login page";
                        link = "Login";
                        image = "Images/Icons/ic_action_accounts_white.png";
                        color = "d7432e";
                        break;
                    }
                case "M":
                    {
                        title = "Go to forgot password page";
                        link = "ForgotPassword/Request";
                        image = "Images/Icons/ic_action_secure.png";
                        color = "cccc00";
                        break;
                    }
            }

            return new Tuple<string, string, string, string>(title, link, image, color);
        }

        public string doneMessage(string mode)
        {
            string message = "";

            switch (mode.ToLower())
            {
                case "welcome":
                    {
                        string[] texts = {
                        "Hello there! Ready to discover or create events?",
                        "Welcome, Don’t forget to bring your friends along!",
                        "Hey! You look gorgeous today!",
                        "Hey! Lovely day, isn’t it?!",
                        "Welcome back! CityCrowders have been looking forward to see you again!",
                        "Hi there, ready for some CityCrowding?",
                        "Welcome! You would make more of CityCrowd if you invite your friends."};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "eventadded":
                    {
                        string[] texts = {
                        "Congratulations! You just created your event!",
                        "Awesome! you just created your event!",
                        "Bravo! You just added an awesome event!",
                        "Nice! Seems like you just added an interesting event!",
                        "Awesome! CityCrowders would join to your event soon!",
                        "You just made a new event! Don’t forget to check the requests page when there were some requests.",
                        "Great, you created an event. Wanna add more?!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "eventmodified":
                    {
                        string[] texts = {
                        "Well done! You just modified your event!",
                        "Yep! Your event got modified!",
                        "The event is now updated!",
                        "Cool! The event would be much more fun now!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "eventdeleted":
                    {
                        string[] texts = {
                        "Alright, you just deleted your event! Wanna add another one instead?",
                        "The event is deleted! What about creating a new one now?!",
                        "You just removed your event! Don’t forget you can always modify the events if something needs to be changed!",
                        "It’s a sad day! CityCrowders just lost one of their precious events!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "reportsent":
                    {
                        string[] texts = {
                        "Nice! We have successfully received your report!",
                        "Your report has been received! We will analyze it soon!",
                        "We received your report! Thanks for your time!",
                        "Thanks for letting us know about this matter! Your report would be checked ASAP!",
                        "Thanks for the report! CityCrowders appreciate your concern!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "reviewsubmited":
                    {
                        string[] texts = {
                        "Thank you! You just reviewed this CityCrowder!",
                        "CityCrowders appreciate your review!",
                        "Thanks for the review, you are an awesome CityCrowder!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "feedbacksubmited":
                    {
                        string[] texts = {
                        "Nice! We have successfully received your feedback!",
                        "Thanks for the feedback! It will help alot!",
                        "We received your feedback! Thank you!",
                        "Thank you! CityCrowd will improve with your feedback!",
                        "Thank you! CityCrowd would be even a better place having CityCrowders like you!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "reviewsempty":
                    {
                        string[] texts = {
                        "Currently you have no review requests!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "explore":
                    {
                        string[] texts = {
                        "Unfortunatly there is no event to show now!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "nearby":
                    {
                        string[] texts = {
                        "Unfortunatly there is no event in your city to show now!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "requests0":
                    {
                        string[] texts = {
                        "Well! You know what?! In order to have requests, you need to create an event first!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "requests2":
                    {
                        string[] texts = {
                        "No request yet. Don’t worry! I hope you get one really soon!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "eventscreated":
                    {
                        string[] texts = {
                        "Well! You don’t have any events now!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "eventsaccepted":
                    {
                        string[] texts = {
                        "Well! You haven’t been accepted in any upcoming events!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "eventsrequested":
                    {
                        string[] texts = {
                        "You haveen't requested to participate in any upcoming events!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "eventsbookmarked":
                    {
                        string[] texts = {
                        "You haven't bookmarked any events!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "emailverified":
                    {
                        string[] texts = {
                        "You have successfully verified your email address!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                    //////////////////////////////////// ERROR
                case "404":
                    {
                        string[] texts = {
                        "Page not found!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "noprofile":
                    {
                        string[] texts = {
                        "No profile found!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "restrictedprofile":
                    {
                        string[] texts = {
                        "This profile is restricted!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "eventnotfound":
                    {
                        string[] texts = {
                        "We were unable to find this event!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "usernoteventowner":
                    {
                        string[] texts = {
                        "You are not the owner of this event!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "prequestnotfound":
                    {
                        string[] texts = {
                        "Your verification code for recovering your password was not found in the system!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "emailverificationfailed":
                    {
                        string[] texts = {
                        "Something when wrong when you were verifying your email address!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
                case "following":
                    {
                        string[] texts = {
                        "Currently, there is no event from your followings to be shown!"};
                        Random rnd = new Random();
                        int number = rnd.Next(0, texts.Length);
                        message = texts[number];
                        break;
                    }
            }

            return message;
        }
    }
}