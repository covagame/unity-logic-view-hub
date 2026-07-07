using System.IO;
using System.Linq;
using UnityEditor;

namespace Covagame.LVH.EditorTool
{
    public static class ScriptTemplatesInstaller
    {
        private const string SourceFolder = "Packages/jp.covagame.logic-view-hub/Editor/Templates";
        private const string DestinationFolder = "Assets/ScriptTemplates";

        [MenuItem("CovaGame/Logic-View-Hub/Script Templates/Install or Update", priority = 10)]
        public static void InstallOrUpdate()
        {
            if (!Directory.Exists(SourceFolder))
            {
                EditorUtility.DisplayDialog("Script Templates", $"Source not found:\n{SourceFolder}", "OK");
                return;
            }

            Directory.CreateDirectory(DestinationFolder);

            var txtFiles = Directory.GetFiles(SourceFolder, "*.txt", SearchOption.AllDirectories);
            var copied = 0;

            foreach (var srcPath in txtFiles)
            {
                var fileName = Path.GetFileName(srcPath);
                var dstPath = Path.Combine(DestinationFolder, fileName).Replace("\\", "/");
                File.Copy(srcPath, dstPath, true);
                copied++;
            }

            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog(
                "Script Templates",
                $"Installed/Updated: {copied} file(s)\n\nRestarting the Unity Editor is recommended.",
                "OK");
        }

        [MenuItem("CovaGame/Logic-View-Hub/Uninstall (Remove from Assets/ScriptTemplates)", priority = 11)]
        public static void Uninstall()
        {
            if (!Directory.Exists(DestinationFolder))
            {
                EditorUtility.DisplayDialog("Script Templates", "Nothing to remove.", "OK");
                return;
            }

            var txtFiles = Directory.GetFiles(DestinationFolder, "*.txt", SearchOption.TopDirectoryOnly);
            foreach (var path in txtFiles)
            {
                File.Delete(path);
            }

            if (!Directory.EnumerateFileSystemEntries(DestinationFolder).Any())
            {
                Directory.Delete(DestinationFolder, true);
            }

            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog(
                "Script Templates",
                "Uninstalled.\n\nExisting menu items may remain until the Unity Editor is restarted.",
                "OK");
        }
    }
}
