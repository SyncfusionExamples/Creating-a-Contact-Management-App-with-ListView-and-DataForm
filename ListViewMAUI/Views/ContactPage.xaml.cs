using Syncfusion.Maui.Core;

namespace ListViewMAUI
{
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
        }

        private void OnChipClicked(object sender, EventArgs e)
        {
            var viewmodel = this.BindingContext as ViewModel;
            var chip = (sender as SfChip);
            var layout = chip.Children[0] as HorizontalStackLayout;
            var action = (layout.BindingContext as ContactOption).ActionName;
            if (string.IsNullOrEmpty(action))
                return;

            switch(action)
            {
                case "Edit":
                    viewmodel.OnEditContactCommand();
                    break;
                case "Delete":
                    viewmodel.OnDeleteContactCommand();
                    break;
                case "Save":
                    viewmodel.OnSaveContact();
                    contactForm.Commit();
                    break;
                case "Cancel":
                    viewmodel.OnCancelContact();
                    break;
            }
        }
    }
}