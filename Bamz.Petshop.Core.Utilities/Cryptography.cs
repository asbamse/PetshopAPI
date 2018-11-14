﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace Bamz.Petshop.Core.Utilities
{
    public class Cryptography
    {
        public static string Encrypt(string str)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(str));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
