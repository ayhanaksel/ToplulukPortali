<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TubitetBackEnd.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <ext:ResourceManager runat="server" />

    <ext:Viewport runat="server">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel
                runat="server"
                Title="Giriş Ekranı"
                Width="400"
                Frame="true"
                BodyPadding="13"
                DefaultAnchor="100%">
                <Items>
                    <ext:TextField
                        runat="server"
                        AllowBlank="false"
                        FieldLabel="Kullanıcı Adı"
                        Name="txtEmail"
                        ID="txtEmail"
                        EmptyText="Kullanıcı Adı" /> 

                    <ext:TextField
                        runat="server"
                        AllowBlank="false"
                        FieldLabel="Şifre"
                        Name="txtPass"
                        ID="txtPass"
                        EmptyText="Şifre"
                        InputType="Password" />

                    <ext:Checkbox runat="server" FieldLabel="Remember me" Name="remember"/>        

                </Items>
                <Buttons>         
                    <ext:Button runat="server" ID="btnLogin" Text="Giriş" Icon="DoorIn" IconAlign="Right" Margins="1 10 1 10" OnDirectClick="btnLogin_DirectClick"></ext:Button>
                </Buttons>
            </ext:FormPanel>
        </Items>
    </ext:Viewport>


     <ext:Window runat="server" ID="wndHata" Title="UYARI" Modal="true" Hidden="true" width="350" Height="150" Icon="Error">
        <Items>           
            <ext:Label runat="server" ID="lbHataMessage" Text="Kullanıcı Adı veya Şifre hatalı" Padding="10"></ext:Label>
        </Items>
        <Buttons>
            <ext:Button runat="server" ID="btnClose" Text="Kapat" Icon="Cancel" OnDirectClick="btnClose_DirectClick"></ext:Button>
        </Buttons>
    </ext:Window>

    <ext:Window runat="server" ID="wndYetkisiz" Title="UYARI" Modal="true" Hidden="true" width="350" Height="150" Icon="Error">
        <Items>           
            <ext:Label runat="server" ID="Label1" Text="Giriş için yetkiniz yok !" Padding="10"></ext:Label>
        </Items>
        <Buttons>
            <ext:Button runat="server" ID="Button1" Text="Kapat" Icon="Cancel" OnDirectClick="Button1_DirectClick"></ext:Button>
        </Buttons>
    </ext:Window>

</body>
</html>
