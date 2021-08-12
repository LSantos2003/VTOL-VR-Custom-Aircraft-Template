using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomAircraftTemplate
{
    class AircraftSetup
    {
        public static GameObject Fa26;
        public static GameObject customAircraft;

        public static void CreateCanopyAnimation()
        {
            WeaponManager wm = Fa26.GetComponentInChildren<WeaponManager>(true);

            GameObject faCanopyAnim = AircraftAPI.GetChildWithName(Fa26, "CanopyAnimator");
            faCanopyAnim.SetActive(false);
            CanopyAnimator canopyAnim = faCanopyAnim.GetComponent<CanopyAnimator>();

            //Attach any handles for the canopy animation here!
            //canopyAnim.handleInteractables[0] = leftHandleInt;
            
            canopyAnim.animator = AircraftAPI.GetChildWithName(customAircraft, "CanopyAnimatorN").GetComponent<Animator>();
            canopyAnim.canopyTf = AircraftAPI.GetChildWithName(customAircraft, "Nighthawk_CanopyTf").transform;

            Fa26.GetComponentInChildren<EjectionSeat>().canopyObject = AircraftAPI.GetChildWithName(customAircraft, "Nighthawk_CanopyTf");

            faCanopyAnim.SetActive(true);
            AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(Fa26, "Canopy"), wm);

            VRInteractable canopyInteractable = AircraftAPI.FindInteractable("Canopy");
            VRLever canopyLever = canopyInteractable.gameObject.GetComponentInChildren<VRLever>(true);
            canopyAnim.SetCanopyImmediate(canopyLever.currentState == 1);
        }


        //This is where you create all your control surface!
        //Note: These surfaces are really just for show and do not affect the flight characteristics
        public static void CreateControlSurfaces()
        {

            AeroController controller = Fa26.GetComponentInChildren<AeroController>();

            //Aileron Example
            AircraftAPI.createControlSurface(controller, AircraftAPI.GetChildWithName(customAircraft, "l_Wing_CS_1").transform, new Vector3(0, 0, 1), 35, 70, 0, 1, 0, 0, 20, false, 0, 0);
            AircraftAPI.createControlSurface(controller, AircraftAPI.GetChildWithName(customAircraft, "r_Wing_CS_1").transform, new Vector3(0, 0, -1), 35, 70, 0, 1, 0, 0, 20, false, 0, 0);

            AircraftAPI.createControlSurface(controller, AircraftAPI.GetChildWithName(customAircraft, "l_Rudder_Tip").transform, new Vector3(0, 1, 0), 25, 50, 0.6f, 0, -0.4f, 0, 20, false, 0, 0);
            AircraftAPI.createControlSurface(controller, AircraftAPI.GetChildWithName(customAircraft, "r_Rudder_Tip").transform, new Vector3(0, -1, 0), 25, 50, 0.6f, 0, 0.4f, 0, 20, false, 0, 0);

            AircraftAPI.createControlSurface(controller, AircraftAPI.GetChildWithName(customAircraft, "l_Wing_CS_2").transform, new Vector3(1, 0, 0), 35, 40, 0f, 0, 0f, 0, -1, true, 0, 1);
            AircraftAPI.createControlSurface(controller, AircraftAPI.GetChildWithName(customAircraft, "r_Wing_CS_2").transform, new Vector3(-1, 0, 0), 35, 40, 0f, 0, 0f, 0, -1, true, 0, 1);
        }

        public static void CreateLandingGear()
        {
            VRLever gearLever = AircraftAPI.FindInteractable("Landing Gear").gameObject.GetComponent<VRLever>();
            CustomLandingGear gear = customAircraft.AddComponent<CustomLandingGear>();
            gear.animToggle = AircraftAPI.GetChildWithName(customAircraft, "GearAnimator").GetComponent<AnimationToggle>();
            gear.gearLever = gearLever;
        }
        public static void SetUpHardpoints()
        {
            GameObject hpRight = AircraftAPI.GetChildWithName(customAircraft, "HPRightTf");
            GameObject hpLeft = AircraftAPI.GetChildWithName(customAircraft, "HPLeftTf");

            Transform hp12 = AircraftAPI.FindHardpoint(12).transform;
            Transform hp11 = AircraftAPI.FindHardpoint(11).transform;

            hp12.transform.SetParent(hpRight.transform);
            hp12.transform.localPosition = Vector3.zero;
            hp12.transform.localEulerAngles = Vector3.zero;

            hp11.transform.SetParent(hpLeft.transform);
            hp11.transform.localPosition = Vector3.zero;
            hp11.transform.localEulerAngles = Vector3.zero;


        }

        public static void SetUpRefuelPort()
        {
            RefuelPort port = Fa26.GetComponentInChildren<RefuelPort>();
            AnimationToggle animToggle = AircraftAPI.GetChildWithName(customAircraft, "fuelPort").GetComponent<AnimationToggle>();

            port.OnOpen.AddListener(animToggle.Deploy);
            port.OnClose.AddListener(animToggle.Retract);

        }

        public static void SetUpInvisTGP()
        {
            WeaponManager wm = Fa26.GetComponentInChildren<WeaponManager>(true);
            wm.OnWeaponEquipped += WeaponManager_OnWeaponEquipped;

        }

        private static void WeaponManager_OnWeaponEquipped(HPEquippable hp)
        {
            Debug.Log("Weapon equip thing ran!");
            if (hp.shortName == "TGP1")
            {
                Debug.Log("Disabled tgp mesh!");
                AircraftAPI.DisableMesh(hp.transform.parent.gameObject);
            }
           
        }

        public static void SetUpEngines()
        {
            foreach(ModuleEngine engine in Fa26.GetComponentsInChildren<ModuleEngine>(true))
            {
                engine.autoAB = false;
                engine.autoABThreshold = 1f;
                engine.maxThrust = 100f;
            }

            VRThrottle throttle = Fa26.GetComponentInChildren<VRThrottle>(true);
            throttle.abGate = false;
            throttle.abGateThreshold = 1.1f;

        }

        public static void SetUpWheels()
        {
            SuspensionWheelAnimator frontSus = AircraftAPI.GetChildWithName(Fa26, "FrontGear").GetComponentInChildren<SuspensionWheelAnimator>(true);
            SuspensionWheelAnimator leftSus = AircraftAPI.GetChildWithName(Fa26, "LeftGear").GetComponentInChildren<SuspensionWheelAnimator>(true);
            SuspensionWheelAnimator rightSus = AircraftAPI.GetChildWithName(Fa26, "RightGear").GetComponentInChildren<SuspensionWheelAnimator>(true);

            SuspensionWheelAnimator frontSusCustomAircraft = AircraftAPI.GetChildWithName(customAircraft, "FrontGearMain").GetComponentInChildren<SuspensionWheelAnimator>(true);
            SuspensionWheelAnimator leftSusCustomAircraft = AircraftAPI.GetChildWithName(customAircraft, "leftGearMain").GetComponentInChildren<SuspensionWheelAnimator>(true);
            SuspensionWheelAnimator rightSusCustomAircraft = AircraftAPI.GetChildWithName(customAircraft, "rightGearMain").GetComponentInChildren<SuspensionWheelAnimator>(true);

            frontSusCustomAircraft.suspension = frontSus.suspension;
            leftSusCustomAircraft.suspension = leftSus.suspension;
            rightSusCustomAircraft.suspension = rightSus.suspension;

        }
        public static void SetUpHud()
        {
            CollimatedHUDUI hud = Fa26.GetComponentInChildren<CollimatedHUDUI>(true);
            hud.depth = 1000f;
            hud.UIscale = 1.5f;
        }

        public static void SetUpEjectionSeat()
        {
            Fa26.GetComponentInChildren<EjectionSeat>(true).canopyObject = AircraftAPI.GetChildWithName(customAircraft, "Canopy_Main") ;
        }

        public static void SetUpEOTS()
        {
            OpticalTargeter targeter = customAircraft.GetComponentInChildren<OpticalTargeter>();

            WeaponManager wm = Fa26.GetComponentInChildren<WeaponManager>(true); ;
            targeter.actor = Fa26.GetComponentInChildren<Actor>(true);
            targeter.wm = wm;

            wm.opticalTargeter = targeter;
            Fa26.GetComponentInChildren<TargetingMFDPage>(true).opticalTargeter = targeter;
        }


        public static void SetUpMissileLaunchers()
        {
            InternalWeaponBay[] bays = customAircraft.GetComponentsInChildren<InternalWeaponBay>(true);

            if (bays == null) return;

            foreach (MissileLauncher ml in Fa26.GetComponentsInChildren<MissileLauncher>(true))
            {
                FlightLogger.Log("Found " + ml.missilePrefab.name);
                ml.openAndCloseBayOnLaunch = true;

                foreach(InternalWeaponBay bay in bays)
                {
                    if(bay.hardpointIdx == ml.gameObject.GetComponent<HPEquippable>().hardpointIdx)
                    {
                        FlightLogger.Log("Assigning iwb");
                        ml.SetInternalWeaponBay(bay);
                    }
                }
                
            }

        }

        public static void DisableWingFlex()
        {
            foreach(WingFlex flex in Fa26.GetComponentsInChildren<WingFlex>(true))
            {
                flex.flexFactor = 0;
            }
        }
        
        public static void ScaleNavMap()
        {
            Transform mfd1 = AircraftAPI.GetChildWithName(Fa26, "MFD1").transform;
            Transform mapParent = AircraftAPI.GetChildWithName(Fa26, "MapParent").transform;
            Transform mapDisplay = AircraftAPI.GetChildWithName(Fa26, "MapDisplay").transform;
            Transform mapTest = AircraftAPI.GetChildWithName(Fa26, "MapTest").transform;
            Transform mapTransform = AircraftAPI.GetChildWithName(Fa26, "MapTransform").transform;

            float small = mfd1.transform.localScale.x / 99.73274f;
            float big = 99.73274f / mfd1.transform.localScale.x;

            Vector3 smallScale = Vector3.one * small;
            Vector3 bigScale = Vector3.one * big;

            mapParent.transform.localScale = smallScale;
            mapDisplay.transform.localScale = bigScale;
            mapTest.transform.localScale = bigScale;
            mapTransform.transform.localScale = bigScale;

        }

        public static void SetUpRCS()
        {
            RadarCrossSection rcs = Fa26.GetComponent<RadarCrossSection>();
            rcs.size = 7.381652f;
            rcs.overrideMultiplier = 0.25f;

            foreach(HPEquippable hp in Fa26.GetComponentsInChildren<HPEquippable>(true))
            {
                hp.rcsMasked = true;
            }
        }

        public static void SetWingFold()
        {
            Main.instance.StartCoroutine(WingFoldRoutine());

        }

        public static IEnumerator WingFoldRoutine()
        {
            yield return new WaitForSeconds(1);

            Fa26.GetComponentInChildren<VehicleMaster>(true).SetWingFoldImmediate(false);
            Fa26.GetComponentInChildren<FlightWarnings>(true).RemoveCommonWarning(FlightWarnings.CommonWarnings.WingFold);

            VRLever wingLever = AircraftAPI.FindInteractable("Wing Fold").gameObject.GetComponent<VRLever>();
            wingLever.gameObject.GetComponent<AudioSource>().volume = 0;
            wingLever.RemoteSetState(0);

        }

        public static void ChangeRWRIcon()
        {
            Fa26.GetComponentInChildren<Radar>(true).radarSymbol = "custom";
        }
    }
}
