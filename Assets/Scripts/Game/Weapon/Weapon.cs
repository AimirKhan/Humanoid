using System;
using System.Collections;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Humanoid.Weapon
{
    public enum EAmmoType
    {
        Ammo762,
        Ammo545
    }
    
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private EAmmoType ammoType;
        [SerializeField] private int magSize;
        [Tooltip("Fire Rate in milliseconds")]
        [SerializeField] private float fireRate;
        [Tooltip("Reloading time in seconds")]
        [SerializeField] private float reloadTime;
        [SerializeField] private int damage;
        [SerializeField] private string soundName;
        [SerializeField] private InputActionReference shootInput;

        protected bool IsCanShoot { get; private set; } = true;
        //protected int MagCount { get; private set; }
        protected readonly ReactiveProperty<int> MagCount = new();
        
        protected Camera PlayerCamera { get; private set; }

        private PlayerValues _playerValues;
        private PlayerWeaponValues _playerWeaponValues;
        
        public void Init(PlayerValues playerValues,
            PlayerWeaponValues playerWeaponValues)
        {
            _playerValues = playerValues;
            PlayerCamera = playerValues.PlayerCamera;
            _playerWeaponValues = playerWeaponValues;
            MagCount.Subscribe(ctx => playerWeaponValues.AmmoCount.Value = ctx).AddTo(this);
        }

        private void OnEnable()
        {
            //shootInput.action.performed += OnPlayerShoot;
            Observable.EveryUpdate()
                .Where(ctx => shootInput.action.IsPressed())
                .Subscribe(ctx => OnPlayerShoot())
                .AddTo(this);
            MagCount.Value = magSize;
        }

        protected virtual void OnPlayerShoot()
        {
        
        }

        protected void OnStartShoot()
        {
            MagCount.Value--;
            _playerValues.PlayAnimation.Execute(EPlayerAnimation.Fire);
            // soundPlayer.Play(soundName, 1);
            StartCoroutine(FireRPM(fireRate));
        }

        private IEnumerator FireRPM(float delayInSec)
        {
            IsCanShoot = false;
            yield return new WaitForSeconds(delayInSec * .01f);
            IsCanShoot = true;
        }

        protected IEnumerator Reloading()
        {
            //TODO Add reloading animations, sounds
            yield return new WaitForSeconds(reloadTime);
            MagCount.Value = magSize;
        }
    }
}