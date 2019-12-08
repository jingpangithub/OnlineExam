using System;

namespace OnlineExam.Model
{
    /// <summary>
    /// IPTable:实体类
    /// </summary>
    [Serializable]
    public partial class IPTable
    {
        public IPTable()
        { }

        #region Model
        private int id;
        private string username;
        private string ip;

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

        public string IP
        {
            set { ip = value; }
            get { return ip; }
        }
        #endregion Model

    }
}
