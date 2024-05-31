namespace CerealAPI.Models
{
    public record CerealProduct(
        int Id,
        string Name,
        char Manufacturer,
        char CerealType,
        short Calories,
        byte Protein,
        byte Fat,
        short Sodium,
        double Fiber,
        double Carbohydrates,
        short Sugars,
        short Potassium,
        short Vitamins,
        byte Shelf,
        double Weight,
        double Cups,
        double Rating) : Dto(Id);
}
