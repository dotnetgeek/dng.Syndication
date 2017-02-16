using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dng.Syndication
{
    public class Image
    {
        /// <summary>
        /// <para>Optional</para>
        /// <para>Specifies the text in the HTML title attribute of the link around the image</para>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// <para>Optional</para>
        /// <para>Defines the height of the image. Default is 31. Maximum value is 400</para>
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// <para>Required.</para>
        /// <para>Defines the hyperlink to the website that offers the channel</para>
        /// </summary>
        public Uri Link { get; set; }

        /// <summary>
        /// <para>Required.</para>
        /// <para>Defines the text to display if the image could not be shown</para>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// <para>Required.</para>
        /// <para>Specifies the URL to the image</para>
        /// </summary>
        public Uri Url { get; set; }
        
        /// <summary>
        /// <para>Optional.</para>
        /// <para>Defines the width of the image. Default is 88. Maximum value is 144</para>
        /// </summary>
        public int? Width { get; set; }

        private Image()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">Specifies the URL to the image</param>
        /// <param name="title">Defines the text to display if the image could not be shown</param>
        /// <param name="link">Defines the hyperlink to the website that offers the channel</param>
        public Image(Uri imageUrl)
            : this()
        {
            Url = imageUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">Specifies the URL to the image</param>
        /// <param name="title">Defines the text to display if the image could not be shown</param>
        /// <param name="link">Defines the hyperlink to the website that offers the channel</param>
        public Image(Uri imageUrl, string title, Uri link)
            : this(imageUrl)
        {
            Title = title;
            Link = link;
        }

    }
}
