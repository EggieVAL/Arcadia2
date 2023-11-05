namespace Yolk.Engine
{
    public readonly struct CoroutineKey
    {
        private readonly CoroutineManager manager;
        private readonly Coroutine coroutine;

        public CoroutineKey(CoroutineManager manager, Coroutine coroutine)
        {
            this.manager = manager;
            this.coroutine = coroutine;
        }

        public bool IsPaused()
        {
            return coroutine is not null && coroutine.Paused;
        }

        public bool IsRunning()
        {
            return coroutine is not null && manager.IsRunning(coroutine);
        }

        public bool Pause()
        {
            return coroutine.Paused
                ? false
                : (coroutine.Paused = true);
        }

        public bool Resume()
        {
            return coroutine.Paused
                ? !(coroutine.Paused = false)
                : false;
        }

        public bool Terminate()
        {
            return manager.Terminate(coroutine);
        }
    }
}
