#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TistoryAPISharpTest {
    class Program {
        static void Main(string[] args) {
            TistoryAPISharp.TistoryAPISharp api = new TistoryAPISharp.TistoryAPISharp();

            api.SetAccessToken("48c17c657fd9d6e8bab6e99e3e101cc3_bedbd619074209a19963092e0d4ad485");

            // 블로그 정보 얻기
            //string result = api.GetBlogInformation(TistoryAPISharp.TistoryAPISharp.OutputStyle.JSON);

            // 글 목록
            string reuslt = api.GetPostList(TistoryAPISharp.TistoryAPISharp.OutputStyle.JSON, "mos6502", 1);

            Console.WriteLine(reuslt);

        }
    }
}
