﻿using Microsoft.FlightSimulator.SimConnect;
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

        public enum EVENTS
        {
            PITOT_TOGGLE,
            FLAPS_INC,
            FLAPS_DEC,
            FLAPS_UP,
            FLAPS_DOWN,
            REQUEST_AI_SET_SLEW,
            REQUEST_SET_SLEW_AHEAD,
            REQUEST_SET_SLEW_ALT,
            REQUEST_SET_SLEW_HDG,
            REQUEST_SET_SLEW_BANK,
            REQUEST_SET_SLEW_PITCH,
            REQUEST_SET_SLEW_SIDE,
            REQUEST_AI_RELEASE,
            SIMCONNECT_GROUP_PRIORITY_HIGHEST

        };

        enum NOTIFICATION_GROUPS
        {
            GROUP0,
        }

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

        public void CreateAircraft(string callsign, double latitude, double longitude, double altitude, string type)
        {
            simconnect.AICreateNonATCAircraft(type, callsign, new SIMCONNECT_DATA_INITPOSITION() {
                Latitude = latitude,
                Longitude = longitude,
                Altitude = altitude,
                Pitch = -0,
                Bank = -0,
                Heading = 270,
                OnGround = 1,
                Airspeed = 0
            }, EVENTS.FLAPS_DEC);
        }

        public void MoveAITrafficEvents()
        {
            simconnect.MapClientEventToSimEvent(EVENTS.REQUEST_AI_SET_SLEW, "SLEW_ON");

            simconnect.MapClientEventToSimEvent(EVENTS.REQUEST_SET_SLEW_AHEAD, "AXIS_SLEW_AHEAD_SET");

            simconnect.MapClientEventToSimEvent(EVENTS.REQUEST_SET_SLEW_ALT, "AXIS_SLEW_ALT_SET");

            simconnect.MapClientEventToSimEvent(EVENTS.REQUEST_SET_SLEW_HDG, "AXIS_SLEW_HEADING_SET");

            simconnect.MapClientEventToSimEvent(EVENTS.REQUEST_SET_SLEW_BANK, "AXIS_SLEW_BANK_SET");

            simconnect.MapClientEventToSimEvent(EVENTS.REQUEST_SET_SLEW_PITCH, "AXIS_SLEW_PITCH_SET");

            simconnect.MapClientEventToSimEvent(EVENTS.REQUEST_SET_SLEW_SIDE, "AXIS_SLEW_SIDEWAYS_SET");
        }

        public void SetSlewAITraffic()
        {
            simconnect.AIReleaseControl(1, EVENTS.REQUEST_AI_RELEASE);

            simconnect.TransmitClientEvent(1, EVENTS.REQUEST_AI_SET_SLEW, 1, EVENTS.SIMCONNECT_GROUP_PRIORITY_HIGHEST, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
        }

        void SlewArround(uint bank, uint pitch, uint hdg, uint ahead, uint alt, uint side)
        {
            simconnect.TransmitClientEvent(1, EVENTS.REQUEST_SET_SLEW_BANK, bank, NOTIFICATION_GROUPS.GROUP0, SIMCONNECT_EVENT_FLAG.DEFAULT);

            simconnect.TransmitClientEvent(1, EVENTS.REQUEST_SET_SLEW_PITCH, pitch, NOTIFICATION_GROUPS.GROUP0, SIMCONNECT_EVENT_FLAG.DEFAULT);

            simconnect.TransmitClientEvent(1, EVENTS.REQUEST_SET_SLEW_HDG, hdg, NOTIFICATION_GROUPS.GROUP0, SIMCONNECT_EVENT_FLAG.DEFAULT);

            simconnect.TransmitClientEvent(1, EVENTS.REQUEST_SET_SLEW_AHEAD, ahead, NOTIFICATION_GROUPS.GROUP0, SIMCONNECT_EVENT_FLAG.DEFAULT);

            simconnect.TransmitClientEvent(1, EVENTS.REQUEST_SET_SLEW_ALT, alt, NOTIFICATION_GROUPS.GROUP0, SIMCONNECT_EVENT_FLAG.DEFAULT);

            simconnect.TransmitClientEvent(1, EVENTS.REQUEST_SET_SLEW_SIDE, side, NOTIFICATION_GROUPS.GROUP0, SIMCONNECT_EVENT_FLAG.DEFAULT);
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

                // subscribe to pitot heat switch toggle 
                simconnect.MapClientEventToSimEvent(EVENTS.PITOT_TOGGLE, "PITOT_HEAT_TOGGLE");
                simconnect.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.GROUP0, EVENTS.PITOT_TOGGLE, false);

                // subscribe to all four flaps controls 
                simconnect.MapClientEventToSimEvent(EVENTS.FLAPS_UP, "FLAPS_UP");
                simconnect.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.GROUP0, EVENTS.FLAPS_UP, false);
                simconnect.MapClientEventToSimEvent(EVENTS.FLAPS_DOWN, "FLAPS_DOWN");
                simconnect.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.GROUP0, EVENTS.FLAPS_DOWN, false);
                simconnect.MapClientEventToSimEvent(EVENTS.FLAPS_INC, "FLAPS_INCR");
                simconnect.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.GROUP0, EVENTS.FLAPS_INC, false);
                simconnect.MapClientEventToSimEvent(EVENTS.FLAPS_DEC, "FLAPS_DECR");
                simconnect.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.GROUP0, EVENTS.FLAPS_DEC, false);

                // set the group priority 
                simconnect.SetNotificationGroupPriority(NOTIFICATION_GROUPS.GROUP0, SimConnect.SIMCONNECT_GROUP_PRIORITY_HIGHEST);

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
    }
}
