using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sonido
{
    public class SurfaceIDComponent : MonoBehaviour
    {
        [SerializeField]
        private Sonido.SurfaceID type = SurfaceID.Def;

        public Sonido.SurfaceID getType() { return type; }
    }
}