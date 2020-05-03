using System.Collections;
using Constants;
using UnityEngine;

namespace Controllers.BaseControllers {
    public class BaseCharacterController : MonoBehaviour {
        public float moveSpeed = 5f;
        public int hitPoints = 1;
        public bool tookDamage = false;

        protected Animator animator;
        protected Rigidbody2D rigidBody;
        protected Vector2 moveDirection;
        protected Vector2 damageVelocity;
        protected Vector2 moveToPosition;

        public virtual void TakeDamage(int damage, Vector2 attackDirection){}

        public virtual void Move(){}

        public virtual void FindDirection(){}

        public virtual IEnumerator PauseMovement(float seconds){
            MoveDirectionToZero();
            yield return new WaitForSeconds(seconds);
        }

        public virtual void MoveDirectionToZero(){
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0;
            animator.SetFloat("Speed", 0);
            moveDirection = Vector2.zero;
        }

//        void OnTriggerEnter2D(Collider2D collider) {
//            Debug.Log(this.ToString() + "-collider-enter: " + collider.gameObject.name);
//        }

//        void OnTriggerStay2D(Collider2D collider) {
//            Debug.Log(this.ToString() + "-collider-stay: " + collider.gameObject.name);
//        }

//        void OnTriggerExit2D(Collider2D collider) {
//            Debug.Log(this.ToString() + "-collider-exit: " + collider.gameObject.name);
//        }

        void OnCollisionEnter2D(Collision2D collision) {
            Debug.Log(this.ToString() + "-collision: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
            if(collision.gameObject.tag == TagConstants.BOUNDARIES && tookDamage){
                MoveDirectionToZero();
            }
        }

//        void OnCollisionStay2D(Collision2D collision) {
//            Debug.Log(this.ToString() + "-collision-stay: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
//        }

//        void OnCollisionExit2D(Collision2D hit) {
//            Debug.Log(this.ToString() + "-hit: " + hit.gameObject.name);
//        }
    }
}