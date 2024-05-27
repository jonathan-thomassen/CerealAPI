using CerealAPI.Enums;
using CerealAPI.Models;

namespace CerealAPI.Services
{
    public interface ICerealService
    {
        List<CerealProduct> GetCereal(
            int? id,
            string? name,
            Manufacturer? manufacturer,
            CerealType? cerealType,
            short? minCalories,
            bool? minCalInc,
            short? maxCalories,
            bool? maxCalInc,
            byte? minProtein,
            bool? minProInc,
            byte? maxProtein,
            bool? maxProInc,
            byte? minFat,
            byte? maxFat,
            short? minSodium,
            short? maxSodium,
            double? minFiber,
            double? maxFiber,
            short? minSugar,
            short? maxSugar,
            short? minPotass,
            short? maxPotass,
            short? minVitamins,
            short? maxVitamins,
            byte? shelf,
            double? minWeight,
            double? maxWeight,
            double? minCups,
            double? maxCups,
            double? minRating,
            double? maxRating,
            CerealProperty? sortBy,
            bool sortAsc);
    }
}
