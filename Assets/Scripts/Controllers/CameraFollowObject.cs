using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers.EnemyControllers.Stationary;
using Controllers.EnemyControllers;

namespace Controllers {
    public class CameraFollowObject : MonoBehaviour {
        public Transform targetTransform;
        public float zOffSet = 0;

        void LateUpdate() {
            Vector3 followVector = new Vector3();
            followVector.x = targetTransform.position.x;
            followVector.y = this.transform.position.y;
            followVector.z = targetTransform.position.z + zOffSet;
            this.transform.position = followVector;
        }
    }
}
