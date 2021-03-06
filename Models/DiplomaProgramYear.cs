using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NsccCourseMap.Models
{
  [Table("DiplomaProgramYears")]
  public class DiplomaProgramYear
  {
    //Scalar properties
    public int Id { get; set; }
    [RegularExpression(@"^Year [1-4]$")]
    [StringLength(100, MinimumLength = 1)]
    [Required(ErrorMessage = "Please enter title")]
    public string Title { get; set; }
    [Required]
    [Display(Name = "Diploma Program")]
    public int DiplomaProgramId { get; set; }

    //Navigation properties
    [Display(Name = "Diploma Program")]
    public DiplomaProgram DiplomaProgram { get; set; }
    public ICollection<DiplomaProgramYearSection> DiplomaProgramYearSections { get; set; }
  }
}