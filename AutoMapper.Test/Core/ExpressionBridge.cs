using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using AutoMapper.Should;
using AutoMapper.Test.Core.ConditionalMapping;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Core
{
    namespace ExpressionBridge
    {
        public class SimpleProductDto
        {
            public string Name { set; get; }
            public string ProductSubcategoryName { set; get; }
            public string CategoryName { set; get; }
        }
        public class ExtendedProductDto
        {
            public string Name { set; get; }
            public string ProductSubcategoryName { set; get; }
            public string CategoryName { set; get; }
            public List<BillOfMaterialsDto> BOM { set; get; }
        }
        public class ComplexProductDto
        {
            public string Name { get; set; }
            public ProductSubcategoryDto ProductSubcategory { get; set; }
        }
        public class ProductSubcategoryDto
        {
            public string Name { get; set; }
            public ProductCategoryDto ProductCategory { get; set; }
        }
        public class ProductCategoryDto
        {
            public string Name { get; set; }
        }
        public class AbstractProductDto
        {
            public string Name { set; get; }
            public string ProductSubcategoryName { set; get; }
            public string CategoryName { set; get; }
            public List<ProductTypeDto> Types { get; set; }
        }
        public abstract class ProductTypeDto { }
        public class ProdTypeA : ProductTypeDto {}
        public class ProdTypeB : ProductTypeDto {}

        public class ProductTypeConverter : TypeConverter<ProductType, ProductTypeDto>
        {
            protected override ProductTypeDto ConvertCore(ProductType source)
            {
                if (source.Name == "A")
                    return new ProdTypeA();
                if (source.Name == "B")
                    return new ProdTypeB();
                throw new ArgumentException();
            }
        }


        public class ProductType
        {
            public string Name { get; set; }
        }

        public class BillOfMaterialsDto
        {
            public int BillOfMaterialsID { set; get; }
        }

        public class Product
        {
            public string Name { get; set; }
            public ProductSubcategory ProductSubcategory { get; set; }
            public List<BillOfMaterials> BillOfMaterials { set; get; }
            public List<ProductType> Types { get; set; }
        }

        public class ProductSubcategory
        {
            public string Name { get; set; }
            public ProductCategory ProductCategory { get; set; }
        }

        public class ProductCategory
        {
            public string Name { get; set; }
        }

        public class BillOfMaterials
        {
            public int BillOfMaterialsID { set; get; }
        }

        [TestFixture]
        public class When_mapping_using_expressions : NonValidatingSpecBase
        {
            private List<Product> _products;
            private Expression<Func<Product, SimpleProductDto>> _simpleProductConversionLinq;
            private Expression<Func<Product, ExtendedProductDto>> _extendedProductConversionLinq;
            private Expression<Func<Product, AbstractProductDto>> _abstractProductConversionLinq;
            private List<SimpleProductDto> _simpleProducts;
            private List<ExtendedProductDto> _extendedProducts;

            protected override void Establish_context()
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Product, SimpleProductDto>()
                        .ForMember(m => m.CategoryName, dst => dst.MapFrom(p => p.ProductSubcategory.ProductCategory.Name));
                    cfg.CreateMap<Product, ExtendedProductDto>()
                        .ForMember(m => m.CategoryName, dst => dst.MapFrom(p => p.ProductSubcategory.ProductCategory.Name))
                        .ForMember(m => m.BOM, dst => dst.MapFrom(p => p.BillOfMaterials));
                    cfg.CreateMap<BillOfMaterials, BillOfMaterialsDto>();
                    cfg.CreateMap<Product, ComplexProductDto>();
                    cfg.CreateMap<ProductSubcategory, ProductSubcategoryDto>();
                    cfg.CreateMap<ProductCategory, ProductCategoryDto>();
                    cfg.CreateMap<Product, AbstractProductDto>();
                    cfg.CreateMap<ProductType, ProductTypeDto>()
                        //.ConvertUsing(x => ProductTypeDto.GetProdType(x));
                        .ConvertUsing<ProductTypeConverter>();
                });

                _simpleProductConversionLinq = Mapper.Engine.CreateMapExpression<Product, SimpleProductDto>();
                _extendedProductConversionLinq = Mapper.Engine.CreateMapExpression<Product, ExtendedProductDto>();
                _abstractProductConversionLinq = Mapper.Engine.CreateMapExpression<Product, AbstractProductDto>();

                _products = new List<Product>()
                {
                    new Product
                    {
                        Name = "Foo",
                        ProductSubcategory = new ProductSubcategory
                        {
                            Name = "Bar",
                            ProductCategory = new ProductCategory
                            {
                                Name = "Baz"
                            }
                        },
                        BillOfMaterials = new List<BillOfMaterials>
                        {
                            new BillOfMaterials
                            {
                                BillOfMaterialsID = 5
                            }
                        }
                        ,
                        Types = new List<ProductType>
                                    {
                                        new ProductType() { Name = "A" },
                                        new ProductType() { Name = "B" },
                                        new ProductType() { Name = "A" }
                                    }
                    }
                };
            }

            protected override void Because_of()
            {
                var queryable = _products.AsQueryable();

                _simpleProducts = queryable.Select(_simpleProductConversionLinq).ToList();

                _extendedProducts = queryable.Select(_extendedProductConversionLinq).ToList();

            }

            [Test]
            public void Should_map_and_flatten()
            {


                _simpleProducts.Count.ShouldBe(1);
                _simpleProducts[0].Name.ShouldBe("Foo");
                _simpleProducts[0].ProductSubcategoryName.ShouldBe("Bar");
                _simpleProducts[0].CategoryName.ShouldBe("Baz");

                _extendedProducts.Count.ShouldBe(1);
                _extendedProducts[0].Name.ShouldBe("Foo");
                _extendedProducts[0].ProductSubcategoryName.ShouldBe("Bar");
                _extendedProducts[0].CategoryName.ShouldBe("Baz");
                _extendedProducts[0].BOM.Count.ShouldBe(1);
                _extendedProducts[0].BOM[0].BillOfMaterialsID.ShouldBe(5);
            }
           
            [Test]
            public void Should_use_extension_methods()
            {

                
                var queryable = _products.AsQueryable();

                var simpleProducts = queryable.ProjectTo<SimpleProductDto>().ToList();

                simpleProducts.Count.ShouldBe(1);
                simpleProducts[0].Name.ShouldBe("Foo");
                simpleProducts[0].ProductSubcategoryName.ShouldBe("Bar");
                simpleProducts[0].CategoryName.ShouldBe("Baz");

                var extendedProducts = queryable.ProjectTo<ExtendedProductDto>().ToList();

                extendedProducts.Count.ShouldBe(1);
                extendedProducts[0].Name.ShouldBe("Foo");
                extendedProducts[0].ProductSubcategoryName.ShouldBe("Bar");
                extendedProducts[0].CategoryName.ShouldBe("Baz");
                extendedProducts[0].BOM.Count.ShouldBe(1);
                extendedProducts[0].BOM[0].BillOfMaterialsID.ShouldBe(5);

                var complexProducts = queryable.ProjectTo<ComplexProductDto>().ToList();

                complexProducts.Count.ShouldBe(1);
                complexProducts[0].Name.ShouldBe("Foo");
                complexProducts[0].ProductSubcategory.Name.ShouldBe("Bar");
                complexProducts[0].ProductSubcategory.ProductCategory.Name.ShouldBe("Baz");
            }
#if !SILVERLIGHT
            [Test]
            [ExpectedException(typeof(InvalidOperationException))]
            public void List_of_abstract_should_be_mapped()
            {
                var mapped = Mapper.Map<AbstractProductDto>(_products[0]);
                mapped.Types.Count.ShouldBe(3);

                var queryable = _products.AsQueryable();

                var abstractProducts = queryable.ProjectTo<AbstractProductDto>().ToList();

                abstractProducts[0].Types.Count.ShouldBe(3);
                abstractProducts[0].Types[0].GetType().ShouldBe(typeof (ProdTypeA));
                abstractProducts[0].Types[1].GetType().ShouldBe(typeof (ProdTypeB));
                abstractProducts[0].Types[2].GetType().ShouldBe(typeof (ProdTypeA));
            }
#endif
        }

#if !NETFX_CORE && !SILVERLIGHT
        namespace CircularReferences
        {
            public class A
            {
                public int AP1 { get; set; }
                public string AP2 { get; set; }
                public virtual B B { get; set; }
            }

            public sealed class B
            {
                public B()
                {
                    BP2 = new HashSet<A>();
                }
                public int BP1 { get; set; }
                public ICollection<A> BP2 { get; set; }
            }

            public sealed class AEntity
            {
                public int AP1 { get; set; }
                public string AP2 { get; set; }
                public BEntity B { get; set; }
            }
            public sealed class BEntity
            {
                public BEntity()
                {
                    BP2 = new HashSet<AEntity>();
                }
                public int BP1 { get; set; }
                public ICollection<AEntity> BP2 { get; set; }
            }

            public class C
            {
                public C Value { get; set; }
            }

            [TestFixture]
            public class When_mapping_circular_references : AutoMapperSpecBase
            {
                private IQueryable<BEntity> _bei;

                protected override void Establish_context()
                {
                    Mapper.CreateMap<BEntity, B>().MaxDepth(3);
                    Mapper.CreateMap<AEntity, A>().MaxDepth(3);
                }

                protected override void Because_of()
                {
                    var be = new BEntity {BP1 = 3};
                    be.BP2.Add(new AEntity() { AP1 = 1, AP2 = "hello", B = be });
                    be.BP2.Add(new AEntity() { AP1 = 2, AP2 = "two", B = be });

                    var belist = new List<BEntity> {be};
                    _bei = belist.AsQueryable();
                }

                [When_configuring_a_member_to_skip_based_on_the_property_metadata.Skip]
                [ExpectedException(typeof(StackOverflowException))]
                public void Should_not_throw_exception()
                {
                    _bei.ProjectTo<B>();
                }
            }
        }
#endif
    }
}
