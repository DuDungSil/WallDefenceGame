using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerSound : MonoBehaviour
{
    public float animationLength;
    public float workSoundDelay;
    public float workSoundDistance = 30f;
    public string workSound;

    private Coroutine coroutine;
    private GameObject temporarySoundPlayer;

    

    void OnEnable()
    {
        coroutine = StartCoroutine(WorkSound());
    }

    void OnDisable()
    {
        StopCoroutine(coroutine);
        if(temporarySoundPlayer != null) Destroy(temporarySoundPlayer);
    }

    private IEnumerator WorkSound()
    {

        while(true)
        {
            temporarySoundPlayer = SoundController.Instance.PlaySound3D(workSound, gameObject.transform, workSoundDelay, false, SoundType.SFX, true, 0, workSoundDistance);

            yield return new WaitForSeconds(animationLength);
        }

    }


}
