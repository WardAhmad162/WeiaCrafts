using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeiaCraftsApplication.DTOs;
using WeiaCraftsApplication.Interfaces;
using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsAPI.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    /// <summary>
    /// Retrieves all regular (non-kid) courses.
    /// </summary>
    /// <returns>List of CourseDto</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return Ok(courses);
    }

    /// <summary>
    /// Retrieves a specific course by ID.
    /// </summary>
    /// <param name="id">Course ID</param>
    /// <returns>CourseDto if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CourseDto>> GetCourseById(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
            return NotFound();
        return Ok(course);
    }

    /// <summary>
    /// Retrieves all kid-friendly courses.
    /// </summary>
    /// <returns>List of CourseDto</returns>
    [HttpGet("kids")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetKidFriendlyCourses()
    {
        var courses = await _courseService.GetKidFriendlyCoursesAsync();
        return Ok(courses);
    }

    /// <summary>
    /// Retrieves courses by category.
    /// </summary>
    /// <param name="categoryName">Category name (e.g., Art, Pottery)</param>
    /// <returns>List of CourseDto</returns>
    [HttpGet("category/{categoryName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByCategory(string categoryName)
    {
        try
        {
            var courses = await _courseService.GetCoursesByCategoryAsync(categoryName);
            return Ok(courses);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves courses using dynamic filters, sorting, and pagination.
    /// </summary>
    /// <param name="filterOptions">Filter, sort, and pagination options</param>
    /// <returns>Paged list of CourseDto</returns>
    [HttpGet("filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByFilter([FromQuery] CourseFilterOptionsDto filterOptions)
    {
        var courses = await _courseService.GetCoursesByFilterAsync(filterOptions);
        return Ok(courses);
    }

    /// <summary>
    /// Creates a new course. Trainer only.
    /// </summary>
    /// <param name="createCourseDto">Course details</param>
    /// <returns>Created CourseDto</returns>
    [Authorize(Roles = "Trainer")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto createCourseDto)
    {
        var createdCourse = await _courseService.CreateCourseAsync(createCourseDto);
        return CreatedAtAction(nameof(GetCourseById), new { id = createdCourse.Id }, createdCourse);
    }

    /// <summary>
    /// Updates an existing course. Trainer only.
    /// </summary>
    /// <param name="id">Course ID</param>
    /// <param name="updateCourseDto">Updated course details</param>
    [Authorize(Roles = "Trainer")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCourse(int id, UpdateCourseDto updateCourseDto)
    {
        var success = await _courseService.UpdateCourseAsync(id, updateCourseDto);
        if (!success)
            return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Deletes a course. Trainer only.
    /// </summary>
    /// <param name="id">Course ID</param>
    [Authorize(Roles = "Trainer")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var success = await _courseService.DeleteCourseAsync(id);
        if (!success)
            return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Retrieves the price of a course converted to the specified currency.
    /// </summary>
    /// <param name="id">Course ID</param>
    /// <param name="currency">Currency code (e.g., USD, EUR)</param>
    /// <returns>AmountWithCurrency</returns>
    [HttpGet("{id}/price/{currency}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AmountWithCurrency>> GetCoursePriceInCurrency(int id, string currency)
    {
        try
        {
            var price = await _courseService.GetCoursePriceInUserCurrency(id, currency.ToUpper());
            return Ok(price);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while converting currency.");
        }
    }
}
