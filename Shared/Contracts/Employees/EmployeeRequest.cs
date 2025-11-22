//public record RelatedEmployeeObject
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public string Description { get; set; }
//}

public record EmployeeResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public double Salary { get; set; }
    // public string Role { get; set; }
    public string Status { get; set; }
}