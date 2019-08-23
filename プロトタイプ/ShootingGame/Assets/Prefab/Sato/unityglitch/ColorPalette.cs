using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[System.Serializable]
[PostProcess(typeof(ColorPaletteRenderer), PostProcessEvent.AfterStack, "Custom/ColorPalette")]
public sealed class ColorPalette : PostProcessEffectSettings
{
    public ColorParameter color1 = new ColorParameter { value = Color.gray };
    public ColorParameter color2 = new ColorParameter { value = Color.gray };
    public ColorParameter color3 = new ColorParameter { value = Color.gray };
    public ColorParameter color4 = new ColorParameter { value = Color.gray };

    private Texture2D _paletteTexture;
    public Texture2D PaletteTexture
    {
        get
        {
            Color[] color = new Color[] { color1, color2, color3, color4 };
            if (_paletteTexture == null)
            {
                _paletteTexture = new Texture2D(color.Length, 1, TextureFormat.ARGB32, false);
                _paletteTexture.filterMode = FilterMode.Point;
            }
            _paletteTexture.SetPixels(color);
            _paletteTexture.Apply();
            return _paletteTexture;
        }
    }
    public FloatParameter paletteWidth = new FloatParameter { };
}
public sealed class ColorPaletteRenderer : PostProcessEffectRenderer<ColorPalette>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/ColorPalette"));
        sheet.properties.SetTexture("_PaletteTex", settings.PaletteTexture);
        sheet.properties.SetFloat("_PaletteWidth", settings.paletteWidth);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}