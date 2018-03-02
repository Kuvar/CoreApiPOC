namespace CoreApiPOC.Models
{
    public class AuthResponse
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public bool isAdmin { get; set; }
        public bool isApproved { get; set; }
        public string token { get; set; }
    }
}
