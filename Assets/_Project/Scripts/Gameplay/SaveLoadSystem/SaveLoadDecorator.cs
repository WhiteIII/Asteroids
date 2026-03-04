using System;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using Zenject;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SaveLoadSystem
{
    public class SaveLoadDecorator : ISaveLoadAsync
    {
        private readonly ISaveLoad _localSaveLoad;
        private readonly ISaveLoadAsync _remoteSaveLoad;
        private readonly SaveLoadView _view;
        
        private bool ThereIsAnInternetConnection => Application.internetReachability != NetworkReachability.NotReachable;
        
        public SaveLoadDecorator(
            ISaveLoad localSaveLoad,
            ISaveLoadAsync remoteSaveLoad, 
            SaveLoadView view)
        {
            _localSaveLoad = localSaveLoad;
            _remoteSaveLoad = remoteSaveLoad;
            _view = view;
        }
        
        public async UniTask<PlayerSaveLoadData> LoadAsync()
        {
            PlayerSaveLoadData localData = _localSaveLoad.Load();
            
            if (ThereIsAnInternetConnection)
            {
                PlayerSaveLoadData remoteData = await _remoteSaveLoad.LoadAsync();

                if (localData.DataTime > remoteData.DataTime)
                {
                    SaveDataType saveDataType = await _view.StartSelectionAsync(localData.DataTime, remoteData.DataTime);
                    switch (saveDataType)
                    {
                        case SaveDataType.Local:
                            await _remoteSaveLoad.SaveAsync(localData);
                            return localData;
                        case SaveDataType.Remote:
                            _localSaveLoad.Save(remoteData);
                            return remoteData;
                    }
                }
                
                return remoteData;
            }
            
            return localData;            
        }

        public async UniTask SaveAsync(PlayerSaveLoadData data)
        {
            DateTime nowDateTime = DateTime.Now; 
            data.DataTime = nowDateTime;
            _localSaveLoad.Save(data);
            if (ThereIsAnInternetConnection)
                await _remoteSaveLoad.SaveAsync(data);
        }
    }
}