using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelPlannerAPP.Model
{
    public class Trip
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float TripCost { get; set; }
        public string Program { get; set; }
       public string ImageUrl {  get; set; }
        public int UserId { get; set; }

        

        
    }
}
