using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SkiaSharp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting image compression...");

        // Приклад використання ImageSharp
        CompressImageWithImageSharp("/opt/images/image1.jpg", "/opt/images/compressed/image1-image-sharp.jpg", 50);

        // Приклад використання SkiaSharp
        CompressImageWithSkiaSharp("/opt/images/image2.jpeg", "/opt/images/compressed/image2-skiasharp.jpeg", 50);

        Console.WriteLine("Image compression completed.");
    }

    static void CompressImageWithImageSharp(string inputPath, string outputPath, int quality)
    {
        using (Image image = Image.Load(inputPath))
        {
            var encoder = new JpegEncoder { Quality = quality };
            image.Save(outputPath, encoder);
        }
    }

    static void CompressImageWithSkiaSharp(string inputPath, string outputPath, int quality)
    {
        using (var input = File.OpenRead(inputPath))
        using (var output = File.OpenWrite(outputPath))
        {
            var bitmap = SKBitmap.Decode(input);
            var image = SKImage.FromBitmap(bitmap);
            var data = image.Encode(SKEncodedImageFormat.Jpeg, quality);
            data.SaveTo(output);
        }
    }
}
