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
    public class MaterialsController : ControllerBase
    {
        private readonly MaterialContext _context;

        public MaterialsController(MaterialContext context)
        {
            _context = context;
        }

        // GET: api/Materials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialDTO>>> GetMaterials()
        {
            return await _context.Materials
                .Select(x => MatToDTO(x))
                .ToListAsync();
        }

        // GET: api/Materials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialDTO>> GetMaterial(long id)
        {
            var material = await _context.Materials.FindAsync(id);

            if (material == null)
            {
                return NotFound();
            }

            return MatToDTO(material);
        }

        // PUT: api/Materials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaterial(long id, MaterialDTO materialDTO)
        {
            if (id != materialDTO.Id)
            {
                return BadRequest();
            }
            
            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            material.MatName = materialDTO.MatName;
            material.MatSource = materialDTO.MatSource;
            material.MatSourceURL = materialDTO.MatSourceURL;
            material.MatCost = materialDTO.MatCost;
            material.MatImage = materialDTO.MatImage;
            material.IsSelected = materialDTO.IsSelected;
           
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!MaterialExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Materials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MaterialDTO>> CreateMaterial(MaterialDTO materialDTO)
        {
            var material = new Material()
            {
                MatName = materialDTO.MatName,
                MatSource = materialDTO.MatSource,
                MatSourceURL = materialDTO.MatSourceURL,
                MatCost = materialDTO.MatCost,
                MatImage = materialDTO.MatImage,
                IsSelected = materialDTO.IsSelected
            };

            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetMaterial),
                new { id = materialDTO.Id }, 
                MatToDTO(material));
        }

        // DELETE: api/Materials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(long id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaterialExists(long id)
        {
            return _context.Materials.Any(e => e.Id == id);
        }

        private static MaterialDTO MatToDTO(Material Material) =>
            new MaterialDTO
            {
                Id = Material.Id,
                MatName = Material.MatName,
                MatSource = Material.MatSource,
                MatSourceURL = Material.MatSourceURL,
                MatCost = Material.MatCost,
                MatImage = Material.MatImage,
                IsSelected = Material.IsSelected
            };
    }
}
