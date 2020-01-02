using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers {
    public class PlayerController : MonoBehaviour {
        float horizontalMove = 0f;
        float verticalMove = 0f;
        public float moveSpeed = 5f;
        public Animator animator;
        public Rigidbody2D rigidbody;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {
            Vector2 moveDirection =  new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

//            moveSpeed = Mathf.Clamp(moveDirection.magnitude, 0.0f, 1.0f);
            rigidbody.velocity = moveDirection * moveSpeed;

            if(moveDirection != Vector2.zero) {
                animator.SetFloat("Horizontal", moveDirection.x);
                animator.SetFloat("Vertical", moveDirection.y);
            }
            animator.SetFloat("Speed", moveDirection.magnitude);
        }
    }
}
