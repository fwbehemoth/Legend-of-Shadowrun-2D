using UnityEngine;

namespace Utilities {
    public class ObjectUtils {
        public static GameObject FindChildByName(GameObject parent, string name){
            foreach (Transform child in parent.transform){
                if (child.name.Equals(name)){
                    return child.gameObject;
                }
            }

            return null;
        }
    }
}