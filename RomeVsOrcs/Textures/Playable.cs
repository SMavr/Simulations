namespace RomeVsOrcs.Textures;
public abstract class Playable
{

    // Is the animation currently running?
    public bool IsPaused { get; private set; }

    protected abstract void Reset();

    protected void Stop()
    {
        Pause();
        Reset();
    }

    protected void Play()
    {
        IsPaused = false;
    }

    protected void Pause()
    {
        IsPaused = true;
    }
}
