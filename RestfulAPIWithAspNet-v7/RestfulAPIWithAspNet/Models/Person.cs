using System;

namespace RestfulAPIWithAspNet.Models
{
    public class Person
    {
        public int IdPerson { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Cpf { get; set; }
        public DateTime BirthDayDate { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public String Permission { get; set; }
    }
}