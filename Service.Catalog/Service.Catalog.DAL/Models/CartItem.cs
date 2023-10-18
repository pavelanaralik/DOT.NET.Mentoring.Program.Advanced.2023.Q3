using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Catalog.DAL.Models;

public class CartItem
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)] // For example, max length of 100 characters
    public string Name { get; set; }

    public ImageInfo Image { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price should be a positive value.")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity should be a positive integer.")]
    public int Quantity { get; set; }
}