using Shared.Enums;
public class EmployeeStatus
{
    public int Id { get; private set; }

    public int EmployeeId { get; private set; }
    public Employee Employee { get; private set; }

    public int EmployeeStatusTypeId { get; private set; }
    public EmployeeStatusType EmployeeStatusType { get ; private set; }

    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }

    public EmployeeStatus()
    {
    }
    public EmployeeStatus(int employeeId, int employeeStatusTypeId, DateTime startTime)
    {
        if (employeeId <= 0) throw new ArgumentException("Invalid employee ID.");
        if (employeeStatusTypeId <= 0) throw new ArgumentException("Invalid status type ID.");
        if (startTime > DateTime.UtcNow.AddYears(1)) throw new ArgumentException("Start time too far in future.");

        EmployeeId = employeeId;
        EmployeeStatusTypeId = employeeStatusTypeId;
        StartTime = startTime;
    }

    public EmployeeStatus(Employee employee, EmployeeStatusType employeeStatusType , DateTime startTime)
    {
        if (employee == null) throw new ArgumentException("Employee is null");
        if (employeeStatusType == null) throw new ArgumentException("Employee Status Type is null!");

        Employee = employee;

        EmployeeStatusTypeId = employeeStatusType.Id;

        StartTime = startTime;
    }

    public void EndStatus()
    {
        if (EndTime != null)
            throw new InvalidOperationException("Status has already ended.");

        if (DateTime.UtcNow < StartTime)
            throw new InvalidOperationException("End time cannot be before start time.");

        EndTime = DateTime.UtcNow;
    }

    public bool IsActive => EndTime == null;
}