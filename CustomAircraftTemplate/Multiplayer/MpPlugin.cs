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
     

        public void MPlock()
        {
            FlightLogger.Log($"Found Multiplayer set {AircraftInfo.AircraftName} mp");
            if (MPActive)
                return;
            MPActive = true;
            //PlayerManager.PlayerIsCustomPlane = true;
            //PlayerManager.LoadedCustomPlaneString = AircraftInfo.AircraftName;

            PlayerManager.onSpawnLocalPlayer = (UnityAction<PlayerManager.CustomPlaneDef>)Delegate.Combine(PlayerManager.onSpawnLocalPlayer, new UnityAction<PlayerManager.CustomPlaneDef>(MPRespawnHook));
            PlayerManager.onSpawnClient = (UnityAction<PlayerManager.CustomPlaneDef>)Delegate.Combine(PlayerManager.onSpawnClient, new UnityAction<PlayerManager.CustomPlaneDef>(ClientAircraftSpawned));

            RegisterCustomPlane();
        }

        private void MPRespawnHook(PlayerManager.CustomPlaneDef def)
        {
            FlightLogger.Log("MP Respawn Hook");
            this.MPRadio(def.planeObj);
        }

        public void MPRadio(GameObject f26)
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

        private void ClientAircraftSpawned(PlayerManager.CustomPlaneDef def)
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

        private void RegisterCustomPlane()
        {
            PlayerManager.RegisterCustomPlane(AircraftInfo.AircraftName, "F/A-26B");
        }

        public void SetCustomPlaneMP()
        {
            AircraftInfo.AircraftSelected = true;
            PlayerManager.SetCustomPlane(AircraftInfo.AircraftName);
            
        }

        public void UnSetCustomPlaneMP()
        {
            if (PlayerManager.LoadedCustomPlaneString != AircraftInfo.AircraftName) return;
            
            PlayerManager.PlayerIsCustomPlane = false;
            PlayerManager.SetCustomPlane("none");

        }


        public bool CheckPlaneSelected()
        {
            if (PlayerManager.LoadedCustomPlaneString == AircraftInfo.AircraftName && PlayerManager.PlayerIsCustomPlane)
            {
                AircraftInfo.AircraftSelected = true;
                return true;
            }

            AircraftInfo.AircraftSelected = false;
            return false;
       
        }

    }
}
