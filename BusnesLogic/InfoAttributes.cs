using System;

namespace BusnesLogic
{
    public class InfoAttributes : Attribute
    {
        public  NameSignatyre Name { get; set; }
    }

    public enum NameSignatyre
    {
        SqlRepository
    }
}
