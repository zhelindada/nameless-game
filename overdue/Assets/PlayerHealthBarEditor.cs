using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerEntity))]
public class PlayerEntityEditor : Editor
{
    public override void OnInspectorGUI()
    {

        PlayerEntity pe = (PlayerEntity)target;

        EditorGUILayout.FloatField("Current Health", pe.GetCurrentHealth());

        if (GUILayout.Button("Take 1000 Damage")) {
            pe.TakeDamage(1000f);
        }
        if (GUILayout.Button("Die"))
        {
            pe.Die();
        }
    }
}
