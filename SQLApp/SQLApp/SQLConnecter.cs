using System.Data.SQLite;

namespace SQLApp
{
    public class SQLConnecter
    {
        private static string _strConnect = "Data Source=../../MyDatabase.sqlite;Version=3;";
        private static SQLConnecter connecter;
        private static SQLiteConnection _con;

        private SQLConnecter()
        {
            _con = new SQLiteConnection();
            _con.ConnectionString = _strConnect;
        }

        public static SQLConnecter GetInstance()
        {
            if (connecter == null)
            {
                connecter = new SQLConnecter();
            }
            return connecter;
        }

        public void OpenConnect()
        {
            _con.Open();
        }

        public void CloseConnect()
        {
            _con.Close();
        }


    }
}
