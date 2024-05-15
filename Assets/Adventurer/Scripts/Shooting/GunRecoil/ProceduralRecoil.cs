using UnityEngine;

namespace Adventurer.Shooting
{
    public class ProceduralRecoil : MonoBehaviour
    {
        [SerializeField] private Transform recoilCamera;

        [Header("Parameters")]
        [SerializeField] private float recoilX;
        [SerializeField] private float recoilY;
        [SerializeField] private float recoilZ;
        [SerializeField] private float kickBackZ;
        [SerializeField] private float snappiness;
        [SerializeField] private float returnAmount;

        private Vector3 currentRotation, targetRotation;
        private Vector3 currentPosition, targetPosition;
        private Vector3 initialGunPosition;
        private System.Func<Vector3> defaultRotation;

        public void Initialize(System.Func<Vector3> GetRotation)
        {
            defaultRotation = GetRotation;
        }

        private void Start()
        {
            initialGunPosition = transform.localPosition;
        }

        private void Update()
        {
            targetRotation = Vector3.Lerp(targetRotation, defaultRotation(), Time.deltaTime * returnAmount);
            recoilCamera.localRotation = Quaternion.Euler(currentRotation);
            Kickback();
        }

        private void FixedUpdate()
        {
            currentRotation = Vector3.Slerp(currentRotation, targetRotation, Time.fixedDeltaTime * snappiness);
            transform.localRotation = Quaternion.Euler(currentRotation);
        }

        public void Recoil()
        {
            targetPosition -= new Vector3(0, 0, kickBackZ);
            targetRotation += new Vector3(recoilX,
                Random.Range(-recoilY, recoilY),
                Random.Range(-recoilZ, recoilZ));
        }

        private void Kickback()
        {
            targetPosition = Vector3.Lerp(targetPosition, initialGunPosition, Time.deltaTime * returnAmount);
            currentPosition = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * snappiness);
            transform.localPosition = currentPosition;
        }
    }
}