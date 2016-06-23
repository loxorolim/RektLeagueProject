using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RektLeague.App_Start
{
    public static class Config
    {
        private const int pageSize = 5;
        private const int fetchSize = 10;
        public static readonly string[] categoryNames = { "Birdadas", "Memes", "Top Jogadas" };
        public static readonly string[] elementNames = { "Text", "Image", "YoutubeURL" };

        public enum ElementType
        {
            Text,
            Image,
            YoutubeURL,
        }
        public enum ExhibitType
        {
            All,
            Category,
            Author,
        }
        //public enum categories { Birdadas, Memes, TopJogadas};


        public static int PageSize
        {
            get
            {
                return pageSize;
            }
        }
        public static int FetchSize
        {
            get
            {
                return fetchSize;
            }
        }
        public static byte[] getFormFileBytes(HttpPostedFileBase file)
        {
            byte[] data = null;
            if(file.InputStream != null)
            {
                using (Stream inputStream = file.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();
                }
            }
            return data;
        }
        public static string getByteArrayBase64String(byte[] imgByteArray)
        {
            if(imgByteArray!=null || imgByteArray.Length != 0)
            {
                var base64 = Convert.ToBase64String(imgByteArray);
                return String.Format("data:image/gif;base64,{0}", base64);
            }
            return String.Empty;
        }

    }
}