using System;
using System.Collections.Generic;
using System.Text;

namespace BoundaryDetector.Models
{

    public class BoundaryDetails
    {
        public string type { get; set; }
        public Feature[] features { get; set; }
        public bool fallback_boundary { get; set; }
    }

    public class Feature
    {
        public int id { get; set; }
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public float[][][] coordinates { get; set; }
    }

    public class Properties
    {
        public string status { get; set; }
        public object corrected { get; set; }
        public string schema_id { get; set; }
        public float area { get; set; }
        public string fb_type { get; set; }
        public string operation_status { get; set; }
        public string report_id { get; set; }
        public DateTime created_date { get; set; }
        public DateTime updated_date { get; set; }
    }

}
