using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NsccCourseMap.Data;
using NsccCourseMap.Models;

namespace NsccCourseMap_Neo.Pages.Instructors
{
  public class NewAdvisingAssignmentsData
  {
    public string AcademicYearTitle { get; set; }
    public string DiplomaProgramTitle { get; set; }
    public string DiplomaProgramYearTitle { get; set; }
    public string DiplomaProgramYearSectionTitle { get; set; }
  }

  public class DetailsModel : PageModel
  {
    private readonly NsccCourseMap.Data.NsccCourseMapContext _context;

    public DetailsModel(NsccCourseMap.Data.NsccCourseMapContext context)
    {
      _context = context;
    }

    public Instructor Instructor { get; set; }

    public List<NewAdvisingAssignmentsData> NewAdvisingAssignmentsDatas { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      Instructor = await _context
      .Instructors
      .Include(i => i.AdvisingAssignments)
      .ThenInclude(aa => aa.DiplomaProgramYearSection.DiplomaProgramYear.DiplomaProgram)
      .Include(i => i.AdvisingAssignments)
      .ThenInclude(aa => aa.DiplomaProgramYearSection.AcademicYear)
      .FirstOrDefaultAsync(m => m.Id == id);

      if (Instructor == null)
      {
        return NotFound();
      }

      NewAdvisingAssignmentsDatas = Instructor.AdvisingAssignments
        .Select(aa => new NewAdvisingAssignmentsData()
        {
          AcademicYearTitle = aa.DiplomaProgramYearSection.AcademicYear.Title,
          DiplomaProgramTitle = aa.DiplomaProgramYearSection.DiplomaProgramYear.DiplomaProgram.Title,
          DiplomaProgramYearTitle = aa.DiplomaProgramYearSection.DiplomaProgramYear.Title,
          DiplomaProgramYearSectionTitle = aa.DiplomaProgramYearSection.Title
        })
        .OrderByDescending(naa => naa.AcademicYearTitle)
        .ToList();

      return Page();
    }
  }
}
