using SimLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PilotClient
{
    public partial class Form1 : SimConnectForm
    {
        // Response number 
        int response = 1;

        // Output text - display a maximum of 10 lines 
        string output = "\n\n\n\n\n\n\n\n\n\n";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_OnSimConnectEvent(Microsoft.FlightSimulator.SimConnect.SimConnect sender, Microsoft.FlightSimulator.SimConnect.SIMCONNECT_RECV_EVENT recEvent)
        {
            switch (recEvent.uEventID)
            {
                case (uint)EVENTS.PITOT_TOGGLE:

                    displayText("PITOT switched");
                    break;

                case (uint)EVENTS.FLAPS_UP:

                    displayText("Flaps Up");
                    break;

                case (uint)EVENTS.FLAPS_DOWN:

                    displayText("Flaps Down");
                    break;

                case (uint)EVENTS.FLAPS_INC:

                    displayText("Flaps Inc");
                    break;

                case (uint)EVENTS.FLAPS_DEC:

                    displayText("Flaps Dec");
                    break;
            }
        }
        
        void recv_server_data_callback(dynamic data)
        {
            CreateAircraft(data.callsign, data.latitude, data.longitude, data.altitude, data.type);
        }

        void displayText(string s)
        {
            // remove first string from output 
            output = output.Substring(output.IndexOf("\n") + 1);

            // add the new string 
            output += "\n" + response++ + ": " + s;

            // display it 
            txtLog.Text = output;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            recv_server_data_callback(new
            {
                callsign = "TSZ001",
                type = "Airbus A321",
                latitude = 38.77,
                longitude = -9.13,
                altitude = 500
            });
        }
    }
}
