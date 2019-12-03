using System;

namespace OnlineExam.Model
{
    /// <summary>
    /// StudentTable:实体类
    /// </summary>
    [Serializable]
    public partial class StudentTable
    {
        public StudentTable()
        { }

        #region Model
        private int id;
        private string username;
        private string password;
        private string name;
        private string sex;
        private string major;

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

        public string Major
        {
            set { major = value; }
            get { return major; }
        }
        #endregion Model

    }
}
