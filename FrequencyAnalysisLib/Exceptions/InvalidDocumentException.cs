using System;

namespace FrequencyAnalysisLib
{
    public class InvalidDocumentException : Exception
    {
        public InvalidDocumentException(string message) : base(message)
        {
            
        }
        public InvalidDocumentException() : base("Document is not valid!")
        {
            
        }
    }
}
