using API_JWT_TravelBooking.Models;

namespace API_JWT_TravelBooking.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> AddEmployee(Employee emp);
        Task<Employee> UpdateEmployee(Employee emp, int id);
        Task DeleteEmployee(int id);
      
    }
}
