using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab4.DataAccess;

namespace Lab4.Pages.StudentManagement
{
    public class DetailsModel : PageModel
    {
        private readonly Lab4.DataAccess.StudentRecordContext _context;

        public DetailsModel(Lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        public Student Student { get; set; } = default!; 

        public List<Course> CourseList { get; set; }


        public async Task<IActionResult> OnGetAsync(string id, string orderby)
        {
            CourseList = await _context.Courses.ToListAsync(); //为了显示course title 所以先用_context调取DB里的Course table，给CourseList赋值 

            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(s => s.AcademicRecords).ThenInclude(a => a.CourseCodeNavigation).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            else 
            {
                Student = student;
            }




            List<AcademicRecord> academicRecordsList = Student.AcademicRecords.ToList(); //IColletion无法sort，所以copy it to a new List called AcademicRecordsList

            if (orderby != null)
            {
                if (orderby == "course")
                {
                    academicRecordsList.Sort((a, b) => a.CourseCodeNavigation.Title.CompareTo(b.CourseCodeNavigation.Title));
                    Student.AcademicRecords = academicRecordsList;
                }
                if (orderby == "grade")
                {
                    academicRecordsList.Sort((a, b) => a.Grade.ToString().CompareTo(b.Grade.ToString()));
                    Student.AcademicRecords = academicRecordsList;
                }
            }



            return Page();
        }


    }
}
