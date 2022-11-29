using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomLib
{
    public static class ExtensionMethod
    {
        public static void Divide(ref this Vector3 curVec, Vector3 vec)
        {
            curVec.x /= vec.x;
            curVec.y /= vec.y;
            curVec.z /= vec.z;
        }
    }
}
