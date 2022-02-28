using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using IFix;
using IFix.Core;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TestInject : MonoBehaviour
{
    public Text console;

    void Start()
    {
        LoadPatchDll(new Uri("http://127.0.0.1/test/Assembly-CSharp.patch.bytes"));
        Debug.Log("loading...");
        HelloWord();
    }

    void LoadPatchDll(Uri uri, string downloadFileName = "Assembly-CSharp.patch.bytes")
    {
#if UNITY_EDITOR
        string patchFile = Path.Combine(Application.dataPath, $"IFix/Resources/{downloadFileName}");
        if (File.Exists(patchFile))
        {
            Debug.Log("load local success.");
            PatchManager.Load(patchFile);
        }
#else
        using (UnityWebRequest downloader = UnityWebRequest.Get(uri))
        {
            downloader.downloadHandler = new DownloadHandlerFile(Path.Combine(Application.persistentDataPath, downloadFileName));
            downloader.SendWebRequest();
            while (!downloader.isDone)
            {
            }

            Debug.Log("is done.");

            if (downloader.error == null)
            {
                Debug.Log("download success.");
                PatchManager.Load(Path.Combine(Application.dataPath, downloadFileName));
            }
            else
            {
                Debug.Log($"download error:{downloader.error}.");
            }
        }
#endif
    }

    void HelloWord()
    {
        string text = "Hello world! I'm release";
        Debug.Log(text);
        console.text = text;
    }
}