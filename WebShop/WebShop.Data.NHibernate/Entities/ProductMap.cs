using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Data.NHibernate.Entities
{
    public class ProductMap : ClassMapping<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id, mapper =>
            {
                mapper.Column("Id");
                mapper.Type(NHibernateUtil.Int32);
                mapper.Generator(Generators.Increment);
                mapper.UnsavedValue(0);
            });
            Property(x => x.Name, mapper =>
            {
                mapper.Column("Name");
                mapper.Type(NHibernateUtil.StringClob);
            });

            Property(x => x.Description, mapper =>
            {
                mapper.Column("Description");
                mapper.Type(NHibernateUtil.StringClob);
            });

            Property(x => x.Price, mapper =>
            {
                mapper.Column("Price");
                mapper.Type(NHibernateUtil.Decimal);
            });

            Property(x => x.AmountInStock, mapper =>
            {
                mapper.Column("AmountInStock");
                mapper.Type(NHibernateUtil.Int32);
            });

            //Schema("public");
            Table("Products");
        }
    }
}
