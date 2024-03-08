using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore_JWT.Context;
using NetCore_JWT.Models;
using NetCore_JWT.Services.AuthService;

namespace NetCore_JWT.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegisterController : ControllerBase
	{
		private readonly JwtAuthenticationManager _jwtManager;
		public readonly CaseDBContext _context;

		public RegisterController(JwtAuthenticationManager jwtManager, CaseDBContext context)
		{
			_jwtManager = jwtManager;
			_context = context;
		}

		[HttpPost]
		public IActionResult RegisterUser([FromBody] User userModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.Users.Add(userModel);
			_context.SaveChanges();


			return Ok(new { Message = "Kullanıcı başarıyla kaydedildi." });
		}
	}
}
