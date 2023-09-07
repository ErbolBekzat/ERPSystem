using BitMiracle.LibJpeg.Classic;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Media3D;

public static class ImageCompressor
{
    public static void CompressImage(string sourceImagePath, string destImagePath, long compressionQuality)
    {
        Bitmap sourceImage = new Bitmap(sourceImagePath);

        jpeg_error_mgr errorManager = new jpeg_error_mgr();
        jpeg_compress_struct cinfo = new jpeg_compress_struct(errorManager);
        Stream outputStream = new FileStream(destImagePath, FileMode.Create);
        cinfo.jpeg_stdio_dest(outputStream);
        cinfo.Image_width = sourceImage.Width;
        cinfo.Image_height = sourceImage.Height;
        cinfo.Input_components = 3;
        cinfo.In_color_space = J_COLOR_SPACE.JCS_RGB;

        cinfo.jpeg_set_defaults();
        cinfo.jpeg_set_quality((int)compressionQuality, true);
        cinfo.jpeg_start_compress(true);

        byte[][] rowData = new byte[1][]; // single row
        int row_stride = cinfo.Image_width; // physical row width in buffer

        // Initialize the image buffer with appropriate dimensions
        // Assuming you have a Bitmap object named 'sourceImage'
        int imageWidth = sourceImage.Width;
        int imageHeight = sourceImage.Height;

        byte[][] image_buffer = new byte[imageHeight][];
        for (int y = 0; y < imageHeight; y++)
        {
            image_buffer[y] = new byte[imageWidth * 3]; // Assuming RGB color space

            for (int x = 0; x < imageWidth; x++)
            {
                Color pixelColor = sourceImage.GetPixel(x, y);
                int index = x * 3; // Index in the image buffer

                // Assign pixel values to the image buffer (assuming RGB color space)
                image_buffer[y][index] = pixelColor.R;     // Red component
                image_buffer[y][index + 1] = pixelColor.G; // Green component
                image_buffer[y][index + 2] = pixelColor.B; // Blue component
            }
        }


        while (cinfo.Next_scanline < cinfo.Image_height)
        {
            rowData[0] = image_buffer[cinfo.Next_scanline];
            cinfo.jpeg_write_scanlines(rowData, 1);
        }

        cinfo.jpeg_finish_compress();
        outputStream.Close();


    }

    private static byte GetPixelValue(int row, int column, Bitmap sourceImage)
    {
        Color pixelColor = sourceImage.GetPixel(column, row);

        // Return the desired pixel value based on your requirements
        // You might need to convert the pixel color to a specific channel value (e.g., R, G, or B)
        return pixelColor.R; // Example: Returning the red channel value
    }


    private static ImageCodecInfo GetJpegEncoder()
    {
        var codecs = ImageCodecInfo.GetImageEncoders();
        foreach (var codec in codecs)
        {
            if (codec.FormatID == ImageFormat.Jpeg.Guid)
            {
                return codec;
            }
        }
        return null;
    }
}
