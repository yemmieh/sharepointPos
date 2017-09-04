<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyRequest.aspx.cs" Inherits="POSManager.SharePoint.Layouts.POSManager.SharePoint.MyRequest" MasterPageFile="POS.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <div style="margin-top: 20px;">
                <div id="msg" runat="server" visible="false" class="alert">
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">My Request(s)</h3>
                    </div>
                    <div class="panel-body">
                        <asp:ListView ID="POSRequests" runat="server" ItemPlaceholderID="ItemPlaceholder">
                            <LayoutTemplate>
                                <table class="table table-hover table-striped table-bordered">
                                    <thead>
                                        <tr>
                                            <th>S/NO</th>
                                            <th>Account Number</th>
                                            <th>Account Name</th>
                                            <th>Merchant Trade Name</th>
                                            <th>Requesting Branch</th>
                                            <th>Initiated By</th>
                                            <th>Initiated On</th>
                                            <th>Pending</th>
                                            <th>Status</th>
                                            <th>Approval</th>
                                        </tr>
                                    </thead>
                                    <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.DataItemIndex + 1 %></td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="AccountNumber" Text='<%# Eval("AccountNumber") %>' NavigateUrl='<%# DetailsLink("POSDetails.aspx", string.Format("posRequestId={0}", Eval("Id"))) %>'></asp:HyperLink></td>
                                    <td><%# Eval("AccountName") %></td>
                                    <td><%# Eval("MerchantTradeName") %></td>
                                    <td><%# GetBranchName(Eval("RequestingBranch").ToString()) %></td>
                                    <td><%# GetUser(Eval("InitiatedBy").ToString()).Name %></td>
                                    <td><%# string.Format("{0:dd-MMM-yyyy H:mm:ss}", Eval("InitiatedOn")) %></td>
                                    <td><%# Eval("Pending") %></td>
                                    <td><%# Eval("Status") %></td>
                                    <td><a href="#" request-id='<%# Eval("Id") %>' class="trigger"><span class="glyphicon glyphicon-user"></span></a></td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        (function () {
            $('.trigger').click(function () {
                var id = $(this).attr("request-id");
                jQuery.ajax({
                    type: "POST",
                    url: "AwaitingApproval.aspx/GetApproval",
                    data: "{ requestId: " + id + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        //$scope.customer = data.d;
                        var msg = "Approval List\n\n";

                        $.each(data.d, function (i, name) {
                            msg += (name + "\n");
                        });
                        alert(msg);
                    },
                    error: function (data) {

                    }
                });
            });
        })();
    </script>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    MyRequest(s)
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    My Application Page
</asp:Content>
