using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NsccCourseMap.Data;
using NsccCourseMap.Models;

namespace NsccCourseMap_Neo.Pages.AcademicYears
{
    public class DeleteModel : PageModel
    {
        private readonly NsccCourseMap.Data.NsccCourseMapContext _context;

        public DeleteModel(NsccCourseMap.Data.NsccCourseMapContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AcademicYear AcademicYear { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AcademicYear = await _context.AcademicYears.FirstOrDefaultAsync(m => m.Id == id);

            if (AcademicYear == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AcademicYear = await _context.AcademicYears.FindAsync(id);

            if (AcademicYear != null)
            {
                _context.AcademicYears.Remove(AcademicYear);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
