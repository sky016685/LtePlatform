﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using Lte.Domain.Lz4Net.Core;

namespace ZipLib.Zip
{
    public class WindowsNameTransform : INameTransform
    {
        private string _baseDirectory;
        private char _replacementChar;
        private bool _trimIncomingPaths;
        private static readonly char[] InvalidEntryChars;
        private const int MaxPath = 260;

        static WindowsNameTransform()
        {
            char[] invalidPathChars = Path.GetInvalidPathChars();
            int num = invalidPathChars.Length + 3;
            InvalidEntryChars = new char[num];
            Array.Copy(invalidPathChars, 0, InvalidEntryChars, 0, invalidPathChars.Length);
            InvalidEntryChars[num - 1] = '*';
            InvalidEntryChars[num - 2] = '?';
            InvalidEntryChars[num - 3] = ':';
        }

        public WindowsNameTransform()
        {
            _replacementChar = '_';
        }

        public WindowsNameTransform(string baseDirectory)
        {
            _replacementChar = '_';
            if (baseDirectory == null)
            {
                throw new ArgumentNullException("baseDirectory", "Directory name is invalid");
            }
            BaseDirectory = baseDirectory;
        }

        public static bool IsValidName(string name)
        {
            return (((name != null) && (name.Length <= MaxPath)) 
                && (String.CompareOrdinal(name, MakeValidName(name, '_')) == 0));
        }

        public static string MakeValidName(string name, char replacement)
        {
            int num;
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            name = WindowsPathUtils.DropPathRoot(name.Replace("/", @"\"));
            while ((name.Length > 0) && (name[0] == '\\'))
            {
                name = name.Remove(0, 1);
            }
            while ((name.Length > 0) && (name[name.Length - 1] == '\\'))
            {
                name = name.Remove(name.Length - 1, 1);
            }
            for (num = name.IndexOf(@"\\", StringComparison.Ordinal); 
                num >= 0; 
                num = name.IndexOf(@"\\", StringComparison.Ordinal))
            {
                name = name.Remove(num, 1);
            }
            num = name.IndexOfAny(InvalidEntryChars);
            if (num >= 0)
            {
                StringBuilder builder = new StringBuilder(name);
                while (num >= 0)
                {
                    builder[num] = replacement;
                    if (num >= name.Length)
                    {
                        num = -1;
                    }
                    else
                    {
                        num = name.IndexOfAny(InvalidEntryChars, num + 1);
                    }
                }
                name = builder.ToString();
            }
            if (name.Length > MaxPath)
            {
                throw new PathTooLongException();
            }
            return name;
        }

        public string TransformDirectory(string name)
        {
            name = TransformFile(name);
            if (name.Length <= 0)
            {
                throw new ZipException("Cannot have an empty directory name");
            }
            while (name.EndsWith(@"\"))
            {
                name = name.Remove(name.Length - 1, 1);
            }
            return name;
        }

        public string TransformFile(string name)
        {
            if (name != null)
            {
                name = MakeValidName(name, _replacementChar);
                if (_trimIncomingPaths)
                {
                    name = Path.GetFileName(name);
                }
                if (_baseDirectory != null)
                {
                    name = Path.Combine(_baseDirectory, name);
                }
                return name;
            }
            name = string.Empty;
            return name;
        }

        public string BaseDirectory
        {
            get
            {
                return _baseDirectory;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _baseDirectory = Path.GetFullPath(value);
            }
        }

        public char Replacement
        {
            get
            {
                return _replacementChar;
            }
            set
            {
                if (InvalidEntryChars.Any(t => t == value))
                {
                    throw new ArgumentException("invalid path character");
                }
                if ((value == '\\') || (value == '/'))
                {
                    throw new ArgumentException("invalid replacement character");
                }
                _replacementChar = value;
            }
        }

        public bool TrimIncomingPaths
        {
            get
            {
                return _trimIncomingPaths;
            }
            set
            {
                _trimIncomingPaths = value;
            }
        }
    }
}
