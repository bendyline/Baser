﻿/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Runtime.CompilerServices;

namespace BL
{
    /// <summary>
    /// Helper functions for computing geopgrahic concepts.
    /// </summary>
     public class GeoUtilities
    {
        private const double EarthsRadius = 6371.009; // Earth's mean radius
        private const int MetersPerLatitudeDegree = 111133; // pi * 6,367,444 / 180

        public static GeoBoundingBox BoundingBoxFromLatLong(double latitude, double longitude, double distanceInKm)
        {
            Geopoint point = new Geopoint();

            point.Latitude = latitude;
            point.Longitude = longitude;

            return BoundingBoxFromPoint(point, distanceInKm);
        }

        public static GeoBoundingBox BoundingBoxFromPoint(Geopoint point, double distanceInKm)
        {
            Geopoint northwestPoint = GeoUtilities.GetDestination(point.Latitude, point.Longitude, distanceInKm, 315);
            Geopoint southeastPoint = GeoUtilities.GetDestination(point.Latitude, point.Longitude, distanceInKm, 135);

            GeoBoundingBox gbb = new GeoBoundingBox();

            gbb.NorthLatitude = northwestPoint.Latitude;
            gbb.WestLongitude = northwestPoint.Longitude;

            gbb.EastLongitude = southeastPoint.Longitude;
            gbb.SouthLatitude = southeastPoint.Latitude;

            return gbb;
        }


        public static Geopoint PointFromLatLong(double latitude, double longitude)
        {
            Geopoint gp = new Geopoint();

            gp.Longitude = longitude;
            gp.Latitude = latitude;

            return gp;
        }

        public static Geopoint PointFromString(String val)
        {
            val = val.Trim();

            if (!val.StartsWith("[") || !val.EndsWith("]"))
            {
                return null;
            }

            String[] vals = val.Split(',');

            if (vals.Length != 2)
            {
                return null;
            }

            Geopoint gp = new Geopoint();

            gp.Longitude = Double.Parse(vals[0]);
            gp.Latitude = Double.Parse(vals[1]);

            return gp;
        }

        public static String GetStringValue(Geopoint point)
        {
            return "[" + point.Longitude + ", " + point.Latitude + "]";
        }

        public double ConvertKmToLatitude(double km)
        {
            return (km * 1000) / MetersPerLatitudeDegree;
        }

        public static double ConvertToRadians(double degrees)
        {
            return (degrees * Math.PI) / 180;
        }

        public static double ConvertToDegrees(double radians)
        {
            return (radians * 180) / Math.PI;
        }

        public static double GetDistance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            double dLat = ConvertToRadians(latitude2 - latitude1);
            double dLon = ConvertToRadians(longitude2 - longitude1);

            latitude1 = ConvertToRadians(latitude1);
            latitude2 = ConvertToRadians(latitude2);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(latitude1) * Math.Cos(latitude2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthsRadius * c;
        }

        public static Geopoint GetDestination(double latitude1, double longitude1, double distanceInKm, double bearing)
        {
            double bearingRad = ConvertToRadians(bearing);

            Geopoint newLocation = new Geopoint();

            double angularDistance = distanceInKm / EarthsRadius;

            latitude1 = ConvertToRadians(latitude1);
            longitude1 = ConvertToRadians(longitude1);

            newLocation.Latitude = Math.Asin((Math.Sin(latitude1) * Math.Cos(angularDistance)) + (Math.Cos(latitude1) * Math.Sin(angularDistance) * Math.Cos(bearingRad)));
            newLocation.Longitude = longitude1 + Math.Atan2(Math.Sin(bearingRad) * Math.Sin(angularDistance) * Math.Cos(latitude1), Math.Cos(angularDistance) - Math.Sin(latitude1) * Math.Sin(newLocation.Latitude));

            newLocation.Longitude = (newLocation.Longitude + 3 * Math.PI) % (2 * Math.PI) - Math.PI;

            newLocation.Latitude = ConvertToDegrees(newLocation.Latitude);
            newLocation.Longitude = ConvertToDegrees(newLocation.Longitude);

            return newLocation;
        }


        public static int GetBearing(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            latitude1 = ConvertToRadians(latitude1);
            latitude2 = ConvertToRadians(latitude2);

            double dLon = ConvertToRadians(longitude2 - longitude1);

            double y = Math.Sin(dLon) * Math.Cos(latitude2);
            double x = Math.Cos(latitude1) * Math.Sin(latitude2) - Math.Sin(latitude1) * Math.Cos(latitude2) * Math.Cos(dLon);

            return (int)Math.Floor((ToDeg(Math.Atan2(y, x)) + 360) % 360);
        }

        public static double ToDeg(double number)
        {
            return number * 180 / Math.PI;
        }
    }
}
