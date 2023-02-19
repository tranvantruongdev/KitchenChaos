using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] private int _maxPlate = 5;
    [SerializeField] private float _spawmInterval = 4f;
    [SerializeField] private GameObject _plateVisual;
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    [SerializeField] private float _yOffset = 0.1f;
    private List<GameObject> _plateListVisual = new();
    private float _timer;
    private Vector3 _offsetTransform;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawmInterval && _plateListVisual.Count <= _maxPlate)
        {
            _timer = 0;

            _offsetTransform.x = GetSpamPoint().position.x;
            _offsetTransform.y = GetSpamPoint().position.y + _yOffset * _plateListVisual.Count;
            _offsetTransform.z = GetSpamPoint().position.z;
            var plateVisual = Instantiate(_plateVisual, _offsetTransform, Quaternion.identity, GetSpamPoint());
            _plateListVisual.Add(plateVisual);
        }
    }

    public override void Interact(Player player)
    {
        // When player doesnt carrying anything
        // and has some plate on the counter
        if (!player.HasKitchenObject() && _plateListVisual.Count > 0)
        {
            KitchenObject.CreateKitchenObject(_kitchenObjectSO, player);
            var plateVisual = _plateListVisual[_plateListVisual.Count - 1];
            _plateListVisual.Remove(plateVisual);
            Destroy(plateVisual);
        }
    }
}
