using System;

namespace _Project.Scripts.UI
{
    public interface IWindow
    {
        event Action<IWindow> CloseRequested;

        void Show();
        void Close();
    }
}
