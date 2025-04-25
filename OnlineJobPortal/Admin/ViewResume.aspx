<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ViewResume.aspx.cs" Inherits="OnlineJobPortal.Admin.ViewResume" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


      <div style="background-image: url('../images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container-fluid pt-4 pb-4">
            <div>
                <asp:Label ID="llbMsg" runat="server"></asp:Label>
            </div>


            <h3 class="text-center">View Resume/Download Resume</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No Record to display..!" AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="AppliedJobId" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                        <Columns>
                            
                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                             <asp:BoundField DataField="CompanyName" HeaderText="Company Name">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Title" HeaderText="Job Title">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Name" HeaderText="User Name">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Email" HeaderText="User Email">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Mobile" HeaderText="Mobile No.">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Resume">
                            <ItemTemplate>
                                <asp:HyperLink 
                                    ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("../{0}", Eval("Resume"))%>'>
                                    <i class="fas fa-download"></i>Download
                                </asp:HyperLink>

                                <asp:HiddenField 
                                    ID="hdnJobId" runat="server" Value='<%# Bind("JobId") %>' />
                           </ItemTemplate>
                           <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


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
    </div>

</asp:Content>
