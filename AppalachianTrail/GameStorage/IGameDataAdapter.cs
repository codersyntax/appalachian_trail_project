using GameStorage.Model;
using System.Collections.Generic;

namespace GameStorage
{
    public interface IGameDataAdapter
    {
        List<TrailSegment> GetMap();
        void WriteHighScore(string hikername, int highscore);
        string[] ReadHighScoreDataFile();
    }
}
