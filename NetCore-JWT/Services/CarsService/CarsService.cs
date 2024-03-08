using NetCore_JWT.Context;
using NetCore_JWT.Models;

namespace NetCore_JWT.Services.CarsService
{
	public class CarsService : ICarsService
	{//private readonly ICarsService _carService;
		public CaseDBContext _context;

		public CarsService(CaseDBContext context)
		{
			//_carService = carService;
			_context = context;
		}
		public void AddCar(Car car)
		{
			_context.Cars.Add(car);
			_context.SaveChanges();
		}

		public void DeleteCar(int id)
		{
			var findCar = _context.Cars.FirstOrDefault(c => c.ID == id);

			if (findCar != null)
			{

				_context.Cars.Remove(findCar);
				_context.SaveChanges();
			}
			else
			{

				throw new Exception("Car not found");
			}
		}

		public IEnumerable<string> GetBrands()
		{
			var brands = _context.Cars.Select(c => c.Brand).Distinct().ToList();

			return brands;
		}

		public IEnumerable<string> GetModelsByBrand(string brand)
		{
			var models = _context.Cars
					  .Where(c => c.Brand == brand)
					  .Select(c => c.Model)
					  .Distinct()
					  .ToList();

			return models;
		}

		public IEnumerable<Car> ListCars()
		{
			var cars = _context.Cars.ToList();

			return cars;
		}

		public void UpdateCar(int id, Car car)
		{
			var findCar = _context.Cars.FirstOrDefault(c => c.ID == id);

			if (findCar != null)
			{
				// Arabanın bilgileri güncellenir
				findCar.Brand = car.Brand;
				findCar.Model = car.Model;
				findCar.Color = car.Color;
				findCar.Price = car.Price;

				_context.SaveChanges();
			}
			else
			{
				throw new Exception("Car not found");
			}
		}
	}
}
