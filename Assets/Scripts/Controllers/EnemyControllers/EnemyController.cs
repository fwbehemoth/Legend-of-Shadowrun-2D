using System.Collections;
using Constants;
using Controllers.BaseControllers;
using UnityEngine;

namespace Controllers.EnemyControllers {
    public class EnemyController : BaseCharacterController {
        public int contactDamage = 1;
        public int attackDamage = 1;

        public virtual void OnContact(){}

//        void OnTriggerEnter2D(Collider2D collider) {
//            Debug.Log(this.ToString() + "-collider-enter: " + collider.gameObject.name + "/tag:" + collider.gameObject.tag);
//        }

//        void OnTriggerStay2D(Collider2D collider) {
//            Debug.Log(this.ToString() + "-collider-stay: " + collider.gameObject.name + "/tag:" + collider.gameObject.tag);
//        }

//        void OnTriggerExit2D(Collider2D collider) {
//            Debug.Log(this.ToString() + "-collider-exit: " + collider.gameObject.name + "/tag:" + collider.gameObject.tag);
//        }

        void OnCollisionEnter2D(Collision2D collision) {
//            Debug.Log(this.ToString() + "-collision-enter: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
            if(collision.gameObject.tag == TagConstants.PLAYER){
                OnContact();
                PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
                playerController.TakeDamage(contactDamage, collision.relativeVelocity);
                StartCoroutine(PauseMovement(2));
            }
        }

        public virtual IEnumerator PauseMovement(int seconds){
            MoveDirectionToZero();
            yield return new WaitForSeconds(seconds);
        }

//        void OnCollisionStay2D(Collision2D collision) {
//            Debug.Log(this.ToString() + "-collision-stay: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
//            if(collision.gameObject.tag == TagConstants.PLAYER){
//                PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
//                playerController.TakeDamage(contactDamage);
//            }
//        }

//        void OnCollisionExit2D(Collision2D collision) {
//            Debug.Log(this.ToString() + "-collision-exit: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
//        }
    }
}
