using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

namespace Sonido
{
    public class WalkingSounds : MonoBehaviour
    {
        [EventRef]
        public string walkEvent = "";
        protected private FMOD.Studio.EventInstance walkInstance;

        [EventRef]
        public string runEvent = "";
        protected private FMOD.Studio.EventInstance runInstance;

        SurfaceID currentSurface = SurfaceID.WoodenFloor;


        [SerializeField]
        private float distForStep = 0.1f;

        private float newDistForStep;

        private Vector3 lastStepPos;

        void Start()
        {
            lastStepPos = transform.position;
            newDistForStep = distForStep;
        }

        void Update()
        {
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(lastStepPos.x, lastStepPos.z)) > newDistForStep)
            {
                lastStepPos = transform.position;

                if(Input.GetKeyDown(KeyCode.LeftShift))
                {
                    runInstance = FMODUnity.RuntimeManager.CreateInstance(runEvent);
                    if (runInstance.start() != FMOD.RESULT.OK) Debug.Log("Couldn't play sound: " + runEvent);
                    else
                    {
                        FMODUnity.RuntimeManager.AttachInstanceToGameObject(runInstance, GetComponent<Transform>(), GetComponent<Rigidbody>());
                        runInstance.setParameterByName("SurfaceType", (float)currentSurface); //0 walk, 1 run
                    }
                    newDistForStep = distForStep * 2;
                }
                else
                {
                    walkInstance = FMODUnity.RuntimeManager.CreateInstance(walkEvent);
                    if (walkInstance.start() != FMOD.RESULT.OK) Debug.Log("Couldn't play sound: " + walkEvent);
                    else
                    {
                        FMODUnity.RuntimeManager.AttachInstanceToGameObject(walkInstance, GetComponent<Transform>(), GetComponent<Rigidbody>());
                        walkInstance.setParameterByName("SurfaceType", (float)currentSurface); //0 walk, 1 run
                    }
                    newDistForStep = distForStep;
                }
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("colisione");
            if (other.GetComponent<SurfaceIDComponent>())
            {
                currentSurface = other.GetComponent<SurfaceIDComponent>().getType();
                Debug.Log(currentSurface);
            }
        }
    }
}
