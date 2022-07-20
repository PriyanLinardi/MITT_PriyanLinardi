using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BackendTest.Services
{
    public class ExecuteDb
    {
        public string queryText { get; set; }
        public SqlParameter[] param { get; set; }
        private const string connection = @"Data Source=LAPTOP-1B7OL74V;Initial Catalog=MandiriTest;Integrated Security=True";

        public ExecuteDb(string queryText)
        {
            this.queryText = queryText;
        }

        public ExecuteDb(string queryText, SqlParameter[] param) : this(queryText)
        {
            this.param = param;
        }

        public object ExecuteScalar()
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            SqlCommand cmd = new SqlCommand(queryText, con);
            cmd.Parameters.AddRange(param);

            var result = cmd.ExecuteScalar();
            con.Close();

            return result;
        }

        public void ExecuteInsertUpdateDelete()
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            SqlCommand cmd = new SqlCommand(queryText, con);
            cmd.Parameters.AddRange(param);

            var result = cmd.ExecuteNonQuery();
            con.Close();
        }

        public List<T> GetList<T>()
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand(queryText, con);
            var dataReader = cmd.ExecuteReader();
           
            List<T> list = new List<T>();
            while (dataReader.Read())
            {
                var type = typeof(T);
                T obj = (T)Activator.CreateInstance(type);
                foreach (var prop in type.GetProperties())
                {
                    var propType = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(dataReader[prop.Name].ToString(), propType));
                }
                list.Add(obj);
            }

            con.Close();
            return list;
        }
    }
}