using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NsccCourseMap.Data;
using NsccCourseMap.Models;

namespace NsccCourseMap_Neo.Pages.AdvisingAssignments
{
  public class NewSectionsData
  {
    public int Id { get; set; }
    public string Title { get; set; }
  }

  public class CreateModel : PageModel
  {
    private readonly NsccCourseMap.Data.NsccCourseMapContext _context;

    public CreateModel(NsccCourseMap.Data.NsccCourseMapContext context)
    {
      _context = context;
    }

    public IActionResult OnGet()
    {
      IQueryable<DiplomaProgramYearSection> sortResult = from dpys in _context.DiplomaProgramYearSections
                                                         select dpys;

      List<NewSectionsData> newSectionsData = sortResult
      .Select(dpys => new NewSectionsData()
      {
        Id = dpys.Id,
        Title = dpys.DiplomaProgramYear.DiplomaProgram.Title
              + " - "
              + dpys.DiplomaProgramYear.Title
              + " - "
              + dpys.Title
      }).ToList();

      ViewData["DiplomaProgramYearSectionId"] = new SelectList(newSectionsData, "Id", "Title");
      //   ViewData["DiplomaProgramYearSectionId"] = new SelectList(_context.DiplomaProgramYearSections, "Id", "Title");
      ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "FirstName");
      return Page();
    }

    [BindProperty]
    public AdvisingAssignment AdvisingAssignment { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      _context.AdvisingAssignments.Add(AdvisingAssignment);
      await _context.SaveChangesAsync();

      return RedirectToPage("./Index");
    }
  }
}