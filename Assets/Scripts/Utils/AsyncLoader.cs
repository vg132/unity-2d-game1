using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Utils
{
	public class AsyncLoader : MonoBehaviour
	{
		[Header("Menu Screens")]
		[SerializeField]
		private GameObject _loadingScreen;
		[SerializeField]
		private GameObject _mainMenu;

		[Header("Slider")]
		[SerializeField]
		private Slider _loadingSlider;

		public void LoadLevel(string name)
		{
			_mainMenu.SetActive(false);
			_loadingScreen.SetActive(true);
			StartCoroutine(LoadLevelAsync(name));
		}

		private IEnumerator LoadLevelAsync(string name)
		{
			var loadOperation = SceneManager.LoadSceneAsync(name);

			while (!loadOperation.isDone)
			{
				var loadingProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
				_loadingSlider.value = loadingProgress;
				yield return null;
			};
		}
	}
}
