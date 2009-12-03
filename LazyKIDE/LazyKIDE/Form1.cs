using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using APE.Parser;
using CompilerModel.APE;
using APE;
using CompilerModel.Lexer;
using System.IO;
using CompilerModel.Symbols;
using CompilerModel.Structures;

namespace LazyKIDE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string inputExpression = textBoxCode.Text;
                StreamWriter sw = File.CreateText("temp.lk");
                sw.Write(inputExpression);
                sw.Close();
                sw.Dispose();


                APEParser parser = new APEParser();

                StackAutomaton ape = parser.GetStackAutomaton();

                Recognizer recognizer = new Recognizer(ape, textBoxOutputName.Text);

                Lexer _lexer = new Lexer("temp.lk");

                //An input is used here to act returning tokens to sintatic as a buffer.
                Input inputChain = new CompilerModel.Symbols.Input();
                while (!_lexer.hasEnded())
                {
                    Token escan = _lexer.scan();
                    inputChain.Add(escan);
                }
                inputChain.resetNext();

                Console.WriteLine("Done!");

                //Recognizer                    
                Console.Write(":: Compiling... ");

                int i = 0; //Just a help to debug this code w/ conditional breakpoints, can be removed later.
                
                while (inputChain.hasNext())
                {
                    Token currentToken, nextToken;

                    currentToken = inputChain.getNext();
                    nextToken = inputChain.getLookAHead();

                    if (!recognizer.RunTransition(currentToken, nextToken))
                    {
                        //ERRO!!
                        Console.WriteLine("\n\n!! Syntax Error: Line " + currentToken.Line + ", Token " + currentToken.tag + " (ASCII Code) not recognized.");
                        Console.WriteLine("!! Current Automaton: " + recognizer.CurrentAutomaton.Name);
                        Console.WriteLine("!! Current State: " + recognizer.CurrentState.Id);
                        Console.WriteLine("!! Stack: " + GetStackAutomatonNames(recognizer.Stack));
                        Console.WriteLine("!! i: " + i);

                        break;
                    }
                    i++;
                }

                if (recognizer.CurrentState.FinalState && recognizer.Stack.Empty)
                {
                    recognizer.Semantic.SaveOutput();
                    Console.WriteLine("Done!");
                    string output = File.ReadAllText(textBoxOutputName.Text);
                    textBoxCOutput.Text = output;
                }
                else
                    Console.WriteLine("!! Syntax errors detected. Recognizer terminated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private string GetStackAutomatonNames(Stack stack)
        {
            StringBuilder sb = new StringBuilder();
            while (!stack.Empty)
            {
                StackPair sp = ((StackPair)stack.Pop());
                sb.Append("(" + sp.Automaton.Name + "," + sp.State.Id + ")");
            }
            return sb.ToString();
        }
    }
}
