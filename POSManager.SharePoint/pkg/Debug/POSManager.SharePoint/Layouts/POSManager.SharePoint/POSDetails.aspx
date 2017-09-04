<%@ Assembly Name="POSManager.SharePoint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=65d3952a678f79a6" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI.WebControls" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POSDetails.aspx.cs" Inherits="POSManager.SharePoint.Layouts.POSManager.SharePoint.POSDetails" MasterPageFile="POS.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="row" ng-app="pos">
        <div class="col-md-12 col-lg-12">
            <div style="margin-top: 20px;" ng-controller="POSController">
                <div id="msg" runat="server" visible="false" class="alert">
                </div>
                <div class="alert alert-info">
                    <div class="row">
                        <div class="col-md-8">
                            <h4>Requested By : 
                            <asp:Literal ID="RequestedBy" runat="server"></asp:Literal></h4>
                        </div>
                        <div class="col-md-4">
                            <h4>Status :
                                <asp:Literal ID="RequestStage" runat="server"></asp:Literal></h4>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Request Details</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-4 col-sm-3">
                                <dl>
                                    <dt>Requesting Branch:
                                    </dt>
                                    <dd>
                                        <asp:Literal ID="RequestingBranch" runat="server" />
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl>
                                    <dt>Department
                                    </dt>
                                    <dd>
                                        <asp:Literal ID="Department" runat="server"></asp:Literal>
                                    </dd>
                                </dl>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Account Number</dt>
                                    <dd>
                                        <asp:Literal ID="AccountNumber" runat="server"></asp:Literal>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Account Name</dt>
                                    <dd>
                                        <asp:Literal ID="AccountName" runat="server"></asp:Literal></dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Account Opening Date</dt>
                                    <dd>
                                        <asp:Literal ID="AccountOpeningDate" runat="server"></asp:Literal></dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>RC Number</dt>
                                    <dd>
                                        <asp:Literal ID="RCNumber" runat="server"></asp:Literal></dd>
                                </dl>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Registered Address</dt>
                                    <dd>
                                        <asp:Literal ID="RegisteredAddress" runat="server"></asp:Literal></dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Sector</dt>
                                    <dd>
                                        <asp:Literal ID="Sector" runat="server"></asp:Literal></dd>
                                </dl>

                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>
                                        <asp:Literal ID="LastMonthTurnOverLabel" runat="server"></asp:Literal></dt>
                                    <dd>
                                        <asp:Literal ID="LastMonthTurnover" runat="server"></asp:Literal></dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Phone Number</dt>
                                    <dd>
                                        <asp:Literal ID="PhoneNumber" runat="server"></asp:Literal>
                                    </dd>
                                </dl>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-4 col-md-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Email Address</dt>
                                    <dd>
                                        <asp:Literal ID="Email" runat="server"></asp:Literal>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-md-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Account Officer Phone</dt>
                                    <dd>
                                        <asp:Literal ID="AccountOfficerPhone" runat="server"></asp:Literal>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-md-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Account Officer</dt>
                                    <dd>
                                        <asp:Literal ID="AccountOfficer" runat="server"></asp:Literal></dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-md-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Account Status</dt>
                                    <dd>
                                        <asp:Literal ID="AccountStatus" runat="server"></asp:Literal></dd>
                                </dl>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-4 col-sm-3">
                                <dl>
                                    <dt>Merchant Trader Name</dt>
                                    <dd>
                                        <asp:Literal ID="MerchantTradeName" runat="server"></asp:Literal>
                                    </dd>
                                </dl>

                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl>
                                    <dt>Merchant Business Classification</dt>
                                    <dd>
                                        <asp:Literal ID="MerchantBusinessClassification" runat="server"></asp:Literal>
                                    </dd>
                                </dl>

                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl>
                                    <dt>Merchant Category</dt>
                                    <dd>
                                        <asp:Literal ID="MerchantCategory" runat="server"></asp:Literal>
                                    </dd>
                                </dl>

                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl>
                                    <dt>Description of Business</dt>
                                    <dd>
                                        <asp:Literal ID="DescriptionOfBusiness" runat="server"></asp:Literal>
                                    </dd>
                                </dl>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-4 col-md-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Business Opening hours</dt>
                                    <dd>
                                        <asp:Literal ID="BuinessHours" runat="server"></asp:Literal></dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-md-3">
                                <dl class="margin-sm-bottom">
                                    <dt>IKEDC Agent Account Number</dt>
                                    <dd>
                                        <asp:Literal ID="IKEDCAgentAccountNumber" runat="server"></asp:Literal>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-md-3">
                                <dl class="margin-sm-bottom">
                                    <dt>POS Type:</dt>
                                    <dd>
                                        <asp:Literal ID="POSType" runat="server"></asp:Literal>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-md-3">
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Location Log</h3>
                    </div>
                    <asp:HiddenField ID="ApprovalJSON" runat="server" />
                    <asp:HiddenField ID="ApprovalTitle" runat="server" />
                    <asp:HiddenField ID="LocationJSON" runat="server" />
                    <div class="modal fade" id="notificationModal" tabindex="-1" role="dialog" aria-labelledby="routeModalLabel">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                    <h4 class="modal-title" id="routeModalLabel">Notification & Online Access Details</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <dl>
                                                <dt>Needs Email Notification?</dt>
                                                <dd>
                                                    <label class="radio-inline">
                                                        <input type="radio" name="notification" id="notificationYes" value="true" />
                                                        Yes
                                                    </label>
                                                    <label class="radio-inline">
                                                        <input type="radio" name="notification" id="notificationNo" value="false" />
                                                        No
                                                    </label>
                                                </dd>
                                            </dl>
                                        </div>
                                        <div class="col-md-4">
                                            <dl>
                                                <dt>Send Notification To</dt>
                                                <dd>
                                                    <span id=""></span>
                                                </dd>
                                            </dl>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <dl>
                                                <dt>Requires Online Access?</dt>
                                                <dd>
                                                    <label class="radio-inline">
                                                        <input type="radio" name="onlineAccess" id="OnlineAccessYes" value="true">
                                                        Yes
                                                    </label>
                                                    <label class="radio-inline">
                                                        <input type="radio" name="OnlineAccess" id="OnlineAccessNo" value="false">
                                                        No
                                                    </label>
                                                </dd>
                                            </dl>
                                        </div>
                                        <div class="col-md-4">
                                            <dl>
                                                <dt>Username:</dt>
                                                <dd></dd>
                                            </dl>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-md-4">
                                            <dl>
                                                <dt>First Name:</dt>
                                                <dd></dd>
                                            </dl>
                                        </div>
                                        <div class="col-md-4">
                                            <dl>
                                                <dt>Last Name:</dt>
                                                <dd></dd>
                                            </dl>
                                        </div>
                                        <div class="col-md-4">
                                            <dl>
                                                <dt>Email Address:</dt>
                                                <dd></dd>
                                            </dl>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div ng-show="locations.length">
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>S/NO
                                        </th>
                                        <th>Address
                                        </th>
                                        <th>City
                                        </th>
                                        <th>State
                                        </th>
                                        <th>Term. No
                                        </th>
                                        <th>Stlmt Acct</th>
                                        <th>Contact Name</th>
                                        <th>Contact Phone</th>
                                        <th>...</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr ng-repeat="loca in locations">
                                        <td>{{$index + 1}}</td>
                                        <td>{{loca.Address}}</td>
                                        <td>{{loca.City}}</td>
                                        <td>{{loca.State}}</td>
                                        <td>{{loca.NoOfTerminal}}</td>
                                        <td>{{loca.SettlementAccountNumber}}</td>
                                        <td>{{loca.ContactName}}</td>
                                        <td>{{loca.Phone}}</td>
                                        <td><a data-toggle="modal" data-target="#notificationModal"><span class="glyphicon glyphicon-info-sign"></span></a></td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr ng-show="locations.length">
                                        <td colspan="4">
                                            <strong>TOTAL TERMINALS</strong>
                                        </td>
                                        <td colspan="5">
                                            <strong>{{ getTotal() }}</strong>
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Supporting Documents</h3>
                    </div>
                    <div class="panel-body">
                        <p>
                            <asp:HyperLink ID="Documentation" NavigateUrl="#" runat="server" />
                        </p>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Internal Account Details</h3>
                    </div>
                    <div class="panel-body">
                        <table class="table table-bordered table-stripped">
                            <thead>
                                <tr>
                                    <th>Internal Acc No
                                    </th>
                                    <th>Account Name
                                    </th>
                                    <th>Status
                                    </th>
                                    <th>Date Created
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Literal ID="InternalAccountNumber" runat="server"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="InternalAccountName" runat="server"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="InternalAccountStatus" runat="server"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="InternalAccountCreatedOn" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="panel panel-default" id="EBusinessSection" runat="server" visible="false">
                    <div class="panel-heading">
                        <h3 class="panel-title">E-Business Section</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-4 col-md-3">
                                <dl class="margin-sm-bottom">
                                    <dt>Communication Mode</dt>
                                    <dd>
                                        <asp:DropDownList ID="CommunicationMode" CssClass="form-control" runat="server"></asp:DropDownList></dd>
                                </dl>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="TerminalModelJSON" runat="server" />
                <asp:HiddenField ID="TerminalRoutesJSON" runat="server" />
                <asp:HiddenField ID="TerminalProvidersJSON" runat="server" />
                <asp:HiddenField ID="TerminalNetworkDeviceJSON" runat="server" />
                <div class="panel panel-default" id="POSConfiguration" runat="server" visible="false">
                    <div class="panel-heading">
                        <h3 class="panel-title">POS Configuration</h3>
                    </div>
                    <div class="panel-body">
                        <div>
                            <table class="table table-bordered">
                                <tbody ng-repeat="loc in locations">
                                    <tr>
                                        <td colspan="4">
                                            <span class="text-info"><strong>Location - {{$index + 1}}:</strong></span><br />
                                            <span class="text-info"><strong>{{loc.Address}}, {{loc.LGA}}, {{loc.State}}</strong></span>
                                        </td>
                                        <td colspan="5">
                                            <span class="text-info"><strong>Merchant ID:</strong></span><br />
                                            <span class="text-danger"><strong>{{loc.POSRequest.MerchantId}}</strong></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Term. ID</th>
                                        <th>S/N</th>
                                        <th>Term. Model</th>
                                        <th>Switch</th>
                                        <th>SIMM S/N</th>
                                        <th>Route</th>
                                        <th>Provider</th>
                                        <th>Ntwk Device</th>
                                        <th>Dep?</th>
                                    </tr>
                                    <tr ng-repeat="terminal in loc.Terminals">
                                        <td>{{terminal.TerminalId}}</td>
                                        <td>
                                            <input type="text" ng-disabled="terminal.DeployedOn" id="SerialNo" name="SerialNo" ng-model="terminal.SerialNo" /></td>
                                        <td>
                                            <select name="TerminalModel" ng-disabled="terminal.DeployedOn" ng-change="locChanged()" ng-model="terminal.TerminalModel">
                                                <option></option>
                                                <option ng-repeat="model in tModels">{{model}}</option>
                                            </select>
                                        </td>
                                        <td>
                                            <input type="text" ng-disabled="terminal.DeployedOn" ng-blur="locChanged()" id="Switch" ng-model="terminal.Switch" />
                                        </td>
                                        <td>
                                            <input type="text" ng-disabled="terminal.DeployedOn" ng-blur="locChanged()" id="SIM" name="SIM" ng-model="terminal.SIM" />
                                        </td>
                                        <td>
                                            <select name="Route" ng-disabled="terminal.DeployedOn" ng-change="locChanged()" class="tRoute" ng-model="terminal.Route">
                                                <option></option>
                                                <option ng-repeat="route in tRoutes">{{route}}</option>
                                            </select>
                                        </td>
                                        <td>
                                            <select name="Provider" ng-disabled="terminal.DeployedOn" ng-change="locChanged()" class="tProvider" ng-model="terminal.Provider">
                                                <option value=""></option>
                                                <option ng-repeat="provider in tProviders">{{provider}}</option>
                                            </select>
                                        </td>
                                        <td>
                                            <select name="NetworkDevice" ng-disabled="terminal.DeployedOn" ng-change="locChanged()" ng-model="terminal.NetworkDevice">
                                                <option></option>
                                                <option ng-repeat="networkDevice in tNetworkDevices">{{networkDevice}}</option>
                                            </select>
                                        </td>
                                        <td>
                                            <input type="checkbox" ng-change="locChanged()" ng-disabled="terminal.DeployedOn" name="Deployed" ng-model="terminal.Deployed" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="row">
                            <div class="col-xs-4 col-sm-3">
                                <dl>
                                    <dt>PTSP</dt>
                                    <dd>
                                        <asp:DropDownList ID="PTSP" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="" Text="Select PTSP"></asp:ListItem>
                                            <asp:ListItem>EAZYFUEL</asp:ListItem>
                                            <asp:ListItem>ETOP</asp:ListItem>
                                            <asp:ListItem>CITISERVE</asp:ListItem>
                                            <asp:ListItem>ITEX</asp:ListItem>
                                            <asp:ListItem>VALUCARD</asp:ListItem>
                                            <asp:ListItem>Others</asp:ListItem>
                                        </asp:DropDownList>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl>
                                    <dt>ISO Type
                                    </dt>
                                    <dd>
                                        <asp:DropDownList ID="ISOType" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="" Text="Select ISO Type"></asp:ListItem>
                                            <asp:ListItem>Caplink</asp:ListItem>
                                            <asp:ListItem>Echo Solution</asp:ListItem>
                                            <asp:ListItem>EtopNG</asp:ListItem>
                                            <asp:ListItem>PayCycle</asp:ListItem>
                                            <asp:ListItem>PayMaster</asp:ListItem>
                                            <asp:ListItem>PayPlus</asp:ListItem>
                                            <asp:ListItem>PayStream</asp:ListItem>
                                            <asp:ListItem>SWAP</asp:ListItem>
                                            <asp:ListItem>Zenith</asp:ListItem>
                                            <asp:ListItem>Other</asp:ListItem>
                                        </asp:DropDownList>
                                    </dd>
                                </dl>
                            </div>
                            <div class="col-xs-4 col-sm-3">
                                <dl>
                                    <dt>Key Type</dt>
                                    <dd>
                                        <asp:DropDownList ID="KeyType" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="" Text="Select Key Type"></asp:ListItem>
                                            <asp:ListItem>Key1</asp:ListItem>
                                            <asp:ListItem>Key2</asp:ListItem>
                                        </asp:DropDownList>
                                    </dd>
                                </dl>
                            </div>
                        </div>
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
                <hr />
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
    </div>
    <script type="text/javascript">
        (function () {

            if ($("#<%= ApprovalJSON.ClientID %>").val()) {
                var approvals = JSON.parse($("#<%= ApprovalJSON.ClientID %>").val());
                var msg = $("#<%= ApprovalTitle.ClientID %>").val();
                $.each(approvals, function (i, name) {
                    msg += (name + "\n");
                });
                alert(msg);
                var url = window.location.href;
                window.location = url.replace("POSDetails.aspx", "AwaitingApproval.aspx");
            }

            var posApp = angular.module("pos", []);
            posApp.controller('POSController', function ($scope) {
                $scope.locations = [];
                if ($scope.locations.length === 0) {
                    if ($("#<%= LocationJSON.ClientID %>").val()) {
                        $scope.locations = JSON.parse($("#<%= LocationJSON.ClientID %>").val());
                    }
                }
                $scope.tModels = JSON.parse($("#<%= TerminalModelJSON.ClientID %>").val());
                $scope.tRoutes = JSON.parse($("#<%= TerminalRoutesJSON.ClientID %>").val());
                $scope.tProviders = JSON.parse($("#<%= TerminalProvidersJSON.ClientID %>").val());
                $scope.tNetworkDevices = JSON.parse($("#<%= TerminalNetworkDeviceJSON.ClientID %>").val());
                $scope.locChanged = function () {
                    $("#<%= LocationJSON.ClientID %>").val(angular.toJson($scope.locations));
                }

                $scope.$watch("locations", function () {
                    $("#<%= LocationJSON.ClientID %>").val(angular.toJson($scope.locations));
                });
                $scope.getTotal = function () {
                    var total = 0;
                    for (var i = 0; i < $scope.locations.length; i++) {
                        var location = $scope.locations[i];
                        total += parseInt(location.NoOfTerminal);
                    }
                    return total;
                }
            });
        })();
    </script>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    POS Details
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    My Application Page
</asp:Content>
