using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


public class TestWindow : EditorWindow
{
    public List<Object> scenes= new List<Object>();
    public static TestWindow testWindow;
    private EditorBuildSettingsScene[] EditroScenes;
    private List<SceneAsset> sceneAssets = new List<SceneAsset>();
    [MenuItem("Window/Scenes Holder")]
    public static void Open()
    {       
          testWindow= GetWindow<TestWindow>(false,"Project Scenes",true);
          testWindow.Show();
    }
    void OnGUI()
    {
        GUILayout.Label("Scenes Settings", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        if (GUILayout.Button("Add Scene"))
        {
            Object obj = new Object();
            scenes.Add(obj);

        }

        for (int i = 0; i < scenes.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            scenes[i] = EditorGUILayout.ObjectField(scenes[i], typeof(SceneAsset), true);
            sceneAssets.Add(scenes[i]as SceneAsset);
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Open Scene"))
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(scenes[i]));
            }
            GUI.backgroundColor = Color.grey;
            if (GUILayout.Button("Open AdditiveScene"))
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(scenes[i]),OpenSceneMode.Additive);
            }
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove"))
            {
                scenes.Remove(scenes[i]);
                sceneAssets.Remove(scenes[i] as SceneAsset);
            }
            EditorGUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Get Build Scenes"))
        {
            GetScenes();
        }


    }
    private void GetScenes()
    {
        EditroScenes = EditorBuildSettings.scenes;
        for (int i = 0; i < EditroScenes.Length; i++)
        {
            SceneAsset _sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditroScenes[i].path);
            if (!sceneAssets.Contains(_sceneAsset))
            {
                Object obj = new Object();
                scenes.Add(obj);
                scenes[i] = _sceneAsset;
            }
        }

        //for (int i = 0; i <  SceneManager.sceneCountInBuildSettings; i++)
        //{
            
        //    if (i>=scenes.Count)
        //    {
        //        Object obj = new Object();
        //        scenes.Add(obj);
        //        string path= AssetDatabase.GetAssetPath(SceneManager.GetSceneByBuildIndex(i));
        //        SceneAsset sceneAsset = AssetDatabase.GetAssetOrScenePath();
        //        //scenes[i] = s;
        //    }

        //}

    }



}
