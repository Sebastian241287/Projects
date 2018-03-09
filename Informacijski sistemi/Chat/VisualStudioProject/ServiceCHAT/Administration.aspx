<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administration.aspx.cs" Inherits="NoBD.Administration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ListBox ID="ListBox1" runat="server" Height="280px" Width="327px"></asp:ListBox>
    
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:chatdbpbConnectionString %>" SelectCommand="SELECT * FROM [Uporabnik] WHERE ([username] = @username)">
            <SelectParameters>
                <asp:ControlParameter ControlID="MUsername" Name="username" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:TextBox ID="MUsername" runat="server"></asp:TextBox>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:chatdbpbConnectionString %>" SelectCommand="SELECT username,COUNT(*) FROM [Pogovor] GROUP BY username"></asp:SqlDataSource>
        <asp:Button ID="delete" runat="server" OnClick="delete_Click" Text="Zbriši uporabnika" />
        <asp:Button ID="admin" runat="server" OnClick="admin_Click" Text="Administrator" />
        <asp:Button ID="odjava" runat="server" OnClick="odjava_Click" Text="Odjava" />
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:chatdbpbConnectionString %>" DeleteCommand="DELETE FROM [Uporabnik] WHERE [username] = @original_username" InsertCommand="INSERT INTO [Uporabnik] ([username], [ime], [priimek], [geslo]) VALUES (@username, @ime, @priimek, @geslo)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [Uporabnik]" UpdateCommand="UPDATE [Uporabnik] SET [ime] = @ime, [priimek] = @priimek, [geslo] = @geslo WHERE [username] = @original_username">
            <DeleteParameters>
                <asp:Parameter Name="original_username" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="username" Type="String" />
                <asp:Parameter Name="ime" Type="String" />
                <asp:Parameter Name="priimek" Type="String" />
                <asp:Parameter Name="geslo" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="ime" Type="String" />
                <asp:Parameter Name="priimek" Type="String" />
                <asp:Parameter Name="geslo" Type="String" />
                <asp:Parameter Name="original_username" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:chatdbpbConnectionString %>" DeleteCommand="DELETE FROM [Pogovor] WHERE [username] = @username" InsertCommand="INSERT INTO [Pogovor] ([username], [besedilo]) VALUES (@username, @besedilo)" SelectCommand="SELECT * FROM [Pogovor]" UpdateCommand="UPDATE [Pogovor] SET [username] = @username, [besedilo] = @besedilo WHERE [Id] = @Id">
            <DeleteParameters>
                <asp:Parameter Name="username" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="username" Type="String" />
                <asp:Parameter Name="besedilo" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="username" Type="String" />
                <asp:Parameter Name="besedilo" Type="String" />
                <asp:Parameter Name="Id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:chatdbpbConnectionString %>" DeleteCommand="DELETE FROM [Uporabnik] WHERE [username] = @username" InsertCommand="INSERT INTO [Uporabnik] ([username], [ime], [priimek], [geslo], [admin]) VALUES (@username, @ime, @priimek, @geslo, @admin)" SelectCommand="SELECT * FROM [Uporabnik]" UpdateCommand="UPDATE [Uporabnik] SET [admin] = @admin WHERE [username] = @username">
            <DeleteParameters>
                <asp:Parameter Name="username" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="username" Type="String" />
                <asp:Parameter Name="ime" Type="String" />
                <asp:Parameter Name="priimek" Type="String" />
                <asp:Parameter Name="geslo" Type="String" />
                <asp:Parameter Name="admin" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="admin" />
                <asp:Parameter Name="username" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </form>
</body>
</html>
