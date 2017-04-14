using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Northwind.Editor.Shader
{
    public static class MatEdit
    {

        #region MatEdit_Enums

        public enum PropertyParts { x, y, z, w };
        public enum HirarchyLayer { Main, Sub };
        public enum TextureFieldType {Small = 16, Medium = 32, Large = 64};

        #endregion MatEdit_Enums

        #region MatEdit_Stats

        private static Material scopeMaterial;

        #endregion MatEdit_Stats

        #region MatEdit_HelperClasses

        [System.Serializable]
        private class AnimationCurveContainer
        {
            public AnimationCurve localCurve;

            public AnimationCurveContainer(AnimationCurve curve)
            {
                localCurve = curve;
            }
        }

        #endregion MatEdit_HelperClasses

        #region MatEdit_HelperFunctions

        private static Texture2D AnimationCurveToTexture(AnimationCurve curve, int steps, bool debug = false)
        {
            float lStartTime = Time.time;

            Texture2D lResult = new Texture2D(steps, 1);

            Color[] lPixels = new Color[steps];
            float length = steps;
            for (int p = 0; p < steps; p++)
            {
                float point = p;
                float lVal = curve.Evaluate(point / length);
                lPixels[p] = new Color(lVal, (lVal - 1f), (lVal - 2f), 1f);
            }

            lResult.SetPixels(lPixels);
            lResult.Apply();

            if (debug)
            {
                Debug.Log("<color=green>Success:</color> Converted AnimationCurve to Texture2D in " + (Time.time - lStartTime));
            }

            return lResult;
        }

        #endregion MatEdit_HelperFunctions

        ///////////////
        //Scoper

        public static void SetScope(Material material)
        {
            scopeMaterial = material;
        }

        ///////////////
        //Groups

        public static bool BeginFoldOut(GUIContent content, string toggleID)
        {
            return BeginFoldOut(content, toggleID, scopeMaterial);
        }

        public static bool BeginFoldOut(GUIContent content, string toggleID, Material material)
        {
            string lKey = "MatEdit:" + material.GetInstanceID() + "-> ToggleID:" + toggleID;

            EditorGUILayout.BeginVertical(EditorStyles.miniButton);

            if (GUILayout.Button(content, EditorStyles.boldLabel))
                EditorPrefs.SetBool(lKey, !EditorPrefs.GetBool(lKey));

            return EditorPrefs.GetBool(lKey);
        }

        public static void EndGroup()
        {
            EditorGUILayout.EndVertical();
        }

        ///////////////
        //Texture Fields

        public static void TextureField(GUIContent content, string property, TextureFieldType size = TextureFieldType.Small)
        {
            TextureField(content, property, scopeMaterial, size);
        }

        public static void TextureField(GUIContent content, string property, Material material, TextureFieldType size = TextureFieldType.Small)
        {
            Texture2D mainTexture = (Texture2D)EditorGUILayout.ObjectField(content, material.GetTexture(property), typeof(Texture2D), false, GUILayout.Height((float)size));
            material.SetTexture(property, mainTexture);
        }

        public static void NormalTextureField(GUIContent content, string property, TextureFieldType size = TextureFieldType.Small)
        {
            NormalTextureField(content, property, scopeMaterial, size);
        }

        public static void NormalTextureField(GUIContent content, string property, Material material, TextureFieldType size = TextureFieldType.Small)
        {
            Texture2D normalTexture = (Texture2D)EditorGUILayout.ObjectField(content, material.GetTexture(property), typeof(Texture), false, GUILayout.Height((float)size));
            if (normalTexture != null)
            {
                TextureImporter lImporter = (TextureImporter)TextureImporter.GetAtPath(AssetDatabase.GetAssetPath(normalTexture.GetInstanceID()));
                if (lImporter.textureType != TextureImporterType.NormalMap)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    EditorGUILayout.LabelField("Texture is no normal map!");
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Fix now"))
                    {
                        lImporter.textureType = TextureImporterType.NormalMap;
                        lImporter.convertToNormalmap = true;
                    }
                    if (GUILayout.Button("To Settings"))
                    {
                        Selection.activeObject = lImporter;
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                }
            }
            material.SetTexture(property, normalTexture);
        }

        ///////////////
        //Simple Fields

        //Color Field
        public static void ColorField(GUIContent content, string property)
        {
            ColorField(content, property, scopeMaterial);
        }

        public static void ColorField(GUIContent content, string property, Material material)
        {
            material.SetColor(property, EditorGUILayout.ColorField(content, material.GetColor(property)));
        }

        //Toggle Field
        public static void ToggleField(GUIContent content, string property)
        {
            ToggleField(content, property, scopeMaterial);
        }

        public static void ToggleField(GUIContent content, string property, Material material)
        {
            material.SetInt(property, EditorGUILayout.Toggle(content, material.GetInt(property) == 1 ? true : false) ? 1 : 0);
        }

        //Float Field
        public static void FloatField(GUIContent content, string property)
        {
            FloatField(content, property, scopeMaterial);
        }

        public static void FloatField(GUIContent content, string property, Material material)
        {
            material.SetFloat(property, EditorGUILayout.FloatField(content, material.GetFloat(property)));
        }

        //Slider Field
        public static void SliderField(GUIContent content, string property, float min, float max, bool round = false)
        {
            SliderField(content, property, min, max, scopeMaterial, round);
        }

        public static void SliderField(GUIContent content, string property, float min, float max, Material material, bool round = false)
        {
            float lValue = EditorGUILayout.Slider(content, material.GetFloat(property), min, max);
            if (round)
            {
                lValue = Mathf.Round(lValue);
            }
            material.SetFloat(property, lValue);
        }

        ///////////////
        //Special Fields

        //AnimationCurve Field
        public static void AnimationCurveField(GUIContent content, string property)
        {
            AnimationCurveField(content, property, scopeMaterial);
        }

        public static void AnimationCurveField(GUIContent content, string property, Material material)
        {
            string getJSON = EditorPrefs.GetString(material.GetInstanceID() + ":Animation Curve:" + property);
            AnimationCurve curve;
            if (getJSON != "")
            {
                curve = JsonUtility.FromJson<AnimationCurveContainer>(getJSON).localCurve;
            }
            else
            {
                curve = null;
            }

            if (curve == null)
            {
                curve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
            }

            curve = EditorGUILayout.CurveField(content, curve);
            string setJSON = JsonUtility.ToJson(new AnimationCurveContainer(curve));
            EditorPrefs.SetString(material.GetInstanceID() + ":Animation Curve:" + property, setJSON);

            Texture2D mainTexture = AnimationCurveToTexture(curve, 1024);
            material.SetTexture(property, mainTexture);
        }
    }
}