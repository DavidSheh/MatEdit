using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatEditData : ScriptableObject {

    [System.Serializable]
    public class AnimationCurveDictionary : SerializableDictionary<string, AnimationCurve> { }
    [SerializeField]
    public AnimationCurveDictionary animationCurves = new AnimationCurveDictionary();

    [System.Serializable]
    public class GradientDictionary : SerializableDictionary<string, Gradient> { }
    [SerializeField]
    public GradientDictionary gradients = new GradientDictionary();

    [System.Serializable]
    public class BoolDictionary : SerializableDictionary<string, bool> { }
    [SerializeField]
    public BoolDictionary toggles = new BoolDictionary();

    [System.Serializable]
    public class TextureDictonary : SerializableDictionary<string, Texture2D> { }
    [SerializeField]
    public TextureDictonary generatedTextures = new TextureDictonary();

    [SerializeField]
    public TextureDictonary unsavedTextures = new TextureDictonary();
}

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();
    
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }
    
    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
            throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

        for (int i = 0; i < keys.Count; i++)
            this.Add(keys[i], values[i]);
    }
}