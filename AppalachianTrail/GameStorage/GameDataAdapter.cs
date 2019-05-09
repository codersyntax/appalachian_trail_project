using GameStorage.GameValues;
using GameStorage.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace GameStorage
{
    public class GameDataAdapter : IGameDataAdapter
    {
        private List<TrailSegment> m_TrailSegmentData;

        private List<Location> m_LocationData;

        public List<TrailSegment> GetMap()
        {
            if(m_LocationData == null)
            {
                BuildLocations();
            }
            if(m_TrailSegmentData == null)
            {
                BuildTrailSegments();
            }
            return m_TrailSegmentData;
        }

        private void BuildTrailSegments()
        {
            m_TrailSegmentData = new List<TrailSegment>();
            ConvertTrailSegmentDataToTrailSegments(ReadTrailSegmentDataFile());
        }

        private void BuildLocations()
        {
            m_LocationData = new List<Location>();
            ConvertLocationDataToLocation(ReadLocationDataFile());
        }

        private string[] ReadTrailSegmentDataFile()
        {
            return File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "TrailSegmentData.txt"));
        }

        private string[] ReadLocationDataFile()
        {
            return File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "LocationData.txt"));
        }

        public void WriteHighScore(string hikername, int highscore)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "HighScoreData.txt");
            if (File.Exists(path))
            {
                File.AppendAllText(path, hikername + "/" + highscore.ToString() + "\n");
            }


        }
        public string[] ReadHighScoreDataFile()
        {
            return File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "HighScoreData.txt"));

        }

            private void ConvertTrailSegmentDataToTrailSegments(string[] trailSegmentDataArray)
        {
            foreach (string trailSegmentDataInfo in trailSegmentDataArray)
            {
                string[] trailSegmentDataInfoArray = trailSegmentDataInfo.Split('/');
                Location firstLocation = m_LocationData.Find(loc => loc.Name == trailSegmentDataInfoArray[0]);
                Location secondLocation = m_LocationData.Find(loc => loc.Name == trailSegmentDataInfoArray[1]);
                int distance = Int32.Parse(trailSegmentDataInfoArray[2]);
                TrailSegment trailSegment = new TrailSegment(firstLocation, secondLocation, distance);
                m_TrailSegmentData.Add(trailSegment);
            }
        }

        private void ConvertLocationDataToLocation(string[] locationDataArray)
        {
            foreach (string locationDataInfo in locationDataArray)
            {
                string[] locationDataInfoArray = locationDataInfo.Split('/');
                Weather[] locationWeather = BuildLocationWeather(locationDataInfoArray);
                Location location = new Location(locationDataInfoArray[0], locationWeather);
                m_LocationData.Add(location);
            }
        }

        private Weather[] BuildLocationWeather(string[] locationDataInfo)
        {
            Weather[] locationMonthlyWeather = new Weather[12];
            for (int i = 1; i < locationDataInfo.Length; i++)
            {
                bool parseSuccessful = Enum.TryParse(locationDataInfo[i], out Weather weatherObj);
                if (parseSuccessful)
                {
                    locationMonthlyWeather[i - 1] = weatherObj;
                }
                else
                {
                    throw new Exception();
                }
            }
            return locationMonthlyWeather;
        }
    }
}
