using Constants;
using Controllers.EnemyControllers;
using UnityEngine;
using Utilities;
using Controllers.EnemyControllers;

namespace Controllers.BaseControllers {
    public class BaseSpellController : MonoBehaviour{
//        public GameObject objectPrefab;
        public int damage = 1;
        public int knockbackAmount = 10;
        public Vector2 attackRange = new Vector2(1, 1);
        public GameObject playerGameobject;
        public Vector2 moveDirection = Vector2.zero;
        public float moveSpeed = 10f;
        protected Rigidbody2D rigidBody;

        void Start(){
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(LayerConstants.PLAYER),LayerMask.NameToLayer(LayerConstants.WEAPONS), true);
            rigidBody = this.GetComponent<Rigidbody2D>();
        }

        void FixedUpdate() {
            if(this.gameObject.activeSelf){
                rigidBody.velocity = moveDirection * moveSpeed;
            }
        }

        public virtual void Cast(){

        }

        void OnCollisionEnter2D(Collision2D collision) {
            this.gameObject.SetActive(false);

            if(collision.gameObject.tag == TagConstants.ENEMY) {
                EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
                if (enemyController.hasShield) {
                    if(!enemyController.isStopped){
                        Vector2 attackDirection = AngleUtility.FindDirectionBetween2Points(collision.gameObject.transform.position, playerGameobject.transform.position);
                        enemyController.TakeDamage(damage, -attackDirection, knockbackAmount);
                    }
                } else {
                    Vector2 attackDirection = AngleUtility.FindDirectionBetween2Points(collision.gameObject.transform.position, playerGameobject.transform.position);
                    enemyController.TakeDamage(damage, -attackDirection, knockbackAmount);
                }
            }
        }
    }
}