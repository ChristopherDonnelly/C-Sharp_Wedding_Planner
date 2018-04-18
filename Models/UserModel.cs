using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wedding_Planner.Models
{
    public abstract class BaseEntity {
        [Key]
        public int UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class User : BaseEntity
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<WeddingPlan> Plans { get; set; }
 
        public User()
        {
            Plans = new List<WeddingPlan>();
        }
    }
}