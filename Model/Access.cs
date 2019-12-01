using System;

namespace OnlineExam.Model
{
    /// <summary>
	/// Access:实体类
	/// </summary>
	[Serializable]
    public partial class Access
    {
        public Access()
        { }
        #region Model
        private int id;
        private string tablename;
        private string tableadress;
        
        public int ID
        {
            set { id = value; }
            get { return id; }
        }
        
        public string TableName
        {
            set { tablename = value; }
            get { return tablename; }
        }
        
        public string TableAdress
        {
            set { tableadress = value; }
            get { return tableadress; }
        }
        #endregion Model

    }
}
