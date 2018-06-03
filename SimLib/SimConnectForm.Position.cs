﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.FlightSimulator.SimConnect;

namespace SimLib
{
    public partial class SimConnectForm : Form
    {
        private TaskCompletionSource<Position> PositionTask;

        public void RegisterPosition()
        {
            try
            {
                simconnect.AddToDataDefinition(DEFINITIONS.Position, "Title", null, SIMCONNECT_DATATYPE.STRING256, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Position, "PLANE LATITUDE", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Position, "PLANE LONGITUDE", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Position, "PLANE ALTITUDE", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Position, "PLANE PITCH", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Position, "PLANE BANK", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

                simconnect.RegisterDataDefineStruct<Position>(DEFINITIONS.Position);
            }
            catch (COMException ex)
            {
                throw ex;
            }
        }

        public async Task<Position> GetPositionAsync()
        {
            PositionTask = new TaskCompletionSource<Position>();
            simconnect.RequestDataOnSimObjectType(DATA_REQUESTS.Position, DEFINITIONS.Position, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);

            return await PositionTask.Task;
        }

        private void OnRecvPosition(object sender, Position position)
        {
            if (PositionTask != null)
                PositionTask.SetResult(position);
        }
    }
}
