using NetCore_JWT.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NetCore_JWT.Services.AuthService;
using NetCore_JWT.Services.CarsService;
using System.Net;
using NetCore_JWT.DTOS;
using NetCore_JWT.DTOS;
using NetCore_JWT.Services.AuthService;
using NetCore_JWT.Services.CarsService;

var builder = WebApplication.CreateBuilder(args);





var jwtIssuer = builder.Configuration.GetSection("JWT:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("JWT:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
	 options.TokenValidationParameters = new TokenValidationParameters
	 {
		 ValidateIssuer = false,
		 ValidateAudience = false,
		 ValidateLifetime = true,
		 ValidateIssuerSigningKey = true,
		 ValidIssuer = jwtIssuer,
		 ValidAudience = jwtIssuer,
		 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
	 };
 });

builder.Services.AddSingleton<JwtAuthenticationManager>(new JwtAuthenticationManager(builder.Configuration["Jwt:Key"]));
builder.Services.AddTransient<ICarsService, CarsService>();
builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CaseDBContext>(b => b.UseSqlServer
(builder.Configuration.GetConnectionString("SqlServer")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
	await next();

	if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized) // 401
	{
		context.Response.ContentType = "application/json";
		await context.Response.WriteAsync(new ResponseDTO()
		{
			StatusCode = 401,
			Message = "Token geçerli deðil. Lütfen giriþ yapýnýz."
		}.ToString());
	}
});
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
