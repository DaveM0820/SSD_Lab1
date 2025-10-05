namespace SSD_Lab1.Models
{
    // This is only a shape to bind values from configuration.
    public class AppSecrets
    {
        public string? AdminEmail { get; set; }
        public string? AdminPwd { get; set; }

        public string? EmployeeEmail { get; set; }
        public string? EmployeePwd { get; set; }
    }
}
