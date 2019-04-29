namespace GameLogic.Entities
{
    public class TrailSegment
    {
        private Location m_StartLocation;

        private Location m_EndLocation;

        private int m_Distance;

        public TrailSegment(Location start, Location end, int distance)
        {
            m_StartLocation = start;
            m_EndLocation = end;
            m_Distance = distance;
        }

        public int Distance()
        {
            return m_Distance;
        }

        public Location StartOfTrailSegment()
        {
            return m_StartLocation;
        }

        public Location EndOfTrailSegment()
        {
            return m_EndLocation;
        }
    }
}
