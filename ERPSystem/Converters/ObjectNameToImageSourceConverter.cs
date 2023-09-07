using ERPSystem.Utilities.GoogleStorage;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ERPSystem.Converters
{
    public class ObjectNameToImageSourceConverter : IValueConverter
    {
        string imagePathFolder = "C:\\Users\\derbo\\OneDrive\\Изображения\\40Tribes\\";
        CloudStorageService cloudStorageService;

        public ObjectNameToImageSourceConverter()
        {
            cloudStorageService = new CloudStorageService();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string objectName = value as string;

            if (!string.IsNullOrEmpty(objectName))
            {
                string imagePath = Path.Combine(imagePathFolder, objectName);

                if (File.Exists(imagePath))
                {
                    // Image exists in the local folder
                    return LoadImage(imagePath);
                }
                else
                {
                    // Image does not exist in the local folder, download from GCS and save locally
                    cloudStorageService.DownloadFile(objectName, imagePath);
                    return LoadImage(imagePath);
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private BitmapImage LoadImage(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            return bitmap;
        }
    }

}
