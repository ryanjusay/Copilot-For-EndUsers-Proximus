// representing historical pilots from 1903-1910 based
// on actual early aviators
public class Pilot
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BirthYear { get; set; }
    public double Weight { get; set; } // Important for early aviation constraints
    public double TotalFlightHours { get; set; }
    public string AircraftType { get; set; }
}
