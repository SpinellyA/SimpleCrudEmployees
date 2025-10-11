public class Employee
{
    private string _name;
    private string _description;
    private string _address;
    private double _salary;

    public int Id { get; private set; }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty!");
            _name = value;
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (value is null)
                throw new ArgumentException("Description cannot be null!");
            _description = value;
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Address cannot be empty!");
            _address = value;
        }
    }

    public double Salary
    {
        get => _salary;
        set
        {
            if (value < 0)
                throw new ArgumentException("Salary cannot be negative!");
            _salary = value;
        }
    }

   
    private Employee() { }

    public Employee(string name, string address, double salary, string description)
    {
        Name = name;
        Address = address;
        Salary = salary;
        Description = description;
    }

    // For our example we're only using crud. So we dont really need this. 
    //public void NewSalary(double salary) 
    // if (Salary < 0) 
    // throw new Exception("Salary cannot be negative!"); 
    // Salary = salary; //} }
}
