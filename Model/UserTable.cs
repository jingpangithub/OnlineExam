using System;

namespace OnlineExam.Model
{
    /// <summary>
	/// User:实体类
	/// </summary>
	[Serializable]
    public partial class UserTable
    {
        public UserTable()
        { }

        #region Model
        private int id;
        private string usertype;
        private string username;
        private string password;
        
        public int ID
        {
            set { id = value; }
            get { return id; }
        }
        
        public string UserType
        {
            set { usertype = value; }
            get { return usertype; }
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
        #endregion Model

    }
}
