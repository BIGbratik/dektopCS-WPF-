//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dektopCS
{
    using System;
    using System.Collections.Generic;
    
    public partial class MPS
    {
        public int ID { get; set; }
        public Nullable<int> ObjectID { get; set; }
        public string MPSType { get; set; }
        public string MPSfile { get; set; }
    
        public virtual CSobject CSobject { get; set; }
    }
}