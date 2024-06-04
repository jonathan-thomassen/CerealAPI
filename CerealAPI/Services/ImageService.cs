using CerealAPI.Enums;
using CerealAPI.Models;
using CerealAPI.Repositories;

namespace CerealAPI.Services
{

    public class ImageService(IImageRepository repository) : IImageService
    {
        private const string PATH = "./Images/";

        public async Task<(byte[]? image, ImageType? imageType)>
            GetImageByCerealId(int cerealId)
        {
            ImageEntry? imageEntry =
                await repository.GetImageEntryByCerealId(cerealId);

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

            IFormFile file = fileList[0];
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

            using (FileStream stream = File.Create(path))
            {
                await file.CopyToAsync(stream);
            }

            var imageEntry = new ImageEntry(
                Id: 0,
                CerealId: cerealId,
                Path: path);

            bool success = await repository.PostImageEntry(imageEntry);

            return success ? imageEntry : null;
        }

        public async Task<(ImageEntry? imageEntry, bool existed)>
            UpdateImage(int cerealId, IList<IFormFile> fileList)
        {
            ImageEntry? oldImageEntry =
                await repository.GetImageEntryByCerealId(cerealId);

            string path = PATH + cerealId;

            IFormFile file = fileList[0];
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
                return (null, oldImageEntry != null);
            }

            var newImageEntry = new ImageEntry(
                Id: oldImageEntry != null ? oldImageEntry.Id : 0,
                CerealId: cerealId,
                Path: path);

            if (oldImageEntry != null)
            {
                File.Delete(oldImageEntry.Path);

                using (FileStream stream = File.Create(path))
                {
                    await file.CopyToAsync(stream);
                }

                bool success = await repository.UpdateImageEntry(
                    oldImageEntry, newImageEntry);
                return success ? (newImageEntry, true) : (null, true);
            }
            else
            {
                bool success = await repository.PostImageEntry(newImageEntry);
                return success ? (newImageEntry, false) : (null, false);
            }
        }

        public async Task<bool?> DeleteImageByCerealId(int cerealId)
        {
            ImageEntry? imageEntry = await repository.GetImageEntryByCerealId(cerealId);

            if (imageEntry != null)
            {
                if (File.Exists(imageEntry.Path))
                {
                    File.Delete(imageEntry.Path);
                }

                bool success = await repository.DeleteImageEntry(imageEntry);
                return success;
            }
            else
            {
                return null;
            }
        }
    }
}
