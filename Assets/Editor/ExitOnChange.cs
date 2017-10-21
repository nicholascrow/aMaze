using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[InitializeOnLoad]
public class ExitOnChange
{
    static float replayTime = -1;
    static ExitOnChange()
    {
        EditorApplication.update -= ExitAndReplay;
        EditorApplication.update += ExitAndReplay;
        EditorApplication.update += EditorUpdate;
    }
    static void ExitAndReplay()
    {
        if (EditorApplication.isCompiling && EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
            replayTime = Time.realtimeSinceStartup;
            Debug.Log("Stopping application because a script was changed.");
        }

         

    }
    static void EditorUpdate()
    {
        if(replayTime != -1 && (replayTime - Time.realtimeSinceStartup ) > 3f)
        {
            replayTime = -1;
            EditorApplication.isPlaying = true;
            Debug.Log("Starting application (was stopped due to script being changed)");
        }
    }

}
