using Constants;
using UnityEngine;

namespace Controllers.EnemyControllers {
    public class MoveTowardPlayerAggroTrigger : MonoBehaviour {
        public GameObject playerObject;
        public MoveTowardsPlayerEnemyController enemyController;

//        void OnTriggerEnter2D(Collider2D collider) {
//            Debug.Log(this.ToString() + "-collider-exit: " + collider.gameObject.name + "/tag:" + collider.gameObject.tag);
//            if(collider.gameObject.tag == TagConstants.PLAYER) {
//                enemyController.Move();
//            }
//        }

        void OnTriggerExit2D(Collider2D collider) {
//            Debug.Log(this.name + "-collider-exit: " + collider.gameObject.name + "/tag:" + collider.gameObject.tag);
            if(collider.gameObject.tag == TagConstants.PLAYER){
                enemyController.MoveDirectionToZero();
            }
        }

        void OnTriggerStay2D(Collider2D collider) {
//            Debug.Log(this.name + "-collider-stay: " + collider.gameObject.name + "/tag:" + collider.gameObject.tag);
            if(collider.gameObject.tag == TagConstants.PLAYER) {
                enemyController.Move();
                enemyController.CheckDirection();
            }
        }
    }
}