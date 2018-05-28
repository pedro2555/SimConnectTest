using SimLib;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net.Http;

namespace PilotClient
{   

    public partial class connectedExampleFrm : SimConnectForm
    {
        private static readonly HttpClient client = new HttpClient();

        private HttpRequestMessage message;
        // Response number 
        int response = 1;

        // Output text - display a maximum of 10 lines 
        string output = "\n\n\n\n\n\n\n\n\n\n";

        public connectedExampleFrm()
        {
            InitializeComponent();
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

        private void connectedExampleFrm_SimConnectOpen(object sender, EventArgs e)
        {
            displayText("Connected to simulator");

            Process.Start("http://37.59.115.154/html/login.html");
            
        }

        private void requestLoginSquawk()
        {
            message = new HttpRequestMessage(HttpMethod.Get, "https://49ce63db-126a-45c8-9493-04dd5206674f.mock.pstmn.io/login");
        }

        private void connectedExampleFrm_SimConnectClosed(object sender, EventArgs e)
        {
            displayText("Disconnected from simulator");
        }
    }
}
