using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CompilerModel.Semantic
{
    class ExecutionEnvironment
    {
        private StreamReader _reader;
        private String _pathName;
        private String _definitions;

        public ExecutionEnvironment(string path)
        {
            _pathName = path;
        }

        public ExecutionEnvironment()
        {
            _pathName = "ExecutionEnvironment//environment.c";
        }
        public void GetExecutionEnvFromFile()
        {
            string directoryName = Path.GetDirectoryName(_pathName);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            _reader = new StreamReader(_pathName, Encoding.Default,false);
            _definitions = _reader.ReadToEnd();
            _reader.Close();
        }
        public String GetDefinitions()
        {
            return _definitions;
        }
    }
}
