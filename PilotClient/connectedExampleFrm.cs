using SimLib;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net.Http;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text;

namespace PilotClient
{   

    public partial class connectedExampleFrm : SimConnectForm
    {
        private static readonly HttpClient client = new HttpClient();

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

            requestLoginSquawk();
        }

        private async void requestLoginSquawk()
        {
            Console.WriteLine("Connecting...");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://9e26cedc-3121-401b-8ed6-b20f49ffb955.mock.pstmn.io");

            HttpResponseMessage response = client.GetAsync("/token").Result;
            
            compareSquawk(response);

        }

        private void compareSquawk(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {

                if (response.Content.ReadAsStringAsync().Result == "4700")
                {
                    Console.WriteLine("Connected");
                    MessageBox.Show("API Squawk Correct");
                }

            }
            else
            {
                Console.WriteLine("Not Authorized");
                requestLoginSquawk();
                Console.WriteLine("Trying Again...");
            }
        }

        private void connectedExampleFrm_SimConnectClosed(object sender, EventArgs e)
        {
            displayText("Disconnected from simulator");
        }

    }
}
