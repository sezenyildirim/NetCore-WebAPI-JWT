namespace NetCore_JWT.Models
{
	public class User
	{
		[System.Text.Json.Serialization.JsonIgnore]
		public int ID { get; set; }
		public string FullName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		[System.Text.Json.Serialization.JsonIgnore]

		public string? JwtToken { get; set; }
	}
}
