namespace Service.Catalog.Domain.ValueObjects;

public class ImageInfo
{
    public string Url { get; private set; }
    public string AltText { get; private set; }

    public ImageInfo(string url, string altText)
    {
        Url = url;
        AltText = altText;
    }
}