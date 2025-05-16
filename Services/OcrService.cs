using System.IO;
using System.Threading.Tasks;
using Tesseract;

public class OcrService
{
    public async Task<string> ExtractLicensePlateFromImageAsync(byte[] imageData)
    {
        string resultText = string.Empty;
        string tempFilePath = Path.GetTempFileName();

        try
        {
            await File.WriteAllBytesAsync(tempFilePath, imageData);

            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            using (var img = Pix.LoadFromFile(tempFilePath))
            {
                var page = engine.Process(img);
                resultText = page.GetText().Trim();
            }
        }
        finally
        {
            File.Delete(tempFilePath);
        }

        return resultText;
    }
}
