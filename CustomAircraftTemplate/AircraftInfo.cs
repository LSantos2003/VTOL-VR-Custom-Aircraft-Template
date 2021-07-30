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

        public const string AircraftName = "F-117";
        public const string AircraftNickName = "Nighthawk";
        public const string AircraftDescription = "Stealth bomber";

        public const string PreviewPngFileName = "NighthawkVehicleImage.png";


        public const string AircraftAssetbundleName = "f117";
        public const string AircraftPrefabName = "Nighthawk_V4.prefab";

        public const string UnityMoverFileName = "NighthawkPositions.surg";



    }
}
