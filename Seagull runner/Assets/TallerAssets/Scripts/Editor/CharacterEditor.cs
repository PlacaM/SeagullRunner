using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Taller;


[CustomEditor(typeof(Character),true ), CanEditMultipleObjects] 
public class CharacterEditor : Editor
{

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();


    }
}
