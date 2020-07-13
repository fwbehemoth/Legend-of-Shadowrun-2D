using System.Collections;
using Constants;
using Controllers.BaseControllers;
using UnityEngine;
using Utilities;
using Controllers.EnemyControllers.Stationary;
using Controllers.EnemyControllers;

namespace Controllers.EnemyControllers {
    public class EnemyController : BaseCharacterController {
        public int contactDamage = 1;
        public int attackDamage = 1;
        public int knockbackAmount = 10;
        public bool isStopped = false;

        protected Vector2 playerPosition;

        public bool hasShield = false;
        public int clangBack = 2;

        void OnCollisionEnter2D(Collision2D collision) {
            if(collision.gameObject.tag == TagConstants.PLAYER){
                MoveDirectionToZero();
                PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
                playerController.TakeDamage(contactDamage, AngleUtility.FindDirectionBetween2Points(this.transform.position, playerController.transform.position), knockbackAmount);
                StartCoroutine(PauseMovement(0.5f));
            } else if(collision.gameObject.tag == TagConstants.BOUNDARIES){
                MoveDirectionToZero();
                StartCoroutine(PauseMovement(0.5f));
            }
        }

        public override void TakeDamage(int damage, Vector2 attackDirection, int toBeKnockbacked){
            tookDamage = true;
            hitPoints -= damage;
            this.attackDirection = attackDirection;
            this.toBeKnockbacked = toBeKnockbacked;
            if(hitPoints <= 0){
                Destroy(this.gameObject);
            } else {
                tookDamage = true;
                StartCoroutine(PauseMovement(0.5f));
            }
        }

        public override IEnumerator PauseMovement(float seconds){
            yield return base.PauseMovement(seconds);
            MoveDirectionToZero();
            tookDamage = false;
        }

        public void CheckDirection(){
            Vector2 currentPosition = this.gameObject.transform.position;
            moveDirection = AngleUtility.FindDirectionBetween2Points(playerPosition, currentPosition);

            if (moveDirection != Vector2.zero) {
                animator.SetFloat("Horizontal", moveDirection.x);
                animator.SetFloat("Vertical", moveDirection.y);
            }
        }
    }
}
