using Constants;
using UnityEngine;

namespace Controllers {
    public class EnemyController : MonoBehaviour {
        public float moveSpeed = 5f;
        public Animator animator;
        public Rigidbody2D rigidBody;
        public Vector2 originPosition;
//        public Vector2 startPosition;
        public Vector2 finalPosition;
        public float boundariesDistance = 5f;
        float finalDistance = 0;

        Vector2 moveDirection;
        // Start is called before the first frame update
        void Start() {
            originPosition = this.transform.position;
            findDirection();
            GameObject bounds = new GameObject();
            bounds.name = this.name + "-bounds";
            bounds.AddComponent<BoxCollider2D>();
            bounds.tag = TagConstants.BOUNDARIES;
//            bounds.AddComponent<BoundTrigger>();

            BoxCollider2D boxCollider = bounds.GetComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
            boxCollider.size = new Vector2(boundariesDistance, boundariesDistance);
        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {
            Vector2 currentPosition = this.transform.position;
//            if(rigidBody.velocity.Equals(Vector2.zero) || currentPosition.x == originPosition.x || currentPosition.y == originPosition.y) {
//                findDirection();
//            if(Mathf.Abs(currentPosition.x) >= Mathf.Abs(finalPosition.x) || Mathf.Abs(currentPosition.y) >= Mathf.Abs(finalPosition.y)){
//                moveDirection = Vector2.zero;
//                Move();
//                findDirection();
//            }

            Move();
        }

        void Move(){
            rigidBody.velocity = finalPosition + (moveDirection * Time.fixedDeltaTime);

            if (moveDirection != Vector2.zero) {
                animator.SetFloat("Horizontal", moveDirection.x);
                animator.SetFloat("Vertical", moveDirection.y);
            }
            animator.SetFloat("Speed", moveDirection.magnitude);
        }

        void findDirection(){
            Vector2 newDirection = Vector2.zero;
            do{
                int randomDirection = Random.Range(0, 4);

                Vector2 currentPosition = this.transform.position;
                float randomDistance = 0f;

                switch (randomDirection) {
                    case 0:
                        newDirection = new Vector2(0, -1);
                        randomDistance = Random.Range(0, boundariesDistance - currentPosition.y);
                        finalPosition = new Vector2(0, -randomDistance);
                        break;

                    case 1:
                        newDirection = new Vector2(-1, 0);
                        randomDistance = Random.Range(0, boundariesDistance - currentPosition.x);
                        finalPosition = new Vector2(-randomDistance, 0);
                        break;

                    case 2:
                        newDirection = new Vector2(0, 1);
                        randomDistance = Random.Range(0, boundariesDistance + currentPosition.y);
                        finalPosition = new Vector2(0, randomDistance);
                        break;

                    case 3:
                        newDirection = new Vector2(1, 0);
                        randomDistance = Random.Range(0, boundariesDistance + currentPosition.x);
                        finalPosition = new Vector2(randomDistance, 0);
                        break;
                }
                Debug.Log("new directon: " + newDirection);
            }while (newDirection.Equals(moveDirection));

            moveDirection = Vector2.zero;
            moveDirection = newDirection;
            Debug.Log("Final Position: " + finalPosition);
        }

//        void OnTriggerEnter2D(Collider2D collider) {
//            Debug.Log(this.ToString() + "-collider-enter: " + collider.gameObject.name + "/tag:" + collider.gameObject.tag);
//        }
//
//        void OnTriggerStay2D(Collider2D collider) {
//            Debug.Log(this.ToString() + "-collider-stay: " + collider.gameObject.name + "/tag:" + collider.gameObject.tag);
//        }

        void OnTriggerExit2D(Collider2D collider) {
            Debug.Log(this.ToString() + "-collider-exit: " + collider.gameObject.name + "/tag:" + collider.gameObject.tag);
            if(collider.gameObject.tag == TagConstants.BOUNDARIES){
                moveDirection = Vector2.zero;
                Move();
                findDirection();
            }
        }

//        void OnCollisionEnter2D(Collision2D collision) {
//            Debug.Log(this.ToString() + "-collision-enter: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
//        }
//
//        void OnCollisionStay2D(Collision2D collision) {
//            Debug.Log(this.ToString() + "-collision-stay: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
//        }
//
//        void OnCollisionExit2D(Collision2D collision) {
//            Debug.Log(this.ToString() + "-collision-exit: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
//        }
    }
}
