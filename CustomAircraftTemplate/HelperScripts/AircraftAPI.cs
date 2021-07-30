using Harmony;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CustomAircraftTemplate
{
    class AircraftAPI
    {
        public static GameObject SEAT_ADJUST_POSE_BOUNDS;
        private static Texture2D MenuTexture;

        public static void FindSwitchBounds()
        {
            SEAT_ADJUST_POSE_BOUNDS = GetChildWithName(VTOLAPI.GetPlayersVehicleGameObject(), ("MasterArmPoseBounds"));
            Debug.Log("pose bound found: " + SEAT_ADJUST_POSE_BOUNDS);


        }

        public static VRInteractable FindInteractable(string interactableName)
        {
            foreach(VRInteractable interactble in VTOLAPI.GetPlayersVehicleGameObject().GetComponentsInChildren<VRInteractable>(true))
            {
                if(interactble.interactableName == interactableName)
                {
                    return interactble;
                }
            }

            Debug.LogError($"Could not find VRinteractable: {interactableName}");
            return null;
        }

        public static HardpointVehiclePart FindHardpoint(int idx)
        {
            foreach (HardpointVehiclePart vp in VTOLAPI.GetPlayersVehicleGameObject().GetComponentsInChildren<HardpointVehiclePart>(true))
            {
                if (vp.hpIdx == idx)
                {
                    return vp;
                }
            }

            Debug.LogError($"Could not find hardpoint index: {idx}");
            return null;
        }

        public static void DisableMesh(GameObject parent, WeaponManager wm, bool nochildren)
        {
            if (!nochildren)
                return;
            MeshRenderer meshes = parent.GetComponent<MeshRenderer>();

            if (meshes)
                meshes.enabled = false;


        }

        public static void DisableMesh(GameObject parent, WeaponManager wm)
        {
            MeshRenderer[] meshes = parent.GetComponentsInChildren<MeshRenderer>(true);

            foreach (MeshRenderer mesh in meshes)
            {
                if (wm != null && !isHardPoint(mesh, wm))
                {
                    mesh.enabled = false;
                }

            }

        }

        public static void DisableMesh(GameObject parent)
        {
            MeshRenderer[] meshes = parent.GetComponentsInChildren<MeshRenderer>(true);

            foreach (MeshRenderer mesh in meshes)
            {

                mesh.enabled = false;


            }

        }

        public static bool isHardPoint(MeshRenderer mesh, WeaponManager wm)
        {
            foreach (Transform transform in wm.hardpointTransforms)
            {
                if (mesh.gameObject.transform.IsChildOf(transform) && transform != wm.hardpointTransforms[15])
                {
                    return true;
                }
            }

            return false;
        }


        public static void Disable26Mesh()
        {
            GameObject go = VTOLAPI.GetPlayersVehicleGameObject();
            WeaponManager wm = go.GetComponentInChildren<WeaponManager>(true);
            DisableMesh(GetChildWithName(go, "body"), wm);
            DisableMesh(GetChildWithName(go, "wingRightPart"), wm);
            DisableMesh(GetChildWithName(go, "wingLeftPart"), wm);
            DisableMesh(GetChildWithName(go, "elevonLeftPart"), wm);
            DisableMesh(GetChildWithName(go, "elevonRightPart"), wm);
            DisableMesh(GetChildWithName(go, "vertStabLeft_part"), wm);
            DisableMesh(GetChildWithName(go, "vertStabRight_part"), wm);
            DisableMesh(GetChildWithName(go, "fa26-leftEngine"), wm);
            DisableMesh(GetChildWithName(go, "fa26-rightEngine"), wm);
            DisableMesh(GetChildWithName(go, "LandingGear"), wm);
            DisableMesh(GetChildWithName(go, "Canopy"), wm);
            DisableMesh(GetChildWithName(go, "windshield"), wm);
            DisableMesh(GetChildWithName(go, "dash"), wm);
            DisableMesh(GetChildWithName(go, "HookTurret"), wm);
            DisableMesh(GetChildWithName(go, "airbrakeParent"), wm);
            DisableMesh(GetChildWithName(go, "HP14 TGP"));

        }

        public static GameObject GetChildWithName(GameObject obj, string name)
        {


            Transform[] children = obj.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in children)
            {
                if (child.name == name || child.name.Contains(name + "(clone"))
                {
                    return child.gameObject;
                }
            }


            return null;

        }


        //Adds a control surface item to an AeroController array
        public static AeroController.ControlSurfaceTransform[] AddItemToArray(AeroController.ControlSurfaceTransform[] original, AeroController.ControlSurfaceTransform itemToAdd)
        {
            AeroController.ControlSurfaceTransform[] finalArray = new AeroController.ControlSurfaceTransform[original.Length + 1];

            original.CopyTo(finalArray, 0);

            finalArray[finalArray.Length - 1] = itemToAdd;

            return finalArray;
        }


        //Turns an object into a control surface
        public static void createControlSurface(AeroController controller, Transform surface, Vector3 axis, float maxDeflection, float actuatorSpeed, float pitchFactor, float rollFactor,
                                                float yawFactor, float brakeFactor, float aoaLimit, bool oneDirectional, float trim, float flapFactor)
        {
            AeroController.ControlSurfaceTransform newSurface = new AeroController.ControlSurfaceTransform();
            newSurface.transform = surface;
            newSurface.axis = axis;
            newSurface.maxDeflection = maxDeflection;
            newSurface.actuatorSpeed = actuatorSpeed;
            newSurface.pitchFactor = pitchFactor;
            newSurface.rollFactor = rollFactor;
            newSurface.yawFactor = yawFactor;
            newSurface.oneDirectional = oneDirectional;
            newSurface.aoaLimit = aoaLimit;
            newSurface.brakeFactor = brakeFactor;
            newSurface.trim = trim;
            newSurface.flapsFactor = flapFactor;

            controller.controlSurfaces = AddItemToArray(controller.controlSurfaces, newSurface);
            controller.controlSurfaces[controller.controlSurfaces.Length - 1].Init();
        }



        //Loads a png into the game
        public static IEnumerator CreatePlaneMenuItem()
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(Path.Combine(Main.instance.ModFolder, Main.vehicleImageFileName));
            yield return www.SendWebRequest();

            if (www.responseCode != 200)
            {
                Debug.Log("WWW Response code isn't 200, it's " + www.responseCode + "\n" + www.error);
            }
            else
            {
                MenuTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Debug.Log("Loaded plane image.");
            }

            Debug.Log("Debug 1");
            Traverse traverse = Traverse.Create(typeof(VTResources));
            PlayerVehicleList vehicles = (PlayerVehicleList)traverse.Field("playerVehicles").GetValue();

            Debug.Log("Debug 2");
            PlayerVehicle newVehicle = ScriptableObject.CreateInstance<PlayerVehicle>();
            newVehicle.vehicleName = AircraftInfo.AircraftName;
            newVehicle.nickname = AircraftInfo.AircraftNickName;
            newVehicle.description = AircraftInfo.AircraftDescription;
            newVehicle.campaigns = PilotSaveManager.GetVehicle("F/A-26B").campaigns;
            newVehicle.vehicleImage = MenuTexture;
            vehicles.playerVehicles.Add(newVehicle);

            Debug.Log("Debug 3");
            traverse.Field("playerVehicles").SetValue(vehicles);


            Debug.Log("Debug 4");
            Traverse traverse2 = Traverse.Create(typeof(PilotSaveManager));
            List<PlayerVehicle> vehicleList = (List<PlayerVehicle>)traverse2.Field("vehicleList").GetValue();
            vehicleList.Add(newVehicle);
            traverse.Field("vehicleList").SetValue(vehicleList);


        }





    }
}
