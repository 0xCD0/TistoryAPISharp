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

            api.SetAccessToken("cc7718ddca2f6e196f044eee7f6b57e2_ad89a4a9db58649ae87f7801a05748b3");

            // 블로그 정보 얻기
            //string result = api.GetBlogInformation(TistoryAPISharp.TistoryAPISharp.OutputStyle.JSON);

            // 글 목록
            //string reuslt = api.GetPostList(TistoryAPISharp.TistoryAPISharp.OutputStyle.JSON, "mos6502", 1);

            // 글 읽기
            //string result = api.GetPostContent(TistoryAPISharp.TistoryAPISharp.OutputStyle.JSON, "mos6502", "2");

            // 글 쓰기
            //string result = api.WritePost(TistoryAPISharp.TistoryAPISharp.OutputStyle.JSON, "mos6502", "테스트 타이틀","테스트 컨텐츠", "테스트 태그, 테스트 태그 2");

            // 글 수정
            //string result = api.ModifyPost(TistoryAPISharp.TistoryAPISharp.OutputStyle.JSON, "mos6502", "49" ,"테스트 타이틀3","테스트 컨텐츠2", "테스트 태그1, 테스트 태그 3");

            // 파일 첨부
            string result = api.AttatchFile("mos6502", "C:\\Users\\64bitm\\Desktop\\nhkNews\\logo.jpg");

            if (string.IsNullOrEmpty(result)) {
                Console.WriteLine("Request Fail");
            }
            else {
                Console.WriteLine(result);
            }

            Console.ReadLine();
        }
    }
}
