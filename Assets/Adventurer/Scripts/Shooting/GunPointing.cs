using Adventurer.Shooting;
using UnityEngine;

public class GunPointing : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private ProceduralRecoil recoil;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private Vector3 defaultRotation;
    [SerializeField] private Vector3 gunUpwardsDirection = Vector3.up;
    [SerializeField] private Transform testAimTransform;

    private Vector3 aimGizmosPosition;

    private void Start()
    {
        recoil.Initialize(GetCurrentDirection);
    }

    public Vector3 GetCurrentDirection()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(bulletSpawnPosition.transform.position, bulletSpawnPosition.transform.forward * 200, Color.red);
            aimGizmosPosition = hit.point;
            return defaultRotation;
            return Quaternion.LookRotation(hit.point, gunUpwardsDirection).eulerAngles;
        }

        return defaultRotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(aimGizmosPosition, 0.2f);
    }
}