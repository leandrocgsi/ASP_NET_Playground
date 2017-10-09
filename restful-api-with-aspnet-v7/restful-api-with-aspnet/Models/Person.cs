using System;

namespace RestfulAPIWithAspNet.Models
{
    public class Person
    {
        public int idPerson { get; set; }
        public String name { get; set; }
        public String email { get; set; }
        public String phone { get; set; }
        public String cpf { get; set; }
        public DateTime? birthDayDate { get; set; }
        public String login { get; set; }
        public String password { get; set; }
        public String permission { get; set; }
    }
}