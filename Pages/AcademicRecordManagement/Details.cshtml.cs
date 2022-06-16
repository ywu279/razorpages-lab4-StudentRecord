﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab4.DataAccess;

namespace Lab4.Pages.AcademicRecordManagement
{
    public class DetailsModel : PageModel
    {
        private readonly Lab4.DataAccess.StudentRecordContext _context;

        public DetailsModel(Lab4.DataAccess.StudentRecordContext context)
        {
            _context = context;
        }

      public AcademicRecord AcademicRecord { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.AcademicRecords == null)
            {
                return NotFound();
            }

            var academicrecord = await _context.AcademicRecords.FirstOrDefaultAsync(m => m.StudentId == id);
            if (academicrecord == null)
            {
                return NotFound();
            }
            else 
            {
                AcademicRecord = academicrecord;
            }
            return Page();
        }
    }
}
