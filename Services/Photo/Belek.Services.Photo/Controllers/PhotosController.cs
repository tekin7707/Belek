using Belek.Services.Photo.Dtos;
using Belek.Shared.ControllerBases;
using Belek.Shared.Dtos;
using Belek.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Belek.Services.Photo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {

        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {//cancellationToken =>eğer client tarafında işlem iptal edilirse burada da yarıda kesmek için

            var err = "";

            var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";
            var ms = new MemoryStream();
            await photo.CopyToAsync(ms);

            using var thumbMs = await BelekTools.MakeThumbnailAsync(ms);

            err = await BelekTools.saveBlobAsync("tekin001", $"thumbs/{randomFileName}", thumbMs);

            if (string.IsNullOrEmpty(err))
                BelekTools.saveBlobAsync("tekin001", randomFileName, ms);

            if (string.IsNullOrEmpty(err))
                return CreateActionResultInstance(Response<string>.Success(randomFileName, 200));

            return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty", 400));

            /*
            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using var stream = new FileStream(path,FileMode.Create);
                await photo.CopyToAsync(stream,cancellationToken);

                var returnPath = photo.FileName;

                PhotoDto photoDto = new() { Url = returnPath };
                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }

            return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty",400));
            */
        }

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("Photo not found", 400));
            }
            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(204));

        }
    }
}
