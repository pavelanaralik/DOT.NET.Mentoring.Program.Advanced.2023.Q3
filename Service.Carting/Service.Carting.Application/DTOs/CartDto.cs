using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Carting.Application.DTOs;

public class CartDto
{
    public int Id { get; set; }

    public List<CartItemDto> Items { get; set; }
}