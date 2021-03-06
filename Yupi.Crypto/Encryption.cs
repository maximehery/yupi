﻿namespace Yupi.Crypto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using System.Threading.Tasks;

    using Yupi.Crypto.Cryptography;
    using Yupi.Crypto.Utils;

    public class Encryption
    {
        #region Fields

        private static Encryption _instance;

        private DiffieHellman _dh;
        private RSACrypto _rsa;

        #endregion Fields

        #region Constructors

        public Encryption(RSACParameters parameters, int dhBitLength)
        {
            this._rsa = new RSACrypto(parameters);
            this._dh = DiffieHellman.CreateInstance(dhBitLength);
        }

        #endregion Constructors

        #region Methods

        public static Encryption GetInstance()
        {
            if (_instance == null)
            {
                throw new NullReferenceException("HabboEncryption not initialized");
            }

            return _instance;
        }

        public static Encryption GetInstance(RSACParameters parameters, int dhBitLength)
        {
            if (_instance == null)
            {
                _instance = new Encryption(parameters, dhBitLength);
            }

            return GetInstance();
        }

        public static ARC4 InitializeARC4(BigInteger sharedKey)
        {
            byte[] sharedKeyBytes = sharedKey.ToByteArray(false);

            return new ARC4(sharedKeyBytes);
        }

        public BigInteger CalculateDiffieHellmanSharedKey(string publicKey)
        {
            publicKey = this.GetRSADecryptedString(publicKey, false);

            return this._dh.CalculateSharedKey(BigInteger.Parse(publicKey));
        }

        public string GetRSADiffieHellmanGKey()
        {
            string key = this._dh.G.ToString();
            return this.GetRSAEncryptedString(key, true);
        }

        public string GetRSADiffieHellmanPKey()
        {
            string key = this._dh.P.ToString();
            return this.GetRSAEncryptedString(key, true);
        }

        public string GetRSADiffieHellmanPublicKey()
        {
            string key = this._dh.PublicKey.ToString();
            return this.GetRSAEncryptedString(key, true);
        }

        private string GetRSADecryptedString(string data, bool usePrivate)
        {
            byte[] bytes = Converter.HexStringToBytes(data);
            byte[] decryptedBytes = this._rsa.Decrypt(bytes, usePrivate);

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        private string GetRSAEncryptedString(string data, bool usePrivate)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] encryptedBytes = this._rsa.Encrypt(bytes, usePrivate);

            return Converter.BytesToHexString(bytes);
        }

        #endregion Methods
    }
}