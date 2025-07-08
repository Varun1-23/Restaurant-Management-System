using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.Model
{
    public class Cuisine
    {
        public long Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public float NoOfPeople { get; set; }
        public bool IsActive { get; set; }
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
