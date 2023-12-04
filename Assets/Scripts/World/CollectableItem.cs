using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
	public void Collect()
	{
		Destroy(gameObject);
		PlayCollectSound();
	}

	protected virtual void PlayCollectSound()
	{
		// override this function: AudioSource.PlayClipAtPoint and the specific sound
	}

}