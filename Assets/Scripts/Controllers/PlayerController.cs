using System.Collections;
using Constants;
using Controllers.BaseControllers;
using UnityEngine;

namespace Controllers {
    public class PlayerController : BaseCharacterController {
        public GameObject weaponSlot;
        public GameObject weapon;

        // Start is called before the first frame update
        void Start() {
            animator = this.GetComponent<Animator>();
            rigidBody = this.GetComponent<Rigidbody2D>();
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

        public override void Move(){
            if(!tookDamage) {
                weapon.transform.position = weaponSlot.transform.position;
                moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
//                rigidBody.velocity = moveDirection * moveSpeed;
                Vector2 thisPoisiton = transform.position;
                transform.position = thisPoisiton + (moveDirection * moveSpeed) * Time.deltaTime;
//                rigidBody.MovePosition(thisPoisiton + (moveDirection * moveSpeed) * Time.deltaTime);
//
                if (moveDirection != Vector2.zero) {
                    animator.SetFloat("Horizontal", moveDirection.x);
                    animator.SetFloat("Vertical", moveDirection.y);
                    weapon.transform.rotation = Quaternion.LookRotation(Vector3.back, moveDirection);
                }
                animator.SetFloat("Speed", moveDirection.magnitude);
            } else {
//                rigidBody.MovePosition(moveToPosition + damageVelocity * Time.deltaTime);
            }
        }

        void Attack(){

        }

        public override void TakeDamage(int damage, Vector2 dealerMoveDirection) {
//            base.TakeDamage(damage, dealerMoveDirection);
            hitPoints -= damage;
            tookDamage = true;
//            Debug.Log("Dealer Move Direction: " + dealerMoveDirection);
//            damageVelocity = dealerMoveDirection;
//            Debug.Log(this + "-Damage Velocity = " + damageVelocity);
//            Vector2 thisPoisiton = transform.position;
//            moveToPosition = thisPoisiton + (dealerMoveDirection * 5);

//            damageVelocity = dealerMoveDirection;
//            Debug.Log(this + "-Damage Velocity = " + damageVelocity);
//            rigidBody.AddForce(damageVelocity, ForceMode2D.Impulse);
            StartCoroutine(PauseMovement(2));
        }

        public override IEnumerator PauseMovement(int seconds) {
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
                moveDirection = Vector2.zero;
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
