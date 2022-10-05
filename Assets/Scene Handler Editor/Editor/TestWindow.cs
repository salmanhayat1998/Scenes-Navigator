using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.IO;

public class TestWindow : EditorWindow
{
    public List<Object> scenes = new List<Object>();
    public static TestWindow testWindow;
    private EditorBuildSettingsScene[] EditroScenes;
    Vector2 scrollPos = Vector2.zero;
    static int assetno = 0;
    [MenuItem("Window/Scenes Holder")]
    public static void Open()
    {
        testWindow = GetWindow<TestWindow>(false, "Project Scenes", true);
        testWindow.Show();
    }
    private void OnEnable()
    {
        testWindow = GetWindow<TestWindow>(false, "Project Scenes", true);
    }
    void OnGUI()
    {
        GUILayout.Label("Scenes Menu", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Scene From Project"))
        {
            Object obj = new Object();
            scenes.Add(obj);

        }   
        if (GUILayout.Button("Create New Scene"))
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            string[] path = EditorSceneManager.GetActiveScene().path.Split(char.Parse("/"));
            path[path.Length - 1] = "New scene " + assetno + ".unity";
            if (!File.Exists(string.Join("/", path)))
            {
                assetno++;
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                Scene s = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
                EditorSceneManager.SaveScene(s, string.Join("/", path));
                Object obj = AssetDatabase.LoadAssetAtPath<SceneAsset>(s.path);
                scenes.Add(obj);
            }
            GUIUtility.ExitGUI();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();


        if (scenes.Count > 0)
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false, GUILayout.Width(testWindow.position.width), GUILayout.Height(testWindow.position.height - 120));
            for (int i = 0; i < scenes.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                scenes[i] = EditorGUILayout.ObjectField(scenes[i], typeof(SceneAsset), true);
                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("Open"))
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(scenes[i]));
                }
                GUI.backgroundColor = Color.cyan;
                if (GUILayout.Button("Open Additive"))
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(scenes[i]), OpenSceneMode.Additive);
                }
                GUI.backgroundColor = Color.red;
                Texture icon = Resources.Load("delicon") as Texture;
                if (GUILayout.Button(icon))
                {
                    scenes.RemoveAt(i);
                }


                EditorGUILayout.EndHorizontal();

                GUI.backgroundColor = Color.white;
            }
            EditorGUILayout.EndScrollView();
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Get Build Scenes"))
        {
            GetBuildScenes();
        }

        if (scenes.Count > 0)
        {
            EditorGUILayout.Space();
            if (GUILayout.Button("Remove All Scenes"))
            {
                RemoveAllScenes();
            }
        }


    }
    private void GetBuildScenes()
    {
        EditroScenes = EditorBuildSettings.scenes;

        for (int i = 0; i < EditroScenes.Length; i++)
        {
            SceneAsset _sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditroScenes[i].path);
            if (!scenes.Contains(_sceneAsset))
            {
                scenes.Add(_sceneAsset);
            }
        }

        for (int i = 0; i < scenes.Count; i++)
        {
            if (scenes[i] == null)
            {
                scenes.RemoveAt(i);
            }
        }
    }
    private void RemoveAllScenes()
    {
        scenes.Clear();
    }

}
