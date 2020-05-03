using Constants;
using Controllers.EnemyControllers;
using UnityEngine;
using Utilities;

namespace Controllers.BaseControllers {
    public class BaseWeaponController : MonoBehaviour{
        public int damage = 1;
        public Vector2 attackRange = new Vector2(1, 1);
        public GameObject playerGameobject;

        void Start(){
            Physics2D.IgnoreLayerCollision(10,11, true);
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
                Vector2 attackDirection = AngleUtility.FindDirectionBetween2Points(collision.gameObject.transform.position, playerGameobject.transform.position);
                Debug.Log(this + ": Attack Direction = " + attackDirection);
                enemyController.TakeDamage(damage, attackDirection);
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