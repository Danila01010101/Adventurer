using EvolveGames;
using System.Collections;
using UnityEngine;

public class MachineGun : Weapon
{
    [Header("Effects")]
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private ParticleSystem flashParticles;
    [SerializeField] private ParticleSystem impactParticles;
    [Space(20)]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private HeadBob headBob;
    [Header("Parameters")]
    [SerializeField] private float reloadTime;
    [SerializeField] private int ammoAmount;
    [SerializeField] private float recoilValue = -0.2f;

    private float bulletSpeed = 100;
    private bool isAiming = false;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TryShoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            SwitchAimMode();
        }
    }

    protected override void Shoot()
    {
        base.Shoot();
        Instantiate(flashParticles, bulletSpawnPoint.position, bulletSpawnPoint.transform.rotation, bulletSpawnPoint);
        Vector3 direction = GetShootDirection();
        if (Physics.Raycast(bulletSpawnPoint.position, direction, out RaycastHit hit, 100))
        {
            TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);

            trail.transform.position = bulletSpawnPoint.position;
            StartCoroutine(SpawnTrail(trail, hit, hit.normal, true));
        }
        playerController.PunchHead(Vector3.left * recoilValue);
        headBob.ShowHeadPunch(Vector3.left * recoilValue, 0.09f);
    }

    private Vector3 GetShootDirection()
    {
        Vector3 direction = transform.forward;
        Vector3.Normalize(direction);
        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit, Vector3 hitNormal, bool MadeImpact)
    {
        Vector3 startPosition = trail.transform.position;
        float distance = Vector3.Distance(trail.transform.position, hit.point);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, 1 - (remainingDistance / distance));

            remainingDistance -= bulletSpeed * Time.deltaTime;

            yield return null;
        }
        trail.transform.position = hit.point;
        if (MadeImpact)
        {
            Instantiate(impactParticles, hit.point, Quaternion.LookRotation(hitNormal));
        }

        Destroy(trail.gameObject, trail.time);
    }

    public void SwitchAimMode()
    {
        isAiming = !isAiming;
        animator.SetBool("IsAiming", isAiming);
    }
}
