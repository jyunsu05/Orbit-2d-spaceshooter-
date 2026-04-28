#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine.Rendering;

[InitializeOnLoad]
public static class ForceWindowsGraphicsApi
{
    static ForceWindowsGraphicsApi()
    {
        ApplyForWindows(BuildTarget.StandaloneWindows);
        ApplyForWindows(BuildTarget.StandaloneWindows64);
    }

    private static void ApplyForWindows(BuildTarget target)
    {
        GraphicsDeviceType[] desiredApis = { GraphicsDeviceType.Direct3D11 };

        bool useDefault = PlayerSettings.GetUseDefaultGraphicsAPIs(target);
        GraphicsDeviceType[] currentApis = PlayerSettings.GetGraphicsAPIs(target);

        bool same = currentApis != null && currentApis.SequenceEqual(desiredApis);
        if (!useDefault && same)
            return;

        PlayerSettings.SetUseDefaultGraphicsAPIs(target, false);
        PlayerSettings.SetGraphicsAPIs(target, desiredApis);
    }
}
#endif
