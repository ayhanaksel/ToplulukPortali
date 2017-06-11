<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SaloonList.aspx.cs" Inherits="TubitetBackEnd.SaloonList" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <ext:resourcemanager runat="server"></ext:resourcemanager>

    <form id="form1" runat="server">

        <ext:GridPanel runat ="server" Title="Salon Listesi" ID="grdList">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button runat="server" ID="btnNewSaloon" Text="Yeni Salon" icon="Add" OnDirectClick="btnNewSaloon_DirectClick"></ext:Button>
                        <ext:TextField runat="server" ID="txtFilter" FieldLabel="Filter"></ext:TextField>   
                        <ext:Button runat="server" ID="btnList" Text="Listele" Icon="Find" Margin="10" OnDirectClick="btnList_DirectClick">
                            <DirectEvents>
                                <Click Timeout="500000">
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
                                <ext:ModelField Name="SaloonName" Type="String"></ext:ModelField>
                                <ext:ModelField Name="SaloonQuata" Type="Int"></ext:ModelField>
                                <ext:ModelField Name="SaloonAddress" Type="String"></ext:ModelField>
                                <ext:ModelField Name="IsDeleted" Type="Boolean"></ext:ModelField>
                            </Fields>
                        </ext:Model>
                    </Model>
                </ext:Store>
            </Store>

            <ColumnModel>
                <Columns>
                    <ext:RowNumbererColumn runat="server" Text="Sıra No" Width="80"></ext:RowNumbererColumn>
                    <ext:Column runat="server" Text="Salon Adı" DataIndex="SaloonName" Flex="1"></ext:Column>
                    <ext:Column runat="server" Text="Salon Kapasitesi" DataIndex="SaloonQuata" Flex="1"></ext:Column>
                    <ext:Column runat="server" Text="Salon Adresi" DataIndex="SaloonAddress" Flex="1"></ext:Column>

                    <ext:CommandColumn runat="server" Width="160" ID="grdCommands">
                        <Commands>
                            <ext:GridCommand Icon="ApplicationEdit" Text="Güncelle" CommandName="cmdUpdate"></ext:GridCommand>
                            <ext:GridCommand Icon="Delete" Text="Sil" CommandName="cmdDelete"></ext:GridCommand>
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


    <ext:Window runat="server" ID="wndNew" Title="Salon Kartı" Modal="true" Hidden="true" width="350" Height="200">
        <Items>
            <ext:Hidden ID="hdnID" runat="server"></ext:Hidden>
            <ext:TextField runat="server" ID="txtSaloonName" FieldLabel="Salon Adı" width="325" Margin="10"></ext:TextField>
            <ext:TextField runat="server" ID="txtSaloonQuata" FieldLabel="Salon Kapasitesi" width="325" Margin="10"></ext:TextField>
            <ext:TextField runat="server" ID="txtSaloonAddress" FieldLabel="Salon Adresi" width="325" Margin="10"></ext:TextField>
        </Items>
        <Buttons>
            <ext:Button runat="server" ID="btnSave" Text="Kaydet" Icon="TableSave" OnDirectClick="btnSave_DirectClick"></ext:Button>
            <ext:Button runat="server" ID="btnClose" Text="Vazgeç" Icon="Cancel" OnDirectClick="btnClose_DirectClick"></ext:Button>
        </Buttons>
    </ext:Window>

    </form>
</body>
</html>
