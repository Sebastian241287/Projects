<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="NoBD.Chat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Prijavljeni ste kot
    
        <asp:Label ID="CurrentUser" runat="server" Text="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Logout" runat="server" Text="Odjava" OnClick="Logout_Click" />
    
    </div>
        <asp:ListBox ID="Messages" runat="server" Height="274px" Width="448px" DataSourceID="SqlDataSource1" DataTextField="besedilo" DataValueField="Id"></asp:ListBox>
        <asp:ListBox ID="Users" runat="server" Height="274px" Width="182px"></asp:ListBox>
        <p>
            <asp:TextBox ID="Message" runat="server" Width="438px"></asp:TextBox>
            <asp:Button ID="Send" runat="server" OnClick="Send_Click" Text="Pošlji" />
            <asp:Button ID="Refresh" runat="server" Text="Osveži" OnClick="Refresh_Click" />
        </p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:chatdbpbConnectionString %>" DeleteCommand="DELETE FROM [Pogovor] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Pogovor] ([username], [besedilo], [cas]) VALUES (@username, @besedilo, @cas)" SelectCommand="SELECT * FROM [Pogovor]" UpdateCommand="UPDATE [Pogovor] SET [username] = @username, [besedilo] = @besedilo, [cas] = @cas WHERE [Id] = @Id">
            <DeleteParameters>
                <asp:Parameter Name="Id" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="username" Type="String" />
                <asp:Parameter Name="besedilo" Type="String" />
                <asp:Parameter Name="cas" Type="DateTime" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="username" Type="String" />
                <asp:Parameter Name="besedilo" Type="String" />
                <asp:Parameter Name="cas" Type="DateTime" />
                <asp:Parameter Name="Id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
