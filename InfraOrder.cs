//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace infragistics
{
    using System;
    using System.Collections.Generic;
    
    public partial class InfraOrder
    {
        public int Order_ID { get; set; }
        public Nullable<int> Customer_ID { get; set; }
        public Nullable<System.DateTime> Update_Date { get; set; }
    
        public virtual InfraOrder InfraOrders1 { get; set; }
        public virtual InfraOrder InfraOrder1 { get; set; }
    }
}
