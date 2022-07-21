using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Domain;

public class Book
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Author { get; set; }
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    
}