
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ListViewMAUI
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<ContactInfo>? contactsInfo;
        private ContactInfo? selectContact;
        private bool isReadOnly;

        #endregion

        #region Constructor

        public ViewModel()
        {
            GenerateSource(100);
        }

        #endregion

        #region Properties

        public ObservableCollection<ContactInfo>? ContactsInfo
        {
            get { return contactsInfo; }
            set
            {
                this.contactsInfo = value;
                OnPropertyChanged(nameof(contactsInfo));
            }
        }

        public ContactInfo? SelectedItem
        {
            get
            {
                return selectContact;
            }
            set
            {
                selectContact = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return isReadOnly;
            }
            set
            {
                isReadOnly = value;

                OnPropertyChanged(nameof(IsReadOnly));
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public bool IsVisible
        {
            get
            {
                return !IsReadOnly;
            }
        }

        public ObservableCollection<ContactOption>? ContactOptions { get; set; }

        public ObservableCollection<ContactOption>? CRUDOptions { get; set; }

        public ObservableCollection<ContactOption>? CommitOptions { get; set; }

        #endregion

        #region ItemSource

        public void GenerateSource(int count)
        {
            ContactsInfoRepository contactRepository = new();
            contactsInfo = contactRepository.GetContactDetails(count);
            InitializeCommands();
            InitializeContactOptions();
            InitializeCRUDOptions();
            InitializeCommitOptions();
        }

        #endregion

        #region  Commands
        public Command? CreateContactCommand { get; set; }
        public Command? SaveContactCommand { get; set; }
        public Command? CancelContactCommand { get; set; }
        public Command? EditContactCommand { get; set; }
        public Command? DeleteContactCommand { get; set; }
        public Command? TapCommand { get; set; }
        public Command? ShowContactCommand { get; set; }

        private void InitializeCommands()
        {
            CreateContactCommand = new Command(OnCreateContact);
            SaveContactCommand = new Command(OnSaveContact);
            CancelContactCommand = new Command(OnCancelContact);
            TapCommand = new Command(OnTapCommand);
            EditContactCommand = new Command(OnEditContactCommand);
            DeleteContactCommand = new Command(OnDeleteContactCommand);
            ShowContactCommand = new Command(OnShowContactCommand);
        }

        private void InitializeContactOptions()
        {
            ContactOptions = new ObservableCollection<ContactOption>();
            ContactOptions.Add(new ContactOption() { ActionName = "Call", ActionIcon = "\ue763" });
            ContactOptions.Add(new ContactOption() { ActionName = "Message", ActionIcon = "\ue759" });
            ContactOptions.Add(new ContactOption() { ActionName = "Video", ActionIcon = "\ue76b" });
            ContactOptions.Add(new ContactOption() { ActionName = "Info", ActionIcon = "\ue719" });
        }

        private void InitializeCRUDOptions()
        {
            CRUDOptions = new ObservableCollection<ContactOption>();
            CRUDOptions.Add(new ContactOption() { ActionName = "Edit", ActionIcon = "\ue73d" });
            CRUDOptions.Add(new ContactOption() { ActionName = "Delete", ActionIcon = "\ue73c" });
        }
        private void InitializeCommitOptions()
        {
            CommitOptions = new ObservableCollection<ContactOption>();
            CommitOptions.Add(new ContactOption() { ActionName = "Save", ActionIcon = "\ue726" });
            CommitOptions.Add(new ContactOption() { ActionName = "Cancel", ActionIcon = "\ue755" });
        }
        internal async void OnCreateContact()
        {
            SelectedItem = new ContactInfo();
            IsReadOnly = false;
            var editPage = new ContactPage();
            editPage.BindingContext = this;
            var navigation = Application.Current?.Windows[0].Navigation;
            if (navigation != null)
                await navigation.PushModalAsync(editPage);
        }

        internal async void OnCancelContact()
        {
            SelectedItem = null;
            var navigation = Application.Current?.Windows[0].Navigation;
            if (navigation != null)
                await navigation.PopModalAsync();
        }

        internal async void OnSaveContact()
        {
            if (contactsInfo != null && SelectedItem != null && !contactsInfo.Contains(SelectedItem) && App.Current != null)
            {
                contactsInfo.Add(SelectedItem);
            }
            var navigation = Application.Current?.Windows[0].Navigation;
            if (navigation != null)
                await navigation.PopModalAsync();
        }

        private void OnTapCommand(object eventArgs)
        {
            var tappedEventArgs = eventArgs as Syncfusion.Maui.ListView.ItemTappedEventArgs;
            if (tappedEventArgs != null)
            {
                SelectedItem = tappedEventArgs.DataItem as ContactInfo;
                if (SelectedItem == null)
                    return;
                SelectedItem.IsExpanded = !SelectedItem.IsExpanded;

                if (!SelectedItem.IsExpanded)
                    return;

                if (ContactsInfo != null)
                {
                    foreach (var contact in ContactsInfo)
                    {
                        if (contact != SelectedItem)
                            contact.IsExpanded = !SelectedItem.IsExpanded;
                    }
                }
            }
        }


        internal async void OnShowContactCommand()
        {
            IsReadOnly = true;
            var editPage = new ContactPage();
            editPage.BindingContext = this;
            var navigation = Application.Current?.Windows[0].Navigation;
            if (navigation != null)
                await navigation.PushModalAsync(editPage);
        }

        internal void OnEditContactCommand()
        {
            IsReadOnly = false;
        }

        internal async void OnDeleteContactCommand()
        {
            if (contactsInfo != null && SelectedItem != null)
            {
                contactsInfo.Remove(SelectedItem);
            }
            var navigation = Application.Current?.Windows[0].Navigation;
            if (navigation != null)
                await navigation.PopModalAsync();
        }

        #endregion

        #region Interface Member

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
