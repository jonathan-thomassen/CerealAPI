using CerealAPI.Enums;
using CerealAPI.Models;
using CerealAPI.Repositories;

namespace CerealAPI.Services
{
    public class CerealService(
        ICerealRepository repository, IImageRepository imageRepository) :
        ICerealService
    {
        public List<CerealProduct> GetCereal(
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
            SortOrder sortOrder = SortOrder.Asc)
        {
            List<CerealProduct> cereals = repository.GetAllCereal();

            // TODO: Create some more functions here
            if (id != null)
            {
                cereals = cereals.Where(c => c.Id == id).ToList();
            }
            if (name != null)
            {
                cereals = cereals.Where(c => c.Name.Contains(name,
                    StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (manufacturer != null)
            {
                var manufacturerId = manufacturer?.ToString()[0];
                cereals = cereals.Where(c => c.Manufacturer == manufacturerId)
                    .ToList();
            }
            if (cerealType != null)
            {
                var cerealTypeId = cerealType?.ToString()[0];
                cereals = cereals.Where(c => c.CerealType == cerealTypeId)
                    .ToList();
            }
            if (minCalories != null || maxCalories != null)
            {
                cereals = MinMaxFilterInt(cereals, CerealProperty.Calories,
                    minCalories, minCalIncl, maxCalories, maxCalIncl);
            }
            if (minProtein != null || maxProtein != null)
            {
                cereals = MinMaxFilterInt(cereals, CerealProperty.Protein,
                    minProtein, minProIncl, maxProtein, maxProIncl);
            }
            if (minFat != null || maxFat != null)
            {
                cereals = MinMaxFilterInt(cereals, CerealProperty.Fat, minFat,
                    minFatIncl, maxFat, maxFatIncl);
            }
            if (minSodium != null || maxSodium != null)
            {
                cereals = MinMaxFilterInt(cereals, CerealProperty.Sodium,
                    minSodium, minSodIncl, maxSodium, maxSodIncl);
            }
            if (minFiber != null || maxFiber != null)
            {
                cereals = MinMaxFilterFloat(cereals, CerealProperty.Fiber,
                    minFiber, minFibIncl, maxFiber, maxFibIncl);
            }
            if (minCarbohydrates != null || maxCarbohydrates != null)
            {
                cereals = MinMaxFilterFloat(cereals,
                    CerealProperty.Carbohydrates, minCarbohydrates,
                    minCarbIncl, maxCarbohydrates, maxCarbIncl);
            }
            if (minSugars != null || maxSugars != null)
            {
                cereals = MinMaxFilterInt(cereals, CerealProperty.Sugars,
                    minSugars, minSugIncl, maxSugars, maxSugIncl);
            }
            if (minPotassium != null || maxPotassium != null)
            {
                cereals = MinMaxFilterInt(cereals, CerealProperty.Potassium,
                    minPotassium, minPotIncl, maxPotassium, maxPotIncl);
            }
            if (minVitamins != null || maxVitamins != null)
            {
                cereals = MinMaxFilterInt(cereals, CerealProperty.Vitamins,
                    minVitamins, minVitIncl, maxVitamins, maxVitIncl);
            }
            if (minWeight != null || maxWeight != null)
            {
                cereals = MinMaxFilterFloat(cereals, CerealProperty.Weight,
                    minWeight, minWeightIncl, maxWeight, maxWeightIncl);
            }
            if (minCups != null || maxCups != null)
            {
                cereals = MinMaxFilterFloat(cereals, CerealProperty.Cups,
                    minCups, minCupsIncl, maxCups, maxCupsIncl);
            }
            if (minRating != null || maxRating != null)
            {
                cereals = MinMaxFilterFloat(cereals, CerealProperty.Rating,
                    minRating, minRatingIncl, maxRating, maxRatingIncl);
            }
            if (shelf != null)
            {
                cereals = cereals.Where(c => c.Shelf == shelf).ToList();
            }
            if (sortBy != null)
            {
                cereals = Sort(cereals, (CerealProperty)sortBy, sortOrder);
            }

            return cereals;

            List<CerealProduct> MinMaxFilterInt(
                List<CerealProduct> cereals, CerealProperty property, int? min,
                bool? minIncl, int? max, bool? maxIncl)
            {
                if (min != null)
                {
                    if (minIncl != null && (bool)minIncl)
                    {
                        cereals = cereals.Where(c => (int?)c
                            .GetType().GetProperty(property.ToString())?
                            .GetValue(c) >= min).ToList();
                    }
                    else
                    {
                        cereals = cereals.Where(c => (int?)c
                            .GetType().GetProperty(property.ToString())?
                            .GetValue(c) > min).ToList();
                    }
                }
                if (max != null)
                {
                    if (maxIncl != null && (bool)maxIncl)
                    {
                        cereals = cereals.Where(c => (int?)c
                            .GetType().GetProperty(property.ToString())?
                            .GetValue(c) >= min).ToList();
                    }
                    else
                    {
                        cereals = cereals.Where(c => (int?)c
                            .GetType().GetProperty(property.ToString())?
                            .GetValue(c) > min).ToList();
                    }
                }

                return cereals;
            }

            List<CerealProduct> MinMaxFilterFloat(
                List<CerealProduct> cereals, CerealProperty property,
                double? min, bool? minIncl, double? max, bool? maxIncl)
            {
                if (min != null)
                {
                    if (minIncl != null && (bool)minIncl)
                    {
                        cereals = cereals.Where(c => (double?)c
                            .GetType().GetProperty(property.ToString())?
                            .GetValue(c) >= min).ToList();
                    }
                    else
                    {
                        cereals = cereals.Where(c => (double?)c
                            .GetType().GetProperty(property.ToString())?
                            .GetValue(c) > min).ToList();
                    }
                }
                if (max != null)
                {
                    if (maxIncl != null && (bool)maxIncl)
                    {
                        cereals = cereals.Where(c => (double?)c
                            .GetType().GetProperty(property.ToString())?
                            .GetValue(c) >= min).ToList();
                    }
                    else
                    {
                        cereals = cereals.Where(c => (double?)c
                            .GetType().GetProperty(property.ToString())?
                            .GetValue(c) > min).ToList();
                    }
                }

                return cereals;
            }

            List<CerealProduct> Sort(List<CerealProduct> cereals,
                CerealProperty sortBy, SortOrder sortOrder)
            {
                if (sortOrder == SortOrder.Asc)
                {
                    cereals = cereals.OrderBy(c => c
                        .GetType().GetProperty(sortBy.ToString())?
                        .GetValue(c)).ToList();
                }
                else
                {
                    cereals = cereals.OrderByDescending(c => c
                        .GetType().GetProperty(sortBy.ToString())?
                        .GetValue(c)).ToList();
                }

                return cereals;
            }
        }
        public async Task<CerealProduct?> PostCereal(CerealProduct cereal)
        {
            var success = await repository.PostCereal(cereal);

            return success ? cereal : null;
        }

        public async Task<(CerealProduct? cereal, bool existed)> UpdateCereal(
            CerealProduct newCereal)
        {
            var oldCereal = await repository.GetCerealById(newCereal.Id);

            if (oldCereal != null)
            {
                var success = await repository.UpdateCereal(
                    oldCereal, newCereal);
                return success ? (oldCereal, true) : (null, true);
            }
            else
            {
                var success = await repository.PostCereal(newCereal);
                return success ? (newCereal, false) : (null, false);
            }
        }

        public async Task<bool?> DeleteCereal(int id)
        {
            var cereal = await repository.GetCerealById(id);

            // Delete soon-to-be orphan image file if it exists
            var imageEntry = await imageRepository.GetImageEntryByCerealId(id);
            if (imageEntry != null)
            {
                File.Delete(imageEntry.Path);
            }

            if (cereal != null)
            {
                var success = await repository.DeleteCereal(cereal);
                return success;
            }
            else
            {
                return null;
            }
        }
    }
}
