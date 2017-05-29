using Model.Registers;
using Persistence.DAL.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Registers
{
    public class SupplierService
    {
        private SupplierDAL supplierDAL = new SupplierDAL();

        public IQueryable<Supplier> GetOrderedByName()
        {
            return supplierDAL.GetOrderedByName();
        }

    public Supplier ById(long id)
    {
        return supplierDAL.ById(id);
    }

    public void Save(Supplier supplier)
    {
        supplierDAL.Save(supplier);
    }

    public Supplier Delete(long id)
    {
        return supplierDAL.Delete(id);
    }

    
}
}