using Core.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Data.Seed
{
    public static class ProductAndBrandSeed
    {
        public static async Task SeedProducts(StoreContext context)
        {
            //if (!context.Brands.Any())
            //{
            //    var newBrands = new List<Brand>
            //    {
            //        new Brand { Name = "Adidas",UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
            //            CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7", },
            //        new Brand { Name = "Activ",UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
            //            CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7", },
            //        new Brand { Name = "Nike",UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
            //            CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7", },
            //        new Brand { Name = "Puma",UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
            //            CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7", },
            //    };

            //    await context.Brands.AddRangeAsync(newBrands);
            //}
            if (!context.Products.Any())
            {
                var newProducts = new List<Product>
                {
                    new Product
                    {
                        CountInStock = 80,
                        AverageRating = 0,
                        Name = "WOMEN LIFESTYLE RUN 70S SHOES",
                        BrandId = 25,
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/14/827751/1.jpg?0253",
                        NumReviews = 0,
                        Description = "This is Adidas women lifestyle shoes that is very comfortable",
                        OriginalPrice = 480.9m,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        PriceAfterDiscount = 420,
                    },
                    new Product
                    {
                        CountInStock = 36,
                        AverageRating = 0,
                        Name = "Adidas RUN FALCON 2.0 SHOES GX8240",
                        BrandId = 25,
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/63/540962/1.jpg?5568",
                        NumReviews = 0,
                        Description = "Available in all sizes",
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        OriginalPrice = 236.5m,
                        PriceAfterDiscount = 210,
                    },
                    new Product
                    {
                        CountInStock = 70,
                        AverageRating = 0,
                        Name = "Adidas WOMEN RUNNING Energy Cloud V Shoes",
                        BrandId = 25,
                        Image = "https://eg.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/64/827751/3.jpg?0253",
                        NumReviews = 0,
                        Description = "New And Comfortable shoes",
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        OriginalPrice = 440,
                        PriceAfterDiscount = 410,
                    },
                    new Product
                    {
                        CountInStock = 56,
                        AverageRating = 0,
                        Name = "Adidas Men • Running X9000L2 SHOES S23649",
                        BrandId = 25,
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/88/874622/1.jpg?0205",
                        NumReviews = 0,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        Description = "This is Adidas men lifestyle shoes that is very comfortable",
                        OriginalPrice = 200,
                        PriceAfterDiscount = 180,
                    },
                    new Product
                    {
                        CountInStock = 78,
                        AverageRating = 0,
                        Name = "Activ Bi-Tone Polka Dots Lime & Black Lace Up Sneakers",
                        BrandId = 26,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        Image = "https://www.jumia.com.eg/activ-bi-tone-polka-dots-lime-black-lace-up-sneakers-25942209.html",
                        NumReviews = 0,
                        Description = "Activ is one of the most proprietary brands in the sports fields. We are adhering to be existed as a strong supporter of the various kinds of athletic activities. Not only we became a sponsor of many football teams, young champions, local championships, and it is not in the football game only, but also we are sponsors of basket balls, tennis and the Olympics delegations too. In Addition, Activ is very unique in providing great collection of Casual Shoes, bags, belts, wallets and under-wears.",
                        OriginalPrice = 600,
                        PriceAfterDiscount = 480.5m,
                    },
                    new Product
                    {
                        CountInStock = 20,
                        AverageRating = 0,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        Name = "Activ Bi-Tone Polka Dots Orange & Navy Blue Lace Up Sneakers",
                        BrandId = 26,
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/97/224952/1.jpg?1869",
                        NumReviews = 0,
                        Description = "Activ is one of the most proprietary brands in the sports fields. We are adhering to be existed as a strong supporter of the various kinds of athletic activities. Not only we became a sponsor of many football teams, young champions, local championships,",
                        OriginalPrice = 250.7m,
                        PriceAfterDiscount = 230,
                    },
                    new Product
                    {
                        CountInStock = 90,
                        AverageRating = 0,
                        Name = "Activ Decorative Side Stitches Lace Up Casual Shoes - Brown",
                        BrandId = 26,
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/06/195632/1.jpg?2925",
                        NumReviews = 0,
                        Description = "Activ is one of the most proprietary brands in the sports fields. We are adhering to be existed as a strong supporter of the various kinds of athletic activities. Not only we became a sponsor of many football teams, young champions, local championships, and it is not in the football game only, but also we are sponsors of basket balls, tennis and the Olympics delegations too. In Addition",
                        OriginalPrice = 170,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        PriceAfterDiscount = 140.5m,
                    },
                    new Product
                    {
                        CountInStock = 60,
                        AverageRating = 0,
                        Name = "Activ Lace Up Bi-Tone Mesh Sneakers - Black & Fuchsia",
                        BrandId = 26,
                        Image = "https://eg.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/97/677332/1.jpg?7759",
                        NumReviews = 0,
                        Description = "Activ is one of the most proprietary brands in the sports fields. We are adhering to be existed as a strong supporter of the various kinds of athletic activities. Not only we became a sponsor of many football teams, young champions, local championships, and it is not in the football game only, but also we are sponsors of basket balls, tennis and the Olympics delegations too. In Addition, Activ is very unique in providing great collection of Casual Shoes, bags, belts, wallets and under-wears.",
                        OriginalPrice = 355,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        PriceAfterDiscount = 350.99m,
                    },
                    new Product
                    {
                        CountInStock = 45,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        AverageRating = 0,
                        Name = "Nike Air Max Excee CD4165 009 Particle Grey/White/Black",
                        BrandId = 27,
                        Image = "https://eg.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/41/229072/1.jpg?7452",
                        NumReviews = 0,
                        Description = "Nike air-max sneaker \n Inspired by the Nike Air Max 90, the Nike Air Max Excee is a celebration of a classic through a new lens. Elongated lines and distorted proportions on the upper bring the ’90s look you love into a new, modern space.",
                        OriginalPrice = 180,
                        PriceAfterDiscount = 160.5m,
                    },
                    new Product
                    {
                        CountInStock = 8,
                        AverageRating = 0,
                        Name = "Nike CROSS TRAINING LEGEND ESSENTIAL 2 SHOES CQ9356 018",
                        BrandId = 27,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/89/506372/1.jpg?3352",
                        NumReviews = 0,
                        Description = "Breathable textile upper with synthetic overlays Eyelets with secure lace up closure React foam midsole provides soft",
                        OriginalPrice = 220,
                        PriceAfterDiscount = 220,
                    },
                    new Product
                    {
                        CountInStock = 10,
                        AverageRating = 0,
                        Name = "Nike Reposto Shoes CZ5631 009 Grey",
                        BrandId = 27,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/51/750472/1.jpg?5028",
                        NumReviews = 0,
                        Description = "Mixed synthetic and fabric upper provides lightweight durability with added stretch for flexible comfort.\n The thick foam midsole offers plush cushioning underfoot for a smooth ride. \n 'NKE 72' branding on the midsole nods to the year Nike was established, honouring a strong heritage rooted in sports \n .Zig - zag stitching adds a vintage shoemaker's finish to a sleek, modern design. \n Rubber modified Waffle outsole provides multi - surface traction.",
                        OriginalPrice = 2000,
                        PriceAfterDiscount = 1650,
                    },
                    new Product
                    {
                        CountInStock = 21,
                        AverageRating = 0,
                        Name = "Comfortable Women Sneakers Sport Shoes Latest Model For Women - White",
                        BrandId = 27,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/76/716142/1.jpg?6943",
                        NumReviews = 0,
                        Description = "Flexable Leather material \n Comes with secure lace-up closure \n Sturdy rubber outsole offers added comfort \n Soft footed ensures all-day comfort",
                        OriginalPrice = 210,
                        PriceAfterDiscount = 190,
                    },
                    new Product
                    {
                        CountInStock = 78,
                        AverageRating = 0,
                        Name = "Puma Nova Funky Shoes 37013102",
                        BrandId = 28,
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/31/182052/1.jpg?4326",
                        NumReviews = 0,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        Description = "'Only See Great' will explore the career path of our brand ambassadors, as they will talk about their own journey to striving for greatness, listening to their hearts, and finding a vision that no one else can see.",
                        OriginalPrice = 230,
                        PriceAfterDiscount = 230,
                    },
                    new Product
                    {
                        CountInStock = 21,
                        AverageRating = 0,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        Name = "Puma Kids Carina L White 37067806",
                        BrandId = 28,
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/52/681262/1.jpg?8306",
                        NumReviews = 0,
                        Description = "80's inspired, genuine leather upper with perforations \n Eyelets with secure lacing system \n SoftFoam+ comfort sockliner for long - lasting comfort that provides soft cushioning \n Patterned rubber outsole for better grip \n Contrast PUMA signature branding",
                        OriginalPrice = 440,
                        PriceAfterDiscount = 200,
                    },
                    new Product
                    {
                        CountInStock = 21,
                        AverageRating = 0,
                        Name = "Puma Rs - Trophy Shoes 36936205",
                        BrandId = 28,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/49/303372/1.jpg?7035",
                        NumReviews = 0,
                        Description = "Durable and lightweight synthetic upper with stitched edges",
                        OriginalPrice = 440,
                        PriceAfterDiscount = 200,
                    },
                    new Product
                    {
                        CountInStock = 25,
                        AverageRating = 0,
                        Name = "Puma Storm.Y Metallic Shoes 37141202",
                        BrandId = 28,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/81/182052/1.jpg?4390",
                        NumReviews = 0,
                        Description = "Cushioned footbed for added comfort",
                        OriginalPrice = 1500,
                        PriceAfterDiscount = 1449,
                    },
                    new Product
                    {
                        CountInStock = 4,
                        AverageRating = 0,
                        Name = "Puma Lqdcell Shatter Xt Shift Shoes 19263101",
                        BrandId = 28,
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/42/182052/1.jpg?4509",
                        NumReviews = 0,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        Description = "Semi knitted breathable textile upper",
                        OriginalPrice = 220,
                        PriceAfterDiscount = 220,
                    },
                    new Product
                    {
                        CountInStock = 65,
                        AverageRating = 0,
                        Name = "Adidas Men's • Essentials Kaptir 2.0 Shoes H00279",
                        BrandId = 25,
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/08/804742/1.jpg?2980",
                        NumReviews = 0,
                        Description = "Running can be as much about comfort as it is about style. These adidas running-inspired shoes have a sculpted Cloudfoam midsole that provides pillow-soft comfort. A knit upper and a bold adidas logo complete the look.",
                        OriginalPrice = 220,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        PriceAfterDiscount = 210,
                    },
                    new Product
                    {
                        CountInStock = 25,
                        AverageRating = 0,
                        Name = "Adidas Kids Unisex • Basketball HOOPS MID SHOES GW6110",
                        BrandId = 28,
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/77/640962/1.jpg?6165",
                        NumReviews = 0,
                        Description = "Add some court-inspired style to your young B-ball fan's wardrobe. These kids' adidas sneakers bring the support and stability of a basketball shoe to an everyday sneaker. The synthetic leather upper gives them an elevated look while keeping the shoes lightweight enough for easy movement. A mid-top silhouette hearkens back to the golden age of basketball greats.",
                        OriginalPrice = 130,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        PriceAfterDiscount = 100,
                    },
                    new Product
                    {
                        CountInStock = 50,
                        AverageRating = 0,
                        Name = "Activ Leather Black Lace Up Casual Shoes",
                        BrandId = 26,
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/71/277952/1.jpg?8251",
                        NumReviews = 0,
                        Description = "Activ is one of the most proprietary brands in the sports fields. We are adhering to be existed as a strong supporter of the various kinds of athletic activities.",
                        OriginalPrice = 1209,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        PriceAfterDiscount = 1200,
                    },
                    new Product
                    {
                        CountInStock = 12,
                        AverageRating = 0,
                        Name = "Activ Decorative Lace Slip On Comfy Sneakers - Light Grey",
                        BrandId = 26,
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/43/835252/1.jpg?6987",
                        NumReviews = 0,
                        Description = "Activ is one of the most proprietary brands in the sports fields. We are adhering to be existed as a strong supporter of the various kinds of athletic activities.",
                        OriginalPrice = 200,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        PriceAfterDiscount = 200,
                    },
                    new Product
                    {
                        CountInStock = 25,
                        AverageRating = 0,
                        Name = "Nike Shoes Nike PICO 5 (TDV) AR4162006",
                        BrandId = 26,
                        Image = "https://eg.jumia.is/unsafe/fit-in/680x680/filters:fill(white)/product/28/087362/1.jpg?8849",
                        NumReviews = 0,
                        UpdatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        CreatedBy = "fbde8e4d-1bcd-4063-9cd3-72b3927916a7",
                        Description = "A comfortable two-velcro fastening will make it easier for your child to put on and take off his shoes on his own. The loop sewn on the back of the upper will also be helpful.",
                        OriginalPrice = 600,
                        PriceAfterDiscount = 550,
                    },
                };
                await context.Products.AddRangeAsync(newProducts);
            }
            try
            {

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }
        }
    }
}
