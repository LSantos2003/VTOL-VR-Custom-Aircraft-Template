using Harmony;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomAircraftTemplate
{
    public class Main : VTOLMOD
    {

        public static Main instance;
        public static GameObject aircraftPrefab;
        public static Texture2D aircraftTexture;

        public const string vehicleImageFileName = "NighthawkVehicleImage.png";
        private static string fileName;
        private static string prefabName;

        public const string vehicleName = "F-117";
        public static bool aircraftSelected = false;
        // This method is run once, when the Mod Loader is done initialising this game object
        public override void ModLoaded()
        {
         
            HarmonyInstance harmonyInstance = HarmonyInstance.Create("C-137.CustomAircraft");
            harmonyInstance.PatchAll();

            instance = this;


            string pathToBundle = Path.Combine(instance.ModFolder, "f117");
            Debug.Log(pathToBundle);
            aircraftPrefab = FileLoader.GetAssetBundleAsGameObject(pathToBundle, "Nighthawk_V4.prefab");

            Debug.Log("Got le" + aircraftPrefab.name);
            StartCoroutine(AircraftHelper.LoadPlaneImage());
            //Adding aircraft to screen
           

            //This is an event the VTOLAPI calls when the game is done loading a scene
            VTOLAPI.SceneLoaded += SceneLoaded;
            base.ModLoaded();



        }


      
        //This method is called every frame by Unity. Here you'll probably put most of your code
        void Update()
        {
        }

        //This method is like update but it's framerate independent. This means it gets called at a set time interval instead of every frame. This is useful for physics calculations
        void FixedUpdate()
        {

        }

        //This function is called every time a scene is loaded. this behaviour is defined in Awake().
        private void SceneLoaded(VTOLScenes scene)
        {
            //If you want something to happen in only one (or more) scenes, this is where you define it.

            //For example, lets say you're making a mod which only does something in the ready room and the loading scene. This is how your code could look:
            switch (scene)
            {
                case VTOLScenes.ReadyRoom:
                    //Add your ready room code here
                    break;
                case VTOLScenes.LoadingScene:
                    //Add your loading scene code here
                    break;
            }
        }
    }
}