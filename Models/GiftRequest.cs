using Microsoft.AspNetCore.Identity;

namespace lab5_start.Models
{
    public class GiftRequest
    {
        public int Id { get; set; }
        public int GiftId { get; set; }
        public virtual Gift Gift { get; set; }
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
    }
}
