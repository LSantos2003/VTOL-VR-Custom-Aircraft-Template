using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomAircraftTemplate
{
    public class customTransform
    {
        public String name;

        public Vector3 positions;

        public Vector3 rotations;

        public Vector3 scales;

        public bool skip = false;
    }

    public class UnityMover : MonoBehaviour
    {
        public GameObject gs;

        // Start is called before the first frame update
        void Awake()
        {
            gs = gameObject;
        }
        public void load(bool doCockpit = true)
        {

            Debug.Log("load1");
            if (gs == null)
                return;
            List<customTransform> listCustomTraans = new List<customTransform>();
            string fileLocation = Path.Combine(Main.instance.ModFolder, AircraftInfo.UnityMoverFileName);
            using (BinaryReader reader = new BinaryReader(File.Open(fileLocation, FileMode.Open)))
            {

                int total = reader.ReadInt32();

                for (int i = 0; i < total; i++)
                {
                    int totalString = reader.ReadInt32();
                    string render = "";

                    render = reader.ReadString();
                    string name = reader.ReadString();
                    string pname = reader.ReadString();
                    float x = reader.ReadSingle();
                    float y = reader.ReadSingle();
                    float z = reader.ReadSingle();

                    float rx = reader.ReadSingle();
                    float ry = reader.ReadSingle();
                    float rz = reader.ReadSingle();

                    float sx = reader.ReadSingle();
                    float sy = reader.ReadSingle();
                    float sz = reader.ReadSingle();

                    customTransform trans = new customTransform();
                    trans.name = name;
                    trans.positions = new Vector3(x, y, z);
                    trans.rotations = new Vector3(rx, ry, rz);
                    trans.scales = new Vector3(sx, sy, sz);

                    foreach (var ts in gs.GetComponentsInChildren<Transform>(true))
                    {

                        if (ts.name == (name))
                        {

                            if (ts.parent.name == (pname) || pname == "noparent")
                            {
                                if (render == "disable")
                                    ts.gameObject.active = false;

                                //if (render == "render")
                                // ts.gameObject.GetComponent<MeshRenderer>().enabled = true;

                                if (render == "dontrender")
                                    AircraftAPI.DisableMesh(ts.gameObject, null, true);
                                //ts.gameObject.GetComponent<MeshRenderer>().enabled = false;

                                ts.localPosition = trans.positions;
                                ts.localEulerAngles = trans.rotations;
                                ts.localScale = trans.scales;
                                if (doCockpit)
                                    if (ts.GetComponentsInChildren<VRInteractable>() != null)
                                    {
                                        VRInteractable[] interactables = ts.GetComponentsInChildren<VRInteractable>();

                                        foreach (var intact in interactables)
                                        {

                                            Vector3 posesize = new Vector3();
                                            bool resize = false;
                                            if (intact.poseBounds != null && AircraftAPI.SEAT_ADJUST_POSE_BOUNDS != null)
                                            {
                                                if (intact.poseBounds.pose != GloveAnimation.Poses.Joystick || intact.poseBounds.pose != GloveAnimation.Poses.Throttle || intact.poseBounds.pose != GloveAnimation.Poses.JetThrottle)
                                                {
                                                    resize = true;
                                                    posesize = intact.poseBounds.size;
                                                    GameObject newBounds = Instantiate(AircraftAPI.SEAT_ADJUST_POSE_BOUNDS, gameObject.transform);
                                                    newBounds.name = "new" + ts.parent.name;
                                                    newBounds.transform.position = intact.transform.parent.gameObject.transform.position;
                                                    newBounds.transform.eulerAngles = intact.transform.parent.transform.eulerAngles;
                                                    intact.poseBounds = newBounds.GetComponent<PoseBounds>(); //Assigns bounds for switch
                                                    if (resize) intact.poseBounds.size = posesize;
                                                }


                                            }

                                        }
                                    }
                            }
                        }
                    }
                }
            }
            Debug.Log("load2");
        }




        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("l"))
            {
                load();
            }
        }
    }
}
