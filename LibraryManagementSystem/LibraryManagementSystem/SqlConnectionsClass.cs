using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    class SqlCommondClass
    {
        #region 新建一个数据库连接
        public static void OpenConnection(SqlConnection connection, string conStr)
        {
            #region 新建数据库连接
            //string conStr = @"Data Source = SMILEBOY; Initial Catalog = Student; Integrated Security = true";
            connection = new SqlConnection(conStr);
            //SqlCommand cmd = connection.CreateCommand();
            #endregion

            #region 打开数据库
            connection.Open();
            #endregion

            #region  测试用
            //System.Console.WriteLine("数据库连接--OPEN（）前信息：");
            //System.Console.WriteLine(" ConnectionTimeout = [{0}]", connection.ConnectionTimeout);
            //System.Console.WriteLine(" Database = [{0}]", connection.Database);
            //System.Console.WriteLine(" DataSource = [{0}]", connection.DataSource);
            //System.Console.WriteLine(" PacketSize = [{0}]", connection.PacketSize);
            //System.Console.WriteLine(" StatisticsEnable = [{0}]", connection.StatisticsEnabled);
            //System.Console.WriteLine(" Workstation = [{0}]", connection.WorkstationId);
            //System.Console.WriteLine(" State = [{0}]", connection.State);
            

            //cmd.CommandText = "Select * from SC";
            //SqlDataReader reader = cmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    System.Console.Write("{0}\t", reader.GetString(0));
            //    System.Console.Write("{0}\t", reader.GetInt32(1));
            //    System.Console.Write("{0}\t", reader.GetInt32(2));
            //    System.Console.WriteLine();
            //}

            //打印打开连接后的SqlConnection的信息
            //System.Console.WriteLine(" 数据库连接--Open（）的信息：");
            //System.Console.WriteLine(" ServerVersion = [{0}]", connection.ServerVersion);
            //System.Console.WriteLine(" State = [{0}]", connection.State);


            //关闭数据库连接
            //connection.Close();

            //打印数据库关闭之后的信息
            //System.Console.WriteLine(" 数据库连接--close（）后的信息：");
            //System.Console.WriteLine(" State = [{0}]", connection.State);
            #endregion
        }
        #endregion

        #region 获取数据集
        public static DataSet GetDataSet(SqlCommand sqlCommand, string strTableName, SqlConnection sqlconnection)
        {
            sqlCommand.Connection = sqlconnection;

            try
            {
                if (sqlconnection.State == ConnectionState.Closed) sqlconnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet sqlDataSet = new DataSet();
                sqlDataAdapter.Fill(sqlDataSet, strTableName);
                return sqlDataSet;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sqlconnection.State == ConnectionState.Open) sqlconnection.Close();
            }
        }
        #endregion

        #region 获取数据集
        public static DataSet GetDataSet(string strCmd, string strTableName, SqlConnection sqlconnection)
        {
            SqlCommand sqlCommand = new SqlCommand(strCmd);
            return GetDataSet(sqlCommand, strTableName, sqlconnection);
        }
        #endregion

        #region 获取指令影响的行数
        public static int SqlExecuteNonQuery(SqlCommand sqlCommand, SqlConnection sqlconnection)
        {
            sqlCommand.Connection = sqlconnection;

            try
            {
                if (sqlconnection.State == ConnectionState.Closed) sqlconnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sqlconnection.State == ConnectionState.Open) sqlconnection.Close();
            }
        }
        #endregion

        #region 获取指令影响的行数
        public static int SqlExecuteNonQuery(string strCmd, SqlConnection sqlconnection)
        {
            SqlCommand sqlCommand = new SqlCommand(strCmd);
            return SqlExecuteNonQuery(sqlCommand, sqlconnection);
        }
        #endregion


    }
}
