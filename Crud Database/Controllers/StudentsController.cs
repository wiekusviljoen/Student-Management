using Crud_Database.Data;
using Crud_Database.Models;
using Crud_Database.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud_Database.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly MVCDemoDbContext1 mvcDemoDbContext1;

        public StudentsController(MVCDemoDbContext1 mvcDemoDbContext1)


        {
            this.mvcDemoDbContext1 = mvcDemoDbContext1;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await mvcDemoDbContext1.Students.ToListAsync();
            return View(students);
        }




        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel addStudentRequest)
        {
            var student = new Student()
            {
                Id = Guid.NewGuid(),
                Name = addStudentRequest.Name,
                Email = addStudentRequest.Email,
                Salary = addStudentRequest.Salary,
                Department = addStudentRequest.Department,
                DateOfBirth = addStudentRequest.DateOfBirth

            };

            await mvcDemoDbContext1.Students.AddAsync(student);
            await mvcDemoDbContext1.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var student = await mvcDemoDbContext1.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student != null)
            {
                var viewModel = new UpdateStudentViewModel()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Salary = student.Salary,
                    Department = student.Department,
                    DateOfBirth = student.DateOfBirth
                };
                return await Task.Run(() => View("View", viewModel));

            }



            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateStudentViewModel model)
        {
            var employee = await mvcDemoDbContext1.Students.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mvcDemoDbContext1.SaveChangesAsync();

                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateStudentViewModel model)
        {
            var student = await mvcDemoDbContext1.Students.FindAsync(model.Id);

            if (student != null)
            {
                mvcDemoDbContext1.Students.Remove(student);
                await mvcDemoDbContext1.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

    }
}
