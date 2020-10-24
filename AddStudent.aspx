<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeBehind="AddStudent.aspx.cs" Inherits="Lab_4.AddStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="App_Themes/SiteStyles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container sm">
        <h1>Add Student Records</h1>
        <br />

        <asp:Label ID="lblAddedCourses" runat="server">Course:</asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="drpAddedCourses" runat="server" AutoPostBack="true" CssClass="dropdown" OnSelectedIndexChanged="drpAddedCourses_SelectedIndexChanged"></asp:DropDownList>

        <br /><br />

        <asp:Label ID="lblStudentNum" runat="server">Student Number:</asp:Label>
        &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtStudentNum" runat="server" CssClass="input" />

        <asp:Label ID="validateTxtStudentNum" runat="server" CssClass="error"></asp:Label>
        <asp:Label ID="ValidateIfExistId" runat="server" CssClass="error"></asp:Label>

        <br /><br />

        <asp:Label ID="lblStudentName" runat="server">Student Name:</asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtStudentName" runat="server" CssClass="col control input" />

        <asp:Label ID="validateStudentName" runat="server" CssClass="error"></asp:Label>


        <br /><br />

        <asp:Label ID="lblGrade" runat="server">Grade:</asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtGrade" runat="server" CssClass="col control input" />

        <asp:Label ID="validateTxtStudentGrade" runat="server" CssClass="error"></asp:Label>


        <br /><br />

        <asp:Button ID="btnAddStudent" runat="server" AutoPostBack="true" Text="Add to Course" CssClass="btn btn-primary" OnClick="btnAddStudent_Click" />
    
        <br /><br />
    
        <h3>The selected course has the following student records:</h3>
        <br />
        <asp:Table ID="tblStudents" runat="server" CssClass="table">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Id</asp:TableHeaderCell>
                <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                <asp:TableHeaderCell>Grade</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
