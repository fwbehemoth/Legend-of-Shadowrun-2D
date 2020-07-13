using UnityEngine;

namespace Controllers.EnemyControllers.Roaming {
    public class RoamingShieldedWhenStoppedEnemyController : RoamingEnemyController{
        public override void TakeDamage(int damage, Vector2 attackDirection, int toBeKnockbacked){
            if(!isStopped){
                base.TakeDamage(damage, attackDirection, toBeKnockbacked);
            }
        }
    }
}