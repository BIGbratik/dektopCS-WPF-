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
    
    public partial class CSobject
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string ObjectName { get; set; }
        public string Vedomstvo { get; set; }
        public string Subordination { get; set; }
        public string IsReady { get; set; }
        public string Num { get; set; }
        public string Place { get; set; }
        public string Phone { get; set; }
    
        public virtual Category Category { get; set; }
    }
}
