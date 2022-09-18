<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="GUI.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <script type="text/javascript">
        function ExceuteAPICall(numOfOperands, name, apiEndpoint) {
            var textBoxThree = document.getElementById('numThreetxt');
            var lblThree = document.getElementById('numThreeLbl');
            var validatorThree = document.getElementById('numThreeValidator');
            var modalPopup = document.getElementById("modalPanel");
            document.getElementById('nameLbl').innerHTML = name;

            if (numOfOperands == 2) {
                textBoxThree.hidden = true;
                lblThree.hidden = true;
                validatorThree.style.visibility = "hidden";
                validatorThree.enabled = false;
            }

            modalPopup.style.left = "350px"
            modalPopup.style.top = "200px"
            modalPopup.style.display = "inline";

            var apiEndpoint = $.ajax(
                {
                    type: "POST", url: "https://localhost:44369/Menu", data: "&apiEndpoint=" + apiEndpoint
                });
        }
    </script>

    <hr />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <asp:Button runat="server" ID="allServicesBtn"  Text="All Services" OnClick="allServicesBtn_Click"/>
                    <asp:Button runat="server" ID="searchBtn"  Text="Search" OnClick="searchBtn_Click" ValidationGroup="search"/>
                    <asp:TextBox runat="server" ID="searchTxt" placeholder="Enter Search Term" ValidationGroup="search"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" 
                        ControlToValidate="searchTxt" 
                        ErrorMessage="Search term required" 
                        Display="static" ValidationGroup="search" 
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="panel-primary">
                    <div class="panel-heading"><%Response.Write(PanelHeader); %></div>
                        <div class="panel-body pan">
                            <div class="form-body pal">
                                <div class="table-responsive">
                                    <table id="resultsTable" style="border-bottom: 1px solid #ddd" ClientInstanceName="resultsTable"
                                        class="table table-striped table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    Name
                                                </th>
                                                <th>
                                                    Description
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Literal ID="litResults" runat="server"></asp:Literal>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="resultPanel">
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:Label runat="server" ID ="resultsLbl" Text="Results"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:TextBox runat="server" ID="resultsTxt" placeholder="Results"></asp:TextBox>
                </div>
            </div>

        </asp:Panel>
        <ModalPopupExtender ID="mpe" runat="server" PopupControlID="modalPanel" CancelControlID="btnClose"/>
        <asp:Panel ID="modalPanel" ClientIDMode="Static" runat="server" Width="500px" Height="500px" CssClass="modal" BackColor="Gray">
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="nameLbl" ClientIDMode="static" CssClass="style=" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="numOneLbl" ClientIDMode="Static" Text="Enter your first number"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="numOnetxt" ClientIDMode="Static" placeholder="Number one" CausesValidation="true" TextMode="Number" ></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="numOnetxt" ErrorMessage="number required" Display="Dynamic" ValidationGroup="modalPanel"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="numTwoLbl" ClientIDMode="Static" Text="Enter your second number"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="numTwotxt" placeholder="Number two" CausesValidation="true" TextMode="Number"> </asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="numTwotxt" ErrorMessage="number required" Display="Dynamic" ValidationGroup="modalPanel"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="numThreeLbl" ClientIDMode="Static" Text="Enter your third number"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="numThreetxt" ClientIDMode="Static" placeholder="Number three" CausesValidation="true" TextMode="Number" > </asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="numThreeValidator" ClientIDMode="Static" ControlToValidate="numThreetxt" ErrorMessage="number required" Display="Dynamic" ValidationGroup="modalPanel"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Button runat="server" ID="testBtn" Text="Test" OnClick="testBtn_Click" ValidationGroup="modalPanel" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnClose" Text="Close" />
                    </div>
                </div>
            </div>
            
            
        </asp:Panel>
    </div>
</asp:Content>
