using CerealAPI.Enums;
using CerealAPI.Models;
using CerealAPI.Repositories;
using System;

namespace CerealAPI.Services
{
    
    public class ImageService(IImageRepository repository) : IImageService
    {
        private const string PATH = "./Images/";

        public async Task<(byte[]? image, ImageType? imageType)>
            GetImageByCerealId(int cerealId)
        {
            var imageEntry = repository.GetImageEntryByCerealId(cerealId);

            if (imageEntry != null)
            {
                ImageType? imageType;
                string fileEnding = imageEntry.Path.Split(".").Last();
                if (fileEnding == "jpg" || fileEnding == "jpeg")
                {
                    imageType = ImageType.Jpeg;
                }
                else if (fileEnding == "png")
                {
                    imageType = ImageType.Png;
                }
                else
                {
                    throw new SystemException(
                        "Image filetype from file retrieved from database " +
                        $"was not recognized: .{fileEnding}.");
                }

                byte[] image = await File.ReadAllBytesAsync(imageEntry.Path);

                return (image, imageType);
            }
            else
            {
                return (null, null);
            }
        }

        public async Task<ImageEntry?> PostImage(
            int cerealId, ImageType imageType, byte[] image)
        {
            string path = PATH + cerealId;
            if (imageType == ImageType.Jpeg)
            {
                path += ".jpg";
            }
            else if (imageType == ImageType.Png)
            {
                path += ".png";
            } else
            {
                throw new SystemException(
                        "Image filetype was not recognized by ImageService: " +
                        $"{imageType}.");
            }

            await File.WriteAllBytesAsync(path, image);

            var imageEntry = new ImageEntry(
                Id: 0,
                CerealId: cerealId,
                Path: path);

            var success = await repository.PostImageEntry(imageEntry);

            return success ? imageEntry : null;
        }
    }
}
