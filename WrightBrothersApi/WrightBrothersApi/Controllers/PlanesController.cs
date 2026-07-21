using Microsoft.AspNetCore.Mvc;
using WrightBrothersApi.Models;

namespace WrightBrothersApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanesController : ControllerBase
    {
        private readonly ILogger<PlanesController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanesController"/> class.
        /// </summary>
        /// <param name="logger">The logger used for request diagnostics.</param>
        public PlanesController(ILogger<PlanesController> logger)
        {
            _logger = logger;
        }

        private static readonly List<Plane> Planes = new List<Plane>
        {
            new Plane
            {
                Id = 1,
                Name = "Wright Flyer",
                Year = 1903,
                Description = "The first powered aircraft.",
                RangeInKm = 12,
                Image = "wright-flyer.jpg"
            },
            new Plane
            {
                Id = 2,
                Name = "Wright Flyer II",
                Year = 1904,
                Description = "Original Flyer with better performance.",
                RangeInKm = 24,
                Image = "wright-flyer-ii.jpg"
            },
            new Plane
            {
                Id = 3,
                Name = "Wright Model A",
                Year = 1908,
                Description = "The first commercial airplane.",
                RangeInKm = 40,
                Image = "wright-model-a.jpg"
            },
            new Plane
            {
                Id = 4,
                Name = "Wright Model B",
                Year = 1910,
                Description = "Improved design with better control.",
                RangeInKm = 60,
                Image = "wright-model-b.jpg"
            },
            new Plane
            {
                Id = 5,
                Name = "Wright Model C",
                Year = 1912,
                Description = "Further refinement of the Wright design.",
                RangeInKm = 80,
                Image = "wright-model-c.jpg"
            },
            new Plane
            {
                Id = 6,
                Name = "Wright Model D",
                Year = 1914,
                Description = "Advanced model with enhanced stability.",
                RangeInKm = 100,
                Image = "wright-model-d.jpg"
            }
        };

        /// <summary>
        /// Retrieves all planes currently available in the in-memory store.
        /// </summary>
        /// <returns>A list of all planes.</returns>
        [HttpGet]
        public ActionResult<List<Plane>> GetAll()
        {
            _logger.LogInformation("GET all ✈✈✈ NO PARAMS ✈✈✈");

            return Ok(Planes);
        }

        /// <summary>
        /// Retrieves a single plane by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the plane.</param>
        /// <returns>The matching plane when found; otherwise a 404 response.</returns>
        [HttpGet("{id}")]
        public ActionResult<Plane> GetById(int id)
        {
            var plane = Planes.Find(p => p.Id == id);

            if (plane == null)
            {
                return NotFound();
            }

            return Ok(plane);
        }

        /// <summary>
        /// Adds a new plane to the in-memory store.
        /// </summary>
        /// <param name="plane">The plane to create.</param>
        /// <returns>The created plane with a location header for retrieval.</returns>
        [HttpPost]
        public ActionResult<Plane> Post(Plane plane)
        {
            if (plane == null)
            {
                return BadRequest();
            }

            Planes.Add(plane);

            return CreatedAtAction(nameof(GetById), new { id = plane.Id }, plane);
        }

        /// <summary>
        /// Replaces the full in-memory plane dataset with the provided list.
        /// </summary>
        /// <param name="planes">The complete set of planes to store.</param>
        /// <returns>An OK response when the setup operation completes.</returns>
        [HttpPost("setup")]
        public ActionResult SetupPlanesData(List<Plane> planes)
        {
            Planes.Clear();
            Planes.AddRange(planes);

            return Ok();
        }

        /// <summary>
        /// Updates an existing plane by identifier.
        /// </summary>
        /// <param name="id">The identifier of the plane to update.</param>
        /// <param name="plane">The updated plane payload.</param>
        /// <returns>The updated plane when successful; otherwise a validation or not-found response.</returns>
        [HttpPut("{id}")]
        public ActionResult<Plane> Put(int id, Plane plane)
        {
            if (plane == null)
            {
                return BadRequest();
            }

            if (plane.Id != 0 && plane.Id != id)
            {
                return BadRequest("Plane ID in body must match route ID.");
            }

            var index = Planes.FindIndex(p => p.Id == id);

            if (index == -1)
            {
                return NotFound();
            }

            plane.Id = id;
            Planes[index] = plane;

            return Ok(plane);
        }

        /// <summary>
        /// Deletes an existing plane by identifier.
        /// </summary>
        /// <param name="id">The identifier of the plane to delete.</param>
        /// <returns>No content when deleted; otherwise a not-found response.</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var plane = Planes.Find(p => p.Id == id);

            if (plane == null)
            {
                return NotFound();
            }

            Planes.Remove(plane);

            return NoContent();
        }


    }
}
