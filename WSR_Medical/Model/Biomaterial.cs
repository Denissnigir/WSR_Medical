//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WSR_Medical.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Biomaterial
    {
        public int Id { get; set; }
        public int PateintId { get; set; }
    
        public virtual Patient Patient { get; set; }
        public virtual Order Order { get; set; }
    }
}
