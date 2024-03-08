using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore_JWT.Models;
using NetCore_JWT.Services.CarsService;

namespace NetCore_JWT.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]

	public class CarsController : ControllerBase
	{
		private readonly ICarsService _carService;

		public CarsController(ICarsService carService)
		{
			_carService = carService;
		}
		[HttpGet("GetBrands")]
		public IActionResult GetBrands()
		{
			var brands = _carService.GetBrands();
			return Ok(brands);
		}

		[HttpGet("ModelsByBrand/{brand}")]
		public IActionResult GetModelsByBrand(string brand)
		{
			var models = _carService.GetModelsByBrand(brand);
			return Ok(models);
		}

		[HttpGet("ListCars")]
		public IActionResult ListCars()
		{
			var filteredCars = _carService.ListCars();
			return Ok(filteredCars);
		}

		[HttpPost("AddCar")]
		public IActionResult AddCar([FromBody] Car car)
		{
			try
			{

				_carService.AddCar(car);
				return Ok("Yeni araç eklendi");
			}
			catch
			{

				return BadRequest("Araç ekleme işlemi sırasında bir hata oluştu");
			}

		}

		[HttpPut("UpdateCar/{id}")]
		public IActionResult UpdateCar(int id, [FromBody] Car car)
		{
			try
			{
				_carService.UpdateCar(id, car);
				return Ok("Araç özellikleri güncellendi");
			}
			catch
			{
				return BadRequest("Araç güncelleme işlemi sırasında bir hata oluştu");

			}

		}

		[HttpDelete("DeleteCar/{id}")]
		public IActionResult DeleteCar(int id)
		{
			try
			{
				_carService.DeleteCar(id);
				return Ok("Araç silindi");
			}
			catch
			{
				return BadRequest("Araç silme işlemi sırasında bir hata oluştu");

			}

		}
	}
}
