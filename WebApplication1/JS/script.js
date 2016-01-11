//general values
var UNIsvalidServer = false;
var nFooterShortClass = "nFooterShort40";
var screenShortParam = 35;
var isMenuEnabled = true;


//checking if scrollbar is visible
function scrollbarIsVisible(selector) {
    return $(selector).get(0).scrollHeight > $(selector).height();
}

function showTheMainContent() {
    $('.pageNormalContent').removeClass('invisible');
};

//convert em to px
function px(input) {
    var emSize = parseFloat($("body").css("font-size"));
    return (input * emSize);
}

//email validation
function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}
function validateName(name) {
    var re = /^[^±!@£$%^&*_+§¡€#¢§¶•ªº«\\/<>?:;|=.,]{1,20}$/;
    return re.test(name);
}

function validateUsernameServer(name) {
    if (name != "") {
        console.log("username :" + name);
        var finalResult;
        var chat = $.connection.chatHub;
        UNIsvalidServer = false;



        $.connection.hub.start().done(function () {
            chat.server.usernameValidator(name).done(function (result) {
                console.log("titr : " + result);
                if (result.toString() == true) {
                    console.log("titr true");
                    finalResult = false;
                    UNIsvalidServer = false;
                } else if (result == false) {
                    finalResult = true;
                    UNIsvalidServer = true;
                    console.log("titr false");
                }
            })
        })
        return finalResult;
    }
}

function validateUsername(name) {
    var re1 = /^[0-9]{3,16}$/;
    if (re1.test(name)) {
        return false;
    }
    var re = /^[a-z0-9_-]{3,16}$/;
    var result = re.test(name);
    return re.test(name);
}
//returns true if the words are the same
function SameWords(word1, word2) {
    return word1 == word2;
};
function validateTimeIsFuture(givenDate) {
    if (typeof givenDate !== 'undefined') {
        var x = new Date(givenDate);
        console.log("x" + x);
        var y = new Date();
        y.setDate(y.getDate() - 1);
        console.log("y" + y);
        if (+x >= +y) {
            console.log("right date");
            return true;
        }
        else {
            console.log("bad date");
            return false;
        }
    }
    else {
        console.log("wrong date type");
        return false;
    }
}
function validateHoursIsFuture(givenDate, givenHours) {
    if (typeof givenDate !== 'undefined' && validateTimeIsFuture(givenDate)) {

        var x = new Date(givenDate);
        //x.setHours();
        var hoursAndMins = givenHours.split(':');
        var hours = parseInt(hoursAndMins[0]) - 3;
        var mins = parseInt(hoursAndMins[1]) - 15;
        x.setHours(x.getHours() + hours);
        x.setMinutes(x.getMinutes() + mins);
        console.log("hours " + hours + " mins: " + mins);
        console.log("x" + x);

        var y = new Date();
        y.setDate(y.getDate());
        console.log("y" + y);
        if (+x >= +y) {
            console.log("right hours and mins");
            return true;
        }
        else {
            console.log("bad hours and mins");
            return false;
        }
    }
    else {
        console.log("wrong date type");
        return false;
    }
}

function validateAdult(givenDate, targetAge) {
    if (typeof givenDate !== 'undefined') {
        var x = new Date();
        console.log("x" + x);
        var y = new Date(givenDate);
        y.setDate(y.getDate() - 1);
        var originalYear = y.getFullYear() + targetAge;
        console.log(originalYear);
        y.setFullYear(originalYear);
        console.log("y" + y);
        if (+x >= +y) {
            console.log("right date");
            return true;
        }
        else {
            console.log("bad date");
            return false;
        }
    }
    else {
        console.log("wrong date type");
        return false;
    }
}

//popup menu function (logo, boxes class, closeByClick)
//'.nDotsLogo', '.nDotsBox', 
//call the function again to close the menu manually
function popUp(targetLogo, targetBoxes, closeByClick) {

    var dotsOpened = false;
    if (closeByClick) {
        var selector = targetBoxes + " div";
        $(targetBoxes).find('div').click(function () {
            $(targetBoxes).addClass("hidden");
            dotsOpened = false;
        });
    }
    $(targetBoxes).addClass("hidden");
    //dots function
    $(targetLogo).click(function () {
        event.stopPropagation();
        if (!dotsOpened) {
            $(targetBoxes).removeClass("hidden")
            dotsOpened = true;
        }
    });
    $(targetBoxes).click(function () {
        event.stopPropagation();
    });
    $('html').click(function () {
        $(targetBoxes).addClass("hidden");
        dotsOpened = false;
        $('.nDotBoxLinks').removeClass('hide');
        $('.nDotsBoxShare').addClass('hide');
    });
}

//optional selected tab by number
function tabbedContent(tabNumber) {
    //without preselected tab
    if (typeof tabNumber === 'undefined') {

    } else {
        $(".nTabs").removeClass("nExploreSelected");
        var selector = ".nTabs:eq(" + tabNumber + ")";
        console.log(selector);
        $(selector).addClass("nExploreSelected");
        $(".nTabContents").addClass("hidden");
        tabNumber++;
        var selector = ".nExploreContentHolder .nTabContents:nth-child(" + tabNumber + ")";
        $(selector).removeClass("hidden");
    }
    $(".nTabs").click(function () {
        if ($(this).hasClass("nExploreSelected")) {
        }
        else {
            $(".nTabs").removeClass("nExploreSelected");
            $(this).addClass("nExploreSelected");
            var n = $(this).attr("nTabNumber");
            $(".nTabContents").addClass("hidden");
            var selector = ".nExploreContentHolder .nTabContents:nth-child(" + n + ")";
            $(selector).removeClass("hidden");
        }
    })
}


/*time format: 1/30/2015 12:00:00 AM
    create message for remained time 
    id of the value, id of the target
    */
function convertDate(givenDate, targetDiv) {
    if (typeof givenDate == 'undefined') {
        console.log('empty date');
        return;
    }
    //setting the the remained time in days and hours and minutes
    var remainedMinutes = 0;
    var remainedHours = 0;
    var remainedDays = 0;
    var AMorPM = "unknown";
    //console.log($(givenDate).val());
    var str = $(givenDate).val();
    var startDateOriginalFormat = str.substring(0, str.indexOf(" "));
    var startHourOriginalFormat = str.substring(str.indexOf(" ") + 1, str.lastIndexOf(" "));
    var AMorPM = str.substring(str.lastIndexOf(" ") + 1);

    //converting the starting date to datestring
    fromDate = startDateOriginalFormat.split("/");
    fromHours = startHourOriginalFormat.split(":");
    if (AMorPM == "PM") {
        f = new Date(fromDate[2], fromDate[0] - 1, fromDate[1], fromHours[2] + 12, fromHours[1], fromDate[0], 0);
    } else if (AMorPM == "AM") {
        f = new Date(fromDate[2], fromDate[0] - 1, fromDate[1], fromHours[2], fromHours[1], fromDate[0], 0);
    } else {
        console.log("ERROR: couldn't read AM or PM!");
    }

    //testing the start time
    //console.log("****" + f.toLocaleDateString("en-US") + "****" + f.toLocaleTimeString("en-US"));
    //converting the start date datestring to miliseconds
    var startTime = f.getTime();
    //console.log("f: " + f.getTime());

    function updateRemainingTimeText() {
        var now = $.now();

        //subtracting now from start time
        var differenceInMS = Math.abs(startTime - now);

        //check if event is passed
        if (startTime > now) {
            $(targetDiv).html("This event is passed!");
            return;
        }
        //calculating difference in days, hours, and minutes
        var x = differenceInMS / 1000;
        var seconds = x % 60;
        x /= 60;
        var minutes = Math.floor(x % 60);
        x /= 60;
        var hours = Math.floor(x % 24);
        x /= 24;
        var days = Math.floor(x);
        //console.log("days:" + days + "hours:" + hours + "minutes:" + minutes);

        //translating difference time to text
        var onlyParameter = "",
            daysParameters = "",
            daysAndParameter = "",
            hoursParameters = "",
            hoursAndParameter = "",
            minutesParameter = "";

        // write only if it's only minutes remaining
        if (days == 0 && hours == 0) { onlyParameters = "only " };
        //determining day parameters, daysAndParameter
        if (hours != 0 || minutes != 0) {
            daysAndParameter = "and ";
        }
        if (days == 1) {
            daysParameters = "One day " + daysAndParameter;
        } else if (days > 1) {
            daysParameters = days + " days " + daysAndParameter;
        }

        //determining hoursAndParameter
        if (minutes != 0) {
            hoursAndParameter = "and ";
        }
        //determining hoursParameters
        if (hours == 1) {
            hoursParameters = "One hour " + hoursAndParameter;
        } else if (hours > 1) {
            hoursParameters = hours + " hours " + hoursAndParameter;
        }


        //determining minutsParameters
        //console.log("***********" + minutes);
        if (minutes == 1) {
            minutesParameter = "One minute";
        } else if (minutes > 1) {
            minutesParameter = minutes + " minutes";
        }
        remainedTimeMessage = onlyParameter + daysParameters + hoursParameters + minutesParameter + " later";
        $(targetDiv).html(remainedTimeMessage);
    };
    updateRemainingTimeText();
    //calculate difference time every 60 seconds
    setInterval(function () { updateRemainingTimeText() }, 60000);
}

function getRootUrl() {
    return window.location.origin ? window.location.origin + '/' : window.location.protocol + '/' + window.location.host + '/';
}
function addGoogleAnalytics() {
    //google analytics
    (function (i, s, o, g, r, a, m) {
        i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
            (i[r].q = i[r].q || []).push(arguments)
        }, i[r].l = 1 * new Date(); a = s.createElement(o),
        m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
    })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

    if ($('#HiddenFieldUserId').val() && $('#HiddenFieldUserId').val() !== "null" && $('#HiddenFieldUserId').val() !== "undefined") {
        ga('create', 'UA-65192021-1', { 'userId': $('#HiddenFieldUserId').val() });
        console.log("ga id added:" + $('#HiddenFieldUserId').val());
    } else {
        ga('create', 'UA-65192021-1', 'auto');
        console.log("ga no id added");
    }

    /*ga('create', 'UA-65192021-1', 'auto');
    ga('send', 'pageview');
    if ($('#HiddenFieldUserId').val() && $('#HiddenFieldUserId').val() !== "null" && $('#HiddenFieldUserId').val() !== "undefined") {
        console.log('ga caled')
        ga('set', '&uid', $('#HiddenFieldUserId').val()); // Set the user ID using signed-in user_id.
    }*/
}
//status: follow || unfollow
function setFollowBtnStyle(status) {
    if (status == "follow") {
        $('.nProfileFollowButton').addClass('nGreenBG');
    } else if (status == "unfollow") {
        $('.nProfileFollowButton').removeClass('nGreenBG');
    }
}
function menu() {
    addGoogleAnalytics();
    //choosing the active menu
    if ($('.nPageTitle').html() && $('.nPageTitle').html() !== "null" && $('.nPageTitle').html() !== "undefined") {
        var pageTitle = $('.nPageTitle').html().toLowerCase()
    };

    var selector = "div[data-menuButton='" + pageTitle + "']"
    $(selector).addClass("menuSelected");
    console.log($(selector).html());

    //keeping the status of the side of visible notifications
    var messageFront = true;
    var notificationFront = true;
    var requestFront = true;
    var menuFront = true;
    var messageLatest = '0';
    var notificationLatest = '0';
    var requestLatest = '0';
    var menuLatest = '0';

    //setting layout
    //HiddenFieldLoginStatus, 0 = guest, 1 = user
    //guest
    if ($('#HiddenFieldLoginStatus').val() == "0") {
        $('.nMenuContainerUser').addClass('invisible');
        $('.nMenuContainerGuest').removeClass('invisible');
        $('.nMenuButtonNotification').addClass('hidden');

    }
        //user
    else if ($('#HiddenFieldLoginStatus').val() == "1") {
        $('.nMenuContainerUser').removeClass('invisible');
        $('.nMenuContainerGuest').addClass('invisible');

        //try to get the cookie
        try {
            var vc = $.cookie("VC");
            if (vc.length > 0) {
                vc = vc.substring(3);
            }
        }
        catch (err) {
            console.log("couldn't read vc");
        }
        var chat = $.connection.chatHub;

        // messages, notifications, requests 
        chat.client.updateNotificationNumbers = function (messages, notifications, requests) {
            // Html encode display name and message.
            console.log("received");
            console.log(messages + " " + notifications + " " + requests);

            //the notification number already exists
            if (messages == messageLatest) {
                //do nothing
            } else {
                $('.nMenuNotificationMessages').toggleClass('flipped');
                if (!messageFront) {
                    $('.nMenuNotificationMessages .front').text(messages);
                } else {
                    $('.nMenuNotificationMessages .back').text(messages);
                }
                messageFront = !messageFront;
                messageLatest = messages;
                if (!messageFront) {
                    $('.nMenuNotificationMessages figure').css('-ms-backface-visibility', 'visible');
                }
                else {
                    $('.nMenuNotificationMessages figure').css('-ms-backface-visibility', 'hidden');
                }

            }

            if (notifications == notificationLatest) {
                //do nothing
            } else {
                if (!notificationFront) {
                    $('.nMenuNotificationNotifications .front').text(notifications);
                } else {
                    $('.nMenuNotificationNotifications .back').text(notifications);
                }
                notificationFront = !notificationFront;
                notificationLatest = notifications;
                if (!notificationFront) {
                    $('.nMenuNotificationNotifications figure').css('-ms-backface-visibility', 'visible');
                }
                else {
                    $('.nMenuNotificationNotifications figure').css('-ms-backface-visibility', 'hidden');
                }
                $('.nMenuNotificationNotifications').toggleClass('flipped');
            }

            if (requests == requestLatest) {
                //do nothing
            } else {
                if (!requestFront) {
                    $('.nMenuNotificationRequests  .front').text(requests);
                } else {
                    $('.nMenuNotificationRequests  .back').text(requests);
                }
                requestFront = !requestFront;
                requestLatest = requests;
                if (!requestFront) {
                    $('.nMenuNotificationRequests  figure').css('-ms-backface-visibility', 'visible');
                }
                else {
                    $('.nMenuNotificationRequests  figure').css('-ms-backface-visibility', 'hidden');
                }
                $('.nMenuNotificationRequests ').toggleClass('flipped');
            }
            var total = parseInt(messages) + parseInt(notifications) + parseInt(requests);
            //setting the page title

            if (total != 0) {
                setTimeout(function () {
                    document.title = "(" + total + ") " + $('.nPageTitle').html();
                }, 1000);

            }
            console.log('total :' + total);
            if (total.toString() == menuLatest.toString()) {
                //do nothing
            } else {
                if (!menuFront) {
                    console.log('11111111 ' + total.toString());
                    $('.nMenuButtonNotification .front').text(total.toString());
                } else {
                    console.log('2222222');
                    $('.nMenuButtonNotification .back').text(total.toString());
                }
                menuFront = !menuFront;
                $('.nMenuButtonNotification ').toggleClass('flipped');
                if (!menuFront) {
                    $('.nMenuButtonNotification figure').css('-ms-backface-visibility', 'visible');
                }
                else {
                    $('.nMenuButtonNotification figure').css('-ms-backface-visibility', 'hidden');
                }
                menuLatest = total.toString();
            }

            if (messages != '0') {
                $('.nMenuNotificationMessages').removeClass('hidden');
            } else if (messages == '0') {
                $('.nMenuNotificationMessages').addClass('hidden');
            }
            if (notifications != '0') {
                $('.nMenuNotificationNotifications').removeClass('hidden');
            } else if (notifications == '0') {
                $('.nMenuNotificationNotifications').addClass('hidden');
            }
            if (requests != '0') {
                $('.nMenuNotificationRequests').removeClass('hidden');
            } else if (requests == '0') {
                $('.nMenuNotificationRequests').addClass('hidden');
            }
            console.log("402" + totalNotifications);
            if (total != 0) {
                $('.nMenuButtonNotification').removeClass('hidden');
            } else if (total == 0) {
                $('.nMenuButtonNotification').addClass('hidden');
            }

        };

        chat.client.updateMessage = function () {
            console.log("update message receieved");
        }

        // Create a function that the hub can call to broadcast messages.
        chat.client.broadcastMessage = function (name, message) {
            // Html encode display name and message.
            var encodedName = $('<div />').text(name).html();
            var encodedMsg = $('<div />').text(message).html();
            // Add the message to the page.
            $('#discussion').append('<li><strong>' + encodedName
                + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
        };
        chat.client.printInfo = function (message) {

            //$('.nPageTitle').html(message);
        };
        chat.client.checkMehrad = function (message) {
            //$('.nPageTitle').html(message);
        }

        // Start the connection.
        $.connection.hub.start().done(function () {
            $('body').click(function () {
                // Call the Send method on the hub.
                //chat.server.hop($('#displayname').val(), $('#message').val());
                chat.server.gimmeTheInfo();
                // Clear text box and reset focus for next comment.
                $('#message').val('').focus();
            });
            chat.server.setId(vc);
            //chat.server.checkGroup();
        });
    }

    //setting notification Numbers
    $('.nMenuNotificationMessages .front').html($('#HiddenFieldMessages').val());
    $('.nMenuNotificationNotifications .front').html($('#HiddenFieldNotifications').val());
    $('.nMenuNotificationRequests .front').html($('#HiddenFieldRequests').val());
    messageLatest = $('#HiddenFieldMessages').val();
    notificationLatest = $('#HiddenFieldNotifications').val();
    requestLatest = $('#HiddenFieldRequests').val();
    var messages = parseInt($('#HiddenFieldMessages').val());
    var notifications = parseInt($('#HiddenFieldNotifications').val());
    var requests = parseInt($('#HiddenFieldRequests').val());
    if (messages == 0) {
        $('.nMenuNotificationMessages').addClass('hidden');
    }
    if (notifications == 0) {
        $('.nMenuNotificationNotifications').addClass('hidden');
    }
    if (requests == 0) {
        $('.nMenuNotificationRequests').addClass('hidden');
    }
    var totalNotifications = messages + notifications + requests;
    $('.nMenuButtonNotification .front').html(totalNotifications);
    console.log("467" + totalNotifications);
    if (totalNotifications == 0) {
        $('.nMenuButtonNotification').addClass('hidden');
    } else if (totalNotifications != 0 && $('#HiddenFieldLoginStatus').val() == "1") {
        console.log("this thing works");
        $('.nMenuButtonNotification').removeClass('hidden');
        setTimeout(function () {
            document.title = "(" + totalNotifications + ") " + $('.nPageTitle').html();
        }, 1000);
    }
    $(".nMenuLoginBut").parent().click(function () {
        var url = getRootUrl() + "Login";
        window.location.href = url;
    })
    $(".nMenuRegisterBut").parent().click(function () {
        var url = getRootUrl() + "Register";
        window.location.href = url;
    })
    $(".nMenuAdd").click(function () {
        var url = getRootUrl() + "Events/Add";
        window.location.href = url;
    })
    $(".nEmailBut").parent().click(function () {
        var url = getRootUrl() + "Messages";
        window.location.href = url;
    })
    $(".nNewEventBut").parent().click(function () {
        var url = getRootUrl() + "Notifications";
        window.location.href = url;
    })
    $(".nPersonBut").parent().click(function () {
        var url = getRootUrl() + "Requests";
        window.location.href = url;
    })
    $(".nExploreBut").parent().click(function () {
        var url = getRootUrl() + "Explore";
        window.location.href = url;
    })
    $(".nFeedBut").parent().click(function () {
        var url = getRootUrl() + "Nearby";
        window.location.href = url;
    })
    $(".nAboutBut").parent().click(function () {
        var url = getRootUrl() + "About";
        window.location.href = url;
    })
    $(".nFollowingBut").parent().click(function () {
        var url = getRootUrl() + "Following";
        window.location.href = url;
    })
    $(".nEventsBut").parent().click(function () {
        var url = getRootUrl() + "Events";
        window.location.href = url;
    })
    $(".nReviewBut").parent().click(function () {
        var url = getRootUrl() + "Review";
        window.location.href = url;
    })
    $(".nSearchBut").parent().click(function () {
        var url = getRootUrl() + "Search";
        window.location.href = url;
    })
    $(".nCalendarBut").parent().click(function () {
        var url = getRootUrl() + "Calendar";
        window.location.href = url;
    })
    $(".nSettingsBut").parent().click(function () {
        var url = getRootUrl() + "Settings";
        window.location.href = url;
    })
    $(".nBlogBut").parent().click(function () {
        var url = getRootUrl() + "Blog";
        window.location.href = url;
    })
    $(".nFeedbackBut").parent().click(function () {
        var url = getRootUrl() + "Feedback";
        window.location.href = url;
    })
    $(".nInviteBut").parent().click(function () {
        var url = getRootUrl() + "Invite";
        window.location.href = url;
    })

    //logout
    $(".nLogoutInsidePanel").click(function () {
        var url = getRootUrl() + "Logout";
        window.location.href = url;
    })

    var photoUrl = $('#HiddenFieldPhotoUrl').val();
    photoUrl = getRootUrl() + photoUrl;
    $('.nProfilePicture').css('background-image', 'url(' + photoUrl + ')');

    var profileId = $('#HiddenFieldUserId').val();
    $(".nPanelBox1LoggedIn").click(function () {
        var profileIDlink = getRootUrl() + "profile/" + profileId;
        //console.log("profileIDlink" + profileIDlink);
        window.location.href = profileIDlink;
    });
}

function introductionPage() {
    var currentPage = 1;
    $('.nProfileNextButton').click(function () {
        currentPage++;
        if (currentPage == 6) {
            var url = getRootUrl() + "Done/Register";
            location.href = getRootUrl() + "Done/Register";
        } else {
            $('.nIntroductionPage').addClass('invisible');
            console.log("selector: " + '.nIntroductionPage [data-page=' + currentPage + ']');
            $('.nIntroductionPage[data-page=' + currentPage + ']').removeClass('invisible');
        }
    })
    $('.nNotButton').click(function () {
        var url = getRootUrl() + "Done/Register";
        location.href = getRootUrl() + "Done/Register";
    })

}

function fBFollowPage() {
    var isFollow = true;
    setFollowBtnStyle("follow");
    //console.log("userid , asfdsaf sf " + userId + " " +profileId);
    $('.nMessagesPicture').each(function () {
        //var profileId = $(this).find('.nEVX').attr('data-id');
        console.log("oopsi " + $(this).find("input").val().substr(2));
        var bgString = "url('" + getRootUrl() + $(this).find("input").val().substr(2) + "')";
        $(this).css('background-image', bgString);
    })

    var chat = $.connection.chatHub;
    var userId = parseInt($('#HiddenFieldUserId').val());

    $.connection.hub.start().done(function () {
        $(".nProfileFollowButton").click(function () {
            var profileId = $(this).parent().find(".nEVX").attr("data-id");
            chat.server.follow(userId, profileId).done(function (result) {
                console.log('result ' + result);
                if (result == 1) {
                    message = "You followed this user";
                    smilee = ":)";
                    $('.nProfileFollowButton').text('UNFOLLOW');
                    //set follow button style
                    setFollowBtnStyle("unfollow");
                }
                else if (result == 2) {
                    message = "You unfollowed this user";
                    smilee = ":(";
                    $('.nProfileFollowButton').text('FOLLOW');
                    //set follow button style
                    setFollowBtnStyle("follow");
                }



            });
        })
    });


}

function reportPage() {
    nFooterShortClass = "nFooterShort20";
    screenShortParam = 18; //em
    $(".nFooterButton").click(function () {
        $("#ContentPlaceHolder1_ButtonSubmit").trigger("click");
    });
}
function requestsShowPage() {
    var chat = $.connection.chatHub;
    var userId = parseInt($('#HiddenFieldUserId').val());
    //~/Requests/{EventId}/{RequestId}/{ActionCode}
    $.connection.hub.start().done(function () {

        $('.nRequestShowActionOk').click(function () {
            //requestResponse(int userId, Int64 requestId , bool response)
            var requestId = $(this).parent().find('.nRequestShowRequestId').text();
            chat.server.requestResponse(userId, requestId, true).done(function (result) {
                console.log('succesfully accepted');
            })
            /*var url = getRootUrl() + 'Requests/' + $(this).parent().find('.nRequestShowEventId').text() + '/' + $(this).parent().find('.nRequestShowRequestId').text() + '/1';
            console.log(url);
            window.location.href = url; */
            $(this).parent().parent().parent().hide('slow');
        });
        $('.nRequestShowActionNot').click(function () {
            var requestId = $(this).parent().find('.nRequestShowRequestId').text();
            chat.server.requestResponse(userId, requestId, false).done(function (result) {
                console.log('succesfully denied');
            })
            $(this).parent().parent().parent().hide('slow');
            /*var url = getRootUrl() + 'Requests/' + $(this).parent().find('.nRequestShowEventId').text() + '/' + $(this).parent().find('.nRequestShowRequestId').text() + '/2';
            console.log(url);
            window.location.href = url;*/
        });
    })
    //setting pictures of each requets
    $('.fullNotificationContainer').each(function () {
        //Images/Icons/notifications/{TypeId}-24.png
        var imageUrl = $(this).find('.nNotificationImageUrl').text();
        imageUrl = imageUrl.substr(2);
        console.log(imageUrl);
        var bg = "url('" + getRootUrl() + imageUrl + "')";
        $(this).find('.nMessagesPicture').css('background-image', bg);
    });
}

function setDoneReceivedValues(result) {
    //DoneStatus: true
    //Item1Color: "d7432e
    //"Item1Image: "Images/Icons/plus48.png"
    //Item1Text: "Add an event"
    //Item1Url: "Events/Add"
    //Item2Color: "1dd2ff"
    //Item2Image: "Images/Icons/pin48.png"
    //Item2Text: "See events in your city"
    //Item2Url: "Nearby/City"
    //Item3Color: "ff1d2d"
    //Item3Image: "Images/Icons/addmessage48.png"
    //Item3Text: "Invite your friends to the app"
    //Item3Url: "Invite"Item4
    //Item4Color: ""
    //Item4Image: ""
    //Item4Text: ""
    //Item4Url: ""
    //Message: ""
    //Number: 4
    //Smiley: ":)"
    $('.pageErrorContent').removeClass('invisible');
    $('.pageNormalContent').addClass('invisible');
    $('.nDoneRemovable').addClass('invisible');

    if (result.Title.length > 0) {
        $('.nPageTitle').html(result.Title);
        document.title = result.Title;
    };
    console.log("result.Message " + result.Message);
    $('.nDoneSmiley').html(result.Smiley);
    $('.nDoneMessage').html(result.Message);
    $('.nDoneSmiley').removeClass("invisible");
    $('.nDoneMessage').removeClass("invisible");

    //$('#ContentPlaceHolder1_HiddenFieldLinksNumber').val(4);
    if (result.Number > 0) {
        console.log('affrimitve');
        $('.nDoneInsideImage4').css('background-image', 'url(' + getRootUrl() + result.Item1Image + ')');
        $('.nDoneText4').html(result.Item1Text);
        $('.nDoneImage4').css('background-color', '#' + result.Item1Color);
        $('.nDoneButton4').removeClass('invisible');
    }
    if (result.Number > 1) {
        $('.nDoneInsideImage3').css('background-image', 'url(' + getRootUrl() + result.Item2Image + ')');
        $('.nDoneText3').html(result.Item2Text);
        $('.nDoneImage3').css('background-color', '#' + result.Item2Color);
        $('.nDoneButton3').removeClass('invisible');
    }
    if (result.Number > 2) {
        $('.nDoneInsideImage2').css('background-image', 'url(' + getRootUrl() + result.Item3Image + ')');
        $('.nDoneText2').html(result.Item3Text);
        $('.nDoneImage2').css('background-color', '#' + result.Item3Color);
        $('.nDoneButton2').removeClass('invisible');
    }
    if (result.Number > 3) {
        $('.nDoneInsideImage1').css('background-image', 'url(' + getRootUrl() + result.Item4Image + ')');
        $('.nDoneText1').html(result.Item4Text);
        $('.nDoneImage1').css('background-color', '#' + result.Item4Color);
        $('.nDoneButton1').removeClass('invisible');
    }

    $(".nDoneButton4").click(function () {
        var url;
        var first4char = result.Item1Url.substring(0, 4);
        if (first4char.toLowerCase() == 'http') {
            url = result.Item1Url;
        } else {
            url = getRootUrl() + result.Item1Url;
        }
        window.location.href = url;
    })
    $(".nDoneButton3").click(function () {
        var url;
        var first4char = result.Item2Url;
        if (first4char.toLowerCase() == 'http') {
            url = result.Item2Url;
        } else {
            url = getRootUrl() + result.Item2Url;
        }
        window.location.href = url;
    })
    $(".nDoneButton2").click(function () {
        var url;
        var first4char = result.Item3Url;
        if (first4char.toLowerCase() == 'http') {
            url = getRootUrl() + result.Item3Url;
        } else {
            url = getRootUrl() + result.Item3Url;
        }
        window.location.href = url;
    })
    $(".nDoneButton1").click(function () {
        var url;
        var first4char = result.Item4Url;
        if (first4char.toLowerCase() == 'http') {
            url = getRootUrl() + result.Item4Url;
        } else {
            url = getRootUrl() + result.Item4Url;
        }
        window.location.href = url;
    })
};
//string type, int userId, int typeId
function setDoneValues(type, userId, eventId) {
    var chat = $.connection.chatHub;
    $.connection.hub.start().done(function () {

        chat.server.done(type, userId, eventId).done(function (result) {
            console.log('message received');
            console.log(result);
            setDoneReceivedValues(result);
        })
    })

}
function aboutPage() {
    //setting emails:
    $('.nAboutPersonEmailFarhad').find('a').attr('href', 'mailto:farhad@farhade.com');
    $('.nAboutPersonEmailMehrad').find('a').attr('href', 'mailto:m3hrad@gmail.com');

    //modes: about-contact-terms-privacy-safety
    var pageMode = $('#ContentPlaceHolder1_HiddenFieldMode').val();
    modeNumber = 0;
    //`var modeNumber = 0;
    if (pageMode == 'about') {
        modeNumber = 0
    } else if (pageMode == 'contact') {
        modeNumber = 1
    } else if (pageMode == 'terms') {
        modeNumber = 2
    } else if (pageMode == 'privacy') {
        modeNumber = 3
    } else if (pageMode == 'safety') {
        modeNumber = 4
    }
    tabbedContent(modeNumber);
}
function donePage() {

    //type would be read from the url
    //types: register
    /*eventadded
    eventmodified
    eventdeleted
    reportsent
    reviewsubmited
    feedbacksubmited
    reviewsempty */

    //type should be lowercased
    // messages, notifications, requests
    //other pages modes: explore, nearby, notifications(done koochooloo), requests, 

    var url = location.pathname;
    var urlLastPart = url.substr(url.lastIndexOf('/') + 1).toString().toLowerCase();
    console.log("urlLastPart  " + urlLastPart);

    var userId;
    if ($('#HiddenFieldUserId').val() != "") {
        userId = parseInt($('#HiddenFieldUserId').val());
    } else {
        userId = 0;
    }
    setDoneValues(urlLastPart, userId, 0);

    $('.nMainPanelDiv').addClass('nfullHeight');

}

//selected tab by number, type is either add or modify
function setEventPages(pageNumber) {
    $(".nAEPage").addClass("invisible");
    var selector = ".nAEPage:eq(" + pageNumber + ")";
    $(selector).removeClass("invisible");
    var modifyPage = $('.nPageTitle').text() == 'Modify Event';

    //displaying the delete button in modify page 
    //adding animation to logos
    if (pageNumber == 0) {
        setTimeout(function () {
            $('.nActivityLogo').addClass('animationAll');
        }, 200);

        if (modifyPage) {
            $('.nProfileFooterContainerOneButton').addClass("invisible");
            $('.nProfileFooterContainerTwoButton').removeClass("invisible");
            $('.nNotButton span').text('Delete the Event');
        } else if (!modifyPage) {
            $('.nProfileFooterContainerOneButton').removeClass("invisible");
            $('.nProfileFooterContainerTwoButton').addClass("invisible");
        }
        $('.nProfileNextButton span').html("Next");
    }


    //displaying previous button
    if (pageNumber == 1) {
        $('.nActivityLogo').removeClass('animationAll');
        $('.nProfileFooterContainerOneButton').addClass("invisible");
        $('.nProfileFooterContainerTwoButton').removeClass("invisible");
        $('.nNotButton span').text('Previous');
        if ($('.nEAtime').val() == "") {
            $('.nEATimeMessage').html("");
            $('.nEADateMessage').html("");
        }

        $('.nProfileNextButton span').html("Finish");
    }

    if (pageNumber == 2) {
        $('.nEATimeMessage').html("");
        $('.nProfileFooterContainerOneButton').addClass("invisible");
        $('.nProfileFooterContainerTwoButton').removeClass("invisible");
    }
}
function settingsLocation() {
    //change of the city
    $('#ContentPlaceHolder1_DropDownListCountry, #ContentPlaceHolder1_DropDownListCity').change(function () {
        $('#ContentPlaceHolder1_HiddenFieldLocationId').val($('#ContentPlaceHolder1_DropDownListCity').val());
        console.log($('#ContentPlaceHolder1_HiddenFieldLocationId').val());
    })

    //signalR
    var chat = $.connection.chatHub;
    $.connection.hub.start().done(function () {
        //change in the country detected
        $('#ContentPlaceHolder1_DropDownListCountry').change(function () {
            chat.server.getCities($('#ContentPlaceHolder1_DropDownListCountry').val()).done(function (result) {
                if (result) {
                    console.log('it should be changed');
                    console.log(result);
                    //deletig the notification
                    $('#ContentPlaceHolder1_DropDownListCity').html(result);
                    $('#ContentPlaceHolder1_HiddenFieldLocationId').val($('#ContentPlaceHolder1_DropDownListCity').val());
                    console.log($('#ContentPlaceHolder1_HiddenFieldLocationId').val());
                }
            })
        })
    })


    $('#ContentPlaceHolder1_HiddenFieldMode').val('location');
    //displaying modify location panel in case it's just changed
    console.log("asfsdfsa " + $('#ContentPlaceHolder1_HiddenFieldStep').val());
    if ($('#ContentPlaceHolder1_HiddenFieldStep').val() == "Location") {
        $('.nSIviewLocation').addClass('invisible');
        $('.nSIviewLocation').next().removeClass('invisible');
        $('.nSIviewLocation').next().find('input').focus();
    }

    //initial settings
    if ($('#ContentPlaceHolder1_HiddenFieldLocationDetect').val() == "True") {
        //$('.nSIRowLocation').html('On');
        //$('.nSLLocationFlip').val('on').slider("refresh");
    } else {
        //$('.nSIRowLocation').html('Off');
        //$('.nSLLocationFlip').val('off').slider("refresh");
    }

    function setSIvalues() {
        //email notifications
        console.log("YO " + $('#ContentPlaceHolder1_HiddenFieldLocationDetect').val());
        if ($('#ContentPlaceHolder1_HiddenFieldLocationDetect').val() == "True") {
            $('.nSIRowLocation').html('On');
        } else {
            $('.nSIRowLocation').html('Off');
        }
    }
    $('.nSLLocationFlip').change(function () {
        if ($('.nSLLocationFlip').val() == "on") {
            $('#ContentPlaceHolder1_HiddenFieldLocationDetect').val("True");
        } else {
            $('#ContentPlaceHolder1_HiddenFieldLocationDetect').val("False");
        }
        setSIvalues();
    })
}

//gets page name as argument: settings.
function chooseBackground(pageName) {
    //setting covers backgrounds
    $('.nSearchLogo').each(function () {
        var id = $(this).attr('activity-id');
        $(this).css('background-image', 'url(../../Images/Covers/Thumbs/' + id + '.png)');
    });

    //setting cover photo my design
    $(".nSearchLogo").click(function () {
        $(".nSearchLogo").removeClass("nSPCoverPhotoSelected");
        activity = $(this).attr("data");
        //$(this).css("background-color", "rgb(255,187,5)");
        $(this).addClass("nSPCoverPhotoSelected");
        //activity = $(this).attr("data");
        activityID = $(this).attr("activity-id");
        $('.nSPCoverPhoto').css('background-image', 'url(../../Images/Covers/' + activityID + '.jpg)');
        //$('.nEAACtivityType').text(activity);
        console.log(activityID);
        $('#ContentPlaceHolder1_HiddenFieldCoverId').val(activityID);
        if (pageName == "settings") {
            $('#ContentPlaceHolder1_HiddenFieldValue').val(activityID);
            console.log('#ContentPlaceHolder1_HiddenFieldValue ' + activityID);
            $('#ContentPlaceHolder1_HiddenFieldMode').val('cover');
            $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
        }
    })
}
function settingsPhotos() {
    //hidden field status, remove kone = 0, upload kone 1
    //hiding remove button
    if ($('#ContentPlaceHolder1_HiddenFieldStatus').val() == '0') {
        $('.nSPRemoveButton').addClass('invisible');
    }

    //$('#ContentPlaceHolder1_ButtonSubmit').attr('CommandName', 'photo');
    chooseBackground('settings');
    //setting initial background value
    var coverID = $('#ContentPlaceHolder1_HiddenFieldCoverId').val();
    $('.nSPCoverPhoto').css('background-image', 'url(../../Images/Covers/' + coverID + '.jpg)');
    //initial selecting the right circle
    var selector = '.nSearchLogo[activity-id="' + coverID + '"]';
    $(selector).addClass("nSPCoverPhotoSelected");

    //setting activity initially
    /*var activityID = $('#ContentPlaceHolder1_HiddenFieldTypeId').val();
    var selector = ".nSearchLogo[activity-id=" + activityID + "]";
    $(selector).css("background-color", "rgb(255,187,5)");
    */

    //setting activity original design
    /*
    $(".nSearchLogo").click(function () {
        $(".nSearchLogo").css("background-color", "rgb(222,222,222)");
        activity = $(this).attr("data");
        $(this).css("background-color", "rgb(255,187,5)");
        //activity = $(this).attr("data");
        //activityID = $(this).attr("activity-id");
        //$('.nEAACtivityType').text(activity);
        //$('#ContentPlaceHolder1_HiddenFieldTypeId').val(activityID);
    })
    */

    //setting cover photo initially
    /*var activityID = $('#ContentPlaceHolder1_HiddenFieldTypeId').val();
    var selector = ".nSPCoverPhoto[activity-id=" + activityID + "]";
    $(selector).addClass("nSPCoverPhotoSelected");
    */

    //setting cover photo original design
    /*
    $(".nSPCoverPhoto").click(function () {
        $(".nSPCoverPhoto").removeClass("nSPCoverPhotoSelected");
        $(this).addClass("nSPCoverPhotoSelected");
        //activity = $(this).attr("data");
        //activityID = $(this).attr("activity-id");
        //$('.nEAACtivityType').text(activity);
        //$('#ContentPlaceHolder1_HiddenFieldTypeId').val(activityID);
    })
    */

    //displaying uploaded photo
    $('.nSPAspChooseButton').change(function () {
        var files = $(".nSPAspChooseButton")[0].files[0];
        if (files) {
            console.log("if is true");
            var reader = new FileReader();
            reader.onload = function (e) {
                console.log("onload function called");
                $('.nProfpagePicture')
                    .css('background-image', 'url(' + e.target.result + ')');
                $('.nSPUploadButton').removeClass('invisible');
                // $('.nCompletionPhotoInside').addClass('invisible');
                //$('.nEventCoverBoxSides').removeClass('hidden');
                //$('#ContentPlaceHolder1_HiddenFieldMode').val('photo');
                //$('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
            };
            reader.readAsDataURL(files);
        }
    });

    //displaying the error message
    if ($("#ContentPlaceHolder1_LabelPhotoMessage").text() != "") {
        $('.nWrongLogin').removeClass("invisible");
    }

    var photoUrl = $('#ContentPlaceHolder1_HiddenFieldPhotoUrl').val();
    photoUrl = '../' + photoUrl;
    $('.nProfpagePicture').css('background-image', 'url(' + photoUrl + ')');

    $(".nSPChooseFileButton").click(function () {
        $(".nSPAspChooseButton").trigger("click");
    })
    $(".nSPUploadButton").click(function () {
        $('#ContentPlaceHolder1_HiddenFieldMode').val('photo');
        $(".nSPAspUploadButton").trigger("click");
    })
    $(".nSPRemoveButton").click(function () {
        $("#ContentPlaceHolder1_ButtonPhotoRemove").trigger("click");
    })
}
function settingsPage() {

    //setting username message if it is clicked
    //HiddenFieldStatus 
    //0 user entered it's own username 
    //1 successful
    //-1 exists before 
    //-2 username not valid 
    //-3 no special char 
    //-4 enter username 
    usernameStatus = $('#ContentPlaceHolder1_HiddenFieldStatus').val();
    //usernameStatus = '-4';
    switch (usernameStatus) {
        case '0':
            $('.nSettingsUNError0').removeClass('invisible');
            break;
        case '1':
            //$('.nSettingsUNError1').removeClass('invisible');
            break;
        case '-1':
            $('.nSettingsUNError-1').removeClass('invisible');
            break;
        case '-2':
            $('.nSettingsUNError-2').removeClass('invisible');
            break;
        case '-3':
            $('.nSettingsUNError-3').removeClass('invisible');
            break;
        case '-4':
            $('.nSettingsUNError-4').removeClass('invisible');
            break;
    }

    // command names: information, photo, location, preferences, account, privacy
    //$('#ContentPlaceHolder1_ButtonSubmit').attr('CommandName', 'information');
    //triggering back button
    //saving the initiatial value of the username to be compared later
    var initialUname = $('#ContentPlaceHolder1_TextBoxUsername').val();
    //setting birthdate
    //console.log('pekh ' + $('#ContentPlaceHolder1_HiddenFieldDOB').val())
    $('#nSIDateInput').val($('#ContentPlaceHolder1_HiddenFieldDOB').val());
    //preventing enter key in inputs
    $('input').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            if (this.id == 'ContentPlaceHolder1_TextBoxFirstName') {
                console.log(validateName($('#ContentPlaceHolder1_TextBoxFirstName').val()));
                if (!validateName($('#ContentPlaceHolder1_TextBoxFirstName').val())) { //firstname
                    $('.nSettingsFNError').removeClass('invisible');
                } else {
                    $('.nSettingsFNError').addClass('invisible');
                    $('#ContentPlaceHolder1_HiddenFieldMode').val('firstname');
                    $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
                }
            } else if (this.id == 'ContentPlaceHolder1_TextBoxLastName') { //lastname
                if (!validateName($('#ContentPlaceHolder1_TextBoxLastName').val())) {
                    $('.nSettingsLNError').removeClass('invisible');
                } else {
                    $('.nSettingsLNError').addClass('invisible');
                    $('#ContentPlaceHolder1_HiddenFieldMode').val('lastName');
                    $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
                    console.log('last name');
                }
            } else if (this.id == 'ContentPlaceHolder1_TextBoxUsername') { //username
                console.log(validateName($('#ContentPlaceHolder1_TextBoxUsername').val()));
                if (!validateName($('#ContentPlaceHolder1_TextBoxUsername').val())) {
                    //if (false) {
                    $('.nSettingsUNError').removeClass('invisible');
                } else {
                    $('.nSettingsUNError').addClass('invisible');
                    if (!SameWords($('#ContentPlaceHolder1_TextBoxUsername').val(), initialUname)) {
                        $('#ContentPlaceHolder1_HiddenFieldMode').val('username');
                        $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
                        console.log('not same ');
                    } else {
                        console.log(' same ');
                    }
                }
            } else if (this.id == 'nSIDateInput') { //birthdate
                if (!validateAdult($('#nSIDateInput').val(), 18)) {
                    //$('#nSIDateInput').val($('#ContentPlaceHolder1_HiddenFieldDOB').val());
                    $('.nSettingsDOBError').removeClass('invisible');
                } else {
                    $('#ContentPlaceHolder1_HiddenFieldDOB').val($('#nSIDateInput').val());
                    console.log($('#ContentPlaceHolder1_HiddenFieldDOB').val());
                    $('.nSettingsDOBError').addClass('invisible');
                    $('#ContentPlaceHolder1_HiddenFieldMode').val('dob');
                    $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
                }
            }
            event.preventDefault();
            return false;
        }
    });


    //initial set of date
    $('#ContentPlaceHolder1_HiddenFieldDOB').val($('#nSIDateInput').val());
    //settings values of 
    function setSIvalues() {
        //first name
        if (validateName($('#ContentPlaceHolder1_TextBoxFirstName').val())) {
            $('.nSIRowFirstName').html($('#ContentPlaceHolder1_TextBoxFirstName').val());
            $('.nSettingsFNError').addClass('invisible');
        }
        //last name
        if (validateName($('#ContentPlaceHolder1_TextBoxLastName').val())) {
            $('.nSIRowLastName').html($('#ContentPlaceHolder1_TextBoxLastName').val());
            $('.nSettingsLNError').addClass('invisible');
        }


        console.log("validateUsername($('#ContentPlaceHolder1_TextBoxUserName').val())" +
            validateUsername($('#ContentPlaceHolder1_TextBoxUserName').val()));

        console.log("validateUsernameServer($('#ContentPlaceHolder1_TextBoxUserName').val())" +
            validateUsernameServer($('#ContentPlaceHolder1_TextBoxUserName').val()));
        //username on client and server
        if (validateUsername($('#ContentPlaceHolder1_TextBoxUserName').val()) &&
            validateUsernameServer($('#ContentPlaceHolder1_TextBoxUserName').val())) {
            $('.nSIRowUserName').html($('#ContentPlaceHolder1_TextBoxUsername').val());
        }
        if (validateUsername($('#ContentPlaceHolder1_TextBoxUserName').val())) {
            $('.nSIRowUserName').html($('#ContentPlaceHolder1_TextBoxUsername').val());
            $('.nSettingsUNError').addClass('invisible');
        }
        if (validateUsernameServer($('#ContentPlaceHolder1_TextBoxUserName').val())) {
            $('.nSIRowUserName').html($('#ContentPlaceHolder1_TextBoxUsername').val());
            $('.nSettingsUNServerError').addClass('invisible');
        }

        //Gender
        if ($('#ContentPlaceHolder1_DropDownListGender').val() != "0") {
            var gender = $('#ContentPlaceHolder1_DropDownListGender').val();
            $('.nSIRowGender').html($('#ContentPlaceHolder1_DropDownListGender').find('[Value=' + gender + ']').text());
            $('.nSettingsGenderError').addClass('invisible');
        }

        //birthdate
        if (validateAdult($('#nSIDateInput').val(), 18)) {
            $('.nSIRowDob').html($('#ContentPlaceHolder1_HiddenFieldDOB').val());
            $('.nSettingsDOBError').addClass('invisible');
        }

        //$('.nSIRowDob').html($('#ContentPlaceHolder1_HiddenFieldDOB').val());
        //about
        $('.NSRowAboutValueDiv').html($('#ContentPlaceHolder1_TextBoxAbout').val());
    }

    //opening modify rows
    $(".nSIview").click(function () {
        $(this).addClass('invisible');
        $(this).next().removeClass('invisible');
        $(this).next().find('input').focus();
    })

    //enter the values and trigger the submit button
    //validate first name
    $('.nSIHelperFirstName').click(function () {
        console.log(validateName($('#ContentPlaceHolder1_TextBoxFirstName').val()));
        if (!validateName($('#ContentPlaceHolder1_TextBoxFirstName').val())) {
            $('.nSettingsFNError').removeClass('invisible');
            return;
        } else {
            $('.nSettingsFNError').addClass('invisible');
            $('#ContentPlaceHolder1_HiddenFieldMode').val('firstname');
            $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
        }
    })
    //validate last name
    $('.nSIHelperLastName').click(function () {
        if (!validateName($('#ContentPlaceHolder1_TextBoxLastName').val())) {
            $('.nSettingsLNError').removeClass('invisible');
            return;
        } else {
            $('.nSettingsLNError').addClass('invisible');
            $('#ContentPlaceHolder1_HiddenFieldMode').val('lastName');
            $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
            console.log('last name');
        }
    })
    //validate usr name
    $('.nSIHelperUsername').click(function () {
        console.log(validateName($('#ContentPlaceHolder1_TextBoxUsername').val()));
        if (!validateName($('#ContentPlaceHolder1_TextBoxUsername').val())) {
            //if (false) {
            $('.nSettingsUNError').removeClass('invisible');
            return;
        } else {
            if (!SameWords($('#ContentPlaceHolder1_TextBoxUsername').val(), initialUname)) {
                $('.nSettingsUNError').addClass('invisible');
                $('#ContentPlaceHolder1_HiddenFieldMode').val('username');
                $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
                console.log('username');
            } else {
            }
        }
    })

    //gender modify 
    $('.nSIHelperGender').click(function () {
        if ($('#ContentPlaceHolder1_DropDownListGender').val() == "0") {
            $('.nSettingsGenderError').removeClass('invisible');
            return;
        } else {
            $('.nSettingsGenderError').addClass('invisible');
            $('#ContentPlaceHolder1_HiddenFieldMode').val('gender');
            $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
        }
    });
    //birthdate modify
    $('.nSIHelperDOB').click(function () {
        console.log("date   " + validateAdult($('#nSIDateInput').val()));
        if (!validateAdult($('#nSIDateInput').val(), 18)) {
            $('#nSIDateInput').val($('#ContentPlaceHolder1_HiddenFieldDOB').val());
            $('.nSettingsDOBError').removeClass('invisible');
            return;
        } else {
            $('#ContentPlaceHolder1_HiddenFieldDOB').val($('#nSIDateInput').val());
            console.log($('#ContentPlaceHolder1_HiddenFieldDOB').val());
            $('.nSettingsDOBError').addClass('invisible');
            $('#ContentPlaceHolder1_HiddenFieldMode').val('dob');
            $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
        }
    });
    //about
    $('.nSIHelperAbout').click(function () {
        $('#ContentPlaceHolder1_HiddenFieldMode').val('about');
        $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
    })
    //password
    $('.nSIHelperPassword').click(function () {
        $('#ContentPlaceHolder1_ButtonPassword').trigger('click');
    })
    //location
    $('.nSIHelperLocation').click(function () {
        console.log($('#ContentPlaceHolder1_DropDownListCountry :selected').text());
        var selectedCountry = $('#ContentPlaceHolder1_DropDownListCountry :selected').text();
        var selectedCity = $('#ContentPlaceHolder1_DropDownListCity :selected').text();

        if (selectedCountry == 'Select Country' || selectedCity == 'Select City') {
            $('.nSettingsLocationError').removeClass('invisible');
            console.log('yeps');
            return;
        } else {
            console.log('Nops');
            $('.nSettingsFNError').addClass('invisible');
            $('#ContentPlaceHolder1_HiddenFieldMode').val('location');
            $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
        }
    })
    //email notification
    $('.nSIHelperEmailNotification').click(function () {
        //needs to be set properly
        $('#ContentPlaceHolder1_HiddenFieldMode').val('notifications');
        $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
    })
    //phone
    $('.nSIHelperEmailPhone').click(function () {

        /*$('#ContentPlaceHolder1_HiddenFieldMode').val('mobile');
        $('#ContentPlaceHolder1_ButtonSubmit').trigger('click'); */
    })
    setSIvalues();

    var url;
    //back button
    $(".nProfileMessageButton").click(function () {
        url = getRootUrl() + "Settings";
        window.location.href = url;
    })
    $(".nSettingsListInformation").click(function () {
        url = "../Settings/Information";
        window.location.href = url;
    })
    $(".nSettingsListPhoto").click(function () {
        url = "../Settings/Photo";
        window.location.href = url;
    })
    $(".nSettingsListLocation").click(function () {
        url = "../Settings/Location";
        window.location.href = url;
    })
    $(".nSettingsListPreferences").click(function () {
        url = "../Settings/Preferences";
        window.location.href = url;
    })
    $(".nSettingsListAccount").click(function () {
        url = "../Settings/Account";
        window.location.href = url;
    })
    $(".nSettingsListPrivacy").click(function () {
        url = "../Settings/Privacy";
        window.location.href = url;
    })
}

function settingsAccount() {
    var userId = parseInt($('#HiddenFieldUserId').val());
    console.log("userId " + userId);
    var chat = $.connection.chatHub;
    $.connection.hub.start().done(function () {
        $('.nSAVerifyEmail').click(function () {
            //console.log("verify email cliceked:");
            chat.server.verifyEmail(userId);
            $('.nSVerifyEmailContainer').addClass('invisible');
            $('.nSVerifyEmailMessage').removeClass('invisible');
            /*chat.server.verifyEmail(userId).done(function (result) {
                    console.log("result :" + result);
                 
            }) */
        })
    })

    //nSLVerifyEmail
    var emailVerified = $('#ContentPlaceHolder1_HiddenFieldEmailVerify').val();
    if (emailVerified == 'False') {
        $('.nSVerifyEmailContainer').removeClass('invisible');
    }
    //check if the user doesn't have password yet?
    var noPassword = $('#ContentPlaceHolder1_HiddenFieldStatus').val() == "0";
    //noPassword = true;
    console.log("noPassword " + noPassword);
    if (noPassword) {
        $('.nSettingsOldPassword').addClass('invisible');
    }
    //displaying the email

    //opening password tab if there is an error
    if ($('#ContentPlaceHolder1_LabelPasswordMessage').length > 0) {
        $('.nSAchangePassword').trigger('click');
    }
    //$('#ContentPlaceHolder1_ButtonSubmit').attr('CommandName', 'account');
    $('.nSAchangePassword').click(function () {
        $('#ContentPlaceHolder1_TextBoxPasswordOld').focus();
    })
    $('.nProfileFollowButton').click(function () {
        $('#ContentPlaceHolder1_ButtonFollow').trigger('click');
    });
}
function settingsPreferences() {
    //$('#ContentPlaceHolder1_ButtonSubmit').attr('CommandName', 'preferences');
    //initial settings
    if ($('#ContentPlaceHolder1_HiddenFieldNotifications').val() == "True") {
        $('.nSIRowEmails').html('On');
        $('#emailNotificationFlip').val('on').slider("refresh");
    } else {
        $('.nSIRowEmails').html('Off');
        $('#emailNotificationFlip').val('off').slider("refresh");
    }
    //sound notifications
    if ($('#ContentPlaceHolder1_HiddenFieldSound').val()) {
        $('.nSIRowSounds').html('On');
        $('#soundFlip').val('on').slider("refresh");
    } else {
        $('.nSIRowSounds').html('Off');
        $('#soundFlip').val('off').slider("refresh");
    }

    function setSIvalues() {
        //email notifications
        console.log("YO " + $('#ContentPlaceHolder1_HiddenFieldNotifications').val());
        if ($('#ContentPlaceHolder1_HiddenFieldNotifications').val() == "True") {
            $('.nSIRowEmails').html('On');
        } else {
            $('.nSIRowEmails').html('Off');
        }
        //sound notifications
        if ($('#ContentPlaceHolder1_HiddenFieldSound').val() == "True") {
            $('.nSIRowSounds').html('On');
        } else {
            $('.nSIRowSounds').html('Off');
        }
    }

    //setting email
    $('#emailNotificationFlip').change(function () {
        if ($('#emailNotificationFlip').val() == "on") {
            $('#ContentPlaceHolder1_HiddenFieldNotifications').val("True");
        } else {
            $('#ContentPlaceHolder1_HiddenFieldNotifications').val("False");
        }
        setSIvalues();
    })
    //setting sound
    $('#soundFlip').change(function () {
        if ($('#soundFlip').val() == "on") {
            $('#ContentPlaceHolder1_HiddenFieldSound').val("True");
        } else {
            $('#ContentPlaceHolder1_HiddenFieldSound').val("False");
        }
        setSIvalues();
    })
}

function completionPage() {
     isMenuEnabled = false;
    var footerClicked = false;
    //$('.nMainPanelDiv').addClass('nCompletionBG');
    footerFix(38, "nFooterShort40");

    //change of the city
    $('.nCompletionLocationInput ').change(function () {
        $('#ContentPlaceHolder1_HiddenFieldLocationId').val($('.nCompletionLocationLastInput').val());
        console.log($('#ContentPlaceHolder1_HiddenFieldLocationId').val());
    })

    //setting the photo if it is already there
    if ($('#ContentPlaceHolder1_HiddenFieldHasPhoto').val() == "True") {
        $('.nCompletionCircleMain').css('background-image', "url('" + getRootUrl() + $('#ContentPlaceHolder1_HiddenFieldPhotoUrl').val() + "')");
        $('.nCompletionCircleMain div').addClass("hidden");
    }

    //signalR
    var chat = $.connection.chatHub;
    $.connection.hub.start().done(function () {
        //change in the country detected
        $('.nCompletionLocationInputCountry').change(function () {
            chat.server.getCities($('.nCompletionLocationInputCountry').val()).done(function (result) {
                if (result) {
                    console.log('it should be changed');
                    console.log(result);
                    //deleting the notification
                    $('.nCompletionLocationLastInput').html(result);
                    var selectedItem = $('.nCompletionLocationLastInput').val();
                    $('#ContentPlaceHolder1_HiddenFieldLocationId').val(selectedItem);
                    console.log($('#ContentPlaceHolder1_HiddenFieldLocationId').val());
                }
            })
        })


    })

    addGoogleAnalytics();
    //functionality of not listed locations
    var cityIsDefined = true;
    //HiddenFieldLocationStatus 1 ok 0 requested)
    $('#ContentPlaceHolder1_HiddenFieldLocationStatus').val('1');
    //display adding new location
    $('.nCompletionNotListedButton').click(function () {
        $('.nCompletionLocationInput').addClass('invisible');
        $('.nCompletionNewCountry').removeClass('invisible');
        $('.nCompletionNewCity').removeClass('invisible');
        $('.nCompletionListedButton').removeClass('invisible');
        $('.nCompletionNotListedButton').addClass('invisible');
        cityIsDefined = false;
        $('#ContentPlaceHolder1_HiddenFieldLocationStatus').val('0');
    });
    //display location lists
    $('.nCompletionListedButton').click(function () {
        $('.nCompletionLocationInput').removeClass('invisible');
        $('.nCompletionNewCountry').addClass('invisible');
        $('.nCompletionNewCity').addClass('invisible');
        $('.nCompletionListedButton').addClass('invisible');
        $('.nCompletionNotListedButton').removeClass('invisible');
        cityIsDefined = true;
        $('#ContentPlaceHolder1_HiddenFieldLocationStatus').val('1');
    });
    var locationFlip = "False";
    //saving location status
    $('.nCompletionLocationFlip').change(function () {
        if ($('.nCompletionLocationFlip').val() == "on") {
            //$('#ContentPlaceHolder1_HiddenFieldLocationDetect').val("True");
            locationFlip = "True";
            $('.nCompletionBoxSecondLocation').css('height', '4em');
            $('.nCompletionBoxSecondLocation').parent().css('height', '4em');
        } else {
            //$('#ContentPlaceHolder1_HiddenFieldLocationDetect').val("False");
            locationFlip = "False";
            $('.nCompletionBoxSecondLocation').css('height', '5em');
            $('.nCompletionBoxSecondLocation').parent().css('height', '5em');
        }
    })

    //showing error if it exists
    if ($('#ContentPlaceHolder1_LabelMessage').length) {
        $('.nCompletionValidationErrorMessageBackEnd').removeClass('invisible');
    };
    //hiding the photo if he has it 
    if ($('.nCompletionCircleUpload') == 1) {
        $('.nCompletionCircle').addClass('hidden');
    };

    //upload photo button pressed:
    $('.nCompletionCircleUpload').click(function () {
        $('#ContentPlaceHolder1_ButtonSave').trigger('click');
    });
    //displaying uploaded photo
    $('#ContentPlaceHolder1_FileUpload1').change(function () {
        var files = $("#ContentPlaceHolder1_FileUpload1")[0].files[0];
        if (files) {
            console.log("if is true");
            var reader = new FileReader();
            reader.onload = function (e) {
                console.log("onload function called");
                $('.nCompletionCircleMain')
                    .css('background-image', 'url(' + e.target.result + ')');
                $('.nCompletionPhotoInside').addClass('invisible');
                $('.nEventCoverBoxSides').removeClass('hidden');
            };
            reader.readAsDataURL(files);
        }
    });
    //set upload input to get only images
    $('#ContentPlaceHolder1_FileUpload1').attr('accept', "image/*");

    //uploading photos
    $('.nCompletionCircleMain').click(function () {
        $('#ContentPlaceHolder1_FileUpload1').trigger('click');
        if ($('#ContentPlaceHolder1_FileUpload1').val() != "") {

        };
    });

    $('#nMainPanelLi').addClass('nMainPanelScrollable');


    //setting date of birth
    //var dateOfBirth = console.log("asdfkhsdafsdaf: " + $('#ContentPlaceHolder1_HiddenFieldDOB').val());
    $('.nCompletionDateInput').val($('#ContentPlaceHolder1_HiddenFieldDOB').val());
    $('.nCompletionDateInput').change(function () {
        console.log($('.nCompletionDateInput').val());
        $('#ContentPlaceHolder1_HiddenFieldDOB').val($('.nCompletionDateInput').val());
    })
    setLogos();

    //opening location if user is setting it
    if ($('#ContentPlaceHolder1_DropDownListCountry').val() != 0) {
        $('.nCompletionBoxSecondLocation').css('height', '8em');
        $('.nCompletionBoxSecondLocation').parent().css('height', '8em');
        $('.nCompletionBoxFirstLocation').addClass('invisible');
        $('.nCompletionBoxSecondLocation').removeClass('invisible');
    }
    //prevent form submit by enter
    $('.nCompletionBox').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13)
            event.preventDefault();
    });

    $('.nCompletionTextInputFN').change(function () {
        var firstNameValue = $('#ContentPlaceHolder1_TextBoxFirstName').val();
        if (!validateName(firstNameValue)) {
            $('.nCompletionFNError').removeClass('invisible');
        } else {
            $('.nCompletionFNError').addClass('invisible');
        }
        setLogos();
    })

    $('.nCompletionTextInputLN').change(function () {
        var lastNameValue = $('#ContentPlaceHolder1_TextBoxLastName').val();
        if (!validateName(lastNameValue)) {
            $('.nCompletionLNError').removeClass('invisible');
        } else {
            $('.nCompletionLNError').addClass('invisible');
        }
        setLogos();
    })

    $('.nCompletionTextInputUN').change(function () {
        var usernameValue = $('.nCompletionTextInputUN').val();
        if (!validateUsername(usernameValue)) {
            $('.nCompletionUNError').removeClass('invisible');
            $('.nCompletionUNServerError').addClass('invisible');
        } else {
            $('.nCompletionUNError').addClass('invisible');
            validateUsernameServer(usernameValue);
            setTimeout(function () {
                if (UNIsvalidServer == false) {
                    $('.nCompletionUNServerError').removeClass('invisible');
                } else {
                    $('.nCompletionUNServerError').addClass('invisible');
                }
            }, 1000);
        }
        setLogos();
    })

    //gender
    $('.nCompletionInputGender').change(function () {
        if ($('#ContentPlaceHolder1_DropDownListGender').val() == "0") {
            $('.nCompletionGenderError').removeClass('invisible');
        } else {
            $('.nCompletionGenderError').addClass('invisible');
        }
        setLogos();
    })

    //birthdate
    $('.nCompletionDateInput').change(function () {
        var dobValid = true;
        var dob = $('#ContentPlaceHolder1_HiddenFieldDOB').val();
        if ($('#ContentPlaceHolder1_HiddenFieldDOB').val() == "") {
            dobValid = false;
            if (!$(this).hasClass('nFooterButton')) {
                $('.nCompletionValidationErrorMessage').addClass('invisible');
            }
            $('.nCompletionBDError').removeClass('invisible');
        } else {
            //$('.nCompletionBDError').addClass('invisible');
            //validate 18 years
            if (!validateAdult(dob, 18) && dobValid) {
                if (!validateAdult(dob, 0)) {
                    $('.nCompletionBDfutureError').removeClass('invisible');
                } else {
                    $('.nCompletionBDadultError').removeClass('invisible');
                }
            }
            else {
                $('.nCompletionBDadultError').addClass('invisible');
                $('.nCompletionBDfutureError').addClass('invisible');
            }
        }
        setLogos();
    })

    //location
    $('.nCompletionLocationInputCountry').change(function () {
        //city is defined
        console.log("cityIsDefined :" + cityIsDefined);
        console.log("cityIsDefined2 :" + $('.nCompletionLocationInputCountry').val());
        if (cityIsDefined) {
            if ($('.nCompletionLocationInputCountry').val() == "0") {

                $('.nCompletionLocationLastInput').html('');
                $('.nCompletionLocationError').removeClass('invisible');
            } else {
                $('.nCompletionLocationError').addClass('invisible');
            }
        }
            //new city
        else {
            if (!validateName(newCountry) || !validateName(newCity)) {
                $('.nCompletionNewLocationError').removeClass('invisible');
            } else {
                $('.nCompletionNewLocationError').addClass('invisible');
            }
        }
        setLogos();
    })


    $('.nValidatorButton').click(function () {
        if ($(this).hasClass("nFooterButton")) {
            footerClicked = true;
        } else {
            footerClicked = false;
        }
        $('.nCompletionErrorMessage').html("");
        var valid = true;
        //validation for required fields
        var firstNameValue = $('#ContentPlaceHolder1_TextBoxFirstName').val();
        var lastNameValue = $('#ContentPlaceHolder1_TextBoxLastName').val();
        var usernameValue = $('#ContentPlaceHolder1_TextBoxUsername').val();
        var newCountry = $('.nCompletionNewCountry').val();
        var newCity = $('.nCompletionNewCity').val();


        //first name
        if ($(this).hasClass('nCompletionArrowFN') || $(this).hasClass('nFooterButton')) {
            console.log("FN validation called");
            if (!validateName(firstNameValue)) {
                valid = false;
                if (footerClicked) {
                    $('.nCompletionValidationErrorMessage').addClass('invisible');
                }
                $('.nCompletionFNError').removeClass('invisible');
            } else {
                $('.nCompletionFNError').addClass('invisible');
            }
        }

        //last name
        if ($(this).hasClass('nCompletionArrowLN') || $(this).hasClass('nFooterButton')) {
            if (!validateName(lastNameValue)) {
                valid = false;
                if (!$(this).hasClass('nFooterButton')) {
                    $('.nCompletionValidationErrorMessage').addClass('invisible');
                }
                $('.nCompletionLNError').removeClass('invisible');
            } else {
                $('.nCompletionLNError').addClass('invisible');
            }
        }

        //username
        if ($(this).hasClass('nCompletionArrowUN') || $(this).hasClass('nFooterButton')) {
            if (!validateUsername(usernameValue)) {
                valid = false;
                if (!footerClicked) {
                    $('.nCompletionValidationErrorMessage').addClass('invisible');
                }
                $('.nCompletionUNError').removeClass('invisible');
                $('.nCompletionUNServerError').addClass('invisible');
                validateUsernameServer(usernameValue);
            } else {
                setTimeout(function () {
                    console.log("UNIsvalidServer :" + UNIsvalidServer);
                    if (UNIsvalidServer == false) {
                        valid = false;
                        if (!footerClicked) {
                            $('.nCompletionValidationErrorMessage').addClass('invisible');
                        }
                        $('.nCompletionUNServerError').removeClass('invisible');
                    } else {
                        $('.nCompletionUNServerError').addClass('invisible');
                    }
                }, 1000);
                $('.nCompletionUNError').addClass('invisible');
            }

            //validating server part


        }

        //gender
        if ($(this).hasClass('nCompletionArrowGender') || $(this).hasClass('nFooterButton')) {
            if ($('#ContentPlaceHolder1_DropDownListGender').val() == "0") {
                valid = false;
                if (!$(this).hasClass('nFooterButton')) {
                    $('.nCompletionValidationErrorMessage').addClass('invisible');
                }
                $('.nCompletionGenderError').removeClass('invisible');

            } else {
                $('.nCompletionGenderError').addClass('invisible');
            }
        }

        //birthdate
        var dobValid = true;
        var dob = $('#ContentPlaceHolder1_HiddenFieldDOB').val();
        if ($(this).hasClass('nCompletionArrowBD') || $(this).hasClass('nFooterButton')) {
            if ($('#ContentPlaceHolder1_HiddenFieldDOB').val() == "") {
                valid = false;
                dobValid = false;
                if (!$(this).hasClass('nFooterButton')) {
                    $('.nCompletionValidationErrorMessage').addClass('invisible');
                }
                $('.nCompletionBDError').removeClass('invisible');
            } else {
                $('.nCompletionBDError').addClass('invisible');
            }

            //validate 18 years
            if (!validateAdult(dob, 18) && dobValid) {
                valid = false;
                if (!$(this).hasClass('nFooterButton')) {
                    $('.nCompletionValidationErrorMessage').addClass('invisible');
                }
                if (!validateAdult(dob, 0)) {
                    $('.nCompletionBDfutureError').removeClass('invisible');
                } else {
                    $('.nCompletionBDadultError').removeClass('invisible');
                }
            }
            else {
                $('.nCompletionBDadultError').addClass('invisible');
                $('.nCompletionBDfutureError').addClass('invisible');
            }
        }

        //location
        if ($(this).hasClass('nCompletionArrowLocation') || $(this).hasClass('nFooterButton')) {
            //city is defined
            if (cityIsDefined) {
                if ($('#ContentPlaceHolder1_DropDownListCity').val() == "0") {
                    valid = false;
                    if (!$(this).hasClass('nFooterButton')) {
                        $('.nCompletionValidationErrorMessage').addClass('invisible');
                    }
                    $('.nCompletionLocationError').removeClass('invisible');

                } else {
                    $('.nCompletionLocationError').addClass('invisible');
                }
            }
                //new city
            else {
                if (!validateName(newCountry) || !validateName(newCity)) {
                    valid = false;
                    if (!$(this).hasClass('nFooterButton')) {
                        $('.nCompletionValidationErrorMessage').addClass('invisible');
                    }
                    $('.nCompletionNewLocationError').removeClass('invisible');
                } else {
                    $('.nCompletionNewLocationError').addClass('invisible');
                }
            }
        }

        setTimeout(function () {
            console.log("footer check: " + valid + " " + footerClicked);
            if (valid && footerClicked) {
                console.log("final button clicked");
                //$('.nCompletionErrorMessage').html("done");
                $("#ContentPlaceHolder1_ButtonSubmit").trigger("click");
            }
        }, 1100);

    })

    $('.nCompletionBoxFirst').click(function () {
        $(this).addClass('invisible');
        $(this).next().removeClass('invisible');
        if ($(this).hasClass('nCompletionBoxFirstLocation')) {
            if (locationFlip == "False") {
                $(this).next().css('height', '5em');
                $(this).parent().css('height', '5em');
            } else {
                $(this).next().css('height', '5em');
                $(this).parent().css('height', '5em');
            }
        }
        var desiredClasses = $(this).attr('class').split(" ");
        var desiredClass = "." + desiredClasses[1];
        setTimeout(function () {
            $(desiredClass).next().find(".nCompletionInput input").focus();
            $(desiredClass).next().find(".nCompletionDropdown select").focus();
            $(desiredClass).next().find(".nCompletionLocationContainer .nCompletionLocationInputCountry").focus();
        }, 100);
    })

    $('.nCompletionArrow').click(function () {
        setLogos();
        $(this).parent().addClass('invisible');
        $(this).parent().parent().find('.nCompletionBoxFirst').removeClass('invisible');
        if ($(this).parent().hasClass('nCompletionBoxSecond')) {
            $(this).parent().parent().find('.nCompletionBoxFirst').css('height', '2em');
            $(this).parent().parent().find('.nCompletionBoxSecond').css('height', '2em');
            $(this).parent().parent().css('height', '2em');
        }
    })
    //set logos
    function setLogos() {
        //firstname
        if (!validateName($('#ContentPlaceHolder1_TextBoxFirstName').val())) {
            $('.nCompletionBoxFirstFN').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/ERROR_icon.png')");
        } else {
            $('.nCompletionBoxFirstFN').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/OK_icon.png')");
        }
        //last name
        if (!validateName($('#ContentPlaceHolder1_TextBoxLastName').val())) {
            $('.nCompletionBoxFirstLN').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/ERROR_icon.png')");
        } else {
            $('.nCompletionBoxFirstLN').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/OK_icon.png')");
        }
        //user name
        if (!validateUsername($('#ContentPlaceHolder1_TextBoxUsername').val())) {
            $('.nCompletionBoxFirstUN').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/ERROR_icon.png')");

        } else {
            $('.nCompletionBoxFirstUN').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/OK_icon.png')");
            setTimeout(function () {

                console.log("!validateUsername($('#ContentPlaceHolder1_TextBoxUsername').val()) " + !validateUsername($('#ContentPlaceHolder1_TextBoxUsername').val()));
                console.log("!UNIsvalidServer" + !UNIsvalidServer);
                if (!validateUsername($('#ContentPlaceHolder1_TextBoxUsername').val()) || !UNIsvalidServer) {
                    $('.nCompletionBoxFirstUN').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/ERROR_icon.png')");
                } else {
                    $('.nCompletionBoxFirstUN').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/OK_icon.png')");
                }
            }, 1100);
        }

        //gender
        if ($('#ContentPlaceHolder1_DropDownListGender').val() == "0") {
            $('.nCompletionBoxFirstGender').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/ERROR_icon.png')");
        } else {
            $('.nCompletionBoxFirstGender').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/OK_icon.png')");
        }
        //date
        if ($('#date').val() == "") {
            $('.nCompletionBoxFirstBD').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/ERROR_icon.png')");
        } else {
            $('.nCompletionBoxFirstBD').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/OK_icon.png')");
        }
        //location
        if (cityIsDefined) {
            if ($('#ContentPlaceHolder1_DropDownListCountry').val() == "0") {
                $('.nCompletionBoxFirstLocation').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/ERROR_icon.png')");
            } else {
                $('.nCompletionBoxFirstLocation').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/OK_icon.png')");
            }
        }
        else {
            if (!validateName($('.nCompletionNewCountry').val()) || !validateName($('.nCompletionNewCity').val())) {
                $('.nCompletionBoxFirstLocation').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/ERROR_icon.png')");
            } else {
                $('.nCompletionBoxFirstLocation').next().find('.nCompletionStatus').css('background-image', "url('../Images/Icons/OK_icon.png')");
            }
        }
    }
}

function notificationsPage() {
    //signalR
    // Declare a proxy to reference the hub.
    var chat = $.connection.chatHub;

    // text, notificationType, notificationid,notificationLink
    chat.client.receiveNotification = function (text, notificationType, notificationId, notificationLink) {
        console.log('message received');
        var htmlcode = '<div class="fullNotificationContainer" style = "background-color: rgba(255, 255, 204,0.5);">\
                <div class="nMessagesPicture nNotificationPicture nRequestsRequestPic" style="background-image: url(http://localhost:6752/Images/Icons/Notifications/' + notificationType + '-24.png);">\
                </div>\
                <div class="nMessagesInsideDiv">\
                    <div class="nNotificationBody">\
                        <span>' + text + '</span>\
                    </div>\
                    <div class="nNotificationX">\
                        x\
                        <div class="invisible">' + notificationId + '</div>\
                    </div>\
                    <div class="MessageListDate">\
                        <div class="nSmallIcon nBlogClock nMessagesClock"></div>\
                        <span >right now</span>\
                    </div>\
                </div>\
                <div class="clear"></div>\
                <hr class="nMessagesHr nNotificationHr">\
                <div class="invisible">\
                    <span id="ContentPlaceHolder1_RepeaterNotifications_LabelImage_0" class="nNotificationImageUrl">Images/Icons/Notifications/4-24.png</span>\
                    <span id="ContentPlaceHolder1_RepeaterNotifications_LabelUnread_0" class="nNotificationUnread">True</span>\
                    <div class="ui-btn ui-input-btn ui-corner-all ui-shadow"><input type="submit" name="ctl00$ContentPlaceHolder1$RepeaterNotifications$ctl00$ButtonDelete" value="" id="ContentPlaceHolder1_RepeaterNotifications_ButtonDelete_0"></div>\
                    <div class="nNotificationLink">\
                        <input type="hidden" name="ctl00$ContentPlaceHolder1$RepeaterNotifications$ctl00$HiddenFieldLink" id="ContentPlaceHolder1_RepeaterNotifications_HiddenFieldLink_0" value="'+ notificationLink + '">\
                    </div>\
                </div>\
            </div>';
        $('.nNotificationsAllContainer').prepend(htmlcode);
    };

    $.connection.hub.start().done(function () {
        $('body').on('click', '.nNotificationX', function () {
            var userId = parseInt($('#HiddenFieldUserId').val());
            event.preventDefault()
            //var url = getRootUrl() + "Notifications/Delete/" + $(this).find('div').text();
            //console.log(url);
            var notificationId = parseInt($(this).find('div').text());
            //$target.hide('slow');
            $(this).parent().parent().hide('slow');
            //console.log('userID, notificationID' + userId + ' ' + notificationId);
            chat.server.deleteNotification(userId, notificationId).done(function (result) {
                if (result) {
                    console.log('it should be deleted!')
                    //deletig the notification
                    $(this).parent().parent().html('');
                }
            })
            return false;
        })
    })
    //displaying errors
    var errorStatus = $('#HiddenFieldLoginStatus').val();
    if (errorStatus == '0') {
        $('.nDoneSmiley').removeClass('invisible');
        $('.nDoneMessage').removeClass('invisible');
    }

    $('.nMainPanelDiv').addClass('nfullHeight');
    //setting size of notification row
    function messageResize() {
        /*var width = $(window).width() - $('.nMessagesPicture').width() - $('.nMessagesNumberContainer').width() - 50;
        $('.nMessagesInsideDiv').width(width);*/
    }
    messageResize();
    $(window).resize(function () {
        messageResize();
    });

    /*$('.nNotificationX').click(function () {
        event.preventDefault()
        var url = getRootUrl() + "Notifications/Delete/" + $(this).find('div').text();
        console.log(url);
        window.location.href = url;
        return false;
    });*/
    //click on notifications
    $('body').on('click', '.fullNotificationContainer', function () {
        var href = $(this).find('.nNotificationLink').find('input').val();
        console.log('clickkkkkkkkkked ' + href);

        if (href != "0") {
            window.location.href = href;
        }
    });
    //setting background and show number
    $('.fullNotificationContainer').each(function () {
        var isMessageNew = $(this).find('.nNotificationUnread').text();
        //console.log(isMessageNew);
        if (isMessageNew == "True") {
            $(this).css("background-color", "rgba(255,255,204,0.5)");
            $(this).find('.nMessagesNumberContainer').removeClass("hidden");
        }
        //setting pictures
        //Images/Icons/notifications/{TypeId}-24.png
        var bg = "url('" + $(this).find('.nNotificationImageUrl').text() + "')";
        $(this).find('.nMessagesPicture').css('background-image', bg);
    });
}
function FeedbackPage() {
    nFooterShortClass = "nFooterShort30";
    screenShortParam = 30; //em
    //prevent Enter Key
    $(window).keydown(function (event) {
        if (event.keyCode == 13 & !$("#ContentPlaceHolder1_TextBoxFeedback").is(":focus")) {
            event.preventDefault();
            return false;
        }
    });

    var rate = '5';
    $('#ContentPlaceHolder1_HiddenFieldRate').val('5');
    $('.nFBEachOuterContainer').click(function () {
        $('.nFBEachOuterContainer').removeClass('nFBEachOuterContainerSelected');
        $(this).addClass('nFBEachOuterContainerSelected');
        console.log($(this).attr('data-value'));
        $('#ContentPlaceHolder1_HiddenFieldRate').val($(this).attr('data-value'));
    });
    $('.nFooterButton').click(function () {
        $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
    });
}
function followingPage() {
    var frontUserId = $('#HiddenFieldUserId').val();
    function setFollowingValues(result) {
        /*
        public Int64 EventId { get; private set; }
        public string EventName { get; private set; }
        public string RemainedTime { get; private set; }
        public Int16 TypeId { get; private set; }
        public Int16 CoverId { get; private set; }
        public int UserId { get; private set; }
        public string Location { get; private set; }
        public int UserRate { get; private set; }
        public string OwnerName { get; private set; }
        public int Participants { get; private set; }
        public int ParticipantsAccepted { get; private set; }
        public string ProfilePicUrl { get; private set; }
        */
        console.log("result.EventId " + result);
        var htmlCode = '<div class="nContentCover nNearbyCover" id="cover-' + result.EventId + '" data-eventId="' + result.EventId + '"=>\
                    <div class="nContentCoverTransparent">\
                        <div class="nContentTitle">\
                            <b><span class="nNearbyEventName" data-eventId="'+ result.EventId + '">' + result.EventName + '</span>\
                                <!-- </b><span class="nEVBy">by</span><span class="nEVEventName">' + result.OwnerName + '</span> -->\
                        </div>\
                        <div class="nTimeAndLocation nNearbyTimeAndLocation">\
                            <div class="nExploreTime"><i class="fa fa-clock-o"></i>&nbsp <span class="nEVdate">'+ result.RemainedTime + '</span> </div>\
                            <!--<div class="nExploreLocation">\
                                <i class="fa fa-map-marker"></i>&nbsp<span >'+ result.Location + '</span>\
                            </div>-->\
                        </div>\
                        <div class="clear"></div>\
                        <span class="nEVBy">by</span>\
                        <span class="nEVEventName">' + result.OwnerName + '</span>\
                        <div class="nFollowingLocation">\
                            <i class="fa fa-map-marker"></i>&nbsp<span >'+ result.Location + '</span>\
                        </div>\
                        <div class="clear"></div>\
                        <div class="nEventCoverBox">\
                            <div class="nRoundPicture nActivityPicture">\
                            </div>\
                        </div>\
                        <div class="nEventCoverBox">\
                            <div class="nRoundPicture nEVOwnerPicture" data-ownerId="' + result.UserId + '">\
                                <canvas id="nOwnerCanvas-'+ result.EventId + '"class="nRequestCanvas" width="115" height="115"></canvas>\
                            </div>\
                        </div>\
                        <div class="nEventCoverBox">\
                            <div class="nRoundPicture nParticipantNumberPicture">\
                                <div class="nParticipantNumberPictureTop">\
                                    ' + result.ParticipantsAccepted + "/" + result.Participants + '\
                                    <canvas id="'+ result.EventId + '"class="nFeedParticipantsCanvas" width="115" height="115"></canvas>\
                                </div>\
                                <div class="nParticipantNumberPictureBot">\
                                </div>\
                            </div>\
                        </div>\
                    <div class="nNearbyButton" data-userId="' + result.UserId + '" data-eventId="' + result.EventId + ' "=>SEND REQUEST</div>\
                    </div>\
                </div>';
        $('.nNearbyEventsContainer').append(htmlCode);
        //setting the profile pic
        var selector = ".nContentCover" + "[data-eventid='" + result.EventId + "']";
        console.log("selector " + selector);
        //$(selector).html("yeps");
        var container = $(selector);
        //nEVOwnerPicture
        var urlString = "url('" + getRootUrl() + result.ProfilePicUrl + "')";
        container.find('.nEVOwnerPicture').css('background-image', urlString);
        //setting the profile page click
        //setting the cover photo
        var urlString = "url('" + getRootUrl() + "Images/Covers/" + result.CoverId + ".jpg')";
        container.css('background-image', urlString);
        //setting the activity icon
        var urlString = "url('" + getRootUrl() + "Images/Icons/activity" + result.TypeId + ".png')";
        container.find('.nActivityPicture').css('background-image', urlString);
        //running the animations
        //participants animation
        //canvasId,text,taken, all, radius,width,firstColor,secondColor (all the sizes are in em)
        explore(result.EventId, false, result.ParticipantsAccepted, result.Participants, 2.05, 0.2, "rgb(52,108,15)", null);
        //owner rating animation
        explore("nOwnerCanvas-" + result.EventId, false, result.UserRate, 100, 2.3, 0.3, "rgb(215,67,46)", null);
        //function of the " send request " button
    }

    var chat = $.connection.chatHub;
    $.connection.hub.start().done(function () {
        var latestEventId = 0;
        function getTheNearbyEvents() {
            eventId = latestEventId;
            chat.server.getFollowingEvents(parseInt(frontUserId), parseInt(eventId)).done(function (result) {
                console.log('result received.' + result);
                var output = '';
                var eachRequestEventCounter = 0;
                var counter = 0;
                for (var property in result) {
                    output += property + ': ' + result[property] + '; ';
                    var eachObject = result[property];
                    if (eachObject && eachObject !== "null" && eachObject !== "undefined" && eachObject.EventId != "0") {
                        eachRequestEventCounter++;

                        counter++;
                        setFollowingValues(eachObject);
                        latestEventId = eachObject.EventId;
                        if (counter == 1) {
                            counter = latestEventId;
                        }
                        console.log(latestEventId);
                        for (var property in eachObject) {
                            output += property + ': ' + eachObject[property] + '; ';
                        }
                    }
                }
                //hiding more button if there are less than 5 responses from the server
                if (eachRequestEventCounter < 5) {
                    $('.nNearbyEventsContainerFooterButton').parent().addClass('invisible');
                }
                else {
                    $('.nNearbyEventsContainerFooterButton').parent().removeClass('invisible');
                }
                //alert(output);
                //displaying the error
                if (counter == 0) {
                    setDoneValues("following", 0, 0);
                    $('.pageErrorContent').removeClass("invisible");
                    $('.nNearbyEventsContainer').addClass("invisible");
                } else {
                    $('.pageErrorContent').addClass("invisible");
                    $('.nNearbyEventsContainer').removeClass("invisible");
                }

            })
        }
        getTheNearbyEvents();
        $('.nNearbyTimeButtons').click(function () {
            if (parseInt($(this).attr('data-mode')) != mode) {
                $('.nNearbyEventsContainer').html("");
                $('.nNearbyTimeButtons').removeClass('nNearbyTimeButtonsSelected');
                $(this).addClass('nNearbyTimeButtonsSelected');
                mode = parseInt($(this).attr('data-mode'));
                console.log(mode);
                latestEventId = 0;
                getTheNearbyEvents();
            }
        })
        $('.nNearbyEventsContainerFooterButton').click(function () {
            getTheNearbyEvents();
            var href = "#cover-" + counter;
            $('html, body').animate({
                scrollTop: $(href).offset().top
            }, 500);
        })

        $(document).on("click", ".nNearbyButton", function () {
            console.log("clicked");
            console.log("variables " + parseInt(frontUserId), parseInt($(this).attr("data-EventId")));
            chat.server.sendRequest(parseInt(frontUserId), parseInt($(this).attr("data-EventId"))).done(function (result) {
                console.log("result console");
            })
            $(this).parent().parent().parent().hide('slow');
        });
    })


    //displaying the error

    //needs to be changed **************
    if ($('#ContentPlaceHolder1_HiddenFieldStatus').val() == '0') {
        setDoneValues("following", 0, 0);
        //$('.nExploreFooter').addClass('invisible');
    } else {
        $('.pageNormalContent').removeClass('invisible');
    }
}
function nearbyPage() {

    $(document).on("click", ".nNearbyEventName", function () {
        var url = getRootUrl() + "Events/" + $(this).attr("data-EventId");
        console.log(url);
        location.href = url;
    });

    //clicking on change location
    $('.nSearchLocation').click(function () {
        var url = getRootUrl() + "Settings/Location";
        location.href = url;
    })

    //initiating the values;
    //0 by default, id of the latest event
    var eventId = 0;
    //mode 1 all 2 day 3 week 4 month
    var mode = 1;
    var userId = parseInt($('#HiddenFieldUserId').val());
    //pressing the times buttons

    $(document).on("click", ".nEVOwnerPicture", function () {
        var url = getRootUrl() + "Profile/" + $(this).attr('data-ownerId');
        window.location.href = url;
    });


    function setNearbyValues(result) {
        /*
        public Int64 EventId { get; private set; }
        public string EventName { get; private set; }
        public string RemainedTime { get; private set; }
        public Int16 TypeId { get; private set; }
        public Int16 CoverId { get; private set; }
        public int UserId { get; private set; }
        public string Location { get; private set; }
        public int UserRate { get; private set; }
        public string OwnerName { get; private set; }
        public int Participants { get; private set; }
        public int ParticipantsAccepted { get; private set; }
        public string ProfilePicUrl { get; private set; }
        */
        console.log("result.EventId " + result);
        var htmlCode = '<div class="nContentCover nNearbyCover" id="cover-' + result.EventId + '" data-eventId="' + result.EventId + '"=>\
                    <div class="nContentCoverTransparent">\
                        <div class="nContentTitle">\
                            <b><span class="nNearbyEventName" data-eventId="'+ result.EventId + '">' + result.EventName + '</span>\
                                <!-- </b><span class="nEVBy">by</span><span class="nEVEventName">' + result.OwnerName + '</span> -->\
                        </div>\
                        <div class="clear"></div>\
                        <span class="nEVBy">by</span>\
                        <span class="nEVEventName">' + result.OwnerName + '</span>\
                        <div class="nTimeAndLocation nNearbyTimeAndLocation">\
                            <div class="nExploreTime"><i class="fa fa-clock-o"></i>&nbsp <span class="nEVdate">'+ result.RemainedTime + '</span> </div>\
                            <!--<div class="nExploreLocation">\
                                <i class="fa fa-map-marker"></i>&nbsp<span >'+ result.Location + '</span>\
                            </div>-->\
                        </div>\
                        <div class="clear"></div>\
                        <div class="nEventCoverBox">\
                            <div class="nRoundPicture nActivityPicture">\
                            </div>\
                        </div>\
                        <div class="nEventCoverBox">\
                            <div class="nRoundPicture nEVOwnerPicture" data-ownerId="' + result.UserId + '">\
                                <canvas id="nOwnerCanvas-'+ result.EventId + '"class="nRequestCanvas" width="115" height="115"></canvas>\
                            </div>\
                        </div>\
                        <div class="nEventCoverBox">\
                            <div class="nRoundPicture nParticipantNumberPicture">\
                                <div class="nParticipantNumberPictureTop">\
                                    ' + result.ParticipantsAccepted + "/" + result.Participants + '\
                                    <canvas id="'+ result.EventId + '"class="nFeedParticipantsCanvas" width="115" height="115"></canvas>\
                                </div>\
                                <div class="nParticipantNumberPictureBot">\
                                </div>\
                            </div>\
                        </div>\
                    <div class="nNearbyButton" data-userId="' + result.UserId + '" data-eventId="' + result.EventId + ' "=>SEND REQUEST</div>\
                    </div>\
                </div>';
        $('.nNearbyEventsContainer').append(htmlCode);
        //setting the profile pic
        var selector = ".nContentCover" + "[data-eventid='" + result.EventId + "']";
        console.log("selector " + selector);
        //$(selector).html("yeps");
        var container = $(selector);
        //nEVOwnerPicture
        var urlString = "url('" + getRootUrl() + result.ProfilePicUrl + "')";
        container.find('.nEVOwnerPicture').css('background-image', urlString);
        //setting the profile page click
        //setting the cover photo
        var urlString = "url('" + getRootUrl() + "Images/Covers/" + result.CoverId + ".jpg')";
        container.css('background-image', urlString);
        //setting the activity icon
        var urlString = "url('" + getRootUrl() + "Images/Icons/activity" + result.TypeId + ".png')";
        container.find('.nActivityPicture').css('background-image', urlString);
        //running the animations
        //participants animation
        //canvasId,text,taken, all, radius,width,firstColor,secondColor (all the sizes are in em)
        explore(result.EventId, false, result.ParticipantsAccepted, result.Participants, 2.05, 0.2, "rgb(52,108,15)", null);
        //owner rating animation
        explore("nOwnerCanvas-" + result.EventId, false, result.UserRate, 100, 2.2, 0.3, "rgb(215,67,46)", null);
        //function of the " send request " button

    }
    var chat = $.connection.chatHub;
    $.connection.hub.start().done(function () {
        var latestEventId = 0;
        function getTheNearbyEvents() {
            eventId = latestEventId;
            chat.server.getNearbyEvents(parseInt(userId), parseInt(eventId), parseInt(mode)).done(function (result) {
                console.log('result received.' + result);
                var output = '';
                var eachRequestEventCounter = 0;
                var counter = 0;
                for (var property in result) {
                    output += property + ': ' + result[property] + '; ';
                    var eachObject = result[property];
                    if (eachObject && eachObject !== "null" && eachObject !== "undefined" && eachObject.EventId != "0") {
                        eachRequestEventCounter++;

                        counter++;
                        setNearbyValues(eachObject);
                        latestEventId = eachObject.EventId;
                        if (counter == 1) {
                            counter = latestEventId;
                        }
                        console.log(latestEventId);
                        for (var property in eachObject) {
                            output += property + ': ' + eachObject[property] + '; ';
                        }
                    }
                }
                //hiding more button if there are less than 5 responses from the server
                if (eachRequestEventCounter < 5) {
                    $('.nNearbyEventsContainerFooterButton').parent().addClass('invisible');
                }
                else {
                    $('.nNearbyEventsContainerFooterButton').parent().removeClass('invisible');
                }
            })

        }
        getTheNearbyEvents();
        $('.nNearbyTimeButtons').click(function () {

            if (parseInt($(this).attr('data-mode')) != mode) {
                $('.nNearbyEventsContainer').html("");
                $('.nNearbyTimeButtons').removeClass('nNearbyTimeButtonsSelected');
                $(this).addClass('nNearbyTimeButtonsSelected');
                mode = parseInt($(this).attr('data-mode'));
                console.log(mode);
                latestEventId = 0;
                getTheNearbyEvents();

            }
        })
        $('.nNearbyEventsContainerFooterButton').click(function () {
            getTheNearbyEvents();
            var href = "#cover-" + counter;
            $('html, body').animate({
                scrollTop: $(href).offset().top
            }, 500);
        })
        $(document).on("click", ".nNearbyButton", function () {
            console.log("clicked");
            console.log("variables " + parseInt(userId), parseInt($(this).attr("data-EventId")));
            chat.server.sendRequest(parseInt(userId), parseInt($(this).attr("data-EventId"))).done(function (result) {
                console.log("result console");
            })
            $(this).parent().parent().parent().hide('slow');
        });
    })


    //displaying the error
    setDoneValues();
    if ($('#ContentPlaceHolder1_HiddenFieldStatus').val() == '0') {
        setDoneValues("explore", 0, 0);
        //$('.nExploreFooter').addClass('invisible');
    } else {
        $('.pageNormalContent').removeClass('invisible');
    }

    //setting the mode of the tab
    //ContentPlaceHolder1_HiddenFieldMode: city, following
    var selectedTab = 0;
    if ($('#ContentPlaceHolder1_HiddenFieldMode').val() == 'city') {
        selectedTab = 0;
    } else if ($('#ContentPlaceHolder1_HiddenFieldMode').val() == 'following') {
        selectedTab = 1;
    }
    //displaying the following section
    $('.nNearbyFollowingButton').click(function () {
        var url = getRootUrl() + 'Nearby/Following';
        window.location.href = url;
    })
    $('.nNearbyCityButton').click(function () {
        var url = getRootUrl() + 'Nearby';
        window.location.href = url;
    })

    $('.nMainPanelDiv').addClass('nfullHeight');
    //url of events
    $('.nFeedCover').click(function () {
        var url = getRootUrl() + "events/" + $(this).find('.nFeedEventId').text();
        window.location.href = url;
    });

    //drawing profile graphs
    $('.nContentCover').each(function () {
        //profile graph
        //setting the id of canvas
        var requestID = $(this).find('.nFeedEventId').text();
        var requestRate = $(this).find('.nFeedUserRate').text();
        var feedOwnerCanvasID = "canvas_" + requestID;
        $(this).find('.nRequestCanvas').attr('id', feedOwnerCanvasID);
        //canvasId,text,taken, all, radius,width,firstColor,secondColor (all the sizes are in em)
        explore(feedOwnerCanvasID, false, requestRate, 100, 2.2, 0.3, "rgb(215,67,46)", null);

        //participants graph
        var participants = $(this).find('.nFeedParticipants').text();
        var participantsAvailable = $(this).find('.nFeedParticipantsAvailable').text();
        var ParticipantsCanvasId = "canvas_participants_" + requestID;
        $(this).find('.nFeedParticipantsCanvas').attr('id', ParticipantsCanvasId);
        //canvasId,text,taken, all, radius,width,firstColor,secondColor (all the sizes are in em)
        console.log('participants ' + participants);
        console.log('participantsAvailable' + participantsAvailable);
        explore(ParticipantsCanvasId, false, participantsAvailable, participants, 2.05, 0.2, "rgb(52,108,15)", null);
        //explore("EVcanvas", false, participants, all, 2.05, 0.2, "rgb(52,108,15)", null);
    });

    tabbedContent(selectedTab);

    //setting cover
    $('.nContentCover').each(function () {
        var coverId = $(this).find('.nFeedCoverId').text();
        console.log("pekhe " + coverId);
        $(this).css('background-image', 'url(../../Images/Covers/' + coverId + '.jpg)');
    });

    //setting feed image
    $('.nEVOwnerPicture').each(function () {
        var bg = "url('../../" + $(this).find('.invisible').text().substr(1) + "')";
        console.log(bg);
        $(this).css('background-image', bg);
    });

    //setting activity image
    $('.nActivityPicture').each(function () {
        var bg = "url('../../Images/Icons/activity" + $(this).find('.invisible').text() + ".png')";
        console.log(bg);
        $(this).css('background-image', bg);
    });
}

function requestsPage() {

    var displayError = $('#ContentPlaceHolder1_HiddenFieldRequestsStatus').val();
    console.log("displayError: " + displayError);
    if (displayError == '0') {
        setDoneValues("requests0", 0, 0);
    } else if (displayError == '2') {
        setDoneValues("requests2", 0, 0);
    }
    else if (displayError == '1') {
        $('.pageNormalContent').removeClass('invisible');
    }

    //each notification click
    $('.fullNotificationContainer').click(function () {
        //~/Requests/EventId
        var url = getRootUrl() + 'Requests/' + $(this).find('.nNotificationLink').text();
        console.log(url);
        window.location.href = url;
    });


    //setting background and show number
    $('.fullNotificationContainer').each(function () {

        //setting pictures
        //Images/Icons/notifications/{TypeId}-24.png
        var bg = "url('" + $(this).find('.nNotificationImageUrl').text() + "')";
        $(this).find('.nMessagesPicture').css('background-image', bg);
    });

    $('.nMainPanelDiv').addClass('nfullHeight');
    //setting image links
    $(".nRequestPicture").click(function () {
        console.log($(this).parent().parent().find('.requestSenderId').text());
        var url = getRootUrl() + "profile/" + $(this).parent().parent().find('.requestSenderId').text();
        window.location.href = url;
    })

    tabbedContent();
    //setting the graphs
    //setting the image of each request
    $('.nRequestPicture').each(function () {
        var bgURL = "url('" + $(this).find('.nRequestProfilePic').text().substring(2) + "')";
        console.log(bgURL);
        $(this).css('background-image', bgURL);
    });
    $('.nMessagesNumberContainer').each(function () {
        if ($(this).find('.nMessagesUnreadNumber').find('span').text() == '0') {
            $(this).addClass('invisible');
        }
        //setting the id of canvas
        /*var requestID = $(this).find('.LabelRequestId').text();
        var requestRate = $(this).find('.LabelRequestRate').text();
        requestID = "canvas_" + requestID;
        $(this).find('.nRequestCanvas').attr('id', requestID);
        //canvasId,text,taken, all, radius,width,firstColor,secondColor (all the sizes are in em)
        explore(requestID, false, requestRate, 100, 2.2, 0.3, "rgb(215,67,46)", null);

        var myText = $(this).find('.nRequestMessage span').text();
        if (!myText) {
            $(this).find('.nRequestMessage').css('background-color', 'transparent');
        }
        */
    });

    //nRequestRemainedTime
    //setting button links
    /*
    Requests/{RequestId}/{ActionCode}
    action code 1 for accept
    action code 2 for reject
    */
    $(".nNotButton").click(function () {
        var id = $(this).parent().parent().parent().find('.invisible').find('.LabelRequestId').text();
        var url = "requests/" + id + "/2";
        window.location.href = url;
    })
    $(".nRequestButton").click(function () {
        var id = $(this).parent().parent().parent().find('.invisible').find('.LabelRequestId').text();
        var url = "requests/" + id + "/1";
        window.location.href = url;
    })
}

function messagesShowPage() {
    //try to get the cookie
    //var vc = $.cookie("VC");
    //vc = vc.substring(3);
    //signalR
    var userId = parseInt($('#HiddenFieldUserId').val());
    var targetId = parseInt($('#ContentPlaceHolder1_HiddenFieldOtherId').val());
    var messageId = 0;
    console.log('userId + targetId' + userId + ' ' + targetId);
    var chat = $.connection.chatHub;
    var tenMessagesHtmlCode = "";
    var moreMessagesLoadable = false;
    var itIsFirstTime = true;
    var firstMessageId = 0;

    // function to receive and show the message if the user is in the chat page with the sender
    chat.client.receiveMessage = function (sender, message) {
        // display the message
        if ($('#ContentPlaceHolder1_HiddenFieldOtherId').val() == sender) {
            console.log('message received' + message);
            var htmlcode = '<div class="eachMessage"> \
                    <div class="nMessagesPicture nMessagesShowPicture" style="background-image: url('+ getRootUrl() + 'Files/ProfilesPhotos/' + sender + '-100.jpg);"> \
                        <div class="nMessagesUrl invisible">'+ getRootUrl() + '/Profile/' + sender + '</div> \
                    </div> \
                    <div> \
                    </div> \
                    <div class="nMessageBody"> \
                        <span>'+ message + '</span> \
                        <div class="nMessageDecoration"></div> \
                    </div> \
                    <!--<span id="ContentPlaceHolder1_RepeaterMessages_LabelUnread_16" class="EachMessageUnread ViewClass">1</span> --> \
                    <div class="invisible"> \
                        <span id="ContentPlaceHolder1_RepeaterMessages_LabelSender_16" class="EachMessageSender">False</span> \
                    </div> \
                    <div class="clear"></div> \
                    <div class="MessageListDate nMSDate"> \
                        <span id="ContentPlaceHolder1_RepeaterMessages_LabelDate_16" class="EachMessageDate ViewClass"> . </span> \
                    </div> \
                </div>\
                <div class="clear"></div>';
            $('.nMSMessagesContainer').append(htmlcode);
            $(".nMSMessagesContainer").animate({ scrollTop: $('.nMSMessagesContainer')[0].scrollHeight }, 1000);
        }
    };
    function sendButtonClicked() {
        var htmlcode = '<div class="eachMessage nRight">\
                    <div class="nMessagesPicture nMessagesShowPicture nMSOtherSenderPicture" style="background-image: url('+ getRootUrl() + 'Files/ProfilesPhotos/' + userId + '-100.jpg);">\
                        <div class="nMessagesUrl invisible">'+ userId + '</div>\
                        <input type="hidden" name="ctl00$ContentPlaceHolder1$RepeaterMessages$ctl39$HiddenFieldProfilePicUrl" id="ContentPlaceHolder1_RepeaterMessages_HiddenFieldProfilePicUrl_39" value="~/Files/ProfilesPhotos/9-100.jpg">\
                        <input type="hidden" name="ctl00$ContentPlaceHolder1$RepeaterMessages$ctl39$HiddenFieldUserId" id="ContentPlaceHolder1_RepeaterMessages_HiddenFieldUserId_39" value="9">\
                    </div>\
                    <div>\
                    </div>\
                    <div class="nMessageBody nMessageBodyOther">\
                        <span>' + $('.nMSTextArea textarea').val() + '</span>\
                        <div class="nMessageDecoration nMessageDecorationOther"></div>\
                    </div>\
                    <!--<span id="ContentPlaceHolder1_RepeaterMessages_LabelUnread_39" class="EachMessageUnread ViewClass">0</span> -->\
                    <div class="invisible">\
                        <span id="ContentPlaceHolder1_RepeaterMessages_LabelSender_39" class="EachMessageSender">True</span>\
                    </div>\
                    <div class="clear"></div>\
                    <div class="MessageListDate nMSDate MessageListDateOther">\
                        <span id="ContentPlaceHolder1_RepeaterMessages_LabelDate_39" class="EachMessageDate ViewClass">.</span>\
                    </div>\
                </div>';
        $('.nMSMessagesContainer').append(htmlcode);
        //$('.nMSMessagesContainer').scrollTop($('.nMSMessagesContainer').prop("scrollHeight"));
        $(".nMSMessagesContainer").animate({ scrollTop: $('.nMSMessagesContainer')[0].scrollHeight }, 1000);
        $('.nMSTextArea textarea').val('');
        $('.nMSTextArea textarea').focus();
    };


    function addMessageToTen(message, userId, targetId) {
        if (message.Message == "") {
            return;
        }
        /*Int64 MessageId ,bool Sender ,string Message ,string PassedDate ,bool Unread ,int UserId*/
        //console.log("userId " + userId);
        //console.log("targetId " + targetId);
        var photoUrl = "";
        var eachMessageClass = "";
        var messageBodyClass = "";
        var messageListDateClass = "";
        var senderPictureClass = "";
        var decorationClass = "";
        var senderPictureClass = "";
        var listDateClass = "";
        var firstMessageIdCode = "";
        if (tenMessagesHtmlCode == "") {
            firstMessageIdCode = 'id="message-' + message.MessageId + '"';
        }
        if (message.Sender) {
            //user is sender (blue)
            photoUrl = $('#HiddenFieldPhotoUrl').val();
            eachMessageClass = "nRight";
            messageBodyClass = "nMessageBodyOther";
            messageListDateClass = "MessageListDateOther";
            senderPictureClass = "nMSOtherSenderPicture";
            decorationClass = "nMessageDecorationOther";
            senderPictureclass = "nMSOtherSenderPicture";
        } else {
            //user is not sender (green)
            photoUrl = $('#ContentPlaceHolder1_HiddenFieldOtherPhotoUrl').val();
        }

        var messageHTMLCode = ' <div ' + firstMessageIdCode + ' class="eachMessage ' + eachMessageClass + '">\
                                    <div class="nMessagesPicture nEVMessagesPicture nMessagesShowPicture ' + senderPictureClass + '" style="background-image: url(' + getRootUrl() + photoUrl + ');">\
                                    </div>\
                                    <div class="nMessageBody ' + messageBodyClass + '">\
                                        <span>'+ message.Message + '</span>\
                                        <div class="nMessageDecoration ' + decorationClass + '"></div>\
                                    </div>\
                                    <div class="clear"></div>\
                                    <div class="MessageListDate nMSDate '+ messageListDateClass + '">\
                                        <div class="nSmallIcon nBlogClock nMessagesClock"></div>\
                                        <span class="EachMessageDate ViewClass">' + message.PassedDate + '</span>\
                                    </div>\
                                </div>\
                                <br/>\
                                <div class"clear"></div>';

        tenMessagesHtmlCode += messageHTMLCode;
    }

    $.connection.hub.start().done(function () {
        //get latest 10 messages
        function loadTenMessages() {
            console.log("userId, targetId, firstMessageId" + userId + " " + targetId + " " + firstMessageId);
            chat.server.getTenMessages(userId, targetId, firstMessageId).done(function (result) {

                if (result) {
                    $('.nMSmoreButton').addClass('hidden');
                    firstMessageId = 0;
                    tenMessagesHtmlCode = "";
                    console.log('10 should be changed');

                    for (var property in result) {
                        var eachObject = result[property];
                        console.log(eachObject);
                        if (eachObject && eachObject !== "null" && eachObject !== "undefined") {
                            if (eachObject.MessageId != 0) {
                                console.log("MessageId:" + eachObject.MessageId);
                                if (firstMessageId == 0) { firstMessageId = parseInt(eachObject.MessageId) };
                                addMessageToTen(eachObject, userId, targetId);
                            }
                        }
                    }

                    if (firstMessageId && firstMessageId !== "null" && firstMessageId !== "undefined") {
                        console.log("firstMessageId :" + firstMessageId);
                        //$('.nMSMessagesContainer').prepend(tenMessagesHtmlCode);
                        $('.nMSmoreButton').after(tenMessagesHtmlCode);
                        if (itIsFirstTime) {
                            $(".nMSMessagesContainer").scrollTop($(".nMSMessagesContainer")[0].scrollHeight);
                            itIsFirstTime = false;
                            //moreMessagesLoadable = true;
                        } else {
                            //scroll to the first element
                            var firstElementSeletor = "#message-" + firstMessageId;
                            console.log("offset top:" + $(firstElementSeletor).offset().top);
                            $('.nMSMessagesContainer').animate({
                                scrollTop: $(firstElementSeletor).offset().top + 200
                            }, 500);
                        }

                        //load more messages if there is no scroll
                        if (!scrollbarIsVisible(".nMSMessagesContainer")) {
                            console.log("load more messages");
                            loadTenMessages();
                            moreMessagesLoadable = false;
                        }

                        setTimeout(function () {
                            moreMessagesLoadable = true;
                        }, 500);
                    }
                }
            })
        }
        loadTenMessages();
        setTimeout(function () {
            $('.nMSMessagesContainer').scroll(function () {
                if (moreMessagesLoadable && $('.nMSMessagesContainer').scrollTop() < 20) {
                    $('.nMSmoreButton').removeClass('hidden');
                    //add more messages
                    console.log("load more messages");
                    loadTenMessages();
                    moreMessagesLoadable = false;
                }
            });

        }, 1000);


        $(".nMSSendButton").click(function () {
            //validate to check if textbox is not empty
            if ($('.nMSTextArea textarea').val() != "") {
                //$(".invisible").find('input').trigger("click");
                chat.server.sendMessage(userId, targetId, $('.nMSTextArea textarea').val());
                sendButtonClicked();
            }
        })
        //prevent Enter Key
        $(window).keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                if ($('.nMSTextArea textarea').val() != "") {
                    //$(".invisible").find('input').trigger("click");
                    chat.server.sendMessage(userId, targetId, $('.nMSTextArea textarea').val());
                    sendButtonClicked();
                }
                return false;
            }
        });

        //load messages
    });


    $('.nMainPanelDiv').addClass('zeroMinHeight');
    //owner picture
    $(document).on("click", ".nMessagesPicture", function () {
        console.log("yo");
        var url = getRootUrl() + "Profile/" + $(this).find('.nMessagesUrl').text();
        window.location.href = url;
    });


    // check each message whether it is the sender
    $('.eachMessage').each(function () {
        if ($(this).find('.EachMessageSender').text() == "True") {
            $(this).addClass('nRight');
            $(this).find('.nMessagesPicture').addClass('nMSOtherSenderPicture');
            $(this).find('.nMessageBody').addClass('nMessageBodyOther');
            $(this).find('.nMessageDecoration').addClass('nMessageDecorationOther');
            $(this).find('.MessageListDate').addClass('MessageListDateOther');
        }
        //removing empty messages
        if ($(this).find('.nMessageBody').find('span').text() == "") {
            $(this).addClass('invisible');
        }
    });

    $(".nFooterButton").click(function () {
        window.location.href = "/Messages";
    })
    $('.nMessagesPicture').each(function () {
        var photoUrl = $(this).find('input').val();
        if (typeof photoUrl != 'undefined') {
            photoUrl = photoUrl.substring(1);
            $(this).css('background-image', 'url(' + photoUrl + ')');
        }
    });

    $(".nMSMessagesContainer").scrollTop($(".nMSMessagesContainer")[0].scrollHeight);
}

function blogPage() {
    $('.nMainPanelDiv').addClass('nfullHeight');
    $('.nBlogBlogEach').click(function () {
        console.log("href:" + $(this).find('a').attr('href'));
        var href = getRootUrl() + $(this).find('a').attr('href');
        location.href = href;
    });
}
function blog() {
    nFooterShortClass = "nFooterShort25";
    screenShortParam = 23; //em
    //sharing
    var fbLink;
    var twitterLink;
    fbLink = "http://www.facebook.com/share.php?u=" + document.URL;
    twitterLink = "http://twitter.com/home?status=" + document.URL;
    $('.nEVShareButtonFB').attr('href', fbLink);
    $('.nEVShareButtonTwitter').attr('href', twitterLink);

    //back to blog button
    $(".nProfileMessageButton").click(function () {
        window.location.href = "../../blog";
    });
}

function searchPage() {

    nFooterShortClass = "nFooterShort25";
    screenShortParam = 25; //em


    //clicking on the change location button

    $('.nSearchLocationLink').click(function () {
        location.href = getRootUrl() + 'Settings/Location';
    })
    //clicking on the current location 
    $('.nSearchLocation').click(function () {
        location.href = getRootUrl() + 'Settings/Location';
    })
    //profile pictures of the results
    $('.nProfileFollowingsContainer').each(function () {
        var url = $(this).find('span').html();
        console.log('yo ' + url);
        $(this).find('.nMessagesPicture').css('background-image', 'url(' + getRootUrl() + url + ')');
    });

    //clicking on the people results
    $(document.body).on('click', '.nProfileFollowingsContainer', function () {
        var id = $(this).find('.nSRPeopleId').html();
        var url = getRootUrl() + "Profile/" + id;
        console.log(url);
        location.href = url;
    })

    //icons of the events results
    $('.nEventsNotificationContiner').each(function () {
        var eventID = $(this).find('.nSREventTypeId').html();
        //eventID = 1;
        $(this).find('.nMessagesPicture').css('background-image', 'url(' + getRootUrl() + 'Images/Icons/activity' + eventID + '.png)');
        console.log('eventID ' + eventID);
    });

    //clicking on the event results
    $(document.body).on('click', '.fullNotificationContainer', function () {
        var eventID = $(this).find('.nSREventId').html();
        var url = getRootUrl() + "Events/" + eventID;
        console.log(url);
        location.href = url;
    })
    //setting locations value
    $('.nSearchLocationSpan').html($('#ContentPlaceHolder1_LabelLocation').html());

    //setting search type id for the back
    $('.nTabs').click(function () {
        var tabID = $(this).attr('ntabnumber');
        //HiddenFieldSearchType 1: username 2:hashtag 3:filter
        switch (tabID) {
            //HiddenFieldSearchType 1: username 2:hashtag 3:filter
            //username
            case "1":
                console.log('1111');
                $('#ContentPlaceHolder1_HiddenFieldSearchType').val('2');
                break;
                //hashtag
            case "2":
                console.log('2222');
                $('#ContentPlaceHolder1_HiddenFieldSearchType').val('3');
                break;
                //hashtag
            case "3":
                console.log('333');
                $('#ContentPlaceHolder1_HiddenFieldSearchType').val('1');

                break;
            default: break;
        }
        $('#ContentPlaceHolder1_HiddenFieldSearchType').val();
    });


    $('.nMainPanelDiv').addClass('nfullHeight');
    //setting records to the correct place
    var searchID = $('#ContentPlaceHolder1_HiddenFieldSearchType').val();
    var searchResult = $('.nSearchResults').html();
    var errorDiv = $('.nSearchErrorContainer').html();
    var searchStatus = $('#ContentPlaceHolder1_HiddenFieldSearchStatus').val();
    //searchStatus = '0';
    var tabVar;
    switch (searchID) {
        //HiddenFieldSearchType 1: username 2:hashtag 3:filter
        case "1":
            console.log('1111');
            if (searchStatus == '0') {
                $('.nSearchResultsPeople').html(errorDiv);
            } else {
                $('.nSearchResultsPeople').html(searchResult);
            }
            tabVar = 2;
            tabbedContent(tabVar);
            break;
        case "2":
            console.log('2222');
            if (searchStatus == '0') {
                $('.nSearchResultsTag').html(errorDiv);
            } else {
                $('.nSearchResultsTag').html(searchResult);
            }
            tabVar = 0;
            tabbedContent(tabVar);
            break;
        case "3":
            console.log('333');
            if (searchStatus == '0') {
                $('.nSearchResultsFilter').html(errorDiv);
            } else {
                $('.nSearchResultsFilter').html(searchResult);
            }
            tabVar = 1;
            tabbedContent(tabVar);
            break;
        default: tabbedContent();
    }
    console.log('fcknig searchrt2 ' + $('.nSearchResultsTag').html());
    var activity;
    var activityID = $('#ContentPlaceHolder1_HiddenFieldTypeId').val();



    //setting activity
    var selector = ".nSearchLogo[activity-id=" + activityID + "]";
    $(selector).css("background-color", "rgb(255,187,5)");

    //setting size of search row
    function searchResize() {
        var width = $('.nDescriptionContent').width() - $('.nSearchButton').width() - 15;
        $('.nSearchSearchBox').width(width);
    }
    searchResize();
    $(window).resize(function () {
        searchResize();
    });

    $(".nSearchButton").click(function () {
        $("#ButtonSearch").trigger("click");
    });
    $(".nSearchLogo").click(function () {
        $(".nSearchLogo").css("background-color", "rgb(222,222,222)");
        activity = $(this).attr("data");
        $(this).css("background-color", "rgb(255,187,5)");
        activity = $(this).attr("data");
        activityID = $(this).attr("activity-id");
        //$('.nEAACtivityType').text(activity);
        $('#ContentPlaceHolder1_HiddenFieldTypeId').val(activityID);
    })
    $('.nSearchButton').click(function () {
        $('#ContentPlaceHolder1_ButtonSearch').trigger('click');
    });
}

//error page
function errorPage() {
    var url = location.pathname;
    var urlLastPart = url.substr(url.lastIndexOf('/') + 1).toString().toLowerCase();
    console.log("urlLastPart  " + urlLastPart);
    setDoneValues(urlLastPart, 0, 0);
    function loginResize() {
        //taking care of middle window size
        if ($(window).width() > 301) {
            $(".nMiddleWindow").width('300px');
        } else {
            $(".nMiddleWindow").width('90%');
        }
        //error image size
        logoWidth = $(".nErrorImage").width();
        $(".nErrorImage").css("height", logoWidth);
    }

    loginResize();
    $(window).resize(function () {
        loginResize();
    });

    //Write a message button
    $(".nProfileMessageButton").click(function () {
        var url = getRootUrl() + "explore";
        window.location.href = url;
    });
}

//messages
function messagesPage() {
    //there is not messages to display
    if ($('#ContentPlaceHolder1_HiddenFieldStatus').val() == '0') {
        $('.nDoneSmiley, .nDoneMessage').removeClass('invisible');
    }

    //deleting messages
    //ContentPlaceHolder1_ButtonDelete
    $('.nMessagesX').click(function () {
        console.log($(this).find('input').val());
        var messageId = $(this).find('input').val();
        $('#ContentPlaceHolder1_HiddenFieldMessageListIdDelete').val(messageId);
        $('#ContentPlaceHolder1_ButtonDelete').trigger('click');

        event.preventDefault();
        return false;
    });
    //setting size of message row
    /*function messageResize() {
        var width = $(window).width() - $('.nMessagesPicture').width() - $('.nMessagesNumberContainer').width() - 50;
        console.log("*" + width);
        $('.nMessagesInsideDiv').width(width);
    }
    messageResize();
    $(window).resize(function () {
        messageResize();
    });
    */
    $('.nMessagesPicture').click(function () {
        event.preventDefault();
        var href = $('.fullNotificationContainer').find('a').attr('href');
        var id = href.substring(href.lastIndexOf('/') + 1);
        var url = getRootUrl() + "Profile/" + id;
        window.location.href = url;
        return false;
    });
    $('.fullNotificationContainer').click(function () {
        var href = $('.fullNotificationContainer').find('a').attr('href');
        window.location.href = href
    });
    $('.fullNotificationContainer').each(function () {
        //setting background and show number
        var isMessageNew = $(this).find('.nMessagesNewBool').text();
        if (isMessageNew == "True") {
            $(this).css("background-color", "rgb(255,255,204)");
            $(this).find('.nMessagesNumberContainer').removeClass("hidden");
        }
    });
    $('.nMessagesPicture').each(function () {
        var photoUrl = $(this).find('input').val();
        if (typeof photoUrl != 'undefined') {
            photoUrl = photoUrl.substring(1);
            $(this).css('background-image', 'url(' + photoUrl + ')');
        }
    });
}

function loginResize() {
    //taking care of logo size

    var loginTop = $(window).innerHeight() * 30 / 100;
    //$(".nLoginBox").css("top", loginTop);
    if ($(window).width() > 450) {
        $(".nMiddleWindow").width('400px');
        $(".nLoginFooterSecondary").width('400px');
    } else {
        $(".nMiddleWindow").width('90%');
        $(".nLoginFooterSecondary").width('90%');
    }
    $(".loginMainWindow").css("height", $(window).innerHeight());
    if ($(window).height() > 450) {

    }
    else {
        //$(".loginMainWindow").css("height", '450px');
    }
    logoWidth = $(".nLoginLogo").width();

    var logoHeight = logoWidth / 4.2475;
    $(".nLoginLogo").css("height", logoHeight);
}
//displaying server errors in login, register, forgot password
function errorMessage() {
    if ($("#LabelError").text() != "") {
        $('.nWrongLogin').removeClass("hidden");
    }
}

function forgotPage() {
    loginResize();
    footerFix(29, "nFooterShort30");
    $(window).resize(function () {
        loginResize();
    });

    //hide the buttons if the status is on
    if ($('#HiddenFieldStatus').val() == '1') {
        $('.nLoginBox').addClass('invisible');

    }

    addGoogleAnalytics();

    //show the success message if password is succesfully changed
    // 1 = succesful -> peygham neshoon dade she, 0 = fail 
    if ($('#HiddenFieldStatus').val == '1') {
        $('.nWrongLogin').removeClass('hidden');
    }

    function forgotButtonClicked() {
        var valid = true;
        if (!validateEmail($('#TextBoxEmail').val())) {
            valid = false;
            $('.nForgotEmailError').removeClass('invisible');
        } else {
            $('.nForgotEmailError').addClass('invisible');
        }
        if (valid) {
            $("#ButtonRequest").trigger("click");
        }
    };
    //prevent form submit by enter
    $('#TextBoxEmail').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            forgotButtonClicked();
        }
    });

    errorMessage();

    //forgot password button
    $(".nFPButton").click(function () {
        forgotButtonClicked();
    });


    //*************recover**********
    function recoverButtonClicked() {
        valid = true;
        if ($('#TextBoxPassword1').val() == "") {
            $('.nForgotPass1Error').removeClass('invisible');
            valid = false;
        } else {
            $('.nForgotPass1Error').addClass('invisible');
        }
        if ($('#TextBoxPassword1').val() != $('#TextBoxPassword2').val()) {
            $('.nForgotPass2Error').removeClass('invisible');
            valid = false;
        } else {
            $('.nForgotPass2Error').addClass('invisible');
        }
        if (valid) {
            $("#ButtonRecover").trigger("click");
        }
    }
    //recover password button
    $(".nFPRecoverButton").click(function () {
        recoverButtonClicked();
        // nFPRecoverButton();
        //$('#ButtonRecover').trigger('click');
    });

    //prevent enter button
    $('#TextBoxPassword1').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            recoverButtonClicked();
        }
    });
    $('#TextBoxPassword2').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            recoverButtonClicked();
        }
    });
    //displaying error message in forgot password page
    if ($("#LabelMessage").text() != "") {
        $('.nLabelMessageCopy').text($("#LabelMessage").text());
        $('.nWrongLogin').removeClass("hidden");
    }

    //login button in forgot page
    $(".nForgotLogin").click(function () {
        window.location.href = getRootUrl() + "Login";
    });

    //register button in forgot page
    $(".nForgotRegister").click(function () {
        window.location.href = getRootUrl() + "Register";
    });
}


//nInviteEachContainer
function InvitePage() {
    //toast
    // message, type(1but or 2but), closable (bool),button1Text, button1url, button1Type(url, action), button2Text, button2url, button2Type(url, action)
    if ($('#ContentPlaceHolder1_HiddenFieldToastStatus').val() == '1') {
        var message = $('#ContentPlaceHolder1_HiddenFieldToastMessage').val();
        var button1Text = $('#ContentPlaceHolder1_HiddenFieldButton1Text').val();
        var button2Text = $('#ContentPlaceHolder1_HiddenFieldButton2Text').val();
        var button1url = $('#ContentPlaceHolder1_HiddenFieldButton1Url').val();
        var button2url = $('#ContentPlaceHolder1_HiddenFieldButton2Url').val();
        var color1 = $('#ContentPlaceHolder1_HiddenFieldButton1Color').val();
        var color2 = $('#ContentPlaceHolder1_HiddenFieldButton2Color').val();
        var smilee = $('#ContentPlaceHolder1_HiddenFieldToastSmiley').val();
        openTheToast(message, smilee, '1but', false, button1Text, 'close', 'url', color1, button2Text, button2url, 'url', color2);
    }

    //prevent Enter Key
    $(window).keydown(function (event) {
        if (event.keyCode == 13 && !validateEmail($('#ContentPlaceHolder1_TextBoxEmail').val())) {
            $('.nGeneralErrorMessage').html("Please enter the email in the correct format.");
            event.preventDefault();
            return false;
        }
    });


    $('.nInviteEmailarrow').click(function () {
        if (!validateEmail($('#ContentPlaceHolder1_TextBoxEmail').val())) {
            $('.nGeneralErrorMessage').html("Please enter the email in the correct format.");
            console.log("no");
        } else {
            console.log("yes");
            $('#ContentPlaceHolder1_ButtonSubmit').trigger('click');
        }
    });

    var userID = $('#ContentPlaceHolder1_HiddenFieldUserId').val();
    $('.nInviteEachContainerFB').click(function () {
        console.log('userID: ' + userID);
        var fbLink = "http://www.facebook.com/share.php?u=" + getRootUrl() + 'Register/' + userID;
        console.log(fbLink);
        window.location.href = fbLink;
    })
    $('.nInviteEachContainerTwitter').click(function () {
        twitterLink = "http://twitter.com/home?status=" + getRootUrl() + 'Register/' + userID;
        window.location.href = twitterLink;
    })
    $('.nInviteEachContainerMail').click(function () {
        $('.nInviteEmailOuterContainer').toggleClass('invisible');
        //$('.nInviteEmailOuterContainer').removeClass('invisible');
    })
}
function registerPage() {

    //$('#HiddenFieldInviteStatus').val("0");
    //check if the user has been invited by someone else
    if ($('#HiddenFieldInviteStatus').val() == "1") {
        //the user is invited by some one
        var bgURL = "url('" + getRootUrl() + $('#HiddenFieldInvitePhotoUrl').val() + "')";
        console.log(bgURL);
        $('.nPanelProfilePicture').css('background-image', bgURL);

        $('.nRegisterInviteSpan').html($('#HiddenFieldInviteName').val());
        $('.nRegisterInviteContainer').removeClass('invisible');
        $('.nRegisterFooter').addClass('nRegisterFooterTall');
        $('.nRegisterFooterTall').removeClass('nRegisterFooter');
        $('.nLoginBox').addClass('nRegisterBoxSmall');
        $('.nRegisterBoxSmall').removeClass('nLoginBox');

    }

    //nPanelProfilePicture


    addGoogleAnalytics();
    //facebook button click
    $('.nLoginFacebookButton').click(function () {
        var endOfUrl = "";
        if ($('#HiddenFieldInvite').val() != "0") {
            endOfUrl = "/" + $('#HiddenFieldInvite').val();
        }
        var url = getRootUrl() + "Facebooksync";
        location.href = url;
    })


    loginResize();
    $(window).resize(function () {
        loginResize();
    })

    function registerButtonClicked() {
        var valid = true;
        if (!validateEmail($('#TextBoxEmail').val())) {
            valid = false;
            $('.nRegisterEmailError').removeClass('invisible');
        } else {
            $('.nRegisterEmailError').addClass('invisible');
        }

        if ($('#TextBoxPassword1').val() == "") {
            valid = false;
            $('.nRegisterPasswordError').removeClass('invisible');
        } else {
            $('.nRegisterPasswordError').addClass('invisible');
        }
        if (($('#TextBoxPassword1').val() != $('#TextBoxPassword2').val()) || ($('#TextBoxPassword2').val() == "")) {
            valid = false;
            $('.nRegisterRepeatPassError').removeClass('invisible');
        } else {
            $('.nRegisterRepeatPassError').addClass('invisible');
        }
        if (valid == true) {
            $("#ButtonRegister").trigger("click");
        }
    }
    //prevent form submit by enter
    $('#TextBoxEmail').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            registerButtonClicked();
        }
    });
    $('#TextBoxPassword1').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            registerButtonClicked();
        }
    });
    $('#TextBoxPassword2').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            registerButtonClicked();
        }
    });
    //login button in register page
    $(".nRegisterLogin").click(function () {
        window.location.href = getRootUrl() + "Login";
    });

    //register button
    $(".nRegisterButton").click(function () {
        registerButtonClicked();
    });

    //displaying error message
    errorMessage();
}
//login, forgot password. register

function loginPage() {
    addGoogleAnalytics();

    //facebook button click
    $('.nLoginFacebookButton').click(function () {
        var url = getRootUrl() + "Facebooksync";
        location.href = url;
    })

    loginResize();
    footerFix(29, "nFooterShort30");
    $(window).resize(function () {
        loginResize();
    })
    function loginbuttonClicked() {
        var valid = true;
        //validation
        if (!validateEmail($('#TextBoxUsername').val())) {
            $('.nLoginEmailError').removeClass('invisible');
            valid = false;
        } else {
            $('.nLoginEmailError').addClass('invisible');
        }
        if ($('#TextBoxPassword').val() == "") {
            $('.nLoginPasswordError').removeClass('invisible');
            valid = false;
        } else {
            $('.nLoginPasswordError').addClass('invisible');
        }
        if (valid)
            $("#ButtonLogin").trigger("click");
    }
    //prevent form submit by enter
    $('#TextBoxUsername').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            loginbuttonClicked();
        }
    });

    //prevent form submit by enter
    $('#TextBoxPassword').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            loginbuttonClicked();
        }
    });

    //login button
    $(".nLoginButton").click(function () {
        loginbuttonClicked();
    });

    //forget button
    $(".nLoginForgot").click(function () {
        window.location.href = "ForgotPassword/Request";
    });

    //register button in login page
    $(".nLoginRegister").click(function () {
        window.location.href = "Register";
    });

    errorMessage();



}

/*returns nothing
(the class of objects, bgcolor of selected, bgcolor of all,optional innerclass)
*/
function nRadioButton(givenClass, selected, all, innerClass) {
    //setting activity name and id

    if (innerClass == null) {
        $(givenClass).click(function () {
            $(givenClass).css("background-color", all);
            $(this).css("background-color", selected);
        })
    }
    else {
        $(givenClass).click(function () {
            $(givenClass).find(innerClass).css("background-color", all);
            $(this).find(innerClass).css("background-color", selected);
        })
    }
}

function eventsModifyPage() {

    nFooterShortClass = "nFooterShort38";
    screenShortParam = 40; //em

    //change of the city
    $('#ContentPlaceHolder1_DropDownListCountry, #ContentPlaceHolder1_DropDownListCity').change(function () {
        $('#ContentPlaceHolder1_HiddenFieldLocationId').val($('#ContentPlaceHolder1_DropDownListCity').val());
        console.log($('#ContentPlaceHolder1_HiddenFieldLocationId').val());
    })

    var alternateTime = false;
    if ($('[type="time"]').prop('type') != 'time') {
        $('[type="time"]').addClass('invisible');
        $('.nTimeAlternateContainer').removeClass('invisible');
        alternateTime = true;
    }


    //signalR
    var chat = $.connection.chatHub;
    $.connection.hub.start().done(function () {
        //change in the country detected
        $('#ContentPlaceHolder1_DropDownListCountry').change(function () {
            chat.server.getCities($('#ContentPlaceHolder1_DropDownListCountry').val()).done(function (result) {
                if (result) {
                    console.log('it should be changed');
                    console.log(result);
                    $('#ContentPlaceHolder1_DropDownListCity').html(result);
                    $('#ContentPlaceHolder1_HiddenFieldLocationId').val($('#ContentPlaceHolder1_DropDownListCity').val());
                }
            })
        })
    })

    //setting initial maximum value of participants
    var sliderMin = $('#ContentPlaceHolder1_HiddenFieldParticipantsAccepted').val();
    var sliderHTML = "<input type='number' class='participantsSlider' name='slider-fill' data-highlight='true' data-mini='true' data-type='range' min='" + sliderMin + "' max='100 ' step='1' value='17'>"
    $(sliderHTML)
       .appendTo(".participantsSliderContainer")
       .slider()
       .textinput();
    //setting cover photo 
    chooseBackground();
    //setting initial background value
    var coverID = $('#ContentPlaceHolder1_HiddenFieldCoverId').val();
    console.log('coverIDcoverID ' + coverID);
    $('.nSPCoverPhoto').css('background-image', 'url(../../Images/Covers/' + coverID + '.jpg)');
    //initial selecting right circle
    var selector = '.nSearchLogo[activity-id="' + coverID + '"]';
    $(selector).addClass("nSPCoverPhotoSelected");

    var activity;
    var activityID;
    var timeType;
    var currentPage = $('#ContentPlaceHolder1_HiddenFieldStep').val();
    if (currentPage == "") {
        currentPage = 0;
    } else {
        currentPage--;
    }
    //if page has had error in last stage
    if (currentPage == 3) {
        console.log("something went wrong");
        currentPage = 0;
    }

    //setting values
    //setting activity type and icon
    activityID = $('#ContentPlaceHolder1_HiddenFieldTypeId').val();
    var selector = ".nActivityLogo[activity-id=" + activityID + "]";
    $(selector).css("background-color", "rgb(255,187,5)");
    activity = $(selector).attr("data");
    $('.nEAACtivityType').text(activity);

    //setting the participat slider
    $(".participantsSlider").val($('#ContentPlaceHolder1_HiddenFieldParticipants').val());
    $(".participantsSlider").slider("refresh");

    //setting the date
    var eventDateFull = $('#ContentPlaceHolder1_HiddenFieldDate').val();
    console.log("full" + eventDateFull);
    var eventDate = eventDateFull.split(" ");
    //30/1/2015
    var eventDateSeperated = eventDate[0].split("/");
    var d = new Date(eventDateSeperated[2], eventDateSeperated[1] - 1, eventDateSeperated[0]);
    console.log(d.toDateString());
    //console.log("$('#ContentPlaceHolder1_HiddenFieldDate').val() " + $('#ContentPlaceHolder1_HiddenFieldDate').val());

    //setting the date
    var eventMonth = d.getMonth() + 1;
    if (eventMonth < 10) {
        eventMonth = "0" + eventMonth;
    } else {
    }
    var eventsDate;
    if (d.getDate() < 10) {
        eventsDate = "0" + d.getDate();
    } else {
        eventsDate = d.getDate();
    }
    var finalDate = "2015" + "-" + eventMonth + "-" + eventsDate;
    $('.nEAdate').val(finalDate);

    console.log("eventDate[1] " + eventDate[1]);
    //setting the time
    $('.nEAtime').val(eventDate[1]);
    var totalTime = eventDate[1].split(":");
    var hours = totalTime[0];
    var minutes = totalTime[1];
    var minutes = 5 * Math.round(minutes / 5);

    $(".nTimeAlternateHourSelect").val(hours);
    $(".nTimeAlternateMinSelect").val(minutes);
    $(".nTimeAlternateAMPMSelect").val(eventDate[2]);

    //setting the duration slider
    $(".nEADurationSlider").val($('#ContentPlaceHolder1_HiddenFieldDuration').val());
    $(".nEADurationSlider").slider("refresh");

    //setting the duration type
    timeType = $('#ContentPlaceHolder1_HiddenFieldDurationType').val();
    var selector = ".nTimeTypeContainer[time-type=" + timeType + "]";
    $(selector).find('.nRadioCircle').css("background-color", "rgb(215,67,46)");

    //if the page is reloaded in changing location part
    if (currentPage == 2) {

    }
        //if the page is loaded for the first time
    else if (currentPage == 0) {
        timeType = "Unknown";
        //$(".nActivityMore").css("background-color", "rgb(255,187,5)");
        $(".nTimeTypeContainerUnknown").find('.nRadioCircle').css("background-color", "rgb(215,67,46)");
        //$('#ContentPlaceHolder1_HiddenFieldTypeId').val("8");

        //setting date to the current date
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var today = now.getFullYear() + "-" + (month) + "-" + (day);
        $('#ContentPlaceHolder1_HiddenFieldDate').val($('.nEAdate').val());

        //setting duration type to unknown
        $('#ContentPlaceHolder1_HiddenFieldDurationType').val("Unknown");

    }

    setEventPages(currentPage);

    //choosing activity type
    nRadioButton('.nActivityLogo', "rgb(255,187,5)", 'rgb(222, 222, 222)');
    $('.nActivityLogo').click(function () {
        activity = $(this).attr("data");
        activityID = $(this).attr("activity-id");
        $('.nEAACtivityType').text(activity);
        $('#ContentPlaceHolder1_HiddenFieldTypeId').val(activityID);
        $('.nEAACtivityType').removeClass('nGeneralErrorMessage');
    })

    //setting participants
    $('.participantsSlider').change(function () {
        console.log($('.participantsSlider').val());
        $('#ContentPlaceHolder1_HiddenFieldParticipants').val($('.participantsSlider').val());
    })

    //setting start time
    var alternateTimeResult = "01:00";
    if (alternateTime) {
        $('.nTimeAlternateHourSelect').change(function () {
            console.log($('.nTimeAlternateHourSelect').val());
            $('#ContentPlaceHolder1_HiddenFieldTime').val($('.nEAtime').val());
        })
        $('.nTimeAlternateMinSelect').change(function () {
            console.log($('.nTimeAlternateMinSelect').val());
            $('#ContentPlaceHolder1_HiddenFieldTime').val($('.nEAtime').val());
        })
        $('.nTimeAlternateAMPMSelect').change(function () {
            console.log($('.nTimeAlternateAMPMSelect').val());
            $('#ContentPlaceHolder1_HiddenFieldTime').val($('.nEAtime').val());
        })

        $('.nTimeAlternateContainer select').change(function () {
            if ($('.nTimeAlternateAMPMSelect').val() == "AM") {
                alternateTimeResult = $('.nTimeAlternateHourSelect').val() + ":" + $('.nTimeAlternateMinSelect').val();
            } else if ($('.nTimeAlternateAMPMSelect').val() == "PM") {
                var hours = (parseInt($('.nTimeAlternateHourSelect').val()) + 12);
                alternateTimeResult = hours + ":" + $('.nTimeAlternateMinSelect').val();
            }
            console.log(alternateTimeResult);
        })
    }
    //setting complete date when date is changed
    $(".nEAdate").change(function () {
        var completeTime = $('.nEAdate').val() + " " + $('.nEAtime').val();
        console.log('yeps ' + completeTime);
        $('#ContentPlaceHolder1_HiddenFieldDate').val(completeTime);
    });
    //setting complete date when time is changed
    $(".nEAtime").change(function () {
        var completeTime = $('.nEAdate').val() + " " + $('.nEAtime').val();
        console.log('yeps ' + completeTime);
        $('#ContentPlaceHolder1_HiddenFieldDate').val(completeTime);
    });


    //setting duration
    $('.nEADurationSlider').change(function () {
        console.log($('.nEADurationSlider').val());
        $('#ContentPlaceHolder1_HiddenFieldDuration').val($('.nEADurationSlider').val());
    })

    //setting duration type
    nRadioButton('.nTimeTypeContainer', "rgb(215,67,46)", 'white', ".nRadioCircle");
    $('.nTimeTypeContainer').click(function () {
        timeType = $(this).attr("time-type");
        $('#ContentPlaceHolder1_HiddenFieldDurationType').val(timeType);
    })

    //setting participants
    $(".participantsSlider").change(function () {
        $('#ContentPlaceHolder1_TextBoxParticipants').val($('.participantsSlider').val());
    });

    //setting date
    $(".nEAdate").change(function () {
        $('#ContentPlaceHolder1_HiddenFieldDate').val($('.nEAdate').val());
    });
    //var languages = [];
    popUp('.nEAlanguageButtonsAdd', '.nDotsBox', true);

    //displaying the initial languages
    var languagesRaw = $('#ContentPlaceHolder1_HiddenFieldLanguages').val();
    var languages = languagesRaw.split(',');
    console.log('languages ' + languages);
    for (index = 0; index < languages.length; ++index) {
        //console.log(a[index]);
        var appendText = '<div class="nEAlanguageButtons ' + 'nEAlanguageButtons' + languages[index] + '">' + languages[index] + '<div class="nLanguageX">X</div></div>';;
        $('.nEAlanguagesAll').append(appendText);
    }
    //adding the languages
    /*$('.nDotsBox').find('div').click(function () {
        var language = $(this).attr('data-language');
        if ($.inArray(language, languages) == -1) {
            languages.push(language);
            var appendText = '<div class="nEAlanguageButtons ' + 'nEAlanguageButtons' + language + '">' + language + '<div class="nLanguageX">X</div></div>';
            $('.nEAlanguagesAll').append(appendText);
            $('#ContentPlaceHolder1_TextBoxLanguages').val(languages);
            $('#ContentPlaceHolder1_HiddenFieldLanguages').val(languages);
            console.log($('#ContentPlaceHolder1_HiddenFieldLanguages').val());
        }
    });

    //removing the language
    $('.nEAlanguagesAll').on('click', '.nEAlanguageButtons', function () {
        var language = $(this).html().substring(0, 2);
        console.log(language);
        if ($.inArray(language, languages) != -1) {
            languages.splice($.inArray(language, languages), 1);
            var removeSelector = ".nEAlanguageButtons" + language;
            $(removeSelector).remove();
            $('#ContentPlaceHolder1_TextBoxLanguages').val(languages);
            $('#ContentPlaceHolder1_HiddenFieldLanguages').val(languages);
            console.log($('#ContentPlaceHolder1_HiddenFieldLanguages').val());
        }
    });
    */

    //prevent form submit by enter
    $('.nFooterButton').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13)
            event.preventDefault();
    });

    //validation of each page
    function nAEpageButtonPressed() {
        if (currentPage < 5) {
            //validation of fields
            if (currentPage == 0) {
                if (activityID == "0") {
                    $('.nEAACtivityType').html('Please choose event type :-)');
                    $('.nEAACtivityType').addClass('nGeneralErrorMessage');
                    return;
                }
                else if ($('#ContentPlaceHolder1_TextBoxName').val() == "") {
                    $('#ContentPlaceHolder1_TextBoxName').attr('placeholder', 'Please assign a name for the event :-)');
                    $('#ContentPlaceHolder1_TextBoxName').focus();
                    console.log("no");
                    return;
                }
                else if (!$.isNumeric($('.participantsSlider').val()) || $('.participantsSlider').val() < 1) {
                    $('.nEAParticipantsError').html("Please enter participants in correct format.");
                    console.log("no");
                    return;
                }
                if (!validateTimeIsFuture($('.nEAdate').val())) {
                    $('.nEADateMessage').html('Please set the date correctly :-)');
                    $('#ContentPlaceHolder1_TextBoxDate').focus();
                    console.log("no");
                    return;
                }
                if ($('.nEAtime').val() == "" && !alternateTime) {
                    $('.nEATimeMessage').html('Please set the starting time :-)');
                    $('#ContentPlaceHolder1_TextBoxName').focus();
                    console.log("no");
                    return;
                }
                //2check if hours are in future
                if (!alternateTime && !validateHoursIsFuture($('.nEAdate').val(), $('.nEAtime').val())) {
                    $('.nEATimeMessage').html('Starting time must be at least 15 minutes from now:-)');
                    $('#ContentPlaceHolder1_TextBoxName').focus();
                    console.log("no");
                    return;
                }
                else if (alternateTime && !validateHoursIsFuture($('.nEAdate').val(), alternateTimeResult)) {
                    $('.nEATimeMessage').html('Starting time can\'t be in the past! :-)');
                    $('#ContentPlaceHolder1_TextBoxName').focus();
                    console.log("no");
                    return;
                }
                $('.nEADateMessage').html('');
            }
            if (currentPage == 1) {
                console.log("*************" + $('#ContentPlaceHolder1_DropDownListCity').val() == 0);
                if ($('#ContentPlaceHolder1_DropDownListCountry').val() == "0") {
                    $('.nEALocationMessage').html("Select the event's country please :-)");
                    $('#ContentPlaceHolder1_DropDownListCountry').focus();
                    return;
                }
                if ($('#ContentPlaceHolder1_DropDownListCity').val() == "0") {
                    $('.nEALocationMessage').html("Select the event's city please :-)");
                    $('#ContentPlaceHolder1_DropDownListCity').focus();
                    return;
                }
                /*address validating
                if ($('#ContentPlaceHolder1_TextBoxAddress').val() == "") {
                    $('.nEAAdressMessage').html("Please enter the events street address :-)");
                    $('#ContentPlaceHolder1_TextBoxAddress').focus();
                    return;
                }*/
                //ready to create your event?
                //openTheToast();
                //Ready to create your event;
                console.log("whate the~!?");
                openTheToast('Are you done modifying your event?', ':)', '2but', false, 'Cancel', 'close', 'url', null, 'Confirm', '#ContentPlaceHolder1_ButtonSubmit', 'action', null);
                $('.nEADateMessage').html('');
            }
            else {
                currentPage++;
                console.log("change to:" + currentPage);
                setEventPages(currentPage);
            }
        }
    }


    $(".nProfileNextButton").click(function () {
        nAEpageButtonPressed();
    })
    $(".nNotButton").click(function () {
        if (currentPage == 0) {
            //display the toast
            openTheToast('Are you sure you want to delete this event?', ':(', '2but', true, 'Cancel', 'close', 'url', null, 'Delete', '#ContentPlaceHolder1_ButtonDelete', 'action', null);
        } else {
            currentPage--;
            setEventPages(currentPage);
        }
    })
    //preventing post submit by click
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            nAEpageButtonPressed();
            return false;
        }
    });
}

function eventsAddPage() {

    nFooterShortClass = "nFooterShort40";
    screenShortParam = 40; //em

    //initial set of the country
    var initialCountry = $('#ContentPlaceHolder1_HiddenFieldUserCountry').val();
    console.log("initialCountry :" + initialCountry);
    $('#ContentPlaceHolder1_DropDownListCountry').val(initialCountry);

    //change of the city
    $('#ContentPlaceHolder1_DropDownListCountry, #ContentPlaceHolder1_DropDownListCity').change(function () {
        $('#ContentPlaceHolder1_HiddenFieldLocationId').val($('#ContentPlaceHolder1_DropDownListCity').val());
        console.log($('#ContentPlaceHolder1_HiddenFieldLocationId').val());
    })

    var alternateTime = false;
    if ($('[type="time"]').prop('type') != 'time') {
        $('[type="time"]').addClass('invisible');
        $('.nTimeAlternateContainer').removeClass('invisible');
        alternateTime = true;
    }
    //footerFix(19, "nFooterShort20");
    //footerFix(38, "nFooterShort40");
    //signalR
    var chat = $.connection.chatHub;
    $.connection.hub.start().done(function () {
        //change in the country detected
        $('#ContentPlaceHolder1_DropDownListCountry').change(function () {
            chat.server.getCities($('#ContentPlaceHolder1_DropDownListCountry').val()).done(function (result) {
                if (result) {
                    console.log('it should be changed');
                    console.log(result);
                    $('#ContentPlaceHolder1_DropDownListCity').html(result);
                    $('#ContentPlaceHolder1_HiddenFieldLocationId').val($('#ContentPlaceHolder1_DropDownListCity').val());
                    //initial set of the city
                    var initialCity = $('#ContentPlaceHolder1_HiddenFieldUserCity').val();
                    console.log("initialCountry :" + initialCity);
                    $('#ContentPlaceHolder1_DropDownListCity').val(initialCity);
                }
            })
        })
    })

    chooseBackground();
    //setting initial background value
    var coverID = "101";
    $('.nSPCoverPhoto').css('background-image', 'url(../../Images/Covers/' + coverID + '.jpg)');
    //initial selecting right circle
    var selector = '.nSearchLogo[activity-id="' + coverID + '"]';
    $(selector).addClass("nSPCoverPhotoSelected");

    var activity;
    var activityID;
    var timeType;
    var currentPage = $('#ContentPlaceHolder1_HiddenFieldStep').val();
    if (currentPage == "") {
        currentPage = 0;
    } else {
        currentPage--;
    }
    //if the page has had error in last stage
    if (currentPage == 3) {
        console.log("something went wrong");
        currentPage = 0;
    }

    if (currentPage == "") {
        currentPage = 0;
        activity = "";
        activityID = "0";
        timeType = "Unknown";
        //$(".nActivityMore").css("background-color", "rgb(255,187,5)");
        $(".nTimeTypeContainerUnknown").find('.nRadioCircle').css("background-color", "rgb(215,67,46)");
        //$('#ContentPlaceHolder1_HiddenFieldTypeId').val("8");

        //setting date to the current date
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var today = now.getFullYear() + "-" + (month) + "-" + (day);
        $('.nEAdate').val(today);
        console.log(today);
        $('#ContentPlaceHolder1_HiddenFieldDate').val($('.nEAdate').val());

        //setting duration type to unknown
        $('#ContentPlaceHolder1_HiddenFieldDurationType').val("Unknown");
    }

    setEventPages(currentPage);

    //choosing activity type
    nRadioButton('.nActivityLogo', "rgb(255,187,5)", 'rgb(222, 222, 222)');
    $('.nActivityLogo').click(function () {
        activity = $(this).attr("data");
        activityID = $(this).attr("activity-id");
        $('.nEAACtivityType').text(activity);
        $('#ContentPlaceHolder1_HiddenFieldTypeId').val(activityID);
        $('.nEAACtivityType').removeClass('nGeneralErrorMessage');
    })

    //setting participants
    $('.participantsSlider').change(function () {
        console.log($('.participantsSlider').val());
        $('#ContentPlaceHolder1_HiddenFieldParticipants').val($('.participantsSlider').val());
    })

    //setting start time
    var alternateTimeResult = "01:00";
    if (alternateTime) {
        $('.nTimeAlternateHourSelect').change(function () {
            console.log($('.nTimeAlternateHourSelect').val());
            $('#ContentPlaceHolder1_HiddenFieldTime').val($('.nEAtime').val());
        })
        $('.nTimeAlternateMinSelect').change(function () {
            console.log($('.nTimeAlternateMinSelect').val());
            $('#ContentPlaceHolder1_HiddenFieldTime').val($('.nEAtime').val());
        })
        $('.nTimeAlternateAMPMSelect').change(function () {
            console.log($('.nTimeAlternateAMPMSelect').val());
            $('#ContentPlaceHolder1_HiddenFieldTime').val($('.nEAtime').val());
        })

        $('.nTimeAlternateContainer select').change(function () {
            if ($('.nTimeAlternateAMPMSelect').val() == "AM") {
                alternateTimeResult = $('.nTimeAlternateHourSelect').val() + ":" + $('.nTimeAlternateMinSelect').val();
            } else if ($('.nTimeAlternateAMPMSelect').val() == "PM") {
                var hours = (parseInt($('.nTimeAlternateHourSelect').val()) + 12);
                alternateTimeResult = hours + ":" + $('.nTimeAlternateMinSelect').val();
            }
            console.log(alternateTimeResult);
        })
    }


    //setting duration
    $('.nEADurationSlider').change(function () {
        console.log($('.nEADurationSlider').val());
        $('#ContentPlaceHolder1_HiddenFieldDuration').val($('.nEADurationSlider').val());
    })

    //setting duration type
    nRadioButton('.nTimeTypeContainer', "rgb(215,67,46)", 'white', ".nRadioCircle");
    $('.nTimeTypeContainer').click(function () {
        timeType = $(this).attr("time-type");
        $('#ContentPlaceHolder1_HiddenFieldDurationType').val(timeType);
    })

    //setting participants
    $(".participantsSlider").change(function () {
        $('#ContentPlaceHolder1_TextBoxParticipants').val($('.participantsSlider').val());
    });


    /*var languages = [];
    popUp('.nEAlanguageButtonsAdd', '.nDotsBox', true);

    //adding the languages
    $('.nDotsBox').find('div').click(function () {
        var language = $(this).attr('data-language');
        if ($.inArray(language, languages) == -1) {
            languages.push(language);
            var appendText = '<div class="nEAlanguageButtons ' + 'nEAlanguageButtons' + language + '">' + language + '<div class="nLanguageX">X</div></div>';
            $('.nEAlanguagesAll').append(appendText);
            $('#ContentPlaceHolder1_TextBoxLanguages').val(languages);
            $('#ContentPlaceHolder1_HiddenFieldLanguages').val(languages);
            console.log($('#ContentPlaceHolder1_HiddenFieldLanguages').val());
        }
    });

    //removing the language
    $('.nEAlanguagesAll').on('click', '.nEAlanguageButtons', function () {
        var language = $(this).html().substring(0, 2);
        console.log(language);
        if ($.inArray(language, languages) != -1) {
            languages.splice($.inArray(language, languages), 1);
            var removeSelector = ".nEAlanguageButtons" + language;
            $(removeSelector).remove();
            $('#ContentPlaceHolder1_TextBoxLanguages').val(languages);
            $('#ContentPlaceHolder1_HiddenFieldLanguages').val(languages);
            console.log($('#ContentPlaceHolder1_HiddenFieldLanguages').val());
        }
    });
    */

    //setting the complete date when date is changed
    $(".nEAdate").change(function () {
        if (!alternateTime) {
            var completeTime = $('.nEAdate').val() + " " + $('.nEAtime').val();
            console.log('yeps ' + completeTime);
            $('#ContentPlaceHolder1_HiddenFieldDate').val(completeTime);
        } else {
            var completeTime = $('.nEAdate').val() + " " + alternateTimeResult;
            console.log('yeps ' + completeTime);
            $('#ContentPlaceHolder1_HiddenFieldDate').val(completeTime);
        }
    });
    //setting complete date when time is changed
    $(".nEAtime").change(function () {
        var completeTime = $('.nEAdate').val() + " " + $('.nEAtime').val();
        console.log('yeps ' + completeTime);
        $('#ContentPlaceHolder1_HiddenFieldDate').val(completeTime);
    });

    //setting complete date when the alternate time is changed
    $(".nTimeAlternateContainer select").change(function () {
        var completeTime = $('.nEAdate').val() + " " + alternateTimeResult;
        console.log('yeps ' + completeTime);
        $('#ContentPlaceHolder1_HiddenFieldDate').val(completeTime);
    });


    //prevent form submit by enter
    $('.nFooterButton').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13)
            event.preventDefault();
    });

    //validation of each page
    function nAEpageButtonPressed() {
        $('.nEAFirstPageError').html('');
        console.log("current page: " + currentPage);
        if (currentPage < 5) {
            //validation of fields
            if (currentPage == 0) {
                if (activityID == "0") {
                    $('.nEAFirstPageError').html('Please choose the event type ');
                    return;
                }
                else if ($('#ContentPlaceHolder1_TextBoxName').val() == "") {
                    $('#ContentPlaceHolder1_TextBoxName').attr('placeholder', 'Please assign a name for the event :-)');
                    $('#ContentPlaceHolder1_TextBoxName').focus();
                    console.log("no");
                    return;
                }
                else if (!$.isNumeric($('.participantsSlider').val()) || $('.participantsSlider').val() < 1) {
                    $('.nEAParticipantsError').html("Please enter participants in correct format.");
                    console.log("no");
                    return;
                }

                if (!validateTimeIsFuture($('.nEAdate').val())) {
                    $('.nEADateMessage').html('Please set the date correctly :-)');
                    $('#ContentPlaceHolder1_TextBoxDate').focus();
                    console.log("no");
                    return;
                }

                if ($('.nEAtime').val() == "" & !alternateTime) {
                    $('.nEATimeMessage').html('Please set the starting time :-)');
                    $('#ContentPlaceHolder1_TextBoxName').focus();
                    console.log("no");
                    return;
                }
                //2check if hours are in future
                if (!alternateTime && !validateHoursIsFuture($('.nEAdate').val(), $('.nEAtime').val())) {
                    $('.nEATimeMessage').html('Starting time must be at least 15 minutes from now:-)');
                    $('#ContentPlaceHolder1_TextBoxName').focus();
                    console.log("no");
                    return;
                }
                else if (alternateTime && !validateHoursIsFuture($('.nEAdate').val(), alternateTimeResult)) {
                    $('.nEATimeMessage').html('Starting time can\'t be in the past! :-)');
                    $('#ContentPlaceHolder1_TextBoxName').focus();
                    console.log("no");
                    return;
                }
                $('.nEADateMessage').html('');
            }
            if (currentPage == 1) {
                console.log("*************" + $('#ContentPlaceHolder1_DropDownListCity').val() == 0);
                if ($('#ContentPlaceHolder1_DropDownListCountry').val() == "0") {
                    $('.nEALocationMessage').html("Select the event's country please :-)");
                    $('#ContentPlaceHolder1_DropDownListCountry').focus();
                    return;
                }
                if ($('#ContentPlaceHolder1_DropDownListCity').val() == "0") {
                    $('.nEALocationMessage').html("Select the event's city please :-)");
                    $('#ContentPlaceHolder1_DropDownListCity').focus();
                    return;
                }
                /*address validating
                if ($('#ContentPlaceHolder1_TextBoxAddress').val() == "") {
                    $('.nEAAdressMessage').html("Please enter the events street address :-)");
                    $('#ContentPlaceHolder1_TextBoxAddress').focus();
                    return;
                }*/
                //ready to create your event?
                //openTheToast();
                //Ready to create your event;
                openTheToast('Ready to create your event?', ':)', '2but', false, 'Cancel', 'close', 'url', null, 'Confirm', '#ContentPlaceHolder1_ButtonSubmit', 'action', null);
            } else {
                currentPage++;
                setEventPages(currentPage);
            }
        }

    }


    $(".nProfileNextButton").click(function () {
        nAEpageButtonPressed();
    })
    $(".nNotButton").click(function () {
        currentPage--;
        setEventPages(currentPage);
    })
    //preventing post submit by click
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            nAEpageButtonPressed();
            return false;
        }
    });
}


function eventsPage() {
    nFooterShortClass = "nFooterShort30";
    screenShortParam = 30; //em


    //making the errors smaller
    $('.pageErrorContent').addClass('nEventsPageErrorContent');
    //clicking on footer
    $('.nFooterButton').click(function () {
        var url = getRootUrl() + "Events/Add";
        window.location.href = url;
    })
    $('.nMainPanelDiv').addClass('nfullHeight');
    //each notification click
    $('.fullNotificationContainer').click(function () {
        var eventLink = $(this).find('.nEventsUrl').attr('href');
        eventLink = eventLink.substring(eventLink.lastIndexOf('/') + 1);
        var url = getRootUrl() + "Events/" + eventLink;
        console.log("url" + url);
        window.location.href = url;
    });
    //setting images
    $('.nMessagesPicture').each(function () {
        var bgURL = "url('" + getRootUrl() + "/Images/Icons/activity" + $(this).find('input').val() + ".png')";
        console.log(bgURL);
        $(this).css('background-image', bgURL);
    });
    var pageError = $('#ContentPlaceHolder1_HiddenFieldStatus').val();
    var url = window.location.pathname;
    var urlLastSegment = url.substr(url.lastIndexOf('/') + 1);
    if (urlLastSegment == "Created") {
        tabbedContent(0);
        if (pageError == "0") {
            setDoneValues("eventsCreated", 0, 0);
        }
    } else if (urlLastSegment == "Accepted") {
        tabbedContent(1);
        if (pageError == "0") {
            setDoneValues("eventsAccepted", 0, 0);
        }
    } else if (urlLastSegment == "Requested") {
        tabbedContent(2);
        if (pageError == "0") {
            setDoneValues("eventsRequested", 0, 0);
        }
    } else if (urlLastSegment == "Bookmarked") {
        tabbedContent(3);
        if (pageError == "0") {
            setDoneValues("eventsBookmarked", 0, 0);
        }
    }
}

function barGraph() {
    var total = 0;
    $('.nGraphBar').each(function () {
        console.log($(this).find("span").text());
        total += parseInt($(this).find("span").text());
    })
    console.log("total " + total);
    $('.nGraphBar').each(function () {
        var maxWidth = $(this).parent().width();
        var number = parseInt($(this).find("span").text());
        console.log("number " + number);
        var width = (number / total);
        if (width == 0) { width = 0.01 };
        width *= maxWidth;
        //width -= width / 10;
        $(this).css("width", width);
    });
    setTimeout(function () { $('.nGraphBar').removeClass('animationAll'); }, 400);

}

function ReviewPage() {
    nFooterShortClass = "nFooterShort35";
    screenShortParam = 35; //em

    //getReviewInfo
    console.log('page loaded');
    var chat = $.connection.chatHub;
    var userId = parseInt($('#HiddenFieldUserId').val());
    var thisEventCoverId;
    var thisEventId;
    var thisEventName;
    var thisEventTypeId;
    var thisExist;
    var thisFirstName;
    var thisHasPhoto;
    var thisLastName;
    var thisReviewRequestId;
    var thisUserId;
    var thisEventOwnerHasPhoto;
    var thisEventOwnerId;
    //check if the review exists
    //EventCoverId, EventId, EventName, EventTypeId, Exist
    //, FirstName,LastName,OwnerId: kesi ke dare request ro mibine,ReviewRequestId,UserId : sazandeye request
    $('input').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            return false;
        }
    })
    function resetValues() {
        $('#ContentPlaceHolder1_HiddenFieldRate').val('5');
        $('.nFBEachOuterContainer').removeClass("nFBEachOuterContainerSelected");
        $('.nFBEachOuterContainer[data-value="5"]').addClass("nFBEachOuterContainerSelected");
        $('.nReveiwCommentBox').val("");
    }
    resetValues();
    function setValues(result) {
        resetValues();
        thisEventCoverId = result.EventCoverId;
        thisEventId = result.EventId;
        thisEventName = result.EventName;
        thisEventTypeId = result.EventTypeId;
        thisExist = result.Exist;
        thisFirstName = result.FirstName;
        thisHasPhoto = result.HasPhoto;
        thisLastName = result.LastName;
        thisReviewRequestId = result.ReviewRequestId;
        thisUserId = result.UserId;
        thisEventOwnerHasPhoto = result.EventOwnerHasPhoto;
        thisEventOwnerId = result.EventOwnerId;
        if (result.Exist) {

            //setting cover photo
            var bgURL = "url('" + getRootUrl() + "Images/Covers/" + result.EventCoverId + ".jpg')";
            $('.nContentCover').css('background-image', bgURL);
            //setting the event name 
            $('.nContentTitle b').html(result.EventName);
            $('.nReviewEventNameLabel').html(result.EventName);

            //setting the persons name 
            $('.nReviewUserLink').html(result.FirstName + " " + result.LastName);
            //setting the link of the name
            $('.nReviewUserLink').attr('href', getRootUrl() + "Profile/" + result.UserId);


            //setting event owner pic
            // if photo is false :Images/NoPhoto.png
            //else Files/ProfilesPhotos/" + UserId.ToString() + "-100.jpg
            var profBgURL;
            if (result.EventOwnerHasPhoto) {
                profBgURL = "url('" + getRootUrl() + "Files/ProfilesPhotos/" + result.EventOwnerId + "-100.jpg')";
            } else {
                profBgURL = "url('" + getRootUrl() + "Images/NoPhoto.png')";
            }
            $('.nProfpagePicture').css('background-image', profBgURL);

            //setting reviwee pic
            // if photo is false :Images/NoPhoto.png
            //else Files/ProfilesPhotos/" + UserId.ToString() + "-100.jpg
            if (result.HasPhoto) {
                profBgURL = "url('" + getRootUrl() + "Files/ProfilesPhotos/" + result.UserId + "-100.jpg')";
            } else {
                profBgURL = "url('" + getRootUrl() + "Images/NoPhoto.png')";
            }
            $('.nReviewRevieweePhoto').css('background-image', profBgURL);
            showTheMainContent();
        } else {
            setDoneValues("reviewsEmpty", 0, 0);
        }
    }

    //user id kasiye ke dare review mikone
    //reviewCancel(int userId, Int64 reviewRequestId)
    //public int reviewAdd(int userId, Int64 reviewRequestId, string comment, int rate(5))

    $.connection.hub.start().done(function () {
        chat.server.getReviewInfo(userId).done(function (result) {
            //Int64 reviewrequestid, int userid, Int64 eventid, string firstname,
            //  string lastname, Boolean hasphoto, string eventname, int eventtypeid, int eventcoverid, Boolean exist
            console.log('message received!');
            console.log(result);
            setValues(result);
        })
        $('.nRequestButton').click(function () {
            //public bool reviewSend(bool reviewed, int userId, Int64 reviewRequestId, string comment, int rate)
            var comment;
            if ($('.nReveiwCommentBox').val().length > 0) {
                comment = $('.nReveiwCommentBox').val();
            } else {
                comment = "";
            };
            var rate = $('#ContentPlaceHolder1_HiddenFieldRate').val();
            console.log(true, userId, thisReviewRequestId, comment, rate);
            chat.server.reviewSend(true, userId, thisReviewRequestId, comment, rate).done(function (result) {
                //Int64 reviewrequestid, int userid, Int64 eventid, string firstname,
                //  string lastname, Boolean hasphoto, string eventname, int eventtypeid, int eventcoverid, Boolean exist
                console.log('review sent');
                chat.server.getReviewInfo(userId).done(function (result) {
                    console.log('new values received');
                    console.log(result);
                    setValues(result);
                })
            })
        });
        //user did the review
        $('.nNotButton').click(function () {
            console.log('clicked!');
            console.log(false, userId, thisReviewRequestId, "comment", "rate");

            //chat.server.reviewSend(false, userId, thisReviewRequestId, "comment", "rate").done(function (result) {
            chat.server.reviewSend(false, userId, thisReviewRequestId, "comment", 0).done(function (result) {
                console.log('review sent');
                chat.server.getReviewInfo(userId).done(function (result) {
                    console.log('new values received');
                    console.log(result);
                    setValues(result.EventCoverId, result.EventId, result.EventName, result.EventTypeId, result.Exist
                                , result.FirstName, result.HasPhoto, result.LastName, result.ReviewRequestId
                                , result.UserId);
                })
            })
        });
    })

    $('.nContentCoverTransparent').click(function () {
        var href = getRootUrl() + "Profile/" + thisEventOwnerId;
        location.href = href
    })
    $('.nReviewRevieweePhoto').click(function () {
        var href = getRootUrl() + "Profile/" + thisUserId;
        location.href = href;
    })

    var rate = '5';
    $('#ContentPlaceHolder1_HiddenFieldRate').val('5');
    $('.nFBEachOuterContainer').click(function () {
        $('.nFBEachOuterContainer').removeClass('nFBEachOuterContainerSelected');
        $(this).addClass('nFBEachOuterContainerSelected');
        $('#ContentPlaceHolder1_HiddenFieldRate').val($(this).attr('data-value'));
    });
};

function profilePage() {
    nFooterShortClass = "nFooterShort30";
    screenShortParam = 28; //em
    if ($("#ContentPlaceHolder1_HiddenFieldFollowText").val() == "FOLLOW") {
        setFollowBtnStyle("follow");
    } else if ($("#ContentPlaceHolder1_HiddenFieldFollowText").val() != "FOLLOW") {
        setFollowBtnStyle("unfollow");
    }

    //Setting the color of the rating
    var userPercent = parseInt($('#ContentPlaceHolder1_LabelRatePercent').text());
    console.log("userPercent :" + userPercent);
    var percentColor;
    if (userPercent > 80) {
        percentColor = "#009933";
    } else if (userPercent > 60) {
        percentColor = "#CCCC00";
    } else if (userPercent > 40) {
        percentColor = "#FF9933";
    } else if (userPercent > 20) {
        percentColor = "#FF0000 ";
    } else if (userPercent > 0) {
        percentColor = "#800000";
    }
    $('.nGraphTitle').css('color', percentColor);
    //toast variables
    var message;
    var smilee;
    var followText;
    var userId = parseInt($('#HiddenFieldUserId').val());
    var targetId = parseInt($('#ContentPlaceHolder1_HiddenFieldUserId').val());
    var chat = $.connection.chatHub;

    $.connection.hub.start().done(function () {
        $(".nProfileFollowButton").click(function () {
            chat.server.follow(userId, targetId).done(function (result) {
                console.log('result ' + result);
                if (result == 1) {
                    message = "You followed this user";
                    smilee = ":)";
                    $('.nProfileFollowButton').text('UNFOLLOW');
                    //set follow button style
                    setFollowBtnStyle("unfollow");
                }
                else if (result == 2) {
                    message = "You unfollowed this user";
                    smilee = ":(";
                    $('.nProfileFollowButton').text('FOLLOW');
                    //set follow button style
                    setFollowBtnStyle("follow");
                }

                openTheToast(message, smilee, '1but', false, "close", 'close', 'url', "red", "button2Text", "button2url", 'url', "color2");

            });
        })
    });


    //toast
    // message, type(1but or 2but), closable (bool),button1Text, button1url, button1Type(url, action), button2Text, button2url, button2Type(url, action)
    if ($('#ContentPlaceHolder1_HiddenFieldToastStatus').val() == '1') {
        var message = $('#ContentPlaceHolder1_HiddenFieldToastMessage').val();
        var button1Text = $('#ContentPlaceHolder1_HiddenFieldButton1Text').val();
        var button2Text = $('#ContentPlaceHolder1_HiddenFieldButton2Text').val();
        var button1url = $('#ContentPlaceHolder1_HiddenFieldButton1Url').val();
        var button2url = $('#ContentPlaceHolder1_HiddenFieldButton2Url').val();
        var color1 = $('#ContentPlaceHolder1_HiddenFieldButton1Color').val();
        var color2 = $('#ContentPlaceHolder1_HiddenFieldButton2Color').val();
        var smilee = $('#ContentPlaceHolder1_HiddenFieldToastSmiley').val();

    }

    console.log(' hellooooo ' + $("#ContentPlaceHolder1_HiddenFieldFollowText").val());
    $('.nProfileFollowButton').html($("#ContentPlaceHolder1_HiddenFieldFollowText").val());
    //displaying errors of each section 
    //no events
    //$('#ContentPlaceHolder1_HiddenFieldEventsStatus').val('2');
    if ($('#ContentPlaceHolder1_HiddenFieldEventsStatus').val() == '0') {
        $('.nDoneSmileyNoEvents').removeClass('invisible');
        $('.nDoneMessageNoEvents').removeClass('invisible');
        $('.nProfileEventsOuterContainer').addClass('invisible');
    }

    //no reviews
    //$('#ContentPlaceHolder1_HiddenFieldReviewsStatus').val('0');
    if ($('#ContentPlaceHolder1_HiddenFieldReviewsStatus').val() == '0') {
        $('.nDoneSmileyNoReviews').removeClass('invisible');
        $('.nDoneMessageNoReviews').removeClass('invisible');
        $('.nProfileReviewsOuterContainer').addClass('invisible');
    }

    //no followers
    //$('#ContentPlaceHolder1_HiddenFieldFollowersStatus').val('0');
    if ($('#ContentPlaceHolder1_HiddenFieldFollowersStatus').val() == '0') {
        $('.nDoneSmileyNoFollowers').removeClass('invisible');
        $('.nDoneSmileyNoFollowers').removeClass('invisible');
        $('.nProfileFollowersOuterContainer').addClass('invisible');
    }

    //no followings
    //$('#ContentPlaceHolder1_HiddenFieldFollowingStatus').val('0');
    if ($('#ContentPlaceHolder1_HiddenFieldFollowingStatus').val() == '0') {
        $('.nDoneSmileyNoFollowings').removeClass('invisible');
        $('.nDoneSmileyNoFollowings').removeClass('invisible');
        $('.nProfileFollowingsOuterContainer').addClass('invisible');
    }


    // setting bottom button
    // 0 : guest: login / register
    // 1 user: send message
    // 2 owner: edit profile
    //check if it's own profile
    var pageStatus = $('#ContentPlaceHolder1_HiddenFieldButtonStatus').val();
    //pageStatus = '0';
    //guest
    if (pageStatus == '0') {
        $('.nNotButton, .nRequestButton').removeClass('invisible');
        //hiding follow button
        $('.nProfileFollowButton').addClass('invisible');
        //changing dots menu
        $('.nDotsBoxInsideContainerLinks').addClass('invisible');
        $('.nDots').addClass('invisible');
        $('.nDotsBoxInsideContainerEdit').removeClass('invisible');
    } else if (pageStatus == '1') { //user
        $('.nProfileMessageButton').removeClass('invisible');
        $('.nProfileMessageButton span').html('SEND MESSAGE');

    } else if (pageStatus == '2') { //owner
        $('.nProfileMessageButton').removeClass('invisible');
        $('.nProfileMessageButton span').html('EDIT PROFILE');
        //hiding follow button
        $('.nProfileFollowButton').addClass('invisible');
        //changing dots menu
        $('.nDotsBoxInsideContainerLinks').addClass('invisible');
        $('.nDots').addClass('invisible');
        $('.nDotsBoxInsideContainerEdit').removeClass('invisible');
    }
    if ($('#HiddenFieldUsername').val() == $('#ContentPlaceHolder1_LabelUsername').text()) {
        ownProfile = true;
        //changing bottom button
        //$('.nProfileMessageButton').text('Edit Profile');
    }

    //footer buttons
    $('.nFooterButton').click(function () {
        // 0 : guest: login / register
        if (pageStatus == '0') {

        } else if (pageStatus == '1') { // 1 user: send message
            var messageUrl = "../../Messages/" + $('#ContentPlaceHolder1_HiddenFieldUserId').val();
        } else if (pageStatus == '2') { // 2 owner: edit profile
            var messageUrl = "../../Settings";
        }
        location.href = messageUrl;
    })

    $('.nNotButton').click(function () {
        var messageUrl = getRootUrl() + 'Login';
        location.href = messageUrl;
    })
    $('.nRequestButton').click(function () {
        var messageUrl = getRootUrl() + 'Register';
        location.href = messageUrl;
    })

    //setting the rating or ratings word
    if ($('#ContentPlaceHolder1_LabelRateCount').html() > 1) {
        $('.nProfileRatingsWord').html('ratings');
    }
    //clicking on the followers photo
    $('.nProfileFollowersEachContainer,.nProfileFollowingsContainer').click(function () {
        var href = getRootUrl() + 'Profile/' + $(this).find('.nEVparticipantName').attr('href');
        console.log(href);
        window.location.href = href;
    })

    //setting followers pics
    $('.nProfileFollowersPicture, .nProfileFolloweingPicture').each(function () {
        bgURL = "";
        fullURL = "";
        bgURL = $(this).find('input').val();
        var bgURL = bgURL.substring(2);
        fullURL = "url('" + "../../../" + bgURL + "')";
        $(this).css("background-image", fullURL);
    });

    //setting the smilees
    $('.nFBEachLogo').each(function () {
        var rate = $(this).find('span').html();
        if (rate == '1') {
            $(this).html(':((');
        } else if (rate == '2') {
            $(this).html(':(');
        } else if (rate == '3') {
            $(this).html(':|');
        } else if (rate == '4') {
            $(this).html(':)');
        } else if (rate == '5') {
            $(this).html(':D');
        }
    })


    //clicking on reviewer photos
    $('.nProfileEachReview').click(function () {
        var href = $(this).find('.nProfileReviewerName').attr('href');
        window.location.href = href;
    })

    //setting the review images
    $('.nProfileReviewPicture').each(function () {
        bgURL = "";
        fullURL = "";
        bgURL = $(this).find('input').val();
        var bgURL = bgURL.substring(2);
        fullURL = "url('" + "../../../" + bgURL + "')";
        $(this).css("background-image", fullURL);
    });

    //each notification click
    $('.fullNotificationContainer').click(function () {
        var eventLink = $(this).find('.nEventsUrl').attr('href');
        eventLink = eventLink.substring(eventLink.lastIndexOf('/') + 1);
        var url = getRootUrl() + "Events/" + eventLink;
        console.log("url" + url);
        window.location.href = url;
    });
    //setting images of each event
    $('.nEventsPicture').each(function () {
        var bgURL = "url('" + getRootUrl() + "Images/Icons/activity" + $(this).find('input').val() + ".png')";
        //bgURL = 'Images/Icons/activity1.png';
        console.log(bgURL);
        $(this).css('background-image', bgURL);
    });

    //setting cover photo
    $('#ContentPlaceHolder1_HiddenFieldCoverUrl').val();
    var bgURL = "url('../" + $('#ContentPlaceHolder1_HiddenFieldCoverUrl').val() + "')";
    $('.nContentCover').css('background-image', bgURL);
    //report user
    $('.nDotsBoxEachLogoReport').next().click(function () {
        var url = window.location.pathname;
        var urlLastSegment = url.substr(url.lastIndexOf('/') + 1);
        var userId = urlLastSegment;
        var target = getRootUrl() + "Report/User/" + userId;
        window.location.href = target;
    });
    //setting profile page main pic
    var ProfilePictureUrl = $('#ContentPlaceHolder1_HiddenFieldProfilePhoto').val();
    //console.log(ProfilePictureUrl);
    $('.nProfpagePicture').css('background-image', 'url(../../' + ProfilePictureUrl + ')');

    //setting flag background
    var bgURL = $('#ContentPlaceHolder1_HiddenFieldFlagId').val();
    console.log("flag: " + bgURL);
    var fullURL = "url('" + "../Images/Flags/" + bgURL + ".png')";
    $('.nCountryFlag').css("background-image", fullURL);

    //setting verification
    if ($('#ContentPlaceHolder1_HiddenFieldProfileVerified').val() == "False") {
        $('.arrow-right').addClass('hidden');
    }

    var ownProfile = false;
    //showing tab contents
    tabbedContent();
    //$('#ContentPlaceHolder1_LabelUsername').text('asdfs');


    //setting followers pictures
    $('.nMessagesPicture').each(function () {
        bgURL = "";
        fullURL = "";
        bgURL = $(this).find('input').val();
        var bgURL = bgURL.substring(2);
        fullURL = "url('" + "../../../" + bgURL + "')";
        // $(this).css("background-image", fullURL);
    });

    //verified logo button
    $('.nVerified').click(function () {
        tabbedContent(1);
    })

    //followers label button
    $('.nProfileFollowersLabel').click(function () {
        tabbedContent(2);
    })

    //followings label button
    $('.nProfileFollowingsLabel').click(function () {
        tabbedContent(3);
    })


    //displaying no record message
    if ($('#ContentPlaceHolder1_LabelNoRecord').text() != "") {
        $('.nProfileErrorMessage').html($('#ContentPlaceHolder1_LabelNoRecord').text());
    }

    //edit button
    $('.nDotsEditProfile').next().click(function () {
        window.location.href = "../settings/";
    })

    //clicking on profile picture
    $('.nProfpagePicture, .nEVOwnerName').click(function () {
        if ($('#HiddenFieldUsername').val() == $('#ContentPlaceHolder1_LabelUsername').text()) {
            window.location.href = "../settings/";
        } else {
            tabbedContent(1);
        }
    })


    //setting the link of the profile image
    var photoUrl = $('#ContentPlaceHolder1_HiddenFieldProfilePhoto').val();
    //$('.nProfilePicture').css('background-image', 'url(../../' + photoUrl+ ')');

    popUp('.nDotsLogo', '.nDotsBox', true);

    var score = $('#ContentPlaceHolder1_LabelRatePercent').text();
    console.log('score ' + score);
    //canvasId,text,taken, all, radius,width,firstColor,secondColor (all the sizes are in em)
    explore("canvas2", false, score, 100, 3.3, 0.3, "rgb(215,67,46)", null);

    //converting and assigning number of
    var scores_array = [];
    $('.nGraphBar').each(function () {
        var score = $(this).find('span').text();
        //$(this).attr('data-score',score);
        scores_array.push(score);
    })
    var scoreArrayMax = Math.max.apply(Math, scores_array);
    var factor = 100 / scoreArrayMax;
    var counter = 0;
    $('.nGraphBar').each(function () {
        var finalScore = scores_array[counter] * factor;
        $(this).attr('data-score', finalScore);
        counter++;
    });
    console.log("maxxxxxxx" + scoreArrayMax);
    $('.reviewButton').click(function () {
        barGraph();
    });
}


function explorePage() {
    nFooterShortClass = "nFooterShort30";
    screenShortParam = 28; //em
    console.log('toast in being opened');

    //openTheToast("congratulations!!!", '1but', false, 'aprove', 'Events', 'naaaaaaaa', 'close');
    var displayRequestMessage = false;
    //displaying error if the HiddenFieldStatus 1 yani event dare ke neshun bede 0 yani hichi va peighame done
    var displayError = $('#ContentPlaceHolder1_HiddenFieldStatus').val();

    if (displayError == '0') {
        setDoneValues("explore", 0, 0);
        //$('.nExploreFooter').addClass('invisible');
    } else {
        $('.pageNormalContent').removeClass('invisible');
    }

    //triggering action buttons
    $('.nNotButton').click(function () {
        $('#ContentPlaceHolder1_ButtonActionNo').trigger('click');
    });

    $('.nExploreRequestButton').click(function () {
        $('#ContentPlaceHolder1_ButtonActionYes').trigger('click');
        /*if (displayRequestMessage == false) {
            displayRequestMessage = true;
            $('.nFooterButton').addClass('invisible');
            $('.nProfileMessageButton').removeClass('invisible');
            $('.nEVRequestMessageContainer').removeClass('invisible');
        }
        else if (displayRequestMessage == true) {
            if ($('.nEVRequestMessage').val()) {
                $('#ContentPlaceHolder1_ButtonActionYes').trigger('click');
            } else {
                $(".nEVRequestMessageError").removeClass("invisible");
            }
        } */
    });

    //check if there is no event
    if ($('#ContentPlaceHolder1_LabelEventName').text() == "") {
        $('.nDoneSmiley, .nDoneMessage').removeClass('invisible');
        $('.nContentCover, .nTabbedContent').addClass('invisible');
    }

}

//localStorage.getItem("posLat");
//localStorage.getItem("posLon");
function getCurrentLocation() {

    var x = document.getElementById("demo");
    function getLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
        } else {
            x.innerHTML = "Geolocation is not supported by this browser.";
        }
    }
    function showPosition(position) {
        latitude = position.coords.latitude;
        longitude = position.coords.longitude;
        localStorage.setItem("posLat", latitude);
        localStorage.setItem("posLon", longitude);
        //$.cookie("posLat", latitude);
        //$.cookie("posLon", longitude);
    }
    getLocation();
}
//add hashtags to content
function addHashtag(target) {
    //adding hashtags
    hashtag_regexp = /#([a-zA-Z0-9]+)/g;
    function linkHashtags(text) {
        return text.replace(
            hashtag_regexp,
            '<a class="hashtag" href="' + getRootUrl() + 'Search/Tag/$1">#$1</a>'
        );
    }
    $(target).html(linkHashtags($(target).html()));
}


function addGMap(address, title, zoom, markersArray) {
    // google maps
    var map;
    var geocoder;
    var markers = new Array();
    var firstLoc;

    function myGeocodeFirst() {
        geocoder = new google.maps.Geocoder();

        geocoder.geocode({ 'address': address },
          function (results, status) {
              if (status == google.maps.GeocoderStatus.OK) {
                  firstLoc = results[0].geometry.location;
                  map = new google.maps.Map(document.getElementById("map_canvas"),
                  {
                      center: firstLoc,
                      zoom: zoom,
                      mapTypeId: google.maps.MapTypeId.ROADMAP
                  });

                  if (markersArray != "noMarker") {
                      var marker = new google.maps.Marker({
                          position: firstLoc,
                          map: map,
                          title: title
                      });
                  }

              }
              else {
                  console.log("gmaps status" + status);
              }
          }
        );
    }
    myGeocodeFirst();
    // google maps---->

}
// eventsview page
function eventsView() {
    nFooterShortClass = "nFooterShort30";
    screenShortParam = 28; //em
    function sendButtonClicked() {
        var userId = $('#HiddenFieldUserId').val();
        var eventId = $('#ContentPlaceHolder1_HiddenFieldEventId').val();
        console.log('it should be changed');
        var htmlcode = '<div class="eachMessage nRight">\
                    <div class="nMessagesPicture nMessagesShowPicture nMSOtherSenderPicture" style="background-image: url('+ getRootUrl() + 'Files/ProfilesPhotos/' + userId + '-100.jpg);">\
                        <div class="nMessagesUrl invisible">'+ userId + '</div>\
                        <input type="hidden" name="ctl00$ContentPlaceHolder1$RepeaterMessages$ctl39$HiddenFieldProfilePicUrl" id="ContentPlaceHolder1_RepeaterMessages_HiddenFieldProfilePicUrl_39" value="~/Files/ProfilesPhotos/9-100.jpg">\
                        <input type="hidden" name="ctl00$ContentPlaceHolder1$RepeaterMessages$ctl39$HiddenFieldUserId" id="ContentPlaceHolder1_RepeaterMessages_HiddenFieldUserId_39" value="9">\
                    </div>\
                    <div>\
                    </div>\
                    <div class="nMessageBody nMessageBodyOther">\
                        <span>' + $('.nMSTextArea textarea').val() + '</span>\
                        <div class="nMessageDecoration nMessageDecorationOther"></div>\
                    </div>\
                    <!--<span id="ContentPlaceHolder1_RepeaterMessages_LabelUnread_39" class="EachMessageUnread ViewClass">0</span> -->\
                    <div class="invisible">\
                        <span id="ContentPlaceHolder1_RepeaterMessages_LabelSender_39" class="EachMessageSender">True</span>\
                    </div>\
                    <div class="clear"></div>\
                    <div class="MessageListDate nMSDate MessageListDateOther">\
                        <span id="ContentPlaceHolder1_RepeaterMessages_LabelDate_39" class="EachMessageDate ViewClass">.</span>\
                    </div>\
                </div>';
        $('.nEVMessagesContainer').append(htmlcode);
        //$('.nMSMessagesContainer').scrollTop($('.nMSMessagesContainer').prop("scrollHeight"));
        $(".nEVMessagesContainer").animate({ scrollTop: $('.nEVMessagesContainer')[0].scrollHeight }, 1000);
        $('.nMSTextArea textarea').val('');
        $('.nMSTextArea textarea').focus();
    }

    //signalR
    var chat = $.connection.chatHub;
    $.connection.hub.start().done(function () {
        //change in the country detected
        $//clicking on send message button
        $('.nMSSendButton').click(function () {
            //$('#ContentPlaceHolder1_ButtonBoardMessageAdd').trigger('click');
            if ($('#ContentPlaceHolder1_TextBoxBoardMessageAdd').val() != "") {
                var userId = $('#HiddenFieldUserId').val();
                var eventId = $('#ContentPlaceHolder1_HiddenFieldEventId').val();
                $('.nDoneMessageNoMessage').addClass("invisible");
                console.log(eventId, userId, $('#ContentPlaceHolder1_TextBoxBoardMessageAdd').val());

                chat.server.sendBoardMessage(eventId, userId, $('#ContentPlaceHolder1_TextBoxBoardMessageAdd').val()).done(function (result) {
                    if (result) {
                        sendButtonClicked();
                    }
                })
            }
        })
        //prevent Enter Key
        $(window).keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                if ($('.nMSTextArea textarea').val() != "") {
                    var userId = $('#HiddenFieldUserId').val();
                    var eventId = $('#ContentPlaceHolder1_HiddenFieldEventId').val();
                    $('.nDoneMessageNoMessage').addClass("invisible");
                    //$(".invisible").find('input').trigger("click");
                    chat.server.sendBoardMessage(eventId, userId, $('#ContentPlaceHolder1_TextBoxBoardMessageAdd').val()).done(function (result) {
                        if (result) {
                            sendButtonClicked();
                        }
                    })
                }
                return false;
            }
        });


    })

    var completeAddress = $('#ContentPlaceHolder1_LabelAddress').html() + " " + $('#ContentPlaceHolder1_LabelLocation').html();
    if ($('#ContentPlaceHolder1_LabelAddress').html() != "") {
        console.log("first case");
        addGMap(completeAddress, $("#ContentPlaceHolder1_LabelEventName").val(), 14, "smthng");
    } else {
        console.log("second case");
        addGMap(completeAddress, $("#ContentPlaceHolder1_LabelEventName").val(), 12, "noMarker");
    }


    //setting the address filler
    if ($('#ContentPlaceHolder1_LabelAddress').html() != "") {
        $('.nEVAddressFiller').html("&nbsp;-&nbsp;");
    }

    //removing participants
    //ContentPlaceHolder1_HiddenFieldParticipantValue should be set to the id of the user
    var ownerId = $('#ContentPlaceHolder1_HiddenFieldOwnerId').val();
    $('.nEVX').each(function () {
        if (ownerId == $(this).attr('data-id')) {
            $(this).addClass('invisible');
        }
    })
    $('.nEVX').click(function () {
        console.log('id: ' + $(this).attr('data-id'));
        var id = $(this).attr('data-id');
        $('#ContentPlaceHolder1_HiddenFieldParticipantValue').val(id);
        openTheToast('Do you really want to remove this user?', ':(', '2but', false, 'Cancel', 'close', 'url',
            null, 'Remove', '#ContentPlaceHolder1_ButtonRemoveParticipant', 'action', null);
        //$('#ContentPlaceHolder1_ButtonRemoveParticipant').trigger('click');
    });


    // message, type(1but or 2but), closable (bool),button1Text, button1url, button1Type(url, action), button2Text, button2url, button2Type(url, action)
    if ($('#ContentPlaceHolder1_HiddenFieldToastStatus').val() == '1') {
        var message = $('#ContentPlaceHolder1_HiddenFieldToastMessage').val();
        var button1Text = $('#ContentPlaceHolder1_HiddenFieldButton1Text').val();
        var button2Text = $('#ContentPlaceHolder1_HiddenFieldButton2Text').val();
        var button1url = $('#ContentPlaceHolder1_HiddenFieldButton1Url').val();
        var button2url = $('#ContentPlaceHolder1_HiddenFieldButton2Url').val();
        var color1 = $('#ContentPlaceHolder1_HiddenFieldButton1Color').val();
        var color2 = $('#ContentPlaceHolder1_HiddenFieldButton2Color').val();
        var smilee = $('#ContentPlaceHolder1_HiddenFieldToastSmiley').val();
        openTheToast(message, smilee, '1but', false, button1Text, 'close', 'url', color1, button2Text, button2url, 'url', color2);
    }

    //setting the name of the event
    $('.nEVEventName').html($('#ContentPlaceHolder1_HiddenFieldOwnerFullname').val());

    //displaying or hiding messages,send message button and participants 
    //0 peighame inke participant nisti request bede
    //1 namayesh bede va betoone message add kone 
    var displayChat = $('#ContentPlaceHolder1_HiddenFieldBoardStatus').val();
    if (displayChat == '0') {
        //chat section
        $('.nEVMessagesContainer, .nEVPanel, .nParticipantsListContainer').addClass('invisible');
        $('.nDoneSmileyNotAllowed, .nDoneMessageNotAllowed').removeClass('invisible');
    }

    //displaying the error if there is no message
    if (!$.trim($('.nEVMessagesContainerInside').html()).length && displayChat == '1') {
        console.log('test1' + !$.trim($('.nEVMessagesContainer').html()).length);
        console.log('test2' + displayChat);
        $('.nDoneSmileyNoMessage').removeClass('invisible');
        $('.nDoneMessageNoMessage').removeClass('invisible');
    }
    //displaying the error if there is no participants
    if ($('#ContentPlaceHolder1_LabelNoRecord').text() == 'No participants yet!') {
        $('.nDoneSmileyParticipants').removeClass('invisible');
        $('.nDoneMessageParticipants').removeClass('invisible');
    }


    //setting the correct tab
    currentTab = '0';
    //if nTabs3 ContentPlaceHolder1_HiddenFieldStep
    console.log('initial ($(#ContentPlaceHolder1_HiddenFieldStep).val()' + $('#ContentPlaceHolder1_HiddenFieldStep').val());
    if ($('#ContentPlaceHolder1_HiddenFieldStep').val() == '1') {
        currentTab = '0';
    } else if ($('#ContentPlaceHolder1_HiddenFieldStep').val() == '2') {
        currentTab = '1';
        if (displayChat != '0' && $('.nTabs3 [ntabnumber="2"]').hasClass('nExploreSelected')) {
            $('.nEVPanel').removeClass('invisible');
        }
    } else if ($('#ContentPlaceHolder1_HiddenFieldStep').val() == '3') {
        currentTab = '2';
    }

    $('.nTabs4').click(function () {
        var tabNumber = $(this).attr('ntabnumber');
        console.log('tabNumber ' + tabNumber);
        $('#ContentPlaceHolder1_HiddenFieldStep').val(tabNumber);
        console.log('set to ($(#ContentPlaceHolder1_HiddenFieldStep).val()' + $('#ContentPlaceHolder1_HiddenFieldStep').val());

        //showing and hiding message sending box
        //&& displayChat == '0'
        if (tabNumber != '2') {
            console.log('displayChat ' + displayChat);
            $('.nEVPanel').addClass('invisible');
        } else if (displayChat != '0') {
            console.log('displayChat ' + displayChat);
            $('.nEVPanel').removeClass('invisible');
            $(".nEVMessagesContainer").animate({ scrollTop: $('.nEVMessagesContainer')[0].scrollHeight }, 1000);
        }
    })

    addHashtag('.nDescriptionContent');

    //setting cover background
    var typeId = $('#ContentPlaceHolder1_HiddenFieldCoverId').val();
    var coverUrl = getRootUrl() + "Images/Covers/" + typeId + ".jpg";
    $('.nContentCover').css('background-image', 'url(' + coverUrl + ')');


    //seting footer buttons values
    //age 0 bood payin 1 button “Login / Register” link ~/Login, 
    //age 1 bood “Edit Event” link ~/Events/Modify/{EventId} 
    //age 2 bood hamoon buttone send request
    //age 3 user request dade
    //age 4 user participante (Cancel Participation) [faghat ButtonButtonRequest_Click trigger she]
    var viewMode = $('#ContentPlaceHolder1_HiddenFieldButtonStatus').val();
    //clicking on edit event
    var displayRequestMessage = false;
    $('.nProfileMessageButton').click(function () {
        if (viewMode == '1') {
            var eventID = $('#ContentPlaceHolder1_HiddenFieldEventId').val();
            var url = getRootUrl() + "Events/Modify/" + eventID;
            window.location = url;
        }
        else if (viewMode == '2') {
            /*if (displayRequestMessage == false) {
                displayRequestMessage = true;
                $('.nProfileMessageButton').html('CONFIRM REQUEST');
                $('.nEVRequestMessageContainer').removeClass('invisible');
            }
            else if (displayRequestMessage == true) {
                if ($('.nEVRequestMessage').val()) {
                    $("#ContentPlaceHolder1_ButtonRequest").trigger("click");
                } else {
                    $(".nEVRequestMessageError").removeClass("invisible");
                }
            }*/
            $("#ContentPlaceHolder1_ButtonRequest").trigger("click");
        } else if (viewMode == '3' || viewMode == '4') {
            $("#ContentPlaceHolder1_ButtonRequest").trigger("click");
        }
    })
    if (viewMode == "0") {
        $('.nNotButton').removeClass('invisible');
        $('.nRequestButton').removeClass('invisible');
        $('.nNotButton span').html('LOGIN');
        $('.nRequestButton').html('REGISTER');
        //hiding dots button
        $('.nDotsBoxEachLogoBookmark, .nEVbookmarkButton, .nDotsBoxEachLogoReport, .nEVReportButton').addClass('invisible');
    }
    else if (viewMode == "1") {
        $('.nProfileMessageButton').removeClass('invisible');
    }
    else if (viewMode == "2") {
        $('.nProfileMessageButton').removeClass('invisible');
        $('.nProfileMessageButton span').html('SEND REQUEST');
        $('.nFooter1Image').removeClass('nProfileImageGrey');
        $('.nFooter1Image').addClass('nProfileImageGreen');
        $('.nFooterButton').addClass('nRequestButton');
    }
    else if (viewMode == "3") {
        $('.nProfileMessageButton').removeClass('invisible');
        $('.nProfileMessageButton span').html('CANCEL REQUEST');
    }
    else if (viewMode == "4") {
        $('.nProfileMessageButton').removeClass('invisible');
        $('.nProfileMessageButton span').html('CANCEL PARTICIPATION');
    }
    //console.log(localStorage.getItem("posLat"));
    //setting bookmark text
    $('.nEVbookmarkButton').text($('#ContentPlaceHolder1_ButtonBookmark').val());
    console.log('currentTabcurrentTabcurrentTabcurrentTab' + currentTab);
    console.log('currentTabcurrentTabcurrentTabcurrentTab2' + $('#ContentPlaceHolder1_HiddenFieldStep').val());

    tabbedContent(currentTab);
    var isMenuCloseble = false;

    //setting event creator image
    var imageID = $('#ContentPlaceHolder1_HiddenFieldOwnerPhotoUrl').val();
    //console.log(activityId);
    $('.nEVOwnerPicture').css('background-image', 'url(../../' + imageID + ')');

    //clicking on owner picture
    $('.nEVOwnerPicture').click(function () {
        console.log($('#ContentPlaceHolder1_HiddenFieldOwnerId').val());
        var ownerUrl = "../../profile/" + $('#ContentPlaceHolder1_HiddenFieldOwnerId').val();
        console.log(ownerUrl);
        location.href = ownerUrl;
    })

    //clicking on paticipants graph
    $('#EVcanvas').click(function () {
        if ($('.nPageTitle').text() != 'Explore') {
            tabbedContent(2);
        }
    })

    //click on messages photo
    $(document).on("click", ".nMessagesPicture", function () {
        var ownerUrl = "../../profile/" + $(this).find('.nEVIid').find('input').val();
        location.href = ownerUrl;
    });

    //setting different messages styles
    //setting bg color of messeages
    $('.eachMessage').each(function () {
        // if it is not owner
        if ($(this).find('.nEVIsOwner').find('input').val() == "false") {
            $(this).addClass('nRight');
            $(this).find('.nMessagesPicture').addClass('nMSOtherSenderPicture');
            $(this).find('.nMessageBody').addClass('nMessageBodyOther');
            $(this).find('.nMessageDecoration').addClass('nMessageDecorationOther');
            $(this).find('.MessageListDate').addClass('nEVMessageTime');
        }
    });

    $('.nDotsBoxShare').click(function () {
        popUp(".nDotsLogo", ".nDotsBox", false);
    })

    var fbLink;
    var twitterLink;
    if ($('.nPageTitle').text() == "Explore") {
        var eventID = $('#ContentPlaceHolder1_HiddenFieldEventId').val();
        fbLink = "http://www.facebook.com/share.php?u=" + getRootUrl() + "Events/" + eventID;
        twitterLink = "http://twitter.com/home?status=" + getRootUrl() + "Events/" + eventID;
    } else {
        fbLink = "http://www.facebook.com/share.php?u=" + document.URL;
        twitterLink = "http://twitter.com/home?status=" + document.URL;
    }
    $('.nEVShareButtonFB').attr('href', fbLink);
    $('.nEVShareButtonTwitter').attr('href', twitterLink);

    var imgDim = px(3);
    $('.nEVshareImg').attr('width', imgDim);
    $('.nEVshareImg').attr('height', imgDim);

    $('.nEVshareImg').click(function () {
        $('.nDotsBox').addClass("hidden");
        dotsOpened = false;
        $('.nDotBoxLinks').removeClass('hide');
        $('.nDotsBoxShare').addClass('hide');
    })

    $('.nDotsBoxEachLogoShare').next().click(function () {
        $('.nDotBoxLinks').addClass('hide');
        $('.nDotsBoxShare').removeClass('hide');
    })

    //report
    var url = window.location.pathname;
    var urlLastSegment = url.substr(url.lastIndexOf('/') + 1);
    var eventId = urlLastSegment;
    $('.nDotsBoxEachLogoReport').next().click(function () {
        //~/Report/Events/EventId
        var reportLink = "../../Report/Event/" + eventId;
        console.log(reportLink);
        window.location.href = reportLink;
    })

    var activityId = $('#ContentPlaceHolder1_HiddenFieldTypeId').val();
    //console.log(activityId);
    $('.nActivityPicture').css('background-image', 'url(../../Images/Icons/activity' + activityId + '.png)');

    //convertDate("#ContentPlaceHolder1_HiddenFieldDate", ".nEVdate");
    $('.nEVdate').html($('#ContentPlaceHolder1_HiddenFieldDate').val());

    $(".nEVbookmarkButton").click(function () {
        $("#ContentPlaceHolder1_ButtonBookmark").trigger("click");
    });

    $('.nMessagesPicture').each(function () {
        bgURL = "";
        fullURL = "";
        bgURL = $(this).find('input').val();
        var bgURL = bgURL.substring(2);
        fullURL = "url('" + "../../../" + bgURL + "')";
        $(this).css("background-image", fullURL);
    });

    popUp(".nDotsLogo", ".nDotsBox", false);

    var participants = $('#ContentPlaceHolder1_LabelParticipantsAvailable').text();
    $('.nParticipantAvailable').text(participants);

    //footer buttons actions
    $(".nRequestButton").click(function () {
        //guest
        if (viewMode == '0') {
            var url = getRootUrl() + "Register";
            window.location.href = url;
        }
            //owner
        else if (viewMode == '1') {

        }
            //user
        else if (viewMode == '2') {

        }

    });
    $(".nNotButton").click(function () {
        //guest
        if (viewMode == '0') {
            var url = getRootUrl() + "Login";
            window.location.href = url;
        }
            //owner
        else if (viewMode == '1') {

        }
            //user
        else if (viewMode == '2') {
            $("#ContentPlaceHolder1_ButtonActionNo").trigger("click");
        }

    });

    $(".hiddenButtons").addClass("invisible");

    var score = $('#ContentPlaceHolder1_HiddenFieldOwnerRateScore').val();
    //canvasId,text,taken, all, radius,width,firstColor,secondColor (all the sizes are in em)
    explore("canvas2", false, score, 100, 2.3, 0.3, "rgb(215,67,46)", null);


    /*if ($('.nPageTitle').text() == 'Explore') {
        var all = $('#ContentPlaceHolder1_HiddenFieldParticipants').val();
        var participants = $('#ContentPlaceHolder1_HiddenFieldParticipantsAvailable').val();
        //setting cover
        //var coverId = $('#ContentPlaceHolder1_HiddenFieldCoverId').val();
        //$('.nContentCover').css('background-image', 'url(../../Images/Covers/' + coverId + '.jpg)');
    } else {
    */
    var all = $('#ContentPlaceHolder1_LabelParticipants').text();
    var participants = $('#ContentPlaceHolder1_LabelParticipantsAvailable').text();
    //var EventPictureID = $('#ContentPlaceHolder1_HiddenFieldCoverId').val();
    //$('.nContentCover').css('background-image', 'url(../../images/event' + EventPictureID + '.jpg)');
    //}
    console.log("participants " + participants);
    console.log("all " + all);
    //canvasId,text,taken, all, radius,width,firstColor,secondColor (all the sizes are in em)
    explore("EVcanvas", false, participants, all, 2.05, 0.2, "rgb(52,108,15)", null);
}

function setPositions() {
    $(".nAll").css("height", window.innerHeight);
    var mainLeft = $('#nMainPanelLi').offset().left;
    var ulLeft = $('.nAll').offset().left;
    $('.nAll').offset({ left: ulLeft - mainLeft });
}
function footerFix(ScreenParam, desiredClass) {
    console.log("footerFix called");
    //function windowResized() {

    //    console.log(ScreenParam + ' ScreenPara');
    //    if (ScreenParam && ScreenParam !== "null" && ScreenParam !== "undefined") {
    //    } else {
    //        ScreenParam = 32;
    //    }
    //    if (desiredClass && desiredClass !== "null" && desiredClass !== "undefined") {
    //    } else {
    //        desiredClass = "nFooterShort35";
    //    }
    //    console.log('window.height ' + $(window).height() + ' screenpa ' + ScreenParam + ' screenpa px' + px(ScreenParam));
    //    if ($(window).height() > px(ScreenParam)) {
    //        console.log("bigger");
    //        $('.nLoginFooter').removeClass(desiredClass);
    //        $('.nLoginFooter').removeClass("nFooterShort");
    //        $('.nExploreFooter').removeClass(desiredClass);
    //        $('.nExploreFooter').removeClass("nFooterShort");
    //        $('.nExploreContentHolder').removeClass("nExploreContentHolderShortSc");
    //        $('.nMainPanelDiv').removeClass("nMainPanelDivShortSc");
    //        $('.nMSMessagesContainer').removeClass("nMSMessagesContainerShortSc");

    //    } else {
    //        console.log("smaller");
    //        $('.nLoginFooter').addClass(desiredClass);
    //        $('.nLoginFooter').addClass("nFooterShort");
    //        $('.nExploreFooter').addClass(desiredClass);
    //        $('.nExploreFooter').addClass("nFooterShort");
    //        $('.nExploreContentHolder').addClass("nExploreContentHolderShortSc");
    //        $('.nMainPanelDiv').addClass("nMainPanelDivShortSc");
    //        $('.nMSMessagesContainer').addClass("nMSMessagesContainerShortSc");
    //    }
    //}
    //$(window).resize(function () {
    //    windowResized();
    //});
    //windowResized();
}
function addAnimations() {
    $('#nMainPanelLi, .nAll, .nExploreFooter, .nTabs , .nTabs b, .nNewGraphTop, .nNewGraphBottom').addClass('animationAll');
}
function closeTheToast() {
    $('.nToastBG').addClass('invisible');
}
// message,smilee, type(1but or 2but), closable (bool),button1Text, button1url, button1Type(url, action), button1Color, button2Text, button2url, button2Type(url, action),button1Color
// set the url to 'close' to close the toast
function openTheToast(message, smilee, butType, closable, but1txt, but1link, but1Type, but1Color, but2txt, but2link, but2Type, but2Color) {
    $('.nToastSmilee').html(smilee);
    $('.nToastBG').removeClass('invisible');
    $('.nToastMessage').html(message)
    if (but1Color != null) {
        $('.nToastFooterButtonlong , .nToastFooterButtonShort1').css('background-color', '#' + but1Color);
    }
    if (but2Color != null) {
        $('.nToastFooterButtonShort2').css('background-color', '#' + but2Color);
    }
    if (butType == '1but') {
        $('.nToast').addClass('nToast1but');
        $('.nToastFooterButtonlong').removeClass('invisible');
        $('.nToastFooterButtonlong').html(but1txt);
    } else if (butType == '2but') {
        $('.nToastFooterButtonShort1, .nToastFooterButtonShort2').removeClass('invisible');
        $('.nToastFooterButtonShort1').html(but1txt);
        $('.nToastFooterButtonShort2').html(but2txt);
    }
    $('.nToastFooterButtonlong').click(function () {
        if (but1link != 'close') {
            if (but1Type == 'url') {
                var url = getRootUrl() + but1link;
                location.href = url;
            } else if (but1Type == 'action') {
                var Button = but1link;
                $(Button).trigger('click');
            }
        } else {
            closeTheToast();
        }
    });
    $('.nToastFooterButtonShort1').click(function () {
        if (but1link != 'close') {
            if (but1Type == 'url') {
                var url = getRootUrl() + but1link;
                location.href = url;
            } else if (but1Type == 'action') {
                var Button = but1link;
                $(Button).trigger('click');
            }
        } else {
            closeTheToast();
        }
    });
    $('.nToastFooterButtonShort2').click(function () {
        console.log("but2link " + but2link);
        console.log("but2Type: " + but2Type);
        if (but2link != 'close') {
            if (but2Type == 'url') {
                var url = getRootUrl() + but1link;
                location.href = url;
            } else if (but2Type == 'action') {
                console.log('this should work!');
                var Button = but2link;
                $(Button).trigger("click");
            }
        }
        else { closeTheToast(); }
    });
    $('.nToast').click(function () {
        return false;
    });
    $('.nToastBG').click(function () {
        if (closable) { closeTheToast(); }
    });


}
function generalLook() {
    var hasUserOpenedMenu = false;

    var screenHeight = $(window).height() + 'px';
    $('.nAll').css("height", screenHeight);
    //footerFix();

    if ($('[type="date"]').prop('type') != 'date') {
        $('[type="date"]').datepicker();
    }
    setTimeout(function () { addAnimations(); }, 500);
    var toastOpen = false;

    var isInitialState = true;
    var isMenuOpen = false;
    var wideScreenParam = 800; //px


    console.log('isMenuEnabled :' + isMenuEnabled);
    if ($(window).width() > wideScreenParam && isMenuEnabled == true) {
        isMenuOpen = true;
    }
    var isWideScreen = false;
    var isShortScreen = false;
    var isMenuShort = false;
    var isDoneShort = false;

    var menuShortParam = 28; //em
    var doneShortParam = 28; //em


    // to do: set wide screen , short screen and menu open in the initiation
    //open the window if 
    function setTheLook(tmpIsWideScreen, tmpIsShortScreen) {
        //console.log("set the look called. tmpIsWideScreen: " + tmpIsWideScreen +
        //  ". tmpIsShortScreen: " + tmpIsShortScreen + ". isMenuOpen: " + isMenuOpen);
        var displayMode = -1;
        if (isMenuOpen) {
            if (tmpIsShortScreen) {
                if (tmpIsWideScreen) {
                    // isMenuOpen, isShortScreen, isWideScreen
                    displayMode = 0;
                } else {
                    // isMenuOpen, isShortScreen, !isWideScreen
                    displayMode = 1;
                }
            }
            else {
                if (tmpIsWideScreen) {
                    // isMenuOpen, !isShortScreen, isWideScreen
                    displayMode = 2;
                } else {
                    // isMenuOpen, !isShortScreen, !isWideScreen
                    displayMode = 3;
                }
            }
        }
        else {
            if (tmpIsShortScreen) {
                if (tmpIsWideScreen) {
                    // !isMenuOpen, isShortScreen, isWideScreen
                    displayMode = 4;
                } else {
                    // !isMenuOpen, isShortScreen, !isWideScreen
                    displayMode = 5;
                }
            }
            else {
                if (tmpIsWideScreen) {
                    // !isMenuOpen, !isShortScreen, isWideScreen
                    displayMode = 6;
                } else {
                    // !isMenuOpen, !isShortScreen, !isWideScreen
                    displayMode = 7;
                }
            }
        }
        console.log("displayMode :" + displayMode);
        switch (displayMode) {
            case 0:
                var mainLeft = $('#nMainPanelLi').offset().left;
                var ulLeft = $('.nAll').offset().left;
                $('.nExploreFooter').addClass(nFooterShortClass);
                $('.nExploreFooter').addClass("nFooterShort");
                $('.nExploreFooter').css("left", "0");
                $('.nAll').offset({ left: 0 });
                $('#nMainPanelLi').addClass('nMainPanelLiWideScreen');
                $('.nExploreFooter').removeClass('nFooterWideScreen');
                $('.nMSPanel').addClass('nFooterWideScreen');
                $('.nEVTabbedContent').addClass('nEVTabbedContentShortSc');
                $('.nProfileTabbedContent').addClass('nProfileTabbedContentShortSc');
                break;
            case 1:
                var mainLeft = $('#nMainPanelLi').offset().left;
                var ulLeft = $('.nAll').offset().left;
                $('.nExploreFooter').addClass(nFooterShortClass);
                $('.nExploreFooter').addClass("nFooterShort");
                $('.nExploreFooter').css("left", "0");
                $('.nAll').offset({ left: 0 });
                $('.nEVTabbedContent').addClass('nEVTabbedContentShortSc');
                $('.nProfileTabbedContent').addClass('nProfileTabbedContentShortSc');
                break;
            case 2:
                var mainLeft = $('#nMainPanelLi').offset().left;
                var ulLeft = $('.nAll').offset().left;
                $('.nAll').offset({ left: 0 });
                $('.nExploreFooter').removeClass(nFooterShortClass);
                $('.nExploreFooter').removeClass("nFooterShort");
                $('.nExploreFooter').offset({ left: px(16) });
                $('#nMainPanelLi').addClass('nMainPanelLiWideScreen');
                $('.nExploreFooter').addClass('nFooterWideScreen');
                $('.nMSPanel').addClass('nFooterWideScreen');
                $('.nEVTabbedContent').removeClass('nEVTabbedContentShortSc');
                $('.nProfileTabbedContent').removeClass('nProfileTabbedContentShortSc');
                break;
            case 3:
                var mainLeft = $('#nMainPanelLi').offset().left;
                var ulLeft = $('.nAll').offset().left;
                $('.nAll').offset({ left: 0 });
                $('.nExploreFooter').removeClass(nFooterShortClass);
                $('.nExploreFooter').removeClass("nFooterShort");
                $('.nExploreFooter').offset({ left: px(16) });
                $('#nMainPanelLi').removeClass('nMainPanelLiWideScreen');
                $('.nExploreFooter').removeClass('nFooterWideScreen');
                $('.nMSPanel').removeClass('nFooterWideScreen');
                $('.nEVTabbedContent').removeClass('nEVTabbedContentShortSc');
                $('.nProfileTabbedContent').removeClass('nProfileTabbedContentShortSc');
                break;
            case 4:
                var mainLeft = $('#nMainPanelLi').offset().left;
                var ulLeft = $('.nAll').offset().left;
                $('.nAll').offset({ left: ulLeft - mainLeft });
                $('.nExploreFooter').css("left", "0");
                $('.nThickFooter').addClass(nFooterShortClass);
                $('.nThickFooter').addClass("nFooterShort");
                $('#nMainPanelLi').removeClass('nMainPanelLiWideScreen');
                $('.nEVTabbedContent').addClass('nEVTabbedContentShortSc');
                $('.nProfileTabbedContent').addClass('nProfileTabbedContentShortSc');
                break;
            case 5:
                var mainLeft = $('#nMainPanelLi').offset().left;
                var ulLeft = $('.nAll').offset().left;
                $('.nAll').offset({ left: ulLeft - mainLeft });
                $('.nExploreFooter').addClass(nFooterShortClass);
                $('.nExploreFooter').addClass("nFooterShort");
                $('.nExploreFooter').removeClass("nFooterWideScreen");
                $('.nMSPanel').removeClass("nFooterWideScreen");
                $('#nMainPanelLi').removeClass('nMainPanelLiWideScreen');
                $('.nEVTabbedContent').addClass('nEVTabbedContentShortSc');
                $('.nProfileTabbedContent').addClass('nProfileTabbedContentShortSc');
                break;
            case 6:
                var mainLeft = $('#nMainPanelLi').offset().left;
                var ulLeft = $('.nAll').offset().left;
                $('.nAll').offset({ left: ulLeft - mainLeft });
                $('.nThickFooter').offset({ left: 0 });
                $('.nExploreFooter').offset({ left: 0 });
                $('.nExploreFooter').removeClass('nFooterWideScreen');
                $('.nMSPanel').removeClass('nFooterWideScreen');
                $('.nThickFooter').removeClass(nFooterShortClass);
                $('.nExploreFooter').removeClass("nFooterShort");
                $('#nMainPanelLi').removeClass('nMainPanelLiWideScreen');
                $('.nEVTabbedContent').removeClass('nEVTabbedContentShortSc');
                $('.nProfileTabbedContent').removeClass('nProfileTabbedContentShortSc');
                break;
            case 7:
                var mainLeft = $('#nMainPanelLi').offset().left;
                var ulLeft = $('.nAll').offset().left;
                $('.nAll').offset({ left: ulLeft - mainLeft });
                $('.nAll').css
                $('.nThickFooter').offset({ left: 0 });
                $('.nExploreFooter').offset({ left: 0 });
                $('#nMainPanelLi').removeClass('nMainPanelLiWideScreen');
                $('.nExploreFooter').removeClass('nFooterWideScreen');
                $('.nMSPanel').removeClass('nFooterWideScreen');
                $('.nExploreFooter').removeClass(nFooterShortClass);
                $('.nExploreFooter').removeClass("nFooterShort");
                $('.nEVTabbedContent').removeClass('nEVTabbedContentShortSc');
                $('.nProfileTabbedContent').removeClass('nProfileTabbedContentShortSc');
                break;
        }

    }
    function setTheMenu(tmpIsMenuShort) {
        if (tmpIsMenuShort) {
            $('.logoutPanel').removeClass('logoutPanelShortScreen');
        } else {
            $('.logoutPanel').addClass('logoutPanelShortScreen');
        }
    };

    function setTheDone(tmpIsDoneShort) {
        if (tmpIsDoneShort) {
            $('.nDoneAllButtons').removeClass('nDoneAllButtonsShortScreen');
        } else {
            $('.nDoneAllButtons').addClass('nDoneAllButtonsShortScreen');
        }
    };
    //this is the only function to be called to change the look
    function windowResized() {
        var screenHeight = $(window).height() + 'px';
        $('.nAll').css("height", screenHeight);
        //to do: detect widescreen, short screen,
        //set iswidescreen 
        var tmpIsWideScreen;
        var tmpIsShortScreen;
        var tmpIsMenuShort;
        var tmpIsMenuOpen;
        var tmpIsDoneShort;
        if ($(window).width() > wideScreenParam) {
            tmpIsWideScreen = true;
        } else {
            tmpIsWideScreen = false;
            console.log("tmpIsMenuOpen : " + tmpIsMenuOpen + "isMenuOpen : " + isMenuOpen);
            if (isMenuOpen && !hasUserOpenedMenu) {
                isMenuOpen = !isMenuOpen;
            }
        }

        //set isshortscreen
        if ($(window).height() < px(screenShortParam)) {
            tmpIsShortScreen = true;
        } else {
            tmpIsShortScreen = false;
        }

        if ($(window).height() > px(menuShortParam)) {
            tmpIsMenuShort = true;
        }

        if ($(window).height() > px(menuShortParam)) {
            tmpIsMenuShort = true;
        }

        if ($(window).height() > px(doneShortParam)) {
            tmpIsDoneShort = true;
        }
        //detecting the change in state of variables and trigger the actions
        //widescreen has been changed
        if (isInitialState) {
            setTheLook(tmpIsWideScreen, tmpIsShortScreen);
            setTheMenu(tmpIsMenuShort);
            setTheDone(tmpIsDoneShort);
            isInitialState = false;
        } else {

            if (tmpIsWideScreen != isWideScreen || tmpIsShortScreen != isShortScreen || tmpIsMenuOpen != isMenuOpen) {
                isWideScreen = tmpIsWideScreen;
                isShortScreen = tmpIsShortScreen;
                tmpIsMenuOpen = isMenuOpen;
                setTheLook(tmpIsWideScreen, tmpIsShortScreen);
            }
            if (tmpIsMenuShort != isMenuShort) {
                setTheMenu(tmpIsMenuShort);
            }
            if (tmpIsDoneShort != isDoneShort) {
                setTheDone(tmpIsDoneShort);
            }
        }

    }

    //initial settings of the dispaly
    $(window).resize(function () {
        windowResized();
    });
    windowResized();

    //setting the main div position and closing the menu in wide screens
    $('.nMenuButton').click(function () {
        //console.log(isMenuOpen);
        if (isMenuOpen == true) {
            isMenuOpen = false;
        }
        else if (isMenuOpen == false) {
            isMenuOpen = true;
            hasUserOpenedMenu = true;
            setTimeout(function () { hasUserOpenedMenu = false }, 1000);
        }
        windowResized();
    });

    //closing the menu if the page is not wide!
    $('.pageNormalContent').click(function (event) {
        //console.log(isMenuOpen);
        if (isMenuOpen == true && $(window).width() < wideScreenParam) {
            isMenuOpen = false;
            windowResized();
            event.stopPropagation();
            return false;
        }

    });
}

function newGraph(taken, all, topElement, botElement) {
    var botRatio = 100 * taken / all;
    var topRatio = 100 * (1 - (taken / all));
    console.log(topRatio);
    $(botElement).css("height", botRatio + "%");
    $(topElement).css("height", topRatio + "%");
}

//drawing canvases on explore page
//radius and width would be in em
function explore(canvasName, textBool, taken, all, radius, width, firstColor, secondColor) {
    if (!taken || isNaN(taken)) {
        taken = 0.1;
    }
    //console.log($('#HiddenFieldOwnerId').val());
    var currentEndAngle = -0.5;
    var currentStartAngle = -0.5;
    var lineRadius = px(radius);

    var lineWidth = px(width);
    var canvasWidth = 2 * (lineRadius + lineWidth);
    var canvas = document.getElementById(canvasName);


    //setting canvas size
    $(canvas).prop("width", canvasWidth);
    $(canvas).prop("height", canvasWidth);



    var context = canvas.getContext("2d");
    if (textBool) {
        context.font = "1em Arial";
        context.fillStyle = 'lightblue';
        context.fillText(taken + "/" + all, canvasWidth / 4, 3 * canvasWidth / 5);
    }
    var myVar = setInterval(function () { draw() }, 5);

    //drawing the circle
    function draw() { /***************/

        var x = canvas.width / 2;
        var y = canvas.height / 2;
        var radius;
        var width;
        var secondColorPoint = 2 * taken / all - 0.5;

        var startAngle = currentStartAngle * Math.PI;
        var endAngle = (currentEndAngle) * Math.PI;

        currentStartAngle = currentEndAngle - 0.01;
        currentEndAngle = currentEndAngle + 0.01;

        if (currentStartAngle > secondColorPoint) {
            if (secondColor == null) {
                clearInterval(myVar);
            }
            currentColor = secondColor;
            radius = lineRadius;
            width = lineWidth;
            //width = lineWidth + 3;
        } else {
            currentColor = firstColor;
            radius = lineRadius;
            width = lineWidth;
        }

        var counterClockwise = false;
        context.beginPath();
        context.arc(x, y, radius, startAngle, endAngle, counterClockwise);
        context.lineWidth = width;
        // line color
        context.strokeStyle = currentColor;
        context.stroke();
        //console.log(currentStartAngle);

        if (endAngle > 1.5 * 3.14) { clearInterval(myVar); }
        /************************************************/
    }
}