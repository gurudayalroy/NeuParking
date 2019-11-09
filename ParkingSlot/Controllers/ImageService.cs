using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;


namespace ParkingSlot.Controllers
{
    public class ImageService
    {
        public static async Task<string> MakeOCRRequest(HttpPostedFileBase imageToUpload)
        {
            try
            {
                string imageFilePath = imageToUpload.FileName;
                Stream stream = imageToUpload.InputStream;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add(
                    "Ocp-Apim-Subscription-Key", "e0b2b71bc1ec404a8833583d0a75b3e3");
                string requestParameters = "language=unk&detectOrientation=true";
                string uriBase = "https://southcentralus.api.cognitive.microsoft.com/vision/v1.0/ocr";
                string uri = uriBase + "?" + requestParameters;
                HttpResponseMessage response;
                byte[] bytedata = GetImageAsByteArray(imageToUpload.InputStream);
                using (ByteArrayContent content = new ByteArrayContent(bytedata))
                {
                    content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");
                    response = await client.PostAsync(uri, content);
                }
                string contentString = await response.Content.ReadAsStringAsync();
                JObject obj = JObject.Parse(contentString);
                JToken sec = obj["regions"];
                string numberPlate = string.Empty;
                foreach (JToken token in sec)
                {
                    JToken jToken = token["lines"].First["words"];
                    foreach (JToken item in jToken)
                    {
                        numberPlate += item["text"].ToString();
                    }
                }
                Console.WriteLine(numberPlate);
                Console.WriteLine("\nResponse:\n\n{0}\n",
                    JToken.Parse(contentString).ToString());
                return numberPlate;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
            return "";
        }

        private static byte[] GetImageAsByteArray(Stream data)
        {
            byte[] bytes = null;
            try
            {
                BinaryReader binaryReader = new BinaryReader(data);
                bytes = binaryReader.ReadBytes((int)data.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return bytes;
        }

    }
}