namespace CryEngine.Game
{
    using System;
    using CryEngine.Resources;
    using CryEngine.UI;
    using CryEngine.UI.Components;

    public class Game : IGameUpdateReceiver, IDisposable
    {
        private static Game instance;

        private Game()
        {
            GameFramework.RegisterForUpdate(this);
            Mouse.ShowCursor();
            Engine.EngineReloaded += CreateUi;
            CreateUi();
        }

        public static void Initialize()
        {
            if (instance == null)
            {
                instance = new Game();
            }
        }

        public void Dispose()
        {
            GameFramework.UnregisterFromUpdate(this);
        }

        private static void CreateUi()
        {
            var canvas = SceneObject.Instantiate<Canvas>(SceneManager.RootObject);
            var logoElement = SceneObject.Instantiate<UIElement>(canvas);
            var logoImage = logoElement.AddComponent<Image>();
            var quitButton = SceneObject.Instantiate<Button>(canvas);

            logoElement.RectTransform.Size = new Point(300f, 300f);
            logoElement.RectTransform.Alignment = Alignment.Center;
            logoImage.Source = ResourceManager.ImageFromFile(UIElement.DataDirectory + "/GDME.png", true);

            quitButton.RectTransform.Alignment = Alignment.Bottom;
            quitButton.RectTransform.Padding = new Padding(0f, -160f);
            quitButton.RectTransform.Size = new Point(300f, 30f);

            quitButton.Ctrl.Text.Content = "Quit";
            quitButton.Ctrl.Text.Height = 18;
            quitButton.Ctrl.Text.DropsShadow = true;
            quitButton.Ctrl.Text.Alignment = Alignment.Center;

            quitButton.Ctrl.OnPressed += OnQuitButtonPressed;
        }

        private static void OnQuitButtonPressed()
        {
            Engine.Shutdown();
        }

        public static void Shutdown()
        {
            instance?.Dispose();
            instance = null;
        }

        public void OnUpdate()
        {
            // Nothing to Update
        }
    }
}