using UnityEngine;

namespace Controllers.EnemyControllers.Roaming {
    public class RoamingThenMoveEnemyController : RoamingEnemyController{
        public float aggroDistance = 5f;
        public GameObject playerObject;

//        GameObject aggroTriggerObject;

        void Start(){
            animator = this.GetComponent<Animator>();
            rigidBody = this.GetComponent<Rigidbody2D>();

//            AddAggroController();

//            aggroTriggerObject = new GameObject();
//            aggroTriggerObject.name = this.name + "-bounds";
//            aggroTriggerObject.AddComponent<BoxCollider2D>();
//            aggroTriggerObject.AddComponent<MoveTowardsPlayerAggroTrigger>();
//            aggroTriggerObject.tag = TagConstants.BOUNDARIES;
//
//            BoxCollider2D boxCollider = aggroTriggerObject.GetComponent<BoxCollider2D>();
//            boxCollider.isTrigger = true;
//            boxCollider.size = new Vector2(aggroDistance, aggroDistance);
//
//            MoveTowardsPlayerAggroTrigger trigger = aggroTriggerObject.GetComponent<MoveTowardsPlayerAggroTrigger>();
//            trigger.playerObject = playerObject;
//            trigger.enemyController = this;
//
//            aggroTriggerObject.transform.parent = this.gameObject.transform;
//            aggroTriggerObject.transform.position = this.gameObject.transform.position;

            lastPosition = this.gameObject.transform.position;

            originPosition = this.transform.position;
            boundaryVector = new Vector2(originPosition.x + boundariesDistance, originPosition.y + boundariesDistance);
            FindDirection();
        }
    }
}