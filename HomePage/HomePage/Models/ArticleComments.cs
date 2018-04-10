using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace HomePage.Models
{
    public class ArticleComments
    {
        private const string conn = @"Data Source =.\SQLEXPRESS; uid =lbs; pwd =mini1234!; Initial Catalog =HomePage;";
        string connectionString = conn;

        SqlConnection scon = null;
        SqlCommand scom = null;
        SqlDataReader sdr = null;
        SqlDataAdapter sda = null;
        public List<ArticleCommentsVO> CommentList(int ArticleIDX,int pageNum)
        {
            List<ArticleCommentsVO> list = new List<ArticleCommentsVO>();

            try
            {
                scon = new SqlConnection(connectionString);
                sda = new SqlDataAdapter();
                scom = new SqlCommand("dbo.ArticleCommentsCommentList", scon);
                scom.Parameters.Add(new SqlParameter("@pageNum", pageNum));
                scom.Parameters.Add(new SqlParameter("@ArticleIDX", ArticleIDX));
                scom.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = scom;

                scon.Open();

                DataSet ds = new DataSet();
                sda.Fill(ds);
                

                list = (from r in ds.Tables[0].AsEnumerable()
                        select new ArticleCommentsVO
                        {
                            ArticleIDX = r.Field<int>("ArticleIDX"),
                            CommentID = r.Field<int>("CommentID"),
                            MemberID = r.Field<string>("MemberID"),
                            RegistDate = r.Field<DateTime>("RegistDate"),
                            Comments = r.Field<string>("Comments"),
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

        public Boolean CommentCreate(string ArticleIDX, string Comments,string ID)
        {
            Boolean check = false;

            string RegistDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                scon = new SqlConnection(connectionString);
                scom = new SqlCommand();
                scom.Connection = scon;
                scom.CommandText = "dbo.ArticleCommentsCommentCreate";
                scom.CommandType = CommandType.StoredProcedure;

                //파라미터 추가
                scom.Parameters.AddWithValue("@ArticleIDX", ArticleIDX);
                scom.Parameters.AddWithValue("@MemberID", ID);
                scom.Parameters.AddWithValue("@Comments", Comments);
                scom.Parameters.AddWithValue("@RegistDate", RegistDate);



                scon.Open();

                scom.ExecuteNonQuery();


                check = true;

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


            return check;
        }

        public Boolean CommentDelete(int CommentID)
        {
            Boolean result = false;

            try
            {
                scon = new SqlConnection(connectionString);
                scom = new SqlCommand();
                scom.Connection = scon;
                scom.CommandText = "dbo.ArticleCommentsCommentDelete";
                scom.CommandType = CommandType.StoredProcedure;

                //파라미터 추가
                //scom.Parameters.AddWithValue("@groupid",1);
                scom.Parameters.AddWithValue("@CommentID", CommentID);

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

        public ArticleCommentsVO OneSelect(int CommentID)
        {
            ArticleCommentsVO acvo = new ArticleCommentsVO();
            try
            {
                scon = new SqlConnection(connectionString);
                sda = new SqlDataAdapter();
                scom = new SqlCommand("dbo.ArticleCommentsOneSelect", scon);
                scom.Parameters.Add(new SqlParameter("@CommentID", CommentID));
                scom.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = scom;

                scon.Open();

                DataSet ds = new DataSet();
                sda.Fill(ds);

                acvo.CommentID = (int)ds.Tables[0].Rows[0][0];
                acvo.ArticleIDX = (int)ds.Tables[0].Rows[0][1];
                acvo.MemberID = ds.Tables[0].Rows[0][2].ToString();
                acvo.Comments = ds.Tables[0].Rows[0][3].ToString();
                acvo.RegistDate = (DateTime)ds.Tables[0].Rows[0][4];
                

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


            return acvo;
        }

        public Boolean Update(string Comments, int CommentID)
        {
            Boolean result = false;

            try
            {
               
                    scon = new SqlConnection(connectionString);
                    scom = new SqlCommand();
                    scom.Connection = scon;
                    scom.CommandText = "dbo.ArticleCommentsUpdate";
                    scom.CommandType = CommandType.StoredProcedure;
                    //파라미터 추가
                    //scom.Parameters.AddWithValue("@groupid",1);
                    scom.Parameters.AddWithValue("@Comments", Comments);
                    scom.Parameters.AddWithValue("@CommentID", CommentID);



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
    }
}