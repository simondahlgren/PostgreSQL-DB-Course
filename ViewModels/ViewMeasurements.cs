using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservationApp.ViewModels
{
   public class ViewMeasurements
  
    {

        public List<ViewMeasurements> categorymeasurmentlist { get; set; } = new List<ViewMeasurements>();
        public string Category_name { get; set; }
        public double Value { get; set; }
        public string Unit_name { get; set; }

        public override string ToString()
        {
            return $"{Value} {Unit_name} {Category_name}";
        }

    }
}
