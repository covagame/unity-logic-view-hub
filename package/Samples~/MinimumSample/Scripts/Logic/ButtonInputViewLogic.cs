using System; 
using UnityEngine;
using R3;
using Covagame.LVH.Sample.Common;

namespace Covagame.LVH.Sample.Logic
{
    public class ButtonInputViewLogic : IDisposable
    {
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly ILabelProperty _stateLabel;
        private readonly ILabelProperty _swipeDirectionLabel;
        private readonly ILabelProperty _holdTimeLabel;
        private readonly ILabelProperty _releaseOutsideLabel;

        public void Dispose() => _disposable.Dispose();

        public ButtonInputViewLogic(
            IButtonProperty buttonProperty,
            ILabelProperty stateLabel,
            ILabelProperty swipeDirectionLabel,
            ILabelProperty holdTimeLabel,
            ILabelProperty releaseOutsideLabel)
        {
            _stateLabel = stateLabel;
            _swipeDirectionLabel = swipeDirectionLabel;
            _holdTimeLabel = holdTimeLabel;
            _releaseOutsideLabel = releaseOutsideLabel;

            SetInitialLabels();

            buttonProperty.OnPressed
                .Subscribe(_ => _stateLabel.Label.Value = "State: Pressed")
                .AddTo(_disposable);

            buttonProperty.OnHold
                .Subscribe(OnHold)
                .AddTo(_disposable);

            buttonProperty.OnRelease
                .Subscribe(OnRelease)
                .AddTo(_disposable);
        }

        private void SetInitialLabels()
        {
            _stateLabel.Label.Value = "State: Idle";
            _swipeDirectionLabel.Label.Value = "SwipeDirection: (0.00, 0.00)";
            _holdTimeLabel.Label.Value = "Hold Time: 0.00s";
            _releaseOutsideLabel.Label.Value = "Released Outside: -";
        }

        private void OnHold(ButtonHoldProperty property)
        {
            _stateLabel.Label.Value = "State: Holding";
            _swipeDirectionLabel.Label.Value = $"SwipeDirection: ({property.SwipeDirection.x:0.00}, {property.SwipeDirection.y:0.00})";
            _holdTimeLabel.Label.Value = $"Hold Time: {property.ElapsedTime:0.00}s";
        }

        private void OnRelease(ButtonReleaseProperty property)
        {
            _stateLabel.Label.Value = "State: Released";
            _swipeDirectionLabel.Label.Value = $"SwipeDirection: ({property.SwipeDirection.x:0.00}, {property.SwipeDirection.y:0.00})";
            _holdTimeLabel.Label.Value = $"Hold Time: {property.ElapsedTime:0.00}s";
            _releaseOutsideLabel.Label.Value = $"Released Outside: {!property.IsInside}";
        }
    }

}
