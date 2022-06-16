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

        public async Task OnGetAsync()
        {
            if (_context.Students != null)
            {
                Student = await _context.Students.ToListAsync(); //we have nothing to do but to wait for students result comes back
            }
        }
    }
}
