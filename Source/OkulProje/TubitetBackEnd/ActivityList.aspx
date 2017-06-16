<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivityList.aspx.cs" Inherits="TubitetBackEnd.ActivityList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" />

        <ext:GridPanel runat ="server" Title="Etkinlik Listesi" ID="grdList">

            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button runat="server" ID="btnNewActivity" Text="Yeni Etkinlik" icon="Add" OnDirectClick="btnNewActivity_DirectClick" />
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
                                <ext:ModelField Name="ID" Type="Int" />
                                <ext:ModelField Name="ActivityName" Type="String" />
                                <ext:ModelField Name="ActivityType" Type="String" />
                                <ext:ModelField Name="ActivityDate" Type="String" />
                                <ext:ModelField Name="Saloon" Type="String" />
                                <ext:ModelField Name="GuessLimit" Type="Int" />
                                <ext:ModelField Name="ActivityPhoto" Type="String" />
                                <ext:ModelField Name="IsDeleted" Type="Boolean" />
                            </Fields>
                        </ext:Model>
                    </Model>
                </ext:Store>
            </Store>

            <ColumnModel>
                <Columns>
                    <ext:RowNumbererColumn runat="server" Text="Sıra No" Width="80"></ext:RowNumbererColumn>
                    <ext:Column runat="server" Text="Etkinlik Adı" DataIndex="ActivityName" Flex="1"></ext:Column>
                    <ext:Column runat="server" Text="Etkkinlik Türü" DataIndex="ActivityType" Flex="1"></ext:Column>
                    <ext:Column runat="server" Text="Etkinlik Tarihi" DataIndex="ActivityDate" Flex="1"></ext:Column>
                    <ext:Column runat="server" Text="Salon Adı" DataIndex="Saloon" Flex="1"></ext:Column>
                    <ext:Column runat="server" Text="Katılımcı Sayısı" DataIndex="GuessLimit" Flex="1"></ext:Column>
                    <ext:Column runat="server" Text="Etkinlik Afişi" DataIndex="ActivityPhoto" Flex="1"></ext:Column>
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


    <ext:Window runat="server" ID="wndNew" Title="Etkinlik Kartı" Modal="true" Hidden="true" width="365" Height="600">
        <Items>
            <ext:Hidden ID="hdnID" runat="server"></ext:Hidden>
            <ext:TextField runat="server" ID="txtActivityName" FieldLabel="Etkinlik Adı" width="325" Margin="10"></ext:TextField>
            <ext:ComboBox
                    runat="server"
                    Width="325"
                    ID="cmbxActivityType"
                    Editable="false"
                    FieldLabel="Etkinlik Türü"
                    DisplayField="TypeName"
                    ValueField="ID"
                    QueryMode="Local"
                    Margin="10"
                    TriggerAction="All"
                    EmptyText="Etkinlik Türünü seçiniz">
                <Store>
                        <ext:Store ID="store" runat="server">
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ID" />
                                        <ext:ModelField Name="TypeName" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ListConfig>
                        <ItemTpl runat="server">
                            <Html>
                                <div class="list-item">
                                {TypeName}
                            </Html>
                        </ItemTpl>
                    </ListConfig>
                </ext:ComboBox>
            <ext:DateField
                    ID="dtfActivityDate"
                    runat="server"
                    Vtype="daterange"
                    FieldLabel="Etkinlik Tarihi"
                    Margin="10">
                   <CustomConfig>
                        <ext:ConfigItem Name="endDateField" Value="dtfActivityDate" Mode="Value" />
                    </CustomConfig>
                </ext:DateField>
            <ext:ComboBox
                    runat="server"
                    Width="325"
                    ID="cmbxSaloon"
                    Editable="false"
                    FieldLabel="Salon Adı"
                    DisplayField="SaloonName"
                    ValueField="ID"
                    QueryMode="Local"
                    Margin="10"
                    TriggerAction="All"
                    EmptyText="Etkinlik Salonunu seçiniz">
                <Store>
                        <ext:Store ID="store1" runat="server">
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ID" />
                                        <ext:ModelField Name="SaloonName" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ListConfig>
                        <ItemTpl runat="server">
                            <Html>
                                <div class="list-item">
                                {SaloonName}
                            </Html>
                        </ItemTpl>
                    </ListConfig>
                </ext:ComboBox>
            <ext:TextField runat="server" ID="txtGuessLimit" FieldLabel="Katılımcı Sayısı" width="325" Margin="10"></ext:TextField>
            <ext:FileUploadField ID="attachPhoto" runat="server" FieldLabel="Afiş Ekle" Width="325" Icon="Attach" Margin="10" />
                <ext:Button runat="server" ID="btnKaydet" Text="Afiş Kaydet" Icon="TableSave" Margin="10" OnDirectClick="btnPhotoSave_DirectClick" />
                <ext:Image ID="Image1" runat="server" Width="300" Height="250" Margin="10"></ext:Image>
        </Items>
        <Buttons>
            <ext:Button runat="server" ID="btnSave" Text="Kaydet" Icon="TableSave" OnDirectClick="btnSave_DirectClick"></ext:Button>
            <ext:Button runat="server" ID="btnClose" Text="Vazgeç" Icon="Cancel" OnDirectClick="btnClose_DirectClick"></ext:Button>
        </Buttons>
    </ext:Window>
    
    </form>
</body>
</html>
