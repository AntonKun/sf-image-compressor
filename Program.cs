using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using ImageMagick;
using Aspose.Imaging;
using Aspose.Imaging.ImageOptions;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting image compression...");

        // Приклад використання ImageSharp
        CompressImageWithImageSharp("/opt/images/image1.jpg", "/opt/images/compressed/image1-image-sharp.jpg", 20);
        CompressImageWithImageSharp("/opt/images/image2.jpeg", "/opt/images/compressed/image2-image-sharp.jpeg", 20);
        CompressImageWithImageSharp("/opt/images/image3.jpg", "/opt/images/compressed/image3-image-sharp.jpg", 20);
        CompressImageWithImageSharp("/opt/images/image4.png", "/opt/images/compressed/image4-image-sharp.png", 20);

        // Приклад використання Magick.NET
        CompressImageWithMagickNET("/opt/images/image1.jpg", "/opt/images/compressed/image1-magicknet.jpg", 20);
        CompressImageWithMagickNET("/opt/images/image2.jpeg", "/opt/images/compressed/image2-magicknet.jpeg", 20);
        CompressImageWithMagickNET("/opt/images/image3.jpg", "/opt/images/compressed/image3-magicknet.jpg", 20);
        CompressImageWithMagickNET("/opt/images/image4.png", "/opt/images/compressed/image4-magicknet.png", 20);

        // Приклад використання Aspose.Imaging
        CompressImageWithAspose("/opt/images/image1.jpg", "/opt/images/compressed/image1-aspose.jpg", 20);
        CompressImageWithAspose("/opt/images/image2.jpeg", "/opt/images/compressed/image2-magicknet.jpeg", 20);
        CompressImageWithAspose("/opt/images/image3.jpg", "/opt/images/compressed/image3-magicknet.jpg", 20);
        CompressImageWithAspose("/opt/images/image4.png", "/opt/images/compressed/image4-magicknet.png", 20);

        Console.WriteLine("Image compression completed.");
    }

    static void CompressImageWithImageSharp(string inputPath, string outputPath, int quality)
    {
        using (var image = SixLabors.ImageSharp.Image.Load(inputPath))
        {
            var encoder = new JpegEncoder { Quality = quality };
            image.Save(outputPath, encoder);
        }
    }

    static void CompressImageWithMagickNET(string inputPath, string outputPath, int quality)
    {
        using (var image = new MagickImage(inputPath))
        {
            image.Quality = quality;
            image.Format = MagickFormat.Jpeg; // or Png based on your need
            image.Write(outputPath);
        }
    }

    static void CompressImageWithAspose(string inputPath, string outputPath, int quality)
    {
        using (var image = Aspose.Imaging.Image.Load(inputPath))
        {
            var options = new JpegOptions { Quality = quality };
            image.Save(outputPath, options);
        }
    }
}
