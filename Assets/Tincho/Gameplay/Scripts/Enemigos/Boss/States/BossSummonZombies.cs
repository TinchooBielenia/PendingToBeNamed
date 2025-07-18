using System.Collections;
using UnityEngine;

public class BossSummonZombies : BossState
{
    private bool hasSummoned = false;

    public BossSummonZombies(Boss boss) : base(boss) { }

    public override void Enter()
    {
        if (!hasSummoned && boss.enemySpawner != null)
        {
            boss.enemySpawner.StartSpawning(boss.zombiesToSpawn);
            hasSummoned = true;
        }

        boss.StartCoroutine(WaitThenReturnToShoot());
    }

    private IEnumerator WaitThenReturnToShoot()
    {
        yield return new WaitForSeconds(3f);
        boss.ChangeState(new RangedAttackState(boss));
    }
}
