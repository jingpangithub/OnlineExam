using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineExam.Models
{
    public class LoginModel
    {
        [Display(Name ="用户名")]
        [Required(ErrorMessage ="不能为空")]
        public string userName { get; set; }

        [Display(Name ="密码")]
        [Required(ErrorMessage ="不能为空")]
        [DataType(DataType.Password)]
        public string passWord { get; set; }   

        public string Login()
        {
            string result = null;

            using (var t = new AdminModel())
            {
                var q = from w in t.admin
                        where w.aname == userName && w.apassword == passWord
                        select w;
                var r = q.ToArray();
                if (r.Count() != 0)
                    result = "admin";

            }
            using (var t = new StudentModel())
            {
                var q = from w in t.student
                        where w.sid == userName && w.spassword == passWord
                        select w;
                var r = q.ToArray();
                if (r.Count() != 0)
                    result = "student";
            }
            using (var t = new TeacherModel())
            {
                var q = from w in t.teacher
                        where w.tname == userName && w.tpassword == passWord
                        select w;
                var r = q.ToArray();
                if (r.Count() != 0)
                    result = "teacher";
            }
            return result;
        }
    }
}