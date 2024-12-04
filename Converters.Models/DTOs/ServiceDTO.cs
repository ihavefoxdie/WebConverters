namespace Converters.Models;
public class ServiceDTO
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = "Untitled";
    public string Type { get; set; } = "Unknown";
    public string CategoryName { get; set; } = "Untitled";
    public string Description { get; set; } = "This is description";
    public string Address { get; set; } = "0.0.0.0";
}
