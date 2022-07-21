namespace MinimalApi.Dto;

public class GetBookDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Author { get; set; }
    public DateTime Date { get; set; }
}