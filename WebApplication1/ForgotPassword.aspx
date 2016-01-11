<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="WebApplication1.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.4.2/jquery.mobile-1.4.2.min.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script>
        $(document).bind("mobileinit", function () {
            $.mobile.ajaxEnabled = false;
        });
    </script>
    <script src="http://code.jquery.com/mobile/1.4.2/jquery.mobile-1.4.2.min.js"></script>

    <script src="../JS/script.js"></script>
    <script src="../../JS/script.js"></script>
    <script>
        $(document).ready(function () {
            forgotPage();
        });
    </script>
    <link rel="stylesheet" href="Styles/style.css" />
</head>
<body>

    <div class="loginMainWindow">
        <div class="nLoginTransparent">
            <div class="nMiddleWindow">
                <form id="form1" runat="server">
                    <div class="nLoginLogo"></div>
                    <asp:Panel ID="PanelRequest" runat="server" Visible="False">
                        <div class="nLoginBox">
                            <asp:TextBox ID="TextBoxEmail" runat="server"
                                ValidationGroup="ForgotPasss" placeholder="Email" data-mini="true"></asp:TextBox>
                            <div class="nLoginValidationMessage invisible nForgotEmailError">Please enter valid email</div>
                            <div class="nFPButton"><b>Forgot Password</b></div>
                            <div class="nWrongLogin hidden">
                                <div class="nWrongLoginIcon"></div>
                                <b>
                                    <!-- we sent message-->
                                    <asp:Label ID="LabelMessage" runat="server" Visible="False"></asp:Label>
                                </b>
                            </div>
                        </div>
                        <div class="hidden">
                            <asp:Button ID="ButtonRequest" runat="server" OnClick="ButtonRequest_Click" Text="Send Request" />

                        </div>
                    </asp:Panel>

                    <asp:Panel ID="PanelRecover" runat="server" Visible="False">
                        <div class="nLoginBox">
                            <asp:TextBox ID="TextBoxPassword1" runat="server" TextMode="Password" CssClass="TextBoxRequiredL2R" placeholder="New password" data-mini="true" data-role="none"></asp:TextBox>
                            <div class="nLoginValidationMessage invisible nForgotPass1Error">Please enter new password</div>
                            <asp:TextBox ID="TextBoxPassword2" runat="server" CssClass="TextBoxRequiredL2R" TextMode="Password" placeholder="Re-type the password" data-mini="true"></asp:TextBox>
                            <div class="nLoginValidationMessage invisible nForgotPass2Error">Please re-enter the new password</div>
                            <div class="nFPRecoverButton"><b>Recover</b></div>

                            <div class="invisible">
                                <asp:Button ID="ButtonRecover" runat="server" OnClick="ButtonRecover_Click" Text="Recover" />
                            </div>
                        </div>

                    </asp:Panel>
                    <div class="nWrongLogin hidden">
                        <div class="nWrongLoginIcon"></div>
                        <b>
                            <!-- we sent message-->
                            <span class="nLabelMessageCopy"></span>
                        </b>
                    </div>
                    <div class="nExploreFooter nLoginFooter">
                        <div class="nLoginFooterSecondary">
                            <div class="nForgotLogin nFooterButton2small nFooterButton2SmallLeft">Login</div>
                            <div class="nForgotRegister nFooterButton2small nFooterButton2SmallRight">Register</div>
                        </div>
                    </div>
                    <asp:HiddenField ID="HiddenFieldStatus" runat="server" Value="0" />
                </form>
            </div>
        </div>
    </div>

    <!--
        <div>
                <p>
                    <asp:Label ID="Label1" runat="server" Text="Forgot your password?"
                        CssClass="Header"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="LabelEmail" runat="server"
                        Text="E-mail:"
                        AssociatedControlID="TextBoxEmail"></asp:Label>
                    
                </p>
                <p>
                    
                </p>
            
            
                <p>
                    <asp:Label ID="Label2" runat="server" Text="Change password"
                        CssClass="Header"></asp:Label>
                </p>
                <asp:Label ID="LabelEmail0" runat="server" CssClass="FormLabel"
                    Text="Email:"></asp:Label>
                &nbsp;<asp:Label ID="LabelRecoverEmail" runat="server" CssClass="FormLabel"></asp:Label>
                <br />
                <asp:Label ID="LabelPassword1" runat="server" Text="New password:"
                    CssClass="FormLabel" AssociatedControlID="TextBoxPassword1"></asp:Label>
                
                <br />
                <asp:Label ID="LabelPassword2" runat="server" AssociatedControlID="TextBoxPassword2" CssClass="FormLabel" Text="Re-type new password:"></asp:Label>
                &nbsp;
                <br />
                <br />
    <!--
        </div>
     -->

</body>
</html>
