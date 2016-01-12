using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//add this
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    //decare the grants and services class so it is available
    //to all methods
    GrantsAndServices gs = new GrantsAndServices();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if it is not a postback--ie. if it is not
        //a repost due to an action on the page
        //call the FillDropDownList Method
        if(!IsPostBack)
        FillDropDownList();
    }
    protected void ServiceDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //when the index of the selected item in the
        //drop down box changes call the FillGridViewMethod
        FillGridView();
    }

    protected void FillDropDownList()
    {
        //Declare the dataTabe
        DataTable table = null; ;
        try
        {
            //call the method in the class to fill the table
             table= gs.GetServices();
        }
        catch(Exception ex)
        {
            ErrorLabel.Text=ex.Message;
        }
        //Attach the table as a datasource for the 
        //drop down list
        //assign the display and value fields
        ServiceDropDownList.DataSource = table;
        ServiceDropDownList.DataTextField = "ServiceName";
        ServiceDropDownList.DataValueField = "ServiceKey";
        ServiceDropDownList.DataBind();
    }

    protected void FillGridView()
    {
        //Get the value from the drop down list
        //selected value
        int serviceKey = int.Parse(ServiceDropDownList.SelectedValue.ToString());
        DataTable tbl = null;
        try
        {
            //call the GetServiceGrants field and pass 
            //it the service key
            tbl = gs.GetServiceGrants(serviceKey);

        }
        catch (Exception ex)
        {
            ErrorLabel.Text = ex.Message;
        }

        //attach the table as a data source to the
        //gridView
        GrantsGridView.DataSource = tbl;
        GrantsGridView.DataBind();
    }
}