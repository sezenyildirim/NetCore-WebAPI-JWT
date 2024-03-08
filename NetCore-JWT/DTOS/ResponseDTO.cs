using System.Text.Json;

namespace NetCore_JWT.DTOS
{
	public class ResponseDTO
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public override string ToString()
		{
			return JsonSerializer.Serialize(this);
		}
	}
}
