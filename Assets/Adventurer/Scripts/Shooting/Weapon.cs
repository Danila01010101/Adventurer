using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] protected Animator animator;

    private float timeSinceLastShot = Mathf.Infinity;
    private bool isReloading = false;

    public void TryShoot()
    {
        if (CanShoot() == false) return;

        Shoot();
    }

    protected virtual void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    private bool CanShoot() => !isReloading && timeSinceLastShot > 1f / (weaponData.FireRate / 60f);

    protected virtual void Shoot()
    {
        timeSinceLastShot = 0;
    }
}
