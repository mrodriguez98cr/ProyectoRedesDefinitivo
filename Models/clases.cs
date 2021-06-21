using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace ProyectoRedesDefinitivo
{

    public class Link
    {
        public string link1 { get; set; }
        public string link2 { get; set; }
        public string Resultado { get; set; }
        public string Grado { get; set; }

        public void analizador()
        {
            FaceDetectResponse fd1, fd2;
            string urls = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=true&returnFaceLandmarks=true&returnFaceAttributes=age,gender";
            using (var q = new WebClient())
            {
                q.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                q.Headers.Add("Ocp-Apim-Subscription-Key", "32be3d3431444338842c8f29541c9c0b");
                string x = "{\"url\":\"" + link1 + "\"}";
                string resp1 = q.UploadString(urls, x);
                fd1 = JsonConvert.DeserializeObject<FaceDetectResponse[]>(resp1)[0];
            }

            using (var q = new WebClient())
            {
                q.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                q.Headers.Add("Ocp-Apim-Subscription-Key", "32be3d3431444338842c8f29541c9c0b");
                string x = "{\"url\":\"" + link2 + "\"}";
                string resp2 = q.UploadString(urls, x);
                fd2 = JsonConvert.DeserializeObject<FaceDetectResponse[]>(resp2)[0];
            }

            Respuesta(fd1, fd2);
        }

        public void Respuesta(FaceDetectResponse fd1, FaceDetectResponse fd2)
        {

            using (var q = new WebClient())
            {
                q.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                q.Headers.Add("Ocp-Apim-Subscription-Key", "32be3d3431444338842c8f29541c9c0b");
                string url2 = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/verify";
                string json = "{\"faceId1\":\"" + fd1.FaceId + "\",\"faceId2\":\"" + fd2.FaceId + "\"}";
                string str = q.UploadString(url2, json);
                FaceVerifyResponse resp3 = JsonConvert.DeserializeObject<FaceVerifyResponse>(str);


                if (resp3.IsIdentical == true)
                {
                    Resultado = "Son la misma persona";

                }
                else
                {
                    Resultado = "No son la misma persona";
                }

                Grado = "El grado de parecido es: " + resp3.Confidence * 100 + " % ";

            }





        }




        public class FaceDetectResponse
        {
            public string FaceId { get; set; }
            public FaceAttributes FaceAttributes { get; set; }
            public FaceLandmarks FaceLandmarks { get; set; }
            public FaceRectangle FaceRectangle { get; set; }


        }

        public class FaceRectangle
        {
            public int Height { get; set; }
            public int Left { get; set; }
            public int Top { get; set; }
            public int With { get; set; }

        }
        public class FaceLandmarks
        {
            public FeatureCoordinate EyebrowLeftInner { get; set; }
            public FeatureCoordinate EyebrowLeftOuter { get; set; }
            public FeatureCoordinate EyebrowRighInner { get; set; }
            public FeatureCoordinate EyebrowleftOuner { get; set; }
            public FeatureCoordinate EyeLeftBottom { get; set; }
            public FeatureCoordinate EyeLeftInner { get; set; }
            public FeatureCoordinate EyeLeftOuer { get; set; }
            public FeatureCoordinate EyeLeftTop { get; set; }
            public FeatureCoordinate EyeRightBottom { get; set; }
            public FeatureCoordinate EyeRightInner { get; set; }
            public FeatureCoordinate EyeRightOuer { get; set; }
            public FeatureCoordinate EyeRightTop { get; set; }
            public FeatureCoordinate MouthLeft { get; set; }
            public FeatureCoordinate MouthRight { get; set; }
            public FeatureCoordinate NoseLeftAlarOutTip { get; set; }
            public FeatureCoordinate NoseLeftAlarTop { get; set; }
            public FeatureCoordinate NoseRightAlarOutTip { get; set; }
            public FeatureCoordinate NoseRightAlarTop { get; set; }
            public FeatureCoordinate NoseRootLeft { get; set; }
            public FeatureCoordinate NoseRootRight { get; set; }
            public FeatureCoordinate NoseRootTip { get; set; }
            public FeatureCoordinate PupilLeft { get; set; }
            public FeatureCoordinate PupilRight { get; set; }
            public FeatureCoordinate UnderLipBottom { get; set; }
            public FeatureCoordinate UnderLiptop { get; set; }
            public FeatureCoordinate UpperLipBottom { get; set; }
            public FeatureCoordinate UpperLiptop { get; set; }


        }
        public class FeatureCoordinate
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
        public class FaceAttributes
        {
            [JsonConverter(typeof(StringEnumConverter))]
            public Glasses Glasses { get; set; }
            public double Age { get; set; }
            public FacialHair FacialHair { get; set; }
            public string Gender { get; set; }
            public HeadPose HeadPose { get; set; }
            public double Smile { get; set; }


        }
        public enum Glasses
        {
            NoGlasses = 0,
            Sunglasses = 1,
            ReadingGlasses = 2,
            SwimmingGoggles = 3
        }
        public class FacialHair
        {
            public double Beard { get; set; }
            public double Moustache { get; set; }
            public double Sideburns { get; set; }
        }
        public class HeadPose
        {
            public double Pitch { get; set; }
            public double Roll { get; set; }
            public double Yaw { get; set; }
        }

        public class FaceVerifyResponse
        {
            public double Confidence { get; set; }
            public bool IsIdentical { get; set; }
        }

    }
}