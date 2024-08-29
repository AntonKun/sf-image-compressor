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
        CompressImageWithImageSharp("ImageSharp", "/opt/images/image1.jpg", "/opt/images/compressed/image1-image-sharp.jpg", 20);
        CompressImageWithImageSharp("ImageSharp", "/opt/images/image2.jpeg", "/opt/images/compressed/image2-image-sharp.jpeg", 20);
        CompressImageWithImageSharp("ImageSharp", "/opt/images/image3.jpg", "/opt/images/compressed/image3-image-sharp.jpg", 20);
        CompressImageWithImageSharp("ImageSharp", "/opt/images/image4.png", "/opt/images/compressed/image4-image-sharp.png", 20);

        // Приклад використання Magick.NET
        CompressImageWithMagickNET("Magick.NET", "/opt/images/image1.jpg", "/opt/images/compressed/image1-magicknet.jpg", 20);
        CompressImageWithMagickNET("Magick.NET", "/opt/images/image2.jpeg", "/opt/images/compressed/image2-magicknet.jpeg", 20);
        CompressImageWithMagickNET("Magick.NET", "/opt/images/image3.jpg", "/opt/images/compressed/image3-magicknet.jpg", 20);
        CompressImageWithMagickNET("Magick.NET", "/opt/images/image4.png", "/opt/images/compressed/image4-magicknet.png", 20);

        // Приклад використання Aspose.Imaging
        CompressImageWithAspose("Aspose.Imaging", "/opt/images/image1.jpg", "/opt/images/compressed/image1-aspose.jpg", 20);
        CompressImageWithAspose("Aspose.Imaging", "/opt/images/image2.jpeg", "/opt/images/compressed/image2-aspose.jpeg", 20);
        CompressImageWithAspose("Aspose.Imaging", "/opt/images/image3.jpg", "/opt/images/compressed/image3-aspose.jpg", 20);
        CompressImageWithAspose("Aspose.Imaging", "/opt/images/image4.png", "/opt/images/compressed/image4-aspose.png", 20);

        Console.WriteLine("Image compression completed.");
    }

    static void CompressImageWithImageSharp(string toolName, string inputPath, string outputPath, int quality)
    {
        long originalSize = new FileInfo(inputPath).Length;

        using (var image = SixLabors.ImageSharp.Image.Load(inputPath))
        {
            var encoder = new JpegEncoder { Quality = quality };
            image.Save(outputPath, encoder);
        }

        long compressedSize = new FileInfo(outputPath).Length;
        PrintCompressionResult(toolName, originalSize, compressedSize);
    }

    static void CompressImageWithMagickNET(string toolName, string inputPath, string outputPath, int quality)
    {
        long originalSize = new FileInfo(inputPath).Length;

        using (var image = new MagickImage(inputPath))
        {
            image.Quality = quality;
            image.Format = MagickFormat.Jpeg; // or Png based on your need
            image.Write(outputPath);
        }

        long compressedSize = new FileInfo(outputPath).Length;
        PrintCompressionResult(toolName, originalSize, compressedSize);
    }

    static void CompressImageWithAspose(string toolName, string inputPath, string outputPath, int quality)
    {
        long originalSize = new FileInfo(inputPath).Length;

        using (var image = Aspose.Imaging.Image.Load(inputPath))
        {
            if (inputPath.EndsWith(".png"))
            {
                var options = new PngOptions();
                image.Save(outputPath, options);
            }
            else
            {
                var options = new JpegOptions { Quality = quality };
                image.Save(outputPath, options);
            }
        }

        long compressedSize = new FileInfo(outputPath).Length;
        PrintCompressionResult(toolName, originalSize, compressedSize);
    }

    static void PrintCompressionResult(string toolName, long originalSize, long compressedSize)
    {
        double percentage = ((double)(originalSize - compressedSize) / originalSize) * 100;
        Console.WriteLine($"{toolName}: {originalSize / 1024}KB -> {compressedSize / 1024}KB [{percentage:F2}%]");
    }
}
