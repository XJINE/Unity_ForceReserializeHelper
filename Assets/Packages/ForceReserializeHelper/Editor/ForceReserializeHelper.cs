using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ForceReserializeHelper : EditorWindow
{
    public ForceReserializeAssetsOptions option = ForceReserializeAssetsOptions
                                                 .ReserializeAssetsAndMetadata;

    [MenuItem("Custom/ForceReserializeHelper")]
    static void Init()
    {
        EditorWindow.GetWindow<ForceReserializeHelper>("ForceReserializeHelper");
    }

    protected void OnGUI()
    {
        GUIStyle marginStyle = GUI.skin.label;
                 marginStyle.wordWrap = true;
                 marginStyle.margin = new RectOffset(5, 5, 5, 5);

        EditorGUILayout.LabelField("Reserialize Option");

        this.option = (ForceReserializeAssetsOptions)
                       EditorGUILayout.EnumPopup(this.option);

        if (GUILayout.Button("Click to Reserialize"))
        {
            ForceReserialize();
        }
    }

    protected virtual void ForceReserialize()
    {
        List<string> targets = new List<string>();
        targets.AddRange(AssetDatabase.GetAllAssetPaths());


        foreach (var target in targets) { Debug.Log(target); }

        AssetDatabase.ForceReserializeAssets(targets, this.option);
    }
}