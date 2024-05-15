using Adventurer.Shooting;
using ModestTree.Util;
using UnityEngine;

public class GunPointing : MonoBehaviour
{
    [SerializeField] private ProceduralRecoil recoil;

    public Vector3 currentDirection = Vector3.zero;
    public Vector3 GetCurrentDirection() => currentDirection;

    private void Start()
    {
        recoil.Initialize(GetCurrentDirection);
    }
}