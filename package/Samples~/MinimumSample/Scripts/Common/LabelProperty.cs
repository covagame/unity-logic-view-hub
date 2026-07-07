using System; 
using UnityEngine;
using R3;
using Covagame.LVH.Common;

namespace Covagame.LVH.Sample.Common
{

    public class LabelProperty : ILabelProperty
    {
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly ReactiveProperty<string> _label = new(string.Empty);

        public void Dispose() => _disposable.Dispose();

        public LabelProperty()
        {
            _label.AddTo(_disposable);
        }

        ReactiveProperty<string> ILabelProperty.Label => _label;
    }

}
