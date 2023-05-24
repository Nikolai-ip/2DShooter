using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float distance;
    [SerializeField] protected float bulletVelocity;
    [SerializeField] protected float dispersion;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float delayBetweenShots;
    [SerializeField] protected int maxBulletCount;
    protected int bulletCount;
    public event Action<int> OnBulletCountChanged;
    public int BulletCount { 
        get { return bulletCount; }
        private set
        {
            bulletCount = value;
            OnBulletCountChanged?.Invoke(bulletCount);
        }
    }
    public int MaxBulletCount => maxBulletCount;
    protected Transform tr;
    protected bool canShoot = true;
    public float GetDistance => distance;
    protected BulletContainer bulletContainer;
    protected virtual void Start()
    {
        bulletContainer = FindObjectOfType<BulletContainer>();
        tr = transform;
        bulletCount = maxBulletCount;
    }
    public virtual void Shoot(Vector2 target)
    {
        if (canShoot && BulletCount > 0)
        {
            canShoot = false;
            Bullet bullet = bulletContainer.Pool.GetFreeElement();
            bullet.Damage = damage;
            bullet.Velocity = bulletVelocity;
            bullet.transform.position = shootPoint.position;
            Vector3 dir = (target - new Vector2(shootPoint.position.x, shootPoint.position.y)).normalized;
            float randomDispersion = UnityEngine.Random.Range(-dispersion, dispersion);
            dir.x *= Mathf.Cos(randomDispersion * Mathf.Deg2Rad);
            dir.y += Mathf.Sin(randomDispersion * Mathf.Deg2Rad);
            bullet.Direction = dir;
            bullet.transform.rotation = Quaternion.Euler(tr.rotation.x, tr.rotation.y, tr.localEulerAngles.z - 90);
            BulletCount--;
            Invoke(nameof(SetCanShoot), delayBetweenShots);
        }
    }
    protected void SetCanShoot()
    {
        canShoot = true;
    }
    public void RotateWeapon(Vector2 target, float rotateAngle)
    {
        Vector3 diff = new Vector3(target.x, target.y) - tr.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        tr.rotation = Quaternion.Euler(0f, 0f, rot_z - rotateAngle);
        Flip();
    }
    private void Flip()
    {
        if (Mathf.Abs(tr.localEulerAngles.z) > 90)
        {
            tr.localScale = new Vector3(1,-1,1);
        }
        else
        {
            tr.localScale = new Vector3(1, 1, 1);
        }
    }

}
