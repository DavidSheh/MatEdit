using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Northwind.Editors.Shaders
{
    public abstract class MatEditGUI : ShaderGUI
    {

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            Material targetMat = materialEditor.target as Material;
            MatEdit.SetScope(targetMat);

            OnMaterialGUI(materialEditor, properties);

            EditorGUILayout.HelpBox("Made with MatEdit", MessageType.None);
        }

        public virtual void OnMaterialGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {

        }

    }
}