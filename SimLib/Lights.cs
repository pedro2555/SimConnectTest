using System.Runtime.InteropServices;

namespace SimLib
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class Lights
    {
        public byte lights;
    }

    public enum AllLights
    {
        Nav = 0x0001,
        Beacon = 0x0002,
        Landing = 0x0004,
        Taxi = 0x0008,
        Strobe = 0x0010,
        Panel = 0x0020,
        Recognition = 0x0040,
        Wing = 0x0080,
        Logo = 0x0100,
        Cabin = 0x0200,
    }
}
