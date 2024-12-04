public class DataTableRequestDto
{
    public int Draw { get; set; }
    public List<ColumnDto> Columns { get; set; }
    public List<OrderDto> Order { get; set; }
    public int Start { get; set; }
    public int Length { get; set; }
    public SearchDto Search { get; set; }
}

public class ColumnDto
{
    public string Data { get; set; }
    public string Name { get; set; }
    public bool Searchable { get; set; }
    public bool Orderable { get; set; }
    public SearchDto Search { get; set; }
}

public class OrderDto
{
    public int Column { get; set; }
    public string Dir { get; set; }
}

public class SearchDto
{
    public string Value { get; set; }
    public bool Regex { get; set; }
}
