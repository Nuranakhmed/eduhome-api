using AutoMapper;
using EduHome.Business.DTOs.Courses;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Core.Entities;
using EduHome.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Services.Implementations;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public CourseService(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }
    public async Task<List<CourseDto>> FindAllAsync()
    {
        var courses = await _courseRepository.FindAll().ToListAsync();
        var result = _mapper.Map<List<CourseDto>>(courses);
        return result;
    }

    public async  Task<List<CourseDto>> FindByConditionAsync(Expression<Func<Course, bool>> expression)
    {
      var courses= await  _courseRepository.FindByCondition(expression).ToListAsync();
       var result = _mapper.Map<List<CourseDto>>(courses);
        return result;
    }

    public async Task<CourseDto?> FindByIdAsync(int id)
    {
        var course=await _courseRepository.FindByIdAsync(id);
        if(course == null)
        {
            throw new NotFoundException("Not Found");
        }
        return _mapper.Map<CourseDto>(course);
    }

    public async Task CreateAsync(CoursePostDto course)
    {
        if (course == null) throw new ArgumentNullException();
        var newCourse= _mapper.Map<Course>(course);
       await _courseRepository.CreateAsync(newCourse);
        await _courseRepository.SaveAsync();

    }

    public async Task  UpdateAsync(int id,CourseUpdateDto course)
    {
        if (id != course.Id)
        {
            throw new BadRequestException("Please enter valid ID.");

        }
        var baseCourse= await _courseRepository.FindByIdAsync(id);  
        if (baseCourse == null)
        {
            throw new NotFoundException("Not Found.");
        }
        var updateCourse=_mapper.Map<Course>(baseCourse);
        _courseRepository.Update(updateCourse);
        await _courseRepository.SaveAsync();    
    }
    public void Delete(Course entity)
    {
        throw new BadRequestException("Please enter valid ID");
    }

}


