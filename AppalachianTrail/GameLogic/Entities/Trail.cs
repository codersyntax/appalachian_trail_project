using GameStorage.GameValues;
using System;
using System.Collections.Generic;

namespace GameLogic.Entities
{
    public class Trail
    {
        public List<TrailSegment> Map = new List<TrailSegment>();

        public Trail()
        {
            BuildTrail();
        }

        public Location GetFirstTrailLocation()
        {
            return Map[0].StartOfTrailSegment();
        }

        public Location GetNextLocation(Location currentLocation)
         {
            foreach (TrailSegment segment in Map)
            {
                if (segment.StartOfTrailSegment() == currentLocation)
                {
                    return segment.EndOfTrailSegment();
                }
            }
            return null;
        }

        public int GetDistanceToNextLocation(Location currentLocation)
        {
            foreach (TrailSegment segment in Map)
            {
                if (segment.StartOfTrailSegment() == currentLocation)
                {
                    return segment.Distance();
                }
            }
            return 0;
        }

        private void BuildTrail()
        {
            //Testing out demo locations to make sure location traversal is working correctly, this section will eventually be removed and passed in from storage to build the trail data
            var firstLocation = new Location("Maine", new Weather[12] { Weather.Cold, Weather.Cold, Weather.Cold, Weather.Chilly, Weather.Chilly, Weather.Mild, Weather.Warm, Weather.Mild, Weather.Mild, Weather.Chilly, Weather.Cold, Weather.Cold });
            var secondLocation = new Location("Virginia", new Weather[12] { Weather.Cold, Weather.Cold, Weather.Cold, Weather.Chilly, Weather.Chilly, Weather.Mild, Weather.Warm, Weather.Mild, Weather.Mild, Weather.Chilly, Weather.Cold, Weather.Cold });
            var firstToSecondEdgeDistance = 214;
            AddNextLocationStop(firstLocation, secondLocation, firstToSecondEdgeDistance);

            var thirdLocation = new Location("Georgia", new Weather[12] { Weather.Cold, Weather.Chilly, Weather.Chilly, Weather.Warm, Weather.Hot, Weather.Hot, Weather.Hot, Weather.Warm, Weather.Warm, Weather.Mild, Weather.Chilly, Weather.Chilly });
            var secondToThirdEdgeDistance = 132;
            AddNextLocationStop(secondLocation, thirdLocation, secondToThirdEdgeDistance);
        }

        private void AddNextLocationStop(Location startLocation, Location nextLocation, int distanceToNextLocation)
        {
            Map.Add(new TrailSegment(startLocation, nextLocation, distanceToNextLocation));
        }
    }
}
