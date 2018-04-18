using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wedding_Planner.Models
{
    public class WeddingPlan
    {
        
        public int WeddingPlanId { get; set; }

        public int WeddingId { get; set; }
        public WeddingInfo WeddingInfo { get; set; }

        public int GuestId { get; set; }
        public User Guest { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}