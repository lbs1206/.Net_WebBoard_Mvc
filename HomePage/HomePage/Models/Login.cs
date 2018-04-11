using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace HomePage.Models
{
    public class Login
    {
        private const string conn = @"Data Source =.\SQLEXPRESS; uid =uid; pwd =pwd; Initial Catalog =HomePage;";
        string connectionString = conn;

        SqlConnection scon = null;
        SqlCommand scom = null;
        SqlDataReader sdr = null;
        SqlDataAdapter sda = null;

        //로그인
        public Boolean login(string MemberID,string MemberPWD)
        {
            Boolean result = false;
            
            try
            {
                scon = new SqlConnection(connectionString);
                sda = new SqlDataAdapter();
                scom = new SqlCommand("dbo.LoginLogin", scon);
                scom.Parameters.Add(new SqlParameter("@MemberID", MemberID));
                scom.Parameters.Add(new SqlParameter("@MemberPWD", MemberPWD));
                scom.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = scom;

                scon.Open();


                DataSet ds = new DataSet();
                sda.Fill(ds);
                
                //string s = ds.Tables[0].Rows[0][0].ToString();
                if (ds.Tables[0].Rows[0][0].ToString().Equals("1"))
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

        //세션값가져오기
        public MemberVO session(string MemberID)
        {
            MemberVO mv = new MemberVO();
            try
            {
                scon = new SqlConnection(connectionString);
                sda = new SqlDataAdapter();
                scom = new SqlCommand("dbo.LoginSession", scon);
                scom.Parameters.Add(new SqlParameter("@MemberID", MemberID));
                scom.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = scom;

                scon.Open();

                DataSet ds = new DataSet();
                sda.Fill(ds);

                //string s = ds.Tables[0].Rows[0][0].ToString();
                mv.MemberID = ds.Tables[0].Rows[0][0].ToString();
                mv.Code = (Int32)ds.Tables[0].Rows[0][1];
                


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
