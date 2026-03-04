using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.View.Implementation
{
    public class SaveSelectionWindow : Window<SaveSelectionViewModel>
    {
        [Header("Buttons:")]
        [SerializeField] private Button _localSaveSelectionButton;
        [SerializeField] private Button _remoteSaveSelectionButton;
        [Header("Texts:")] 
        [SerializeField] private TMP_Text _localSaveButtonText;
        [SerializeField] private TMP_Text _remoteSaveButtonText;
        
        private Subject<SaveDataType> _saveDataObservable = new();

        protected override void OnSetup()
        {
            _localSaveSelectionButton
                .OnClickAsObservable()
                .Subscribe(_ => _saveDataObservable.OnNext(SaveDataType.Local))
                .AddTo(this);
            _remoteSaveSelectionButton
                .OnClickAsObservable()
                .Subscribe(_ => _saveDataObservable.OnNext(SaveDataType.Remote))
                .AddTo(this);
            ViewModel
                .LocalDateTime
                .Subscribe(dataTime => _localSaveButtonText.text = "Save time: " + dataTime)
                .AddTo(this);
            ViewModel
                .RemoteDateTime
                .Subscribe(dataTime => _remoteSaveButtonText.text = "Save time: " + dataTime)
                .AddTo(this);
            
            ViewModel.SetObservable(_saveDataObservable);
        } 
    }
}