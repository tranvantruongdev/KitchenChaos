using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundEffectsSO soundEffectsSO;

    private void Start()
    {
        CuttingCounter.OnCutting += OnCutting;
        DeliveryManager.S_Instance.OnOderDeliver += DeliveryManager_OnOderDeliver;
        Player.Instance.OnPlayerMoving += Player_OnPlayerMoving;
    }

    private void Player_OnPlayerMoving(object sender, EventArgs e)
    {
        PlaySoundAtPosition(soundEffectsSO.ArrFootStepSound[GetRandomIndexFromArray(soundEffectsSO.ArrFootStepSound)], Player.Instance.transform.position);
    }

    private void DeliveryManager_OnOderDeliver(object sender, EventArgs e)
    {
        var deliveryStatusArgs = e as DeliveryManager.DeliveryStatusArgs;
        PlaySoundAtPosition((deliveryStatusArgs.IsSuccess ? soundEffectsSO.ArrDeliverySuccessSound : soundEffectsSO.ArrDeliveryFailSound)[GetRandomIndexFromArray(deliveryStatusArgs.IsSuccess ? soundEffectsSO.ArrDeliverySuccessSound : soundEffectsSO.ArrDeliveryFailSound)], DeliveryManager.S_Instance.transform.position);
    }

    private void OnDestroy()
    {
        CuttingCounter.OnCutting -= OnCutting;
    }

    private void OnCutting(object sender, EventArgs e)
    {
        var cuttingCounter = sender as CuttingCounter;
        PlaySoundAtPosition(soundEffectsSO.ArrCuttingSound[GetRandomIndexFromArray(soundEffectsSO.ArrCuttingSound)], cuttingCounter.transform.position);
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
