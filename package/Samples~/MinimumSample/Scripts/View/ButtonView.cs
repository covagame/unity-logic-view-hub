using System; 
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using R3;
using Covagame.LVH.Common;
using Covagame.LVH.Sample.Common;

using Assert = UnityEngine.Assertions.Assert;

namespace Covagame.LVH.Sample.View
{
    public class ButtonView : MonoBehaviour, IView<IButtonProperty>, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private Button _targetButton = null;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private CompositeDisposable _bindingDisposable = new CompositeDisposable();
        private IButtonProperty _property;
        private RectTransform _rectTransform;
        private bool _isPressing;
        private float _pressStartTime;
        private Vector2 _pressStartPosition;
        private Vector2 _currentPointerPosition;

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
            if (_targetButton == null)
            {
                TryGetComponent(out _targetButton);
            }

            _rectTransform = transform as RectTransform;
            Assert.IsNotNull(_targetButton, "[ButtonView] _targetButton is NULL");
            Assert.IsNotNull(_rectTransform, "[ButtonView] RectTransform is NULL");
        }

        private void Update()
        {
            if (!_isPressing || _property == null)
            {
                return;
            }

            _property.RequestHold(CreateHoldProperty());
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

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_property == null)
            {
                return;
            }

            _isPressing = true;
            _pressStartTime = Time.time;
            _pressStartPosition = eventData.position;
            _currentPointerPosition = eventData.position;
            _property.RequestHold(CreateHoldProperty());
        }

        public void OnDrag(PointerEventData eventData)
        {
            _currentPointerPosition = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_property == null)
            {
                return;
            }

            _currentPointerPosition = eventData.position;
            _isPressing = false;

            var isInside = RectTransformUtility.RectangleContainsScreenPoint(
                _rectTransform,
                eventData.position,
                eventData.pressEventCamera);

            _property.RequestRelease(new ButtonReleaseProperty(
                isInside,
                Time.time - _pressStartTime,
                GetSwipeDirection()));
        }

        private ButtonHoldProperty CreateHoldProperty()
        {
            return new ButtonHoldProperty(Time.time - _pressStartTime, GetSwipeDirection());
        }

        private Vector2 GetSwipeDirection()
        {
            var delta = _currentPointerPosition - _pressStartPosition;
            return delta.sqrMagnitude <= Mathf.Epsilon ? Vector2.zero : delta.normalized;
        }
    }

}
