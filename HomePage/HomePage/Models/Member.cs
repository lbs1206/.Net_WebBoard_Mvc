using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace HomePage.Models
{
    public class Member
    {
        private const string conn = @"Data Source =.\SQLEXPRESS; uid =lbs; pwd =mini1234!; Initial Catalog =HomePage;";
        string connectionString = conn;

        SqlConnection scon = null;
        SqlCommand scom = null;
        SqlDataReader sdr = null;
        SqlDataAdapter sda = null;


        //회원 가입
        public Boolean insert(string MemberID, string MemberPWD, string MemberName, string Email, string Telephone)
        {   
            Boolean result = false;
            int code = 2;
            string EntryDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                scon = new SqlConnection(connectionString);
                scom = new SqlCommand();
                scom.Connection = scon;
                scom.CommandType = CommandType.StoredProcedure;
                scom.CommandText = "dbo.MemberInsert";

                //파라미터 추가
                //scom.Parameters.AddWithValue("@groupid",1);
                scom.Parameters.AddWithValue("@MemberID", MemberID);
                scom.Parameters.AddWithValue("@MemberPWD", MemberPWD);
                scom.Parameters.AddWithValue("@MemberName", MemberName);
                scom.Parameters.AddWithValue("@Email", Email);
                scom.Parameters.AddWithValue("@Telephone", Telephone);
                scom.Parameters.AddWithValue("@EntryDate",EntryDate);
                scom.Parameters.AddWithValue("@Code",code);

                scon.Open();

                sdr = scom.ExecuteReader();
              

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

        //회원 삭제
        public Boolean delete(string MemberID)
        {
            Boolean result = false;

            try
            {
                scon = new SqlConnection(connectionString);
                scom = new SqlCommand();
                scom.Connection = scon;
                scom.CommandType = CommandType.StoredProcedure;
                scom.CommandText = "dbo.MemberDelete";

                //파라미터 추가
                //scom.Parameters.AddWithValue("@groupid",1);
                scom.Parameters.AddWithValue("@MemberID", MemberID);

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

        //회원 수정
        public Boolean update(MemberVO mv)
        {
            Boolean result = false; 

            try
            {
                scon = new SqlConnection(connectionString);
                scom = new SqlCommand();
                scom.Connection = scon;
                scom.CommandType = CommandType.StoredProcedure;
                scom.CommandText = "dbo.MemberUpdate";
                //파라미터 추가
                //scom.Parameters.AddWithValue("@groupid",1);
                scom.Parameters.AddWithValue("@MemberID", mv.MemberID);
                scom.Parameters.AddWithValue("@MemberPWD", mv.MemberPWD);
                scom.Parameters.AddWithValue("@MemberName", mv.MemberName);
                scom.Parameters.AddWithValue("@Email", mv.Email);
                scom.Parameters.AddWithValue("@Telephone", mv.Telephone);

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

        //중복 체크
        public Boolean ID_Check(string MemberID)
        {
            Boolean result = false;

            try
            {   
                scon = new SqlConnection(connectionString);
                sda = new SqlDataAdapter();
                scom = new SqlCommand("dbo.MemberIDCheck", scon);
                scom.Parameters.Add(new SqlParameter("@MemberID", MemberID));
                scom.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = scom;

                scon.Open();

                DataSet ds = new DataSet();
                sda.Fill(ds);

                string s = ds.Tables[0].Rows[0][0].ToString();

                if (ds.Tables[0].Rows[0][0].ToString().Equals ("0"))
                {
                    result = true;
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

        //회원 목록
        public List<MemberVO> MemberList(int pN)
        {
            List<MemberVO> list = new List<MemberVO>();
            

            try
            {
                scon = new SqlConnection(connectionString);
                sda = new SqlDataAdapter();
                scom = new SqlCommand("dbo.MemberMemberList", scon);
                scom.Parameters.Add(new SqlParameter("@pageNum", pN));
                scom.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = scom;

                scon.Open();

                DataSet ds = new DataSet();
                sda.Fill(ds);

                list = (from r in ds.Tables[0].AsEnumerable()
                        select new MemberVO
                        {
                            MemberID = r.Field<string>("MemberID"),
                            MemberPWD = r.Field<string>("MemberPWD"),
                            MemberName = r.Field<string>("MemberName"),
                            Email = r.Field<string>("Email"),
                            Telephone = r.Field<string>("Telephone"),
                            EntryDate = r.Field<DateTime>("EntryDate"),
                            Code = r.Field<int>("Code"),
                            No =r.Field<object>("RNUM"),
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

        //마이페이지 초기데이터
        public MemberVO select(string MemberID)
        {
            MemberVO mv = new MemberVO();

            try
            {
                scon = new SqlConnection(connectionString);
                sda = new SqlDataAdapter();
                scom = new SqlCommand("dbo.MemberSelect", scon);
                scom.Parameters.Add(new SqlParameter("@MemberID", MemberID));
                scom.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = scom;

                scon.Open();
                

                DataSet ds = new DataSet();
                sda.Fill(ds);
                mv.MemberID = ds.Tables[0].Rows[0][0].ToString();
                mv.MemberPWD = ds.Tables[0].Rows[0][1].ToString();
                mv.MemberName = ds.Tables[0].Rows[0][2].ToString();
                mv.Email = ds.Tables[0].Rows[0][3].ToString();
                mv.Telephone = ds.Tables[0].Rows[0][4].ToString();

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


            return mv;
        }
    }
}