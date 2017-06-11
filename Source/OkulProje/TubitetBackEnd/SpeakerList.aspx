<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpeakerList.aspx.cs" Inherits="TubitetBackEnd.SpeakerList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <ext:ResourceManager runat="server"></ext:ResourceManager>

    <form id="form1" runat="server">

        <ext:GridPanel runat="server" Title="Konuşmacı Listesi" ID="grdList">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button runat="server" ID="btnNewSpeaker" Text="Yeni Konuşmacı" Icon="Add" OnDirectClick="btnNewSpeaker_DirectClick"></ext:Button>
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
                                <ext:ModelField Name="SpeakerName" Type="String"></ext:ModelField>
                                <ext:ModelField Name="SpeakerPhoto" Type="String"></ext:ModelField>
                                <ext:ModelField Name="SpeakerCV" Type="String"></ext:ModelField>
                                <ext:ModelField Name="SpeakerWorksFor" Type="String"></ext:ModelField>
                                <ext:ModelField Name="SpeakerSpeakAbout" Type="String"></ext:ModelField>
                                <ext:ModelField Name="IsDeleted" Type="Boolean"></ext:ModelField>
                            </Fields>
                        </ext:Model>
                    </Model>
                </ext:Store>
            </Store>

            <ColumnModel>
                <Columns>
                    <ext:RowNumbererColumn runat="server" Text="Sıra No" Width="80"></ext:RowNumbererColumn>
                    <ext:Column runat="server" Text="Konuşmacı Adı" DataIndex="SpeakerName" Flex="1"></ext:Column>
                    <ext:Column runat="server" Text="Konuşmacı Fotoğrafı" DataIndex="SpeakerPhoto" Flex="1"></ext:Column>
                    <ext:Column runat="server" Text="Konuşmacı CV" DataIndex="SpeakerCV" Flex="1"></ext:Column>
                    <ext:Column runat="server" Text="Konuşmacı Kurum" DataIndex="SpeakerWorksFor" Flex="1"></ext:Column>
                    <ext:Column runat="server" Text="Uzmanlık Alanı" DataIndex="SpeakerSpeakAbout" Flex="1"></ext:Column>
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
        <ext:Window runat="server" ID="wndNew" Resizable="false" Title="Konuşmacı Kartı" Modal="true" Hidden="true" Width="370" Height="500">
            <Items>
                <ext:Hidden ID="hdnID" runat="server"></ext:Hidden>
                <ext:TextField runat="server" ID="txtSpeakerName" FieldLabel="Konuşmacı Adı" Width="325" Margin="10" />
                <ext:TextField runat="server" ID="txtSpeakerWorksFor" FieldLabel="Konuşmacı Firması" Width="325" Margin="10" />
                <ext:FileUploadField ID="attachPhoto" runat="server" FieldLabel="Fotoğraf Ekle" Width="325" Icon="Attach" Margin="10" />
                <ext:Button runat="server" ID="btnKaydet" Text="Foto Kaydet" Icon="TableSave" Margin="10" OnDirectClick="btnPhotoSave_DirectClick" />
                <ext:Image ID="Image1" runat="server" Width="200" Height="200" Margin="10"></ext:Image>
                <ext:FileUploadField ID="attachCV" runat="server" FieldLabel="CV Ekle" Width="325" Icon="Attach" Margin="10" />
                <ext:ComboBox
                    runat="server"
                    Width="325"
                    ID="cmbxInterest"
                    Editable="false"
                    FieldLabel="İlgi Alanı"
                    DisplayField="InterestName"
                    ValueField="ID"
                    QueryMode="Local"
                    Margin="10"
                    TriggerAction="All"
                    EmptyText="İlgi alanı seçiniz">
                    <Store>
                        <ext:Store ID="store" runat="server">
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ID" />
                                        <ext:ModelField Name="InterestName" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ListConfig>
                        <ItemTpl runat="server">
                            <Html>
                                <div class="list-item">
                                {InterestName}
                        </div>
                            </Html>
                        </ItemTpl>
                    </ListConfig>
                </ext:ComboBox>
            </Items>
            <Buttons>
                <ext:Button runat="server" ID="btnSave" Text="Kaydet" Icon="TableSave" OnDirectClick="btnSave_DirectClick" />
                <ext:Button runat="server" ID="btnClose" Text="Vazgeç" Icon="Cancel" OnDirectClick="btnClose_DirectClick" />
            </Buttons>
        </ext:Window>

    </form>
</body>
</html>