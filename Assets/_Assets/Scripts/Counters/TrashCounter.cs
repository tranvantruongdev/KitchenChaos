using System;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnObjTrashed;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();

            OnObjTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
