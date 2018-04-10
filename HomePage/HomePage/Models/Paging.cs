using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HomePage.Models
{
    public class Paging
    {

        public String renderPaging(int maxNum_i, int currPageNoIn_i, int rowsPerPage_i, int bottomCount_i,
       string url_i, string scriptName_i)
        {
            int maxNum = 0; // 총 갯수
            int currPageNo = 1; // 현재 페이지 번호 : page_num
            int rowPerPage = 10; // 한페이지에 보여질 행수 : page_size
            int bottomCount = 10; // 바닥에 보여질 페이지 수: 10
            maxNum = maxNum_i;
            currPageNo = currPageNoIn_i;
            rowPerPage = rowsPerPage_i;
            bottomCount = bottomCount_i;

            string url = url_i; // 호출 URL
            string scriptName = scriptName_i; // 호출 자바스크립트

            int maxPageNo = ((maxNum - 1) / rowPerPage) + 1;
            int startPageNo = ((currPageNo - 1) / bottomCount) * bottomCount + 1;//
            int endPageNo = ((currPageNo - 1) / bottomCount + 1) * bottomCount;
            int nowBlockNo = ((currPageNo - 1) / bottomCount) + 1;
            int maxBlockNo = ((maxNum - 1) / bottomCount) + 1;

            int inx = 0;
            StringBuilder html = new StringBuilder();
            if (currPageNo > maxPageNo)
            {
                return "";
            }

            html.Append("<table border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">   \n");
            html.Append("<tr>                       \n");
            html.Append("<td class=\"list_num\">                                                                    \n");
            html.Append("<ul class=\"pagination pagination-sm\">                                                  \n");
            // <<
            if (nowBlockNo > 1 && nowBlockNo <= maxBlockNo)
            {
                html.Append("<li><a href=\"javascript:" + scriptName + "( '" + url + "', 1 " +  ");\">  \n");
                html.Append("&laquo;   \n");
                html.Append("</a></li>      \n");
            }

            // <
            if (startPageNo > bottomCount)
            {
                html.Append("<li><a href=\"javascript:" + scriptName + "( '" + url + "'," + (startPageNo - 1) + "," + ");\"> \n");
                html.Append("<        \n");
                html.Append("</a></li>     \n");
            }



            // 1 2 3 ... 10 (숫자보여주기)
            for (inx = startPageNo; inx <= maxPageNo && inx <= endPageNo; inx++)
            {

                if (inx == currPageNo)
                {
                    html.Append("<li class='active'><a href='#'>" + inx + "</a></li>");
                }
                else
                {
                    html.Append("<li><a href=\"javascript:" + scriptName + "('" + url + "'," + inx + "," + ");\" class=\"num_text\">" + inx + "</a></li> \n");
                }
            }

            // >
            if (maxPageNo >= inx)
            {
                html.Append("<li><a href=\"javascript:" + scriptName + "('" + url + "'," + ((nowBlockNo * bottomCount) + 1) + ");\"> \n");
                html.Append(">                       \n");
                html.Append("</a></li>              \n");
            }

            // >>
            if (maxPageNo >= inx)
            {
                html.Append("<li><a href=\"javascript:" + scriptName + "('" + url + "'," + maxPageNo + ");\">      \n");
                html.Append("&raquo;     \n");
                html.Append("</a></li>    \n");
            }
            html.Append("</ul>  \n");
            html.Append("</td>   \n");
            html.Append("</tr>   \n");
            html.Append("</table>   \n");

            return html.ToString();
        }

        //null값일경우 값 변환
        public String nvl(String str, String defValue)
        {
            String retStr = "";
            if (null == str || str.Equals(""))
            {
                retStr = defValue;
            }
            else
            {
                retStr = str.Trim();
            }

            return retStr;
        }

    }
}