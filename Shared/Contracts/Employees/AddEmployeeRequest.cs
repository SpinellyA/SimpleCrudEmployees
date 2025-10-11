
public record AddEmployeeRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public double Salary { get; set; }
    // public string Role { get; set; }
    public string Status { get; set; }
}