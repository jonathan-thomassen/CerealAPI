using CerealAPI.Enums;
using CerealAPI.Models;
using CerealAPI.Repositories;
using System;
using static System.Net.Mime.MediaTypeNames;

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
            int cerealId, IList<IFormFile> fileList)
        {
            string path = PATH + cerealId;

            var file = fileList[0];
            if (file.ContentType == "image/jpeg")
            {
                path += ".jpg";
            }
            else if (file.ContentType == "image/png")
            {
                path += ".png";
            }
            else
            {
                return null;
            }

            using (var stream = File.Create(path))
            {
                await file.CopyToAsync(stream);
            }

            var imageEntry = new ImageEntry(
                Id: 0,
                CerealId: cerealId,
                Path: path);

            var success = await repository.PostImageEntry(imageEntry);

            return success ? imageEntry : null;
        }
    }
}
