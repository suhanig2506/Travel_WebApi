using API_JWT_TravelBooking.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace API_JWT_TravelBooking.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }
        public async Task<Employee> GetEmployeeById(int id)
        {
            var emp = await _context.Employees.FirstOrDefaultAsync(m => m.EmpId == id);
            return emp;
        }
        public async Task<Employee> AddEmployee(Employee emp)
        {
            if (emp != null)
            {
                await _context.AddAsync(emp);
                await _context.SaveChangesAsync();
            }
            return emp;
        }

        public async Task<Employee> UpdateEmployee(Employee emp, int id)
        {

            Employee? employee = _context.Employees.FirstOrDefault(x => x.EmpId == id);

            if (employee != null)
            {
                employee.EmpFirstName = emp.EmpFirstName;
                employee.EmpLastName = emp.EmpLastName;
                employee.EmpDob = emp.EmpDob;
                employee.EmpAddress = emp.EmpAddress;
                employee.EmpContact = emp.EmpContact;
                _context.SaveChanges();
            }
            return employee;

        }

        public async Task DeleteEmployee(int id)
        {
            Employee? e = _context.Employees.FirstOrDefault(x => x.EmpId == id);

            if (e != null)
            {
                TravelRequest travel_old = _context.TravelRequests.FirstOrDefault(x => x.EmpId == id);
               // _context.TravelRequests.RemoveRange(travel_old);
                _context.Employees.Remove(e);
                await _context.SaveChangesAsync();
            }
           
        }
    }
}

