using System.Collections;
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

        public virtual void TakeDamage(int damage, Vector2 dealerMoveDirection){
//            hitPoints -= damage;
//            tookDamage = true;
//            damageVelocity = dealerMoveDirection * 20;
//            Debug.Log(this + "-Damage Velocity = " + damageVelocity);
//            rigidBody.AddForce(damageVelocity, ForceMode2D.Impulse);
        }

        public virtual void Move(){}

        public virtual void FindDirection(){}

        public virtual IEnumerator PauseMovement(int seconds){
            yield return new WaitForSeconds(seconds);
            MoveDirectionToZero();
        }

        public void MoveDirectionToZero(){
//            rigidBody.velocity = Vector2.zero;
//            rigidBody.angularVelocity = 0;
            animator.SetFloat("Speed", 0);
            moveDirection = Vector2.zero;
        }
    }
}