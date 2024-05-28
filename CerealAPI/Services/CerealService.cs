using CerealAPI.Enums;
using CerealAPI.Models;
using CerealAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CerealAPI.Services
{
    public class CerealService(ICerealRepository cerealRepository) : ICerealService
    {
        public List<CerealProduct> GetCereal(
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
            SortOrder sortOrder = SortOrder.Asc)
        {
            List<CerealProduct> cereals = cerealRepository.GetAllCereal();

            if (id != null)
            {
                cereals = cereals.Where(c => c.Id == id).ToList();
            }
            if (name != null)
            {
                cereals = cereals.Where(c => c.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (manufacturer != null)
            {
                var manufacturerId = manufacturer.ToString()[0];
                cereals = cereals.Where(c => c.Manufacturer == manufacturerId).ToList();
            }
            if (cerealType != null)
            {
                var cerealTypeId = cerealType.ToString()[0];
                cereals = cereals.Where(c => c.CerealType == cerealTypeId).ToList();
            }
            if (minCalories != null || maxCalories != null)
            {
                cereals = MinMaxFilterInt(cereals, CerealProperty.Calories, minCalories, minCalIncl, maxCalories, maxCalIncl);
            }
            if (minProtein != null || maxProtein != null)
            {
                cereals = MinMaxFilterInt(cereals, CerealProperty.Protein, minProtein, minProIncl, maxProtein, maxProIncl);
            }
            if (minFat != null || maxFat != null)
            {
                cereals = MinMaxFilterInt(cereals, CerealProperty.Fat, minFat, minFatIncl, maxFat, maxFatIncl);
            }
            if (minSodium != null || maxSodium != null)
            {
                cereals = MinMaxFilterInt(cereals, CerealProperty.Sodium, minSodium, minSodIncl, maxSodium, maxSodIncl);
            }
            if (minFiber != null || maxFiber != null)
            {
                cereals = MinMaxFilterFloat(cereals, CerealProperty.Fiber, minFiber, minFibIncl, maxFiber, maxFibIncl);
            }
            if (minCarbohydrates != null || maxCarbohydrates != null)
            {
                cereals = MinMaxFilterFloat(cereals, CerealProperty.Carbohydrates, minCarbohydrates, minCarbIncl, maxCarbohydrates, maxCarbIncl);
            }
            if (minSugars != null || maxSugars != null)
            {
                cereals = MinMaxFilterInt(cereals, CerealProperty.Sugar, minSugars, minSugIncl, maxSugars, maxSugIncl);
            }
            if (minPotassium != null || maxPotassium != null)
            {
                cereals = MinMaxFilterInt(cereals, CerealProperty.Potassium, minPotassium, minPotIncl, maxPotassium, maxPotIncl);
            }
            if (minVitamins != null || maxVitamins != null)
            {
                cereals = MinMaxFilterInt(cereals, CerealProperty.Vitamins, minVitamins, minVitIncl, maxVitamins, maxVitIncl);
            }
            if (minWeight != null || maxWeight != null)
            {
                cereals = MinMaxFilterFloat(cereals, CerealProperty.Weight, minWeight, minWeightIncl, maxWeight, maxWeightIncl);
            }
            if (minCups != null || maxCups != null)
            {
                cereals = MinMaxFilterFloat(cereals, CerealProperty.Cups, minCups, minCupsIncl, maxCups, maxCupsIncl);
            }
            if (minRating != null || maxRating != null)
            {
                cereals = MinMaxFilterFloat(cereals, CerealProperty.Rating, minRating, minRatingIncl, maxRating, maxRatingIncl);
            }


            if (sortBy != null)
            {
                cereals = Sort(cereals, (CerealProperty)sortBy, sortOrder);
            }

            return cereals;
        }

        private static List<CerealProduct> MinMaxFilterInt(List<CerealProduct> cereals, CerealProperty property, int? min, bool? minIncl, int? max, bool? maxIncl)
        {
            if (min != null)
            {
                if (minIncl != null && (bool)minIncl)
                {
                    cereals = cereals.Where(c => (int)c.GetType().GetProperty(property.ToString()).GetValue(c) >= min).ToList();
                }
                else
                {
                    cereals = cereals.Where(c => (int)c.GetType().GetProperty(property.ToString()).GetValue(c) > min).ToList();
                }
            }
            if (max != null)
            {
                if (minIncl != null && (bool)minIncl)
                {
                    cereals = cereals.Where(c => (int)c.GetType().GetProperty(property.ToString()).GetValue(c) >= min).ToList();
                }
                else
                {
                    cereals = cereals.Where(c => (int)c.GetType().GetProperty(property.ToString()).GetValue(c) > min).ToList();
                }
            }

            return cereals;
        }

        private static List<CerealProduct> MinMaxFilterFloat(List<CerealProduct> cereals, CerealProperty property, double? min, bool? minIncl, double? max, bool? maxIncl)
        {
            if (min != null)
            {
                if (minIncl != null && (bool)minIncl)
                {
                    cereals = cereals.Where(c => (double)c.GetType().GetProperty(property.ToString()).GetValue(c) >= min).ToList();
                }
                else
                {
                    cereals = cereals.Where(c => (double)c.GetType().GetProperty(property.ToString()).GetValue(c) > min).ToList();
                }
            }
            if (max != null)
            {
                if (minIncl != null && (bool)minIncl)
                {
                    cereals = cereals.Where(c => (double)c.GetType().GetProperty(property.ToString()).GetValue(c) >= min).ToList();
                }
                else
                {
                    cereals = cereals.Where(c => (double)c.GetType().GetProperty(property.ToString()).GetValue(c) > min).ToList();
                }
            }

            return cereals;
        }

        private static List<CerealProduct> Sort(List<CerealProduct> cereals, CerealProperty sortBy, SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Asc)
            {
                cereals = cereals.OrderBy(c => c.GetType().GetProperty(sortBy.ToString()).GetValue(c)).ToList();
            }
            else
            {
                cereals = cereals.OrderByDescending(c => c.GetType().GetProperty(sortBy.ToString()).GetValue(c)).ToList();
            }

            return cereals;
        }
    }
}
