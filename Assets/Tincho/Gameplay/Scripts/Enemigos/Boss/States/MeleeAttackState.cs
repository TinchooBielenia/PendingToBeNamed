//using UnityEngine;
//using UnityEngine.AI;

//public class MeleeAttackState : BossState
//{

//    public MeleeAttackState(Boss boss) : base(boss) { }

//    public override void Enter()
//    {
//        Debug.Log("Entró en estado de ataque cuerpo a cuerpo");

//        boss._enemyAI.allowAIControl = false; 
//        boss._enemyAI.agent.isStopped = false;
//        boss._enemyAI.agent.SetDestination(boss.player.position);

//        boss.animator.SetTrigger("MeleHit");
//        boss.EnableHandColliders(true);
//        boss.SetWeaponActive(false);

//    }


//    public override void Tick()
//    {

//        if (boss.player != null)
//            boss._enemyAI.agent.SetDestination(boss.player.position);

//       /* if (meleeTimer >= meleeDuration)
//        {
//            boss._enemyAI.agent.isStopped = true;
//            boss.EnableHandColliders(false);
//            boss.ChangeState(new RangedAttackState(boss));
//        }*/
//    }

//    public override void Exit()
//    {
//        boss._enemyAI.allowAIControl = true; 
//        boss.EnableHandColliders(false);
//        boss.SetWeaponActive(true);
//    }
//}
