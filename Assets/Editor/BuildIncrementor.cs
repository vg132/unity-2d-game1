using GameOne.Utils;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildIncrementor : IPreprocessBuildWithReport
{
	public int callbackOrder => 1;

	public void OnPreprocessBuild(BuildReport report)
	{
		var buildScriptableObject = ScriptableObject.CreateInstance<BuildScriptableObject>();
		switch (report.summary.platform)
		{
			case BuildTarget.StandaloneWindows:
			case BuildTarget.StandaloneWindows64:
			case BuildTarget.StandaloneLinux64:
			case BuildTarget.StandaloneOSX:
				PlayerSettings.macOS.buildNumber = IncrementBuildNumber(PlayerSettings.macOS.buildNumber);
				buildScriptableObject.BuildNumber = PlayerSettings.macOS.buildNumber;
				break;
			case BuildTarget.Android:
				PlayerSettings.Android.bundleVersionCode++;
				buildScriptableObject.BuildNumber = PlayerSettings.Android.bundleVersionCode.ToString();
				break;
			case BuildTarget.WSAPlayer:
				PlayerSettings.WSA.packageVersion = new System.Version(PlayerSettings.WSA.packageVersion.Major, PlayerSettings.WSA.packageVersion.Minor, PlayerSettings.WSA.packageVersion.Build + 1);
				buildScriptableObject.BuildNumber = PlayerSettings.WSA.packageVersion.ToString();
				break;
			case BuildTarget.PS4:
				PlayerSettings.PS4.appVersion = IncrementBuildNumber(PlayerSettings.PS4.appVersion);
				buildScriptableObject.BuildNumber = PlayerSettings.PS4.appVersion;
				break;
			case BuildTarget.iOS:
				PlayerSettings.iOS.buildNumber = IncrementBuildNumber(PlayerSettings.iOS.buildNumber);
				buildScriptableObject.BuildNumber = PlayerSettings.iOS.buildNumber;
				break;
		}
		AssetDatabase.DeleteAsset("Assets/Resources/Build.asset");
		AssetDatabase.CreateAsset(buildScriptableObject, "Assets/Resources/Build.asset");
		AssetDatabase.SaveAssets();
	}

	private string IncrementBuildNumber(string buildNumber)
	{
		int.TryParse(buildNumber, out int outputBuildNumber);
		return (outputBuildNumber + 1).ToString();
	}
}
