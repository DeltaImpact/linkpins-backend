using System.Security.Cryptography;
using System.Text;

namespace BackSide2.BL
{
    public static class Hash
    {
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static string GetPassHash(this string password)
        {
            using (var md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, password);
            }
        }

    }


}
