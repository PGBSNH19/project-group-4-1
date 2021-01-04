﻿using System;
using System.IO;
using API.Dtos;
using API.Models;

namespace API.Configuration
{
    public class ManualMapper
    {
        public Product ManualMapperPictures(Product product, ProductDto productDto)
        {
            product.PictureBytes = Convert.FromBase64String(productDto.Image);
            return product;
        }

        public ProductDto ManualMapperPicturesReverse(Product product, ProductDto productDto)
        {
            if (product.PictureBytes != null)
            {
                var stream = new MemoryStream(product.PictureBytes);
                var fileBytes = stream.ToArray();
                var base64 = Convert.ToBase64String(fileBytes);
                productDto.Image = string.Format("data:image/jpg;base64,{0}", base64);
            }
            return productDto;
        }


        public Marketplace ManualMapperMarketplacePictures(Marketplace marketplace, MarketplaceDto marketplaceDto)
        {
            using var memoryStream = new MemoryStream();
            marketplaceDto.Picture.CopyToAsync(memoryStream);
            marketplace.PictureBytes = memoryStream.ToArray();
            return marketplace;
        }

        public MarketplaceDto ManualMapperMarketplacePicturesReverse(Marketplace marketplace, MarketplaceDto marketplaceDto)
        {
            if (marketplace.PictureBytes != null)
            {
                var stream = new MemoryStream(marketplace.PictureBytes);
                var fileBytes = stream.ToArray();
                var base64 = Convert.ToBase64String(fileBytes);
                marketplaceDto.Picturesrc = string.Format("data:image/jpg;base64,{0}", base64);
            }
            return marketplaceDto;
        }
    }
}
