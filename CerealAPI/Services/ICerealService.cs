using CerealAPI.Enums;
using CerealAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
            bool? minCalIncl,
            short? maxCalories,
            bool? maxCalIncl,
            byte? minProtein,
            bool? minProIncl,
            byte? maxProtein,
            bool? maxProIncl,
            byte? minFat,
            bool? minFatIncl,
            byte? maxFat,
            bool? maxFatIncl,
            short? minSodium,
            bool? minSodIncl,
            short? maxSodium,
            bool? maxSodIncl,
            double? minFiber,
            bool? minFibIncl,
            double? maxFiber,
            bool? maxFibIncl,
            double? minCarbohydrates,
            bool? minCarbIncl,
            double? maxCarbohydrates,
            bool? maxCarbIncl,
            short? minSugars,
            bool? minSugIncl,
            short? maxSugars,
            bool? maxSugIncl,
            short? minPotassium,
            bool? minPotIncl,
            short? maxPotassium,
            bool? maxPotIncl,
            short? minVitamins,
            bool? minVitIncl,
            short? maxVitamins,
            bool? maxVitIncl,
            double? minWeight,
            bool? minWeightIncl,
            double? maxWeight,
            bool? maxWeightIncl,
            double? minCups,
            bool? minCupsIncl,
            double? maxCups,
            bool? maxCupsIncl,
            double? minRating,
            bool? minRatingIncl,
            double? maxRating,
            bool? maxRatingIncl,
            byte? shelf,
            CerealProperty? sortBy,
            SortOrder sortOrder = SortOrder.Asc);
        Task<CerealProduct?> PostCereal(CerealProduct cereal);
        Task<(CerealProduct? cereal, bool existed)> UpdateCereal(CerealProduct cereal);
        Task<bool?> DeleteCereal(int id);
    }
}
