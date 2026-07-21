using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

[ApiController]
[Route("[controller]")]
public class FlightsController : ControllerBase
{
    private readonly ILogger<FlightsController> _logger;

    private List<Flight> Flights = new List<Flight>
    {
        new Flight
        {
            Id = 1,
            FlightNumber = "WB001",
            Origin = "Kitty Hawk, NC",
            Destination = "Manteo, NC",
            DepartureTime = new DateTime(1903, 12, 17, 10, 35, 0),
            ArrivalTime = new DateTime(1903, 12, 17, 10, 35, 0).AddMinutes(12),
            Status = FlightStatus.Scheduled,
            FuelRange = 100,
            FuelTankLeak = false,
            FlightLogSignature = "171203-DEP-ARR-WB001",
            AerobaticSequenceSignature = "L4B-H2C-R3A-S1D-T2E"
        },
        // Second ever flight of the Wright Brothers
        new Flight
        {
            Id = 2,
            FlightNumber = "WB002",
            Origin = "Kitty Hawk, NC",
            Destination = "Manteo, NC",
            DepartureTime = new DateTime(1903, 12, 17, 10, 35, 0),
            ArrivalTime = new DateTime(1903, 12, 17, 10, 35, 0).AddMinutes(12),
            Status = FlightStatus.Scheduled,
            FuelRange = 100,
            FuelTankLeak = false,
            FlightLogSignature = "171203-DEP-ARR-WB002",
            AerobaticSequenceSignature = "L1A-H1B-R1C-T1E"
        },
        // This is the first Wright Brothers plane that crashed.
        new Flight
        {
            Id = 3,
            FlightNumber = "WB003",
            Origin = "Fort Myer, VA",
            Destination = "Fort Myer, VA",
            DepartureTime = new DateTime(1908, 9, 17, 10, 35, 0),
            ArrivalTime = new DateTime(1908, 9, 17, 10, 35, 0).AddMinutes(12),
            Status = FlightStatus.Scheduled,
            FuelRange = 100,
            // The cause of the crash was NOT a fuel tank leak, but we will pretend it was
            FuelTankLeak = true,
            FlightLogSignature = "170908-DEP-ARR-WB003",
            AerobaticSequenceSignature = "L2A-H2B-R2C"
        },

    };

    public FlightsController(ILogger<FlightsController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Creates a new flight and stores it in memory.
    /// </summary>
    /// <param name="flight">The flight payload to create.</param>
    /// <returns>The created flight with a location header for retrieval.</returns>
    [HttpPost]
    public ActionResult<Flight> Post([FromBody] Flight flight)
    {
        _logger.LogInformation("POST ✈✈✈ NO PARAMS ✈✈✈");

        Flights.Add(flight);

        return CreatedAtAction(nameof(GetById), new { id = flight.Id }, flight);
    }

    /// <summary>
    /// Retrieves a flight by identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the flight.</param>
    /// <returns>The matching flight when found; otherwise a not-found response.</returns>
    [HttpGet("{id}")]
    public ActionResult<Flight> GetById(int id)
    {
        _logger.LogInformation($"GET ✈✈✈ {id} ✈✈✈");

        var flight = Flights.Find(f => f.Id == id);

        if (flight == null)
        {
            return NotFound();
        }

        return Ok(flight);
    }

    /// <summary>
    /// Updates the status of a flight after validating allowed state transitions.
    /// </summary>
    /// <param name="id">The identifier of the flight to update.</param>
    /// <param name="newStatus">The target status for the flight.</param>
    /// <returns>An OK response when status changes; otherwise a bad-request or not-found response.</returns>
    [HttpPost("{id}/status")]
    public ActionResult UpdateFlightStatus(int id, FlightStatus newStatus)
    {
        var flight = Flights.Find(f => f.Id == id);
        if (flight != null)
        {
            switch (newStatus)
            {
                case FlightStatus.Boarding:
                    if (DateTime.Now > flight.DepartureTime)
                    {
                        return BadRequest("Cannot board, past departure time.");
                    }

                    break;

                case FlightStatus.Departed:
                    if (flight.Status != FlightStatus.Boarding)
                    {
                        return BadRequest("Flight must be in 'Boarding' status before it can be 'Departed'.");
                    }

                    break;

                case FlightStatus.InAir:
                    if (flight.Status != FlightStatus.Departed)
                    {
                        return BadRequest("Flight must be in 'Departed' status before it can be 'In Air'.");
                    }
                    break;

                case FlightStatus.Landed:
                    if (flight.Status != FlightStatus.InAir)
                    {
                        return BadRequest("Flight must be in 'In Air' status before it can be 'Landed'.");
                    }

                    break;

                case FlightStatus.Cancelled:
                    if (DateTime.Now > flight.DepartureTime)
                    {
                        return BadRequest("Cannot cancel, past departure time.");
                    }
                    break;

                case FlightStatus.Delayed:
                    if (flight.Status == FlightStatus.Cancelled)
                    {
                        return BadRequest("Cannot delay, flight is cancelled.");
                    }
                    break;

                default:
                    // Handle other statuses or unknown status
                    return BadRequest("Unknown or unsupported flight status.");
            }

            flight.Status = newStatus;

            return Ok($"Flight status updated to {newStatus}.");
        }
        else
        {
            return NotFound("Flight not found.");
        }
    }


    /// <summary>
    /// Simulates a flight over the provided distance and consumes fuel per step.
    /// </summary>
    /// <param name="id">The identifier of the flight to simulate.</param>
    /// <param name="flightLength">The number of distance units to simulate.</param>
    /// <returns>An OK response when the simulation completes.</returns>
    [HttpPost("{id}/takeFlight/{flightLength}")]
    public ActionResult takeFlight(int id, int flightLength)
    {
        var flight = Flights.Find(f => f.Id == id);

        for (int i = 0; i < flightLength; i++)
        {
            if (flight.FuelRange == 0)
            {
                throw new Exception("Plane crashed, due to lack of fuel");
            }
            else
            {
                var fuelConsumption = 0;
                if (flight.FuelTankLeak)
                {
                    fuelConsumption = 2;
                }


                flight.FuelRange -= fuelConsumption;
            }
        }

        return Ok($"Flight took off and flew {flightLength} kilometers/miles.");
    }

    /// <summary>
    /// Simulates a lightning strike scenario for a flight.
    /// </summary>
    /// <param name="id">The identifier of the impacted flight.</param>
    /// <returns>An OK response when recovery succeeds.</returns>
    [HttpPost("{id}/lightningStrike")]
    public ActionResult lightningStrike(int id)
    {
        // Lightning caused recursion on an inflight instrument
        lightningStrike(id);

        return Ok($"Recovers from lightning strike.");
    }

    /// <summary>
    /// Runs a CPU-intensive prime computation to simulate aerodynamic calculations.
    /// </summary>
    /// <param name="id">The identifier of the flight context.</param>
    /// <returns>An OK response when calculations complete.</returns>
    [HttpPost("{id}/calculateAerodynamics")]
    public ActionResult calculateAerodynamics(int id)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        List<int> primes = CalculatePrimes(2, 300000);

        stopwatch.Stop();
        Console.WriteLine($"Found {primes.Count} prime numbers.");
        Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds / 1000.0} seconds");

        return Ok($"Calculated aerodynamics.");
    }

    /// <summary>
    /// Calculates all prime numbers within an inclusive range.
    /// </summary>
    /// <param name="start">The start of the range.</param>
    /// <param name="end">The end of the range.</param>
    /// <returns>A list containing all prime numbers in the range.</returns>
    public static List<int> CalculatePrimes(int start, int end)
    {
        List<int> primes = new List<int>();
        for (int number = start; number <= end; number++)
        {
            if (IsPrime(number))
            {
                primes.Add(number);
            }
        }
        return primes;
    }

    /// <summary>
    /// Determines whether the provided number is prime.
    /// </summary>
    /// <param name="number">The number to evaluate.</param>
    /// <returns><c>true</c> when the number is prime; otherwise <c>false</c>.</returns>
    public static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        for (int i = 2; i < number; i++) // Inefficient check for prime numbers
        {
            if (number % i == 0) return false;
        }
        return true;
    }
}
