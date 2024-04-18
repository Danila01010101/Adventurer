using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] private float shootDelay = 0.05f;

    private float lastTimeShot = -1000f;

    public void TryShoot()
    {
        if (lastTimeShot + shootDelay > Time.time) return;
        Shoot();
    }

    protected virtual void Shoot()
    {
        lastTimeShot = Time.time;
    }
}
