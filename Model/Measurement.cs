using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservationApp.Model
{
    public class Measurement
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public int Observation_id { get; set; }
        public int Category_id { get; set; }
       
    }
}
