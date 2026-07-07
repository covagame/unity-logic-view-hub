using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Covagame.LVH.Sample.View
{
    public static class LVHSampleViewFactory
    {
        public static Canvas CreateCanvas()
        {
            var canvasObject = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            var canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1280, 720);
            scaler.matchWidthOrHeight = 0.5f;

            return canvas;
        }

        public static RectTransform CreatePanel(Transform parent)
        {
            var panelObject = new GameObject("LVH Sample Panel", typeof(RectTransform), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
            panelObject.transform.SetParent(parent, false);

            var rectTransform = panelObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = new Vector2(520f, 320f);

            var layout = panelObject.GetComponent<VerticalLayoutGroup>();
            layout.spacing = 12f;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            var fitter = panelObject.GetComponent<ContentSizeFitter>();
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            return rectTransform;
        }

        public static ButtonView CreateButton(Transform parent, string label)
        {
            var buttonObject = new GameObject("LVH Sample Button", typeof(RectTransform), typeof(Image), typeof(Button), typeof(ButtonView));
            buttonObject.transform.SetParent(parent, false);

            var rectTransform = buttonObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(260f, 64f);

            var layoutElement = buttonObject.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 260f;
            layoutElement.preferredHeight = 64f;

            var image = buttonObject.GetComponent<Image>();
            image.color = new Color(0.22f, 0.45f, 0.95f, 1f);

            var text = CreateText(buttonObject.transform, "Button Label", label, 26f);
            text.alignment = TextAlignmentOptions.Center;
            Stretch(text.rectTransform);

            return buttonObject.GetComponent<ButtonView>();
        }

        public static LabelView CreateLabel(Transform parent)
        {
            var text = CreateText(parent, "LVH Sample Label", string.Empty, 24f);
            text.color = Color.black;
            text.alignment = TextAlignmentOptions.Center;

            var layoutElement = text.gameObject.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 520f;
            layoutElement.preferredHeight = 36f;

            return text.gameObject.AddComponent<LabelView>();
        }

        private static TextMeshProUGUI CreateText(Transform parent, string name, string value, float fontSize)
        {
            var textObject = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI));
            textObject.transform.SetParent(parent, false);

            var text = textObject.GetComponent<TextMeshProUGUI>();
            text.text = value;
            text.fontSize = fontSize;
            text.enableWordWrapping = false;

            return text;
        }

        private static void Stretch(RectTransform rectTransform)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }
    }
}
