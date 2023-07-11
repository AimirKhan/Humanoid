using UniRx;

namespace Humanoid
{
    public class PlayerWeaponValues : IPlayerWeaponValues
    {
        private ReactiveProperty<int> ammoCount = new();
        public ReactiveProperty<int> AmmoCount => ammoCount;
    }

    internal interface IPlayerWeaponValues
    {
        ReactiveProperty<int> AmmoCount { get; }
    }
}
