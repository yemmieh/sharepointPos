<%@ Assembly Name="POSManager.SharePoint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=65d3952a678f79a6" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportDashboard.aspx.cs" Inherits="POSManager.SharePoint.Layouts.POSManager.SharePoint.ReportDashboard" MasterPageFile="POS.master" %>

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
                    <h3 class="panel-title">Queries & Report</h3>
                </div>
                <div class="panel-body">
                    <div>
                        <a class="btn btn-danger btn-success btn-lg reportBtn" onclick="changeSearch('Normal Search')" style="text-decoration: none;">Normal Search</a>
                        <a class="btn btn-danger btn-lg reportBtn" onclick="changeSearch('DB Search')" style="text-decoration: none;">DB Search</a>
                        <a class="btn btn-danger btn-lg reportBtn" style="text-decoration: none;">Switch Reports</a>
                        <a class="btn btn-danger btn-lg reportBtn" style="text-decoration: none;">Merchant Update Search</a>
                    </div>
                    <hr />
                    <div id="normalSearch" class="reportDisplay">
                        <div class="row">
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Account Number</dt>
                                    <dd>
                                        <asp:TextBox ID="NSAccountNumber" runat="server" CssClass="form-control"></asp:TextBox></dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Account Name</dt>
                                    <dd>
                                        <asp:TextBox ID="NSAccountName" runat="server"></asp:TextBox></dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Settlement Account Number</dt>
                                    <dd>
                                        <asp:TextBox ID="NSSettlementAccountNumber" runat="server"></asp:TextBox></dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Merchant Name</dt>
                                    <dd>
                                        <asp:TextBox ID="SNMerchantName" runat="server" CssClass="form-control"></asp:TextBox></dd>
                                </dl>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Merchant Id</dt>
                                    <dd>
                                        <asp:TextBox ID="SNMerchantID" runat="server" CssClass="form-control"></asp:TextBox></dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Terminal Id</dt>
                                    <dd>
                                        <asp:TextBox ID="SNTerminalId" runat="server" CssClass="form-control"></asp:TextBox></dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Terminal Location</dt>
                                    <dd>
                                        <asp:TextBox ID="SNTerminalLocation" runat="server" CssClass="form-control"></asp:TextBox></dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Requesting Branch</dt>
                                    <dd>
                                        <asp:DropDownList ID="SNRequestingBranch" runat="server" CssClass="form-control"></asp:DropDownList></dd>
                                </dl>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Department</dt>
                                    <dd>
                                        <asp:DropDownList ID="SNDeparment" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Requester Number</dt>
                                    <dd>
                                        <asp:TextBox ID="RequesterNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Requester Name</dt>
                                    <dd>
                                        <asp:TextBox ID="RequesterName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt></dt>
                                    <dd></dd>
                                </dl>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        function changeSearch(reportType) {
            $("a.reportBtn").removeClass('btn-success');
            $(this).addClass('btn-success');

            if (reportType == "Normal Search") {
                $(".reportDisplay").hide();
                $("#normalSearch").show();
            }
            else if (reportType == "DB Search") {
                $(".reportDisplay").hide();
                $("#dbSearch").show();
            }
            else if (reportType == "Switch Reports") {
                $(".reportDisplay").hide();
                $("#switchReports").show();
            }
            else if (reportType == "Merchant Update Search") {
                $(".reportDisplay").hide();
                $("#merchantUpdateSearch").show();
            }
        };
        $(function () {

        });
    </script>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    My Application Page
</asp:Content>
