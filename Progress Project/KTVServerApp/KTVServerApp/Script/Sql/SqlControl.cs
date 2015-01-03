using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace KTVServerApp.Script.Sql
{
    public static class SqlControl
    {
        public static SqlConnection InitializeConnection( string database, string user, string pass,string server=".")
        {
            try
            {
                SqlConnection sql = new SqlConnection("Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + user + ";Password=" + pass);
                return sql;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
        public static void TerminateConnection(SqlConnection con)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        public static bool StartConnection(SqlConnection con)
        {
            try
            {
                con.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static SqlDataReader SelectData(string sql, SqlConnection con)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                SqlDataReader reader = new SqlCommand(sql, con).ExecuteReader();
                //con.Close();
                return reader;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
        public static SqlDataReader SelectData(string tablename, SqlConnection con, string condition, params string[] columnnames)
        {
            try
            {
                string sql = "SELECT ";
                int i = 0;
                foreach (string name in columnnames)
                {
                    string sign = "";
                    if (i < columnnames.Length - 1)
                    {
                        sign = ",";
                    }
                    else
                    {
                        sign = "";
                    }
                    sql += name + sign;
                    i++;
                }
                sql += "FROM " + tablename;
                if (condition != null)
                {
                    sql += " WHERE " + condition;
                }
                return SelectData(sql, con);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
        public static SqlDataAdapter SelectData(SqlConnection con,string sql)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                return adapter;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
        
        public static bool InsertData(string sql, SqlConnection con)
        {
            try
            {
                ExecuteCommand(sql, con);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool InsertData(string tablename, SqlConnection con, Hashtable hash)
        {
            try
            {
                //if (con.State == ConnectionState.Closed) con.Open();
                string sql = "INSERT INTO " + tablename + "(";
                string column = "";
                string values = "";
                int i = 0;
                foreach (DictionaryEntry entry in hash)
                {
                   // MessageBox.Show(entry.Key + "");
                    string sign = "";
                    if (entry.Value != null)
                    {
                        if (entry.Key.GetType() == typeof(string))
                        {
                            
                            if (i < hash.Count - 1)
                            {
                                sign = ",";
                            }
                            else
                            {
                                sign = ")";
                            }
                            column += entry.Key + sign;

                            if (entry.Value.GetType() == typeof(string))
                            {
                                values += "N'" + entry.Value + "'" + sign;
                            }
                            else
                            {
                                //if (entry.Value.GetType() == typeof(byte[]))
                                //{
                                //    values +=BitConverter.ToInt64((byte[])entry.Value,0) + sign;
                                //}
                                //else
                                //{
                                values += entry.Value + sign;
                                //
                            }

                        }
                    }
                    else
                    {
                        if (i >= hash.Count - 1)
                        {
                            sign = ")";
                            column=column.Remove(column.Length-1)+ sign;
                            values=values.Remove(values.Length-1)+ sign;
                        }
                    }
                    i++;
                }
                sql += column + " VALUES(" + values;
               // MessageBox.Show(sql);
                ExecuteCommand(sql, con);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
        public static bool InsertData(string tablename,SqlConnection con,params CommandParameter[] param)
        {
            try
            {
                string col="";
                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand com = new SqlCommand();
                for (int i = 0; i < param.Length; i++)
                {
                    //if (param[i].values != null)
                    //{
                        if (i <= param.Length - 2)
                        {
                            col += param[i].paramname + ",";
                        }
                        else
                        {
                            col += param[i].paramname;
                        }
                        com.Parameters.Add(param[i].paramname, param[i].type).Value = param[i].values;
                    //}
                }
                string sql = "INSERT INTO " + tablename + " VALUES(" + col + ")";
                
               // MessageBox.Show(sql);
                com.CommandText = sql;
                com.Connection = con;
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
        public static bool InsertData(string tablename, SqlConnection con,string[] colname, params CommandParameter[] param)
        {
            try
            {
                string col = "";
                string headername = "";
                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand com = new SqlCommand();
                for (int i = 0; i < param.Length; i++)
                {
                    if (param[i].values != null)
                    {
                        if (i <= param.Length - 2)
                        {
                            col += param[i].paramname + ",";
                            headername += colname[i] + ",";
                        }
                        else
                        {
                            col += param[i].paramname;
                            headername += colname[i];
                        }
                        com.Parameters.Add(param[i].paramname, param[i].type).Value = param[i].values;
                    }
                    else
                    {
                        if (i >= param.Length - 1)
                        {
                            col = col.Remove(col.Length - 1);
                            headername = headername.Remove(headername.Length - 1);
                        }
                    }
                }
                string sql = "INSERT INTO " + tablename + "(" + headername + ") VALUES(" + col + ")";

               // MessageBox.Show(sql);
                com.CommandText = sql;
                com.Connection = con;
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
        
        public static bool UpdateData(string tablename, SqlConnection con, string[] columnname, string condition, params CommandParameter[] param)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                string col = "";
                SqlCommand com = new SqlCommand();
                for (int i = 0; i < param.Length; i++)
                {
                    if (param[i].values != null)
                    {
                        if (i <= param.Length - 2)
                        {
                            col += columnname[i] + "=" + param[i].paramname + ",";
                        }
                        else
                        {
                            col += columnname[i] + "=" + param[i].paramname;
                        }
                        com.Parameters.Add(param[i].paramname, param[i].type).Value = param[i].values;
                    }
                    else
                    {
                        if (i >= param.Length - 1)
                        {
                            col = col.Remove(col.Length - 1);
                        }
                    }
                }
                string sql = "UPDATE " + tablename + " SET " + col + " " + condition;
                //MessageBox.Show(sql);
                com.CommandText = sql;
                com.Connection = con;
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }
        public static bool UpdateData(string tablename, SqlConnection con, Hashtable hash, string condition)
        {
            try
            {
                string sql = "UPDATE " + tablename + " SET ";
                int i = 0;
                foreach (DictionaryEntry entry in hash)
                {
                    string sign = "";
                    
                        if (entry.Key.GetType() == typeof(string))
                        {
                            if (i < hash.Count - 1)
                            {
                                sign = ",";
                            }
                            else
                            {
                                sign = "";
                            }
                            if (entry.Value != null)
                            {
                                if (entry.Value.GetType() == typeof(string))
                                {
                                    sql += entry.Key + "=N'" + entry.Value + "'" + sign;
                                }
                                else
                                {
                                    sql += entry.Key + "=" + entry.Value + sign;
                                }
                            }
                            else
                            {
                                //if (i >= hash.Count - 1)
                                //{
                                //    sql = sql.Remove(sql.Length - 1);
                                //}
                                sql += entry.Key + "=Null" + sign;
                            }
                        }
                   
                    i++;
                }
                sql += " WHERE " + condition;
                MessageBox.Show(sql);
                ExecuteCommand(sql, con);
                //MessageBox.Show(sql);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
        public static bool UpdateData(string sql, SqlConnection con)
        {
            try
            {
                ExecuteCommand(sql, con);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool DeleteData(string tablename, SqlConnection con, string condition)
        {
            try
            {
                string sql = "DELETE FROM " + tablename + " WHERE " + condition;
                ExecuteCommand(sql, con);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool DeleteCommand(string sql, SqlConnection con)
        {
            try
            {
                ExecuteCommand(sql, con);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static Hashtable Hash(params object[] args)
        {
            try
            {
                Hashtable hash = new Hashtable(args.Length / 2);
                if (args.Length % 2 != 0)
                {
                    return null;
                }
                for (int i = 0; i < args.Length - 1; i = i + 2)
                {
                    hash.Add(args[i], args[i + 1]);
                }
                return hash;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private static void ExecuteCommand(string sql, SqlConnection con)
        {
            if (con.State == ConnectionState.Closed) con.Open();
            SqlCommand command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public struct CommandParameter
        {
            //public string colname;
            public string paramname;
            public SqlDbType type;
            public object values;

            public CommandParameter(string name, SqlDbType type, object value)
            {
                this.paramname = name;
                this.type = type;
                this.values = value;
            }
        }
    }
}
