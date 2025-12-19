using System.ComponentModel.DataAnnotations;

namespace lab5_start.Models.ViewModels
{
    public class GiftRequestInputModel
    {
        [Required]
        public int GiftId { get; set; }
        public string Description { get; set; }
    }
}
