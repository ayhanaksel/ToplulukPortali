<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TubitetBackEnd.Index" %>

<%@ Import Namespace="System.Collections.Generic" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["kullanici"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            string adsoyad = "Kullanıcı : " + Request.Cookies["kullanici"]["ad"] +" "+ Request.Cookies["kullanici"]["soyad"];
            lbAdSoyad.Text = adsoyad;
        }

        SiteMapNode siteNode = SiteMap.RootNode;
        Node root = this.CreateNode(siteNode);

        //dynamic tree root
        TreePanel2.Root.Add(this.CreateNodeWithOutChildren(siteNode));
    }

    //page tree node loader handler
    protected void LoadPages(object sender, NodeLoadEventArgs e)
    {

        if (!string.IsNullOrEmpty(e.NodeID))
        {
            SiteMapNode siteMapNode = SiteMap.Provider.FindSiteMapNodeFromKey(e.NodeID);

            SiteMapNodeCollection children = siteMapNode.ChildNodes;

            if (children != null && children.Count > 0)
            {
                foreach (SiteMapNode mapNode in siteMapNode.ChildNodes)
                {
                    e.Nodes.Add(this.CreateNodeWithOutChildren(mapNode));
                }
            }
        }

    }

    //dynamic node creation
    private Node CreateNodeWithOutChildren(SiteMapNode siteMapNode)
    {
        Node treeNode;

        if (siteMapNode.ChildNodes != null && siteMapNode.ChildNodes.Count > 0)
        {
            treeNode = new Node();
        }
        else
        {
            treeNode = new Node();
            treeNode.Leaf = true;
        }

        if (!string.IsNullOrEmpty(siteMapNode.Url))
        {

            treeNode.CustomAttributes.Add(new ConfigItem("url", this.Page.ResolveUrl(siteMapNode.Url)));
            //treeNode.Href = this.Page.ResolveUrl(siteMapNode.Url);
        }

        treeNode.NodeID = siteMapNode.Key;
        treeNode.Text = siteMapNode.Title;
        treeNode.Qtip = siteMapNode.Description;


        return treeNode;
    }

    //static node creation with children
    private Node CreateNode(SiteMapNode siteMapNode)
    {
        Node treeNode = new Node();


        if (!string.IsNullOrEmpty(siteMapNode.Url))
        {
            treeNode.CustomAttributes.Add(new ConfigItem("url", this.Page.ResolveUrl(siteMapNode.Url)));
            //treeNode.Href = "#";
        }

        treeNode.NodeID = siteMapNode.Key;
        treeNode.CustomAttributes.Add(new ConfigItem("hash", siteMapNode.Key.GetHashCode().ToString()));
        treeNode.Text = siteMapNode.Title;
        treeNode.Qtip = siteMapNode.Description;

        SiteMapNodeCollection children = siteMapNode.ChildNodes;

        if (children != null && children.Count > 0)
        {
            foreach (SiteMapNode mapNode in siteMapNode.ChildNodes)
            {
                treeNode.Children.Add(this.CreateNode(mapNode));
            }
        }
        else
        {
            treeNode.Leaf = true;
        }

        return treeNode;
    }
</script>


<!DOCTYPE html>

<html>
<head runat="server">
    <title>TUBİTET</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />

    <style type="text/css">
        .banner .x-panel-ıtems {
            background-color:blue;
            color:aqua;
        }
    </style>

    <script>
        var loadPage = function (tabPanel, record) {
            var tab = tabPanel.getComponent("node" + record.data.hash);

            if (!tab) {
                tab = tabPanel.add({
                    id: "node" + record.data.id,
                    title    : record.data.text,
                    closable : true,
                    loader : {
                        url      : record.data.id,
                        renderer     : "frame",
                        loadMask : {
                            showMask : true,
                            msg  : "Loading " + record.data.url + "..."
                        }
                    },
                    autoScroll : true
                });
            }

            tabPanel.setActiveTab(tab);
        };
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <ext:Viewport runat="server" Layout="BorderLayout">
            <Items>       

                        <ext:Panel runat="server" Region="North" Layout="VBoxLayout" Cls="banner">
                            <Items>
                                <ext:ImageButton runat="server" ImageUrl="/Content/logo.png" Width ="150" Height="150" Region="North"/>
                                <ext:Label runat="server" ID ="lbAdSoyad" Text="ad ve soyad" Margin="5"/>             
                                <ext:Button runat="server" ID ="btnCikis" Icon="Cancel" Text="Çıkış" Region="North" Width ="50" Height="25" Margin="5" OnDirectClick="btnCikis_DirectClick"/>                    
                            </Items>
                        </ext:Panel>
                        
                
                <ext:TreePanel
                    ID="TreePanel2"
                    runat="server"
                    Region="West"
                    RootVisible="false"
                    Width="300"
                    Title="Seçenekler"
                    Collapsible="false"
                    Icon="ChartOrganisation">

                    <Listeners>
                        <ItemClick Handler="loadPage(#{Pages}, record);" />
                    </Listeners>
                    <Store>
                        <ext:TreeStore runat="server" OnReadData="LoadPages">
                            <Proxy>
                                 <ext:PageProxy/>
                            </Proxy>
                        </ext:TreeStore>
                    </Store>
                    <ViewConfig LoadMask="false" />
                </ext:TreePanel>

                <ext:TabPanel
                    ID="Pages"
                    runat="server"
                    Region="Center"
                    Border="true"
                    Layout="AccordionLayout"
                    Title="TÜBİTET"/>

            </Items>
        </ext:Viewport>
    </form>
</body>
</html>

