using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.IO; 
/// <summary>
/// Summary description for crypto
/// </summary>
public class crypto
{
	public crypto()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private Byte[] KEY_64 = { 1, 2, 3, 4, 5, 6, 7, 8 };
    private Byte[] IV_64 = { 8, 7, 6, 5, 4, 3, 2, 1 };
    string ss;
    // returns DES encrypted string
    public string Encrypt(string value)
    {
        DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Write);
        StreamWriter sw = new StreamWriter(cs);

        sw.Write(value);
        sw.Flush();
        cs.FlushFinalBlock();
        ms.Flush();

        // convert back to a string
        return Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
    }

    // returns DES decrypted string
    public string Decrypt(string value)
    {
        DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        Byte[] buffer = Convert.FromBase64String(value);
        MemoryStream ms = new MemoryStream(buffer);
        CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_64, IV_64), CryptoStreamMode.Read);
        StreamReader sr = new StreamReader(cs);

        return sr.ReadToEnd();
    }

    public static string Decrypt(string cipherText, string Password)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
            new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
            0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
        byte[] decryptedData = Decrypt(cipherBytes,
            pdb.GetBytes(32), pdb.GetBytes(16));
        return System.Text.Encoding.Unicode.GetString(decryptedData);
    }

    public string EncodeString(int s)
    {

        byte[] b = System.Text.Encoding.Default.GetBytes(s.ToString());

        return Convert.ToBase64String(b, 0, b.Length);

    }

    public string DecodeString(string s)
    {
        try
        {
            byte[] b = Convert.FromBase64String(s);

            return System.Text.Encoding.Default.GetString(b);
        }
        catch (Exception ex)
        {
            string ss = ex.Message;
        }
        return "";
    }

    public static byte[] Decrypt(byte[] cipherData,
                                byte[] Key, byte[] IV)
    {

        MemoryStream ms = new MemoryStream();

        Rijndael alg = Rijndael.Create();
        alg.Key = Key;
        alg.IV = IV;
        CryptoStream cs = new CryptoStream(ms,
            alg.CreateDecryptor(), CryptoStreamMode.Write);

        // Write the data and make it do the decryption 
        cs.Write(cipherData, 0, cipherData.Length);
        cs.Close();
        byte[] decryptedData = ms.ToArray();

        return decryptedData;
    }

}
