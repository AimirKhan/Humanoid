using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Humanoid.Weapon
{
    public class HumanoidController : MonoBehaviour
    {
        [Inject] private IPlayerValues playerValues;
        [Inject] private IPlayerWeaponValues playerWeaponValues;
        [SerializeField] private GameObject weaponPrefab;
        [SerializeField] private Transform weaponOrigin;

        public Transform WeaponOrigin => weaponOrigin;

        private PlayerValues playerValuess;
    
        private void Start()
        {
            var weapon = Instantiate(weaponPrefab, weaponOrigin); //TODO remade
            var initWeapon = weapon.GetComponent<Weapon>();
            initWeapon.Init((PlayerValues)playerValues,
                (PlayerWeaponValues)playerWeaponValues);
        }
    }
}