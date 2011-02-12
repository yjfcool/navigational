using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoFramework;

namespace Core.NavigationElements
{
    public class Services
    {
        private readonly Distance _SimplifyingDistance = new Distance(50, DistanceUnit.Meters);

        public Services()
        {
        }

        public ITrack TrackSimplifier(ITrack track)
        {
            List<ITrackSegment> simplifiedSegmnet = new List<ITrackSegment>();
            foreach (ITrackSegment segment in track.TrackSegments)
            {
                simplifiedSegmnet.Add(SimplifySegment(segment));
            }
            track.TrackSegments.Clear();
            foreach (ITrackSegment segmnet in simplifiedSegmnet)
            {
                track.TrackSegments.Add(segmnet);
            }
            return track;
        }

        private ITrackSegment SimplifySegment(ITrackSegment segment)
        {
            GeoFramework.Position prevPosition = new Position();
            List<IWayPoint> wayPointsToRemove = new List<IWayPoint>();
            foreach (IWayPoint wayPoint in segment.SegmentWaypoints)
            {
                Position position = new Position(wayPoint.Latitude.ToString(), wayPoint.Longitude.ToString());

                if (position.DistanceTo(prevPosition) < _SimplifyingDistance)
                {
                    wayPointsToRemove.Add(wayPoint);

                }
                else
                {
                    prevPosition = position;
                }
                
            }

            foreach (IWayPoint wayPoint in wayPointsToRemove)
            {
                segment.SegmentWaypoints.Remove(wayPoint);
            }
            return segment;

        }
    }
}
