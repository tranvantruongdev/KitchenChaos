using System;

public interface IHasProgress
{
    public event EventHandler<ProgressArgs> OnProgressChanged;
    public class ProgressArgs : EventArgs
    {
        public float progress;
    }
}
