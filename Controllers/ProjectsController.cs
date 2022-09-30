using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanIt.Models;

namespace PlanIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectContext _context;

        public ProjectsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            return await _context.Projects
                .Select(x => ProjectToDTO(x))
                .ToListAsync();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return ProjectToDTO(project);
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(long id, ProjectDTO projectDTO)
        {
            if (id != projectDTO.Id)
            {
                return BadRequest();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            project.ProjectName = projectDTO.ProjectName;
            project.ProjectType = projectDTO.ProjectType;
            project.PatternSource = projectDTO.PatternSource;
            project.PatternSourceURL = projectDTO.PatternSourceURL;
            project.PatternCost = projectDTO.PatternCost;
            project.ProjectThumbnail = projectDTO.ProjectThumbnail;
            project.DateAdded = projectDTO.DateAdded;
            project.DateStarted = projectDTO.DateStarted;
            project.DateCompleted = projectDTO.DateCompleted;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProjectExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> CreateProject(ProjectDTO projectDTO)
        {
            var project = new Project()
            {
                ProjectName = projectDTO.ProjectName,
                ProjectType = projectDTO.ProjectType,
                PatternSource = projectDTO.PatternSource,
                PatternSourceURL = projectDTO.PatternSourceURL,
                PatternCost = projectDTO.PatternCost,
                ProjectThumbnail = projectDTO.ProjectThumbnail,
                DateAdded = projectDTO.DateAdded,
                DateStarted = projectDTO.DateStarted,
                DateCompleted = projectDTO.DateCompleted
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProject),
                new { id = projectDTO.Id },
                ProjectToDTO(project));
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(long id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
        private static ProjectDTO ProjectToDTO(Project Project) =>
            new ProjectDTO
            {
                Id = Project.Id,
                ProjectName = Project.ProjectName,
                ProjectType = Project.ProjectType,  
                PatternSource = Project.PatternSource,
                PatternSourceURL = Project.PatternSourceURL,
                PatternCost = Project.PatternCost,
                ProjectThumbnail = Project.ProjectThumbnail,
                DateAdded = Project.DateAdded,
                DateStarted = Project.DateStarted,
                DateCompleted = Project.DateCompleted
            };
    }
}
