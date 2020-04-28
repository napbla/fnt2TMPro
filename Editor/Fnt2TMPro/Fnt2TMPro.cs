using UnityEngine;
using UnityEditor;
using UnityEngine.TextCore;

using TMPro;
using litefeelcustom;

namespace Fnt2TMPro.EditorUtilities
{
    public class Fnt2TMPro : EditorWindow
    {
        [MenuItem("Window/Bitmap Font Converter")]

        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(Fnt2TMPro), false, "Bitmap Font Converter");
        }
        private Texture2D m_Texture2D;
        private TextAsset m_SourceFontFile;
        private TMP_FontAsset m_DestinationFontFile;
        void PatchGlyph(RawCharacterInfo character, int textureHeight, int textureWidth, ref Glyph g)
        {
            var scaleH = textureWidth / textureHeight > 1 ? textureWidth / textureHeight : 1;
            var scaleW = textureHeight / textureWidth > 1 ? textureHeight / textureWidth : 1;
            g.glyphRect = new GlyphRect(
                character.X * scaleW,
                (textureHeight - character.Y - character.Height) * scaleH,
                character.Width * scaleW,
                character.Height * scaleH
            );
            g.metrics = new GlyphMetrics(
                character.Width,
                character.Height,
                character.Xoffset,
                -character.Yoffset,
                character.Xadvance
            );
        }
        void UpdateFont(TMP_FontAsset fontFile)
        {
            var fontText = m_SourceFontFile.text;
            var fnt = FntParse.GetFntParse(ref fontText);

            for (int i = 0; i < fontFile.characterTable.Count; i++)
            {
                var unicode = fontFile.characterTable[i].unicode;
                var glyphIndex = fontFile.characterTable[i].glyphIndex;
                for (int j = 0; j < fnt.charInfos.Length; j++)
                {
                    if (unicode == fnt.charInfos[j].index)
                    {
                        var glyph = fontFile.glyphLookupTable[glyphIndex];
                        PatchGlyph(fnt.rawCharInfos[j],
                            fnt.textureHeight,
                            fnt.textureWidth,
                            ref glyph);
                        fontFile.glyphLookupTable[glyphIndex] = glyph;
                        break;
                    }
                }
            }

            var newFaceInfo = fontFile.faceInfo;
            newFaceInfo.baseline = fnt.lineBaseHeight;
            newFaceInfo.lineHeight = fnt.lineHeight;
            newFaceInfo.ascentLine = fnt.lineHeight;
            newFaceInfo.pointSize = fnt.fontSize;

            var fontType = typeof(TMP_FontAsset);
            var faceInfoProperty = fontType.GetProperty("faceInfo");
            faceInfoProperty.SetValue(fontFile, newFaceInfo);

            fontFile.material.SetTexture("_MainTex", m_Texture2D);
            fontFile.atlasTextures[0] = m_Texture2D;
        }

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            m_Texture2D = EditorGUILayout.ObjectField("Font Texture",
                m_Texture2D, typeof(Texture2D), false) as Texture2D;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            m_SourceFontFile = EditorGUILayout.ObjectField("Source Font File",
                m_SourceFontFile, typeof(TextAsset), false) as TextAsset;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            m_DestinationFontFile = EditorGUILayout.ObjectField("Destination Font File",
                m_DestinationFontFile, typeof(TMP_FontAsset), false) as TMP_FontAsset;
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Convert"))
            {
                UpdateFont(m_DestinationFontFile);
            }
        }
    }
}