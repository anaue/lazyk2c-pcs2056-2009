using System;
using System.Text;

namespace CompilerModel.Lexer
{
    public class Token
    {
        public int tag;
        public int Line;

        public Token(Char ttag)
        {
            int itag = (int)ttag;
            Array values = Enum.GetValues(typeof(Tag));
            bool tagFound = false;
            foreach (int item in values)
            {
                if (item == ttag)
                {
                    tag = ttag;
                    tagFound = true;
                    break;
                }
            }
            if(!tagFound) throw new ApplicationException("Token " + ttag + " not found.");
        }

        public Token(int t, int line)
        {
            Array values = Enum.GetValues(typeof(Tag));
            bool tagFound = false;
            foreach (int item in values)
            {
                if (item == t)
                {
                    tag = t;
                    Line = line;
                    tagFound = true;
                }
            }
            if (!tagFound) throw new ApplicationException("Token " + t + " not found.");
        }

        public override string ToString()
        {
            return tag.ToString();
        }

        public override bool Equals(object obj)
        {
            return ((Token)obj).tag == tag;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}
