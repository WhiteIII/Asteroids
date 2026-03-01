using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Characters.Base;
using R3;

namespace _Project.Scripts.Gameplay.GameProgress
{
    public class PointsAndKillsCounter : IPointsAndKillsCounterCounter
    {
        public ReadOnlyReactiveProperty<int> Points => _points;

        private readonly ReactiveProperty<int> _points = new();
        private readonly Dictionary<Type, int> _kills = new();

        public int GetKills<T>() where T : Character
        {
            if (TypeContains(typeof(T)))
                return _kills[typeof(T)];
            return 0;
        }
        
        public void AddKill<T>(int points) 
            where T : Character
        {
            _points.Value += points;
            if (TypeContains(typeof(T)))
            {
                _kills[typeof(T)]++;
                return;
            }
            
            _kills.Add(typeof(T), 1);
        }

        private bool TypeContains(Type characterType) 
        {
            foreach (Type type in _kills.Keys)
            {
                if (characterType == type)
                    return true;
            }
            return false;
        }
    }

    public interface IPointsAndKillsCounterCounter
    {
        ReadOnlyReactiveProperty<int> Points { get; }
        int GetKills<T>() where T : Character;
    }
}