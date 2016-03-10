using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class RestaurantReview : IValidatableObject
    {
        public int Id { get; set; }
        [Range(1,10)]
        //Int values are required by default[Required]
        public int Rating { get; set; }
        //[Required(ErrorMessageResourceType = typeof(OdeToFood.Views.Home.Resources), ErrorMessageResourceName = "Error"]
        [Required]
        [StringLength(1024)]
        public string Body { get; set; }
        [Display(Name ="User Name")]
        [DisplayFormat(NullDisplayText ="Anonymous")]
        public string ReviewerName { get; set; }
        public int RestaurantId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Access to entire model!
            if(Rating < 2 && ReviewerName.ToLower().StartsWith("scott"))
            {
                yield return new ValidationResult("Scott is persona non grata");
            }
        }
    }
}