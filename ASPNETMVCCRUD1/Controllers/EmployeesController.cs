using ASPNETMVCCRUD1.Data;
using ASPNETMVCCRUD1.Models;
using ASPNETMVCCRUD1.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD1.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoContext;

        public EmployeesController(MVCDemoDbContext mvcDemoContext)
        {
            this.mvcDemoContext = mvcDemoContext;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoContext.Employees.ToListAsync();
            return View(employees);

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth
            };

            await mvcDemoContext.Employees.AddAsync(employee);
            await mvcDemoContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id) 
        {

            var employee = await mvcDemoContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth
                };
                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = model.Department;
                employee.DateOfBirth = model.DateOfBirth;

                await mvcDemoContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }


        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                mvcDemoContext.Employees.Remove(employee);
                await mvcDemoContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

		}
    }
}
