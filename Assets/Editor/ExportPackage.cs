using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Export scripts and demos of this project for use in other projects.
/// </summary>
public class ExportPackage : MonoBehaviour
{
    /// <summary>
    /// Name of this project for finding assets and exporting.
    /// </summary>
    public static string ProjectName = "PoolSet";

    /// <summary>
    /// Path of assets for the demo export.
    /// </summary>
    public static readonly string[] AssetPaths = {
        Path.Combine(ScriptBatch.AssetDirectory, Constants.ProjectName),
    };

    /// <summary>
    /// Path of just the scripts for the demo export.
    /// </summary>
    public static readonly string[] ScriptsAssetPaths = {
        Path.Combine(ScriptBatch.AssetDirectory, Constants.ProjectName, "Scripts"),
    };

    /// <summary>
    /// File path for exporting the demo package.
    /// </summary>
    public static string PackagePath => Path.Combine(
        ScriptBatch.BuildDirectory, Constants.ProjectName + $"-Examples-{ScriptBatch.VersionNumber}.unitypackage");

    /// <summary>
    /// File path for exporting just the scripts from the demo project.
    /// </summary>
    /// <returns></returns>
    public static string ScriptPackagePath => Path.Combine(
        ScriptBatch.BuildDirectory, Constants.ProjectName + $"-{ScriptBatch.VersionNumber}.unitypackage");

    /// <summary>
    /// Batched method to export all types of packages.
    /// </summary>
    [MenuItem("Build/Package/Export All Packages")]
    public static void ExportAllPackages()
    {
        ExportExampleAssetPackage();
        ExportScriptsAssetPackage();
    }

    /// <summary>
    /// Command to export the entire demo project.
    /// </summary>
    [MenuItem("Build/Package/Export Example Package")]
    public static void ExportExampleAssetPackage()
    {
        AssetDatabase.ExportPackage(
            AssetPaths,
            PackagePath,
            ExportPackageOptions.Recurse |
                ExportPackageOptions.Interactive |
                ExportPackageOptions.IncludeDependencies);
    }

    /// <summary>
    /// Command to export just the scripts for the demo project.
    /// </summary>
    [MenuItem("Build/Package/Export Scripts Package")]
    public static void ExportScriptsAssetPackage()
    {
        AssetDatabase.ExportPackage(
            ScriptsAssetPaths,
            ScriptPackagePath,
            ExportPackageOptions.Recurse |
                ExportPackageOptions.Interactive |
                ExportPackageOptions.IncludeDependencies);
    }

}
