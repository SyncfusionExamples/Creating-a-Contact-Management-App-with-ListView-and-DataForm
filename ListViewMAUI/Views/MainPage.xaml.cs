using Syncfusion.Maui.Core;

namespace ListViewMAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

    private void SfChipGroup_ChipClicked(object sender, EventArgs e)
    {
        var chip = (sender as SfChip);
        var label = chip.Children[0] as Label;
        var action = (label.BindingContext as ContactOption).ActionName;
        var viewModel = this.BindingContext as ViewModel;
        if (("Info").Equals(action))
        {
            viewModel.SelectedItem = (sender as SfChip).BindingContext as ContactInfo;
            viewModel.OnShowContactCommand();
        }
    }
    }

}
