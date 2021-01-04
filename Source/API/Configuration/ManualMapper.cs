using System;
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

        public ProductDto ManualMapperPicturesReverse(Product product, ProductDto productDto)
        {
            if (product.PictureBytes != null)
            {
                var stream = new MemoryStream(product.PictureBytes);
                var fileBytes = stream.ToArray();
                var base64 = Convert.ToBase64String(fileBytes);
                productDto.Picturesrc = string.Format("data:image/jpg;base64,{0}", base64);
            }
            return productDto;
        }
    }
}
