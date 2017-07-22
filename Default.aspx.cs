/*
 * This code is written for the Abilis exercise using ASP.Net webforms. 
 * 1. Develop a web page that will plot to the screen the multiplication table up to 10X10. 
 * 2. Add a tooltip to each cell, to show the exercise that it is the result of (i.e. 32 will show 8X4=32 in the tool tip) 
 * 3. Add a URL parameter matrix_size (or GUI control) that selects the value (default =10) between 3 to 15 and refresh the page to show the relevant table. 
 * 4. Add a URL parameter matrix_base (or GUI control) which has three options (decimal, binary, hex), so the user can decide in which format he will see the table. Once selected the table is refreshed with the relevant values. 
 * 5. (Optional) – color the background of the Prime numbers.   
 **/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public enum TableBase
    {
        Decimal,
        Binary,        
        Hex
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterAsyncPostBackControl(matrixBaseDDL);

        if (!IsPostBack)
        {
            int matrixSize = 10;
            InitializeForm();
            DrawTable(matrixSize, TableBase.Decimal);
        }            
    }

    /*
     * This is the method that draws and fills in the multiplication table
     * input parameters:
     *       matrixSize: determines the size of the multiplication table
     *       tableBase: determines the base (Decimal, Binary, Hex)
     **/
    private void DrawTable(int matrixSize, TableBase tableBase)
    {
        int rowNum = 0;
        int colNum = 0;        

        MultiplicationTable.CellSpacing = 0;

        //Define the style for the cells
        TableItemStyle tableStyle = new TableItemStyle();
        tableStyle.HorizontalAlign = HorizontalAlign.Center;
        tableStyle.VerticalAlign = VerticalAlign.Middle;
        tableStyle.Width = Unit.Pixel(30);
        tableStyle.Height = Unit.Pixel(30);
        tableStyle.BorderStyle = BorderStyle.Solid;
        tableStyle.BorderWidth = Unit.Pixel(1);

        //Define the style for header cells
        TableItemStyle headerStyle = new TableItemStyle();
        headerStyle.HorizontalAlign = HorizontalAlign.Center;
        headerStyle.VerticalAlign = VerticalAlign.Middle;
        headerStyle.Width = Unit.Pixel(30);
        headerStyle.Height = Unit.Pixel(30);
        headerStyle.BackColor = Color.LightBlue;
        headerStyle.Font.Bold = true;       

        //add header row
        TableRow headerRow = new TableRow();        
        MultiplicationTable.Rows.Add(headerRow);
        for (colNum = 0; colNum <= matrixSize; colNum++)
        {
            TableCell headerCell = new TableCell();
            headerCell.ApplyStyle(headerStyle);


            if (colNum == 0)
            {
                headerCell.CssClass = "topLeftBorder";
                headerCell.Text = "X";
                headerCell.BackColor = Color.White;
                headerCell.Font.Bold = false;
            }
            else
            {                
                headerCell.CssClass = "topBorder";

                if (colNum == matrixSize)
                    headerCell.CssClass = "topRighttBorder";

                switch (tableBase)
                {
                    case TableBase.Hex:
                        // Convert the decimal value to a hexadecimal value in string form.                        
                        string hexOutput = Convert.ToString(colNum, 16);
                        headerCell.Text = hexOutput;
                        break;
                    case TableBase.Binary:
                        // Convert the decimal value to a binary value in string form.
                        string binaryOutput = Convert.ToString(colNum, 2);
                        headerCell.Text = binaryOutput;
                        break;
                    default:
                        headerCell.Text = colNum.ToString();
                        break;
                }
                    
            }    
            
            headerRow.Cells.Add(headerCell);
        }


        //fill the multiplication table
        for (rowNum = 1; rowNum <= matrixSize; rowNum++)
        {           
            //add new row to matrix
            TableRow newRow = new TableRow();
            MultiplicationTable.Rows.Add(newRow);
            for (colNum = 0; colNum <= matrixSize; colNum++)
            {                
                //add and fill the cells in the row
                TableCell newCell = new TableCell();

                if (colNum == 0)
                {
                    newCell.ApplyStyle(headerStyle);

                    if (rowNum == matrixSize)
                        newCell.CssClass = "bottomLeftBorder";
                    else
                        newCell.CssClass = "leftBorder";

                    switch (tableBase)
                    {
                        case TableBase.Hex:
                            // Convert the decimal value to a hexadecimal value in string form.                        
                            string hexOutput = Convert.ToString(rowNum, 16);
                            newCell.Text = hexOutput;
                            break;
                        case TableBase.Binary:
                            // Convert the decimal value to a binary value in string form.
                            string binaryOutput = Convert.ToString(rowNum, 2);
                            newCell.Text = binaryOutput;
                            break;
                        default:
                            newCell.Text = rowNum.ToString();
                            break;
                    }                    
                }
                else
                {
                    newCell.ApplyStyle(tableStyle);

                    if (rowNum == matrixSize && colNum == matrixSize)
                        newCell.CssClass = "bottomRightBorder";
                    else if (rowNum == matrixSize)
                        newCell.CssClass = "bottomBorder";
                    else if (colNum == matrixSize)
                        newCell.CssClass = "righttBorder";

                    int product = rowNum * colNum;

                    if (IsPrime(product))
                        newCell.BackColor = Color.Red;

                    switch (tableBase)
                    {
                        case TableBase.Hex:
                            // Convert the decimal value to a hexadecimal value in string form.                        
                            string hexOutput = Convert.ToString(product, 16);
                            newCell.Text = hexOutput;
                            break;
                        case TableBase.Binary:
                            // Convert the decimal value to a binary value in string form.
                            string binaryOutput = Convert.ToString(product, 2);
                            newCell.Text = binaryOutput;
                            break;
                        default:
                            newCell.Text = product.ToString();
                            break;
                    }                    
                
                    //add tooltip
                    newCell.ToolTip = rowNum + "X" + colNum + "=" + product;

                    if (rowNum == colNum)
                    {
                        newCell.BackColor = Color.LightBlue;
                        newCell.Font.Bold = true;
                    }
                }                

                newRow.Cells.Add(newCell);                
            }
        }
    }

    /*
     * This method initializes the GUI controls
     * */
    private void InitializeForm()
    {
        InitializeSizeDDL();
        InitializeBaseDDL();
    }

    /*
     * This method populates the multiplication table size dropdown list
     **/
    private void InitializeSizeDDL()
    {        
        IList<ListItem> sizeList = new List<ListItem>();

        for (int i = 3; i <= 15; i++)
        {
            ListItem item = new ListItem();
            item.Text = i.ToString();
            item.Value = i.ToString();
            sizeList.Add(item);
        }

        matrixSizeDDL.DataSource = sizeList;

        //set the default to 10        
        matrixSizeDDL.SelectedValue = sizeList.Where(r => r.Text == "10").FirstOrDefault().Value;

        matrixSizeDDL.DataBind();
    }

    /*
     * This method populates the multiplication table base dropdown list
     **/
    private void InitializeBaseDDL()
    {
        IList<ListItem> baseList = new List<ListItem>();

        foreach (TableBase b in Enum.GetValues(typeof(TableBase)))
        {
            ListItem item = new ListItem();
            item.Text = b.ToString();
            item.Value = b.ToString();
            baseList.Add(item);
        }                           

        matrixBaseDDL.DataSource = baseList;
        matrixBaseDDL.DataBind();
    }

    /*
     * This method determines whether the input number is a prime number or not
     **/
    private static bool IsPrime(int number)
    {
        if (number == 1)
            return false;

        if (number == 2)
            return true;

        bool isPrime = true;
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
            {
                isPrime = false;
                return isPrime;
            }
        }

        return isPrime;
    }

    protected void MatrixSizeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        int matrixSize = Convert.ToInt32(matrixSizeDDL.SelectedValue);

        string selectedBase = matrixBaseDDL.SelectedValue;
        TableBase currentBase = (TableBase)Enum.Parse(typeof(TableBase), selectedBase);

        DrawTable(matrixSize, currentBase);
    }

    protected void MatrixBaseDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        int matrixSize = Convert.ToInt32(matrixSizeDDL.SelectedValue);

        string selectedBase = matrixBaseDDL.SelectedValue;
        TableBase currentBase = (TableBase)Enum.Parse(typeof(TableBase), selectedBase);

        DrawTable(matrixSize, currentBase);
        TableUpdatePanel.Update();
    }
   
}