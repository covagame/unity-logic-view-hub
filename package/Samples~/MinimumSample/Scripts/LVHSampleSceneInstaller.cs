using System;
using Covagame.LVH.Common;
using Covagame.LVH.Sample.Common;
using Covagame.LVH.Sample.Logic;
using Covagame.LVH.Sample.View;
using R3;
using UnityEngine;

namespace Covagame.LVH.Sample
{
    public sealed class LVHSampleSceneInstaller : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();

        private void Awake()
        {
            var canvas = FindFirstObjectByType<Canvas>() ?? LVHSampleViewFactory.CreateCanvas();
            var root = LVHSampleViewFactory.CreatePanel(canvas.transform);

            var buttonProperty = new ButtonProperty().AddTo(_disposable);
            var stateLabelProperty = new LabelProperty().AddTo(_disposable);
            var swipeDirectionLabelProperty = new LabelProperty().AddTo(_disposable);
            var holdTimeLabelProperty = new LabelProperty().AddTo(_disposable);
            var releaseOutsideLabelProperty = new LabelProperty().AddTo(_disposable);

            new ButtonInputViewLogic(
                buttonProperty,
                stateLabelProperty,
                swipeDirectionLabelProperty,
                holdTimeLabelProperty,
                releaseOutsideLabelProperty).AddTo(_disposable);

            var buttonView = LVHSampleViewFactory.CreateButton(root, "Input Button");
            ((IView<IButtonProperty>)buttonView).Bind(buttonProperty);

            BindLabel(LVHSampleViewFactory.CreateLabel(root), stateLabelProperty);
            BindLabel(LVHSampleViewFactory.CreateLabel(root), swipeDirectionLabelProperty);
            BindLabel(LVHSampleViewFactory.CreateLabel(root), holdTimeLabelProperty);
            BindLabel(LVHSampleViewFactory.CreateLabel(root), releaseOutsideLabelProperty);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        private static void BindLabel(LabelView view, ILabelProperty property)
        {
            ((IView<ILabelProperty>)view).Bind(property);
        }
    }
}
