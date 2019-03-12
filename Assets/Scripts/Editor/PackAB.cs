using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PackAB {

#if UNITY_EDITOR
    [MenuItem("Custom Editor/Create Scene")]
    static void CreateSceneALL()
    {
        //清空一下缓存  
        Caching.ClearCache();

        //获得用户选择的路径的方法，可以打开保存面板（推荐）
        string Path = EditorUtility.SaveFilePanel("保存资源", "SS", "" + "Scene_02", "unity3d");
        //string Path = Application.dataPath + "/Scene_02.unity3d";

        //另一种获得用户选择的路径，默认把打包后的文件放在Assets目录下
        //string Path = Application.dataPath + "/MyScene.unity3d";

        //选择的要保存的对象 
        string[] levels = { "Assets/Scenes/Scene_02.unity" };

        //打包场景  
        BuildPipeline.BuildPlayer(levels, Path, BuildTarget.StandaloneWindows64, BuildOptions.BuildAdditionalStreamedScenes);

        // 刷新，可以直接在Unity工程中看见打包后的文件
        AssetDatabase.Refresh();
    }
#endif
}
