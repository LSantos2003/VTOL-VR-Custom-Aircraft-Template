using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace CustomAircraftTemplate
{
    class CustomLandingGear : MonoBehaviour
    {
        public AnimationToggle animToggle;
        public VRLever gearLever;
        private void Start()
        {
            this.gearLever.OnSetState.AddListener(this.onGearLever);
        }
        public void onGearLever(int state)
        {
            if(state == 0)
            {
                animToggle.Retract();
                return;
            }

            animToggle.Deploy();
        }


    }
}
