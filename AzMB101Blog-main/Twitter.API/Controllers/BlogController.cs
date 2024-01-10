using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twitter.Business.Dtos.BlogDtos;
using Twitter.Business.Dtos.TopicDtos;
using Twitter.Business.Exceptions.Common;
using Twitter.Business.Services.Interfaces;
using Twitter.Core.Entities;

namespace Twitter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        IBlogService _service { get; }

        public BlogsController(IBlogService service)
        {
            _service = service;
        }
      
        [HttpPost]
        public async Task<IActionResult> Post(BlogCreateDto dto)
        {
            try
            {
                await _service.CreateAsync(dto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (ExistException<Blog> ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoveAsync(id);
            return Ok();
        }



        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
       
     
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BlogCreateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok();
        }

    }
}
