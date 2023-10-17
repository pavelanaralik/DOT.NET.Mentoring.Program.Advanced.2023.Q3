using System.ComponentModel.DataAnnotations;

namespace Service.Catalog.DAL.Models;

public class ImageInfo
{
    [Url(ErrorMessage = "Invalid URL format.")]
    public string Url { get; set; }

    [MaxLength(200)] // For example, max length of 200 characters
    public string AltText { get; set; }
}