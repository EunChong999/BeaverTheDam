using System.Collections;
using UnityEngine;
using TMPro;

public class DialogTest : MonoBehaviour
{
	private	DialogSystem	dialogSystem;

	private IEnumerator Start()
	{
		dialogSystem = DialogSystem.instance;

		if (dialogSystem == null) 
			yield break;

		yield return new WaitUntil(()=>dialogSystem.UpdateDialog());

		dialogSystem.EndDialogSystem();
	}
}

