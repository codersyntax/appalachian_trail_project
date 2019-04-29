using GameStorage.GameValues;
using System;

namespace GameLogic.Entities
{
    internal class Hiker
    {
        internal string Name { get; set; }

        internal Occupation Occupation { get; set; }

        internal Backpack Backpack { get; set; }

        internal Location CurrentLocation { get; set; }

        internal DateTime CurrentDate { get; set; }

        internal Pace CurrentPace { get; set; }

        internal Ration CurrentFoodRation { get; set; }

        internal int Wallet { get; set; }

        internal Hiker(string name, Occupation occupation, Month startDate, Location startLocation)
        {
            Name = name;

            Occupation = occupation;

            CurrentLocation = startLocation;

            CurrentDate = new DateTime(2019, (int)startDate, 1, 8, 0, 0);

            CurrentPace = Pace.Steady;

            CurrentFoodRation = Ration.Filling;

            Backpack = new Backpack();
        }
    }
}
