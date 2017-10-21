using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class SaveOnPlay
{
    static SaveOnPlay()
    {
        EditorApplication.playmodeStateChanged += SaveCurrentScene;
    }

    static void SaveCurrentScene()
    {
        if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode && (EditorSceneManager.GetActiveScene().isDirty))
        {
            Debug.Log("Saving unsaved scene!");
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene()); 
        }
    }
}