using UnityEditor;
using UnityEngine;

namespace Utils.Editor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(SceneLoader), true)]
    public class SceneLoaderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SceneLoader myTarget = (SceneLoader)target;
            
            string[] scenes = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes);
            
            string[] sceneNames = new string[scenes.Length];
            for (int i = 0; i < scenes.Length; i++)
            {
                sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(scenes[i]);
            }
            
            int currentIndex = Mathf.Max(System.Array.IndexOf(sceneNames, myTarget.NameSceneLoading), 0);
            currentIndex = EditorGUILayout.Popup("Scene Loading", currentIndex, sceneNames);
            
            myTarget.NameSceneLoading = sceneNames[currentIndex];
            
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }

            DrawDefaultInspector();
        }
    }
#endif
}