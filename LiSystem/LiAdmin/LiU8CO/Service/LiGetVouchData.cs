using LiU8CO.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO.Service
{
    public class LiGetVouchData
    {
        public LiGetVouchData()
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(U8Login.clsLogin u8Login)
        {
            this.u8Login = u8Login;
        }

        public int getU8VouchListCount(LiU8ApiGetDataModel liU8ApiGetData)
        {
            string strConn = this.u8Login.UFDataConnstringForNet;
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd.Connection = conn;
                cmd.CommandText = " exec sp_U8QueryList_Count @cardNumber,@bQueryType,@whereSql";
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add(new SqlParameter("@cardNumber", liU8ApiGetData.sCardNumber));
                cmd.Parameters.Add(new SqlParameter("@bQueryType", liU8ApiGetData.bQueryType));
                cmd.Parameters.Add(new SqlParameter("@whereSql", liU8ApiGetData.sWhereString));
                int iCount = (int)cmd.ExecuteScalar();
                return iCount;
            }
        }

        public DataTable getU8VouchList(LiU8ApiGetDataModel liU8ApiGetData)
        {
            string strConn = this.u8Login.UFDataConnstringForNet;
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                if(conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd.Connection = conn;
                cmd.CommandText = " exec sp_U8QueryList @cardNumber,@bQueryType,@fieldSql,@whereSql,@orderBySql,@rangeSql";
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add(new SqlParameter("@cardNumber", liU8ApiGetData.sCardNumber));
                cmd.Parameters.Add(new SqlParameter("@bQueryType", liU8ApiGetData.bQueryType));
                cmd.Parameters.Add(new SqlParameter("@fieldSql", liU8ApiGetData.sSelectFields));
                cmd.Parameters.Add(new SqlParameter("@whereSql", liU8ApiGetData.sWhereString));
                cmd.Parameters.Add(new SqlParameter("@orderBySql", liU8ApiGetData.sOrderByString));
                cmd.Parameters.Add(new SqlParameter("@rangeSql", string.Format(" AND iPageRow >= {0} AND iPageRow<={1}", liU8ApiGetData.iStart, liU8ApiGetData.iEnd)));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt;
            }
        }

        ~LiGetVouchData()
        {
        }
        ///// <summary>
        ///// 连接数据库
        ///// </summary>
        //public ADODB.Connection conn;
        //public ADODB.Recordset rs;
        //public OleDbDataAdapter oleDA;

        /// <summary>
        /// U8Loginn
        /// </summary>
        public U8Login.clsLogin u8Login;

        public string strSql;
        public string strConn;
    }
}
