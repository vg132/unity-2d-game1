using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestBehaviour : StateMachineBehaviour
{
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		MusicManager.Instance.PlaySound(MusicManager.GameSounds.Finish);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
