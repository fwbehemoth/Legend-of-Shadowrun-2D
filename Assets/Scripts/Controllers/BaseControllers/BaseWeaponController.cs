using Constants;
using Controllers.EnemyControllers;
using UnityEngine;
using Utilities;
using Controllers.EnemyControllers.Stationary;
using Controllers.EnemyControllers;

namespace Controllers.BaseControllers {
    public class BaseWeaponController : MonoBehaviour{
        public int damage = 1;
        public int knockbackAmount = 10;
//        public Vector2 attackRange = new Vector2(1, 1);
        public GameObject playerGameobject;

        void Start(){
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(LayerConstants.PLAYER),LayerMask.NameToLayer(LayerConstants.WEAPONS), true);
        }

        void FixedUpdate() {

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
            if(collision.gameObject.tag != TagConstants.PLAYER){
                Debug.Log(this.ToString() + "-collision: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
            }

            if(collision.gameObject.tag == TagConstants.ENEMY){
                EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
                Vector2 attackDirection = AngleUtility.FindDirectionBetween2Points(playerGameobject.transform.position, collision.gameObject.transform.position);
                if(enemyController.isStopped && enemyController.hasShield){
                    playerGameobject.GetComponent<PlayerController>().PushBack(attackDirection, enemyController.clangBack);
                } else {
                    enemyController.TakeDamage(damage, attackDirection, knockbackAmount);
                }
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