
public class EmployeeStatusType
{

    public EmployeeStatusType()
    {

    }

    public int Id { get;  set; }
    public string Name { get; set; }
    public string Description { get; set; }


    public EmployeeStatusType(string name, string description)
    {
        if(name == null || name == string.Empty) throw new ArgumentNullException("Name cannot be empty!");
        if (description == null || description == string.Empty) throw new ArgumentNullException("Description cannot be empty!");

        Name = name;
        Description = description;
    }
}