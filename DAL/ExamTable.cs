﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

namespace OnlineExam.DAL
{
    /// <summary>
	/// 数据访问类:ExamTable
	/// </summary>
    public partial class ExamTable
    {
        public ExamTable()
        { }

        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ExamTable");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.ExamTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ExamTable(");
            strSql.Append("Teacher,Major,Start,Last,Filepath,Nowstate)");
            strSql.Append(" values (");
            strSql.Append("@Teacher,@Major,@Start,@Last,@Filepath,@Nowstate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Teacher", SqlDbType.VarChar,50),
                    new SqlParameter("@Major", SqlDbType.NVarChar,50),
                    new SqlParameter("@Start", SqlDbType.VarChar,50),
                    new SqlParameter("@Last", SqlDbType.Int,4),
                    new SqlParameter("@Filepath", SqlDbType.NVarChar,50),
                    new SqlParameter("@Nowstate", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Teacher;
            parameters[1].Value = model.Major;
            parameters[2].Value = model.Start;
            parameters[3].Value = model.Last;
            parameters[4].Value = model.Filepath;
            parameters[5].Value = model.Nowstate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.ExamTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ExamTable set ");
            strSql.Append("Teacher=@Teacher,");
            strSql.Append("Major=@Major,");
            strSql.Append("Start=@Start,");
            strSql.Append("Last=@Last,");
            strSql.Append("Filepath=@Filepath,");
            strSql.Append("Nowstate=@Nowstate");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@Teacher", SqlDbType.VarChar,50),
                    new SqlParameter("@Major", SqlDbType.NVarChar,50),
                    new SqlParameter("@Start", SqlDbType.VarChar,50),
                    new SqlParameter("@Last", SqlDbType.Int,4),
                    new SqlParameter("@Filepath", SqlDbType.NVarChar,50),
                    new SqlParameter("@Nowstate", SqlDbType.NVarChar,50),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Teacher;
            parameters[1].Value = model.Major;
            parameters[2].Value = model.Start;
            parameters[3].Value = model.Last;
            parameters[4].Value = model.Filepath;
            parameters[5].Value = model.Nowstate;
            parameters[6].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ExamTable ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ExamTable ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ExamTable GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Teacher,Major,Start,Last,Filepath,Nowstate from ExamTable ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            Model.ExamTable model = new Model.ExamTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ExamTable DataRowToModel(DataRow row)
        {
            Model.ExamTable model = new Model.ExamTable();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Teacher"] != null)
                {
                    model.Teacher = row["Teacher"].ToString();
                }
                if (row["Major"] != null)
                {
                    model.Major = row["Major"].ToString();
                }
                if (row["Start"] != null)
                {
                    model.Start = row["Start"].ToString();
                }
                if (row["Last"] != null && row["Last"].ToString() != "")
                {
                    model.Last = int.Parse(row["Last"].ToString());
                }
                if (row["Filepath"] != null)
                {
                    model.Filepath = row["Filepath"].ToString();
                }
                if (row["Nowstate"] != null)
                {
                    model.Nowstate = row["Nowstate"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Teacher,Major,Start,Last,Filepath,Nowstate ");
            strSql.Append(" FROM ExamTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,Teacher,Major,Start,Last,Filepath,Nowstate ");
            strSql.Append(" FROM ExamTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM ExamTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from ExamTable T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  BasicMethod
    }
}