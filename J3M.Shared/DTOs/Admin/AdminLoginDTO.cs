
namespace J3M.Shared.DTOs.Admin
{
    public class AdminLoginDTO
    {
        // mark required to satisfy non-nullable property warnings (CS8618)
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
