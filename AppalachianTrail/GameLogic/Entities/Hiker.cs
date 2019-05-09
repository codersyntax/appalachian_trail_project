using GameStorage.GameValues;
using GameStorage.Model;
using System;

namespace GameLogic.Entities
{
    public class Hiker
    {
        internal string Name { get; set; }

        internal Occupation Occupation { get; set; }

        internal Backpack Backpack { get; set; }

        internal Location CurrentLocation { get; set; }

        internal DateTime CurrentDate { get; set; }

        internal Pace CurrentPace { get; set; }

        internal Ration CurrentFoodRation { get; set; }

        internal HealthStatus CurrentHealthStatus { get; set; }

        internal int HealthMeter { get; set; }

        public int Wallet { get; set; }
        
        internal int DistanceToNextLocation { get; set; }

        internal int TotalMilesTraveled { get; set; }

        internal int GameScore { get; set; }

        internal Hiker(string name, Occupation occupation, Month startDate, Location startLocation)
        {
            Name = name;

            Occupation = occupation;

            Wallet = (int)Occupation;

            CurrentLocation = startLocation;

            CurrentDate = new DateTime(2019, (int)startDate, 1, 8, 0, 0);

            CurrentPace = Pace.Steady;

            CurrentFoodRation = Ration.Filling;

            CurrentHealthStatus = HealthStatus.Good;

            HealthMeter = 100;

            Backpack = new Backpack();

            TotalMilesTraveled = 0;

            GameScore = 0;
        }
    }
}
