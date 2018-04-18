using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wedding_Planner.Models
{

    public class WeddingInfo
    {
        [Key]
        public int WeddingId { get; set; }

        [Display(Name = "Wedder One: ")]
        [Required(ErrorMessage="Wedder One is required!")]
        [MinLength(2, ErrorMessage = "Wedder One's Name must contain at least 2 characters!")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Wedder One's Name can only contain letters!")]
        public string WedderOne { get; set; }

        [Display(Name = "Wedder Two: ")]
        [Required(ErrorMessage="Wedder Two is required!")]
        [MinLength(2, ErrorMessage = "Wedder Two's Name must contain at least 2 characters!")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Wedder Two's Name can only contain letters!")]
        public string WedderTwo { get; set; }

        [Display(Name = "Wedding Address: ")]
        [Required(ErrorMessage="Address of the Wedding is required!")]
        public string WeddingAddress { get; set; }

        [Display(Name = "Date: ")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage="Date is required!")]
        [CurrentDate(ErrorMessage = "Date must be before current date!")]
        public DateTime WeddingDate { get; set; } = DateTime.Now;

        public int CreatedById { get; set; }

        public User CreatedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<WeddingPlan> Guests { get; set; }
 
        public WeddingInfo()
        {
            Guests = new List<WeddingPlan>();
        }
    }

    public class CurrentDateAttribute : ValidationAttribute
    {
        public CurrentDateAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            if(dt > DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}