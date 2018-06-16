﻿using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimLib
{
    public static class FSX
    {
        public static SimConnect Sim;

        internal static Dictionary<int, Type> idMap = new Dictionary<int, Type>();

        internal static Dictionary<Type, int> typeMap = new Dictionary<Type, int>();

        public static class Player
        {
            public static string Callsign
            {
                get { return obj.Callsign; }
                set { obj.Callsign = value; }
            }

            private static Aircraft obj = new Aircraft();

            public static async Task<Aircraft> Get()
            {
                await obj.Read();

                return obj;
            }
        }

        public static class Traffic
        {
            private static Dictionary<string, Aircraft> knownTraffic
                = new Dictionary<string, Aircraft>();

            public static void Set(Aircraft traffic)
            {
                if (knownTraffic.ContainsKey(traffic.Callsign))
                    knownTraffic[traffic.Callsign].Update(traffic);
                else
                {
                    knownTraffic.Add(traffic.Callsign, traffic);
                    traffic.Create();
                }
            }
        }

        public static void GetSimList()
        {
            foreach (var directory in Directory.GetDirectories(@"C:\\Microsoft Flight Simulator X\\SimObjects\\Airplanes"))
            {
                var dir = new DirectoryInfo(directory);
                MyModels.Add(new MyModelMatching { ModelTitle = dir.Name });
            }
        }

        public class Aircraft
        {
            public string Callsign
            { get; set; }

            public string ModelName
            { get; set; }

            public int ObjectId
            { get; internal set; }

            public AircraftState State
            { get; set; }

            public async void Create()
            {
                ObjectId = await SimObjectType<AircraftState>.
                    AICreateNonATCAircraft(ModelName, Callsign, State);

                modelMatchingOnServer.Add(new ModelMatchingOnServer { ModelCallsign = "TSZ213", ModelTitle = State.title });

                await VerifyModelMatching();
            }

            internal async Task<AircraftState> Read()
            {
                State = await SimObjectType<AircraftState>
                    .RequestDataOnSimObjectType();

                return State;
            }

            public async void Update(Aircraft newTraffic)
            {
                State = newTraffic.State;

                await SimObjectType<AircraftState>.
                    SetDataOnSimObject((uint)ObjectId, State);
            }

            public async Task VerifyModelMatching()
            {
                foreach (var modelOnServer in modelMatchingOnServer)
                {
                    foreach (var simModels in MyModels)
                    {
                        try
                        {
                            ///compare here
                            if (File.ReadLines(String.Format("C:\\Microsoft Flight Simulator X\\SimObjects\\Airplanes\\{0}\\aircraft.cfg", simModels.ModelTitle)).Any(line => line.Contains(modelOnServer.ModelTitle)))
                                Console.WriteLine("True");
                        }
                        catch (Exception ex)
                        {
                            
                        }
                    }
                }
            }
        }

        public static List<ModelMatchingOnServer> modelMatchingOnServer = new List<ModelMatchingOnServer>();

        public static List<MyModelMatching> MyModels = new List<MyModelMatching>();

        public class MyModelMatching
        {
            public string ModelTitle
            { get; set; }
        }

        public class ModelMatchingOnServer
        {
            public string ModelCallsign
            { get; set; }

            public string ModelTitle
            { get; set; }

        }
    }
}
