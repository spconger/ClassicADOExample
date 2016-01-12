using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//add these three libraries
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// This class has a constructor and three methods.
/// The constructor initializes the connection object
/// by calling the configuration manager to retreive
/// the connnection string from the web config file.
/// The connection string is safer there since that file
/// resides on the server and cannot be seen from the client
/// even in a debug mode.
/// The first method sets up the SQL String to retreive
/// the services and the serviceKeys. It initilizes the 
/// command object passing it the sql string and the connection
/// then passes the command object itself to the 
/// ProcessQuery method. It returns a datatable object.
/// The GetServices method does exactly the same with the added
/// feature that the sql string has a variable 
/// A parameter is added to the command object and
/// assigned the value of the ServiceKey that is passed
/// into the method. It also returns a dataTable object.
/// The Process Query Method executes the sql and returns
/// a datatable. It declares a new dataTable object and a
/// SQLDataReader object. It then Opens the connection.
/// This is the first place in all the code that a runtime
/// error can occur, because this is the first time the code is
/// actually executing--the rest is all in preparation.
/// If the code fails here it means the connection string is faulty.
/// The next act is to fill the reader by executing the 
/// cmd.ExecuteReader method. If the code fails here it means
/// the SQL string is faulty.
/// The results returned by the query are loaded into the 
/// DataTable and the connection is closed
/// The try catch is there to catch any errors. The throw throws
/// the error back to the calling method. In our case
/// the calling methods throw it again to the web form.
/// </summary>
public class GrantsAndServices
{
    //create connection object
    SqlConnection connect;
	public GrantsAndServices()
	{
        //initialize connection object
        //the connection string is in the web config
        //The config manager lets the code
        //access the web config file
        connect = new SqlConnection(ConfigurationManager.
            ConnectionStrings["CommunityAssistConnectionString"].ToString());
	}

    public DataTable GetServices()
    {
        //set up the sql string
        string sql = "Select ServiceKey, ServiceName from CommunityService";
        DataTable tbl;//declare the table
        //initialize the command passing it
        //the sql command and the connection
        SqlCommand cmd = new SqlCommand(sql, connect);
        try
        {
            //we call the ProcessQuery method
            //try catch in case there is an error
            tbl = ProcessQuery(cmd);
        }
        catch(Exception ex)
        {
            //throw it back to the form
            throw ex;
        }
        return tbl;
        
    }
    public DataTable GetServiceGrants(int serviceKey)
    {
        string sql = "Select GrantKey, GrantDate, "
        + "GrantNeedExplanation, GrantAllocation "
        + " from ServiceGrant "
        + "Where ServiceKey = @ServiceKey";
        SqlCommand cmd = new SqlCommand(sql, connect);
        //for this command we need also to add a parameter
        //to store the variable from the drop down list
        //All SQL variables start with an @ sign
        cmd.Parameters.Add("@ServiceKey", serviceKey);

        DataTable tbl;
        try
        {
            tbl = ProcessQuery(cmd);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return tbl;

    }

    private DataTable ProcessQuery(SqlCommand cmd)
    {
       //This method processes the queries
        //since the code would be identical in
        //both queries, I refractored it into
        //its own method. No need to repeat more
        //than is necessary
        DataTable table = new DataTable();
        SqlDataReader reader;
        try
        {
            connect.Open();
            reader = cmd.ExecuteReader();
            table.Load(reader);
            connect.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return table;
    }
}