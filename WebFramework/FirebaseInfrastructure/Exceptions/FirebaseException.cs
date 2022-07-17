namespace WebFramework.FirebaseInfrastructure.Exceptions
{
    using System;

    using FirebaseInfrastructure.Models;

    public class FirebaseException : Exception
    { 
        public int Code { get; set; }

        public FirebaseException(FirebaseContentError contentError) : base(contentError.error.message)
        {
            Code = contentError.error.code;
        }
    }
}
