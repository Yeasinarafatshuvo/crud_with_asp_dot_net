
namespace crudAsp.Models;
using System.ComponentModel.DataAnnotations;


public class Student
{
    public int Id {get; set;}

    [Required(ErrorMessage ="Name is required")]
    [StringLength(50, ErrorMessage ="Name cannot exceed 50 characters")]
    public string Name {get;set;} = string.Empty;

    [EmailAddress(ErrorMessage ="Enter a valid Email Address")]
    public string Email {get; set;} = string.Empty;

    [Range(1, 100, ErrorMessage ="Age Must betweeen 1 and 100")]
    public int Age {get; set;}
}