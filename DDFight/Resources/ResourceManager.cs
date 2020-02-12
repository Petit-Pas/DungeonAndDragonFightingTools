using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace DDFight.Resources
{
    /// <summary>
    ///     static class to get resources
    /// </summary>
    public class ResourceManager
    {
        /// <summary>
        ///     Converts a Bitmap into a BitmapImage
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static BitmapImage ImageFromBuffer(Bitmap source)
        {
            BitmapImage retval;

            using (MemoryStream stream = new MemoryStream())
            {
                source.Save(stream, ImageFormat.Png);
                stream.Position = 0;

                retval = new BitmapImage();
                retval.BeginInit();
                retval.CacheOption = BitmapCacheOption.OnLoad;
                retval.UriSource = null;
                retval.StreamSource = stream;
                retval.EndInit();
            }

            return retval;
        }

        /// <summary>
        ///     Get a bitmapImage from the Checked resource
        /// </summary>
        /// <returns></returns>
        public static BitmapImage BmChecked()
        {
            return ImageFromBuffer(DDFight.Properties.Resources._checked);
        }

        /// <summary>
        ///     Get a bitmapImage from the Unchecked resource
        /// </summary>
        /// <returns></returns>
        public static BitmapImage BmUnchecked()
        {
            return ImageFromBuffer(DDFight.Properties.Resources._unchecked);
        }
    }
    }
