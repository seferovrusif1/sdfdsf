﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twitter.Business.Dtos.TopicDtos;
using Twitter.Business.Exceptions.Common;
using Twitter.Business.Services.Interfaces;
<<<<<<< HEAD
using Twitter.Core.Entities;
=======
using Twitter.Core.Enums;
>>>>>>> 946e5f7a3622000852deb9beeea7f42957a071d9

namespace Twitter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        ITopicService _service { get; }

        public TopicsController(ITopicService service)
        {
            _service = service;
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
        [Authorize(Roles =nameof(Roles.Admin))]
        [HttpPost]
        public async Task<IActionResult> Post(TopicCreateDto dto)
        {
            try
            {
                await _service.CreateAsync(dto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (ExistException<Topic> ex)
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TopicUpdateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok();
        }
    }
}
