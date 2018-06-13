using System.Runtime.InteropServices;

namespace SimLib
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class AircraftState
    {
        public double latitude;
        public double longitude;
        public double altitude;
        public double pitch;
        public double bank;
        public double heading;
        public uint airspeed;
    }
}
