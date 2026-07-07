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

            var buttonProperty = new ButtonProperty();
            var stateLabelProperty = new LabelProperty();
            var swipeDirectionLabelProperty = new LabelProperty();
            var holdTimeLabelProperty = new LabelProperty();
            var releaseOutsideLabelProperty = new LabelProperty();
            buttonProperty.AddTo(_disposable);
            stateLabelProperty.AddTo(_disposable);
            swipeDirectionLabelProperty.AddTo(_disposable);
            holdTimeLabelProperty.AddTo(_disposable);
            releaseOutsideLabelProperty.AddTo(_disposable);

            new ButtonInputViewLogic(
                buttonProperty,
                stateLabelProperty,
                swipeDirectionLabelProperty,
                holdTimeLabelProperty,
                releaseOutsideLabelProperty).AddTo(_disposable);

            new KeyboardButtonInputLogic(buttonProperty).AddTo(_disposable);

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
