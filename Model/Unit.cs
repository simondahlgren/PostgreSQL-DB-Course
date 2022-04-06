using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservationApp.Model
{
   public class Unit

    {
        public int? Id { get; set; }
        public string Type { get; set; }
        public string Abbrevation { get; set; }
       
        public override string ToString()

        {
            return $"{Abbrevation}";
        }

    }
}
