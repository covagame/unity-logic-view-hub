using System; 
using UnityEngine;
using UnityEngine.UI;
using R3;
using Covagame.LVH.Common;
using Covagame.LVH.Sample.Common;

using Assert = UnityEngine.Assertions.Assert;

namespace Covagame.LVH.Sample.View
{
    public class ButtonView : MonoBehaviour, IView<IButtonProperty>
    {
        [SerializeField] private Button _targetButton = null;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private CompositeDisposable _bindingDisposable = new CompositeDisposable();
        private IButtonProperty _property;

        public void Dispose()
        {
            Unbind();
            _bindingDisposable.Dispose();
            _disposable.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        void Reset()
        {
            TryGetComponent(out _targetButton);
        }


        void Awake()
        {
            Assert.IsNotNull(_targetButton, "[ButtonView] _targetButton is NULL");
        }

        void IView<IButtonProperty>.Bind(IButtonProperty property)
        {
            Unbind();
            _property = property;

            _targetButton.onClick.AddListener(_property.RequestPress);
            _property.IsEnabled
                .Subscribe(isEnabled => _targetButton.interactable = isEnabled)
                .AddTo(_bindingDisposable);
        }

        private void Unbind()
        {
            if (_property == null || _targetButton == null)
            {
                return;
            }

            _targetButton.onClick.RemoveListener(_property.RequestPress);
            _property = null;
            _bindingDisposable.Dispose();
            _bindingDisposable = new CompositeDisposable();
        }
    }

}
