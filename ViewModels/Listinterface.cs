using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservationApp.ViewModels
{
   public class Listinterface
  

        
    {
       /// <summary>
       /// Create list to display information in the interface
       /// </summary>
        public List<Listinterface> Listtemporary { get; set; } = new List<Listinterface>();
        /// <summary>
        /// Adds properties to store information from multible tables.
        /// </summary>
        public int Measurement_id { get; set; }
        public string Category_name { get; set; }
        public double Value { get; set; }

        public string Unit_name { get; set; }

        /// <summary>
        /// Displays relevant information in interface
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Value} {Unit_name} {Category_name}";
        }

    }
}
