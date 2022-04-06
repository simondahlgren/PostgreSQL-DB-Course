using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservationApp.Model
{
   public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Country_Id { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
