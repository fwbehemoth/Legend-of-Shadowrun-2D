using System.Collections;
using UnityEngine;
using Utilities;

namespace Controllers.EnemyControllers.Roaming {
    public class RoamingEnemyController : EnemyController {
        protected Vector2 lastPosition;
        protected Vector2 finalPosition;

        public float boundariesDistance = 5f;

        protected Vector2 boundaryVector;
        protected Vector2 originPosition;

        void Start() {
            GameObject sprite = ObjectUtils.FindChildByName(this.gameObject, "Sprite");
            animator = sprite.GetComponent<Animator>();
            rigidBody = this.GetComponent<Rigidbody2D>();

            originPosition = this.transform.position;
            boundaryVector = new Vector2(originPosition.x + boundariesDistance, originPosition.y + boundariesDistance);
            FindDirection();
        }

        void FixedUpdate() {
            Move();

            if(moveDirection != Vector2.zero){
                directionFacing = moveDirection;
            }
        }

        public override void Move(){
            if(!tookDamage) {
                if(!isStopped) {
                    rigidBody.MovePosition(Vector2.MoveTowards(this.gameObject.transform.position, finalPosition, moveSpeed * Time.fixedDeltaTime));
                }
                if (Vector2.Distance(rigidBody.position, finalPosition) <= 0.1f) {
                    if (!isStopped) {
                        isStopped = true;
                        StartCoroutine(PauseMovement(2));
                    }
                } else {
                    animator.SetFloat("Horizontal", moveDirection.x);
                    animator.SetFloat("Vertical", moveDirection.y);
                    animator.SetFloat("Speed", moveDirection.magnitude);
                }
            } else {
                rigidBody.velocity = -attackDirection * toBeKnockbacked;
            }
        }

        public override void FindDirection(){
            Vector2 newDirection = Vector2.zero;
            int randomDirection = Random.Range(0, 4);

            Vector2 currentPosition = this.transform.position;
            float randomDistance = 0f;

            switch (randomDirection) {
                case 0:
                    newDirection = Vector2.down;
                    randomDistance = NewRandomDistance(currentPosition.x);
                    finalPosition = new Vector2(0, -randomDistance);
                    break;

                case 1:
                    newDirection = Vector2.left;
                    randomDistance = NewRandomDistance(currentPosition.x);
                    finalPosition = new Vector2(-randomDistance, 0);
                    break;

                case 2:
                    newDirection = Vector2.up;
                    randomDistance = NewRandomDistance(currentPosition.y);
                    finalPosition = new Vector2(0, randomDistance);
                    break;

                case 3:
                    newDirection = Vector2.right;
                    randomDistance = NewRandomDistance(currentPosition.y);
                    finalPosition = new Vector2(randomDistance, 0);
                    break;
            }

            moveDirection = Vector2.zero;
            moveDirection = newDirection;
            isStopped = false;
        }

        float NewRandomDistance(float startPostion){
            float newDistance = Random.Range(2, boundariesDistance + startPostion);
            if(newDistance > boundariesDistance) newDistance = boundariesDistance;
            return newDistance;
        }

        public override IEnumerator PauseMovement(float seconds){
            yield return base.PauseMovement(seconds);
            FindDirection();
        }
    }
}
