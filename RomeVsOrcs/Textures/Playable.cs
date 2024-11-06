using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
