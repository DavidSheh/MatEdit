using UnityEngine;
using UnityEditor;
using System;

using Northwind.Editor.Shader;

public class ExampleUnlitShader_Editor : ShaderGUI
{

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        Material targetMat = materialEditor.target as Material;

        MatEdit.SetScope(targetMat);

        MatEdit.TextureField(new GUIContent("Main Texture", "The main texture for the material"), "_MainTex");
        MatEdit.ColorField(new GUIContent("Tint Color", "The tint color for the main texture"), "_TintColor");

        MatEdit.SliderField(new GUIContent("Scroll Duration", "The time in seconds for one scroll"), "_ScrollingDuration", 0f, 10f);
        MatEdit.AnimationCurveField(new GUIContent("Scroll Speed", "The scroll speed over time"), "_ScrollingSpeed");
    }

}
