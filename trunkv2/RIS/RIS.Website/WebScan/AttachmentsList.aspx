<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttachmentsList.aspx.cs" Inherits="WebScan_AttachmentsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DataMed | Radiology Information System | List of Views</title>
</head>
<body>
        
    <form id="form1" runat="server">
    <center>
        
        <div style="width:1000px">
            <div>
                <img src="<%=GetHeaderURL()%>" />
            </div>
   
                <table style="margin-top:20px;margin-bottom:20px;">
                <tr>
                <td>
                <asp:HyperLink ID="hlAddAttachment1" runat="server">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/derivatives.png" />                        
                </asp:HyperLink>
                </td>
                
                <td>
                    <asp:HyperLink ID="hlAddAttachment2" runat="server" >
                        Attach a new View to this Exam
                    </asp:HyperLink>
                </td>
                </tr>
                </table>
   
   
            <div >
            
                
            
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    DataKeyNames="AttachmentId" DataSourceID="edsAttachments" ForeColor="Black" 
                    GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="AttachmentId" HeaderText="AttachmentId" 
                            ReadOnly="True" SortExpression="AttachmentId" />
                        <asp:BoundField DataField="StudyId" HeaderText="StudyId" 
                            SortExpression="StudyId" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Description" HeaderText="Description" 
                            SortExpression="Description" />
                        <asp:BoundField DataField="ScannedBy" HeaderText="ScannedBy" 
                            SortExpression="ScannedBy" />
                        <asp:BoundField DataField="ScannedTime" HeaderText="ScannedTime" 
                            SortExpression="ScannedTime" />
                        <asp:BoundField DataField="AttachmentType" HeaderText="AttachmentType" 
                            SortExpression="AttachmentType" />
                        <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                            
                                                      
                                <asp:HyperLink runat="server" NavigateUrl='<%#"~/WebScan/DownloadAttachment.aspx?attachmentId=" + Eval("AttachmentId")%>'>Download</asp:HyperLink>
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
            
                
            
                <asp:EntityDataSource ID="edsAttachments" runat="server" 
                    ConnectionString="name=RISEntities" DefaultContainerName="RISEntities" 
                    EnableDelete="True" EnableFlattening="False" EntitySetName="Attachments" 
                    EnableUpdate="True" EntityTypeFilter="Attachment" Where="" 
                    AutoGenerateWhereClause="True">
                    <WhereParameters>
                        <asp:QueryStringParameter DefaultValue="0" Name="StudyId" 
                            QueryStringField="studyId" DbType="Int32" />
                    </WhereParameters>
                </asp:EntityDataSource>
            
            </div>
            
        </div>
    
    </center>
    </form>
    
    </body>
</html>
