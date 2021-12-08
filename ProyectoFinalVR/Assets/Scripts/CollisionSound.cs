using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

namespace Sonido
{
    public class CollisionSound : MonoBehaviour
    {
        [EventRef]
        public string Event = "";
        protected private FMOD.Studio.EventInstance instance;
        private bool hasBeenDropped = false;
        private bool exeecedTreshold = false;
        private Rigidbody rb = null;
        [SerializeField]
        private float velocityForSound = 0.5f;
        [SerializeField]
        private float maxSpeedForSound = 5.0f;
        private float velocityBeforeCollision;

        public void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void dropFromHand()
        {
            //hasBeenDropped = true;


            //lol
        }

        public void Update()
        {
            if (!exeecedTreshold) exeecedTreshold = rb.velocity.magnitude > velocityForSound;
            if (exeecedTreshold) velocityBeforeCollision = rb.velocity.magnitude;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (exeecedTreshold)
            {
                //Debug.Log(rb.velocity.magnitude);
                hasBeenDropped = false;
                exeecedTreshold = false;
                instance = FMODUnity.RuntimeManager.CreateInstance(Event);
                if (instance.start() != FMOD.RESULT.OK) Debug.Log("Couldn't play sound: " + Event);
                else
                {
                    FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
                    instance.setParameterByName("ItemVelocity", calculateSpeedVariable());
                    
                    if (collision.gameObject.GetComponent<SurfaceIDComponent>() != null)
                    {
                        SurfaceID surfaceType = collision.gameObject.GetComponent<SurfaceIDComponent>().getType();
                        instance.setParameterByName("SurfaceType", (float)surfaceType);
                    }
                    else
                    {
                        instance.setParameterByName("SurfaceType", 4.0f);
                    }
                }
            }
        }

        private float calculateSpeedVariable()
        {
            if (velocityBeforeCollision >= maxSpeedForSound) return 10.0f;
            else return velocityBeforeCollision / maxSpeedForSound * 10.0f;
        }
    }
}