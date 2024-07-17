using Adventurer;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/NewWeapon")]
public class WeaponData : ItemData
{
    [Header("Shooting characteristics")]
    [SerializeField] private float damage;
    [SerializeField] private float maxDistance;

    [Header("Reloading characteristics")]
    [SerializeField] private int magazineSize;
    [SerializeField] private float fireRate;
    [SerializeField] private float reloadTime;

    #region PublicProperties
    public string WeaponName => Name;
    public float Damage => damage;
    public float MaxDistance => maxDistance;
    public int MagazineSize => magazineSize;
    public float FireRate => fireRate;
    public float ReloadTime => reloadTime;
    #endregion
}