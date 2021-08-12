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

        private static Vector3 aircraftLocalPosition = new Vector3(0, 0.869f, 1.707f);
        private static Vector3 aircraftLocalEuler = new Vector3(0, 90, 0);
        private static Vector3 aircraftLocalScale= new Vector3(5f, 5f, 5f);
       

        public static void Prefix(WeaponManager __instance)
        {
            FlightLogger.Log("Awake prefix ran in wm!");
            bool mpCheck = true;

            if (MpPlugin.MPActive)
            {
                mpCheck = Main.instance.plugin.CheckPlaneSelected();

            }

            if (mpCheck && __instance.gameObject.GetComponentInChildren<PlayerFlightLogger>() && VTOLAPI.GetPlayersVehicleEnum() == VTOLVehicles.FA26B && AircraftInfo.AircraftSelected)
            {
                Main.playerGameObject = __instance.gameObject;


                AircraftAPI.FindSwitchBounds();

                
                UnityMover mover = __instance.gameObject.AddComponent<UnityMover>();
                mover.gs = __instance.gameObject;
                mover.FileName = AircraftInfo.UnityMoverFileName;
                mover.load(true);
               

             

                FlightLogger.Log("About to add nighthawk");



                GameObject aircraft = GameObject.Instantiate(Main.aircraftPrefab);
                aircraft.transform.SetParent(Main.playerGameObject.transform);
                aircraft.transform.localPosition = aircraftLocalPosition;
                aircraft.transform.localEulerAngles = aircraftLocalEuler;
                aircraft.transform.localScale = aircraftLocalScale;

                AircraftSetup.Fa26 = Main.playerGameObject;
                AircraftSetup.customAircraft = aircraft;

                //Creates the canopy animation and assigns the canopyobject to the ejection seat
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

                //Disables the Fa26's wingflex so nav lights don't get screwy
                AircraftSetup.DisableWingFlex();
                //Assigns the correct variables for the EOTS
                //AircraftSetup.SetUpEOTS();

                //Fixes the weird shifting nav map bug. Must be called after unity mover
                AircraftSetup.ScaleNavMap();

                //Changes the player's rwr icon to "custom" in order for other mods to know it's a custom plane
                AircraftSetup.ChangeRWRIcon();
                
                List<InternalWeaponBay> bays = new List<InternalWeaponBay>();
                foreach (InternalWeaponBay bay in aircraft.GetComponentsInChildren<InternalWeaponBay>(true))
                {
                    bay.weaponManager = __instance;
                    bays.Add(bay);

                }

                //AircraftAPI.FindInteractable("Toggle Altitude Mode").OnInteract.AddListener(logRCS);



                FlightLogger.Log("Disabling mesh");
                AircraftAPI.Disable26Mesh();
               


            }
        }

        public static void logRCS()
        {
            foreach(HPEquippable hp in Main.playerGameObject.GetComponentsInChildren<HPEquippable>(true))
            {
                FlightLogger.Log($"RCS of idx {hp.hardpointIdx}: {hp.GetRadarCrossSection()}");
            }
        }
    }


    [HarmonyPatch(typeof(WeaponManager), nameof(WeaponManager.Start))]
    class PlayerSpawnStartPatch
    {


        public static void Prefix(WeaponManager __instance)
        {

            FlightLogger.Log("Start prefix ran in wm!");
            if (__instance.gameObject.GetComponentInChildren<PlayerFlightLogger>() && VTOLAPI.GetPlayersVehicleEnum() == VTOLVehicles.FA26B && AircraftInfo.AircraftSelected)
            {

                //Makes missiles compatabile with the internal bays
                AircraftSetup.SetUpMissileLaunchers();

                //Reduces the rcs of the fa-26 and intiially sets the hard point rcs to 0
                AircraftSetup.SetUpRCS();

                //Folds the wings down on spawn. Runs a coroutine that waits one second to do so
                AircraftSetup.SetWingFold();

            }
        }



    }


 }
