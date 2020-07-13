using Constants;
using UnityEngine;

namespace Controllers.EnemyControllers.Stationary {
    public class StationaryMoveTowardsPlayerAggroTrigger : MonoBehaviour {
        public GameObject playerObject;
        public EnemyController enemyController;

        void OnTriggerExit2D(Collider2D collider) {
            if(collider.gameObject.tag == TagConstants.PLAYER){
                enemyController.MoveDirectionToZero();
            }
        }

        void OnTriggerStay2D(Collider2D collider) {
            if(collider.gameObject.tag == TagConstants.PLAYER) {
                enemyController.CheckDirection();
                enemyController.Move();
            }
        }
    }
}