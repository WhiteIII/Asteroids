using _Project.Scripts.Gameplay.Enemies.Base;
using _Project.Scripts.Gameplay.Services.ObjectPools.Base;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Enemies.Base
{
    public abstract class Character : 
        MonoBehaviour,
        ICharacter,
        IEnableAndDisableItem
    {
        public Vector2 Position => transform.position;
        
        public void Enable() => 
            gameObject.SetActive(true);

        public void Disable() => 
            gameObject.SetActive(false);
    }
}