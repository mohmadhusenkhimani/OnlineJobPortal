<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="OnlineJobPortal.Admin.UserList" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('../images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container-fluid pt-4 pb-4">
            <div>
                <asp:Label ID="llbMsg" runat="server"></asp:Label>
            </div>


            <h3 class="text-center">User List/Details</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" HeaderStyle-HorizontalAlign="Center" EmptyDataText="No Record to display..!" AutoGenerateColumns="False" AllowPaging="True" PageSize="5"  DataKeyNames="UserId" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting">
                        <Columns>
                            
                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Name" HeaderText="User Name">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Email" HeaderText="Email">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Mobile" HeaderText="Mobile No">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Country" HeaderText="Country">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             

                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="../assets/img/icon/trashIcon.png" 
                                        Width="25px" Height="25px" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this job?');"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>
                        <HeaderStyle BackColor="#7200cf" ForeColor="White"/>
                    </asp:GridView>
                </div>
            </div>

        </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Report" />
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
    </div>
</asp:Content>
