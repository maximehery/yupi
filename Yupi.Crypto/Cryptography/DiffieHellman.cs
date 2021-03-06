﻿namespace Yupi.Crypto.Cryptography
{
    using System;
    using System.Numerics;
    using System.Security.Cryptography;

    using Yupi.Crypto.Utils;

    public class DiffieHellman
    {
        #region Fields

        private int _bitLength;
        private BigInteger _g;
        private BigInteger _p;
        private BigInteger _privateKey;
        private BigInteger _publicKey;

        #endregion Fields

        #region Properties

        public int BitLength
        {
            get
            {
                return this._bitLength;
            }
        }

        public BigInteger G
        {
            get
            {
                return this._g;
            }
        }

        public BigInteger P
        {
            get
            {
                return this._p;
            }
        }

        public BigInteger PublicKey
        {
            get
            {
                return this._publicKey;
            }
        }

        #endregion Properties

        #region Constructors

        public DiffieHellman(int bitLength, BigInteger p, BigInteger g)
        {
            this._bitLength = bitLength;

            this._p = p;
            this._g = g;

            this.GenerateKeys();
        }

        #endregion Constructors

        #region Methods

        public static DiffieHellman CreateInstance(int bitLength)
        {
            RandomNumberGenerator random = Randomizer.GetRandom();
            BigInteger p = BigIntegerPrime.GeneratePseudoPrime(bitLength, 10, random);
            BigInteger g = BigIntegerPrime.GeneratePseudoPrime(bitLength, 10, random);

            if (g > p)
            {
                BigInteger temp = p;
                p = g;
                g = temp;
            }

            return new DiffieHellman(bitLength, p, g);
        }

        public static DiffieHellman CreateInstance(DiffieHellman dh)
        {
            return new DiffieHellman(dh._bitLength, dh._p, dh._g);
        }

        public BigInteger CalculateSharedKey(BigInteger publicKey)
        {
            return BigInteger.ModPow(publicKey, this._privateKey, this._p);
        }

        private void GenerateKeys()
        {
            this._publicKey = 0;
            this._privateKey = 0;

            Random rand = new Random();
            while (this._publicKey == 0)
            {
                byte[] bytes = new byte[(this._bitLength + 7) / 8];
                rand.NextBytes(bytes);
                bytes[bytes.Length - 1] = 0;
                BigInteger privateKey = new BigInteger(bytes);

                this._publicKey = BigInteger.ModPow(this._g, this._privateKey, this._p);
            }
        }

        #endregion Methods
    }
}