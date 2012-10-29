using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;

namespace Backend.Database
{
    public class SQLiteExamples
    {
        /// <summary>
        /// Creates a SQLite database. Assumes that the database does
        /// not exist already.
        /// </summary>
        /// <param name="pathAndFileName">The path and filename of the 
        /// SQLite database to create.</param>
        /// <returns>A SQLiteConnection object representing
        /// a connection to a SQLite database.</returns>
        public SQLiteConnection CreateDataBase(string pathAndFileName)
        {
            //The database should not already exist.
            if (System.IO.File.Exists(pathAndFileName))
            {
                throw new Exception("Database already exists!");
            }

            //This is the connection object. It basically represents the database.
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathAndFileName);

            //by opening and closing a connection to a file that does not exist, 
            //we create the file.
            conn.Open();
            conn.Close();

            return conn;
        }

        /// <summary>
        /// Returns a connection to an existing database.
        /// </summary>
        /// <param name="pathAndFileName"></param>
        /// <returns></returns>
        public SQLiteConnection ConnectToExistingDatabase(string pathAndFileName)
        {
            //The database should already exist.
            if (!System.IO.File.Exists(pathAndFileName))
            {
                throw new Exception("Database does not exist!");
            }

            //This is the connection object. It basically represents the database.
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathAndFileName);
            return conn;
        }

        /// <summary>
        /// Creates an example table containing 'DVD' records.
        /// </summary>
        /// <param name="conn">A SQLiteConnection object.</param>
        public void CreateDVDTable(SQLiteConnection conn)
        {
            //This sql query creates a dvds table with an auto incrementing id field as the primary key and some other columns.
            string sql = "CREATE TABLE dvds (id INTEGER PRIMARY KEY ASC, title TEXT, genre TEXT, date_added DATETIME)";

            //SQLiteCommand objects are where we exectute sql statements.
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                //if anything is wrong with the sql statement or the database,
                //a SQLiteException will show information about it.
                Debug.Print(ex.Message);

                //always make sure the database connectio is closed.
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public void InsertDVDRecord(string dvdTitle, string dvdGenre, SQLiteConnection conn)
        {
            string sql = "INSERT INTO dvds (title, genre, date_added) VALUES (@pTitle, @pGenre, @pDateAdded)";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            //We know the title and the genre, so let's get the date for the date_added field.
            //SQLite likes dates and times in a certain format (ISO-something or other).
            //This is given as the parameter to ToString()
            string currentTimeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            cmd.Parameters.Add(new SQLiteParameter("@pTitle", dvdTitle));
            cmd.Parameters.Add(new SQLiteParameter("@pGenre", dvdGenre));
            cmd.Parameters.Add(new SQLiteParameter("@pDateAdded", currentTimeString));

            //open the connection, exectute the query, and close the connection.
            //should probably be a try/catch block here
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// Returns the genre of a DVD. SQL parameters are showcased here.
        /// These are very important for security and performance.
        /// ExecuteScalar() is also shown, which is used to only
        /// return a single value.
        /// </summary>
        /// <param name="dvdName">The name of the DVD.</param>
        /// <param name="conn">A SQLiteConnection object.</param>
        public string GetDVDGenre(string dvdName, SQLiteConnection conn)
        {
            //let's create a sql statement.

            //the WRONG way:
            string badSQL = "SELECT genre FROM dvds WHERE title = '" + dvdName + "'";

            //the RIGHT way:
            //@pDVDName is a placeholder for the actual value.
            //Note that we don't need to use single quotes for string data types around the placeholder
            //like above.
            string sql = "SELECT genre FROM dvds WHERE title = @pDVDName";

            //The SQLiteCommand handles executing sql statesments against the database.
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            //create a parameter for dvdName
            SQLiteParameter pDVDName = new SQLiteParameter();

            //The ParameterName property must match the placeholder used in the sql variable above.
            pDVDName.ParameterName = "@pDVDName";

            //assign the dvdName variable to the parameter's Value property.
            pDVDName.Value = dvdName;

            //Note that we could alternatively do all of this in the constructor:
            //pDVDName = new SQLiteParameter("@pDVDName", dvdName);

            //now let's add the parameter to the SQLiteCommand's parameter collection:
            cmd.Parameters.Add(pDVDName);

            //now we execute the sql statement
            //we are assuming that there is only one record to return,
            //so we use ExecuteScalar().
            conn.Open();
            string genre = cmd.ExecuteScalar().ToString();
            conn.Close();

            return genre;
        }

        /// <summary>
        /// Retunrs a list of DVD titles. We are showing the use
        /// of a DataReader object here.
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public List<string> GetDVDTitles(SQLiteConnection conn)
        {
            List<string> dvdTitles = new List<string>();

            string sql = "SELECT title FROM dvds";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);


            //open the connection
            conn.Open();

            //we are 'using' a data reader here. Once
            //it leaves the 'using' scope, memory for the 
            //data reader is released. Useful when returning
            //very large datasets and you don't want to depend
            //on the garbage collector.
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                //'reader' iterates through returned
                //records from a sql query. Each column
                //in a record can be accessed via a 0-based index.
                //Because we only returned the 'title' column in
                //our sql query, its column index will be 0.
                //We get the dvd title for the current record
                //by using GetString() with the index as the 
                //parameter.
                string dvdTitle = reader.GetString(0);

                //add it to our list
                dvdTitles.Add(dvdTitle);
            }

            //close the connection
            conn.Close();

            return dvdTitles;
        }

        /// <summary>
        /// A method to test if a table exists in a sqlite database.
        /// </summary>
        /// <param name="tableName">The name of the table to look for.</param>
        /// <param name="conn">A SQLiteConnection object.</param>
        /// <returns>True if the table exists, false otherwise.</returns>
        public bool TableExists(string tableName, SQLiteConnection conn)
        {
            SQLiteCommand cmd = new SQLiteCommand(conn);

            //'sqlite_master' exists in all sqlite databases and holds metadata about the database.
            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='" + tableName + "'";
            conn.Open();
            SQLiteDataReader rdr = cmd.ExecuteReader();
            conn.Close();
            if (rdr.HasRows)
                return true;
            return false;
        }

    }
}
