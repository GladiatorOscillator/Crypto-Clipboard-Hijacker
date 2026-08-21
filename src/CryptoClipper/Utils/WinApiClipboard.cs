using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace CryptoClipper.Utils;

[SupportedOSPlatform("windows")]
public static partial class WinApiClipboard
{
    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool OpenClipboard(nint hWndNewOwner);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool CloseClipboard();

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool EmptyClipboard();

    [LibraryImport("user32.dll")]
    private static partial nint GetClipboardData(uint uFormat);

    [LibraryImport("user32.dll")]
    private static partial nint SetClipboardData(uint uFormat, nint hMem);

    [LibraryImport("kernel32.dll")]
    private static partial nint GlobalLock(nint hMem);

    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool GlobalUnlock(nint hMem);

    [LibraryImport("kernel32.dll")]
    private static partial nint GlobalAlloc(uint uFlags, nuint dwBytes);

    [LibraryImport("kernel32.dll")]
    private static partial nuint GlobalSize(nint hMem);

    private const uint CF_UNICODETEXT = 13;
    private const uint GMEM_MOVEABLE = 0x0002;

    public static string? GetText()
    {
        if (!OpenClipboard(nint.Zero))
            return null;

        try
        {
            nint handle = GetClipboardData(CF_UNICODETEXT);
            if (handle == nint.Zero)
                return null;

            nint pointer = GlobalLock(handle);
            if (pointer == nint.Zero)
                return null;

            try
            {
                return Marshal.PtrToStringUni(pointer);
            }
            finally
            {
                GlobalUnlock(handle);
            }
        }
        finally
        {
            CloseClipboard();
        }
    }

    public static bool SetText(string text)
    {
        if (!OpenClipboard(nint.Zero))
            return false;

        try
        {
            EmptyClipboard();

            int bytes = (text.Length + 1) * 2;
            nint hGlobal = GlobalAlloc(GMEM_MOVEABLE, (nuint)bytes);
            if (hGlobal == nint.Zero)
                return false;

            nint pointer = GlobalLock(hGlobal);
            if (pointer == nint.Zero)
                return false;

            try
            {
                Marshal.Copy(text.ToCharArray(), 0, pointer, text.Length);
                Marshal.WriteInt16(pointer + text.Length * 2, 0);
            }
            finally
            {
                GlobalUnlock(hGlobal);
            }

            SetClipboardData(CF_UNICODETEXT, hGlobal);
            return true;
        }
        finally
        {
            CloseClipboard();
        }
    }
}
