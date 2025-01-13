using System.Linq;
using UnityEditor;

namespace ForceReserializeHelpers
{
    public static class ForceReserializeHelperMenuItems
    {
        #region Field

        // CAUTION:
        // To check existing menu's priority, check the following.
        // Edit / Preferences / Diagnostics / MenuDisplayPriority

        private const string BaseMenuPath     = "Assets/Force Reserialize/";
        private const int    BaseMenuPriority = 41; // after Refresh / Reimport (40)

        private const string MenuItemPathReserializeAssetsAndMetadata = BaseMenuPath + "Assets and Metadata";
        private const string MenuItemPathReserializeAssets            = BaseMenuPath + "Assets";
        private const string MenuItemPathReserializeMetadata          = BaseMenuPath + "Metadata";

        #endregion Field

        #region Method

        // NOTE:
        // MenuItem 2nd argument means "isValidateFunction".

        [MenuItem(MenuItemPathReserializeAssetsAndMetadata, false, BaseMenuPriority)]
        private static void ReserializeAssetsAndMetadataMenu()
        {
            ReserializeAssets(ForceReserializeAssetsOptions.ReserializeAssetsAndMetadata);
        }

        [MenuItem(MenuItemPathReserializeAssetsAndMetadata, true, BaseMenuPriority)]
        private static bool ReserializeAssetsAndMetadataMenuValidate()
        {
            return ValidateSelectionObjects();
        }

        [MenuItem(MenuItemPathReserializeAssets, false, BaseMenuPriority + 1)]
        private static void ReserializeAssetsMenu()
        {
            ReserializeAssets(ForceReserializeAssetsOptions.ReserializeAssets);
        }

        [MenuItem(MenuItemPathReserializeAssets, true, BaseMenuPriority + 1)]
        private static bool ReserializeAssetsMenuValidate()
        {
            return ValidateSelectionObjects();
        }

        [MenuItem(MenuItemPathReserializeMetadata, false, BaseMenuPriority + 2)]
        private static void ReserializeMetadataMenu()
        {
            ReserializeAssets(ForceReserializeAssetsOptions.ReserializeMetadata);
        }

        [MenuItem(MenuItemPathReserializeMetadata, true, BaseMenuPriority + 2)]
        private static bool ReserializeMetadataMenuValidate()
        {
            return ValidateSelectionObjects();
        }

        private static void ReserializeAssets(ForceReserializeAssetsOptions option)
        {
            var proceed = EditorUtility.DisplayDialog(
                nameof(ForceReserializeHelpers),
                "Resizing assets might cause the loss of settings or references.", 
                "Yes",
                "No"
            );

            if (!proceed)
            {
                return;
            }

            var selected   = Selection.objects;
            var assetPaths = selected.Select(AssetDatabase.GetAssetPath).ToArray();
            AssetDatabase.ForceReserializeAssets(assetPaths, option);
        }

        private static bool ValidateSelectionObjects()
        {
            return Selection.objects.All(selectedObject => AssetDatabase.Contains(selectedObject));
        }

        #endregion Method
    }
}