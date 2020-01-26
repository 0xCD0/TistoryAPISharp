#region Using
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TistoryAPISharp {
    public class TistoryAPISharp {
        #region Enums
        public enum OutputStyle {
            XML = 0,
            JSON
        }

        // 발행 상태 (0: 비공개, 1: 보호, 3: 발행)
        public enum Visibillity {
            Private = 0,
            Protection,
            Public
        }

        public enum acceptComment {
            Allow = 1,
            Deny = 0
        }
        #endregion

        #region LocalVariable
        private string str_AccessTokenNotSet = "Access token not set. set access token first.";
        private string str_ConnectionError = "Access token has expired or remote connection error.";
        private string str_ModifyError = "Please check if the access torque is expired or out of connection, and the correct PostID.";

        private string ClientID = string.Empty;
        private string AccessToken = string.Empty;
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
                if (string.IsNullOrEmpty(AccessToken)) {
                    throw new Exception(str_AccessTokenNotSet);
                }

                string url = $"https://www.tistory.com/apis/blog/info?access_token={AccessToken}{GetOutputStyle(outputStyle)}";
                return Get(url);
            }
            catch (Exception ex) {
                switch (ex.HResult) {
                    case -2146233079:
                    throw new Exception(str_ConnectionError);
                }
            }
            return string.Empty;
        }

        // 글 목록
        public string GetPostList(OutputStyle outputStyle, string blogName, int pageNumber) {
            try {
                if (string.IsNullOrEmpty(AccessToken)) {
                    throw new Exception(str_AccessTokenNotSet);
                }

                string url = $"https://www.tistory.com/apis/post/list?access_token={AccessToken}{GetOutputStyle(outputStyle)}&blogName={blogName}&page={pageNumber.ToString()}";
                return Get(url);
            }
            catch (Exception ex) {
                switch (ex.HResult) {
                    case -2146233079:
                    throw new Exception(str_ConnectionError);
                }
            }
            return string.Empty;
        }

        // 글 읽기
        public string GetPostContent(OutputStyle outputStyle, string blogName, string postID) {
            try {
                if (string.IsNullOrEmpty(AccessToken)) {
                    throw new Exception(str_AccessTokenNotSet);
                }

                string url = $"https://www.tistory.com/apis/post/read?access_token={AccessToken}{GetOutputStyle(outputStyle)}&blogName={blogName}&postId={postID}";
                return Get(url);
            }
            catch (Exception ex) {
                switch (ex.HResult) {
                    case -2146233079:
                    throw new Exception(str_ConnectionError);
                }
            }
            return string.Empty;
        }

        // 글 쓰기
        public string WritePost(OutputStyle outputStyle, string blogName, string title, string content, string tag = "", Visibillity visibillity = Visibillity.Private,
            acceptComment acceptComment = acceptComment.Deny, string category = "0", string password = "",
            string published = "", string slogan = "") {
            try {
                if (string.IsNullOrEmpty(AccessToken)) {
                    throw new Exception(str_AccessTokenNotSet);
                }

                Dictionary<string, string> args = new Dictionary<string, string>();
                args.Add("access_token", AccessToken);
                args.Add("output", outputStyle == OutputStyle.JSON ? "json" : "");
                args.Add("blogName", blogName);
                args.Add("title", title);
                args.Add("content", content);
                args.Add("visibility", visibillity.ToString("D"));
                args.Add("acceptComment", acceptComment.ToString("D"));
                args.Add("category", category);
                if (!string.IsNullOrEmpty(tag)) args.Add("tag", tag);
                if (!string.IsNullOrEmpty(password)) args.Add("password", password);
                if (!string.IsNullOrEmpty(published)) args.Add("published", published);
                if (!string.IsNullOrEmpty(slogan)) args.Add("slogan", slogan);

                return Post("https://www.tistory.com/apis/post/write", args);
            }
            catch (Exception ex) {
                switch (ex.HResult) {
                    case -2146233079:
                    throw new Exception(str_ConnectionError);
                }
            }

            return string.Empty;
        }

        // 글 수정
        public string ModifyPost(OutputStyle outputStyle, string blogName, string PostID, string title, string content, string tag = "", Visibillity visibillity = Visibillity.Private,
            acceptComment acceptComment = acceptComment.Deny, string category = "0", string password = "",
            string published = "", string slogan = "") {
            try {
                if (string.IsNullOrEmpty(AccessToken)) {
                    throw new Exception(str_AccessTokenNotSet);
                }
                
                Dictionary<string, string> args = new Dictionary<string, string>();
                args.Add("access_token", AccessToken);
                args.Add("output", outputStyle == OutputStyle.JSON ? "json" : "");
                args.Add("blogName", blogName);
                args.Add("postId", PostID);
                args.Add("title", title);
                args.Add("content", content);
                args.Add("visibility", visibillity.ToString("D"));
                args.Add("acceptComment", acceptComment.ToString("D"));
                args.Add("category", category);
                if (!string.IsNullOrEmpty(tag)) args.Add("tag", tag);
                if (!string.IsNullOrEmpty(password)) args.Add("password", password);
                if (!string.IsNullOrEmpty(published)) args.Add("published", published);
                if (!string.IsNullOrEmpty(slogan)) args.Add("slogan", slogan);

                return Post("https://www.tistory.com/apis/post/modify", args);
            }
            catch (Exception ex) {
                switch (ex.HResult) {
                    case -2146233079:
                    throw new Exception(str_ModifyError);
                }
            }

            return string.Empty;
        }

        public string AttatchFile(string blogName, string filePath) {
            try {
                Dictionary<string, string> args = new Dictionary<string, string>();
                args.Add("access_token", AccessToken);
                args.Add("blogName", blogName);

                FileStream file = new FileStream(filePath, FileMode.Open);
                return PostFileAsync("https://www.tistory.com/apis/post/attach", args, file).Result;
            }
            catch(Exception ex) {
                switch (ex.HResult) {
                    case -2146233079:
                    throw new Exception(str_ConnectionError);
                }
                throw ex;
            }
        }

        #endregion


        #region Get,Post Method
        private string Post(string address, Dictionary<string, string> args) {
            try {
                using (var client = new WebClient()) {
                    var values = new NameValueCollection();

                    foreach (var arg in args) {
                        values[arg.Key] = arg.Value;
                    }

                    var response = client.UploadValues(address, values);

                    return Encoding.Default.GetString(response);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private async Task<string> PostFileAsync(string address, Dictionary<string, string> args, FileStream file) {
            try {
                HttpClient httpClient = new HttpClient();
                MultipartFormDataContent form = new MultipartFormDataContent();

                foreach (var arg in args) {
                    form.Add(new StringContent(arg.Key), arg.Value);
                }

                form.Add(new StreamContent(file), "uploadedfile");
                HttpResponseMessage response = await httpClient.PostAsync(address, form);

                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                return response.Content.ReadAsStringAsync().Result;
            }
            catch(Exception ex) {
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

        #region Utility Functions
        private string GetOutputStyle(OutputStyle outputStyle) {
            string output = string.Empty;
            if (outputStyle == OutputStyle.JSON) {
                output = "&output=json";
            }
            return output;
        }
        #endregion

    }
}
