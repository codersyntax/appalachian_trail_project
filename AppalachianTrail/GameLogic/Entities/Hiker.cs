namespace GameLogic.Entities
{
    internal class Hiker
    {
        internal string Name { get; set; }

        internal Backpack Backpack { get; set; }

        internal Location CurrentLocation { get; set; }

        internal Hiker(string name, Location startLocation)
        {
            Name = name;

            CurrentLocation = startLocation;

            Backpack = new Backpack();
        }
    }
}
