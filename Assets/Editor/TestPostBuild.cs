using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class TestPostBuild
{
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuildProject)
    {
        Debug.Log("Build location: " + pathToBuildProject);
    }
}
