<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MerchantUpdate.aspx.cs" Inherits="POSManager.SharePoint.Layouts.POSManager.SharePoint.MerchantUpdate" MasterPageFile="POS.master" %>

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
                        <h3 class="panel-title">Merchant Update</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Requesting Branch</dt>
                                    <dd>
                                        <asp:DropDownList ID="RequestingBranch" CssClass="form-control" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequestionBranchValidator" ControlToValidate="RequestingBranch" CssClass="text-danger" ValidationGroup="POSRequest" ErrorMessage="Please select requesting branch" Text="Please select request branch" runat="server"></asp:RequiredFieldValidator>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Department</dt>
                                    <dd>
                                        <asp:DropDownList ID="Department" CssClass="form-control" Style="display: none;" runat="server"></asp:DropDownList></dd>
                                </dl>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Merchant ID</dt>
                                    <dd>
                                        <asp:TextBox ID="MerchantId" CssClass="form-control merchantId" runat="server"></asp:TextBox>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Merchant Name</dt>
                                    <dd>
                                        <span runat="server" class="MerchantName" id="MerchantName"></span>
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
                                            <asp:TextBox ID="NewAccountNo" CssClass="form-control accountNo" runat="server"></asp:TextBox>
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
                                            <asp:TextBox ID="CurrentEmailAddress" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
                                        </dd>
                                    </dl>
                                </div>
                                <div class="col-xs-4 col-sm-3">
                                    <dl class="margin-sm-bottom">
                                        <dt>New Email Address</dt>
                                        <dd>
                                            <asp:TextBox ID="NewEmailAddress" CssClass="form-control" runat="server"></asp:TextBox>
                                        </dd>
                                    </dl>
                                </div>
                                <div class="col-xs-4 col-sm-3">
                                    <dl class="margin-sm-bottom">
                                        <dt>Option</dt>
                                        <dd>
                                            <asp:RadioButtonList ID="NotificationOption" RepeatDirection="Horizontal" runat="server">
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
                                            <asp:TextBox ID="CurrentUsername" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
                                        </dd>
                                    </dl>
                                </div>
                                <div class="col-xs-4 col-sm-3">
                                    <dl class="margin-sm-bottom">
                                        <dt>New Username</dt>
                                        <dd>
                                            <asp:TextBox ID="NewUsername" CssClass="form-control" runat="server"></asp:TextBox>
                                        </dd>
                                    </dl>
                                </div>
                                <div class="col-xs-4 col-sm-3">
                                    <dl class="margin-sm-bottom">
                                        <dt>Option</dt>
                                        <dd>
                                            <asp:RadioButtonList ID="ReportOption" RepeatDirection="Horizontal" runat="server">
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
                                            <asp:TextBox ID="NewViewerEmailAddress" CssClass="form-control" runat="server"></asp:TextBox>
                                        </dd>
                                    </dl>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="UpdateType" runat="server" />
                        <asp:HiddenField runat="server" ID="OldTerminals" />
                        <asp:HiddenField runat="server" ID="NewTerminals" />
                        <div id="terminalView" runat="server" class="merchantUpdate">
                            <div class="row">
                                <div class="col-md-7 col-md-offset-5">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:RadioButtonList ID="NewMID" CssClass="newMID" RepeatDirection="Horizontal" runat="server">
                                                <asp:ListItem Value="True" Text="New MID"></asp:ListItem>
                                                <asp:ListItem Value="False" Text="Existing MID"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="ExistingMID" ReadOnly="true" CssClass="form-control tExistingMID" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <dl class="margin-sm-bottom">
                                        <dt>Terminal IDs Attached to this Merchant</dt>
                                        <dd>
                                            <select name="from" size="8" id="multiselect" class="form-control" multiple="multiple">
                                            </select>
                                        </dd>
                                    </dl>
                                </div>
                                <div class="col-md-1">
                                    <br />
                                    <button type="button" id="multiselect_rightAll" class="btn btn-block">
                                        <i class="glyphicon glyphicon-forward"></i>
                                    </button>
                                    <button type="button" id="multiselect_rightSelected" class="btn btn-block">
                                        <i class="glyphicon glyphicon-chevron-right"></i>
                                    </button>
                                    <button type="button" id="multiselect_leftSelected" class="btn btn-block">
                                        <i class="glyphicon glyphicon-chevron-left"></i>
                                    </button>
                                    <button type="button" id="multiselect_leftAll" class="btn btn-block">
                                        <i class="glyphicon glyphicon-backward"></i>
                                    </button>
                                </div>
                                <div class="col-md-4">
                                    <dl>
                                        <dt>Reassigned Terminals
                                        </dt>
                                        <dd>
                                            <select name="to" size="8" id="multiselect_to" class="form-control" multiple="multiple">
                                            </select>
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
                                                    <asp:TextBox ReadOnly="true" ID="NewSettlementAccount" CssClass="form-control tSettleAccount" runat="server"></asp:TextBox>
                                                </dd>
                                            </dl>
                                        </div>
                                        <div class="col-md-4">
                                            <dl class="margin-sm-bottom">
                                                <dt>Account Name</dt>
                                                <dd>
                                                    <span runat="server" class="tAccountName" id="tAccountName"></span>
                                                </dd>
                                            </dl>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div>
                            <div class="form-group" style="margin-top: 5px;">
                                <label for="Documentation">Customer Request</label>
                                <div>
                                    <asp:FileUpload ID="Documentation" CssClass="form-control" runat="server" />

                                </div>
                            </div>
                            <div class="form-group" style="margin-top: 5px;">
                                <label for="Comment">Comment</label>
                                <div>
                                    <asp:TextBox ID="Comment" TextMode="MultiLine" Rows="3" Width="350px" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="Submit" Text="Submit" OnClientClick="SubmitUpate()" OnClick="Submit_Click" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function SubmitUpate() {
            if (confirm("Do you want to update this merchant information?")) {
                var oldTerminal = "";
                var newTerminal = "";
                if ($("#<%= UpdateType.ClientID %>").val() == "Terminal") {
                    $("#multiselect option").each(function () {
                        oldTerminal += ($(this).val() + "\n");
                    });

                    $("#multiselect_to option").each(function () {
                        newTerminal += ($(this).val() + "\n");
                    });
                    $("#<%= OldTerminals.ClientID %>").val(oldTerminal);
                    $("#<%= NewTerminals.ClientID %>").val(newTerminal);
                }
                return true;
            }
            return false
        };

        $(function () {

            $("input[name='ctl00$PlaceHolderMain$NewMID']").click(function (e) {
                var checked = $(this).val();
                if (checked === "True") {
                    $(".tExistingMID").prop("readonly", true);
                    $(".tSettleAccount").prop("readonly", false);
                }
                else {
                    $(".tExistingMID").prop("readonly", false);
                    $(".tSettleAccount").prop("readonly", true);
                }
            });

            $(".merchantId").blur(function () {
                if ($(this).val()) {
                    jQuery.ajax({
                        type: "POST",
                        url: "MerchantUpdate.aspx/GetMerchant",
                        contentType: "application/json; charset=utf-8",
                        data: "{ merchantId: '" + $(this).val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            if (data.d.MerchantName) {
                                $("#<%= CurrentAccountNo.ClientID %>").text(data.d.SettlementAccount);
                                $("#<%= CurrentSettlementAccount.ClientID %>").text(data.d.SettlementAccount);
                                $("span.MerchantName").text(data.d.MerchantName);
                                $("#<%= CurrentEmailAddress.ClientID %>").text(data.d.Email);
                                $("#<%= CurrentUsername.ClientID %>").text(data.d.UserId);
                                $("#multiselect").html(data.d.TerminalId);
                            }
                        },
                        error: function (data) {
                            alert("Merchant information not loaded, please try again.");
                        }
                    });
                }
            });

            $(".tExistingMID").blur(function () {
                if ($(this).val()) {
                    jQuery.ajax({
                        type: "POST",
                        url: "MerchantUpdate.aspx/GetMerchant",
                        contentType: "application/json; charset=utf-8",
                        data: "{ merchantId: '" + $(this).val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            if (data.d.MerchantName) {
                                $(".tSettleAccount").val(data.d.SettlementAccount);
                            }
                        },
                        error: function (data) {
                            alert("Merchant information not loaded, please try again.");
                        }
                    });
                }
            });

            $(".accountNo").blur(function () {
                if ($(this).val()) {
                    jQuery.ajax({
                        type: "POST",
                        url: "NewRequest.aspx/GetCustomer",
                        data: "{ accountNumber: '" + $(this).val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data.d.Name) {
                                $(".AccountName").text(data.d.Name);
                            }
                        },
                        error: function (data) {
                            alert("Customer information not found, please try again.");
                        }
                    });
                }
            });

            $(".tSettleAccount").blur(function () {
                if ($(this).val()) {
                    jQuery.ajax({
                        type: "POST",
                        url: "NewRequest.aspx/GetCustomer",
                        data: "{ accountNumber: '" + $(this).val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data.d.Name) {
                                $("span.tAccountName").text(data.d.Name);
                            }
                        },
                        error: function (data) {
                            alert("Customer information not found, please try again.");
                        }
                    });
                }
            });
            $('#<%= RequestingBranch.ClientID %>').on('change', function (e) {
                var optionSelected = $("option:selected", this);
                var valueSelected = optionSelected.val();
                if (valueSelected === '001') {
                    $('#<%= Department.ClientID %>').show();
                }
                else {
                    $('#<%= Department.ClientID %>').hide();
                }
            });
            $('#multiselect').multiselect();
        })();
    </script>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Merchant Update
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Merchant Update
</asp:Content>
