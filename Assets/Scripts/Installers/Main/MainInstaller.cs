using System.Diagnostics.Contracts;
using Humanoid;
using Zenject;

namespace Installer.Main
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Bind();
        }

        private void Bind()
        {
            Container.BindInterfacesTo<PlayerValues>().AsSingle();
            Container.BindInterfacesTo<GlobalValues>().AsSingle();
        }
    }
}