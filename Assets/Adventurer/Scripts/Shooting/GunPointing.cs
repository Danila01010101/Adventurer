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
    [SerializeField] private LayerMask pointingLayerMask;

    private Vector3 aimGizmosPosition;
    private Transform pointingExample;

    private void Start()
    {
        recoil.Initialize(GetCurrentDirection);
        pointingExample = new GameObject("PointingExample").transform;
        pointingExample.SetParent(bulletSpawnPosition.parent.parent);
        pointingExample.transform.localPosition = Vector3.zero;
    }

    public Vector3 GetCurrentDirection()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        int layerMask = (1 << 0);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            Debug.Log(hit.collider.gameObject.name);
            pointingExample.LookAt(hit.point);
            Debug.DrawRay(pointingExample.transform.position, pointingExample.transform.forward * 200, Color.red);
            aimGizmosPosition = hit.point;
            return pointingExample.localEulerAngles;
            return Quaternion.LookRotation(hit.point, gunUpwardsDirection).eulerAngles;
        }

        return defaultRotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(aimGizmosPosition, 0.2f);
    }
}