namespace CA.JsonColumns.Models;

public class Visits
{
    private Visits() { }

    private Visits(double latitude, double longitude, int count)
    {
        Latitude = latitude;
        Longitude = longitude;
        Count = count;
    }

    public static Visits Create(double latitude, double longitude, int count)
    {
        return new Visits(latitude, longitude, count);
    }

    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public int Count { get; private set; }
    public List<string>? Browsers { get; set; }

    public Visits WithBrowsers(List<string> browsers)
    {
        Browsers = browsers;
        return this;
    }
}