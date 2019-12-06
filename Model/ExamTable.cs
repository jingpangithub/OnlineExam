using System;

namespace OnlineExam.Model
{
    /// <summary>
    /// ExamTable:实体类
    /// </summary>
    [Serializable]
    public partial class ExamTable
    {
        public ExamTable()
        { }

        #region Model
        private int id;
        private string teacher;
        private string major;
        private string start;
        private int last;
        private string filepath;
        private string nowstate;

        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        public string Teacher
        {
            set { teacher = value; }
            get { return teacher; }
        }

        public string Major
        {
            set { major = value; }
            get { return major; }
        }

        public string Start
        {
            set { start = value; }
            get { return start; }
        }

        public int Last
        {
            set { last = value; }
            get { return last; }
        }

        public string Filepath
        {
            set { filepath = value; }
            get { return filepath; }
        }

        public string Nowstate
        {
            set { nowstate = value; }
            get { return nowstate; }
        }
        #endregion Model

    }
}
