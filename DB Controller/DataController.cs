using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace DB_Controller
{
    public class DataController
    {
        OleDbConnection connection;

        public void loadItems(List<Item> dataBase)
        {   
            connection = new OleDbConnection();
            connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ItemDB.accdb";
            connection.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            OleDbCommand command = new OleDbCommand();
            DataSet ds = new DataSet();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Table1";
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dataBase.Add(new Item(Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),Convert.ToString(ds.Tables[0].Rows[i]["ItemName"]),
                Convert.ToDouble(ds.Tables[0].Rows[i]["Price"]), Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"])));
            }
            connection.Close();
        }

        public void clearDB()
        {
            connection = new OleDbConnection();
            connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ItemDB.accdb";
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "DELETE * from Table1";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void saveToDB(List<Item> dataBase)
        {
            connection = new OleDbConnection();
            connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ItemDB.accdb";
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            foreach (Item item in dataBase)
            {
                command.CommandText = "INSERT into Table1 (ID,ItemName,Price,Quantity) values('" +
                    item.id.ToString() + "','" + item.name.ToString() + "','" +
                    item.price.ToString() + "','" + item.quantity.ToString() + "')";
                command.ExecuteNonQuery();
            }
            connection.Close ();
        }
    }
}
