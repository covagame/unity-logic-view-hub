using System; 
using UnityEngine;
using TMPro;
using R3;
using Covagame.LVH.Common;
using Covagame.LVH.Sample.Common;

using Assert = UnityEngine.Assertions.Assert;

namespace Covagame.LVH.Sample.View
{

    public class LabelView : MonoBehaviour, IView<ILabelProperty>
    {
        [SerializeField] private TextMeshProUGUI _label;
        private CompositeDisposable _bindingDisposable = new CompositeDisposable();

        public void Dispose()
        {
            _bindingDisposable.Dispose();
        }
        
        private void OnDestroy()
        {
            Dispose();
        }

        private void Reset()
        {
            TryGetComponent(out _label);
        }
        private void Awake()
        {
            if (_label == null)
            {
                TryGetComponent(out _label);
            }

            Assert.IsNotNull(_label, "[LabelView] _label is NULL");
        }

        void IView<ILabelProperty>.Bind(ILabelProperty property)
        {
            _bindingDisposable.Dispose();
            _bindingDisposable = new CompositeDisposable();
            
            property.Label
                .Subscribe(text => _label.text = text)
                .AddTo(_bindingDisposable);
        }
    }

}
