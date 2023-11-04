using System.Collections;
using UnityEngine;

public class MachineGun : Weapon
{
    [Header("Effects")]
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private ParticleSystem impactParticles;
    [Space(20)]
    [SerializeField] private Transform bulletSpawnPoint;
    [Header("Parameters")]
    [SerializeField] private float reloadTime;
    [SerializeField] private int ammoAmount;

    private float bulletSpeed = 100;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TryShoot();
        }
    }

    protected override void Shoot()
    {
        base.Shoot();

        Vector3 direction = GetShootDirection();
        if (Physics.Raycast(bulletSpawnPoint.position, direction, out RaycastHit hit, 100))
        {
            TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);

            trail.transform.position = bulletSpawnPoint.position;
            StartCoroutine(SpawnTrail(trail, hit, hit.normal, true));
        }
    }

    private Vector3 GetShootDirection()
    {
        Vector3 direction = transform.forward;
        //recoil
        Vector3.Normalize(direction);
        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit, Vector3 hitNormal, bool MadeImpact)
    {
        //float time = 0;
        //Vector3 startPosition = trail.transform.position;
        //Vector3 endPosition = hit.point;

        //Instantiate(impactParticles, hit.point, Quaternion.LookRotation(hit.normal));
        //while (time < 1)
        //{
        //    trail.transform.position += Vector3.Lerp(startPosition, endPosition, time);
        //    time += Time.deltaTime/trail.time;
        //    yield return null;
        //}
        //trail.transform.position = hit.point;
        //Destroy(trail.gameObject, trail.time);


        Vector3 startPosition = trail.transform.position;
        float distance = Vector3.Distance(trail.transform.position, hit.point);
        float remainingDistance = distance;
        //trail.enabled = true;

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
}
