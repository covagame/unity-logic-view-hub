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

}