using NetCore_JWT.Models;

namespace NetCore_JWT.Services.CarsService
{
	public interface ICarsService
	{
		IEnumerable<string> GetBrands();
		IEnumerable<string> GetModelsByBrand(string brand);
		IEnumerable<Car> ListCars();
		void AddCar(Car car);
		void UpdateCar(int id, Car car);
		void DeleteCar(int id);
	}
}
