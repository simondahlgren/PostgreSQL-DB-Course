using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservationApp.Model
{
    public class Observation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Observer_Id { get; set; }
        public int Geolocation_Id { get; set; }
       
        public override string ToString()

        {
            return $"ID: {Id} Datum: {Date}";
        }

    }
}
