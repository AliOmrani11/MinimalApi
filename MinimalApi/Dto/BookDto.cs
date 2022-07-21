using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Dto;

public class BookDto
{
    public string? Name { get; set; }
    public string? Author { get; set; }
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
}