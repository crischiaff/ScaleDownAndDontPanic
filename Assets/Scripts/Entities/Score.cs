using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Score", order = 1)]
public class Score : ScriptableObject
{
    public Rule requirement;

    [System.Serializable]
    public class FeatureScore
    {
        public Feature feature;
        public int score;
    }

    public List<FeatureScore> scores;
}

#if UNITY_EDITOR
[CustomEditor(typeof(Score))]
public class KeyValueExampleEditor : Editor
{
    SerializedProperty keyValues;
    SerializedProperty requirement;

    void OnEnable()
    {
        requirement = serializedObject.FindProperty("requirement");
        keyValues = serializedObject.FindProperty("scores");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(requirement, true);
        EditorGUILayout.PropertyField(keyValues, true);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif