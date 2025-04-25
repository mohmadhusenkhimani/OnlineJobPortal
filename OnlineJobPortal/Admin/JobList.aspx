<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="OnlineJobPortal.Admin.JobList"  EnableEventValidation="false" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div style="background-image: url('../images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container-fluid pt-4 pb-4">
            <%--<div>
                <asp:Label ID="llbMsg" runat="server"></asp:Label>
            </div>--%>


            <div class="btn-toolbar justify-content-between mb-3">
                <div class="btn-group">
                    <asp:Label ID="llbMsg" runat="server"></asp:Label>
                </div>

                <div class="input-group h-25">
                <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="~/Admin/ViewResume.aspx" CssClass="btn btn-secondary" Visible="false"> < Back</asp:HyperLink>
            </div>


            </div>


            


            <h3 class="text-center">Job List/Details</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No Record to display..!" AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="JobId" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            
                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Title" HeaderText="Job Title">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="NoOfPost" HeaderText="NoOfPost">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Qualification" HeaderText="Qualification">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Experience" HeaderText="Experience">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="LastDateToApply" HeaderText="Valid Till" DataFormatString="{0:dd MMMM yyyy}">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="CompanyName" HeaderText="Company">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                             <asp:BoundField DataField="Country" HeaderText="Country">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                             <asp:BoundField DataField="State" HeaderText="State">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="CreateDate" HeaderText="Posted Date" DataFormatString="{0:dd MMMM yyyy}">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:TemplateField Headertext="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditJob" runat="server" CommandName="EditJob" CommandArgument='<%# Eval("JobId") %>'>
                                        <asp:Image  ID="Img" runat="server" ImageUrl="../assets/img/icon/editPencil.png" Height="25px"/>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Center" Width="50px"/>
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
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Report" />

        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />


</asp:Content>
