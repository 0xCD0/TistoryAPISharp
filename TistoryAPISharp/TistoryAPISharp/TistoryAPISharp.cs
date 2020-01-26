#region Using
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
#endregion

namespace TistoryAPISharp {
    public class TistoryAPISharp {

        #region Argument Class
        private class Argument {
            public string key { get; set; }
            public string value { get; set; }
        }
        #endregion

        #region Enums
        public enum OutputStyle {
            XML = 0,
            JSON
        }


        #endregion

        #region LocalVariable
        private string ClientID;
        private string AccessToken;
        #endregion

        #region Get,Set Method
        private string GetClientID() {
            return this.ClientID;
        }

        public void SetClientID(string ClientID) {
            this.ClientID = ClientID;
        }

        private string GetAccessToken() {
            return this.AccessToken;
        }
        public void SetAccessToken(string AccessToken) {
            this.AccessToken = AccessToken;
        }
        #endregion

        #region Functions
        // 블로그 정보
        public string GetBlogInformation(OutputStyle outputStyle) {
            try {
                string output = string.Empty;
                if (outputStyle == OutputStyle.JSON) {
                    output = "&output=json";
                }
                string url = $"https://www.tistory.com/apis/blog/info?access_token={AccessToken}{output}";
                return Get(url);
            }
            catch (Exception ex) {
                switch (ex.HResult) {
                    case -2146233079:
                    throw new Exception("Access token has expired or remote connection error.");
                }
            }
            return string.Empty;
        }

        // 글 목록
        public string GetPostList(OutputStyle outputStyle, string blogName, int pageNumber) {
            try {
                string output = string.Empty;
                if (outputStyle == OutputStyle.JSON) {
                    output = "&output=json";
                }

                string url = $"https://www.tistory.com/apis/post/list?access_token={AccessToken}{output}&blogName={blogName}&page={pageNumber.ToString()}";
                return Get(url);
            }
            catch (Exception ex) {
                switch (ex.HResult) {
                    case -2146233079:
                    throw new Exception("Access token has expired or remote connection error.");
                }
            }
            return string.Empty;
        }



        #endregion


        #region Get,Post Method
        private string Post(List<Argument> args, string Address) {
            try {
                using (var client = new WebClient()) {
                    var values = new NameValueCollection();

                    foreach (var arg in args) {
                        values[arg.key] = arg.value;
                    }

                    var response = client.UploadValues(Address, values);

                    return Encoding.Default.GetString(response);
                }
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        private string Get(string address) {
            try {
                using (var client = new WebClient()) {
                    return client.DownloadString(address);
                }
            }
            catch (Exception ex) {
                throw ex;
            }

        }
        #endregion


    }
}
