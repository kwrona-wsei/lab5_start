using System.ComponentModel.DataAnnotations;

namespace lab5_start.Models.ViewModels
{
    public class GiftInputModel
    {
        [Required]
        public string GiftName { get; set; }
        public bool IsApproved { get; set; }
    }
}
