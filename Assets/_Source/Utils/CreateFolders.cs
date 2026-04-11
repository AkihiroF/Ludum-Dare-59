#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Screen;

namespace CustomEditors
{
    public class CreateFolders : EditorWindow
    {

        [MenuItem("Assets/Create Default Folders")]
        private static void SetUpFolders()
        {
            CreateFolders window = ScriptableObject.CreateInstance<CreateFolders>();
            window.position = new Rect(width / 2, height / 2, 400, 150);
            window.ShowPopup();
        }

        private static void CreateFoldersBase(string basePath, string[] folders)
        {
            foreach (var folder in folders)
            {
                string fullPath = basePath + folder;
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                    Debug.Log($"Folder created: {fullPath}");
                }
            }
            AssetDatabase.Refresh();
        }

        private static void CreateUnitysFolders()
        {
            string[] folders = 
            {
                "Animations",
                "Audio",
                "Editor",
                "Materials",
                "Meshes",
                "Prefabs",
                "Scripts",
                "Scenes",
                "Shaders",
                "Textures",
                "UI/Assets",
                "UI/Fonts",
                "UI/Icon"
            };

            CreateFoldersBase("Assets/", folders);
        }

        private static void CreateNastyaFolders()
        {
            string[] mainFolders =
            {
                "_Presentation",
                "_Source",
                "_Support",
                "Resources",
            };

            CreateFoldersBase("Assets/", mainFolders);

            string[] presentationFolders = new string[]
            {
                "Animations",
                "Audio",
                "Materials",
                "Meshes",
                "Prefabs",
                "Scenes",
                "Shaders",
                "Textures",
                "UI/Assets",
                "UI/Fonts",
                "UI/Icon"
            };
            CreateFoldersBase("Assets/_Presentation/", presentationFolders);

            string[] sourceFolders =
            {
                "Core",
                "Input",
                "Utils"
            };
            CreateFoldersBase("Assets/_Source/", sourceFolders);

        string[] supportFolders =
            {
                "Third Party",
                "Plugins"
            };
            CreateFoldersBase("Assets/_Support/", supportFolders);
        }

        void OnGUI()
        {
            if (GUILayout.Button("Generate Unity Folders!"))
            {
                CreateUnitysFolders();
                this.Close();
            }
            if (GUILayout.Button("Generate Nastya Folders!"))
            {
                CreateNastyaFolders();
                this.Close();
            }
            if (GUILayout.Button("Close"))
            {
                this.Close();
            }
        }
    }
}
#endif
