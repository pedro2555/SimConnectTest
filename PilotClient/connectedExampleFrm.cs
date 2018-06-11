﻿using Newtonsoft.Json;
using SimLib;
using System;
using System.Diagnostics;
using WebSocketSharp;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace PilotClient
{

    public partial class connectedExampleFrm : SimConnectForm
    {
        private WebSocket webSocket;

        private string OAuthToken
        { get; set; }

        // Response number 
        int response = 1;

        // Output text - display a maximum of 10 lines 
        string output = "\n\n\n\n\n\n\n\n\n\n";

        public connectedExampleFrm()
        {
            InitializeComponent();

            OAuthToken = null;
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
            // sim opened, send user to login form
            Process.Start("http://37.59.115.154/html/login.html");
        }

        /// <summary>
        /// Validates a given ASSR code on the Auth API token endpoint, populates OAuthToken when a valid squawk code is set
        /// </summary>
        /// <param name="ASSR"></param>
        private async void ValidateASSR(string ASSR)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://fa-authapi.herokuapp.com");

            HttpResponseMessage response = await client.GetAsync("/token/" + ASSR);


            if ((int)response.StatusCode == 200)
            {
                // logged in

                // this is the secret to send on the next API requests
                OAuthToken = response.Content.ReadAsStringAsync().Result;

                webSocket = new WebSocket(@"wss://fa-live.herokuapp.com/chat");

                webSocket.OnMessage += Receive;

                webSocket.Connect();

                await Send();
                
            }
            else
            {
                OAuthToken = null; // not sure
            }
        }

        private async Task Send()
        {
            while (webSocket.IsAlive)
            {
                Player.State = await SimObjectType<AircraftState>.
                    RequestDataOnSimObjectType();

                string data = JsonConvert.SerializeObject(Player);

                webSocket.Send(data);

                int millisecondDelay = 1500;
                await Task.Delay(millisecondDelay);
            }
        }

        private static Dictionary<string, Aircraft> airTraffic = new Dictionary<string, Aircraft>();

        private async void Receive(object sender, MessageEventArgs e)
        {
            Aircraft srvPlayer = JsonConvert.DeserializeObject<Aircraft>(e.Data);
            Aircraft existingPlayer = null;
            airTraffic.TryGetValue(srvPlayer.Callsign, out existingPlayer);
            if (existingPlayer != null)
            {
                srvPlayer.ObjectId = existingPlayer.ObjectId;
            }
            else
            {
                int trafficId = await SimObjectType<AircraftState>.
                    AICreateNonATCAircraft("", srvPlayer.Callsign, srvPlayer.State);
                srvPlayer.ObjectId = trafficId;
                airTraffic.Add(srvPlayer.Callsign, srvPlayer);
            }

            displayText(srvPlayer.ObjectId.ToString());
        }

        private void connectedExampleFrm_SimConnectClosed(object sender, EventArgs e)
        {
            if (webSocket != null)
                webSocket.Close();
            displayText("Disconnected from simulator");
        }

        static Aircraft Player = new Aircraft()
        {
            Callsign = "TSZ213"
        };

        private async void btnGetPositionAsync_Click(object sender, EventArgs e)
        {
            Player.State = await SimObjectType<AircraftState>.
                RequestDataOnSimObjectType();
            displayText(JsonConvert.SerializeObject(Player));
        }

        private async void btnGeXpndrAsync_Click(object sender, EventArgs e)
        {
            Radios r = await SimObjectType<Radios>.RequestDataOnSimObjectType();
            displayText(JsonConvert.SerializeObject(r.Transponder.ToString("X3")));
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            webSocket = new WebSocket(@"wss://fa-live.herokuapp.com/chat");

            webSocket.OnMessage += Receive;

            webSocket.Connect();

            await Send();
        }
    }
}
