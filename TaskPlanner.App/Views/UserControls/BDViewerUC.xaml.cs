namespace TaskPlanner.App.Views.UserControls
{
    using System.Windows.Controls;
    using TaskPlanner.App.Utils;
    using TaskPlanner.App.ViewModels;

    /// <summary>
    /// Логика взаимодействия для BDViewerUC.xaml
    /// </summary>
    public partial class BDViewerUC : UserControl
    {
        public BDViewerVeiwModel ViewModel { get; }
        public BDViewerUC()
        {
            ViewModel = ServiceLocator.GetService<BDViewerVeiwModel>();
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
