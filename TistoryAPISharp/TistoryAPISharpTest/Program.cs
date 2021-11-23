#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TistoryAPISharp;
#endregion

namespace TistoryAPISharpTest {
    class Program {
        static void Main(string[] args) {
            TistoryAPI api = new TistoryAPI();

            api.SetClientID("");
            api.GetAccessTokenFromWeb("https://mos6502.tistory.com/");
            //api.SetAccessToken("");

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
            //string result = api.AttatchFile("mos6502", "C:\\Users\\64bitm\\Desktop\\nhkNews\\logo.jpg");

            // 카테고리 얻기
            //string result = api.GetCategory(TistoryAPISharp.TistoryAPISharp.OutputStyle.JSON, "mos6502");

            // 최근 댓글 목록
            //string result = api.GetRecentComment(TistoryAPISharp.TistoryAPISharp.OutputStyle.JSON, "mos6502",1,10);

            // 게시글 댓글 목록
            //string result = api.GetCommentFromPost(TistoryAPISharp.TistoryAPISharp.OutputStyle.JSON, "mos6502","3");

            // 댓글 작성
            //string result = api.ModifyComment(TistoryAPISharp.TistoryAPISharp.OutputStyle.JSON, "mos6502", "3", "12051330", "테스트 댓글로 수정", TistoryAPISharp.TistoryAPISharp.SecretComment.Secret);

            // 댓글 삭제
            //string result = api.DeleteComment(TistoryAPISharp.TistoryAPISharp.OutputStyle.XML, "mos6502", "3", "12051328");
            

            //if (string.IsNullOrEmpty(result)) {
            //    Console.WriteLine("Request Fail");
            //}
            //else {
              //  Console.WriteLine(result);
            //}

            Console.ReadLine();
        }
    }
}
