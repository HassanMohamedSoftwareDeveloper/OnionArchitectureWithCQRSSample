using Application.CQRS.Queries;
using Application.CQRS.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        #region Fields :
        private readonly IMediator mediator; 
        #endregion
        #region CTORS :
        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        } 
        #endregion
        #region Actions :
        #region Queries :
        [HttpGet("get-all-students")]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await mediator.Send(new GetAllStudentQuery()));
        }
        [HttpGet("get-student-by-id/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            return Ok(await mediator.Send(new GetStudentByIdQuery() { Id=id}));
        }
        #endregion
        #region Commands :
        [HttpPost("add-new-student")]
        public async Task<IActionResult> AddNewStudent([FromBody]CreateStudentCommand student)
        {
            var result = await mediator.Send(student);
            string url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/api/home/get-student-by-id/{result}";
            return Created(url, result);
        }
        [HttpPut("update-student-by-id/{id}")]
        public async Task<IActionResult> UpdateStudentById(int id,UpdateStudentCommand student)
        {
            if (id != student.Id) return BadRequest();
            return Ok(await mediator.Send(student));
        }
        [HttpDelete("delete-student-by-id/{id}")]
        public async Task<IActionResult> DeleteStudentById(int id)
        {
            return Ok(await mediator.Send(new DeleteStudentByIdCommand() { Id=id}));
        }
        #endregion
        #endregion
    }
}
