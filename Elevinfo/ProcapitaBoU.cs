using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Elevinfo
{
    public class ProcapitaBoU
    {
        static string DSNSetting = "MetaDirectory";
        public DataTable getElevData(long pnr)
        {
            DataTable result = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings[DSNSetting].ConnectionString;
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = @"SELECT fornamn, efternamn, enhet, enhetid, skolform, skolformid, avdelning, avdelningid, klass, klassid, klassarskurs
                                    FROM ProcapitaBoU 
                                    WHERE personnr=@pnr
                                    AND roll='Student'
                                    AND primar=1";


                cmd.Parameters.AddWithValue("@pnr", pnr.ToString());
                result.Load(cmd.ExecuteReader());

                conn.Close();
            }

            return result;
        }

        public DataTable getElevGruppInfo(long pnr)
        {
            DataTable result = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings[DSNSetting].ConnectionString;
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = @"SELECT grupp, gruppid, grupperiod
                                    FROM ProcapitaBoU 
                                    WHERE personnr=@pnr
                                    AND roll='Student'
                                    AND grupp IS NOT NULL";


                cmd.Parameters.AddWithValue("@pnr", pnr.ToString());
                result.Load(cmd.ExecuteReader());

                conn.Close();
            }

            return result;
        }

        public DataTable getElevMentor(long pnr)
        {
            DataTable result = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings[DSNSetting].ConnectionString;
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = @"SELECT m.fornamn, m.efternamn, ad.mail
                                    FROM ProcapitaBoU s
                                    INNER JOIN ProcapitaBoU m ON s.grupp=m.grupp AND m.roll='Instructor'
                                    INNER JOIN ActiveDirectory ad ON m.personnr=ad.employeeNumber
                                    WHERE s.personnr=@pnr";
                cmd.Parameters.AddWithValue("@pnr", pnr.ToString());
                result.Load(cmd.ExecuteReader());

                conn.Close();
            }
            return result;
        }
    }
}