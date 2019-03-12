using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneCreate : MonoBehaviour
{
    public GameObject prefab1;
    public Transform trans;
    void Start()
    {
        Scene newScene = SceneManager.CreateScene("My New Scene"); 
        SceneManager.SetActiveScene(newScene);
        GameObject newObj = GameObject.Instantiate(prefab1,trans.position,trans.rotation);

        //加载.unity3d后缀的场景文件
        StartCoroutine("LoadScene");                                     
    }
    private IEnumerator LoadScene()
    {
        WWW download = WWW.LoadFromCacheOrDownload("file://" + Application.dataPath + "/ScenesPack/Scene_02.unity3d", 1);  
        yield return download;
        var bundle = download.assetBundle;
        AsyncOperation ao = SceneManager.LoadSceneAsync("Scene_02", LoadSceneMode.Additive);   //不需要在build and settings窗口中设置好，这里可以直接用
        yield return ao;
        Scene newScene2 = SceneManager.GetSceneByName("Scene_02");  //获取刚才加载的场景
        SceneManager.SetActiveScene(newScene2);                     //把场景激活
        GameObject newObj = GameObject.Instantiate(prefab1, trans.position, trans.rotation);    //在场景中实例化prefab
       // Scene scene = SceneManager.GetSceneByName("Scene_01"); 
        //SceneManager.UnloadSceneAsync(scene);
    }
}
