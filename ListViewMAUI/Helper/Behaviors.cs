using Syncfusion.Maui.DataSource;

#nullable disable

namespace ListViewMAUI
{
    #region Behavior
    public class SfListViewGroupingBehavior : Behavior<ContentPage>
    {
        #region Fields

        private Syncfusion.Maui.ListView.SfListView ListView;
        private SearchBar searchBar = null;

        #endregion

        #region Overrides
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = ListView = bindable.FindByName<Syncfusion.Maui.ListView.SfListView>("listView"); 
            searchBar = bindable.FindByName<SearchBar>("searchBar");
            ListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "ContactName",
                KeySelector = (obj1) =>
                {
                    var item = obj1 as ContactInfo;                    
                    return (item is not null && (item.ContactName.Length > 0 ) ) ? item.ContactName[0].ToString() : "Empty";
                },
            });
            ListView.DataSource.SortDescriptors.Add(new SortDescriptor()
            {
                PropertyName = "ContactName",
            });
            searchBar.TextChanged += SearchBar_TextChanged;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            ListView = null;
            searchBar = null;
            searchBar.TextChanged -= SearchBar_TextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as SearchBar);
            if (ListView.DataSource != null)
            {
                ListView.DataSource.Filter = FilterContacts;
                ListView.DataSource.RefreshFilter();
            }
            ListView.RefreshView();
        }
        private bool FilterContacts(object obj)
        {
            if (searchBar == null || searchBar.Text == null)
                return true;

            var contactInfo = obj as ContactInfo;
            return (contactInfo.ContactName.ToLower().Contains(searchBar.Text.ToLower()) || (contactInfo.ContactNumber.ToString()).ToLower().Contains(searchBar.Text.ToLower()));
        }


        #endregion
    }
    #endregion
}
