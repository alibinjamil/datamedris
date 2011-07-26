<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="HospitalList.aspx.cs" Inherits="SuperUser_HospitalList" Title="DataMed | Radiology Information System | Hospital Management" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Hospitals List</h1>
<div style="text-align:center">
    <table class="dataEntryTable">
        <tr>
            <td align="right" class="heading">Name:</td>
            <td align="left"><asp:TextBox ID="tbName" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        <tr>
            <td align="right" class="heading">Description:</td>
            <td align="left"><asp:TextBox ID="tbDesc" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        <tr>
            <td align="right" class="heading">Address:</td>
            <td align="left"><asp:TextBox ID="tbAddress" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        <tr>
            <td align="right" class="heading">City:</td>
            <td align="left"><asp:TextBox ID="tbCity" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        <tr>
            <td align="right" class="heading">State:</td>
            <td align="left">
                <asp:DropDownList ID="ddlState" runat="server">
                    <asp:ListItem>Alabama</asp:ListItem>
                    <asp:ListItem>Alaska</asp:ListItem>
                    <asp:ListItem>American Samoa</asp:ListItem>
                    <asp:ListItem>Arizona</asp:ListItem>
                    <asp:ListItem>Arkansas</asp:ListItem>
                    <asp:ListItem>California</asp:ListItem>
                    <asp:ListItem>Colorado</asp:ListItem>
                    <asp:ListItem>Connecticut</asp:ListItem>
                    <asp:ListItem>Delaware</asp:ListItem>
                    <asp:ListItem>District of Columbia</asp:ListItem>
                    <asp:ListItem>Florida</asp:ListItem>
                    <asp:ListItem>Georgia</asp:ListItem>
                    <asp:ListItem>Guam</asp:ListItem>
                    <asp:ListItem>Hawaii</asp:ListItem>
                    <asp:ListItem>Idaho</asp:ListItem>
                    <asp:ListItem>Illinois</asp:ListItem>
                    <asp:ListItem>Indiana</asp:ListItem>
                    <asp:ListItem>Iowa</asp:ListItem>
                    <asp:ListItem>Kansas</asp:ListItem>
                    <asp:ListItem>Kentucky</asp:ListItem>
                    <asp:ListItem>Louisiana</asp:ListItem>
                    <asp:ListItem>Maine</asp:ListItem>
                    <asp:ListItem>Maryland</asp:ListItem>
                    <asp:ListItem>Massachusetts</asp:ListItem>
                    <asp:ListItem>Michigan</asp:ListItem>
                    <asp:ListItem>Minnesota</asp:ListItem>
                    <asp:ListItem>Mississippi</asp:ListItem>
                    <asp:ListItem>Missouri</asp:ListItem>
                    <asp:ListItem>Montana</asp:ListItem>
                    <asp:ListItem>Nebraska</asp:ListItem>
                    <asp:ListItem>Nevada</asp:ListItem>
                    <asp:ListItem>New Hampshire</asp:ListItem>
                    <asp:ListItem>New Jersey</asp:ListItem>
                    <asp:ListItem>New Mexico</asp:ListItem>
                    <asp:ListItem>New York</asp:ListItem>
                    <asp:ListItem>North Carolina</asp:ListItem>
                    <asp:ListItem>North Dakota</asp:ListItem>
                    <asp:ListItem>Northern Marianas Islands</asp:ListItem>
                    <asp:ListItem>Ohio</asp:ListItem>
                    <asp:ListItem>Oklahoma</asp:ListItem>
                    <asp:ListItem>Oregon</asp:ListItem>
                    <asp:ListItem>Pennsylvania</asp:ListItem>
                    <asp:ListItem>Puerto Rico</asp:ListItem>
                    <asp:ListItem>Rhode Island</asp:ListItem>
                    <asp:ListItem>South Carolina</asp:ListItem>
                    <asp:ListItem>South Dakota</asp:ListItem>
                    <asp:ListItem>Tennessee</asp:ListItem>
                    <asp:ListItem>Texas</asp:ListItem>
                    <asp:ListItem>Utah</asp:ListItem>
                    <asp:ListItem>Vermont</asp:ListItem>
                    <asp:ListItem>Virginia</asp:ListItem>
                    <asp:ListItem>Virgin Islands</asp:ListItem>
                    <asp:ListItem>Washington</asp:ListItem>
                    <asp:ListItem>West Virginia</asp:ListItem>
                    <asp:ListItem>Wisconsin</asp:ListItem>
                    <asp:ListItem>Wyoming</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td align="right" class="heading">Zip:</td>
            <td align="left"><asp:TextBox ID="tbZip" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        
    </table>
</div>
</asp:Content>

