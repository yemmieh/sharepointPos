<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateDetails.aspx.cs" Inherits="POSManager.SharePoint.Layouts.POSManager.SharePoint.UpdateDetails" MasterPageFile="POS.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="row">
        <div class="col-md-12 col-kg-12">
            <div style="margin-top: 20px;">
                <div id="msg" runat="server" class="alert" visible="false">
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Merchant Update</h3>
                </div>

                <div class="panel-body">

                    <div class="row">
                        <div class="col-xs-4 col-sm-3">
                            <dl class="margin-sm-bottom">
                                <dt>Requesting Branch</dt>
                                <dd>
                                    <asp:Label ID="RequestingBranch" runat="server"></asp:Label>
                                </dd>
                            </dl>
                        </div>
                        <div class="col-xs-4 col-sm-3">
                            <dl class="margin-sm-bottom">
                                <dt>Department</dt>
                                <dd>
                                    <asp:Label ID="Department" runat="server"></asp:Label></dd>
                            </dl>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4 col-sm-3">
                            <dl class="margin-sm-bottom">
                                <dt>Merchant ID</dt>
                                <dd>
                                    <asp:Label ID="MerchantId" runat="server"></asp:Label>
                                </dd>
                            </dl>
                        </div>
                        <div class="col-xs-4 col-sm-3">
                            <dl class="margin-sm-bottom">
                                <dt>Merchant Name</dt>
                                <dd>
                                    <asp:Label runat="server" ID="MerchantName"></asp:Label>
                                </dd>
                            </dl>
                        </div>
                    </div>
                    <div id="settlementAccountView" runat="server" class="merchantUpdate">
                        <div class="row">
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Current Account No:</dt>
                                    <dd>
                                        <asp:Label ID="CurrentAccountNo" runat="server"></asp:Label>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>New Account No:</dt>
                                    <dd>
                                        <asp:Label ID="NewAccountNo" runat="server"></asp:Label>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>AccountName</dt>
                                    <dd>
                                        <asp:Label runat="server" CssClass="AccountName" ID="AccountName"></asp:Label>
                                    </dd>
                                </dl>
                            </div>
                        </div>
                    </div>

                    <div id="notificationView" runat="server" class="merchantUpdate">
                        <div class="row">
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Current Email Address</dt>
                                    <dd>
                                        <asp:TextBox ID="CurrentEmailAddress" Enabled="false" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>New Email Address</dt>
                                    <dd>
                                        <asp:Label ID="NewEmailAddress" runat="server"></asp:Label>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Option</dt>
                                    <dd>
                                        <asp:RadioButtonList ID="NotificationOption" Enabled="false" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Selected="True" Text="Add To Existing" Value="True"></asp:ListItem>
                                            <asp:ListItem Text="Replace Existing" Value="False"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </dd>
                                </dl>
                            </div>
                        </div>
                    </div>

                    <div id="reportViewerView" runat="server" class="merchantUpdate">
                        <div class="row">
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Current Username</dt>
                                    <dd>
                                        <asp:TextBox ID="CurrentUsername" Enabled="false" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>New Username</dt>
                                    <dd>
                                        <asp:Label ID="NewUsername" runat="server"></asp:Label>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Option</dt>
                                    <dd>
                                        <asp:RadioButtonList ID="ReportOption" Enabled="false" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Selected="True" Text="Add To Existing" Value="True"></asp:ListItem>
                                            <asp:ListItem Text="Replace Existing" Value="False"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>New Email Address</dt>
                                    <dd>
                                        <asp:Label ID="NewViewerEmailAddress" CssClass="form-control" runat="server"></asp:Label>
                                    </dd>
                                </dl>
                            </div>
                        </div>
                    </div>

                    <div id="terminalView" runat="server" class="merchantUpdate">
                        <div class="row">
                            <div class="col-md-7 col-md-offset-5">
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:RadioButtonList ID="NewMID" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Selected="True" Value="True" Text="New MID"></asp:ListItem>
                                            <asp:ListItem Value="False" Text="Existing MID"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="ExistingMID" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <dl class="margin-sm-bottom">
                                    <dt>Terminal IDs Attached to this Merchant</dt>
                                    <dd>
                                        <asp:TextBox ID="OldTerminals" runat="server" TextMode="MultiLine" Rows="4" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-md-1">
                                <br />
                                
                            </div>
                            <div class="col-md-4">
                                <dl>
                                    <dt>Reassigned Terminals
                                    </dt>
                                    <dd>
                                        <asp:TextBox ID="NewTerminals" runat="server" TextMode="MultiLine" ReadOnly="true" Rows="4" CssClass="form-control"></asp:TextBox>
                                    </dd>
                                </dl>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-5">
                                <div class="row">
                                    <div class="col-md-4 col-md-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>Current Settlement Account</dt>
                                            <dd>
                                                <asp:TextBox ID="CurrentSettlementAccount" ReadOnly="true" TextMode="MultiLine" Rows="3" Width="350px" CssClass="form-control" runat="server"></asp:TextBox>
                                            </dd>
                                        </dl>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-7">
                                <div class="row">
                                    <div class="col-md-4">
                                        <dl class="margin-sm-bottom">
                                            <dt>New Settlement Account</dt>
                                            <dd>
                                                <asp:Label ID="NewSettlementAccount" CssClass="tSettleAccount" runat="server"></asp:Label>
                                            </dd>
                                        </dl>
                                    </div>
                                    <div class="col-md-4">
                                        <dl class="margin-sm-bottom">
                                            <dt>Account Name</dt>
                                            <dd>
                                                <asp:Label runat="server" ID="tAccountName"></asp:Label>
                                            </dd>
                                        </dl>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" style="margin-top: 5px;">
                        <label for="Documentation">Customer Request</label>
                        <div>
                            <asp:HyperLink ID="CustomerRequest" runat="server"></asp:HyperLink>
                        </div>
                    </div>
                    <div id="commentDisplay" runat="server" visible="false">
                        <div class="form-group" style="margin-top: 5px;">
                            <label for="Comment">Comment</label>
                            <div>
                                <asp:TextBox ID="Comment" TextMode="MultiLine" Rows="3" Width="350px" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Button ID="Approve" Text="Aprove" CssClass="btn btn-success" runat="server" OnClick="Approve_Click" OnClientClick="return confirm('Are you sure you want to proceed');" />
                        <asp:Button ID="Decline" Text="Decline" CssClass="btn btn-danger" runat="server" OnClick="Decline_Click" OnClientClick="return confirm('Are you sure you want to decline');" />
                    </div>
                </div>
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Request History</h3>
                </div>
                <div class="panel-body">
                    <asp:ListView runat="server" ID="History" ItemPlaceholderID="Placeholder">
                        <LayoutTemplate>
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>S/N</th>
                                        <th>Performed By</th>
                                        <th>Performed On</th>
                                        <th>Stage</th>
                                        <th>Action</th>
                                        <th>Comment</th>
                                    </tr>
                                </thead>

                                <asp:PlaceHolder ID="Placeholder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td></td>
                                <td><%# GetUser(Eval("PerformedBy").ToString()).Name %></td>
                                <td><%# string.Format("{0:yyyy-MMM-dd H:mm:ss}", Eval("PerformedOn")) %></td>
                                <td><%# Eval("RequestStage") %></td>
                                <td><%# Eval("Action") %></td>
                                <td><%# Eval("Comment") %></td>
                            </tr>
                        </ItemTemplate>
                        <EmptyItemTemplate>
                            <p>Entries are not available</p>
                        </EmptyItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Update Details
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    My Application Page
</asp:Content>
