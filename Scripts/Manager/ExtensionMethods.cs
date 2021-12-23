using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static void globalScale(this Transform transform, Vector3 vector3) // Utile nel caso di un oggetto su una piattaforma. 
        {
            Transform parent = transform.parent;
            transform.SetParent(null);
            transform.localScale = vector3;
            transform.SetParent(parent);
        }
    }
}
