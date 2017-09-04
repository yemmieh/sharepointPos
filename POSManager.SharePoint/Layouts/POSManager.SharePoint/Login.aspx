<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" CodeBehind="Login.aspx.cs" Inherits="POSManager.SharePoint.Layouts.POSManager.SharePoint.Login" MasterPageFile="~/_layouts/15/errorv15.master" %>

<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,login_pagetitle%>" EncodeMethod='HtmlEncode' />
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div>
        <link href="/_layouts/15/OffBankingHour.SharePoint/Static/Style/bootstrap.min.css" rel="Stylesheet" />
        <div class="panel panel-default">
            <div class="panel-heading" style="background:#D73327; color: #fff;">
                <h3 class="panel-title">::Off Banking Hour::</h3>
            </div>
            <div class="panel-body">
                <%--<asp:Label ID="FailureText" class="ms-error" runat="server" />--%>
                <div id="msg" runat="server" visible="false" class="alert">
                </div>
                <h4>Please sign in to continue</h4>
                <div class="form" role="form">
                    <div class="form-group">
                        <label for="UserName" class="control-label">Enter Username</label>
                        <asp:TextBox ID="UserName" autocomplete="off" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="Password" class="control-label">Enter Password</label>
                        <asp:TextBox ID="Password" TextMode="Password" autocomplete="off" runat="server" class="form-control" />
                    </div>
                </div>
                <div class="clearfix">
                    <div class="pull-right">
                        <label class="checkbox">
                            <asp:CheckBox ID="RememberMe" Text="Remember Me" runat="server" /></label>
                    </div>
                </div>
                <div class="pull-right">
                    <asp:Button ID="Submit" CssClass="btn btn-primary" CommandName="Login" Text="Login" OnClick="Submit_Click" runat="server" />
                </div>
            </div>
            <div class="panel-footer" style="background:#D73327">
            </div>
        </div>
    </div>

</asp:Content>
