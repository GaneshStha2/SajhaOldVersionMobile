using System;
using System.Collections.Generic;
using System.Text;

namespace Riddha.CourierMS.DependencyServices
{
    public interface ICameraHelper
    {
        void LaunchCamera(FileFormatEnum imageType, string imageId = null);
        void LaunchGallery(FileFormatEnum imageType, string imageId = null);
    }

    public enum FileFormatEnum
    {
        PNG,
        JPEG
    }

}
