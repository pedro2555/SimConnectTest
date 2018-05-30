using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimLib
{
    public class SimConnectForm : Form
    {
        // User-defined win32 event 
        const int WM_USER_SIMCONNECT = 0x0402;

        // SimConnect object 
        SimConnect simconnect = null;


        // Simconnect client will send a win32 message when there is 
        // a packet to process. ReceiveMessage must be called to 
        // trigger the events. This model keeps simconnect processing on the main thread. 

        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_USER_SIMCONNECT)
            {
                if (simconnect != null)
                {
                    simconnect.ReceiveMessage();
                }
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }

        private void closeConnection()
        {
            if (simconnect != null)
            {
                // Dispose serves the same purpose as SimConnect_Close() 
                simconnect.Dispose();
                simconnect = null;
            }
        }

        public async void openConnection()
        {
            while (simconnect == null)
            {
                try
                {
                    // the constructor is similar to SimConnect_Open in the native API 
                    simconnect = new SimConnect("SimLib.SimLibSimConnect", Handle, WM_USER_SIMCONNECT, null, 0);

                    initClientEvent();
                }
                catch (COMException)
                {
                    await Task.Delay(5000);
                }
            }
        }

        // Set up all the SimConnect related event handlers 
        private void initClientEvent()
        {
            try
            {
                // listen to connect and quit msgs 
                simconnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(simconnect_OnRecvOpen);
                simconnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(simconnect_OnRecvQuit);

                // listen to exceptions 
                simconnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(simconnect_OnRecvException);

                // listen to events 
                simconnect.OnRecvEvent += new SimConnect.RecvEventEventHandler(simconnect_OnRecvEvent);

                RegisterDataDefinitions();
            }
            catch (COMException ex)
            {
                throw ex;
            }
        }

        void simconnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
        }

        // The case where the user closes Prepar3D 
        void simconnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            closeConnection();
        }

        void simconnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
        }

        public delegate void SimConnectEvent(SimConnect sender, SIMCONNECT_RECV_EVENT recEvent);
        public event SimConnectEvent OnSimConnectEvent;
        private void simconnect_OnRecvEvent(SimConnect sender, SIMCONNECT_RECV_EVENT recEvent)
        {
            OnSimConnectEvent(sender, recEvent);
        }

        // The case where the user closes the client 
        private void SimConnectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeConnection();
        }

        public void RegisterDataDefinitions()
        {
            // register data structures
            simconnect.AddToDataDefinition(
                DEFINITIONS.Compass,
                "PLANE HEADING DEGREES MAGNETIC",
                "degrees",
                SIMCONNECT_DATATYPE.INT32,
                0.0f,
                SimConnect.SIMCONNECT_UNUSED);
            simconnect.RegisterDataDefineStruct<Telemetry>(DEFINITIONS.Compass);

            simconnect.AddToDataDefinition(
               DEFINITIONS.Pitch,
               "PLANE PITCH DEGREES",
               "degrees",
               SIMCONNECT_DATATYPE.INT32,
               0.0f,
               SimConnect.SIMCONNECT_UNUSED);
            simconnect.RegisterDataDefineStruct<Telemetry>(DEFINITIONS.Pitch);

            simconnect.AddToDataDefinition(
                DEFINITIONS.Altitude,
                "PLANE ALTITUDE",
                "feet",
                SIMCONNECT_DATATYPE.INT32,
                0,
                SimConnect.SIMCONNECT_UNUSED);
            simconnect.RegisterDataDefineStruct<Telemetry>(DEFINITIONS.Altitude);

            simconnect.AddToDataDefinition(
                DEFINITIONS.Latitude,
                "PLANE LATITUDE",
                "degrees",
                SIMCONNECT_DATATYPE.FLOAT64,
                0.0f,
                SimConnect.SIMCONNECT_UNUSED);
            simconnect.RegisterDataDefineStruct<Telemetry>(DEFINITIONS.Latitude);

            simconnect.AddToDataDefinition(
                DEFINITIONS.Longitude,
                "PLANE LONGITUDE",
                "degrees",
                SIMCONNECT_DATATYPE.FLOAT64,
                0.0f,
                SimConnect.SIMCONNECT_UNUSED);
            simconnect.RegisterDataDefineStruct<Telemetry>(DEFINITIONS.Longitude);

            // register the main data event handler
            simconnect.OnRecvSimobjectDataBytype += Simconnect_OnRecvSimobjectDataBytype;

            simconnect.RequestDataOnSimObjectType(DATA_REQUESTS.Compass, DEFINITIONS.Compass, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
            simconnect.RequestDataOnSimObjectType(DATA_REQUESTS.Pitch, DEFINITIONS.Pitch, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
            simconnect.RequestDataOnSimObjectType(DATA_REQUESTS.Altitude, DEFINITIONS.Altitude, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
            simconnect.RequestDataOnSimObjectType(DATA_REQUESTS.Latitude, DEFINITIONS.Latitude, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
            simconnect.RequestDataOnSimObjectType(DATA_REQUESTS.Longitude, DEFINITIONS.Longitude, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
        }

        private void Simconnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            try
            {
                switch ((DATA_REQUESTS)data.dwRequestID)
                {
                    case DATA_REQUESTS.Compass:
                        Telemetry CurrentCompass = (Telemetry)data.dwData[0];
                        if (LastCompass.Heading != CurrentCompass.Heading)
                        {
                            LastCompass.Heading = CurrentCompass.Heading;
                            SimConnectHeadingChanged(sender, new CompassChangedEventArgs() { Heading = CurrentCompass.Heading });

                            // re-register SimConnect listener
                        }
                        simconnect.RequestDataOnSimObjectType(DATA_REQUESTS.Compass, DEFINITIONS.Compass, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                        break;

                    //case DATA_REQUESTS.Pitch:
                    //    Telemetry CurrentPitch = (Telemetry)data.dwData[0];
                    //    if (LastPitch.Pitch != CurrentPitch.Pitch)
                    //    {
                    //        LastPitch.Pitch = CurrentPitch.Pitch;
                    //        SimConnectPitchChanged(sender, new PitchChangedEventArgs() { Pitch = CurrentPitch.Pitch });

                    //        // re-register SimConnect listener
                    //    }
                    //    simconnect.RequestDataOnSimObjectType(DATA_REQUESTS.Pitch, DEFINITIONS.Pitch, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                    //    break;

                    //case DATA_REQUESTS.Latitude:
                    //    Telemetry CurrentLatitude = (Telemetry)data.dwData[0];
                    //    if (LastLatitude.Latitude != CurrentLatitude.Latitude)
                    //    {
                    //        LastLatitude.Latitude = CurrentLatitude.Latitude;
                    //        SimConnectLatitudeChanged(sender, new LatitudeChangedEventArgs() { Latitude = CurrentLatitude.Latitude });

                    //        // re-register SimConnect listener
                    //    }
                    //    simconnect.RequestDataOnSimObjectType(DATA_REQUESTS.Latitude, DEFINITIONS.Latitude, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                    //    break;

                    //case DATA_REQUESTS.Altitude:
                    //    Telemetry CurrentAltitude = (Telemetry)data.dwData[0];
                    //    if (LastAltitude.Altitude != CurrentAltitude.Altitude)
                    //    {
                    //        LastAltitude.Altitude = CurrentAltitude.Altitude;
                    //        SimConnectAltitudeChanged(sender, new AltitudeChangedEventArgs() { Altitude = CurrentAltitude.Altitude });

                    //        // re-register SimConnect listener
                    //    }
                    //    simconnect.RequestDataOnSimObjectType(DATA_REQUESTS.Altitude, DEFINITIONS.Altitude, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                    //    break;

                    //case DATA_REQUESTS.Longitude:
                    //    Telemetry CurrentLongitude = (Telemetry)data.dwData[0];
                    //    if (LastLongitude.Longitude != CurrentLongitude.Longitude)
                    //    {
                    //        LastLongitude.Longitude = CurrentLongitude.Longitude;
                    //        SimConnectLongitudeChanged(sender, new LongitudeChangedEventArgs() { Longitude = CurrentLongitude.Longitude });

                    //        // re-register SimConnect listener
                    //    }
                    //    simconnect.RequestDataOnSimObjectType(DATA_REQUESTS.Longitude, DEFINITIONS.Longitude, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                    //    break;
                    default:
                        break;
                }
            }
            catch (COMException ex)
            {
                throw ex;
            }
        }

        enum DEFINITIONS
        {
            Compass,
            Latitude,
            Longitude,
            Altitude,
            Pitch,
        }

        enum DATA_REQUESTS
        {
            Compass,
            Latitude,
            Longitude,
            Altitude,
            Pitch,
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct Telemetry
        {
            public int Heading;
            public double Latitude;
            public double Longitude;
            public int Altitude;
            public int Pitch;
        }

        public Telemetry LastCompass;
        public Telemetry LastLatitude;
        public Telemetry LastLongitude;
        public Telemetry LastAltitude;
        public Telemetry LastPitch;

        public class CompassChangedEventArgs : EventArgs
        {
            public int Heading
            { get; internal set; }
        }

        public class LatitudeChangedEventArgs : EventArgs
        {
            public double Latitude
            { get; internal set; }
        }

        public class LongitudeChangedEventArgs : EventArgs
        {
            public double Longitude
            { get; internal set; }
        }

        public class AltitudeChangedEventArgs : EventArgs
        {
            public int Altitude
            { get; internal set; }
        }

        public class PitchChangedEventArgs : EventArgs
        {
            public int Pitch
            { get; internal set; }
        }

        public event EventHandler SimConnectHeadingChanged;
        public event EventHandler SimConnectLatitudeChanged;
        public event EventHandler SimConnectLongitudeChanged;
        public event EventHandler SimConnectAltitudeChanged;
        public event EventHandler SimConnectPitchChanged;
    }    
}
