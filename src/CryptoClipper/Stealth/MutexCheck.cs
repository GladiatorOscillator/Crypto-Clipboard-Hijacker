namespace CryptoClipper.Stealth;

public static class MutexCheck
{
    private const string MutexName = "Global\\{8F14E45F-CEEA-367F-A27F-C790AB85D7E8}";
    private static Mutex? _mutex;

    public static bool TryAcquire()
    {
        try
        {
            _mutex = new Mutex(initiallyOwned: true, MutexName, out bool createdNew);
            if (!createdNew)
            {
                _mutex.Dispose();
                _mutex = null;
                return false;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static void Release()
    {
        if (_mutex is not null)
        {
            _mutex.ReleaseMutex();
            _mutex.Dispose();
            _mutex = null;
        }
    }
}
