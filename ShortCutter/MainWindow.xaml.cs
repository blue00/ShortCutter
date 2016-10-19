using System.Windows;

namespace ShortCutter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataModel model = (DataModel)DataContext;
            model.Save();
            model.Dispose();
        }
    }
}
