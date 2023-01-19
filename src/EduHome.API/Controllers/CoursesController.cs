﻿using EduHome.Business.DTOs.Courses;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduHome.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
            var courses= await _courseService.FindAllAsync();
            return Ok(courses); 
                //return StatusCode((int)HttpStatusCode.OK, courses);

            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var course = await _courseService.FindByIdAsync(id);
                return Ok(course);  
            }
            catch (NotFoundException ex)
            {

               return NotFound(ex.Message);
            }
            catch(FormatException ex) 
            {
                return BadRequest(ex.Message);  
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("searchByName/{name}")]
        public async Task<IActionResult>GetByName(string name)
        {
            try
            {
              var result=  await _courseService.FindByConditionAsync(n => n.Name!=null? n.Name.Contains(name):true);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError); 
            }
        }


        [HttpPost("")]
        public async Task<IActionResult> Post(CoursePostDto course)
        {
            try
            {
            await _courseService.CreateAsync(course);
            return StatusCode((int)HttpStatusCode.Created);

            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put( int id,CourseUpdateDto course)
        {
            try
            {
                await _courseService.UpdateAsync(id,course);
                return NoContent();
            }
            catch (BadRequestException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception) 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

    }
}
