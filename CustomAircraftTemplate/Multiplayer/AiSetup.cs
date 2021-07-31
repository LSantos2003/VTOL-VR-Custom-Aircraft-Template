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
			Disable26MeshAi(aiObject);
		}

		public static void Disable26MeshAi(GameObject go)
		{
			WeaponManager componentInChildren = go.GetComponentInChildren<WeaponManager>(true);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "body"), componentInChildren);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "body (LOD1)"), componentInChildren);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "FuelPort"), componentInChildren);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "intakeLeft"), componentInChildren);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "intakeRight"), componentInChildren);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "elevonLeftPart"), componentInChildren);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "elevonRightPart"), componentInChildren);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "verticalStabLeft"), componentInChildren);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "verticalStabRight"), componentInChildren);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "wingLeftPart"), componentInChildren);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "wingRightPart"), componentInChildren);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "combinedEngine"), componentInChildren);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "LandingGear"), componentInChildren);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "airbrakeParent"), componentInChildren);
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "HP14 TGP"));
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "cockpitFrame.002 (LOD1)"));
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "canopy"));
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "windshield"));
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "windshield.002 (LOD1)"));
			AircraftAPI.DisableMesh(AircraftAPI.GetChildWithName(go, "HP14 TGP"));
		}

	}




}
