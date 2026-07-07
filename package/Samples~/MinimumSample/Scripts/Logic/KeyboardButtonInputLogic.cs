using System;
using Covagame.LVH.Sample.Common;
using R3;
using UnityEngine;

namespace Covagame.LVH.Sample.Logic
{
    public sealed class KeyboardButtonInputLogic : IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        private readonly IButtonProperty _buttonProperty;
        private readonly KeyCode _keyCode;
        private bool _isPressing;
        private float _pressStartTime;

        public KeyboardButtonInputLogic(IButtonProperty buttonProperty, KeyCode keyCode = KeyCode.Space)
        {
            _buttonProperty = buttonProperty;
            _keyCode = keyCode;

            Observable.EveryUpdate()
                .Subscribe(_ => Tick())
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void Tick()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                _isPressing = true;
                _pressStartTime = Time.time;
                _buttonProperty.RequestPress();
                _buttonProperty.RequestHold(CreateHoldProperty());
                return;
            }

            if (_isPressing && Input.GetKey(_keyCode))
            {
                _buttonProperty.RequestHold(CreateHoldProperty());
                return;
            }

            if (_isPressing && Input.GetKeyUp(_keyCode))
            {
                _isPressing = false;
                _buttonProperty.RequestRelease(new ButtonReleaseProperty(
                    true,
                    Time.time - _pressStartTime,
                    Vector2.zero));
            }
        }

        private ButtonHoldProperty CreateHoldProperty()
        {
            return new ButtonHoldProperty(Time.time - _pressStartTime, Vector2.zero);
        }
    }
}
