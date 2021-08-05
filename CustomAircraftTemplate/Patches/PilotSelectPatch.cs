using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomAircraftTemplate
{

    [HarmonyPatch(typeof(PilotSelectUI), "Start")]
    class PilotSelectStartPatch
    {

        //Stores whether or not the ccustom vehicle is currently selected
        private static bool tempAircraftSelected = false;

        //Sets the temporary storage to if the vehicle is selected
        public static void Prefix()
        {
            if (VTOLAPI.GetPlayersVehicleEnum() != VTOLVehicles.FA26B)
            {
                AircraftInfo.AircraftSelected = false;
            }
            if (AircraftInfo.AircraftSelected)
            {
                tempAircraftSelected = true;
            }

        }

        //Changes the aircraftselected variable back to true if it was initially selected
        public static void Postfix()
        {

            if (tempAircraftSelected)
            {
                AircraftInfo.AircraftSelected = true;
                Main.instance.checkMPloaded();
                if (MpPlugin.MPActive)
                {
                    Main.instance.plugin.SetCustomPlaneMP();
                }

                tempAircraftSelected = false;
            }

        }
    }


    [HarmonyPatch(typeof(PilotSelectUI), "SelectVehicle")]
    class VehicleSelectPatch
    {
        private static bool lockAircraftSelect = false;
        public static bool Prefix(PilotSelectUI __instance, PlayerVehicle vehicle)
        {
            FlightLogger.Log("Prefix ran!");
            if (vehicle.vehicleName == AircraftInfo.AircraftName)
            {

                FlightLogger.Log("Nighthawk ran!");
                //Bool that decides whether or not to run all the aircraft spawn code
                AircraftInfo.AircraftSelected = true;
                Main.instance.checkMPloaded();
                if (MpPlugin.MPActive)
                {
                    Main.instance.plugin.SetCustomPlaneMP();
                }

                lockAircraftSelect = true;


                __instance.SelectVehicle(PilotSaveManager.GetVehicle("F/A-26B"), null);
                return false;
            }

            if (!lockAircraftSelect)
            {
                Main.instance.checkMPloaded();
                if (MpPlugin.MPActive)
                {
                    Main.instance.plugin.UnSetCustomPlaneMP();
                    

                }


                AircraftInfo.AircraftSelected = false;
            }

            lockAircraftSelect = false;
            return true;
        }
    }
}
