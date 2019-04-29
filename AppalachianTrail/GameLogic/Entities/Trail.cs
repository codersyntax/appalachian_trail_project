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
            List<string[]> TrailData = new List<string[]>();
            string[] firstTrailSegment = new string[] { "Maine", "Virginia", "243" };
            string[] secondTrailSegment = new string[] { "Virginia", "Tennessee", "42543" };
            string[] thirdTrailSegment = new string[] { "Tennessee", "Georgia", "43243" };
            TrailData.Add(firstTrailSegment);
            TrailData.Add(secondTrailSegment);
            TrailData.Add(thirdTrailSegment);

            foreach (var segment in TrailData)
            {
                AddNextLocationStop(segment[0], segment[1], segment[2]);
            }
        }

        private void AddNextLocationStop(string startLocation, string nextLocation, string distanceToNextLocation)
        {
            Map.Add(new TrailSegment(new Location(startLocation), new Location(nextLocation), Int32.Parse(distanceToNextLocation)));
        }
    }
}
