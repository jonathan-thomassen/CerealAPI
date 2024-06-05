using CerealAPI.Enums;
using CerealAPI.Models;

namespace CerealAPI.Services
{
    public interface ICerealService
    {
        Task<List<CerealProduct>> GetCereal(
            int? id = null,
            string? name = null,
            Manufacturer? manufacturer = null,
            CerealType? cerealType = null,
            short? minCalories = null,
            bool? minCalIncl = null,
            short? maxCalories = null,
            bool? maxCalIncl = null,
            byte? minProtein = null,
            bool? minProIncl = null,
            byte? maxProtein = null,
            bool? maxProIncl = null,
            byte? minFat = null,
            bool? minFatIncl = null,
            byte? maxFat = null,
            bool? maxFatIncl = null,
            short? minSodium = null,
            bool? minSodIncl = null,
            short? maxSodium = null,
            bool? maxSodIncl = null,
            double? minFiber = null,
            bool? minFibIncl = null,
            double? maxFiber = null,
            bool? maxFibIncl = null,
            double? minCarbohydrates = null,
            bool? minCarbIncl = null,
            double? maxCarbohydrates = null,
            bool? maxCarbIncl = null,
            short? minSugars = null,
            bool? minSugIncl = null,
            short? maxSugars = null,
            bool? maxSugIncl = null,
            short? minPotassium = null,
            bool? minPotIncl = null,
            short? maxPotassium = null,
            bool? maxPotIncl = null,
            short? minVitamins = null,
            bool? minVitIncl = null,
            short? maxVitamins = null,
            bool? maxVitIncl = null,
            double? minWeight = null,
            bool? minWeightIncl = null,
            double? maxWeight = null,
            bool? maxWeightIncl = null,
            double? minCups = null,
            bool? minCupsIncl = null,
            double? maxCups = null,
            bool? maxCupsIncl = null,
            double? minRating = null,
            bool? minRatingIncl = null,
            double? maxRating = null,
            bool? maxRatingIncl = null,
            byte? shelf = null,
            CerealProperty? sortBy = null,
            SortOrder sortOrder = SortOrder.Asc);
        Task<CerealProduct?> PostCereal(CerealProduct cereal);
        Task<(CerealProduct? cereal, bool existed)> UpdateCereal(CerealProduct cereal);
        Task<bool?> DeleteCereal(int id);
    }
}
