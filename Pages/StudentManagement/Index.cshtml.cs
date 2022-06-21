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
    public class IndexModel : PageModel
    {
        private readonly Lab4.DataAccess.StudentRecordContext _context;

        public IndexModel(Lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public List<AcademicRecord> AcademicRecordsList { get; set; } = default!;



        public async Task OnGetAsync(string orderby)
        {
            if (_context.Students != null)
            {
                Student = await _context.Students.ToListAsync(); //we have nothing to do but to wait for students result comes back

                AcademicRecordsList = await _context.AcademicRecords.ToListAsync(); //用_context调取DB里的AcademicRecord table，给AcademicRecordsList赋值                


                List<Student> students = Student.ToList();  //IList cannot use Sort(), so copy it to a new list called students。 当Sort完之后，再让Student = students                
                
                if (orderby != null)
                {
                    if (orderby == "name")
                    {
                        students.Sort((a, b) => a.Name.CompareTo(b.Name));
                        Student = students;
                    }
                    if (orderby == "numberOfCourses")
                    {
                        students.Sort((a, b) => a.AcademicRecords.Count().CompareTo(b.AcademicRecords.Count()));
                        Student = students;
                    }
                    if (orderby == "avgGrade")
                    {
                        students.Sort((a, b) => a.AcademicRecords.Average(ar => ar.Grade).ToString().CompareTo(b.AcademicRecords.Average(ar => ar.Grade).ToString()));
                        Student = students;
                    }
                }
                
            }
        }

        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {

            var academicrecord = _context.AcademicRecords.Where(m => m.StudentId == id).ToList();
            var student = await _context.Students.FindAsync(id);

            if (id == null || _context.AcademicRecords == null)
            {
                return NotFound();
            }

            if (academicrecord != null)
            { 
                _context.AcademicRecords.RemoveRange(academicrecord);           
            }

            if (student != null)
            {
                _context.Students.Remove(student);   
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }

    
}
