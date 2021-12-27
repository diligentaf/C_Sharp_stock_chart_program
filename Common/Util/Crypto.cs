using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Util {

    /// <summary>
    /// SHA256 암호화
    /// </summary>
    public sealed class Crypto {

        #region "enum HASH_TYPE"
        public enum HASH_TYPE {
            MD5 = 1,
            SHA1 = 2,
            SHA256 = 3,
            SHA384 = 4,
            SHA512 = 5
        };
        #endregion

        public static string ComputeSimpleHash(string str) {
            byte[] saltBytes = Encoding.UTF8.GetBytes("SampleProgram");
            return ComputeHash(str, HASH_TYPE.SHA256, saltBytes).Item1;
        }

        #region "static Tuple<string, string> ComputeHash(string str, HASH_TYPE hashType, byte[] saltBytes)"
        /// <summary>
        /// Hash Function (2018-10-02)
        /// </summary>
        public static Tuple<string, string> ComputeHash(string str, HASH_TYPE hashType, byte[] saltBytes) {
            if (saltBytes == null) {
                saltBytes = new byte[(new Random()).Next(4, 8)];
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider()) {
                    rng.GetNonZeroBytes(saltBytes);
                }
            }
            byte[] saltedStr = Encoding.UTF8.GetBytes(str).Concat<byte>(saltBytes).ToArray();

            HashAlgorithm hash = null;
            switch (hashType) {
                case HASH_TYPE.MD5:
                    hash = new MD5CryptoServiceProvider();
                    break;
                case HASH_TYPE.SHA1:
                    hash = new SHA1Managed();
                    break;
                case HASH_TYPE.SHA256:
                    hash = new SHA256Managed();
                    break;
                case HASH_TYPE.SHA384:
                    hash = new SHA384Managed();
                    break;
                case HASH_TYPE.SHA512:
                    hash = new SHA512Managed();
                    break;
                default:
                    break;
            }
            string hashedValue = Convert.ToBase64String(hash.ComputeHash(saltedStr));
            string saltValue = Convert.ToBase64String(saltBytes);
            return new Tuple<string, string>(hashedValue, saltValue);
        }
        #endregion

        #region "static Tuple<string,string> AESEncrypt(string plainText, string password)"
        /// <summary>
        /// AES Encrypt  (2018-10-02, Copy from http://www.selamigungor.com/post/7/encrypt-decrypt-a-string-in-csharp)
        /// slightly modified
        /// </summary>
        public static Tuple<string, string> AESEncrypt(string plainText, string password) {
            if (plainText == null) {
                return null;
            }

            if (password == null) {
                password = String.Empty;
            }

            //// Get the bytes of the string
            //var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            //var passwordBytes = Encoding.UTF8.GetBytes(password);

            //// Hash the password with SHA256
            //passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            Tuple<string, string> hashValue = Crypto.ComputeHash(password, HASH_TYPE.SHA256, null);
            byte[] passwordBytes = Convert.FromBase64String(hashValue.Item1);
            var bytesEncrypted = Crypto.AESEncrypt(Encoding.UTF8.GetBytes(plainText), passwordBytes);

            return new Tuple<string, string>(Convert.ToBase64String(bytesEncrypted), hashValue.Item2);
        }
        #endregion

        #region "static string AESDecrypt(string encryptedText, string password, string salt)"
        /// <summary>
        /// AES Decrypt (2018-10-02, Copy from http://www.selamigungor.com/post/7/encrypt-decrypt-a-string-in-csharp)
        /// slightly modified
        /// </summary>
        public static string AESDecrypt(string encryptedText, string password, string salt) {
            if (encryptedText == null) {
                return null;
            }

            if (password == null) {
                password = String.Empty;
            }

            byte[] bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            byte[] saltBytes = Convert.FromBase64String(salt);
            Tuple<string, string> hasedValue = Crypto.ComputeHash(password, HASH_TYPE.SHA256, saltBytes);

            byte[] bytesDecrypted = Crypto.AESDecrypt(Convert.FromBase64String(encryptedText), Convert.FromBase64String(hasedValue.Item1));
            return Encoding.UTF8.GetString(bytesDecrypted);
        }
        #endregion

        #region "static byte[] AESEncrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)"
        /// <summary>
        /// AES Decrypt (2018-10-02, Copy from http://www.selamigungor.com/post/7/encrypt-decrypt-a-string-in-csharp)
        /// </summary>
        private static byte[] AESEncrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes) {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream()) {
                using (RijndaelManaged AES = new RijndaelManaged()) {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write)) {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        #endregion

        #region "static byte[] AESDecrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)"
        /// <summary>
        /// AES Decrypt (2018-10-02, Copy from http://www.selamigungor.com/post/7/encrypt-decrypt-a-string-in-csharp)
        /// </summary>
        private static byte[] AESDecrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes) {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream()) {
                using (RijndaelManaged AES = new RijndaelManaged()) {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write)) {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
        #endregion

        #region "static bool ValidatePassword(string password, string confirmpassword)"
        public static bool ValidatePassword(string password, string confirmpassword) {
            const int MIN_LENGTH = 8;
            const int MAX_LENGTH = 20;

            if (!password.Equals(confirmpassword)) throw new Exception("비밀번호가 동일하지 않음");
            if (password == null) throw new ArgumentNullException("Password 입력되지 않음");

            bool meetsLengthRequirements = password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;

            if (meetsLengthRequirements) {
                foreach (char c in password) {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
            }

            bool isValid = meetsLengthRequirements
                        && hasUpperCaseLetter
                        && hasLowerCaseLetter
                        && hasDecimalDigit;
            return isValid;
        }
        #endregion

    }
}
