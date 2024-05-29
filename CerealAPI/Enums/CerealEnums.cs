namespace CerealAPI.Enums
{
    public enum CerealProperty
    {
        Id,
        Name,
        Manufacturer,
        CerealType,
        Calories,
        Protein,
        Fat,
        Sodium,
        Fiber,
        Carbohydrates,
        Sugars,
        Potassium,
        Vitamins,
        Shelf,
        Weight,
        Cups,
        Rating
    }

    public enum Manufacturer
    {
        AmericanHomemadeFoodProducts,
        GeneralMills,
        Kelloggs,
        Nabisco,
        Post,
        QuakerOats,
        RalstonPurina
    }

    public enum CerealType
    {
        Cold,
        Hot
    }

    public enum MinMax
    {
        Min,
        Max
    }

    public enum InclExcl
    {
        Incl,
        Excl
    }

    public enum SortOrder
    {
        Asc,
        Desc
    }
}
