using BepInEx;
using BepInEx.Unity.IL2CPP;
using UnityEngine;
using UnityEngine.XR;

namespace CapuchinTemplate
{
    [BepInPlugin("instel.fly", "Fly", "1.0.0")]
    public class Plugin : BasePlugin
    {
        public override void Load()
        {
            AddComponent<PluginBehaviour>();
        }
    }

    public class PluginBehaviour : MonoBehaviour
    {
        bool ismodded = false;
        void Start()
        {
            Caputilla.CaputillaManager.Instance.OnModdedJoin += OnJoinedModded;
            Caputilla.CaputillaManager.Instance.OnModdedLeave += OnLeaveModded;
        }

        void OnJoinedModded()
        {
            ismodded = true;
        }

        void OnLeaveModded()
        {
            ismodded = false;
        }

        void Update()
        {
            if (ismodded)
            {
                // Credits to Snowy for the refrence code
                InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
                if (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonPressed) && primaryButtonPressed)
                {
                    Vector3 forwardMovement = GameObject.Find("Main Camera").transform.TransformDirection(Vector3.forward) * 0.05f;
                    GameObject.Find("CapuchinPlayer").transform.position += forwardMovement;
                    Rigidbody playerRb = GameObject.Find("CapuchinPlayer").GetComponent<Rigidbody>();
                    playerRb.velocity = Vector3.zero;
                }
            }
        }
    }
}
