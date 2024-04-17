using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelPlannerAPP.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
