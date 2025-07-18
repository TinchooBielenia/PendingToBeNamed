using UnityEngine;

public class RangedAttackState : BossState
{
    float cooldown = 2f;
    float lastShotTime = -10f;

    public RangedAttackState(Boss boss) : base(boss) { }

    public override void Enter()
    {
        boss.animator.SetBool("IsShooting",true);

        boss._lifeBarPrefab.SetActive(true);
        boss.transform.rotation = Quaternion.Euler(0f, 70f, 0f);

    }

    public override void Tick()
    {
        Vector3 targetDirection = boss.player.position - boss.transform.position;
        targetDirection.y = 0f; 
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        if (Time.time > lastShotTime + cooldown)
        {
            boss.isShooting = true;
            boss.rifleSFX.Play();
            GameObject bullet = GameObject.Instantiate(boss.bulletPrefab, boss.firePoint.position, Quaternion.identity);
            Vector3 direction = (boss.player.position - boss.firePoint.position).normalized;
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
            lastShotTime = Time.time;
        }
    }
    public override void Exit()
    {
        base.Exit();
        boss.isShooting = false;
        boss.animator.SetBool("IsShooting", false);
        boss.transform.rotation = Quaternion.identity;
    }
}
