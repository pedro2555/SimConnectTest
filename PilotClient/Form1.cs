using Microsoft.FlightSimulator.SimConnect;
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

        private void Form1_OnSimConnectEvent(Microsoft.FlightSimulator.SimConnect.SimConnect sender, Microsoft.FlightSimulator.SimConnect.SIMCONNECT_RECV_EVENT data)
        {
            //switch (recEvent.uEventID)
            //{
            //    case (uint)EVENTS.PITOT_TOGGLE:

            //        displayText("PITOT switched");
            //        break;

            //}

            switch (data.dwID)
            {
                case (uint)DATA_REQUESTS.REQUEST_1:
                    Struct1 s1 = (Struct1)data.dwData[0];

                    displayText("Title: " + s1.title);
                    displayText("Lat:   " + s1.latitude);
                    displayText("Lon:   " + s1.longitude);
                    displayText("Alt:   " + s1.altitude);
                    break;

                default:
                    displayText("Unknown request ID: " + data.dwID);
                    break;
            }
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

        void recv_server_data_callback(dynamic data)
        {
            CreateAircraft(data.callsign, data.latitude, data.longitude, data.altitude, data.type);
        }

        private void btnCreateAI_Click(object sender, EventArgs e)
        {
            recv_server_data_callback(new
            {
                callsign = "TSZ001",
                type = "Airbus A321",
                latitude = 38.76697,
                longitude = -9.143276,
                altitude = 500
            });
        }

        private void btnGetPosition_Click(object sender, EventArgs e)
        {
            RequestDataOnSimObjectType();
            displayText("Request sent...");
        }

    }
}
