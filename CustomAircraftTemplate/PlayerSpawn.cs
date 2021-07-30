using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomAircraftTemplate
{
    [HarmonyPatch(typeof(WeaponManager), nameof(WeaponManager.Awake))]
    class PlayerSpawnAwakePatch
    {

        private static Vector3 aircraftLocalPosition = new Vector3(0, 0.066f, 1.643f);
        private static Vector3 aircraftLocalEuler = Vector3.zero;
        private static Vector3 aircraftLocalScale= Vector3.one;
        public static void Prefix(WeaponManager __instance)
        {
            FlightLogger.Log("Awake prefix ran in wm!");
            if (VTOLAPI.GetPlayersVehicleGameObject() == __instance.gameObject && VTOLAPI.GetPlayersVehicleEnum() == VTOLVehicles.FA26B && AircraftInfo.AircraftSelected)
            {



                AircraftAPI.FindSwitchBounds();

                
                UnityMover mover = __instance.gameObject.AddComponent<UnityMover>();
                mover.gs = __instance.gameObject;
                mover.load(true);
               

             

                FlightLogger.Log("About to add nighthawk");



                GameObject aircraft = GameObject.Instantiate(Main.aircraftPrefab);
                aircraft.transform.SetParent(VTOLAPI.GetPlayersVehicleGameObject().transform);
                aircraft.transform.localPosition = aircraftLocalPosition;
                aircraft.transform.localEulerAngles = aircraftLocalEuler;
                aircraft.transform.localScale = aircraftLocalScale;

                AircraftSetup.Fa26 = VTOLAPI.GetPlayersVehicleGameObject();
                AircraftSetup.customAircraft = aircraft;

                //Creates the canopy animation
                AircraftSetup.CreateCanopyAnimation();

                //Creates the control surfaces
                AircraftSetup.CreateControlSurfaces();

                //Creates the custom landing gear
                AircraftSetup.CreateLandingGear();

                //Moves the hardpoints in the correct location
                AircraftSetup.SetUpHardpoints();

                //Attaches the refuel port animation to the refuel port class
                AircraftSetup.SetUpRefuelPort();

                //Makes tgp invisible everytime it's equipped
                AircraftSetup.SetUpInvisTGP();

                //Assigns the suspension components to the custom aircraft landing gear
                AircraftSetup.SetUpWheels();

                //Changes characteristics of the engines
                AircraftSetup.SetUpEngines();

                //Changes depth and scale of the hud to make it legible
                AircraftSetup.SetUpHud();

                AircraftSetup.SetUpMissileLaunchers();

                //Assigns the correct variables for the EOTS
                //AircraftSetup.SetUpEOTS();

                List<InternalWeaponBay> bays = new List<InternalWeaponBay>();
                foreach (InternalWeaponBay bay in aircraft.GetComponentsInChildren<InternalWeaponBay>(true))
                {
                    bay.weaponManager = __instance;
                    bays.Add(bay);

                }

                FlightLogger.Log("Disabling mesh");
                AircraftAPI.Disable26Mesh();
               


            }
        }
    }


    [HarmonyPatch(typeof(WeaponManager), nameof(WeaponManager.Start))]
    class PlayerSpawnStartPatch
    {


        public static void Prefix(WeaponManager __instance)
        {

            FlightLogger.Log("Start prefix ran in wm!");
            if (VTOLAPI.GetPlayersVehicleGameObject() == __instance.gameObject && VTOLAPI.GetPlayersVehicleEnum() == VTOLVehicles.FA26B && AircraftInfo.AircraftSelected)
            {


                AircraftSetup.SetUpMissileLaunchers();

            }
        }



    }


 }
