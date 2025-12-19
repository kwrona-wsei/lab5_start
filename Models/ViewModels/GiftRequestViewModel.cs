namespace lab5_start.Models.ViewModels
{
    public class GiftRequestViewModel
    {
        public int Id { get; set; }
        public string GiftName { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
    }
}
