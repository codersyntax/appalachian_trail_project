using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStorage
{
    class GameDataReader
    {
        public static List<List<string>> BuildLocationData()
        {
            string[] lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "LocationData.txt"));
            List<List<string>> completeLocationData = new List<List<string>>();
            
            foreach (string line in lines)
            {
                List<string> specificLocationData = new List<string>();
                var splitLine = line.Split('/');
                foreach (var locationPiece in splitLine)
                {
                    specificLocationData.Add(locationPiece);
                }
                completeLocationData.Add(specificLocationData);
            }
            return completeLocationData;
        }
        public static List<List<string>> BuildTrailSegmentData()
        {
            string[] Segments = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "TrailSegmentData.txt"));
            List<List<string>> completeSegmentData = new List<List<string>>();
            foreach (string line in Segments)
            {
                List<string> specificSegmentData = new List<string>();
                var splitSegments = line.Split('/');
                foreach (var segmentPiece in splitSegments)
                {
                    specificSegmentData.Add(segmentPiece);
                }
                completeSegmentData.Add(specificSegmentData);
            }
            return completeSegmentData;
        }
        
    }
}
