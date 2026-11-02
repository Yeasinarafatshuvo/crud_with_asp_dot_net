using crudAsp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crudAsp.Models;
namespace crudAsp.Controllers;

public class StudentController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentController(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> List()
    {
        var students = await _context.Students.ToListAsync();
        return View(students);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        if(!ModelState.IsValid)
        {
            return View(student);
        }

        _context.Students.Add(student);

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Student created successfully!";

        return RedirectToAction(nameof(List));

    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if(student == null)
        {
            return NotFound();
        }


        return View(student);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if(id != student.Id)
        {
            return BadRequest();
        }

        if(!ModelState.IsValid)
        {
            return View(student);
        }


        var existingStudent = await _context.Students.FindAsync(id);

        if(existingStudent == null)
        {
            return NotFound();
        }


        existingStudent.Name = student.Name;
        existingStudent.Email = student.Email;
        existingStudent.Age = student.Age;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(List));

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if(student == null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(List));
    }



}