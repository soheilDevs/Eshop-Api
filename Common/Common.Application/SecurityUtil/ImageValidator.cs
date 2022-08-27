using System.Drawing;
using Microsoft.AspNetCore.Http;

using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace Common.Application.SecurityUtil;

public static class ImageValidator
{
    public static bool IsImage(this IFormFile file)
    {
        try
        {
            var img = Image.FromStream(file.OpenReadStream());
            return true;
        }
        catch
        {
            return false;
        }
    }
}