using System; 
using UnityEngine;
using R3;
using Covagame.LVH.Common;

namespace Covagame.LVH.Sample.Common
{
    public readonly struct ButtonHoldProperty
    {
        public readonly float ElapsedTime;
        public readonly Vector2 SwipeDirection;

        public ButtonHoldProperty(float elapsed, Vector2 direction)
        {
            ElapsedTime = elapsed;
            SwipeDirection = direction;
        }
    }
    public readonly struct ButtonReleaseProperty
    {
        public readonly bool IsInside;
        public readonly float ElapsedTime;
        public readonly Vector2 SwipeDirection;

        public ButtonReleaseProperty(bool isInside, float elapsed, Vector2 direction)
        {
            IsInside = isInside;
            ElapsedTime = elapsed;
            SwipeDirection = direction;
        }
    }
    
    public interface IButtonProperty : IHubProperty
    {
        ReactiveProperty<bool> IsEnabled { get; }
        Observable<Unit> OnPressed { get; }
        void RequestPress();
        Observable<ButtonHoldProperty> OnHold { get; }
        void RequestHold(ButtonHoldProperty property);
        
        Observable<ButtonReleaseProperty> OnRelease { get; }
        void RequestRelease(ButtonReleaseProperty property);
    }

    public class ButtonProperty : IButtonProperty
    {
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly ReactiveProperty<bool> _isEnabled = new(true);
        
        public void Dispose() => _disposable.Dispose();

        public ButtonProperty()
        {
            _isEnabled.AddTo(_disposable);
            _onPressed.AddTo(_disposable);
            _onHold.AddTo(_disposable);
            _onReleased.AddTo(_disposable);
        }
        // TODO: Implementation TProperty

        ReactiveProperty<bool> IButtonProperty.IsEnabled => _isEnabled;
        
        private readonly Subject<Unit> _onPressed = new();
        Observable<Unit> IButtonProperty.OnPressed => _onPressed;
        void IButtonProperty.RequestPress() => _onPressed.OnNext(Unit.Default);
        
        
        private readonly Subject<ButtonHoldProperty> _onHold = new();
        Observable<ButtonHoldProperty> IButtonProperty.OnHold  => _onHold;
        void IButtonProperty.RequestHold(ButtonHoldProperty property)=> _onHold.OnNext(property);
        
        
        private readonly Subject<ButtonReleaseProperty> _onReleased = new();
        Observable<ButtonReleaseProperty> IButtonProperty.OnRelease => _onReleased;
        void IButtonProperty.RequestRelease(ButtonReleaseProperty property)=> _onReleased.OnNext(property);
    }
}
