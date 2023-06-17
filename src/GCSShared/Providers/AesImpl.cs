using System.Security.Cryptography;

namespace GSendShared.Providers.Internal.Enc
{
    public static class AesImpl
    {
        public static string Encrypt(string plainText, byte[] key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                byte[] iv = aesAlg.IV;

                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(key, iv))
                using (MemoryStream msEncrypt = new())
                {
                    msEncrypt.Write(iv, 0, iv.Length);
                    using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    byte[] encryptedBytes = msEncrypt.ToArray();
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        public static string Decrypt(string encryptedText, byte[] key)
        {
            byte[] cipherText = Convert.FromBase64String(encryptedText);

            using (Aes aesAlg = Aes.Create())
            {
                byte[] iv = new byte[aesAlg.IV.Length];
                byte[] cipherBytes = new byte[cipherText.Length - iv.Length];

                Array.Copy(cipherText, iv, iv.Length);
                Array.Copy(cipherText, iv.Length, cipherBytes, 0, cipherBytes.Length);

                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(key, iv))
                using (MemoryStream msDecrypt = new(cipherBytes))
                using (CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
