using UnityEngine;
using EvolveGames;

namespace Adventurer.Shooting
{
    public class ProceduralRecoil : MonoBehaviour
    {
        [SerializeField] private Transform recoilCamera;
        [SerializeField] private PlayerController playerController;

        [Header("Parameters")]
        [SerializeField] private float recoilX;
        [SerializeField] private float recoilY;
        [SerializeField] private float recoilZ;
        [SerializeField] private float kickBackZ;
        [SerializeField] private float snappiness;
        [SerializeField] private float returnAmount;

        private Quaternion currentRotation, targetRotation;
        private Vector3 currentPosition, targetPosition;
        private Vector3 initialGunPosition;
        private System.Func<Quaternion> defaultRotation;

        private bool isPointing => playerController.IsControllingItem == false;

        public void Initialize(System.Func<Quaternion> defaultRot)
        {
            defaultRotation = defaultRot;
        }

        private void Start()
        {
            initialGunPosition = transform.localPosition;
            currentPosition = initialGunPosition;
            currentRotation = transform.localRotation;
        }

        private void Update()
        {
            if (isPointing == false)
                return;

            Stabilisation();
            Kickback();
        }

        public void Recoil()
        {
            targetPosition -= new Vector3(0, 0, kickBackZ);
            Quaternion recoilRotation = Quaternion.Euler(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));

            targetRotation = recoilRotation * targetRotation;
        }

        private void Stabilisation()
        {
            targetRotation = Quaternion.Lerp(targetRotation, defaultRotation(), Time.deltaTime * returnAmount);
            currentRotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * snappiness);
            transform.localRotation = currentRotation;
        }

        private void Kickback()
        {
            targetPosition = Vector3.Lerp(targetPosition, initialGunPosition, Time.deltaTime * returnAmount);
            currentPosition = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * snappiness);
            transform.localPosition = currentPosition;
        }
    }
}
