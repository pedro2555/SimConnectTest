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
                //case (uint)EVENTS.PITOT_TOGGLE:

                //    displayText("PITOT switched");
                //    break;

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

        private void Form1_SimConnectHeadingChanged(object sender, EventArgs e)
        {
            CompassChangedEventArgs args = (CompassChangedEventArgs)e;

            txtHDG.Text =  args.Heading.ToString();
        }

        private void Form1_SimConnectLatitudeChanged(object sender, EventArgs e)
        {
            LatitudeChangedEventArgs args = (LatitudeChangedEventArgs)e;

            txtLAT.Text = args.Latitude.ToString();
        }

        private void Form1_SimConnectLongitudeChanged(object sender, EventArgs e)
        {
            LongitudeChangedEventArgs args = (LongitudeChangedEventArgs)e;

            txtLon.Text =  args.Longitude.ToString();
        }

        private void Form1_SimConnectAltitudeChanged(object sender, EventArgs e)
        {
            AltitudeChangedEventArgs args = (AltitudeChangedEventArgs)e;

            txtALT.Text = args.Altitude.ToString();
        }

        private void Form1_SimConnectPitchChanged(object sender, EventArgs e)
        {
            PitchChangedEventArgs args = (PitchChangedEventArgs)e;

            txtPitch.Text = args.Pitch.ToString();
        }
    }
}
