using System;
using System.Text;

using System.IO;

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.Threading.Tasks;
using Image = System.Drawing.Image;
using System.Drawing.Imaging;

namespace Belek.Shared.Services
{
    public class BelekTools
    {
        #region AWS S3
        private static AmazonS3Client AwsClient
        {
            get
            {
                var c = new AmazonS3Config();

                c.RegionEndpoint = RegionEndpoint.EUNorth1;
                c.AllowAutoRedirect = true;
                c.ForcePathStyle = true;
                c.Timeout = TimeSpan.FromMinutes(30);

                return new AmazonS3Client("mock", "mock", c);
            }
        }

        public static async Task<string> saveBlobAsync(string containerName, string blobName, Stream stream)
        {
            string err = "";
            stream.Seek(0, SeekOrigin.Begin);
            var rq = new PutObjectRequest();
            rq.BucketName = containerName;
            rq.Key = blobName;
            rq.InputStream = stream;

            var client = AwsClient;

            try
            {
                var rs = await client.PutObjectAsync(rq);

                if (rs.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    err = rs.HttpStatusCode.ToString();
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            stream.Dispose();
            return err;
        }

        public static async Task<string> saveBlobAsync(string containerName, string blobName, byte[] bytes)
        {
            string err = "";

            var rq = new PutObjectRequest();
            rq.BucketName = containerName;
            rq.Key = blobName;
            rq.InputStream = new MemoryStream(bytes);
            rq.CannedACL = S3CannedACL.PublicRead;

            var client = AwsClient;

            try
            {
                var rs = await client.PutObjectAsync(rq);

                if (rs.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    err = rs.HttpStatusCode.ToString();
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

            return err;
        }

        public static async Task<string> saveBlobAsync(string containerName, string blobName, string content)
        {
            return await saveBlobAsync(containerName, blobName, Encoding.Default.GetBytes(content ?? ""));
        }

        public static async Task<MemoryStream> readBlobAsync(string containerName, string blobName)
        {
            System.IO.MemoryStream ms;

            var rq = new GetObjectRequest();
            rq.BucketName = containerName;
            rq.Key = blobName;

            ms = null;

            var client = AwsClient;

            try
            {
                var rs = await client.GetObjectAsync(rq);

                if (rs.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    return null;
                else
                {
                    var i = 1;
                    var buffer = new byte[4096];
                    ms = new System.IO.MemoryStream();

                    while (i > 0)
                    {
                        i = rs.ResponseStream.Read(buffer, 0, buffer.Length);

                        if (i > 0)
                            ms.Write(buffer, 0, i);
                    }

                    ms.Seek(0, SeekOrigin.Begin);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return ms;
        }

        public static async Task<string> readBlobAsStringAsync(string containerName, string blobName)
        {
            string content = "";

            System.IO.MemoryStream ms = await readBlobAsync(containerName, blobName);

            try
            {
                byte[] bytes = new byte[ms.Length];

                ms.Read(bytes, 0, (int)bytes.Length);

                content = Encoding.Default.GetString(bytes);

                ms.Close();
            }
            catch (Exception ex)
            {
            }

            return content;
        }

        public static async Task<string> deleteBlobAsync(string containerName, string blobName)
        {
            string err = "";

            var rq = new DeleteObjectRequest();
            rq.BucketName = containerName;
            rq.Key = blobName;

            var client = AwsClient;

            try
            {
                var rs = await client.DeleteObjectAsync(rq);

                if (rs.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    err = rs.HttpStatusCode.ToString();
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

            return err;
        }

        public static async Task<bool> hasBlobAsync(string containerName, string blobName)
        {
            var rq = new GetObjectMetadataRequest();
            rq.BucketName = containerName;
            rq.Key = blobName;

            var client = AwsClient;

            try
            {
                var rs = await client.GetObjectMetadataAsync(rq);

                if (rs.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return true;
            }
            catch
            {
            }

            return false;
        }
        #endregion

        public static async Task<MemoryStream> MakeThumbnailAsync(MemoryStream ms, int weight = 400, int height = 400)
        {
            Image image = Image.FromStream(ms);
            Image thumb = image.GetThumbnailImage(weight, height, () => false, IntPtr.Zero);
            var thumbMs = new MemoryStream();
            thumb.Save(thumbMs, ImageFormat.Png);
            return thumbMs;
        }

    }
}
