using System.Collections;
using Constants;
using Controllers.BaseControllers;
using UnityEngine;

namespace Controllers.EnemyControllers {
    public class EnemyController : BaseCharacterController {
        public int contactDamage = 1;
        public int attackDamage = 1;

        void OnCollisionEnter2D(Collision2D collision) {
            if(collision.gameObject.tag == TagConstants.PLAYER){
                MoveDirectionToZero();
                PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
                playerController.TakeDamage(contactDamage, Vector2.zero);
                StartCoroutine(PauseMovement(0.5f));
            } else if(collision.gameObject.tag == TagConstants.BOUNDARIES && tookDamage){
                MoveDirectionToZero();
            }
        }

        public override void TakeDamage(int damage, Vector2 attackDirection){
            tookDamage = true;
            hitPoints -= damage;
            if(hitPoints <= 0){
                Destroy(this.gameObject);
            }
            MoveDirectionToZero();
            rigidBody.AddForce(attackDirection.normalized * 300f, ForceMode2D.Force);
        }

        public override IEnumerator PauseMovement(float seconds){
            yield return base.PauseMovement(seconds);
            MoveDirectionToZero();
            tookDamage = false;
        }
    }
}
