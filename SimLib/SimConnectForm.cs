using Microsoft.FlightSimulator.SimConnect;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimLib
{
    /// <summary>
    /// Extends Windows.Forms to provide a wrapped access to SimConnect
    /// </summary>
    public partial class SimConnectForm : Form
    {
        /// <summary>
        /// The user defined win32 event
        /// </summary>
        private const int WM_USER_SIMCONNECT = 0x0402;

        /// <summary>
        /// Internal SimConnect object
        /// </summary>
        private SimConnect simconnect = null;

        enum GROUPS
        {
            ALL,
            SIMCONNECT_GROUP_PRIORITY_HIGHEST
        }

        enum DEFINITIONS
        {
            Radios,
            Position,
            AiTraffic
        }

        enum DATA_REQUESTS
        {
            Radios,
            Position,
            AiTraffic,
            KEY_FREEZE_LATITUDE_LONGITUDE_SET
        }

        enum EVENTS
        {
            REQUEST_AI_SET_SLEW,
            REQUEST_SET_SLEW_HDG,
            REQUEST_AI_RELEASE
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
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

        /// <summary>
        /// Asynchronous wait for a listening SimConnect server.
        /// 
        /// Listen to SimConnectOpen event
        /// </summary>
        public async void OpenSimConnect()
        {
            while (simconnect == null)
            {
                try
                {
                    // the constructor is similar to SimConnect_Open in the native API 
                    simconnect = new SimConnect("SimLib.SimLibSimConnect", Handle, WM_USER_SIMCONNECT, null, 0);

                    RegisterEvents();
                    RegisterDataDefinitions();
                    RegisterPosition();
                }
                catch (COMException)
                {
                    await Task.Delay(5000);
                }
            }
        }

        /// <summary>
        /// Dispose the SimConnect object
        /// object.
        /// </summary>
        private void DisposeSimConnect()
        {
            if (simconnect != null)
            {
                simconnect.Dispose();
                simconnect = null;
            }
        }

        /// <summary>
        /// Dispose the SimConnect object with form
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            DisposeSimConnect();

            base.Dispose(disposing);
        }

        private async void WatchSimConnect()
        {
            while (simconnect != null)
            {
                try
                {

                }
                catch (COMException)
                {

                }
                finally
                {
                    await Task.Delay(SimConnectPoolCooldown);
                }
            }
        }

        public void UpdateAITraffic(uint objectID)
        {
            simconnect.MapClientEventToSimEvent(EVENTS.REQUEST_AI_SET_SLEW, "SLEW_ON");

            simconnect.MapClientEventToSimEvent(EVENTS.REQUEST_SET_SLEW_HDG, "AXIS_SLEW_HEADING_SET");

            simconnect.AIReleaseControl(objectID, EVENTS.REQUEST_AI_RELEASE);

            simconnect.TransmitClientEvent(objectID, EVENTS.REQUEST_AI_SET_SLEW, 1, GROUPS.SIMCONNECT_GROUP_PRIORITY_HIGHEST, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);

            simconnect.TransmitClientEvent(objectID, EVENTS.REQUEST_SET_SLEW_HDG, (uint)1000, GROUPS.ALL, SIMCONNECT_EVENT_FLAG.DEFAULT);
        }
    }
}
