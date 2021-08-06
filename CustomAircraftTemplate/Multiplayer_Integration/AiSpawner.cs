using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Harmony;
using UnityEngine.Events;

namespace CustomAircraftTemplate
{

    public class AiSetup : MonoBehaviour
    {
        private static Vector3 aircraftLocalPosition = new Vector3(0, 0.066f, 1.643f);
        private static Vector3 aircraftLocalEuler = Vector3.zero;
        private static Vector3 aircraftLocalScale = Vector3.one;

        public static void CreateAi(GameObject aiObject)
        {
            GameObject aircraft = GameObject.Instantiate(Main.aircraftPrefab);
            aircraft.transform.SetParent(aiObject.transform);
            aircraft.transform.localPosition = aircraftLocalPosition;
            aircraft.transform.localEulerAngles = aircraftLocalEuler;
            aircraft.transform.localScale = aircraftLocalScale;


            foreach (InternalWeaponBay bay in aircraft.GetComponentsInChildren<InternalWeaponBay>(true))
            {
                bay.weaponManager = aiObject.GetComponentInChildren<WeaponManager>(true);

            }

            GearAnimator gearAnim = aiObject.GetComponentInChildren<GearAnimator>(true);
            AnimationToggle animToggle = AircraftAPI.GetChildWithName(aircraft, "GearAnimator").GetComponent<AnimationToggle>();
            gearAnim.OnOpen = new UnityEvent();
            gearAnim.OnOpen.AddListener(animToggle.Deploy);
            gearAnim.OnClose.AddListener(animToggle.Retract);


            AircraftAPI.Disable26MeshAi(aiObject);


        }



    }
}

