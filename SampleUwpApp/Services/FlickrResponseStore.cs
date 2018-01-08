using SampleUwpApp.Models;
using SampleUwpApp.Services.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace SampleUwpApp.Services
{
    /// <summary>
    /// This class reads the API, parses it and provides the elements from REST API to the UI. It also updates the UI and supports incremental loading.
    /// </summary>
    class FlickrResponseStore : IList, INotifyCollectionChanged, ISupportIncrementalLoading
    {
        // Flickr API related properties and functions.
        private List<PartialPhotoDetails> photos;
        private PhotosSearchApiReader apiReader;
        private string keywords;
        private int perPage;

        // public photos
        public List<FlickrResponseViewModel> viewModel; 

        public FlickrResponseStore(string keywords, int itemsPerPage)
        {
            photos = new List<PartialPhotoDetails>();
            viewModel = new List<FlickrResponseViewModel>();
            this.keywords = keywords;
            this.perPage = itemsPerPage;
            apiReader = new PhotosSearchApiReader(itemsPerPage);
        }

        // This function is called by the incremental loading helpers and it instructs the API readers to read "next page".
        public async Task<LoadMoreItemsResult> ReadApiAheadAsync()
        {
            var holder = new LoadMoreItemsResult();

            var currentSize = photos.Count;
            var responseObject = await apiReader.GetResponseAsync(keywords);
            photos.AddRange(responseObject.photos.photo);

            // Mark the count of elements loaded.
            holder.Count = (uint)responseObject.photos.photo.Count;

            // Create the ViewModel
            foreach (var photo in photos)
            {
                // Store the important photo details.
                var item = new FlickrResponseViewModel();
                item.Description = photo.title;
                item.Id = photo.id;
                item.UserId = photo.owner;
                item.PhotoId = photo.id;
                item.PhotoSecret = photo.secret;

                var url = $"https://farm{photo.farm}.staticflickr.com/{photo.server}/{photo.id}_{photo.secret}.jpg";

                // Small image 320x320 for efficient loading on search results page.
                var smallImageUrl = $"https://farm{photo.farm}.staticflickr.com/{photo.server}/{photo.id}_{photo.secret}_n.jpg";
                item.ImageUri = new Uri(url);
                item.SmallImageUri = new Uri(smallImageUrl);

                viewModel.Add(item);
            }

            // Raise the event to update UI
            UpdateUIForItems(currentSize, perPage);
            return holder;
        }
        
        // This function updates the UI for any updates and changes made to the items collection.
        void UpdateUIForItems(int baseIndex, int count)
        {
            if (CollectionChanged == null)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, photos[i + baseIndex], i + baseIndex);
                CollectionChanged(this, args);
            }
        }

        public void Dispose()
        {
            apiReader.Dispose();
        }

        // The public event to be raised.
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        // ISupportIncrementalLoading interface being implemented here.
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return ReadApiAheadAsync().AsAsyncOperation();
        }

        // IList interface being implemented.
        public int Add(object value)
        {
            photos.Add((PartialPhotoDetails)value);
            return photos.Count;
        }

        public void Clear()
        {
            photos.Clear();
        }

        public bool Contains(object value)
        {
            return photos.Contains((PartialPhotoDetails)value);
        }

        public int IndexOf(object value)
        {
            return photos.IndexOf((PartialPhotoDetails)value);
        }

        public void Insert(int index, object value)
        {
            photos.Insert(index, (PartialPhotoDetails)value);
        }

        public void Remove(object value)
        {
            photos.Remove((PartialPhotoDetails)value);
        }

        public void RemoveAt(int index)
        {
            photos.RemoveAt(index);
        }

        public void CopyTo(Array array, int index)
        {
            photos.CopyTo((PartialPhotoDetails[])array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return photos.GetEnumerator();
        }

        public bool HasMoreItems => true;

        public bool IsFixedSize => false;

        public bool IsReadOnly => false;

        public int Count => photos.Count;

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public object this[int index] { get => viewModel[index]; set => viewModel[index] = (FlickrResponseViewModel)value; }
    }
}
