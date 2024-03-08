using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore_JWT.Context;
using NetCore_JWT.DTOS;
using NetCore_JWT.Services.AuthService;

namespace NetCore_JWT.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		public CaseDBContext _context;
		public JwtAuthenticationManager _authManager;

		public LoginController(JwtAuthenticationManager authManager, CaseDBContext context)
		{
			_authManager = authManager;
			_context = context;
		}

		[HttpPost("Login")]
		public IActionResult Login([FromBody] LoginDTO LoginDTO)
		{
			var user = _context.Users.FirstOrDefault(u => u.UserName == LoginDTO.UserName && u.Password == LoginDTO.Password);

			if (user == null)
			{
				return BadRequest("Geçersiz kullanıcı adı ya da şifre");
			}
			string token = _authManager.GenerateToken(LoginDTO.UserName);
			user.JwtToken = token;
			if (!string.IsNullOrEmpty(token))
			{
				_context.Users.Update(user);
				_context.SaveChanges();
			}

			return Ok(new { Message = "Kullanıcı girişi yapıldı", Token = token });

		}

		[HttpPost("Logout")]
		public IActionResult Logout([FromBody] string token)
		{
			var user = _context.Users.FirstOrDefault(u => u.JwtToken == token);

			if (user == null)
			{
				return BadRequest("Geçersiz token");
			}

			user.JwtToken = null; // Token'i sıfırla

			_context.Users.Update(user);
			_context.SaveChanges();

			return Ok("Başarıyla çıkış yapıldı. Yeniden giriş yapmanız gerekmektedir.");
		}

	}
}
