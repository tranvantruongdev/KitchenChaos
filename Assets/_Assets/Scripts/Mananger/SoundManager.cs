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
        Player.Instance.OnDropKitchenObj += Player_OnDropKitchenObj;
        BaseCounter.OnPickupObj += BaseCounter_OnPickupObj;
        TrashCounter.OnObjTrashed += TrashCounter_OnObjTrashed;
    }

    private void TrashCounter_OnObjTrashed(object sender, EventArgs e)
    {
        var counter = sender as TrashCounter;
        PlaySoundAtPosition(soundEffectsSO.ArrTrashedObjSound[GetRandomIndexFromArray(soundEffectsSO.ArrTrashedObjSound)], counter.transform.position);
    }

    private void BaseCounter_OnPickupObj(object sender, EventArgs e)
    {
        var counter = sender as BaseCounter;
        PlaySoundAtPosition(soundEffectsSO.ArrPickupObjSound[GetRandomIndexFromArray(soundEffectsSO.ArrPickupObjSound)], counter.transform.position);
    }

    private void Player_OnDropKitchenObj(object sender, EventArgs e)
    {
        PlaySoundAtPosition(soundEffectsSO.ArrDropObjSound[GetRandomIndexFromArray(soundEffectsSO.ArrDropObjSound)], Player.Instance.transform.position);
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
