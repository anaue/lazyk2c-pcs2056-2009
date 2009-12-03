﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompilerModel.Structures;
using System.IO;

namespace CompilerModel.Lexer
{
    public class Lexer
    {
        static FileStream file;
        StreamReader sr;

        public int line = 1;
        private char peek;
        private Hashtable words = new Hashtable();

        void reserve(Word t)
        {
            words.Add(t.Lexeme, t);
        }
        public Lexer(string file)
        {

            sr = new StreamReader(new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read));

            //reserve(new Word((int)Tag.TRUE, "true", line));
            //reserve(new Word((int)Tag.FALSE, "false", line));
            //reserve(new Word((int)Tag.PROGRAM, "program", line));
            //reserve(new Word((int)Tag.END, "end", line));
            //reserve(new Word((int)Tag.IF, "if", line));
            //reserve(new Word((int)Tag.AND, "AND", line));
            //reserve(new Word((int)Tag.OR, "OR", line));
            //reserve(new Word((int)Tag.FUNCTION, "function", line));
            //reserve(new Word((int)Tag.BEGIN, "begin", line));
            //reserve(new Word((int)Tag.RETURN, "return", line));
            //reserve(new Word((int)Tag.ENDFUNCTION, "endfunction", line));
            //reserve(new Word((int)Tag.SUB, "sub", line));
            //reserve(new Word((int)Tag.ENDSUB, "endsub", line));
            //reserve(new Word((int)Tag.INT, "int", line));
            //reserve(new Word((int)Tag.FLOAT, "float", line));
            //reserve(new Word((int)Tag.BOOL, "bool", line));
            //reserve(new Word((int)Tag.STRING, "string", line));
            //reserve(new Word((int)Tag.STRUCT, "struct", line));
            //reserve(new Word((int)Tag.ENDSTRUCT, "endestruct", line));
            //reserve(new Word((int)Tag.THEN, "then", line));
            //reserve(new Word((int)Tag.ELSE, "else", line));
            //reserve(new Word((int)Tag.ENDIF, "endif", line));
            //reserve(new Word((int)Tag.WHILE, "while", line));
            //reserve(new Word((int)Tag.LOOP, "loop", line));
            //reserve(new Word((int)Tag.ENDLOOP, "endloop", line));
            //reserve(new Word((int)Tag.INPUT, "input", line));
            //reserve(new Word((int)Tag.OUTPUT, "output", line));
            //reserve(new Word((int)Tag.CALL, "call", line));
        }
        public Token scan()
        {
            peek = (char)sr.Read();

            if (Character.isWhiteSpace(peek) || Character.isTabSpace(peek) || Character.isLineFeed(peek)) return scan();
            if (peek == 47) // char / comentario
            {
                peek = (char)sr.Read();

                if (peek == 47) // char 
                {
                    while (!Character.isCarriegeReturn(peek))
                    {
                        peek = (char)sr.Read();
                    }
                }
                else
                {
                    return new Token(47, line);
                }
            }
            if (Character.isCarriegeReturn(peek))
            {
                line = line + 1;
                return scan();
            }
            else
            {
                Token t = new Token(peek, line);
                return t;
            }

        }
        public bool hasEnded()
        {
            if (!sr.EndOfStream)
                return false;
            else
            {
                sr.Close();
                //file.Close();
                return true;
            }
        }
    }
}
