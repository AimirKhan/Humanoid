using Humanoid.Input;
using UnityEngine;
using Zenject;

public class MainPrefabsInstaller : MonoInstaller
{
    //[SerializeField] private TouchController touchController;
    
    public override void InstallBindings()
    {
        //Container.BindInterfacesAndSelfTo<TouchController>().FromInstance(touchController).AsSingle();
    }
}