using System.Collections;
using Constants;
using UnityEngine;
using Utilities;

namespace Controllers.EnemyControllers.Stationary {
    public class StationaryMoveTowardsPlayerEnemyController : StationaryEnemyController  {
        public float aggroDistance = 5f;
        public GameObject playerObject;

        Vector2 lastPosition;

        public bool aggro = false;
        protected GameObject aggroTriggerObject;

        void Start(){
            GameObject sprite = ObjectUtils.FindChildByName(this.gameObject, "Sprite");
            animator = sprite.GetComponent<Animator>();
            rigidBody = this.GetComponent<Rigidbody2D>();

            aggroTriggerObject = new GameObject();
            aggroTriggerObject.name = this.name + "-bounds";
            aggroTriggerObject.AddComponent<BoxCollider2D>();
            aggroTriggerObject.AddComponent<StationaryMoveTowardsPlayerAggroTrigger>();
            aggroTriggerObject.tag = TagConstants.BOUNDARIES;

            BoxCollider2D boxCollider = aggroTriggerObject.GetComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
            boxCollider.size = new Vector2(aggroDistance, aggroDistance);

            StationaryMoveTowardsPlayerAggroTrigger trigger = aggroTriggerObject.GetComponent<StationaryMoveTowardsPlayerAggroTrigger>();
            trigger.playerObject = playerObject;
            trigger.enemyController = this;

            aggroTriggerObject.transform.parent = this.gameObject.transform;
            aggroTriggerObject.transform.position = this.gameObject.transform.position;

            lastPosition = this.gameObject.transform.position;
        }

        void Update(){
            playerPosition = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
            if(tookDamage) {
                rigidBody.velocity = -attackDirection * toBeKnockbacked;
            }
        }

        public override void Move(){
            if(!tookDamage) {
                rigidBody.velocity = moveDirection * moveSpeed;
                animator.SetFloat("Speed", moveDirection.magnitude);
            }
        }

        public override IEnumerator PauseMovement(float seconds){
            yield return base.PauseMovement(seconds);
            aggroTriggerObject.SetActive(true);
        }

        public override void MoveDirectionToZero(){
            base.MoveDirectionToZero();
            aggroTriggerObject.SetActive(false);
        }

        void OnCollisionEnter2D(Collision2D collision) {
            if(collision.gameObject.tag == TagConstants.PLAYER){
                PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
                playerController.TakeDamage(contactDamage, AngleUtility.FindDirectionBetween2Points(this.transform.position, playerController.transform.position), knockbackAmount);
            } else if(collision.gameObject.tag == TagConstants.BOUNDARIES){
                MoveDirectionToZero();
                StartCoroutine(PauseMovement(0.5f));
            }
        }
    }
}