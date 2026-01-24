using UnityEditor;
using UnityEngine;

namespace Assets.Project.CodeBase.StaticData.Field
{
    [CustomEditor(typeof(LevelData))]
    public class LeveDataEditor : Editor
    {
        private SerializedProperty levelInfoList;

        private void OnEnable()
        {
            levelInfoList = serializedObject.FindProperty("levelInfo");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Levels", EditorStyles.boldLabel);

            if (levelInfoList == null)
            {
                EditorGUILayout.HelpBox("Уровни не найдены.", MessageType.Error);
                return;
            }

            if (GUILayout.Button("➕ Add Level"))
            {
                levelInfoList.arraySize++;                
                var newElement = levelInfoList.GetArrayElementAtIndex(levelInfoList.arraySize - 1);
            }

            EditorGUILayout.Space();
            for (int i = 0; i < levelInfoList.arraySize; i++)
            {
                var levelProp = levelInfoList.GetArrayElementAtIndex(i);
                var levelIdProp = levelProp.FindPropertyRelative("levelId");
                var rowsProp = levelProp.FindPropertyRelative("rows");
                var colsProp = levelProp.FindPropertyRelative("columns");
                var flatMatrixProp = levelProp.FindPropertyRelative("flatMatrix");

                if (rowsProp == null || colsProp == null || flatMatrixProp == null)
                {
                    EditorGUILayout.HelpBox($"Ошибка: уровень {i} повреждён", MessageType.Error);
                    continue;
                }

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField($"Level {i}", EditorStyles.boldLabel);
                if (levelIdProp != null)
                {
                    levelIdProp.intValue = EditorGUILayout.IntField("Level ID", levelIdProp.intValue);
                }
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("🗑️ Delete", GUILayout.Width(80)))
                {
                    levelInfoList.DeleteArrayElementAtIndex(i);
                    serializedObject.ApplyModifiedProperties();
                    return; 
                }
                EditorGUILayout.EndHorizontal();

                int newRows = EditorGUILayout.IntField("Rows", rowsProp.intValue);
                int newCols = EditorGUILayout.IntField("Columns", colsProp.intValue);

                if (newRows != rowsProp.intValue || newCols != colsProp.intValue)
                {
                    rowsProp.intValue = Mathf.Max(1, newRows);
                    colsProp.intValue = Mathf.Max(1, newCols);
                    flatMatrixProp.arraySize = rowsProp.intValue * colsProp.intValue;
                    for (int k = 0; k < flatMatrixProp.arraySize; k++)
                    {
                        flatMatrixProp.GetArrayElementAtIndex(k).intValue = 0;
                    }
                }

                EditorGUILayout.Space();
                for (int r = 0; r < rowsProp.intValue; r++)
                {
                    EditorGUILayout.BeginHorizontal();
                    for (int c = 0; c < colsProp.intValue; c++)
                    {
                        int index = r * colsProp.intValue + c;
                        var cell = flatMatrixProp.GetArrayElementAtIndex(index);
                        cell.intValue = EditorGUILayout.IntField(cell.intValue, GUILayout.Width(40));
                    }
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
