using System;

namespace OnlineExam.Model
{
    /// <summary>
    /// AnswerTable:实体类
    /// </summary>
    [Serializable]
    public partial class AnswerTable
    {
        public AnswerTable()
        { }

        #region Model
        private int id;
        private int exam;
        private string student;
        private string filepath;

        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        public int Exam
        {
            set { exam = value; }
            get { return exam; }
        }

        public string Student
        {
            set { student = value; }
            get { return student; }
        }                

        public string Filepath
        {
            set { filepath = value; }
            get { return filepath; }
        }
        #endregion Model

    }
}
