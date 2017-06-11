using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibrary.Infrastructure
{
    class DAL
    {

        public static int insertSql(string tsql, List<MySqlParameter> prms)
        {
            int returnValue = 0;
            MySqlConnection conn = new MySqlConnection();
            var connectionString = ConfigurationManager.ConnectionStrings["sqlCnn"].ConnectionString;
            conn.ConnectionString = connectionString;
            conn.Open();
            MySqlTransaction transaction;

            transaction = conn.BeginTransaction();
            try
            {
                MySqlCommand cmd = new MySqlCommand(tsql, conn, transaction);

                foreach (MySqlParameter prm in prms)
                    cmd.Parameters.Add(prm);

                cmd.ExecuteNonQuery();

                returnValue = Convert.ToInt32(new MySqlCommand("select @@IDENTITY", conn, transaction)
                   .ExecuteScalar());
                
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                returnValue = -1;
                
            }

            conn.Close();
            return returnValue;
        }

        public static int insertSql(string tsql, MySqlParameter prms)
        {
            int returnValue = 0;
            MySqlConnection conn = new MySqlConnection();
            var connectionString = ConfigurationManager.ConnectionStrings["sqlCnn"].ConnectionString;
            conn.ConnectionString = connectionString;
            conn.Open();
            MySqlTransaction transaction;

            transaction = conn.BeginTransaction();
            try
            {
                MySqlCommand cmd = new MySqlCommand(tsql, conn, transaction);
                cmd.Parameters.Add(prms);

                cmd.ExecuteNonQuery();

                returnValue = Convert.ToInt32(new MySqlCommand("select @@IDENTITY", conn, transaction)
                   .ExecuteScalar());

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                returnValue = -1;
            }

            conn.Close();
            return returnValue;
        }

        public static DataTable readData(string tsql, List<MySqlParameter> prms)
        {
            MySqlConnection conn = new MySqlConnection();
            var connectionString = ConfigurationManager.ConnectionStrings["sqlCnn"].ConnectionString;
            conn.ConnectionString = connectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand(tsql, conn);

            foreach (MySqlParameter prm in prms)
                cmd.Parameters.Add(prm);

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;

        }

        public static DataTable readData(string tsql, MySqlParameter prms)
        {
            MySqlConnection conn = new MySqlConnection();
            var connectionString = ConfigurationManager.ConnectionStrings["sqlCnn"].ConnectionString;
            conn.ConnectionString = connectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand(tsql, conn);
            cmd.Parameters.Add(prms);

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;

        }

        public static DataTable readData(string tsql)
        {
            MySqlConnection conn = new MySqlConnection();
            var connectionString = ConfigurationManager.ConnectionStrings["sqlCnn"].ConnectionString;
            conn.ConnectionString = connectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand(tsql, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;

        }

    }
}
