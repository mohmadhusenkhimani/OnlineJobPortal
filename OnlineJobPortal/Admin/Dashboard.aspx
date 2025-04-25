<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="OnlineJobPortal.Admin.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.12.1/css/all.min.css" />

    <style>
        .card {
            background-color: #fff;
            border-radius: 10px;
            border: none;
            position: relative;
            margin-bottom: 30px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            padding: 20px;
            text-align: center;
        }

        .card .card-icon {
            font-size: 50px;
            margin-bottom: 10px;
        }

        .l-bg-cherry {
            background: linear-gradient(to right, #493240, #f09) !important;
            color: #fff;
        }

        .l-bg-blue-dark {
            background: linear-gradient(to right, #373b44, #4286f4) !important;
            color: #fff;
        }

        .l-bg-green-dark {
            background: linear-gradient(to right, #0a504a, #38ef7d) !important;
            color: #fff;
        }

        .l-bg-orange-dark {
            background: linear-gradient(to right, #a86008, #ffba56) !important;
            color: #fff;
        }

        .dashboard-content, .contacts-content {
            font-size: 12px;
        }

        .l-bg-orange-dark h2, .l-bg-orange-dark h5 {
            font-size: 25px !important;
        }
    </style>
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <div class="container pt-4">
        <h2 class="text-center">Dashboard</h2>
        <div class="row justify-content-center mt-4">
            <div class="col-md-3">
                <div class="card l-bg-cherry">
                    <i class="fas fa-users card-icon"></i>
                    <h5>Total Users</h5>
                    <h2><% Response.Write(Session["Users"]); %></h2>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card l-bg-blue-dark">
                    <i class="fas fa-briefcase card-icon"></i>
                    <h5>Total Jobs</h5>
                    <h2><% Response.Write(Session["Jobs"]); %></h2>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card l-bg-green-dark">
                    <i class="fas fa-check-square card-icon"></i>
                    <h5>Applied Jobs</h5>
                    <h2><% Response.Write(Session["AppliedJobs"]); %></h2>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card l-bg-orange-dark">
                    <i class="fas fa-comments card-icon"></i>
                    <h2>Contact Users</h2>
                    <h5><% Response.Write(Session["Contact"]); %></h5>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
