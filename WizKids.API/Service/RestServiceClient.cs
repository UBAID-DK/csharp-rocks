using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Serialization;

namespace WizKids.API.Model
{
    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    public enum authenticationType
    {
        Basic,
        NTLM
    }


    public enum autheticationTechnique
    {
        RollYourOwn,
        NetworkCredential
    }
    class RestServiceClient
    {
        #region <Property>
        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }
        public authenticationType authType { get; set; }
        public autheticationTechnique authTech { get; set; }
        public string TokenBearer { get; set; }
        #endregion

        #region Constructor   
        public RestServiceClient()
        {
            endPoint = string.Empty;
            httpMethod = httpVerb.GET;
        }
        #endregion

        #region Dynamic Method For Restfull Service To  Get Data       
        public T makeRequest<T>(bool IsAuthorized = false)
        {
            T strResponseValue = default(T);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            // if there's is credential for authentication then we will pass IsAuthentic true.. during calling but credential should be pass

            if (IsAuthorized)
            {
                request.Method = httpMethod.ToString();
                request.Headers.Add("Authorization", " Bearer " + TokenBearer);
            }
            HttpWebResponse response = null;

            try

            {
                if (typeof(T).IsEquivalentTo(typeof(byte[])))
                {
                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(endPoint);
                    myRequest.Method = "GET";
                    WebResponse myResponse = myRequest.GetResponse();
                    MemoryStream ms = new MemoryStream();

                    myResponse.GetResponseStream().CopyTo(ms);

                    strResponseValue = (T)Convert.ChangeType(ms.ToArray(), typeof(T));

                 }
                else
                {
                    response = (HttpWebResponse)request.GetResponse();
                    //Process the resppnse stream... (could be JSON, XML or HTML etc..._
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                strResponseValue = (T)Convert.ChangeType(reader.ReadToEnd(), typeof(T));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strResponseValue = (T)Convert.ChangeType("{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}", typeof(T));
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }

            return strResponseValue;
        }
        #endregion
    }
}