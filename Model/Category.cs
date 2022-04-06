using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservationApp.Model
{
    public class Category
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Basecategory_id { get; set; }
        public int? Unit_id { get; set; }
        public override string ToString()
        {
            return $"{Name}";
        }


    }
}
