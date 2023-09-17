using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LoaderWindow : EditorWindow
{
    [Header("Files (.dcm)")]
    [SerializeField]
    List<DefaultAsset> dicoms = new List<DefaultAsset>();

    [Header("Volume Filename")]
    [SerializeField]
    string filename;

    Editor editor;

    [MenuItem("DicomVolume/Create Volume Texture")]
    public static void ShowWindow()
    {
        GetWindow<LoaderWindow>("Dicom Volume Window");
    }

    public void OnGUI()
    {
        if (!editor) { editor = Editor.CreateEditor(this); }
        if (editor) { editor.OnInspectorGUI(); }
        if (GUILayout.Button("Create"))
        {
            LoadDicom.CreateDicomVolumeTexture(dicoms, filename);
        }
    }

    public void OnInspectorUpdate()
    {
        Repaint();
    }

}
