using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollisionDetection{
    public class BoundTrigger : MonoBehaviour {
        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        void OnTriggerEnter2D(Collider2D collider) {
            Debug.Log(this.ToString() + "-collider-enter: " + collider.gameObject.name);
        }

        void OnTriggerStay2D(Collider2D collider) {
            Debug.Log(this.ToString() + "-collider-stay: " + collider.gameObject.name);
        }

        void OnTriggerExit2D(Collider2D collider) {
            Debug.Log(this.ToString() + "-collider-exit: " + collider.gameObject.name);
        }

        void OnCollisionEnter2D(Collision2D collision) {
            Debug.Log(this.ToString() + "-collision: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
        }

        void OnCollisionStay2D(Collision2D collision) {
            Debug.Log(this.ToString() + "-collision-stay: " + collision.gameObject.name + "/tag:" + collision.gameObject.tag);
        }

        void OnCollisionExit2D(Collision2D hit) {
            Debug.Log(this.ToString() + "-hit: " + hit.gameObject.name);
        }
    }
}
