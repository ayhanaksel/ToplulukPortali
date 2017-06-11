<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webBackEnd.aspx.cs" Inherits="TubitetBackEnd.webBackEnd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <ext:resourcemanager runat="server"></ext:resourcemanager>

    <form id="form1" runat="server">
            
         <ext:GridPanel runat ="server" Title="Web Yonetimi" ID="grdList">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button runat="server" ID="btnAbout" Text="About" Icon="TextAlignJustify" Margin="10" OnDirectClick="btnAbout_DirectClick">
                            <DirectEvents>
                                <Click Timeout="500000">
                                    <EventMask Msg="Lütfen Bekleyiniz..." ShowMask="true"></EventMask>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                         <ext:Button runat="server" ID="btnVision" Text="Vision" Icon="TextAlignJustify" Margin="10" OnDirectClick="btnVision_DirectClick">
                            <DirectEvents>
                                <Click Timeout="500000">
                                    <EventMask Msg="Lütfen Bekleyiniz..." ShowMask="true"></EventMask>
                                </Click>
                            </DirectEvents>
                        </ext:Button>   
                         <ext:Button runat="server" ID="btnMission" Text="Mission" Icon="TextAlignJustify" Margin="10" OnDirectClick="btnMission_DirectClick">
                            <DirectEvents>
                                <Click Timeout="500000">
                                    <EventMask Msg="Lütfen Bekleyiniz..." ShowMask="true"></EventMask>
                                </Click>
                            </DirectEvents>
                        </ext:Button>   
                         <ext:Button runat="server" ID="btnContact" Text="Contact" Icon="TextAlignJustify" Margin="10" OnDirectClick="btnContact_DirectClick">
                            <DirectEvents>
                                <Click Timeout="500000">
                                    <EventMask Msg="Lütfen Bekleyiniz..." ShowMask="true"></EventMask>
                                </Click>
                            </DirectEvents>
                        </ext:Button>      
                    </Items>
                </ext:Toolbar>
            </TopBar> 
        </ext:GridPanel>

        <ext:Window runat="server" ID="webSection" Modal="true" Hidden="true" width="500" Height="500">
        <Items>
            <ext:Hidden ID="hdnSection" runat="server"></ext:Hidden>
            <ext:TextArea runat="server" ID ="txtText" FieldLabel="Text" Width="450" Margin="10" Height="400"></ext:TextArea>
        </Items>
        <Buttons>
            <ext:Button runat="server" ID="btnChange" Text="Save" Icon="TableSave" OnDirectClick="btnChange_DirectClick"></ext:Button>
            <ext:Button runat="server" ID="btnClose" Text="Cancel" Icon="Cancel" OnDirectClick="btnClose_DirectClick"></ext:Button>
        </Buttons>
    </ext:Window>



    </form>
</body>
</html>
