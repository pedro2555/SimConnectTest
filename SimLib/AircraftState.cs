using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Runtime.InteropServices;

namespace SimLib
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class AircraftState
    {
        // this is how you declare a fixed size string 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public String title;
        public double latitude;
        public double longitude;
        public double altitude;
        public double pitch;
        public double bank;
        public double heading;
        public uint airspeed;
        public uint onGround;
        public uint gearPosition;
        public uint landingLight;
        public uint strobeLight;
        public uint beaconLight;
        public uint navLight;
        public uint taxiLight;
        public uint recognitionLight;
        public double flapsPosition;
    }
}
