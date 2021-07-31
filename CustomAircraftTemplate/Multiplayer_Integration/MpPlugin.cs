using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace CustomAircraftTemplate
{
    public class MpPlugin
    {
        public static bool MPActive = false;
        public static void checkMPloaded()
        {
            FlightLogger.Log("Checking if Multiplayer is installed");
            List<Mod> list = new List<Mod>();
            list = VTOLAPI.GetUsersMods();
            foreach (Mod mod in list)
            {
                bool flag = mod.isLoaded && mod.name.Contains("ultiplayer");
                if (flag)
                {
                    MPlock();
                }
            }
        }

        private static void MPlock()
        {
            FlightLogger.Log($"Found multiplayer in {AircraftInfo.AircraftName} mod");
            if (MPActive)
                return;
            MPActive = true;
            PlayerManager.PlayerIsCustomPlane = true;
            PlayerManager.LoadedCustomPlaneString = AircraftInfo.AircraftName;

            PlayerManager.onSpawnLocalPlayer = (UnityAction<PlayerManager.CustomPlaneDef>)Delegate.Combine(PlayerManager.onSpawnLocalPlayer, new UnityAction<PlayerManager.CustomPlaneDef>(MPRespawnHook));
            PlayerManager.onSpawnClient = (UnityAction<PlayerManager.CustomPlaneDef>)Delegate.Combine(PlayerManager.onSpawnClient, new UnityAction<PlayerManager.CustomPlaneDef>(ClientAircraftSpawned));
        }

        private static void MPRespawnHook(PlayerManager.CustomPlaneDef def)
        {
            FlightLogger.Log($"MP Respawn Hook: {AircraftInfo.AircraftNickName}");



            MPRadio(def.planeObj);
            //AircraftSetup.SetUpAircraft(def.planeObj);
           //AircraftInfo.AircraftSelected = true;
            ///this.MPRadioRunAlready = false;
            // this.PlaneSetupDone = false;
            // base.StartCoroutine(this.InitWaiter(def.planeObj));
        }

        private static void MPRadio(GameObject f26)
        {
            Debug.Log("MP Radio Start");

            GameObject mpradiobutton1 = AircraftAPI.GetChildWithName(f26, "1");
            GameObject mpradiobutton2 = AircraftAPI.GetChildWithName(f26, "2");
            GameObject mpradiobutton3 = AircraftAPI.GetChildWithName(f26, "3");
            GameObject mpradiobutton4 = AircraftAPI.GetChildWithName(f26, "4");
            GameObject mpradiobutton5 = AircraftAPI.GetChildWithName(f26, "5");
            GameObject mpradiobutton6 = AircraftAPI.GetChildWithName(f26, "6");
            GameObject mpradiobutton7 = AircraftAPI.GetChildWithName(f26, "7");
            GameObject mpradiobutton8 = AircraftAPI.GetChildWithName(f26, "8");
            GameObject mpradiobutton9 = AircraftAPI.GetChildWithName(f26, "9");
            GameObject mpradiobutton0 = AircraftAPI.GetChildWithName(f26, "0");
            GameObject mpradiobuttonClr = AircraftAPI.GetChildWithName(f26, "Clr");
            GameObject mpradionewDisplay = AircraftAPI.GetChildWithName(f26, "Display(Clone)");

            mpradiobutton0.SetActive(false);
            mpradiobutton1.SetActive(false);
            mpradiobutton2.SetActive(false);
            mpradiobutton3.SetActive(false);
            mpradiobutton4.SetActive(false);
            mpradiobutton5.SetActive(false);
            mpradiobutton6.SetActive(false);
            mpradiobutton7.SetActive(false);
            mpradiobutton8.SetActive(false);
            mpradiobutton9.SetActive(false);
            mpradiobuttonClr.SetActive(false);
            mpradionewDisplay.SetActive(false);

        }



        private static void ClientAircraftSpawned(PlayerManager.CustomPlaneDef def)
        {

            bool flag = def.CustomPlaneString == AircraftInfo.AircraftName;
            if (flag)
            {
                // Debug.Log("spawned f16 in mp");
                AiSetup.CreateAi(def.planeObj);
                //clientAircraftSwapF.aSwaper = this;
                //clientAircraftSwapF.doSetup();
            }
        }




    }
}
