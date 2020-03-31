using BoundaryDetector.Persistence;
using Newtonsoft.Json;
using SQLite;

namespace BoundaryDetector.Models
{
    public class GField : Entity<int>
    {
        public string FieldName { get; set; }
        public string _boundaryDetails { get; set; }
        [Ignore]
        public BoundaryDetails BoundaryDetails
        {
            get
            {
                return _boundaryDetails == null ? null : JsonConvert.DeserializeObject<BoundaryDetails>(_boundaryDetails);
            }
            set
            {
                _boundaryDetails = JsonConvert.SerializeObject(value);
            }
        }
    }
}
