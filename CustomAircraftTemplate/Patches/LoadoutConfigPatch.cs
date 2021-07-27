﻿using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomAircraftTemplate
{

    [HarmonyPatch(typeof(LoadoutConfigurator), "Initialize")]
    public static class LoadoutConfigStartPatch
    {
        public static void Postfix(LoadoutConfigurator __instance)
        {
            if (!AircraftInfo.AircraftSelected) return;
            __instance.AttachImmediate("fa26_tgp", 14);
            __instance.lockedHardpoints.Add(14);
            AircraftHelper.DisableMesh(AircraftHelper.GetChildWithName(VTOLAPI.GetPlayersVehicleGameObject(), "HP14 TGP"));

        }
    }
        [HarmonyPatch(typeof(LoadoutConfigurator), "EquipCompatibilityMask")]
    public static class EquipComaptibilityPatch
    {
        public static bool Prefix(LoadoutConfigurator __instance, HPEquippable equip)
        {
            
            if (!AircraftInfo.AircraftSelected) return true;

            if (true) // fuck you c ; work on manners you ape
            {
                Debug.Log("Section 11");


                // this creates a dictionary of all the wepaons and where they can be mounted, just alter the second string per weapon according to the wepaon you want.
                Dictionary<string, string> allowedhardpointbyweapon = new Dictionary<string, string>();

                allowedhardpointbyweapon.Add("fa26_gun", "");
                allowedhardpointbyweapon.Add("fa26-cft", "");
                allowedhardpointbyweapon.Add("fa26_agm89x1", "");
                allowedhardpointbyweapon.Add("fa26_agm161", "");
                allowedhardpointbyweapon.Add("fa26_aim9x2", "");
                allowedhardpointbyweapon.Add("fa26_aim9x3", "");
                allowedhardpointbyweapon.Add("fa26_cagm-6", "");
                allowedhardpointbyweapon.Add("fa26_cbu97x1", "11, 12");
                allowedhardpointbyweapon.Add("fa26_droptank", "");
                allowedhardpointbyweapon.Add("fa26_droptankXL", "");
                allowedhardpointbyweapon.Add("fa26_gbu12x1", "11, 12");
                allowedhardpointbyweapon.Add("fa26_gbu12x2", "");
                allowedhardpointbyweapon.Add("fa26_gbu12x3", "");
                allowedhardpointbyweapon.Add("fa26_gbu38x1", "11, 12");
                allowedhardpointbyweapon.Add("fa26_gbu38x2", "");
                allowedhardpointbyweapon.Add("fa26_gbu38x3", "");
                allowedhardpointbyweapon.Add("fa26_gbu39x4uFront", "11, 12");
                allowedhardpointbyweapon.Add("fa26_gbu39x4uRear", "11, 12");
                // allowedhardpointbyweapon.Add("fa26_gun", "0");
                allowedhardpointbyweapon.Add("fa26_harmx1", "11, 12");
                allowedhardpointbyweapon.Add("fa26_harmx1dpMount", "");
                allowedhardpointbyweapon.Add("fa26_iris-t-x1", "11, 12");
                allowedhardpointbyweapon.Add("fa26_iris-t-x2", "");
                allowedhardpointbyweapon.Add("fa26_iris-t-x3", "");
                allowedhardpointbyweapon.Add("fa26_maverickx1", "11, 12");
                allowedhardpointbyweapon.Add("fa26_maverickx3", "");
                allowedhardpointbyweapon.Add("fa26_mk82HDx1", "11, 12");
                allowedhardpointbyweapon.Add("fa26_mk82HDx2", "");
                allowedhardpointbyweapon.Add("fa26_mk82HDx3", "");
                allowedhardpointbyweapon.Add("fa26_mk82x2", "");
                allowedhardpointbyweapon.Add("fa26_mk82x3", "");
                allowedhardpointbyweapon.Add("fa26_mk83x1", "11, 12");
                allowedhardpointbyweapon.Add("fa26_sidearmx1", "11, 12");
                allowedhardpointbyweapon.Add("fa26_sidearmx2", "");
                allowedhardpointbyweapon.Add("fa26_sidearmx3", "");
                allowedhardpointbyweapon.Add("fa26_tgp", "14");
                allowedhardpointbyweapon.Add("cagm-6", "");
                allowedhardpointbyweapon.Add("cbu97x1", "11, 12");
                allowedhardpointbyweapon.Add("gbu38x1", "11, 12");
                allowedhardpointbyweapon.Add("gbu38x2", "");
                allowedhardpointbyweapon.Add("gbu38x3", "");
                allowedhardpointbyweapon.Add("gbu39x3", "");
                allowedhardpointbyweapon.Add("gbu39x4u", "");
                allowedhardpointbyweapon.Add("h70-4x4", "11, 12");
                allowedhardpointbyweapon.Add("h70-x7", "11, 12");
                allowedhardpointbyweapon.Add("h70-x19", "11, 12");
                allowedhardpointbyweapon.Add("hellfirex4", "11, 12");
                allowedhardpointbyweapon.Add("iris-t-x1", "11, 12");
                allowedhardpointbyweapon.Add("iris-t-x2", "");
                allowedhardpointbyweapon.Add("iris-t-x3", "");
                allowedhardpointbyweapon.Add("m230", "");
                allowedhardpointbyweapon.Add("marmx1", "");
                allowedhardpointbyweapon.Add("maverickx1", "11, 12");
                allowedhardpointbyweapon.Add("maverickx3", "");
                allowedhardpointbyweapon.Add("mk82HDx1", "11, 12");
                allowedhardpointbyweapon.Add("mk82HDx2", "");
                allowedhardpointbyweapon.Add("mk82HDx3", "");
                allowedhardpointbyweapon.Add("mk82x1", "11, 12");
                allowedhardpointbyweapon.Add("mk82x2", "");
                allowedhardpointbyweapon.Add("mk82x3", "");
                allowedhardpointbyweapon.Add("sidearmx1", "11, 12");
                allowedhardpointbyweapon.Add("sidearmx2", "");
                allowedhardpointbyweapon.Add("sidearmx3", "");
                allowedhardpointbyweapon.Add("sidewinderx1", "11, 12");
                allowedhardpointbyweapon.Add("sidewinderx2", "");
                allowedhardpointbyweapon.Add("sidewinderx3", "");
                allowedhardpointbyweapon.Add("af_aim9", "11, 12");
                allowedhardpointbyweapon.Add("af_amraam", "11, 12");
                allowedhardpointbyweapon.Add("af_amraamRail", "11, 12");
                allowedhardpointbyweapon.Add("af_amraamRailx2", "");
                allowedhardpointbyweapon.Add("af_dropTank", "");
                allowedhardpointbyweapon.Add("af_maverickx1", "11, 12");
                allowedhardpointbyweapon.Add("af_maverickx3", "11, 12");
                allowedhardpointbyweapon.Add("af_mk82", "11, 12");
                allowedhardpointbyweapon.Add("af_tgp", "14");
                allowedhardpointbyweapon.Add("h70-x7ld", "11, 12");
                allowedhardpointbyweapon.Add("h70-x7ld-under", "");
                allowedhardpointbyweapon.Add("h70-x14ld-under", "");
                allowedhardpointbyweapon.Add("h70-x14ld", "");


                Debug.Log("Equipment: " + equip.name + ", Allowed on" + equip.allowedHardpoints);



                if (allowedhardpointbyweapon.ContainsKey(equip.name))
                {
                    equip.allowedHardpoints = (string)allowedhardpointbyweapon[equip.name];
                    Debug.Log("Equipment: " + equip.name + ", Allowed on" + equip.allowedHardpoints);
                }
                else
                {
                    Debug.Log("Equipment: " + equip.name + ", not in dictionary");
                }
            }
            //equip.allowedHardpoints = "0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15";
            return true;
        }
    }

}