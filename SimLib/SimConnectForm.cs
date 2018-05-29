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

        public enum EVENTS
        {
            MENU_ONE,
            MENU_TWO,
            MENU_THREE

        };

        enum NOTIFICATION_GROUPS
        {
            GROUP_MENU,
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

        public void AddMenuSimulator()
        {
                // Create some private events
                simconnect.MapClientEventToSimEvent(EVENTS.MENU_ONE,"");
                simconnect.MapClientEventToSimEvent(EVENTS.MENU_TWO, "");
                simconnect.MapClientEventToSimEvent(EVENTS.MENU_THREE, "");

                // Add one menu item
                simconnect.MenuAddItem("FlyAtlantic", EVENTS.MENU_ONE, 10000);
                simconnect.MenuAddSubItem(EVENTS.MENU_ONE, "Connect", EVENTS.MENU_TWO, 10001);                

                simconnect.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.GROUP_MENU, EVENTS.MENU_TWO, false);

                simconnect.SetNotificationGroupPriority(NOTIFICATION_GROUPS.GROUP_MENU, SimConnect.SIMCONNECT_GROUP_PRIORITY_HIGHEST);

        }
    }
}
