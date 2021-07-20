using System;
using System.Data;
using System.Data.SQLite;

namespace SQLApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection _con = new SQLiteConnection();
            string _strConnect = "Data Source=../../MyDatabase.sqlite;Version=3;";
            _con.ConnectionString = _strConnect;
            _con.Open();
            string sql = "CREATE TABLE IF NOT EXISTS tbl_students ([id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, fullname nvarchar(50), birthday varchar(15), email varchar(30), address nvarchar(100), phone varchar(11))";

            SQLiteCommand command = new SQLiteCommand(sql, _con);
            command.ExecuteNonQuery();

            string strInsert = string.Format("INSERT INTO tbl_students(fullname, birthday, email, address, phone) VALUES('{0}','{1}','{2}','{3}','{4}')", "Pham Hong Phuc", "04/12/1999", "phamhongphuc@gmail.com", "Hai Duong", "123456789");

            SQLiteCommand cmd = new SQLiteCommand(strInsert, _con);
            cmd.ExecuteNonQuery();

            DataSet ds = new DataSet();
            SQLiteDataAdapter da = new SQLiteDataAdapter("select id, fullname as [Full Name], email as [Email], address as [Address], phone as [Phone], birthday as [Birthday] from tbl_students", _con);

            da.Fill(ds);
            DataTable abc = ds.Tables[0];
            DataRowCollection ccc = abc.Rows;
            DataRow data = ccc[0];
            foreach(DataColumn column in abc.Columns)
            {
                Console.WriteLine(column.ColumnName);
            }
            //DataColumn data1 = abc.Columns[1];
            //Console.WriteLine(data1.ColumnName);
            //Console.WriteLine(data.Field<string>(1));
            _con.Close();
            Console.ReadKey();
        }
    }
}
