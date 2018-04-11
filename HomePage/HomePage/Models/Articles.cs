using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace HomePage.Models
{
    public class Articles
    {
        private const string conn = @"Data Source =.\SQLEXPRESS; uid =uid; pwd =pwd; Initial Catalog =HomePage;";
        string connectionString = conn;

        SqlConnection scon = null;
        SqlCommand scom = null;
        SqlDataReader sdr = null;
        SqlDataAdapter sda = null;

        //게시판 목록
        public List<ArticlesVO> ArticleList(int pN)
        {
            List<ArticlesVO> list = new List<ArticlesVO>();


            try
            {
                scon = new SqlConnection(connectionString);
                sda = new SqlDataAdapter();
                scom = new SqlCommand("dbo.ArticleArticleList", scon);
                scom.Parameters.Add(new SqlParameter("@pageNum", pN));
                scom.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = scom;

                scon.Open();

                DataSet ds = new DataSet();
                sda.Fill(ds);


                list = (from r in ds.Tables[0].AsEnumerable()
                        select new ArticlesVO
                        {
                            ArticleIDX = r.Field<int>("ArticleIDX"),
                            Title = r.Field<string>("Title"),
                            ViewCount = r.Field<int>("ViewCount"),
                            RegistDate = r.Field<DateTime>("RegistDate"),
                            RegistMemberID = r.Field<string>("RegistMemberID"),
                            No = r.Field<Int64>("RNUM"),
                            Total = r.Field<object>("TOTAL_CNT")
                        }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (scon != null)
                {
                    scon.Close();
                }
            }

            return list;
        }
        //게시판 목록
        public List<ArticlesVO> SearchList(int pN,string KeyWord)
        {
            List<ArticlesVO> list = new List<ArticlesVO>();


            try
            {
                scon = new SqlConnection(connectionString);
                sda = new SqlDataAdapter();
                scom = new SqlCommand("dbo.ArticleSearchList", scon);
                scom.Parameters.Add(new SqlParameter("@pageNum", pN));
                scom.Parameters.Add(new SqlParameter("@KeyWord", "%" + KeyWord + "%"));
                scom.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = scom;


                scon.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);

              

                list = (from r in ds.Tables[0].AsEnumerable()
                        select new ArticlesVO
                        {
                            ArticleIDX = r.Field<int>("ArticleIDX"),
                            Title = r.Field<string>("Title"),
                            ViewCount = r.Field<int>("ViewCount"),
                            RegistDate = r.Field<DateTime>("RegistDate"),
                            RegistMemberID = r.Field<string>("RegistMemberID"),
                            No = r.Field<Int64>("RNUM"),
                            Total = r.Field<object>("TOTAL_CNT")
                        }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (scon != null)
                {
                    scon.Close();
                }
            }

            return list;
        }

        //게시판 글 작성
        public Boolean Create(string Title, string Contents,string id)
        {
            Boolean result = false;
            string RegistDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                scon = new SqlConnection(connectionString);
                scom = new SqlCommand();
                scom.Connection = scon;
                scom.CommandText = "dbo.ArticleCreate";
                scom.CommandType = CommandType.StoredProcedure;

                //파라미터 추가
                scom.Parameters.AddWithValue("@Title",Title);
                scom.Parameters.AddWithValue("@Contents", Contents);
                scom.Parameters.AddWithValue("@RegistMemberID", id);
                scom.Parameters.AddWithValue("@RegistDate", RegistDate);


                scon.Open();

                scom.ExecuteNonQuery();


                result = true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (scon != null)
                {
                    scon.Close();
                }
            }


            return result;
        }

        //게시판 상세보기
        public ArticlesVO Detail(string No)
        {
            ArticlesVO vo = new ArticlesVO();

            try
            {
                scon = new SqlConnection(connectionString);
                sda = new SqlDataAdapter();
                scom = new SqlCommand("dbo.ArticleDetail", scon);
                scom.Parameters.Add(new SqlParameter("@No", No));
                scom.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = scom;


                scon.Open();
                
                DataSet ds = new DataSet();
                sda.Fill(ds);

                vo.ArticleIDX = (int)ds.Tables[0].Rows[0][0];
                vo.Contents = ds.Tables[0].Rows[0][1].ToString();
                vo.Title = ds.Tables[0].Rows[0][2].ToString();
                vo.ViewCount = (int)ds.Tables[0].Rows[0][3];
                vo.RegistDate = (DateTime)ds.Tables[0].Rows[0][4];
                vo.RegistMemberID = ds.Tables[0].Rows[0][5].ToString();
                vo.No = (Int64)ds.Tables[0].Rows[0][6];

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (scon != null)
                {
                    scon.Close();
                }
            }

            return vo;
        }

        public ArticlesVO SearchDetail(string KeyWord,string No)
        {
            ArticlesVO vo = new ArticlesVO();

            try
            {
                scon = new SqlConnection(connectionString);
                sda = new SqlDataAdapter();
                scom = new SqlCommand("dbo.ArticleSearchDetail", scon);
                scom.Parameters.Add(new SqlParameter("@No", No));
                scom.Parameters.Add(new SqlParameter("@KeyWord", "%" + KeyWord + "%"));
                scom.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = scom;

                scon.Open();
                
                DataSet ds = new DataSet();
                sda.Fill(ds);

                vo.ArticleIDX = (int)ds.Tables[0].Rows[0][0];
                vo.Contents = ds.Tables[0].Rows[0][1].ToString();
                vo.Title = ds.Tables[0].Rows[0][2].ToString();
                vo.ViewCount = (int)ds.Tables[0].Rows[0][3];
                vo.RegistDate = (DateTime)ds.Tables[0].Rows[0][4];
                vo.RegistMemberID = ds.Tables[0].Rows[0][5].ToString();
                vo.No = (Int64)ds.Tables[0].Rows[0][6];

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (scon != null)
                {
                    scon.Close();
                }
            }

            return vo;
        }

        //게시판 수정
        public Boolean Update(string ArticleIDX, string ID, string Title, string Contents)
        {
            Boolean result = false;
            string ModifyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                scon = new SqlConnection(connectionString);
                scom = new SqlCommand();
                scom.Connection = scon;
                scom.CommandText = "dbo.ArticleUpdate";
                scom.CommandType = CommandType.StoredProcedure;
                //파라미터 추가
                //scom.Parameters.AddWithValue("@groupid",1);
                scom.Parameters.AddWithValue("@Title", Title);
                scom.Parameters.AddWithValue("@Contents", Contents);
                scom.Parameters.AddWithValue("@ArticleIDX", ArticleIDX);
                scom.Parameters.AddWithValue("@RegistMemberID", ID);
                scom.Parameters.AddWithValue("@ModifyDate", ModifyDate);



                scon.Open();

                scom.ExecuteNonQuery();


                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (scon != null)
                {
                    scon.Close();
                }
            }

            return result;
        }

        //게시판 수정
        public Boolean Delete(string ArticleIDX)
        {
            Boolean result = false;

            try
            {
                scon = new SqlConnection(connectionString);
                scom = new SqlCommand();
                scom.Connection = scon;
                scom.CommandText = "dbo.ArticleDelete";
                scom.CommandType = CommandType.StoredProcedure;

                //파라미터 추가
                //scom.Parameters.AddWithValue("@groupid",1);
                scom.Parameters.AddWithValue("@ArticleIDX", ArticleIDX);

                scon.Open();

                scom.ExecuteNonQuery();


                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (scon != null)
                {
                    scon.Close();
                }
            }

            return result;
        }
        //ViewCount
        public Boolean ViewCount(string ArticleIDX, int ViewCount)
        {
            Boolean result = false;

            try
            {
                scon = new SqlConnection(connectionString);
                scom = new SqlCommand();
                scom.Connection = scon;
                scom.CommandText = "dbo.ArticleViewCount";
                scom.CommandType = CommandType.StoredProcedure;
 
;
                //파라미터 추가
                //scom.Parameters.AddWithValue("@groupid",1);
                scom.Parameters.AddWithValue("@ViewCount", ViewCount);
                scom.Parameters.AddWithValue("@ArticleIDX", ArticleIDX);


                scon.Open();

                scom.ExecuteNonQuery();


                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (scon != null)
                {
                    scon.Close();
                }
            }

            return result;
        }

        //ViewCount
        public Boolean SearchCheck(string KeyWord)
        {
            Boolean result = false;

            try
            {
                scon = new SqlConnection(connectionString);
                sda = new SqlDataAdapter();
                scom = new SqlCommand("dbo.ArticleSearchCheck", scon);
                scom.Parameters.Add(new SqlParameter("@KeyWord", "%" + KeyWord + "%"));
                scom.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = scom;

                scon.Open();

                DataSet ds = new DataSet();
                sda.Fill(ds);
                
                string s = ds.Tables[0].Rows[0][0].ToString();
                Boolean check = s.Equals("0");
                
                if (check)
                {
                    
                    result = true;//검색어가 없는경우
                }
                else
                {
                    result = false;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (scon != null)
                {
                    scon.Close();
                }
            }

            return result;
        }

    }
}
