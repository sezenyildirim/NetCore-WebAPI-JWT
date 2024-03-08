namespace NetCore_JWT.Models
{
	public class Car
	{
		[System.Text.Json.Serialization.JsonIgnore]
		public int ID { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public string Color { get; set; }
		public string Price { get; set; }
	}
}
