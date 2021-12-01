using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

namespace Sonido
{
    public class ReverbTriggerActivator : MonoBehaviour
    {
        [EventRef]
        public string Event = "";
        protected private FMOD.Studio.EventInstance instance;
        protected private bool loaded = true;
        // Start is called before the first frame update
        void Start()
        {
            instance = FMODUnity.RuntimeManager.CreateInstance(Event);
            if (loaded = (instance.start() == FMOD.RESULT.OK)) FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
            else Debug.Log("Couldn't play sound: " + Event);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Valve.VR.InteractionSystem.FallbackCameraController>())
            {
                Debug.Log("Entras");
            }
        }
    }
}