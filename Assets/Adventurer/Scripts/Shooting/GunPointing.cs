using UnityEngine;

namespace Adventurer.Shooting
{
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

        private void Start()
        {
            recoil.Initialize(GetCurrentDirection);
        }
        public Quaternion GetCurrentDirection()
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            int layerMask = (1 << 0);

            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                //debug
                Debug.Log(hit.collider.gameObject.name);
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 200, Color.red);
                Debug.DrawRay(bulletSpawnPosition.position, bulletSpawnPosition.transform.forward * 200, Color.red);
                aimGizmosPosition = hit.point;

                Vector3 direction = (hit.point - bulletSpawnPosition.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction, gunUpwardsDirection);

                // ��������� ���������� ������� � ��������� ������������ bulletSpawnPosition
                Quaternion localRotation = Quaternion.Inverse(transform.parent.rotation) * lookRotation;

                return localRotation;
            }

            return Quaternion.identity;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(aimGizmosPosition, 0.2f);
        }
    }
}