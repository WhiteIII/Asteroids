using _Project.Scripts.View.Implementation;
using UnityEngine;
using Zenject;

public class MenuWindowFactory : PlaceholderFactory<MainMenuWindow>
{
    private readonly GameObject _menuPrefab;
    private readonly IInstantiator _instantiator;
    private readonly Transform _menuParent;

    public MenuWindowFactory(GameObject menuPrefab, IInstantiator instantiator, Transform menuParent)
    {
        _menuPrefab = menuPrefab;
        _instantiator = instantiator;
        _menuParent = menuParent;
    }

    public override MainMenuWindow Create() => 
        _instantiator.InstantiatePrefab(_menuPrefab, _menuParent).GetComponent<MainMenuWindow>();
}
