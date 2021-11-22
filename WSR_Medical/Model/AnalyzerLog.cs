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
    
    public partial class AnalyzerLog
    {
        public int Id { get; set; }
        public int AnalyzerId { get; set; }
        public int EmployeeId { get; set; }
        public int OrderId { get; set; }
        public System.DateTime ServiceDate { get; set; }
        public double Result { get; set; }
        public System.DateTime Finished { get; set; }
        public bool Accepted { get; set; }
        public int StatusId { get; set; }
    
        public virtual Analyzer Analyzer { get; set; }
        public virtual AnalyzerStatus AnalyzerStatus { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Order Order { get; set; }
    }
}
