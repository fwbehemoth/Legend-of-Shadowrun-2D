using System.Collections.Generic;
using UnityEngine;

namespace Utilities {
    public static class LayersUtil {
        public static Dictionary<string, int> GetUserLayers(){
            Dictionary<string, int> layers = new Dictionary<string, int>();
            for (int i = 0; i <= 31; i++){
                string layerName = LayerMask.LayerToName(i);
                if (layerName.Length > 0) {
                    Debug.Log("Layers: " + i + " - " + layerName);
                    layers.Add(layerName, i);
                }
            }
            return layers;
        }
    }
}