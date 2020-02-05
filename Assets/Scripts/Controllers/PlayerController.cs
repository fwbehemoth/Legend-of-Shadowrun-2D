using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controllers {
    public class PlayerController : MonoBehaviour {
        public float moveSpeed = 5f;
        public Animator animator;
        public Rigidbody2D rigidBody;
        public GameObject weaponSlot;
        public GameObject weapon;

        Vector2 moveDirection;

        // Start is called before the first frame update
        void Start() {
            Move();
            weapon.transform.position = weaponSlot.transform.position;
        }

        // Update is called once per frame
        void Update() {
            if(Input.GetButtonDown("Attack") || Input.GetButton("Attack")){
                weapon.SetActive(true);
            } else {
                weapon.SetActive(false);
            }
        }

        void FixedUpdate() {
            Move();
        }

        void Move(){
            weapon.transform.position = weaponSlot.transform.position;
            moveDirection =  new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            rigidBody.velocity = moveDirection * moveSpeed;

            if(moveDirection != Vector2.zero) {
                animator.SetFloat("Horizontal", moveDirection.x);
                animator.SetFloat("Vertical", moveDirection.y);
                weapon.transform.rotation = Quaternion.LookRotation(Vector3.back, moveDirection);
//                Animator weaponAnimator = weapon.GetComponent<Animator>();
//                weaponAnimator.SetFloat("Horizontal", moveDirection.x);
//                weaponAnimator.SetFloat("Vertical", moveDirection.y);
            }
            animator.SetFloat("Speed", moveDirection.magnitude);
        }

        void Attack(){

        }

//        void OnTriggerEnter2D(Collider2D collider) {
//            Debug.Log(this.ToString() + "-collider-enter: " + collider.gameObject.name);
//        }
//
//        void OnTriggerStay2D(Collider2D collider) {
//            Debug.Log(this.ToString() + "-collider-stay: " + collider.gameObject.name);
//        }
//
//        void OnTriggerExit2D(Collider2D collider) {
//            Debug.Log(this.ToString() + "-collider-exit: " + collider.gameObject.name);
//        }
//
//        void OnCollisionEnter2D(Collision2D collision) {
//            Debug.Log(this.ToString() + "-collision: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
//        }
//
//        void OnCollisionStay2D(Collision2D collision) {
//            Debug.Log(this.ToString() + "-collision-stay: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
//        }
//
//        void OnCollisionExit2D(Collision2D hit) {
//            Debug.Log(this.ToString() + "-hit: " + hit.gameObject.name);
//        }
    }
}
