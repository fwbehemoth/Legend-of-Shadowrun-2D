using UnityEngine;

namespace Controllers {
    public class EnemyController : MonoBehaviour {
        public float moveSpeed = 5f;
        public Animator animator;
        public Rigidbody2D rigidBody;
        public Vector2 startPosition;
        public Vector2 finalPosition;
        public float boundariesDistance = 5f;

        Vector2 moveDirection;
        // Start is called before the first frame update
        void Start() {
            startPosition = this.transform.position;
        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {
            Vector2 currentPosition = this.transform.position;
            if(rigidBody.velocity.Equals(Vector2.zero) || currentPosition.Equals(startPosition)) {
                findDirection();
            }

            Move();
        }

        void Move(){
            rigidBody.MovePosition(finalPosition + moveDirection);

            if (moveDirection != Vector2.zero) {
                animator.SetFloat("Horizontal", moveDirection.x);
                animator.SetFloat("Vertical", moveDirection.y);
            }
            animator.SetFloat("Speed", moveDirection.magnitude);
        }

        void findDirection(){
            int randomDirection = Random.Range(0, 4);
            Vector2 currentPosition = this.transform.position;
            float randomDistance = 0f;

            switch (randomDirection){
                case 0:
                    moveDirection = new Vector2(0, -1);
                    randomDistance = Random.Range(0, boundariesDistance - currentPosition.y);
                    finalPosition = new Vector2(currentPosition.x, -randomDistance);
                    break;

                case 1:
                    moveDirection =  new Vector2(-1, 0);
                    randomDistance = Random.Range(0, boundariesDistance - currentPosition.x);
                    finalPosition = new Vector2(-randomDistance, currentPosition.y);
                    break;

                case 2:
                    moveDirection =  new Vector2(0, 1);
                    randomDistance = Random.Range(0, boundariesDistance + currentPosition.y);
                    finalPosition = new Vector2(currentPosition.x, randomDistance);
                    break;

                case 3:
                    moveDirection =  new Vector2(1, 0);
                    randomDistance = Random.Range(0, boundariesDistance + currentPosition.x);
                    finalPosition = new Vector2(randomDistance, currentPosition.y);
                    break;
            }
            Debug.Log("Final Position: " + finalPosition);
        }
    }
}
