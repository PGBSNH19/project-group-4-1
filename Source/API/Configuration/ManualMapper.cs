using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public List<ProductDto> ManualMapperPicturesReverse(ICollection<Product> products, ProductDto[] productDtos)
        {
            var productDtoList = new List<ProductDto>();
            for (int i = 0; i < products.Count; i++)
            {
                var product = products.ElementAt(i);
                var productDto = productDtos.ElementAt(i);
                if (product.PictureBytes != null)
                {
                    var picture = product.PictureBytes;
                    var stream = new MemoryStream(product.PictureBytes);
                    IFormFile file = new FormFile(stream, 0, product.PictureBytes.Length, "name", "fileName");
                    productDto.Picture = file;
                }
                productDtoList.Add(productDto);
            }
            return productDtoList;
        }
    }
}
