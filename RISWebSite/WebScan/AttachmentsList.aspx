<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttachmentsList.aspx.cs" Inherits="WebScan_AttachmentsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
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
                    <asp:HyperLink ID="hlAddAttachment2" runat="server">
                        Attach a new document to this Exam
                    </asp:HyperLink>
                </td>
                </tr>
                </table>
   
   
            <div >
            
                <asp:GridView ID="gvAttachments" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                    DataKeyNames="AttachmentId" DataSourceID="odsAttachments" ForeColor="#333333" 
                    GridLines="None">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Description" HeaderText="Description" 
                            SortExpression="Description" />
                        <asp:BoundField DataField="UserName" HeaderText="Scanned By" 
                            SortExpression="UserName" ReadOnly="True" />
                        <asp:BoundField DataField="ScannedTime" HeaderText="Scan Time" 
                            SortExpression="ScannedTime" ReadOnly="True" />
                        <asp:TemplateField HeaderText="AttachmentId" InsertVisible="False" 
                            SortExpression="AttachmentId">
                            <EditItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"DownloadAttachment.aspx?attachmentId=" + Eval("AttachmentId") %>' Target="_blank">Attachment</asp:HyperLink>
                                
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"DownloadAttachment.aspx?attachmentId=" + Eval("AttachmentId") %>' Target="_blank">Attachment</asp:HyperLink>                            
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        Nothing attached with this Exam.
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            
            </div>
            
        </div>
    
    </center>
            <asp:ObjectDataSource ID="odsAttachments" runat="server" DeleteMethod="Delete" 
                InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" 
                SelectMethod="GetAttachmentsForStudy" 
                TypeName="AttachmentsTableAdapters.tAttachmentsTableAdapter" 
                UpdateMethod="Update">
                <DeleteParameters>
                    <asp:Parameter Name="Original_AttachmentId" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Original_AttachmentId" Type="Int32" />
                    <asp:Parameter Name="AttachmentId" Type="Int32" />
                </UpdateParameters>
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="studyId" 
                        QueryStringField="studyId" Type="Int32" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="StudyId" Type="Int32" />
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="ScannedBy" Type="Int32" />
                    <asp:Parameter Name="ScannedTime" Type="DateTime" />
                    <asp:Parameter Name="AttachmentData" Type="Object" />
                </InsertParameters>
            </asp:ObjectDataSource>
    </form>
    
    </body>
</html>
