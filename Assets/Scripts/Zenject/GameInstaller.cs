using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerStats>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}
