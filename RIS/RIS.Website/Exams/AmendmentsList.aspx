<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AmendmentsList.aspx.cs" Inherits="Exams_AmendmentsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Common/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0">
                <tr>
                    <td align="right" style="width:14%" class="heading">Patient:</td>
                    <td align="left" style="width:19%" class="normal">
                        <asp:Label ID="lblPatientName" runat="server" Text=""></asp:Label></td>
                    <td align="right" style="width:14%" class="heading">Patient ID:</td>
                    <td align="left" style="width:14%" class="normal"><asp:Label ID="lblPatientId" runat="server" Text=""></asp:Label></td>
                    <td align="right"  class="heading" style="width:14%" class="normal"> Status:</td>
                    <td align="left"  style="width:13%" class="normal"><asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></td>
                    
                </tr>
                <tr>
                    <td align="right"  class="heading"> Exam Date:</td>
                    <td align="left" class="normal"><asp:Label ID="lblExamDate" runat="server" Text=""></asp:Label></td>
                    <td align="right"  class="heading"> Modality:</td>
                    <td align="left" class="normal"><asp:Label ID="lblModality" runat="server" Text=""></asp:Label></td>
                    <td align="right"  class="heading"> Procedure:
                    </td>
                    <td align="left" class="normal"><asp:Label ID="lblProcedure" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td align="right"  class="heading"> Radiologist:</td>
                    <td align="left" class="normal"><asp:Label ID="lblRadiologist" runat="server" Text=""></asp:Label></td>
                    <td align="right"  class="heading"> Physician:</td>
                    <td align="left" class="normal"><asp:Label ID="lblPhysician" runat="server" Text=""></asp:Label></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td class="heading" align="right">Tech Comments:</td>
                    <td colspan="6">
                    <asp:TextBox ID="techComments" runat="server" Width="850px" Rows="2" 
                            TextMode="MultiLine" ReadOnly="True" ></asp:TextBox></td>
                </tr>
                
            </table>
        
            <hr />

            <asp:GridView ID="gvAmendmentsList" runat="server" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
            GridLines="Vertical" Width="100%">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Amendment">
                        <HeaderStyle Width="60%" />
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Amendment")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Radiologist Name">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("ReportDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Exam Time">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("RadiologistName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
