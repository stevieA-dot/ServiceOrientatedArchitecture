<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GUI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-12 mx-auto">
                <div class="row">
                    <div class="col">
                        <center>
                            <img width="150" src="Images/generalUser.png" />
                         </center>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <center>
                            <h3>Login</h3>
                        </center>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <hr />
                    </div>
                </div>
                 <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <center>
                                <asp:TextBox runat="server" ID="usernameTxt" Width="200" CssClass="form-control" placeholder="Username"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="usernameTxt" 
                                    ErrorMessage="Username is required" 
                                    Display="static"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </center>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <center>
                                <asp:TextBox runat="server" ID="passwordTxt" Width="200" CssClass="form-control" placeholder="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" 
                                    ControlToValidate="passwordTxt" 
                                    ErrorMessage="Password is required" 
                                    Display="static"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </center>
                         </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <center>
                                <asp:Button runat="server" ID="registerBtn" ClientInstanceName="registerBtn" Width="100" CssClass="form-control" Text="Register" OnClick="registerBtn_Click">
                                </asp:Button>
                            </center>
                         </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <center>
                                <asp:label runat="server" ID="errorLbl" ClientInstanceName="errorLbl" Width="200" CssClass="form-control">
                                </asp:label>
                            </center>
                         </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <center>
                                <asp:Button runat="server" ID="loginBtn" ClientInstanceName="loginBtn" Width="100" Text="Login" OnClick="loginBtn_Click">
                                </asp:Button>
                            </center>
                         </div>
                    </div>
                </div>

            </div>
        </div>
    </div>


</asp:Content>
