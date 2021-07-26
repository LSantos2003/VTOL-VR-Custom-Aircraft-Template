using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace CustomAircraftTemplate
{
    
    [HarmonyPatch(typeof(InternalWeaponBay), nameof(InternalWeaponBay.Awake))]
    class InternalBayPatch
    {

        public static bool Prefix()
        {
            return false;
        }
    }


    [HarmonyPatch(typeof(InternalWeaponBay), nameof(InternalWeaponBay.Start))]
    class InternalBayPatchStart
    {

        public static bool Prefix(InternalWeaponBay __instance)
        {
            __instance.weaponManager.OnWeaponEquipped += __instance.WeaponManager_OnWeaponEquipped;
            return true;
        }
    }

 
	



}
