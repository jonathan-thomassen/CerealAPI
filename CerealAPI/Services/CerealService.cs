using CerealAPI.Enums;
using CerealAPI.Models;
using CerealAPI.Repositories;

namespace CerealAPI.Services
{
    public class CerealService(ICerealRepository cerealRepository) : ICerealService
    {
        public List<CerealProduct> GetCereal(int? id, string? name, Manufacturer? manufacturer, CerealType? cerealType, short? minCalories, bool? minCalInc, short? maxCalories, bool? maxCalInc, byte? minProtein, bool? minProInc, byte? maxProtein, bool? maxProInc, byte? minFat, byte? maxFat, short? minSodium, short? maxSodium, double? minFiber, double? maxFiber, short? minSugar, short? maxSugar, short? minPotass, short? maxPotass, short? minVitamins, short? maxVitamins, byte? shelf, double? minWeight, double? maxWeight, double? minCups, double? maxCups, double? minRating, double? maxRating, CerealProperty? sortBy, bool sortAsc)
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
            if (minCalories != null && minCalInc != null || maxCalories != null && maxCalInc != null)
            {
                cereals = MinMaxFilterShort(cereals, nameof(CerealProduct.Calories), minCalories, minCalInc, maxCalories, maxCalInc);
            }
            if (minProtein != null && minProInc != null || maxProtein != null && maxProInc != null)
            {
                cereals = MinMaxFilterByte(cereals, nameof(CerealProduct.Protein), minProtein, minProInc, maxProtein, maxProInc);
            }

            if (sortBy != null)
            {
                cereals = Sort(cereals, (CerealProperty)sortBy, sortAsc);
            }

            return cereals;
        }

        private static List<CerealProduct> MinMaxFilterShort(List<CerealProduct> cereals, string property, short? min, bool? minInclusive, short? max, bool? maxInclusive)
        {
            if (min != null && minInclusive != null)
            {
                if ((bool)minInclusive)
                {
                    cereals = cereals.Where(c => (short)c.GetType().GetProperty(property).GetValue(c) >= min).ToList();
                }
                else
                {
                    cereals = cereals.Where(c => (short)c.GetType().GetProperty(property).GetValue(c) > min).ToList();
                }
            }

            if (max != null && maxInclusive != null)
            {
                if ((bool)maxInclusive)
                {
                    cereals = cereals.Where(c => (short)c.GetType().GetProperty(property).GetValue(c) <= max).ToList();
                }
                else
                {
                    cereals = cereals.Where(c => (short)c.GetType().GetProperty(property).GetValue(c) < max).ToList();
                }
            }

            return cereals;
        }

        private static List<CerealProduct> MinMaxFilterByte(List<CerealProduct> cereals, string property, int? min, bool? minInclusive, int? max, bool? maxInclusive)
        {
            if (min != null && minInclusive != null)
            {
                if ((bool)minInclusive)
                {
                    cereals = cereals.Where(c => (byte)c.GetType().GetProperty(property).GetValue(c) >= min).ToList();
                }
                else
                {
                    cereals = cereals.Where(c => (byte)c.GetType().GetProperty(property).GetValue(c) > min).ToList();
                }
            }

            if (max != null && maxInclusive != null)
            {
                if ((bool)maxInclusive)
                {
                    cereals = cereals.Where(c => (byte)c.GetType().GetProperty(property).GetValue(c) <= max).ToList();
                }
                else
                {
                    cereals = cereals.Where(c => (byte)c.GetType().GetProperty(property).GetValue(c) < max).ToList();
                }
            }

            return cereals;
        }        

        private static List<CerealProduct> Sort(List<CerealProduct> cereals, CerealProperty sortBy, bool sortAsc)
        {
            if (sortAsc)
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
