using System.Collections.Generic;
using System.IO;
using API.Dtos;
using API.Models;
using Microsoft.AspNetCore.Http;

namespace API.Configuration
{
    public class ManualMapper
    {
        public Product ManualMapperPictures(Product product, ProductDto productDto)
        {
            using var memoryStream = new MemoryStream();
            productDto.Picture.CopyToAsync(memoryStream);
            product.PictureBytes = memoryStream.ToArray();
            return product;
        }

        public ICollection<ProductDto> ManualMapperPicturesReverse(ICollection<Product> products, ICollection<ProductDto> productDtos)
        {
            foreach (var product in products)
            {
                if (product.PictureBytes != null)
                {
                    var picture = product.PictureBytes;
                    var stream = new MemoryStream(product.PictureBytes);
                    IFormFile file = new FormFile(stream, 0, product.PictureBytes.Length, "name", "fileName");
                    foreach (var productDto in productDtos)
                    {
                        productDto.Picture = file;
                        productDtos.Add(productDto);
                    }
                }
            }
            return productDtos;
        }
    }
}
