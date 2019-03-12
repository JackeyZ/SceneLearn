/*
*Title:"AssetBundle学习"项目开发
*
*Description:
*	加载assetBundle包
*
*Date:2017
*
*Version:0.1
*
*Modify Recoder:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//UnityWebRequest
using UnityEngine.Networking;
public class ABTest : MonoBehaviour
{

    // Use this for initialization
    IEnumerator Start()
    {
        //1、从本地文件加载
        /*AssetBundle ab_share = AssetBundle.LoadFromFile("AssetBundles/share.material");  //先导入共享资源的assetBundle包
        AssetBundle ab_prefab = AssetBundle.LoadFromFile("AssetBundles/cube.prefab");
        GameObject cubePrefab = ab_prefab.LoadAsset<GameObject>("Cube");
        Instantiate(cubePrefab);
        AssetBundle ab_prefab2 = AssetBundle.LoadFromFile("AssetBundles/sphere.prefab");
        GameObject spherePrefab = ab_prefab2.LoadAsset<GameObject>("Sphere");
        Instantiate(spherePrefab);*/


        //2、从内存加载,首先通过File把文件读取进内存里面保存在Byte数组里面，然后在用LoadFromMemroyAsync异步从内存中读取AssetBundle
        /*AssetBundle.LoadFromMemory(File.ReadAllBytes("AssetBundles/share.material")); //同步从内存加载材质
        AssetBundleCreateRequest ab_prefabRequest = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes("AssetBundles/cube.prefab"));//异步加载
        yield return ab_prefabRequest; //等待加载完成，因为是异步加载
        AssetBundle ab_prefab = ab_prefabRequest.assetBundle;
        //使用assetBundle
        GameObject cubePrefab = ab_prefab.LoadAsset<GameObject>("Cube");
        Instantiate(cubePrefab);*/

        //3、用www方式加载AB
        /* while(Caching.ready == false)
         {
             yield return null;
         }
         //本地路径
         //WWW www =  WWW.LoadFromCacheOrDownload(@"file:///K:\Documents\AssetBundle\AssetBundles\cube.prefab", 1);
         //服务器路径
         WWW www =  WWW.LoadFromCacheOrDownload(@"http://localhost/AssetBundles/cube.prefab", 1);
         yield return www;//等待加载完成
         if (!string.IsNullOrEmpty(www.error))
         {
             Debug.LogError("www加载异常");
             yield break;
         }
         AssetBundle ab_prefab = www.assetBundle;
         GameObject cubePrefab = ab_prefab.LoadAsset<GameObject>("Cube");
         Instantiate(cubePrefab);*/

        //4、UnityWebRequest方式（推荐）
        //本地地址
        string uri = @"file:///K:\Documents\AssetBundle\AssetBundles\cube.prefab";
        //服务器地址
        //string uri = @"http://localhost:55972/AssetBundles/cube.prefab";
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(uri);
        yield return request.Send();
        //取得ab的方式1
        //AssetBundle ab_prefab = DownloadHandlerAssetBundle.GetContent(request);
        //取得ab的方式2
        AssetBundle ab_prefab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;

        GameObject cubePrefab = ab_prefab.LoadAsset<GameObject>("Cube");
        Instantiate(cubePrefab);



        AssetBundle manifestAB = AssetBundle.LoadFromFile("AssetBundles/AssetBundles");  //加载总的ab包
        AssetBundleManifest manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");//获取总的ab包的manifest以获取其他包的名字或者依赖关系

        //foreach (string name in manifest.GetAllAssetBundles()) //通过总包的manifest获得所有ab包的名称
        //{
        //    Debug.Log(name);
        //}

        string[] strs = manifest.GetAllDependencies("cube.Prefab"); //获取cube.Prefab这个包所依赖的所有包的名字
        foreach (string name in strs)
        {
            Debug.Log(name);
            AssetBundle.LoadFromFile("AssetBundles/" + name); //加载cube.Prefab这个包所依赖的包
        }
        yield return null;


    }

}
