using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace CustomAircraftTemplate
{
    class AiSetup : MonoBehaviour
    {
        private static Vector3 aircraftLocalPosition = new Vector3(0, 0.869f, 1.707f);
        private static Vector3 aircraftLocalEuler = new Vector3(0, 90, 0);
        private static Vector3 aircraftLocalScale = new Vector3(5, 5, 5);

        public static void CreateAi(GameObject aiObject)
        {
			Disable26MeshAi(aiObject);

			GameObject aircraft = Instantiate<GameObject>(Main.aircraftPrefab);
			aircraft.transform.SetParent(aiObject.transform);
			aircraft.transform.localPosition = aircraftLocalPosition;
			aircraft.transform.localEulerAngles = aircraftLocalEuler;
			aircraft.transform.localScale = aircraftLocalScale;

			AIPilot aiPilot = aiObject.GetComponentInChildren<AIPilot>();
			GearAnimator gearAnim = aiPilot.gearAnimator;

			AnimationToggle animToggle = AircraftAPI.GetChildWithName(aircraft, "GearAnimator").GetComponent<AnimationToggle>();
			gearAnim.OnOpen.AddListener(new UnityAction(animToggle.Retract));
			gearAnim.OnClose.AddListener(new UnityAction(animToggle.Deploy));

			CreateControlSurfaces(aircraft, aiObject);
			SetUpRCS(aiObject);
		}

		public static void Disable26MeshAi(GameObject go)
		{
			WeaponManager componentInChildren = go.GetComponentInChildren<WeaponManager>(true);
			AircraftAPI.DisableMesh(go, componentInChildren);
			
		}

		public static void CreateControlSurfaces(GameObject customAircraft, GameObject aiAircraft)
		{

			AeroController controller = aiAircraft.GetComponentInChildren<AeroController>();

			//Aileron Example
			AircraftAPI.createControlSurface(controller, AircraftAPI.GetChildWithName(customAircraft, "l_Wing_CS_1").transform, new Vector3(0, 0, 1), 35, 70, 0, 1, 0, 0, 20, false, 0, 0);
			AircraftAPI.createControlSurface(controller, AircraftAPI.GetChildWithName(customAircraft, "r_Wing_CS_1").transform, new Vector3(0, 0, -1), 35, 70, 0, 1, 0, 0, 20, false, 0, 0);

			AircraftAPI.createControlSurface(controller, AircraftAPI.GetChildWithName(customAircraft, "l_Rudder_Tip").transform, new Vector3(0, 1, 0), 25, 50, 0.6f, 0, -0.4f, 0, 20, false, 0, 0);
			AircraftAPI.createControlSurface(controller, AircraftAPI.GetChildWithName(customAircraft, "r_Rudder_Tip").transform, new Vector3(0, -1, 0), 25, 50, 0.6f, 0, 0.4f, 0, 20, false, 0, 0);

			AircraftAPI.createControlSurface(controller, AircraftAPI.GetChildWithName(customAircraft, "l_Wing_CS_2").transform, new Vector3(1, 0, 0), 35, 40, 0f, 0, 0f, 0, -1, true, 0, 1);
			AircraftAPI.createControlSurface(controller, AircraftAPI.GetChildWithName(customAircraft, "r_Wing_CS_2").transform, new Vector3(-1, 0, 0), 35, 40, 0f, 0, 0f, 0, -1, true, 0, 1);
		}

		public static void SetUpRCS(GameObject aiAircraft)
		{
			RadarCrossSection rcs = aiAircraft.GetComponent<RadarCrossSection>();
			rcs.size = 7.381652f;
			rcs.overrideMultiplier = 0.5f;

			foreach (HPEquippable hp in aiAircraft.GetComponentsInChildren<HPEquippable>(true))
			{
				hp.rcsMasked = true;
			}
		}


	}




}
