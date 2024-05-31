namespace CerealAPI.Models
{
    public record ImageEntry(
        int Id,
        int CerealId,
        string Path) : Dto(Id);
}
