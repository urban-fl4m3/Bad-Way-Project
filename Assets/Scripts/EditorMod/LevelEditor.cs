using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] private string folderPath = "Assets/Prefab/Test";
    [SerializeField] private Image PrefabBar; 
    [SerializeField] private Button PreviewImage;
    [SerializeField] private Button FolderButton;
    [SerializeField] private Button goBack;

    
    private UnityPool<Button> folderButtons;
    private UnityPool<Button> previewButtons;

    private void Start()
    {
        folderButtons = new UnityPool<Button>(FolderButton);
        previewButtons = new UnityPool<Button>(PreviewImage);

        CheckFolders(folderPath, null);
    }

    private void CheckFolders(string path, string lastPath)
    {
        HideButton();
        var folders = AssetDatabase.GetSubFolders(path);
        SetBackButton(lastPath);
        if (folders.Length == 0) CreatePreview(path);

        var a = 0;
        foreach (var folder in folders)
        {
            var folderName = folder.Remove(0, path.Length+1);
            
            if (folderButtons.Count <= a) folderButtons.Instantiate().transform.SetParent(PrefabBar.transform);
            folderButtons[a].onClick.RemoveAllListeners();
            folderButtons[a].onClick.AddListener(() =>
            {
                CheckFolders(folder,path);
            });
            folderButtons[a].GetComponentInChildren<Text>().text = folderName;
            folderButtons[a].gameObject.SetActive(true);
            a++;
        }
    }

    private void CreatePreview(string path)
    {
        var assetsPath = AssetDatabase.FindAssets("t:prefab", new[] {path});
        var assets = assetsPath.Select(s => AssetDatabase.GUIDToAssetPath(s))
            .Select(a => AssetDatabase.LoadAssetAtPath<Object>(a)).ToList();
        var i = 0;
        foreach (var asset in assets)
        {
            if (previewButtons.Count <= i)
                previewButtons.Instantiate().transform.SetParent(PrefabBar.transform);
            var assetPreviewScene = AssetPreview.GetAssetPreview(asset);
            var rect = new Rect(0, 0, assetPreviewScene.width, assetPreviewScene.height);
            previewButtons[i].GetComponent<Image>().sprite = Sprite.Create(assetPreviewScene,rect,Vector2.one/2);
            previewButtons[i].gameObject.SetActive(true);
            i++;
        }
    }
    private void HideButton()
    {
        var allButton = new List<Button>();
        allButton.AddRange(previewButtons);
        allButton.AddRange(folderButtons);
        foreach (var button in allButton)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void SetBackButton(string lastPath)
    {
        if (string.IsNullOrEmpty(lastPath))
        {
            goBack.gameObject.SetActive(false);
        }
        else
        {
            goBack.gameObject.SetActive(true);
            goBack.onClick.RemoveAllListeners();
            goBack.onClick.AddListener(() =>
            {
                CheckFolders(lastPath,null);
            });
        }
    }
}
