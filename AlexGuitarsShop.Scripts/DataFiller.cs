using AlexGuitarsShop.DAL;
using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.Scripts;

public static class DataFiller
{
    public static Account[] GetAccounts()
    {
        return new Account[]
        {
            new()
            {
                Id = 1,
                Name = "Alex",
                Email = "lex95bond@gmail.com",
                Password = "f969fdbe811d8a66010d6f8973246763147a2a0914afc8087839e29b563a5af0",
                Role = Role.SuperAdmin
            },
            new()
            {
                Id = 2,
                Name = "Anton",
                Email = "Anton@gmail.com",
                Password = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5",
                Role = Role.User
            },
            new()
            {
                Id = 3,
                Name = "Ben",
                Email = "Ben@gmail.com",
                Password = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5",
                Role = Role.User
            },
            new()
            {
                Id = 4,
                Name = "Catrin",
                Email = "Catrin@gmail.com",
                Password = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5",
                Role = Role.User
            },
            new()
            {
                Id = 5,
                Name = "Den",
                Email = "Den@gmail.com",
                Password = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5",
                Role = Role.User
            },
            new()
            {
                Id = 6,
                Name = "Eric",
                Email = "Eric@gmail.com",
                Password = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5",
                Role = Role.User
            },
            new()
            {
                Id = 7,
                Name = "Freddy",
                Email = "Freddy@gmail.com",
                Password = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5",
                Role = Role.User
            },
            new()
            {
                Id = 8,
                Name = "Gwen",
                Email = "Gwen@gmail.com",
                Password = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5",
                Role = Role.User
            },
            new()
            {
                Id = 9,
                Name = "Harry",
                Email = "Harry@gmail.com",
                Password = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5",
                Role = Role.User
            },
            new()
            {
                Id = 10,
                Name = "John",
                Email = "John@gmail.com",
                Password = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5",
                Role = Role.User
            },
            new()
            {
                Id = 11,
                Name = "Olga",
                Email = "Olga@gmail.com",
                Password = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5",
                Role = Role.User
            },
            new()
            {
                Id = 12,
                Name = "Ned",
                Email = "Ned@gmail.com",
                Password = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5",
                Role = Role.User
            }
        };
    }
    
    public static Guitar[] GetGuitars()
    {
        string currentPath = Directory.GetCurrentDirectory();
        string filepath = Path.Combine(currentPath, "products_images");
        return new Guitar[]
        {
            new()
            {
                Id = 1,
                Name = "GIBSON 57 LES PAUL GOLDTOP",
                Price = 5000,
                Description = @"Iconic electric guitar GIBSON 57 LES PAUL GOLDTOP DARKBACK 
                REISSUE VOS DOUBLE GOLD was a favorite instrument of many guitar heroes.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\gibson-57-les-paul-goldtop.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 2,
                Name = "GIBSON ES-335 Figured Sixties Cherry",
                Price = 3000,
                Description = @"The GIBSON ES-335 FIGURED SIXTIES CHERRY semi-acoustic guitar is the perfect combination of shape and 
                functionality.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\gibson-es-335-figured-sixties-cherry.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 3,
                Name = "GIBSON EXPLORER 70s CLASSIC White",
                Price = 1500,
                Description = @"Having been on many stages around the world, the icon of the 70s, 
                the GIBSON EXPLORER 70s CLASSIC WHITE electric guitar, has returned to a new generation of guitarists.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\gibson-explorer-70s-classic-white.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 4,
                Name = "Gibson SG Standard Heritage Cherry",
                Price = 1000,
                Description = @"The GIBSON SG STANDARD HERITAGE CHERRY is an electric guitar immortalized by Santana 
                at Woodstock and smashed on the Townsend stage. The GIBSON SG is a true rock icon.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\gibson-sg-standard-heritage-cherry.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 5,
                Name = "GIBSON FIREBIRD CUSTOM Natural",
                Price = 1200,
                Description = @"The GIBSON FIREBIRD CUSTOM EBONY adds an unmistakable vibe to your electric guitar collection. 
                At the time of its first release in the early 60s, less than a thousand pieces were made, 
                making this instrument a true rare bird.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\gibson-firebird-custom-natural.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 6,
                Name = "FENDER AMERICAN PRO II TELECASTER",
                Price = 1400,
                Description = @"The FENDER AMERICAN PRO II TELECASTER electric guitar draws on over seventy years of innovation, 
                inspiration and evolution to meet the needs of the modern player.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\fender-american-pro-telecaster.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 7,
                Name = "FENDER NOVENTA JAZZMASTER",
                Price = 1100,
                Description = @"Combining classic Fender style and dynamic single-coil pickups, 
                the Noventa series delivers powerful sound, modern playing capabilities and stylish looks.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\fender-noventa-jazzmaster.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 8,
                Name = "Fender Player Stratocaster",
                Price = 1000,
                Description = @"The inspiring sound of the Stratocaster is one of the foundations of Fender. 
                Featuring a classic electric guitar filled with authentic Fender feel and style.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\fender-player-stratocaster.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 9,
                Name = "IBANEZ RG5121 BCF",
                Price = 1250,
                Description = @"The Super Wizard HP 5-ply maple/wenge neck delivers super-smooth playability, 
                while the ebony fingerboard delivers tight lows and mids and signature high-end attack.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\ibanez-rg5121-bcf.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 10,
                Name = "IBANEZ RG370AHMZ BMT",
                Price = 800,
                Description = @"IBANEZ RG370AHMZ BMT is a 6-string electric guitar from the RG line. The model has amazing versatility. 
                It allows you to achieve amazing, very clear and clear sound when playing high-speed solos.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\ibanez-rg370ahmz-bmt.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 11,
                Name = "IBANEZ RGMS8-BK",
                Price = 800,
                Description = @"The Ibanez RGMS8 is an 8-string electric guitar from the popular RG series. 
                The instrument has a powerful, rich tone, which is important for conveying heavy guitar riffs 
                of non-standard electric guitar tuning.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\ibanez-rgms8-bk.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 12,
                Name = "IBANEZ RG8520SLTD NTF",
                Price = 900,
                Description = @"The IBANEZ RG8520SLTD NTF electric guitar is crafted by an elite group of highly skilled craftsmen 
                trained to make instruments of uncompromising quality.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\ibanez-rg8520sltd-ntf.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 13,
                Name = "IBANEZ RG421 MOL",
                Price = 380,
                Description = @"For more than twenty years, Ibanez RG guitars have proudly been the kings of metal guitars. 
                Ibanez RG guitars have evolved side by side with the very genre of metal they were intended for.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\ibanez-rg421-mol.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 14,
                Name = "Mayones Regius 6 Gothic",
                Price = 2000,
                Description = @"An expressive version of the Regius series.This series presents high - quality,
                good - sounding electric guitars that excel in rock music.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\mayones-regius-6-gothic.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 15,
                Name = "Mayones Regius PRO 6",
                Price = 2500,
                Description = @"With great sound and looks, excellent construction and high - quality components,
                this is an instrument that you should definitely try for yourself.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\mayones-regius-pro-6-gothic.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 16,
                Name = "CORT KX507 Multi Scale",
                Price = 750,
                Description = @"The KX500MS, the first multi - scale guitar designed by Cort in 2018,
                has received worldwide recognition from thousands of heavy metal enthusiasts
                and progressive guitarists.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\cort-kx507-multi-scale.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 17,
                Name = "CORT CR250",
                Price = 400,
                Description = @"The Classic Rock series of electric guitars is all about the design,
                feel and performance of vintage Golden Era electric guitars that most players love so much.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\cort-cr250.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 18,
                Name = "CORT KX-5",
                Price = 195,
                Description = @"The KX Series is  for the thoroughly modern player who also appreciates
                the best of time - tested classic design and features.Double - cutaway design with easy access to
                the 24 - fret neck, the KX models are sonically as powerful as they look.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\cort-kx-5.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 19,
                Name = "CORT X-2",
                Price = 150,
                Description = @"Cort X - 2 is a versatile model that is ideal for any guitarist, whether
                it is an experienced person or a completely green beginner.
                Already with one appearance, the guitar says that it is intended for fans ofspeed.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\cort-x-2.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 20,
                Name = "CORT X-5",
                Price = 180,
                Description = @"X Series electric guitars are designed for speed. Aggressive body
                contours and a comfortable neck - to - body
                connection help you play quickly and easily.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\cort-x-5.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 21,
                Name = "B.C.Rich WARCLOCK MK1",
                Price = 1750,
                Description = @"The B.C.Rich Warlock 7 String Electric Guitar features hard tail construction
                with a satin natural painted neck for an easy, speedy
                feel.With fretwork and
                Playability unrivaled at their price point, the Mk1 Series bring an amazing
                level of quality to the entry level player.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\bc-rich-warlock-mk1.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 22,
                Name = "B.C.Rich WARBEAST",
                Price = 1750,
                Description = @"B.C.Rich guitars always stuck out of the crowd for their uniqueness
                and innovative solutions.
                With a body that combines the Warlock and Beast shapes, the Warbeast is an epically B.C.Rich style guitar.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\bc-rich-warbeast.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 23,
                Name = "B.C.Rich IRONBIRD MK-2",
                Price = 2800,
                Description = @"B.C.Rich guitars always stuck out of the crowd for their uniqueness
                and innovative solutions.This 50th Anniversary of the company brings another bold push toward 
                the future of modern guitar making.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\bc-rich-ironbird-mk2.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 24,
                Name = "B.C.Rich MOCKINGBIRD",
                Price = 1900,
                Description = @"The Mockingbird model was designed by B.C.Rich in 1976 and was popular
                from the moment of its release.The September 2010 issue of Guitar World ranked the Mockingbird
                as “the coolest guitar of all time, ” ahead of a number of models with a longer legacy.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\bc-rich-mockinbird.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 25,
                Name = "BC RICH BICH",
                Price = 3500,
                Description = @"This is a well-made unique 10 - string guitar for a highly affordable price.
                This guitar has features like neck - through - body construction, great sounding humbuckers,
                a unique 10 - string set up and a super comfortable neck.The guitar is also light-weight 
                adding to its comfort level.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\bc-rich-bich.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 26,
                Name = "JACKSON JS32T Kelly",
                Price = 500,
                Description = @"Fast, deadly and affordable, the JACKSON JS32T Kelly AH Viola Burst electric guitar takes a giant leap
                forward, making the classic Jackson sound, look and play accessible like never before.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\jackson-js32t-kelly.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 27,
                Name = "JACKSON RR3 RHOADS",
                Price = 1460,
                Description = @"The high - tech electric guitar continues the legacy of inspiring shredding guitars from around the world.
                This one combines a versatile tone with classic rock style and innovative technology to create a purebred
                guitar that proudly displays the deep Jackson pedigree.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\jackson-rr3-roads.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 28,
                Name = "ESP E-II ARROW",
                Price = 2370,
                Description = @"The E - II Standard Series are instruments designed and built by ESPs experienced Japanese luthiers
                that capture all of his soul and craftsmanship in creating the finest guitars and basses.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\esp-e-2-arrow.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 29,
                Name = "ESP E-II ECLIPSE FT",
                Price = 2400,
                Description = @"The E - II Eclipse is one of the most beautiful, loudest and highest quality singlecut guitars
                ever built in Japan at the ESP factory.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\esp-e-2-eclipse-ft.jpg")}",
                IsDeleted = 0
            },
            new()
            {
                Id = 30,
                Name = "LTD F-200",
                Price = 700,
                Description = @"The guitars in the LTD 200 series offer great value and deliver higher quality
                than any other instrument in its price range.",
                Image = $"{ImageConverter.GetBase64String(filepath + "\\ltd_f-200_black_satin.jpg")}",
                IsDeleted = 0
            }
        };
    }
}