using GameStorage.GameValues;

namespace GameLogic.Entities
{
    public class Location
    {
        public string Name { get; set; }

        public Weather[] AverageWeatherForEachMonth { get; set; }

        public Location(string name, Weather[] weather)
        {
            Name = name;

            AverageWeatherForEachMonth = weather;
        }
    }
}
