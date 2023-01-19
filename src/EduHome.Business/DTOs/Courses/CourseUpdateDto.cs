using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.DTOs.Courses;

public class CourseUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int? Id { get; set; }
}
