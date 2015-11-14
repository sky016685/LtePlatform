﻿using System;
using System.Security.Cryptography;

namespace Lte.Domain.Lz4Net.Encryption
{
    public sealed class PkzipClassicManaged : PkzipClassic
    {
        private byte[] key_;

        public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
        {
            key_ = rgbKey;
            return new PkzipClassicDecryptCryptoTransform(Key);
        }

        public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
        {
            key_ = rgbKey;
            return new PkzipClassicEncryptCryptoTransform(Key);
        }

        public override void GenerateIV()
        {
        }

        public override void GenerateKey()
        {
            key_ = new byte[12];
            new Random().NextBytes(key_);
        }

        public override int BlockSize
        {
            get
            {
                return 8;
            }
            set
            {
                if (value != 8)
                {
                    throw new CryptographicException("Block size is invalid");
                }
            }
        }

        public override byte[] Key
        {
            get
            {
                if (key_ == null)
                {
                    GenerateKey();
                }
                return (byte[])key_.Clone();
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (value.Length != 12)
                {
                    throw new CryptographicException("Key size is illegal");
                }
                key_ = (byte[])value.Clone();
            }
        }

        public override KeySizes[] LegalBlockSizes
        {
            get
            {
                return new[] { new KeySizes(8, 8, 0) };
            }
        }

        public override KeySizes[] LegalKeySizes
        {
            get
            {
                return new[] { new KeySizes(0x60, 0x60, 0) };
            }
        }
    }
}
