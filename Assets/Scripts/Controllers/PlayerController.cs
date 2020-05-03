using System.Collections;
using Constants;
using Controllers.BaseControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers {
    public class PlayerController : BaseCharacterController {
        public Transform weaponSlot_up;
        public Transform weaponSlot_right_left;
        public Transform weaponSlot_down;
        public GameObject weapon;

        private Transform weaponSlot;
        private LayersReference layersReference;
        private BaseWeaponController weaponController;

        void Start() {
            animator = this.GetComponent<Animator>();
            rigidBody = this.GetComponent<Rigidbody2D>();
            Move();
            weaponSlot = weaponSlot_down;
            weaponController = weapon.GetComponent<BaseWeaponController>();
            weaponController.transform.position = weaponSlot.position;
            layersReference = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LayersReference>();
        }

        void Update() {
            if(Input.GetButtonDown("Attack") || Input.GetButton("Attack")){
                weapon.SetActive(true);
            } else {
                weapon.SetActive(false);
            }
        }

        void FixedUpdate() {
            moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Move();
            if (moveDirection == Vector2.down){
                weaponSlot = weaponSlot_down;
            } else if (moveDirection == Vector2.up){
                weaponSlot = weaponSlot_up;
            } else if (moveDirection == Vector2.left || moveDirection == Vector2.right){
                weaponSlot = weaponSlot_right_left;
            }
            weaponController.transform.position = weaponSlot.position;
        }

        public override void Move(){
            if(!tookDamage) {
                rigidBody.velocity = moveDirection * moveSpeed;

                if (moveDirection != Vector2.zero) {
                    animator.SetFloat("Horizontal", moveDirection.x);
                    animator.SetFloat("Vertical", moveDirection.y);
                    weapon.transform.rotation = Quaternion.LookRotation(Vector3.back, moveDirection);
                }
                animator.SetFloat("Speed", moveDirection.magnitude);
            }
        }

        void Attack(){

        }

//        void OnDrawGizmosSelected(){
//            if(weaponController == null) return;
//            Gizmos.DrawWireCube(weaponController.transform.position, weaponController.attackRange);
//        }

        public override void TakeDamage(int damage, Vector2 attackDirection) {
            hitPoints -= damage;
            if(hitPoints <= 0){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            } else {
                tookDamage = true;
                StartCoroutine(PauseMovement(0.5f));
            }
        }

        public override IEnumerator PauseMovement(float seconds) {
//            MoveDirectionToZero();
            yield return new WaitForSeconds(seconds);
            tookDamage = false;
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
            Debug.Log(this.ToString() + "-collision: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
            if(collision.gameObject.tag == TagConstants.BOUNDARIES){
                MoveDirectionToZero();
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
