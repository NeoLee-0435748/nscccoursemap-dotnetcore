using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NsccCourseMap.Data;
using NsccCourseMap.Models;

namespace NsccCourseMap_Neo.Pages.DiplomaProgramYears
{
    public class EditModel : PageModel
    {
        private readonly NsccCourseMap.Data.NsccCourseMapContext _context;

        public EditModel(NsccCourseMap.Data.NsccCourseMapContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DiplomaProgramYear DiplomaProgramYear { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DiplomaProgramYear = await _context.DiplomaProgramYears
                .Include(d => d.DiplomaProgram).FirstOrDefaultAsync(m => m.Id == id);

            if (DiplomaProgramYear == null)
            {
                return NotFound();
            }
           ViewData["DiplomaProgramId"] = new SelectList(_context.DiplomaPrograms, "Id", "Title");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DiplomaProgramYear).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiplomaProgramYearExists(DiplomaProgramYear.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DiplomaProgramYearExists(int id)
        {
            return _context.DiplomaProgramYears.Any(e => e.Id == id);
        }
    }
}
