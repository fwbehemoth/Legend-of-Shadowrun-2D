using System.Collections;
using Constants;
using Controllers.BaseControllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Controllers {
    public class PlayerController : BaseCharacterController {
        public Transform weaponSlotUp;
        public Transform weaponSlotRightLeft;
        public Transform weaponSlotDown;
        public GameObject weapon;

        public Transform spellSlotUpDown;
        public Transform spellSlotRight;
        public Transform spellSlotLeft;
        public GameObject spell;

        private Transform weaponSlot;
        private BaseWeaponController weaponController;

        private Transform spellSlot;
        private BaseSpellController spellController;

        void Start() {
            GameObject sprite = ObjectUtils.FindChildByName(this.gameObject, "Sprite");
            animator = sprite.GetComponent<Animator>();
            rigidBody = this.GetComponent<Rigidbody2D>();
            Move();

            weaponSlot = weaponSlotDown;
            weaponController = weapon.GetComponent<BaseWeaponController>();
            weaponController.transform.position = weaponSlot.position;

            spellSlot = spellSlotUpDown;
            spellController = spell.GetComponent<BaseSpellController>();
            spellController.transform.position = spellSlot.position;

            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(LayerConstants.ENEMIES),LayerMask.NameToLayer(LayerConstants.ENEMIES), true);
        }

        void Update() {
            if(Input.GetButtonDown("Attack") || Input.GetButton("Attack")){
                weapon.SetActive(true);
            } else {
                weapon.SetActive(false);
            }

            if(canCastSpell) {
                if ((Input.GetButtonDown("Fire2") || Input.GetButton("Fire2")) && !spell.activeSelf) {
                    Debug.Log("Fire2 True");
                    CastSpell();
                }
            }
        }

        void FixedUpdate() {
            moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if(!spell.activeSelf && !moveDirection.Equals(Vector2.zero)) {
                spellController.moveDirection = moveDirection;
            }
            Move();

            if (moveDirection == Vector2.down){
                weaponSlot = weaponSlotDown;
                spellSlot = spellSlotUpDown;
            } else if (moveDirection == Vector2.up){
                weaponSlot = weaponSlotUp;
                spellSlot = spellSlotUpDown;
            } else if (moveDirection == Vector2.left){
                weaponSlot = weaponSlotRightLeft;
                spellSlot = spellSlotLeft;
            } else if( moveDirection == Vector2.right){
                weaponSlot = weaponSlotRightLeft;
                spellSlot = spellSlotRight;
            }

            if(moveDirection != Vector2.zero){
                directionFacing = moveDirection;
            }

            weaponController.transform.position = weaponSlot.position;
        }

        public override void Move(){
            if(!tookDamage && !isPushBack) {
                rigidBody.velocity = moveDirection * moveSpeed;

                if (moveDirection != Vector2.zero) {
                    animator.SetFloat("Horizontal", moveDirection.x);
                    animator.SetFloat("Vertical", moveDirection.y);
                    weapon.transform.rotation = Quaternion.LookRotation(Vector3.back, moveDirection);
                }
                animator.SetFloat("Speed", moveDirection.magnitude);
            } else {
                rigidBody.velocity = -attackDirection * toBeKnockbacked;
            }
        }

        void Attack() {

        }

        public override void TakeDamage(int damage, Vector2 attackDirection, int toBeKnockbacked) {
            hitPoints -= damage;
            this.attackDirection = attackDirection;
            this.toBeKnockbacked = toBeKnockbacked;
            if(hitPoints <= 0){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            } else {
                tookDamage = true;
                StartCoroutine(PauseMovement(0.5f));
                StartCoroutine(TempInvincibility(0.5f));
            }
        }

        public override void PushBack(Vector2 moveDirection, int toBeKnockbacked){
            isPushBack = true;
            this.attackDirection = attackDirection;
            this.toBeKnockbacked = toBeKnockbacked;
            StartCoroutine(PauseMovement(0.2f));
        }

        public override IEnumerator PauseMovement(float seconds) {
            yield return new WaitForSeconds(seconds);
            tookDamage = false;
            isPushBack = false;
        }

        public override IEnumerator TempInvincibility(float seconds){
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(LayerConstants.PLAYER),LayerMask.NameToLayer(LayerConstants.ENEMIES), true);
            yield return new WaitForSeconds(seconds);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(LayerConstants.PLAYER),LayerMask.NameToLayer(LayerConstants.ENEMIES), false);
        }

        private void CastSpell(){
            spell.transform.position = spellSlot.position;
            spell.SetActive(true);
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
//                MoveDirectionToZero();
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
