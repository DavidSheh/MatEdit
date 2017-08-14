using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Northwind.Editors.Shaders
{
    public abstract class MatEditGUI : ShaderGUI
    {

        //The standard ShaderGUI function
        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            Material targetMat = materialEditor.target as Material;
            MatEdit.SetScope(targetMat);

            OnMaterialGUI(materialEditor, properties);

            EditorGUILayout.HelpBox("Made with MatEdit", MessageType.None);
        }

        //The MatEdit prepared function. Prepared for upcoming features
        public virtual void OnMaterialGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {

        }

    }
}