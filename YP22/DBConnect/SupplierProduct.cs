//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YP22.DBConnect
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupplierProduct
    {
        public int id { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> SupplierId { get; set; }
    
        public virtual SupplierСountry SupplierСountry { get; set; }
        public virtual Product Product { get; set; }
    }
}
