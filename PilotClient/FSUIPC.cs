using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSUIPC;

namespace PilotClient
{
    class FSUIPC
    {

        public double Longitude;

        public double Latitude;

        public Double Compass;

        public double GroundSpeed;

        public double Altitude;

        public short Squawk;

        public static FSUIPC GetCurrent()
        {
            FSUIPC result = new FSUIPC();

            try
            {
                FSUIPCConnection.Process();
            }
            catch (Exception crap)
            {
                return null;
            }

            result.Latitude = FSUIPCOffsets.latitude.Value * (90.0 / (10001750.0 * 65536.0 * 65536.0));
            result.Longitude = FSUIPCOffsets.longitude.Value * (360.0 / (65536.0 * 65536.0 * 65536.0 * 65536.0));
            result.Compass = FSUIPCOffsets.compass.Value;
            result.GroundSpeed = (FSUIPCOffsets.groundspeed.Value / 65536) * 1.94384449;
            result.Altitude = (FSUIPCOffsets.altitude.Value * 3.2808399);
            result.Squawk = FSUIPCOffsets.squawk.Value;

            return result;
        }

        class FSUIPCOffsets
        {
            static public Offset<long> longitude = new Offset<long>(0x0568);
            static public Offset<long> latitude = new Offset<long>(0x0560);
            static public Offset<Double> compass = new Offset<double>(0x02CC);
            static public Offset<int> groundspeed = new Offset<int>(0x02B4);
            static public Offset<Double> altitude = new Offset<Double>(0x6020);
            static public Offset<short> squawk = new Offset<short>(0x0354);
        }
    }
}

