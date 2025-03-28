using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace SimpleGraphicsEngine;

class Program
{
    static void Main(string[] args)
    {

        var nativeWindowSettings =  new NativeWindowSettings() {
            ClientSize = new OpenTK.Mathematics.Vector2i(800, 600),
            Title = "SimpleGraphicsEngine",
            Flags = ContextFlags.ForwardCompatible,
        };

        using var window = new Window(GameWindowSettings.Default, nativeWindowSettings);
        window.Run();

    }
}
