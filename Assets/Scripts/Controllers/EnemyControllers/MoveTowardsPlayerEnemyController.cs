using System.Collections;
using Constants;
using UnityEngine;
using Utilities;

namespace Controllers.EnemyControllers {
    public class MoveTowardsPlayerEnemyController : EnemyController  {
        public float aggroDistance = 5f;
        public GameObject playerObject;

        Vector2 lastPosition;
        Vector2 playerPosition;
        GameObject aggroTriggerObject;

        void Start(){
            animator = this.GetComponent<Animator>();
            rigidBody = this.GetComponent<Rigidbody2D>();

            aggroTriggerObject = new GameObject();
            aggroTriggerObject.name = this.name + "-bounds";
            aggroTriggerObject.AddComponent<BoxCollider2D>();
            aggroTriggerObject.AddComponent<MoveTowardPlayerAggroTrigger>();
            aggroTriggerObject.tag = TagConstants.BOUNDARIES;

            BoxCollider2D boxCollider = aggroTriggerObject.GetComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
            boxCollider.size = new Vector2(aggroDistance, aggroDistance);

            MoveTowardPlayerAggroTrigger trigger = aggroTriggerObject.GetComponent<MoveTowardPlayerAggroTrigger>();
            trigger.playerObject = playerObject;
            trigger.enemyController = this;

            aggroTriggerObject.transform.parent = this.gameObject.transform;
            aggroTriggerObject.transform.position = this.gameObject.transform.position;

            lastPosition = this.gameObject.transform.position;
        }

        void Update(){
            playerPosition = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
        }

        public override void Move(){
            if(!tookDamage) {
                rigidBody.velocity = moveDirection * moveSpeed;
                animator.SetFloat("Speed", moveDirection.magnitude);
            }
        }

        public void CheckDirection(){
            Vector2 currentPosition = this.gameObject.transform.position;
            moveDirection = AngleUtility.FindDirectionBetween2Points(playerPosition, currentPosition);

            if (moveDirection != Vector2.zero) {
                animator.SetFloat("Horizontal", moveDirection.x);
                animator.SetFloat("Vertical", moveDirection.y);
            }
        }

        public override IEnumerator PauseMovement(float seconds){
            yield return base.PauseMovement(seconds);
            aggroTriggerObject.SetActive(true);
        }

        public override void TakeDamage(int damage, Vector2 attackDirection){
            base.TakeDamage(damage, attackDirection);
            StartCoroutine(PauseMovement(0.5f));
        }

        public override void MoveDirectionToZero(){
            base.MoveDirectionToZero();
            aggroTriggerObject.SetActive(false);
        }

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
    }
}