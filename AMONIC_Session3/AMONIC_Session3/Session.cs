//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMONIC_Session3
{
    using System;
    using System.Collections.Generic;
    
    public partial class Session
    {
        public int Id { get; set; }
        public System.DateTime StartTime { get; set; }
        public Nullable<int> SessionEndId { get; set; }
        public int UserId { get; set; }
        public Nullable<int> ErrorId { get; set; }
    
        public virtual Error Error { get; set; }
        public virtual SessionEnd SessionEnd { get; set; }
        public virtual Users Users { get; set; }
    }
}