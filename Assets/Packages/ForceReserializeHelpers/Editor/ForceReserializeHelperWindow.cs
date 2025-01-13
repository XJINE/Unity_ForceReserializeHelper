using UnityEditor;
using UnityEngine;

namespace ForceReserializeHelpers
{
    public class ForceReserializeHelperWindow : EditorWindow
    {
        public ForceReserializeAssetsOptions option = ForceReserializeAssetsOptions.ReserializeAssetsAndMetadata;

        [MenuItem("Custom/"+nameof(ForceReserializeHelpers))]
        private static void Init()
        {
            GetWindow<ForceReserializeHelperWindow>();
        }

        protected void OnGUI()
        {
            WrappedLabel("AssetDatabase.ForceReserializeAssets all assets in the project.");
            WrappedLabel("Current scenes are all saved before processing.");
            WrappedLabel("Be sure to backup your project before using.");

            void WrappedLabel(string text)
            {
                GUILayout.Label(text, EditorStyles.wordWrappedLabel);
            }

            GUILayout.BeginVertical(GUI.skin.box);

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Option:");
            option = (ForceReserializeAssetsOptions) EditorGUILayout.EnumPopup(option);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Execute"))
            {
                AssetDatabase.ForceReserializeAssets(AssetDatabase.GetAllAssetPaths(), option);
            }

            GUILayout.EndVertical();
        }
    }
}