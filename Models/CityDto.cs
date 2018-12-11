using System.Collections.Generic;
using System.Linq;

namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberofPointofInterest  { get { return PointsofInterests.Count(); } }
        public List<PointsofInterestDto> PointsofInterests { get; set; } = new List<PointsofInterestDto>();
    }
}
