using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundEffectsSO soundEffectsSO;

    private void Start()
    {
        CuttingCounter.OnCutting += OnCutting;
    }

    private void OnDestroy()
    {
        CuttingCounter.OnCutting -= OnCutting;
    }

    private void OnCutting(object sender, EventArgs e)
    {
        var cuttingCounter = sender as CuttingCounter;
        AudioClip[] arrCuttingSound = soundEffectsSO.ArrCuttingSound;
        PlaySoundAtPosition(arrCuttingSound[GetRandomIndexFromArray(arrCuttingSound)], cuttingCounter.transform.position);
    }

    private int GetRandomIndexFromArray(AudioClip[] arrSound)
    {
        return Random.Range(0, arrSound.Length);
    }

    private void PlaySoundAtPosition(AudioClip audioClip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClip, position);
    }
}
