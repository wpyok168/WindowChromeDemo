using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace WpfChrome
{
    public class WindowStyleViewModel: ViewModelBase
    {
        private string titleBarColor= "#673AB7"; 
        
        public RelayCommand<object> MinimizeCommand { get; }
        public RelayCommand<object> MaximizeCommand { get; }
        public RelayCommand<object> CloseCommand { get; }
        private PropertyInfo[] colorPropertyInfo = typeof(Colors).GetProperties();
        private RelayCommand<object> colorTitleBarCommand;
        private RelayCommand RelayCommand;


        public string TitleBarColor { get => titleBarColor; set { titleBarColor = value; RaisePropertyChanged(); } }

        public PropertyInfo[] ColorPropertyInfo { get => colorPropertyInfo; set => colorPropertyInfo = value; }
        public RelayCommand<object> ColorTitleBarCommand { get => colorTitleBarCommand; set => colorTitleBarCommand = value; }

        public WindowStyleViewModel()
        {
            MinimizeCommand = new RelayCommand<object>(MinimizeWindow);
            MaximizeCommand = new RelayCommand<object>(MaximizeWindow);
            CloseCommand = new RelayCommand<object>(CloseWindow);
            colorTitleBarCommand = new RelayCommand<object>(this.ColorTitleBarChanged);
        }

        private void ColorTitleBarChanged(object obj)
        {
            if (obj == null)
            {
                return;
            }
            // Color cl = (Color)(obj as PropertyInfo).GetValue(null, null);
            //new SolidColorBrush(ColorConverter.ConvertFromString("#673AB7"))
            PropertyInfo pi = obj as PropertyInfo;
            TitleBarColor = pi.Name;
        }

        private void CloseWindow(object obj)
        {
            SystemCommands.CloseWindow(obj as Window);
        }

        private void MaximizeWindow(object obj)
        {
            var mainWindow = obj as Window;
            switch (mainWindow.WindowState)
            {
                case WindowState.Normal:
                    SystemCommands.MaximizeWindow(mainWindow);
                    break;
                case WindowState.Minimized:
                    break;
                case WindowState.Maximized:
                    SystemCommands.RestoreWindow(mainWindow);
                    break;
                default:
                    break;
            }
        }

        private void MinimizeWindow(object obj)
        {
            SystemCommands.MinimizeWindow(obj as Window);
        }
    }
}
