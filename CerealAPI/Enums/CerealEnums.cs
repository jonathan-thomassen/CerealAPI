namespace CerealAPI.Enums
{    public record FilterParameter<T>(
        CerealProperty Property,
        MinMax MinMax,
        InclExcl InclExcl,
        T Value);

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
        Sugar,
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
