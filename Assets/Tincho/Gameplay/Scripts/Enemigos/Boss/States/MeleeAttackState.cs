using UnityEngine;

public class MeleeAtackState : BossState
{
    private float _stoppingDistance = 1.8f;
    //private float _attackCooldown = 2f;
    //private float _lastAttackTime;

    public MeleeAtackState(Boss boss) : base(boss) { }

    public override void Enter()
    {
        boss._enemyAI.allowAIControl = false;
        boss._enemyAI.agent.isStopped = false;
        boss.SetWeaponActive(false);

        boss.animator.SetBool("IsShooting", false);

        //_lastAttackTime = Time.time;
    }

    public override void Tick()
    {
        if (boss.player == null) return;

        float distance = Vector3.Distance(boss.transform.position, boss.player.position);

        if (distance > _stoppingDistance)
        {
            boss._enemyAI.agent.isStopped = false;
            boss._enemyAI.agent.SetDestination(boss.player.position);
            boss.animator.SetBool("IsWalking", true);
            boss.animator.SetBool("PunchingIdle", true);
            boss.animator.SetBool("IsPunching", false);
        }
        else
        {
            boss._enemyAI.agent.isStopped = true;
            RotateTowardsPlayer();

            boss.animator.SetBool("PunchingIdle", false);
            boss.animator.SetBool("IsWalking", false);
            boss.animator.SetBool("IsPunching", true);
        }
    }


    public override void Exit()
    {
        boss._enemyAI.allowAIControl = true;
        boss.SetWeaponActive(true);
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (boss.player.position - boss.transform.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
