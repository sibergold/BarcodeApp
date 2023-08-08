using System;
using System.IO;
using ZXing;
using System.Drawing;

class Program
{
    static void Main()
    {
       
        Console.Write("Barkod verisini girin: ");
        string barcodeData = Console.ReadLine();

        
        string filename = "barcode.png";
        GenerateBarcode(barcodeData, filename);
        Console.WriteLine("Barkod oluşturuldu ve {0} dosyasına kaydedildi.", filename);

     
        string readData = ReadBarcode(filename);
        Console.WriteLine("Okunan barkod verisi: {0}", readData);
        Console.ReadLine();
    }

    static void GenerateBarcode(string barcodeData, string filename)
    {
        
        BarcodeWriter barcodeWriter = new BarcodeWriter
        {
            Format = BarcodeFormat.CODE_128 
        };

       
        var barcodeBitmap = barcodeWriter.Write(barcodeData);

       
        using (var stream = new FileStream(filename, FileMode.Create))
        {
            barcodeBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        }
    }

    static string ReadBarcode(string filename)
    {
        
        BarcodeReader barcodeReader = new BarcodeReader();

        
        using (var stream = new FileStream(filename, FileMode.Open))
        {
            var barcodeBitmap = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(stream);
            var result = barcodeReader.Decode(barcodeBitmap);
            return result?.Text;
        }
    }
}