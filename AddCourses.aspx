<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeBehind="AddCourses.aspx.cs" Inherits="Lab_4.AddCourses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="App_Themes/SiteStyles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container sm">
        <h1>Add New Courses</h1>
        <br/>

        <asp:Label ID="lblCourseNum" runat="server">Course Number:</asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="txtCourseNum" runat="server" ValidationGroup = "standard" CssClass="col control input" />

        <asp:Label ID="validateTxtCourseNum" runat="server" CssClass="error"></asp:Label>
        <asp:Label ID="ValidateIfExistNum" runat="server" CssClass="error"></asp:Label>


        <br /><br />
        <asp:Label ID="lblCourseName" runat="server">Course Name:</asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtCourseName" runat="server" CssClass="col control input" />

        <asp:Label ID="validateTxtCourseName" runat="server" CssClass="error"></asp:Label>


        <br /><br />
        <asp:Button ID="btnAddCourse" runat="server" Text="Submit Course Information" CssClass="btn btn-primary" OnClick="btnAddCourse_Click"/>

        <br /><br />

        <h3>The following courses are currently in the system:</h3>
        <br />
        <asp:Table ID="tblCourses" runat="server" CssClass="table">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Course Code</asp:TableHeaderCell>
                <asp:TableHeaderCell>Course Title</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
