using Google.Apis.Auth.OAuth2;
using Google.Apis.PhotosLibrary.v1;
using Google.Apis.PhotosLibrary.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ERPSystem.ImageManagement
{
    public class GooglePhotosService
    {
        private const string ApplicationName = "imagestockforwpfapp";
        private const string ClientSecretJsonPath = "ERPSystem/ImageManagement/Creadentials/client_secret_1051684562817-8flkmscgg8f3fuufohc7itnbio804oe3.apps.googleusercontent.com.json";
        private static readonly string[] Scopes = { PhotosLibraryService.Scope.PhotoslibraryReadonly };

        private UserCredential _credential;
        private PhotosLibraryService _service;

        public GooglePhotosService()
        {
            _credential = GetUserCredential();
            _service = new PhotosLibraryService(new BaseClientService.Initializer
            {
                HttpClientInitializer = _credential,
                ApplicationName = ApplicationName
            });
        }

        private UserCredential GetUserCredential()
        {
            using (var stream = new FileStream(ClientSecretJsonPath, FileMode.Open, FileAccess.Read))
            {
                var credentialPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".credentials", "google-photos.json");
                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credentialPath, true)).Result;
            }
        }

        public async Task<List<Photo>> GetPhotos()
        {
            var albums = await _service.Albums.List().ExecuteAsync();
            var photos = new List<Photo>();

            foreach (var album in albums.Albums)
            {
                var albumPhotos = await _service.MediaItems.Search(new SearchMediaItemsRequest
                {
                    AlbumId = album.Id,
                    PageSize = 100
                }).ExecuteAsync();

                photos.AddRange((IEnumerable<Photo>)albumPhotos.MediaItems);
            }

            return photos;
        }

        public async Task<BitmapImage> DownloadImage(string imageUrl)
        {
            var response = await _service.MediaItems.Get(imageUrl).ExecuteAsync();

            if (response.BaseUrl == null || response.MediaMetadata == null)
                return null;

            var imageUri = new Uri($"{response.BaseUrl}=w{response.MediaMetadata.Width}-h{response.MediaMetadata.Height}");
            var bitmap = new BitmapImage(imageUri);

            return bitmap;
        }


        public async Task<string> SaveImage(BitmapImage image, string albumId)
        {
            using (var stream = new MemoryStream())
            {
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);

                var request = _service.MediaItems.BatchCreate(
                    new BatchCreateMediaItemsRequest
                    {
                        AlbumId = albumId,
                        NewMediaItems = new List<NewMediaItem>
                        {
                    new NewMediaItem
                    {
                        SimpleMediaItem = new SimpleMediaItem
                        {
                            UploadToken = Convert.ToBase64String(stream.ToArray())
                        }
                    }
                        }
                    });

                var response = await request.ExecuteAsync();
                var mediaItem = response?.NewMediaItemResults?.FirstOrDefault()?.MediaItem;

                return mediaItem?.Id;
            }
        }

        public async Task<string> CreateAlbum(string albumTitle)
        {
            var createAlbumRequest = new CreateAlbumRequest
            {
                Album = new Album { Title = albumTitle }
            };

            var createdAlbum = await _service.Albums.Create(createAlbumRequest).ExecuteAsync();
            return createdAlbum?.Id;
        }

        public async Task<string> GetAlbumId(string albumTitle)
        {
            var albums = await _service.Albums.List().ExecuteAsync();
            var album = albums.Albums.FirstOrDefault(a => a.Title == albumTitle);

            return album?.Id;
        }


    }
}
