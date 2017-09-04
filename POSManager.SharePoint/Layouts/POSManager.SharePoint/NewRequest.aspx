<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI.WebControls" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="POSManager.SharePoint.Layouts.POSManager.SharePoint.NewRequest" MasterPageFile="~/_layouts/15/POSManager.SharePoint/POS.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:HiddenField ID="Submitted" runat="server" />
    <div class="row" ng-app="pos">
        <div class="col-md-12 col-lg-12">
            <div style="margin-top: 20px;">
                <div>
                    <div>
                        <div id="msg" runat="server" style="display: none;" visible="false" class="alert">
                        </div>
                        <div id="accountError" style="display: none;" class="alert alert-danger">
                            Customer information not found, please try again.
                        </div>
                        <div id="internalAccStatus" style="display: none;" class="alert alert-warning">
                            <strong>Customer Internal Account is not Active, please activate internal account.</strong>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Request Detail</h3>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <asp:HiddenField ID="CustomerJSONHidden" runat="server" />
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
                                            <dt>Account Number</dt>
                                            <dd>
                                                <asp:TextBox ID="AccountNumber" CssClass="form-control numberOnly accountNumber" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="AccountNumber" ValidationGroup="POSRequest" CssClass="text-danger" ErrorMessage="Please enter account number" Text="Please account number" runat="server"></asp:RequiredFieldValidator>
                                            </dd>
                                        </dl>
                                    </div>
                                    <div class="col-xs-4 col-sm-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>Account Name</dt>
                                            <dd>
                                                <asp:Label ID="AccountName" runat="server"></asp:Label></dd>
                                        </dl>
                                    </div>
                                    <div class="col-xs-4 col-sm-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>Account Opening Date</dt>
                                            <dd>
                                                <asp:Label ID="AccountOpeningDate" runat="server"></asp:Label></dd>
                                        </dl>
                                    </div>
                                    <div class="col-xs-4 col-sm-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>RC Number</dt>
                                            <dd>
                                                <asp:Label ID="RCNumber" runat="server"></asp:Label></dd>
                                        </dl>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-4 col-sm-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>Registered Address</dt>
                                            <dd>
                                                <asp:Label ID="Address" runat="server"></asp:Label></dd>
                                        </dl>
                                    </div>
                                    <div class="col-xs-4 col-sm-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>Sector</dt>
                                            <dd>
                                                <asp:Label ID="Sector" runat="server"></asp:Label></dd>
                                        </dl>

                                    </div>
                                    <div class="col-xs-4 col-sm-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>
                                                <asp:Literal ID="LastMonthTurnOverLabel" runat="server"></asp:Literal></dt>
                                            <dd>
                                                <asp:Label ID="LastMonthTurnOver" runat="server"></asp:Label></dd>
                                        </dl>
                                    </div>
                                    <div class="col-xs-4 col-sm-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>Phone Number</dt>
                                            <dd>
                                                <asp:TextBox ID="PhoneNumber" CssClass="form-control numberOnly" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="PhoneNumber" CssClass="text-danger" ValidationGroup="POSRequest" ErrorMessage="Phone number is required" Text="Phone number is required" runat="server"></asp:RequiredFieldValidator>
                                            </dd>
                                        </dl>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-4 col-md-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>Email Address</dt>
                                            <dd>
                                                <asp:TextBox ID="Email" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="text-danger" ControlToValidate="Email" ValidationGroup="POSRequest" ErrorMessage="Email is required" Text="Email is required" runat="server"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ID="RegularExpressionValidator2" ControlToValidate="Email" ValidationGroup="POSRequest" ErrorMessage="Please enter valid email" CssClass="text-danger" Text="Please enter valid email" runat="server"></asp:RegularExpressionValidator>
                                            </dd>
                                        </dl>
                                    </div>
                                    <div class="col-xs-4 col-md-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>Account Officer Phone</dt>
                                            <dd>
                                                <asp:TextBox ID="AccountOfficerPhone" CssClass="form-control numberOnly" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="text-danger" ControlToValidate="AccountOfficerPhone" ValidationGroup="POSRequest" ErrorMessage="Account officer Phone number is required" Text="Account officer Phone number is required" runat="server"></asp:RequiredFieldValidator>
                                            </dd>
                                        </dl>
                                    </div>
                                    <div class="col-xs-4 col-md-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>Account Officer</dt>
                                            <dd>
                                                <asp:Label ID="AccountOfficer" runat="server"></asp:Label></dd>
                                        </dl>
                                    </div>
                                    <div class="col-xs-4 col-md-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>Account Status</dt>
                                            <dd>
                                                <asp:Label ID="AccountStatus" runat="server"></asp:Label></dd>
                                        </dl>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-xs-4 col-sm-3">
                                        <dl>
                                            <dt>Merchant Trader Name
                                            </dt>

                                            <dd>
                                                <asp:TextBox ID="MerchantName" CssClass="form-control" runat="server"></asp:TextBox>
                                                <p class="help-block text-danger" style="color: red;">(this is the name that will appear on POS transaction receipts)</p>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="MerchantName" ValidationGroup="POSRequest" ErrorMessage="Please enter merchant name" CssClass="text-danger" Text="Please enter merchant name" runat="server"></asp:RequiredFieldValidator>
                                            </dd>
                                        </dl>

                                    </div>
                                    <div class="col-xs-4 col-sm-3">
                                        <dl>
                                            <dt>Merchant Business Classification</dt>
                                            <dd>
                                                <asp:DropDownList ID="BusinessClassification" CssClass="form-control" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator CssClass="text-danger" ID="RequiredFieldValidator6" ControlToValidate="BusinessClassification" ValidationGroup="POSRequest" ErrorMessage="Please select merchant business classification" Text="Please select merchant business classification" runat="server"></asp:RequiredFieldValidator>
                                            </dd>
                                        </dl>

                                    </div>
                                    <div class="col-xs-4 col-sm-3">
                                        <dl>
                                            <dt>Merchant Category</dt>
                                            <dd>
                                                <asp:DropDownList ID="MerchantCategory" CssClass="form-control" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="MerchantCategory" ValidationGroup="POSRequest" ErrorMessage="Please select merchant category" Text="Please select merchant category" runat="server"></asp:RequiredFieldValidator>
                                            </dd>
                                        </dl>

                                    </div>
                                    <div class="col-xs-4 col-sm-3">
                                        <dl>
                                            <dt>Description of Business</dt>
                                            <dd>
                                                <asp:DropDownList ID="DescriptionOfBusiness" CssClass="form-control" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="DescriptionOfBusiness" ValidationGroup="POSRequest" ErrorMessage="Please select decription of business" Text="Please select decription of business" runat="server"></asp:RequiredFieldValidator>
                                            </dd>
                                        </dl>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-4 col-md-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>Business Opening hours</dt>
                                            <dd>
                                                <div class="input-group">
                                                    <input type="text" id="FromTime" placeholder="From" runat="server" style="width: 127.5px;" class="form-control keyDisable" />
                                                    <span class="input-group-addon">-</span>
                                                    <input type="text" id="ToTime" placeholder="To" runat="server" style="width: 127.5px;" class="form-control keyDisable" />
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ValidationGroup="POSRequest" runat="server" ControlToValidate="FromTime" CssClass="text-danger" ErrorMessage="Select time range" Text="Please select from time"></asp:RequiredFieldValidator>
                                                &nbsp&nbsp&nbsp&nbsp<asp:RequiredFieldValidator ID="RequiredFieldValidator23" ValidationGroup="POSRequest" runat="server" ControlToValidate="ToTime" CssClass="text-danger" ErrorMessage="Select time range" Text="Please select to time"></asp:RequiredFieldValidator>
                                            </dd>
                                        </dl>
                                    </div>
                                    <div class="col-xs-4 col-md-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>IKEDC Agent Account Number</dt>
                                            <dd>
                                                <asp:TextBox ID="IKEDCAccountNumber" CssClass="form-control numberOnly" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="IKEDCAccountNumber" ValidationGroup="POSRequest" CssClass="text-danger" ErrorMessage="Please enter IKEDC Account Number" Text="Please enter IKEDC Account Number" runat="server"></asp:RequiredFieldValidator>
                                            </dd>
                                        </dl>
                                    </div>
                                    <div class="col-xs-4 col-md-3">
                                        <dl class="margin-sm-bottom">
                                            <dt>POS Type:</dt>
                                            <dd>
                                                <asp:DropDownList ID="POSType" CssClass="form-control" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="POSType" ValidationGroup="POSRequest" CssClass="text-danger" ErrorMessage="Please select POS type" Text="Please select POS type" runat="server"></asp:RequiredFieldValidator>
                                            </dd>
                                        </dl>
                                    </div>
                                    <div class="col-xs-4 col-md-3">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div ng-controller="LocationController">
                            <div ng-form="locationForm">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Location Information</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4 col-sm-3">
                                                <dl>
                                                    <dt>No. of Terminals</dt>
                                                    <dd>
                                                        <input type="text" ng-model="NoOfTerminal" name="NoOfTerminal" class="form-control keyDisable bfh-number" />
                                                    </dd>
                                                </dl>
                                            </div>
                                            <div class="col-xs-4 col-sm-3">
                                                <dl>
                                                    <dt>Settlement Account</dt>
                                                    <dd>
                                                        <select id="SettlementAccountNumber" class="form-control" ng-model="SettlementAccountNumber" name="SettlementAccountNumber" required ng-change="rAccountName(SettlementAccountNumber)">
                                                            <option value="">Select Settlement Account</option>
                                                        </select>
                                                        <p ng-show="locationForm.SettlementAccountNumber.$error.required && locationForm.SettlementAccountNumber.$touched" class="help-block text-danger">Please select settlement account number</p>
                                                    </dd>
                                                </dl>
                                            </div>
                                            <div class="col-xs-4 col-sm-3">
                                                <dl>
                                                    <dt>Settlement Account Name</dt>
                                                    <dd>
                                                        <asp:Literal ID="SettlementAccountName" runat="server" Text="{{SettlementAccountName}}"></asp:Literal>
                                                    </dd>
                                                </dl>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-4 col-sm-3">
                                                <dl>
                                                    <dt>Address</dt>
                                                    <dd>
                                                        <input type="text" class="form-control" name="LocationAddress" required ng-model="LocationAddress" />
                                                        <p ng-show="locationForm.LocationAddress.$error.required && locationForm.LocationAddress.$touched" class="help-block text-danger">Please enter address</p>
                                                    </dd>
                                                </dl>
                                                <dl class="margin-sm-bottom">
                                                    <dt>Contact Name</dt>
                                                    <dd>
                                                        <input class="form-control" name="ContactName" required type="text" ng-model="ContactName" />
                                                        <p ng-show="locationForm.ContactName.$error.required && locationForm.ContactName.$touched" class="help-block text-danger">Please enter record name</p>
                                                    </dd>
                                                </dl>
                                            </div>
                                            <div class="col-xs-4 col-sm-3">
                                                <dl>
                                                    <dt>State</dt>
                                                    <dd>
                                                        <asp:DropDownList ID="LocationState" ng-model="StateCode" CssClass="form-control" ng-change="getLGA(StateCode)" required runat="server"></asp:DropDownList>
                                                        <p ng-show="locationForm.LocationState.$error.required && locationForm.LocationState.$touched" class="help-block text-danger">Please select state</p>
                                                    </dd>
                                                </dl>
                                                <dl class="margin-sm-bottom">
                                                    <dt>Phone</dt>
                                                    <dd>
                                                        <input type="text" ng-model="ContactPhone" name="ContactPhone" required class="form-control numberOnly" />
                                                        <p ng-show="locationForm.ContactPhone.$error.required && locationForm.ContactPhone.$touched" class="help-block text-danger">Please enter phone number</p>
                                                    </dd>
                                                </dl>
                                            </div>
                                            <div class="col-xs-4 col-sm-3">
                                                <dl>
                                                    <dt>LGA</dt>
                                                    <dd>
                                                        <select class="form-control" name="LGA" required ng-model="LGA">
                                                            <option value="">Select LGA</option>
                                                            <option ng-repeat="lga in lgas">{{lga}}</option>
                                                        </select>
                                                        <p ng-show="locationForm.LGA.$error.required && locationForm.LGA.$touched" class="help-block text-danger">Please select LGA</p>
                                                    </dd>
                                                </dl>
                                                <dl class="margin-sm-bottom">
                                                    <dt>Email Address for notification</dt>
                                                    <dd>
                                                        <input type="email" class="form-control" required name="ContactEmail" ng-model="ContactEmail" />
                                                        <p ng-show="(locationForm.ContactEmail.$error.required || locationForm.ContactEmail.$error.email) && locationForm.ContactEmail.$touched" class="help-block text-danger">Please enter valid email</p>
                                                    </dd>
                                                </dl>
                                            </div>
                                            <div class="col-xs-4 col-sm-3">
                                                <dl>
                                                    <dt>City</dt>
                                                    <dd>
                                                        <asp:DropDownList ID="LocationCity" ng-model="City" CssClass="form-control" runat="server"></asp:DropDownList>
                                                        <p ng-show="locationForm.LocationCity.$error.required && locationForm.LocationCity.$touched" class="help-block text-danger">Please select location city</p>
                                                    </dd>
                                                </dl>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">POS Settlement Report Viewer</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4 col-sm-3">
                                                <dl>
                                                    <dt>First Name</dt>
                                                    <dd>
                                                        <input type="text" class="form-control" name="ViewerFirstName" required ng-model="ViewerFirstName" />
                                                        <p ng-show="locationForm.ViewerFirstName.$error.required && locationForm.ViewerFirstName.$touched" class="help-block text-danger">Please enter viewer name</p>
                                                    </dd>
                                                </dl>
                                            </div>
                                            <div class="col-xs-4 col-sm-3">
                                                <dl>
                                                    <dt>Last Name</dt>
                                                    <dd>
                                                        <input type="text" class="form-control" name="ViewerLastName" required ng-model="ViewerLastName" />
                                                        <p ng-show="locationForm.ViewerLastName.$error.required && locationForm.ViewerLastName.$touched" class="help-block text-danger">Please enter viewer last name</p>
                                                    </dd>
                                                </dl>
                                            </div>
                                            <div class="col-xs-4 col-sm-3">
                                                <dl>
                                                    <dt>Email Address</dt>
                                                    <dd>
                                                        <input type="email" class="form-control" name="ViewerEmail" required ng-model="ViewerEmail" />
                                                        <p ng-show="(locationForm.ViewerEmail.$error.required || locationForm.ViewerEmail.$error.email) && locationForm.ViewerLastName.$touched" class="help-block text-danger">Please enter viewer email</p>
                                                    </dd>
                                                </dl>
                                            </div>
                                        </div>
                                    </div>
                                </div>
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
                                                                    <input type="radio" name="notification" id="notificationYes" value="true" /> Yes
                                                                </label>
                                                                <label class="radio-inline">
                                                                    <input type="radio" name="notification" id="notificationNo" value="false" /> No
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
                                                                    <input type="radio" name="onlineAccess" id="OnlineAccessYes" value="true" > Yes
                                                                </label>
                                                                <label class="radio-inline">
                                                                    <input type="radio" name="OnlineAccess" id="OnlineAccessNo" value="false" > No
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
                                <asp:HiddenField ID="LocationJSON" runat="server" />
                                <asp:HiddenField ID="RelatedAccountJSON" runat="server" />
                                <input type="button" class="btn btn-primary" ng-disabled="locationForm.$invalid" value="Add To List" ng-click="addRow()" />
                                <br />
                                <br />
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
                                                <td><a data-toggle="modal" data-target="#notificationModal" ><span class="glyphicon glyphicon-info-sign"></span></a></td>
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
                    </div>
                    <br />
                </div>
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Documentation (Compulsory)
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-inline" role="form">
                            <div class="form-group">
                                <label for="Documentation" class="control-label col-md-2"></label>
                                <asp:FileUpload ID="Documentation" CssClass="form-control col-md-10" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>

                <%--<button id="SubmitPOS" runat="server" onserverclick="Submit_Click" value="Submit"></button>--%>

                <asp:Button ID="SubmitRequest" runat="server" ValidationGroup="POSRequest" formnovalidate CssClass="btn btn-primary" Text="Submit" OnClick="Submit_Click" />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //function SubmitLocation() {
        //    if (Page_ClientValidate('Location')) {
        //        angular.element(document.getElementById('locationController')).scope().addRow();
        //        return false;
        //    }
        //    return false;
        //}

        $(function () {

            if ($("#<%= Submitted.ClientID %>").val()) {
                var approvals = JSON.parse($("#<%= Submitted.ClientID %>").val());
                var msg = "POS Request Submitted successfully, To be approved by any of the following \n\n";
                $.each(approvals, function (i, name) {
                    msg += (name + "\n");
                });
                alert(msg);
                var url = window.location.href;
                window.location = url.replace("NewRequest.aspx", "AwaitingApproval.aspx");
            }

            //account search
            $(".accountNumber").blur(function () {
                jQuery.ajax({
                    type: "POST",
                    url: "NewRequest.aspx/GetCustomer",
                    data: "{ accountNumber: '" + $(this).val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        //$scope.customer = data.d;
                        if (data.d.Name) {
                            $("#<%= AccountName.ClientID %>").text(data.d.Name);
                            $("#<%= AccountOpeningDate.ClientID %>").text(data.d.AccountOpeningDate);
                            $("#<%= RCNumber.ClientID %>").text(data.d.RCNumber);
                            $("#<%= Address.ClientID %>").text(data.d.Address);
                            $("#<%= Sector.ClientID %>").text(data.d.Sector);
                            $("#<%= LastMonthTurnOver.ClientID %>").text(data.d.LastMonthTurnOver);
                            $("#<%= PhoneNumber.ClientID %>").val(data.d.PhoneNumber);
                            $("#<%= Email.ClientID %>").val(data.d.EmailAddress1);
                            $("#<%= AccountOfficer.ClientID %>").text(data.d.RSMName);
                            $("#<%= AccountStatus.ClientID %>").text(data.d.Status);
                            $("#<%= CustomerJSONHidden.ClientID %>").val(angular.toJson(data.d));
                            $("#<%= RelatedAccountJSON.ClientID %>").val(data.d.RelatedAccounts);
                            var accJson = JSON.parse(data.d.RelatedAccounts);
                            $("#SettlementAccountNumber").empty();
                            $("#SettlementAccountNumber").append($('<option>').text('Select Settlement Account').attr('value', ""));
                            $.each(accJson, function (i, relatedAccount) {
                                $("#SettlementAccountNumber").append($('<option>').text(relatedAccount.AccountNumber).attr('value', relatedAccount.AccountNumber));
                            });
                            if (data.d.InternalAccountStatus !== "Active" && data.d.InternalAccountStatus) {
                                $('div#internalAccStatus').show();
                            }
                            $('div#accountError').hide();
                        }
                        else {
                            $("#<%= AccountName.ClientID %>").text('');
                            $("#<%= AccountOpeningDate.ClientID %>").text('');
                            $("#<%= RCNumber.ClientID %>").text('');
                            $("#<%= Address.ClientID %>").text('');
                            $("#<%= Sector.ClientID %>").text('');
                            $("#<%= LastMonthTurnOver.ClientID %>").text('');
                            $("#<%= PhoneNumber.ClientID %>").val('');
                            $("#<%= Email.ClientID %>").val('');
                            $("#<%= AccountOfficer.ClientID %>").text('');
                            $("#<%= AccountStatus.ClientID %>").text('');
                            $("#<%= CustomerJSONHidden.ClientID %>").val('');
                            $("#<%= RelatedAccountJSON.ClientID %>").val('');
                            $("#SettlementAccountNumber").empty();
                            $('div#accountError').show();
                            $('div#internalAccStatus').hide();
                        }
                    },
                    error: function (data) {
                        $("#<%= AccountName.ClientID %>").text('');
                        $("#<%= AccountOpeningDate.ClientID %>").text('');
                        $("#<%= RCNumber.ClientID %>").text('');
                        $("#<%= Address.ClientID %>").text('');
                        $("#<%= Sector.ClientID %>").text('');
                        $("#<%= LastMonthTurnOver.ClientID %>").text('');
                        $("#<%= PhoneNumber.ClientID %>").val('');
                        $("#<%= Email.ClientID %>").val('');
                        $("#<%= AccountOfficer.ClientID %>").text('');
                        $("#<%= AccountStatus.ClientID %>").text('');
                        $("#<%= CustomerJSONHidden.ClientID %>").val('');
                        $("#<%= RelatedAccountJSON.ClientID %>").val('');
                        $("#SettlementAccountNumber").empty();
                        $('.accountError').show();
                        $('div#internalAccStatus').hide();
                    }
                });
            });


            if ($("#<%= CustomerJSONHidden.ClientID %>").val()) {
                var customer = JSON.parse($("#<%= CustomerJSONHidden.ClientID %>").val());
                $("#<%= AccountName.ClientID %>").text(customer.Name);
                $("#<%= AccountOpeningDate.ClientID %>").text(customer.AccountOpeningDate);
                $("#<%= RCNumber.ClientID %>").text(customer.RCNumber);
                $("#<%= Address.ClientID %>").text(customer.Address);
                $("#<%= Sector.ClientID %>").text(customer.Sector);
                $("#<%= LastMonthTurnOver.ClientID %>").text(customer.LastMonthTurnOver);
                <%--$("#<%= PhoneNumber.ClientID %>").val(customer.PhoneNumber);
                $("#<%= Email.ClientID %>").val(customer.EmailAddress1);--%>
                $("#<%= AccountOfficer.ClientID %>").text(customer.RSMName);
                $("#<%= AccountStatus.ClientID %>").text(customer.Status);
                $("#<%= RelatedAccountJSON.ClientID %>").val(customer.RelatedAccounts);
                var accJson = JSON.parse(customer.RelatedAccounts);
                $("#SettlementAccountNumber").empty();
                $("#SettlementAccountNumber").append($('<option>').text('Select Settlement Account').attr('value', ""));
                $.each(accJson, function (i, relatedAccount) {
                    $("#SettlementAccountNumber").append($('<option>').text(relatedAccount.AccountNumber).attr('value', relatedAccount.AccountNumber));
                });
            }

            //
            $('#<%= FromTime.ClientID %>').timepicker();

            $('#<%= FromTime.ClientID %>').on('changeTime', function () {
                $('#<%= ToTime.ClientID %>').timepicker('option', 'minTime', $(this).val());
            });
            $('.keyDisable').keydown(function (e) {
                return false;
            });

            $(".numberOnly").keydown(function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    // Allow: Ctrl+A, Command+A
                    (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                    // Allow: home, end, left, right, down, up
                    (e.keyCode >= 35 && e.keyCode <= 40)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });


            $('#<%= ToTime.ClientID %>').timepicker();
            //Show department when head office is selected
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
        })();
    </script>

    <script type="text/javascript">
        (function () {
            var locationApp = angular.module("pos", []);
            locationApp.controller('LocationController', function ($scope, $http) {
                $scope.locations = []; //[{ NoOfTerminal: 3, SettlementAccountNumber: "87358", Address: "21, ihfkjfjk", City: "hkgkjh", ContactName: "hkjdh", Phone: "89979", State: "Lagos" }, { NoOfTerminal: 3, SettlementAccountNumber: "87358", Address: "21, ihfkjfjk", City: "hkgkjh", ContactName: "hkjdh", Phone: "89979", State: "Lagos" }];
                if ($scope.locations.length === 0) {
                    if ($("#<%= LocationJSON.ClientID %>").val()) {
                        $scope.locations = JSON.parse($("#<%= LocationJSON.ClientID %>").val());
                    }
                }
                $scope.lgas = [];
                $scope.addRow = function () {
                    $scope.locations.push({ 'NoOfTerminal': $scope.NoOfTerminal, 'SettlementAccountNumber': $scope.SettlementAccountNumber, 'SettlementAccountName': $scope.SettlementAccountName, 'Address': $scope.LocationAddress, 'City': $scope.City, 'LGA': $scope.LGA, 'StateCode': $scope.StateCode, 'State': $('#<%= LocationState.ClientID %> option:selected').text(), 'ContactName': $scope.ContactName, 'Phone': $scope.ContactPhone, 'Email': $scope.ContactEmail, 'ViewerFirstName': $scope.ViewerFirstName, 'ViewerLastName': $scope.ViewerLastName, 'ViewerEmail': $scope.ViewerEmail });
                    $scope.NoOfTerminal = '1';
                    $scope.SettlementAccountName = '';
                    $scope.SettlementAccountNumber = '';
                    $scope.LocationAddress = '';
                    $scope.City = '';
                    $scope.LGA = '';
                    $scope.StateCode = '';
                    $scope.State = '';
                    $scope.ContactName = '';
                    $scope.ContactPhone = '';
                    $scope.ContactEmail = '';
                    $scope.ViewerFirstName = '';
                    $scope.ViewerLastName = '';
                    $scope.ViewerEmail = '';
                    $("#<%= LocationJSON.ClientID %>").val(angular.toJson($scope.locations));
                    $scope.locationForm.$setUntouched();
                    $scope.locationForm.$setPristine();
                    $scope.$apply();
                };

                $scope.rAccountName = function (SettlementAccountNumber) {
                    var relateAccount = JSON.parse($("#<%= RelatedAccountJSON.ClientID %>").val());
                    $scope.SettlementAccountName = Enumerable.From(relateAccount).Where(function (x) { return x.AccountNumber === SettlementAccountNumber }).FirstOrDefault().AccountName;
                    $scope.$apply();
                };

                $scope.getLGA = function (state) {
                    jQuery.ajax({
                        type: "POST",
                        url: "NewRequest.aspx/GetLocalGoverment",
                        data: "{ stateCode: '" + state + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            $scope.lgas = data.d;
                            $scope.$apply();
                        },
                        error: function (data) {
                        }
                    });
                }

                $scope.getTotal = function () {
                    var total = 0;
                    for (var i = 0; i < $scope.locations.length; i++) {
                        var location = $scope.locations[i];
                        total += parseInt(location.NoOfTerminal);
                    }
                    return total;
                }
            });
            //Account Controller
        })();

    </script>








</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    POS Request
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    My Application Page
</asp:Content>
