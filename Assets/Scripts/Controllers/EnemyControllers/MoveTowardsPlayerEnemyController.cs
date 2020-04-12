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
//            rigidBody.MovePosition(Vector2.MoveTowards(this.gameObject.transform.position, playerPosition, moveSpeed * Time.fixedDeltaTime));
//            this.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, playerPosition, moveSpeed * Time.fixedDeltaTime);
            rigidBody.velocity = moveDirection * moveSpeed;
            animator.SetFloat("Speed", moveDirection.magnitude);
        }

//        public override void TakeDamage(int damage, Vector2 orignatorsPosition){
//
//        }

        void OnCollisionEnter2D(Collision2D collision) {
//            Debug.Log(this.ToString() + "-collision-enter: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
            if(collision.gameObject.tag == TagConstants.PLAYER){
                PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
                playerController.TakeDamage(contactDamage, moveDirection);
//                damageVelocity = moveDirection * -1;
//                Debug.Log(this + "-Damage Velocity = " + damageVelocity);
//                rigidBody.AddForce(damageVelocity, ForceMode2D.Impulse);
                StartCoroutine(PauseMovement(2));
//                OnContact();
            }
        }

        public void CheckDirection(){
//            moveDirection = Vector2.zero;

            Vector2 currentPosition = this.gameObject.transform.position;
            float angle = AngleUtility.FindAngle(playerPosition.y-currentPosition.y, playerPosition.x-currentPosition.x);

            if (angle > 315.1 || angle < 45) {
                moveDirection = Vector2.right;
            } else if (angle > 45.1 && angle < 135) {
                moveDirection = Vector2.up;
            } else if (angle > 135.1 && angle < 225) {
                moveDirection = Vector2.left;
            } else if (angle > 225.1 && angle < 315) {
                moveDirection = Vector2.down;
            }
//            Debug.Log("Move Direction: " + moveDirection);
            if (moveDirection != Vector2.zero) {
                animator.SetFloat("Horizontal", moveDirection.x);
                animator.SetFloat("Vertical", moveDirection.y);
            }
        }

        public override void OnContact(){
            Debug.Log("OnContact");
            aggroTriggerObject.SetActive(!aggroTriggerObject.activeSelf);
        }

        public override IEnumerator PauseMovement(int seconds){
            yield return new WaitForSeconds(seconds);
            aggroTriggerObject.SetActive(!aggroTriggerObject.activeSelf);
            FindDirection();
        }
    }
}