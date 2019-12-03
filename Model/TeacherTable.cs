using System;

namespace OnlineExam.Model
{
    /// <summary>
    /// TeacherTable:实体类
    /// </summary>
    [Serializable]
    public partial class TeacherTable
    {
        public TeacherTable()
        { }

        #region Model
        private int id;
        private string username;
        private string password;
        private string name;
        private string sex;

        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        public string Username
        {
            set { username = value; }
            get { return username; }
        }

        public string Password
        {
            set { password = value; }
            get { return password; }
        }

        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        public string Sex
        {
            set { sex = value; }
            get { return sex; }
        }
        #endregion Model

    }
}
