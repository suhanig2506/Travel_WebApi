using API_JWT_TravelBooking.Models;
using API_JWT_TravelBooking.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace API_JWT_TravelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            IEnumerable<Employee> lstEmp = await _repository.GetEmployees();
            if (lstEmp != null)
            {
                return Ok(lstEmp);
            }
            return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
           Employee Emp = await _repository.GetEmployeeById(id);
            if (Emp != null)
            {
                return Ok(Emp);
            }
            return BadRequest();
        }
        [HttpPut("{id}"), Authorize]

        public async Task<ActionResult> Put(int id,[FromBody] Employee emp)
        {
            if (emp == null)
            {
                return BadRequest();
            }
            await _repository.UpdateEmployee(emp, id);
            return Ok(emp);
        }
        [HttpPost]
        public async Task<ActionResult> PostEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if (!_validation.IsValid(category.CatCode))
            //{
            //    return BadRequest();
            //}
            _repository.AddEmployee(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmpId }, employee);
            //return CreatedAtAction(nameof(GetCategories), new { CatId = category.CatId }, category);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            Employee? emp = await _repository.GetEmployeeById(id);
            if (emp != null)
            {
                 //_repository.DeleteEmployee(id);
                await _repository.DeleteEmployee(id);
                return Ok();
            }

            return NotFound();
        }
    }
}

