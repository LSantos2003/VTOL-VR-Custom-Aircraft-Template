using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAircraftTemplate
{
    public class AircraftInfo
    {

        //READ ME, IMPORTANT!!!!!!!!
        //You must change HarmonyId in order for your custom aircraft mod to be compatable with other aircraft mods
        public const string HarmonyId = "C137.Nighthawk";

        //Stores if your custom aircraft is selected.
        //This is what prevents your aircraft from constantly replacing the FA-26
        public static bool AircraftSelected = false;

        //Info about your aircraft
        public const string AircraftName = "F-117";
        public const string AircraftNickName = "Nighthawk";
        public const string AircraftDescription = "\"Nighthawk\" A stealthy attack/bomber aircraft with two internal weapon bays. Using an experimental stealth coating, it has a lower rcs than that of the F-45A";

        //Names of the various files you need to put in your builds folder
        public const string PreviewPngFileName = "NighthawkVehicleImage.png";
        public const string AircraftAssetbundleName = "f117";
        public const string UnityMoverFileName = "NighthawkPositions.surg";
        public const string AIUnityMoverFileName = "NighthawkPositionsAI.surg";

        //Name of the prefab of your aircraft from the assetbundle
        public const string AircraftPrefabName = "Nighthawk_V5.prefab";



    }
}
