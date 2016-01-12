<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="ClassicADO.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Services</h1>
        <hr />
        <p>Select a service from the list to see the 
            Grant offered for that service.</p>

        <!--a couple of things are important in the drop down list:
            for one, the AutoPostBack property needs to be set
            to true. This makes it go back to the server everytime
            you make a new selection in the list.
            Also you need the OnSelectedIndexChanged method
            to run code when you change the selection
            -->

        <asp:DropDownList 
            ID="ServiceDropDownList" 
            runat="server" 
            AutoPostBack="true" 
            OnSelectedIndexChanged="ServiceDropDownList_SelectedIndexChanged"
            CssClass="list">
        </asp:DropDownList>

        <asp:GridView 
            ID="GrantsGridView" 
            runat="server">
        </asp:GridView>
        <!--to display any error messages thrown from the code-->
        <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
