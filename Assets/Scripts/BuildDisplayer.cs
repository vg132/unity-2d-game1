using TMPro;
using UnityEngine;

public class BuildDisplayer : MonoBehaviour
{
	private TextMeshProUGUI _buildText;

	private void Awake()
	{
		_buildText = GetComponent<TextMeshProUGUI>();
		var resourceLoader = Resources.LoadAsync<BuildScriptableObject>("Build");
		resourceLoader.completed += ResourceLoader_completed;
	}

	private void ResourceLoader_completed(AsyncOperation obj)
	{
		var buildScriptableObject = ((ResourceRequest)obj).asset as BuildScriptableObject;
		if (buildScriptableObject != null)
		{
			_buildText.SetText($"Build: v{Application.version}.{buildScriptableObject.BuildNumber}");
		}
		else
		{
			Debug.LogError($"Expected {nameof(BuildScriptableObject)} but found nothing in assets/resources/build.asset");
		}
	}
}
