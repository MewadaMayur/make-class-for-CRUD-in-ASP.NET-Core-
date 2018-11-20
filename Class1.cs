using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Configuration;

namespace AppsTech.App_Code
{
    public class Class1
    {

        //SqlConnection con = new SqlConnection(@"Data Source = 198.38.83.200; Initial Catalog = appstech_acquacrm; User ID = appstech_acquacrm; Password = Acqua@2018");

        SqlConnection con = new SqlConnection(@"Data Source = 192.168.2.20; Initial Catalog = Kingston; User ID = sa; Password=AppsTech@2018;");
        public Class1()
        {
            
        }
        //for insert,update,delete query
        public void savedata(string qry)
        {
            con.Open();
           SqlCommand cmd = new SqlCommand(qry, con);
           cmd.ExecuteNonQuery();
           con.Close();
        }
        //for select query
        public DataSet calldata(string qry)
        {
            SqlDataAdapter sd = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            sd.Fill(ds);
            return ds;
        }
        
        //To Encrypt password
        public string Encrypt(string clearText)
        {
            try
            {
                string EncryptionKey = "MAKV2SPBNI99212";
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return clearText;

            }
            catch (Exception ex)
            {
                return "";
            }
        }


        //To Decrypt password
        public string Decrypt(string cipherText)
        {
            try
            {
                string EncryptionKey = "MAKV2SPBNI99212";
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return cipherText;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public void DeclareVariables()
        {
            SqlConnection con = new SqlConnection(@"Data Source = .;Initial Catalog = Kingston;Integrated Security=True;");
        }

        

    }

    public class Customer
    {
        //employe data
        public int id { get; set; }
        public string OldCaseNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string City { get; set; }
        public string Call_Receive_Date { get; set; }
        public string Category { get; set; }
        public string ProductIssue { get; set; }
        public string Solution { get; set; }
        public string Model { get; set; }
        public string Capacity { get; set; }
        public string Callid { get; set; }
        public string DepositeDate { get; set; }
        public string ActionTaken { get; set; }
        public string Status { get; set; }
        public string WMS_Status { get; set; }
        public string Remarks { get; set; }
        public string NewCaseNumber { get; set; }
        public string Emp_code { get; set; }
    }
}