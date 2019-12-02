using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace appLauncher.Helpers
{
    public class PagniationAsyncObservableCollection<AppItems> : ObservableCollection<AppItems>
    {
        private SynchronizationContext _synchronizationContext = SynchronizationContext.Current;
        private List<AppItems> originalCollection;
        private int _currentPageIndex;
        private int _itemCountPerPage;
        public int PageSize
        {
            get { return _itemCountPerPage; }
            set
            {
                if (value >= 0)
                {
                    _itemCountPerPage = value;
                    RecalculateThePageItems();
                    OnPropertyChanged(new PropertyChangedEventArgs("PageSize"));
                }
            }
        }

        public int CurrentPage
        {
            get { return _currentPageIndex; }
            set
            {
                if (value >= 0)
                {
                    _currentPageIndex = value;
                    RecalculateThePageItems();
                    OnPropertyChanged(new PropertyChangedEventArgs("CurrentPage"));
                }
            }
        }
        public PagniationAsyncObservableCollection()
        {
        }

        public PagniationAsyncObservableCollection(IEnumerable<AppItems> list)

        {
            originalCollection = (List<AppItems>)list;
        }

        private void RecalculateThePageItems()
        {
            Clear();
            int startIndex = _currentPageIndex * _itemCountPerPage;

            for (int i = startIndex; i < startIndex + _itemCountPerPage; i++)
            {
                if (originalCollection.Count > i)

                    base.InsertItem(i - startIndex, originalCollection[i]);
            }
           
        }


        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (SynchronizationContext.Current == _synchronizationContext)
            {
                // Execute the CollectionChanged event on the current thread
                RaiseCollectionChanged(e);
            }
            else
            {
                // Raises the CollectionChanged event on the creator thread
                _synchronizationContext.Send(RaiseCollectionChanged, e);
            }
        }

        private void RaiseCollectionChanged(object param)
        {
            // We are in the creator thread, call the base implementation directly
            base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (SynchronizationContext.Current == _synchronizationContext)
            {
                // Execute the PropertyChanged event on the current thread
                RaisePropertyChanged(e);
            }
            else
            {
                // Raises the PropertyChanged event on the creator thread
                _synchronizationContext.Send(RaisePropertyChanged, e);
            }
        }

        private void RaisePropertyChanged(object param)
        {
            // We are in the creator thread, call the base implementation directly
            base.OnPropertyChanged((PropertyChangedEventArgs)param);
        }
    }
}
