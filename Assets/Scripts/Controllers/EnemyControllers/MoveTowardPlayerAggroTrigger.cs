using Constants;
using UnityEngine;

namespace Controllers.EnemyControllers {
    public class MoveTowardPlayerAggroTrigger : MonoBehaviour {
        public GameObject playerObject;
        public MoveTowardsPlayerEnemyController enemyController;

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