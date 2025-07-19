namespace YuvamHazir.API.DTOs
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        // public string UserFullName { get; set; } // İleride istersen eklersin
    }
}
