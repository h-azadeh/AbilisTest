<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">        
        .topBorder { border-top: 1px blue solid; }
        .bottomBorder { border-bottom: 1px blue solid; }
        .leftBorder { border-left: 1px blue solid; }
        .righttBorder { border-right: 1px blue solid; }
        .topLeftBorder { border-top: 1px blue solid; border-left: 1px blue solid}
        .topRighttBorder { border-top: 1px blue solid; border-right: 1px blue solid}
        .bottomLeftBorder { border-bottom: 1px blue solid; border-left: 1px blue solid}
        .bottomRightBorder { border-bottom: 1px blue solid; border-right: 1px blue solid}
    </style>

    <title>Abilis Exercise</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true"></asp:ScriptManager>

        <div style="height:550px; width:550px">
        
        <asp:UpdatePanel ID="TableUpdatePanel" runat="server" UpdateMode="Conditional">                 
            <ContentTemplate>
                <asp:Table ID="MultiplicationTable" runat="server"></asp:Table>                                    
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="matrixBaseDDL" EventName="selectedindexchanged" />
            </Triggers> 
        </asp:UpdatePanel>
        </div>
        <div>&nbsp;</div>
        <div>
            <asp:Label ID="matrixSizeLbl" runat="server" Font-Bold="true">Multiplication Table Size:</asp:Label>
            &nbsp;
            <asp:DropDownList ID="matrixSizeDDL" runat="server" AutoPostBack="True" onselectedindexchanged="MatrixSizeDDL_SelectedIndexChanged"></asp:DropDownList>
            &nbsp;&nbsp;  
            <asp:Label ID="matrixBaseLbl" runat="server" Font-Bold="true">Base:</asp:Label>
            &nbsp;
            <asp:DropDownList ID="matrixBaseDDL" runat="server" AutoPostBack="True" onselectedindexchanged="MatrixBaseDDL_SelectedIndexChanged"></asp:DropDownList> 
        </div>
            
    </form>
</body>
   

</html>
