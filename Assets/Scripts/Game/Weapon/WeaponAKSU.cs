using UnityEngine;
using UnityEngine.InputSystem;

namespace Humanoid.Weapon
{
    public class WeaponAKSU : Weapon
    {
        private RaycastHit hit;

        protected override void OnPlayerShoot()
        {
            base.OnPlayerShoot();
            if (MagCount.Value > 0 && IsCanShoot)
            {
                OnStartShoot();

                var point = new Vector3(PlayerCamera.pixelWidth / 2, PlayerCamera.pixelHeight / 2);
                var ray = PlayerCamera.ScreenPointToRay(point);
                if (Physics.Raycast(ray, out hit))
                {
                    HitIndicator(hit.point);
                }
            }
            else if (MagCount.Value == 0) StartCoroutine(Reloading());
        }

        private void HitIndicator(Vector3 pos)
        {
            var hitObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            hitObject.transform.position = pos;
            hitObject.transform.localScale *= .1f;
            
            Destroy(hitObject, 1);
        }
    }
}