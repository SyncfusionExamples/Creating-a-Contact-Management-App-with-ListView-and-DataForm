using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ListViewMAUI
{
    public class ContactInfo : INotifyPropertyChanged
    {
        #region Fields

        private string? contactName = string.Empty;      
        private string? image = string.Empty;
        private string? contactNumber = string.Empty;        
        private string contactType = string.Empty;
        private bool isExpanded = false;

        #endregion

        #region Constructor

        public ContactInfo()
        {

        }

        #endregion

        #region Public Properties

        [Display(Name = "Name")]
        public string? ContactName
        {
            get { return this.contactName; }
            set
            {
                this.contactName = value;
                RaisePropertyChanged("ContactName");
            }
        }

        [Display(AutoGenerateField = false)]
        public string? ContactImage
        {
            get { return this.image; }
            set
            {
                if (value != null)
                {
                    this.image = value;
                    this.RaisePropertyChanged("ContactImage");
                }
            }
        }

        [Display(Name = "Number")]
        public string? ContactNumber
        {
            get
            {
                return this.contactNumber;
            }
            set
            {
                if (value != null)
                {
                    this.contactNumber = value;
                    this.RaisePropertyChanged("ContactNumber");
                }
            }
        }

        [Display(Name = "Type")]
        public string ContactType
        {
            get { return contactType; }
            set
            {
                this.contactType = value;
                RaisePropertyChanged("ContactType");
            }
        }

        [Display(AutoGenerateField =false)]
        public bool IsExpanded
        {
            get
            {
                return isExpanded;
            }
            set
            {
                isExpanded = value;
                this.RaisePropertyChanged("IsExpanded");
            }
        }
        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(String name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }

    public class ContactOption
    {
        public string ActionName { get; set;}

        public string ActionIcon { get; set;}
    }
}
