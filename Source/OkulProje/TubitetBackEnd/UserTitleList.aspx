<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserTitleList.aspx.cs" Inherits="TubitetBackEnd.UserTitleList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <ext:resourcemanager runat="server"></ext:resourcemanager>

    <form id="form1" runat="server">
   
        <ext:GridPanel runat="server" Title="Ünvan Listesi" ID="grdList">
            <TopBar>
                <ext:Toolbar runat="server">
                     <Items>
                         <ext:Button ID="btnNewUserTitle" runat="server" Text="Yeni Ünvan Kartı" Margin="10" Icon="Add" OnDirectClick="btnNewUserTitle_DirectClick"></ext:Button>
                         <ext:TextField ID="txtFilter" runat="server" FieldLabel="Filter"></ext:TextField>
                         <ext:Button ID="btnFind" runat="server" Icon="Find" Text="Listele" Margin="10" OnDirectClick="btnFind_DirectClick">
                             <DirectEvents>
                                 <Click Timeout="50000">
                                     <EventMask Msg="Lütfen Bekleyiniz..." ShowMask="true"></EventMask>
                                 </Click>
                             </DirectEvents>
                         </ext:Button>     
                     </Items>
                </ext:Toolbar>
            </TopBar>
            <Store>
                <ext:Store runat="server" ItemID="ID">
                    <Model>
                        <ext:Model runat="server">
                            <Fields>
                                <ext:ModelField Name="ID" Type="Int"></ext:ModelField>
                                <ext:ModelField Name="UserTitleName" Type="String"></ext:ModelField>
                                <ext:ModelField Name="IsDeleted" Type="Boolean"></ext:ModelField>
                            </Fields>
                        </ext:Model>
                    </Model>
                </ext:Store>
            </Store>

            <ColumnModel>
                <Columns>
                    <ext:RowNumbererColumn runat="server" Text="Sıra No" Width="80"></ext:RowNumbererColumn>
                    <ext:Column runat="server" Text="Ünvan Adı" DataIndex="UserTitleName" Flex="1"></ext:Column>
                    <ext:CommandColumn runat="server" ID="grdCommand" Width="160">
                        <Commands>
                            <ext:GridCommand Icon="ApplicationEdit" CommandName="cmdUpdate" Text="Guncelle"></ext:GridCommand>
                            <ext:GridCommand CommandName="cmdDelete" Text="SİL" Icon="Delete"></ext:GridCommand>
                        </Commands>
                        <DirectEvents>
                            <Command OnEvent="ColumnEvents" Timeout="5000">
                                <ExtraParams>
                                    <ext:Parameter Mode="Raw" Name="CommandName" Value="command"></ext:Parameter>
                                    <ext:Parameter Mode="Raw" Name="ID" Value="record.data.ID"></ext:Parameter>
                                </ExtraParams>
                            </Command>
                        </DirectEvents>
                    </ext:CommandColumn>
                </Columns>
            </ColumnModel>
        </ext:GridPanel>


        <ext:Window runat="server" Title="Ünvan Kartı" ID="wndNew" Width="350" Height="150" Modal="true" Hidden="true">
            <Items>
                <ext:Hidden ID="hdnID" runat="server"></ext:Hidden>
                <ext:TextField runat="server" ID="txtNewUserTitle" FieldLabel="Ünvan Adı" Margin="10"></ext:TextField>
            </Items>
            <Buttons>
                <ext:Button runat="server" ID="btnSave" Text="Kaydet" Icon="TableSave" OnDirectClick="btnSave_DirectClick"></ext:Button>
                <ext:Button runat="server" ID="btnClose" Text="Vazgeç" Icon="Cancel" OnDirectClick="btnClose_DirectClick"></ext:Button>
            </Buttons>
        </ext:Window>

    </form>
</body>
</html>
